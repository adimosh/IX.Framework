// <copyright file="DynamicQueueSandboxSymbol.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Concurrent;
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
    public class DynamicQueueSandboxSymbol : SandboxSymbol
    {
        private ConcurrentQueue<SandboxValue> value;
        private SmartDisposableWeakReference<SandboxHeap> heap;

        internal DynamicQueueSandboxSymbol(SandboxHeap heap, string name)
            : base(name)
        {
            this.heap = new SmartDisposableWeakReference<SandboxHeap>(heap);
            this.value = new ConcurrentQueue<SandboxValue>();
        }

        /// <summary>
        /// Gets or sets the raw object that is represented by this value.
        /// </summary>
        /// <value>The raw object.</value>
        public override object RawObject
        {
            get
            {
                SandboxValue item = null;
                if (!(this.value?.TryDequeue(out item) ?? true))
                {
                    Interlocked.Exchange(ref this.value, null);
                }

                return item?.RawObject;
            }

            set
            {
                if (!this.heap.TryGetTarget(out SandboxHeap h))
                {
                    throw new SandboxHeapDisposedException();
                }

                if (value != null)
                {
                    var newValue = new ConcurrentQueue<SandboxValue>();

#pragma warning disable IDISP004 // Don't ignore return value of type IDisposable. - They're not, this is a false positive
                    if (EnvironmentSettings.TreatSpecialArraysAsMultipleElements)
                    {
                        switch (value)
                        {
                            case IEnumerable enumerable:
                                {
                                    foreach (var p in enumerable)
                                    {
                                        newValue.Enqueue(h.CreateSandboxValue(p));
                                    }
                                }

                                break;
                            default:
                                {
                                    newValue.Enqueue(h.CreateSandboxValue(value));
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
                                    newValue.Enqueue(h.CreateSandboxValue(ba));
                                }

                                break;
                            case char[] ca:
                                {
                                    newValue.Enqueue(h.CreateSandboxValue(ca));
                                }

                                break;
                            case IEnumerable enumerable:
                                {
                                    foreach (var p in enumerable)
                                    {
                                        newValue.Enqueue(h.CreateSandboxValue(p));
                                    }
                                }

                                break;
                            default:
                                {
                                    newValue.Enqueue(h.CreateSandboxValue(value));
                                }

                                break;
                        }
                    }
#pragma warning restore IDISP004 // Don't ignore return value of type IDisposable.

                    ConcurrentQueue<SandboxValue> oldValue = Interlocked.Exchange(ref this.value, newValue);

                    if (this.value != null)
                    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                        foreach (SandboxValue currentValue in this.value)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                        {
                            currentValue.CaptureOne();
                        }
                    }

                    if (oldValue != null)
                    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                        foreach (SandboxValue o in oldValue)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
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

                ConcurrentQueue<SandboxValue> newValue = null;
                SandboxValue[] newValues = h.CreateSandboxValue(value);

                if (newValues != null && newValues.Length > 0)
                {
                    newValue = new ConcurrentQueue<SandboxValue>(newValues);
                }

                ConcurrentQueue<SandboxValue> oldValue = Interlocked.Exchange(ref this.value, newValue);

                if (this.value != null)
                {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                    foreach (SandboxValue currentValue in this.value)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                    {
                        currentValue.CaptureOne();
                    }
                }

                if (oldValue != null)
                {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                    foreach (SandboxValue o in oldValue)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
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
            ConcurrentQueue<SandboxValue> oldValue = Interlocked.Exchange(ref this.value, null);

            if (oldValue != null)
            {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                foreach (SandboxValue o in oldValue)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                {
                    o.ReleaseOne();
                }
            }

            base.DisposeGeneralContext();
        }
    }
}