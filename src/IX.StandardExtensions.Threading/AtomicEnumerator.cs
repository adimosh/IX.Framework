// <copyright file="AtomicEnumerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// An atomic enumerator that can enumerate items one at a time, atomically.
    /// </summary>
    /// <typeparam name="T">The type of the items to enumerate.</typeparam>
    /// <seealso cref="global::System.Collections.Generic.IEnumerator{T}" />
    public sealed class AtomicEnumerator<T> : IEnumerator<T>
    {
        private IEnumerator<T> existingEnumerator;
        private Func<ReadOnlySynchronizationLocker> readLock;
        private bool disposedValue;
        private bool movedNext;

        private T current;

        /// <summary>
        /// Initializes a new instance of the <see cref="AtomicEnumerator{T}"/> class.
        /// </summary>
        /// <param name="existingEnumerator">The existing enumerator.</param>
        /// <param name="readLock">The read lock.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="existingEnumerator"/>
        /// or
        /// <paramref name="readLock"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </exception>
        public AtomicEnumerator(IEnumerator<T> existingEnumerator, Func<ReadOnlySynchronizationLocker> readLock)
        {
            this.existingEnumerator = existingEnumerator ?? throw new ArgumentNullException(nameof(existingEnumerator));
            this.readLock = readLock ?? throw new ArgumentNullException(nameof(readLock));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AtomicEnumerator{T}"/> class.
        /// </summary>
        ~AtomicEnumerator()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The current element.</value>
        public T Current
        {
            get
            {
                if (!this.movedNext)
                {
                    throw new InvalidOperationException(Resources.MoveNextNotInvoked);
                }

                if (this.disposedValue)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                return this.current;
            }
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The current element.</value>
        object IEnumerator.Current => this.Current;

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            bool result;
            using (this.readLock())
            {
                result = this.existingEnumerator.MoveNext();

                this.movedNext = true;

                if (result)
                {
                    this.current = this.existingEnumerator.Current;
                }
            }

            return result;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            // DO NOT CHANGE the order of these operations!
            this.movedNext = false;

            if (this.disposedValue)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            this.existingEnumerator.Reset();
            this.current = default;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.existingEnumerator.Dispose();
                }

                this.existingEnumerator = null;
                this.readLock = null;

                this.disposedValue = true;
            }
        }
    }
}