// <copyright file="DynamicStackSandboxSymbol.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using IX.Sandbox.Memory.Exceptions;
using IX.StandardExtensions.ComponentModel;
using JetBrains.Annotations;

namespace IX.Sandbox.Memory
{
    /// <summary>
    ///     A dynamic symbol for a list of sandbox values.
    /// </summary>
    /// <seealso cref="IX.Sandbox.Memory.SandboxSymbol" />
    [PublicAPI]
    public class DynamicStackSandboxSymbol : SandboxSymbol
    {
        private SmartDisposableWeakReference<SandboxHeap> heap;
        private ConcurrentStack<SandboxValue> value;

        internal DynamicStackSandboxSymbol(
            SandboxHeap heap,
            string name)
            : base(name)
        {
            this.heap = new SmartDisposableWeakReference<SandboxHeap>(heap);
            this.value = new ConcurrentStack<SandboxValue>();
        }

        /// <summary>
        ///     Gets or sets the raw object that is represented by this value.
        /// </summary>
        /// <value>The raw object.</value>
        public override object RawObject
        {
            get
            {
                SandboxValue item = null;
                if (!(this.value?.TryPop(out item) ?? true))
                {
                    Interlocked.Exchange(
                        ref this.value,
                        null);
                }

                return item?.RawObject;
            }

            set
            {
                if (!this.heap.TryGetTarget(out SandboxHeap h))
                {
                    throw new SandboxHeapDisposedException();
                }

                if (value == null)
                {
                    return;
                }

                var newValue = new ConcurrentStack<SandboxValue>();

#pragma warning disable IDISP004 // Don't ignore return value of type IDisposable. - We're not. We're placing them in a safe disposal concurrent stack
                if (EnvironmentSettings.TreatSpecialArraysAsMultipleElements)
                {
                    switch (value)
                    {
                        case IEnumerable enumerable:
                        {
                            foreach (object p in enumerable)
                            {
                                newValue.Push(h.CreateSandboxValue(p));
                            }
                        }

                            break;
                        default:
                        {
                            newValue.Push(h.CreateSandboxValue(value));
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
                            newValue.Push(h.CreateSandboxValue(ba));
                        }

                            break;
                        case char[] ca:
                        {
                            newValue.Push(h.CreateSandboxValue(ca));
                        }

                            break;
                        case IEnumerable enumerable:
                        {
                            foreach (object p in enumerable)
                            {
                                newValue.Push(h.CreateSandboxValue(p));
                            }
                        }

                            break;
                        default:
                        {
                            newValue.Push(h.CreateSandboxValue(value));
                        }

                            break;
                    }
                }
#pragma warning restore IDISP004 // Don't ignore return value of type IDisposable.

                ConcurrentStack<SandboxValue> oldValue = Interlocked.Exchange(
                    ref this.value,
                    newValue);

                if (this.value != null)
                {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                    foreach (SandboxValue currentValue in this.value)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                    {
                        currentValue.CaptureOne();
                    }
                }

                if (oldValue == null)
                {
                    return;
                }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                foreach (SandboxValue o in oldValue)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                {
                    o.ReleaseOne();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the string representation of this value value.
        /// </summary>
        /// <value>The string representation.</value>
        public override string StringRepresentation
        {
            get => this.value == null
                ? null
                : string.Join(
                    EnvironmentSettings.EnumerableSeparatorSymbol,
                    this.value.Select(p => p.StringRepresentation));

            set
            {
                if (!this.heap.TryGetTarget(out SandboxHeap h))
                {
                    throw new SandboxHeapDisposedException();
                }

                ConcurrentStack<SandboxValue> newValue = null;
                SandboxValue[] newValues = h.CreateSandboxValue(value);

                if (newValues != null && newValues.Length > 0)
                {
                    newValue = new ConcurrentStack<SandboxValue>(newValues);
                }

                ConcurrentStack<SandboxValue> oldValue = Interlocked.Exchange(
                    ref this.value,
                    newValue);

                if (this.value != null)
                {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                    foreach (SandboxValue currentValue in this.value)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                    {
                        currentValue.CaptureOne();
                    }

                    oldValue.Clear();
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
        ///     Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            Interlocked.Exchange(
                ref this.heap,
                null);
            ConcurrentStack<SandboxValue> oldValue = Interlocked.Exchange(
                ref this.value,
                null);

            if (oldValue != null)
            {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Yeah, nothing to do about this.
                foreach (SandboxValue o in oldValue)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                {
                    o.ReleaseOne();
                }

                oldValue.Clear();
            }

            base.DisposeGeneralContext();
        }
    }
}