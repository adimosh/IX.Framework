// <copyright file="DynamicListSandboxSymbol.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IX.Sandbox.Memory.Exceptions;
using IX.StandardExtensions.ComponentModel;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A dynamic symbol for a list of sandbox values.
    /// </summary>
    /// <seealso cref="IX.Sandbox.Memory.SandboxSymbol" />
    public class DynamicListSandboxSymbol : SandboxSymbol
    {
        private List<SandboxValue> value;
        private SmartDisposableWeakReference<SandboxHeap> heap;

        internal DynamicListSandboxSymbol(SandboxHeap heap, string name)
            : base(name)
        {
            this.heap = new SmartDisposableWeakReference<SandboxHeap>(heap);
            this.value = new List<SandboxValue>();
        }

        /// <summary>
        /// Gets or sets the raw object that is represented by this value.
        /// </summary>
        /// <value>The raw object.</value>
        public override object RawObject
        {
            get => this.value?.Select(p => p.RawObject).ToArray();

            set
            {
                if (!this.heap.TryGetTarget(out SandboxHeap h))
                {
                    throw new SandboxHeapDisposedException();
                }

                List<SandboxValue> newValue = null;

                if (value != null)
                {
                    newValue = new List<SandboxValue>();

                    if (EnvironmentSettings.TreatSpecialArraysAsMultipleElements)
                    {
                        switch (value)
                        {
                            case IEnumerable enumerable:
                                {
                                    foreach (var p in enumerable)
                                    {
                                        newValue.Add(h.CreateSandboxValue(p));
                                    }
                                }

                                break;
                            default:
                                {
                                    newValue.Add(h.CreateSandboxValue(value));
                                }

                                break;
                        }
                    }
                    else
                    {
                        switch (value)
                        {
                            case byte[] ba:
                                {
                                    newValue.Add(h.CreateSandboxValue(ba));
                                }

                                break;
                            case char[] ca:
                                {
                                    newValue.Add(h.CreateSandboxValue(ca));
                                }

                                break;
                            case IEnumerable enumerable:
                                {
                                    foreach (var p in enumerable)
                                    {
                                        newValue.Add(h.CreateSandboxValue(p));
                                    }
                                }

                                break;
                            default:
                                {
                                    newValue.Add(h.CreateSandboxValue(value));
                                }

                                break;
                        }
                    }

                    List<SandboxValue> oldValue = Interlocked.Exchange(ref this.value, newValue);

                    if (this.value != null)
                    {
                        foreach (SandboxValue currentValue in this.value)
                        {
                            currentValue.CaptureOne();
                        }
                    }

                    if (oldValue != null)
                    {
                        foreach (SandboxValue o in oldValue)
                        {
                            o.ReleaseOne();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the string representation of this value value.
        /// </summary>
        /// <value>The string representation.</value>
        public override string StringRepresentation
        {
            get => string.Join(EnvironmentSettings.EnumerableSeparatorSymbol, this.value?.Select(p => p.StringRepresentation));

            set
            {
                if (!this.heap.TryGetTarget(out SandboxHeap h))
                {
                    throw new SandboxHeapDisposedException();
                }

                List<SandboxValue> newValue = null;
                SandboxValue[] newValues = h.CreateSandboxValue(value);

                if (newValues != null && newValues.Length > 0)
                {
                    newValue = new List<SandboxValue>(newValues);
                }

                List<SandboxValue> oldValue = Interlocked.Exchange(ref this.value, newValue);

                if (this.value != null)
                {
                    foreach (SandboxValue currentValue in this.value)
                    {
                        currentValue.CaptureOne();
                    }
                }

                if (oldValue != null)
                {
                    foreach (SandboxValue o in oldValue)
                    {
                        o.ReleaseOne();
                    }
                }
            }
        }

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            Interlocked.Exchange(ref this.heap, null);
            List<SandboxValue> oldValue = Interlocked.Exchange(ref this.value, null);

            if (oldValue != null)
            {
                foreach (SandboxValue o in oldValue)
                {
                    o.ReleaseOne();
                }
            }

            base.DisposeGeneralContext();
        }
    }
}