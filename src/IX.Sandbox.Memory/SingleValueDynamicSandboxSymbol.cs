// <copyright file="SingleValueDynamicSandboxSymbol.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Threading;
using IX.Sandbox.Memory.Exceptions;
using IX.StandardExtensions.ComponentModel;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A dynamic symbol for a single sandbox value.
    /// </summary>
    /// <seealso cref="IX.Sandbox.Memory.SandboxSymbol" />
    public class SingleValueDynamicSandboxSymbol : SandboxSymbol
    {
        private SandboxValue value;
        private SmartDisposableWeakReference<SandboxHeap> heap;

        internal SingleValueDynamicSandboxSymbol(SandboxHeap heap, string name)
            : base(name)
        {
            this.heap = new SmartDisposableWeakReference<SandboxHeap>(heap);
        }

        /// <summary>
        /// Gets or sets the raw object that is represented by this value.
        /// </summary>
        /// <value>The raw object.</value>
        public override object RawObject
        {
            get => this.value?.RawObject;

            set
            {
                if (!this.heap.TryGetTarget(out SandboxHeap h))
                {
                    throw new SandboxHeapDisposedException();
                }

                if (value == null)
                {
                    Interlocked.Exchange(ref this.value, null)?.ReleaseOne();
                }
                else
                {
                    if (value != this.value.RawObject)
                    {
                        Interlocked.Exchange(ref this.value, h.CreateSandboxValue(value.GetType(), value))?.ReleaseOne();
                        this.value.CaptureOne();
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
            get => this.value?.StringRepresentation;

            set
            {
                if (!this.heap.TryGetTarget(out SandboxHeap h))
                {
                    throw new SandboxHeapDisposedException();
                }

                if (value == null)
                {
                    Interlocked.Exchange(ref this.value, null)?.ReleaseOne();
                }
                else
                {
                    SandboxValue[] stringInterpretationResults = h.CreateSandboxValue(value);

                    if (stringInterpretationResults.Length == 0)
                    {
                        Interlocked.Exchange(ref this.value, null)?.ReleaseOne();
                    }
                    else if (stringInterpretationResults.Length >= 1)
                    {
                        throw new ValueTypeNotSupportedException(Resources.TheValueTypeIsNotSuppportedSinceAnAttemptWasMadeToAssignAnArrayOfValuesToASingleValueSymbol);
                    }

                    if (stringInterpretationResults[0] != this.value.RawObject)
                    {
                        Interlocked.Exchange(ref this.value, h.CreateSandboxValue(value.GetType(), value))?.ReleaseOne();
                        this.value.CaptureOne();
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
            Interlocked.Exchange(ref this.value, null)?.ReleaseOne();

            base.DisposeGeneralContext();
        }
    }
}