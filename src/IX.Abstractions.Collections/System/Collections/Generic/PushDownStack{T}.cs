// <copyright file="PushDownStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using IX.Abstractions.Collections;
using IX.StandardExtensions.Threading;

namespace IX.System.Collections.Generic
{
    /// <summary>
    /// A stack that pushes down extra items above a certain limit.
    /// </summary>
    /// <typeparam name="T">The stack item type.</typeparam>
    /// <seealso cref="IStack{T}" />
    public class PushDownStack<T> : IStack<T>
    {
        /// <summary>
        /// The limit.
        /// </summary>
        private int limit;

        /// <summary>
        /// The locking object.
        /// </summary>
        private ReaderWriterLockSlim locker;

        /// <summary>
        /// The internal container.
        /// </summary>
        private List<T> internalContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PushDownStack{T}"/> class.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <exception cref="IX.Abstractions.Collections.LimitArgumentNegativeException">limit</exception>
        public PushDownStack(int limit)
        {
            if (limit < 0)
            {
                throw new LimitArgumentNegativeException(nameof(limit));
            }

            this.limit = limit;

            this.internalContainer = new List<T>();
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Gets the number of elements in the observable stack.
        /// </summary>
        /// <value>The count.</value>
        public int Count => this.ReadLock(() => this.internalContainer.Count);

        /// <summary>
        /// Gets or sets the number of items in the push-down stack.
        /// </summary>
        public int Limit
        {
            get => this.limit;
            set
            {
                if (value < 0)
                {
                    throw new LimitArgumentNegativeException();
                }

                this.limit = value;

                if (value != 0)
                {
                    while (this.internalContainer.Count > value)
                    {
                        this.internalContainer.RemoveAt(0);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        /// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
        public bool IsSynchronized => ((ICollection)this.internalContainer).IsSynchronized;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The synchronize root.</value>
        public object SyncRoot => ((ICollection)this.internalContainer).SyncRoot;

        /// <summary>
        /// Clears the observable stack.
        /// </summary>
        public void Clear() => this.WriteLock(() => this.internalContainer.Clear());

        /// <summary>
        /// Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><c>true</c> if the item was found, <c>false</c> otherwise.</returns>
        public bool Contains(T item) => this.ReadLock(() => this.internalContainer.Contains(item));

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(Array array, int index) => this.ReadLock(() => ((ICollection)this.internalContainer).CopyTo(array, index));

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => new AtomicEnumerator<T>(this.internalContainer.GetEnumerator(), () => new ReadOnlySynchronizationLocker(this.locker));

        /// <summary>
        /// Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek() => this.ReadLock(() => (this.internalContainer.Count > 0) ? this.internalContainer[this.internalContainer.Count - 1] : default(T));

        /// <summary>
        /// Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop() => this.WriteLock(() =>
        {
            var index = this.internalContainer.Count - 1;

            if (index < 0)
            {
                return default(T);
            }

            T item = this.internalContainer[index];

            this.internalContainer.RemoveAt(index);

            return item;
        });

        /// <summary>
        /// Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item) => this.WriteLock(() =>
        {
            if (this.internalContainer.Count == this.limit && this.limit != 0)
            {
                this.internalContainer.RemoveAt(0);
            }

            this.internalContainer.Add(item);
        });

        /// <summary>
        /// Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        public T[] ToArray() => this.ReadLock(() => this.internalContainer.ToArray());

        /// <summary>
        /// Sets the capacity to the actual number of elements in the stack if that number is less than 90 percent of current capacity.
        /// </summary>
        public void TrimExcess()
        {
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Invokes an action under a reader lock.
        /// </summary>
        /// <param name="lockedAction">The action to invoke under lock.</param>
        /// <exception cref="TimeoutException">The lock could not be obtained in the timeout period.</exception>
        private void ReadLock(Action lockedAction)
        {
            if (!this.locker.TryEnterReadLock(Constants.ConcurrentLockAcquisitionTimeout))
            {
                throw new TimeoutException();
            }

            try
            {
                lockedAction();
            }
            finally
            {
                this.locker.ExitReadLock();
            }
        }

        /// <summary>
        /// Invokes an action under a reader lock.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="lockedAction">The action to invoke under lock.</param>
        /// <returns>The invocation result.</returns>
        /// <exception cref="TimeoutException">The lock could not be obtained in the timeout period.</exception>
        private TResult ReadLock<TResult>(Func<TResult> lockedAction)
        {
            if (!this.locker.TryEnterReadLock(Constants.ConcurrentLockAcquisitionTimeout))
            {
                throw new TimeoutException();
            }

            try
            {
                return lockedAction();
            }
            finally
            {
                this.locker.ExitReadLock();
            }
        }

        /// <summary>
        /// Invokes an action under a writer lock.
        /// </summary>
        /// <param name="lockedAction">The action to invoke under lock.</param>
        /// <exception cref="TimeoutException">The lock could not be obtained in the timeout period.</exception>
        private void WriteLock(Action lockedAction)
        {
            if (!this.locker.TryEnterWriteLock(Constants.ConcurrentLockAcquisitionTimeout))
            {
                throw new TimeoutException();
            }

            try
            {
                lockedAction();
            }
            finally
            {
                this.locker.ExitWriteLock();
            }
        }

        /// <summary>
        /// Invokes an action under a writer lock.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="lockedAction">The action to invoke under lock.</param>
        /// <returns>The invocation result.</returns>
        /// <exception cref="TimeoutException">The lock could not be obtained in the timeout period.</exception>
        private TResult WriteLock<TResult>(Func<TResult> lockedAction)
        {
            if (!this.locker.TryEnterWriteLock(Constants.ConcurrentLockAcquisitionTimeout))
            {
                throw new TimeoutException();
            }

            try
            {
                return lockedAction();
            }
            finally
            {
                this.locker.ExitWriteLock();
            }
        }
    }
}