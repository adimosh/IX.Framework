// <copyright file="AtomicEnumerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using IX.StandardExtensions.ComponentModel;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    ///     An atomic enumerator that can enumerate items one at a time, atomically.
    /// </summary>
    /// <typeparam name="TItem">The type of the items to enumerate.</typeparam>
    /// <typeparam name="TEnumerator">The type of the enumerator from which this atomic enumerator is derived.</typeparam>
    /// <seealso cref="IEnumerator{T}" />
    [PublicAPI]
    public sealed class AtomicEnumerator<TItem, TEnumerator> : DisposableBase, IEnumerator<TItem>
        where TEnumerator : IEnumerator<TItem>
    {
        private TItem current;
        private TEnumerator existingEnumerator;
        private bool movedNext;
        private Func<ReadOnlySynchronizationLocker> readLock;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AtomicEnumerator{TItem, TEnumerator}" /> class.
        /// </summary>
        /// <param name="existingEnumerator">The existing enumerator. This argument is passed by reference.</param>
        /// <param name="readLock">The read lock.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="existingEnumerator" />
        ///     or
        ///     <paramref name="readLock" />
        ///     is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public AtomicEnumerator(
            [NotNull] TEnumerator existingEnumerator,
            [NotNull] Func<ReadOnlySynchronizationLocker> readLock)
        {
            if (existingEnumerator == null)
            {
                throw new ArgumentNullException(nameof(existingEnumerator));
            }

            this.existingEnumerator = existingEnumerator;

            Contract.RequiresNotNull(ref this.readLock, readLock, nameof(readLock));
        }

        /// <summary>
        ///     Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The current element.</value>
        public TItem Current
        {
            get
            {
                if (!this.movedNext)
                {
                    throw new InvalidOperationException(Resources.MoveNextNotInvoked);
                }

                this.ThrowIfCurrentObjectDisposed();

                return this.current;
            }
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - We cannot do anything about this at this time
        /// <summary>
        ///     Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The current element.</value>
        object IEnumerator.Current => this.Current;
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

        /// <summary>
        ///     Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the enumerator was successfully advanced to the next element;
        ///     <see langword="false" /> if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            this.ThrowIfCurrentObjectDisposed();

            bool result;
            using (this.readLock())
            {
                ref TEnumerator localEnumerator = ref this.existingEnumerator;
                result = localEnumerator.MoveNext();

                this.movedNext = true;

                if (result)
                {
                    this.current = localEnumerator.Current;
                }
            }

            return result;
        }

        /// <summary>
        ///     Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            // DO NOT CHANGE the order of these operations!
            this.movedNext = false;

            this.ThrowIfCurrentObjectDisposed();

            this.existingEnumerator.Reset();
            this.current = default;
        }

        /// <summary>
        ///     Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            base.DisposeManagedContext();

#pragma warning disable IDISP007 // Don't dispose injected. - Ownership of the enumerator is required
            this.existingEnumerator.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.

            Interlocked.Exchange(
                ref this.readLock,
                null);
        }
    }
}