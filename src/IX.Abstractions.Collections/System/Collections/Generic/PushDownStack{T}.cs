// <copyright file="PushDownStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IX.Abstractions.Collections;
using IX.StandardExtensions.Threading;
using IX.System.Threading;

namespace IX.System.Collections.Generic
{
    /// <summary>
    /// A stack that pushes down extra items above a certain limit.
    /// </summary>
    /// <typeparam name="T">The stack item type.</typeparam>
    /// <seealso cref="IStack{T}" />
    [DataContract(Namespace = Constants.DataContractNamespace, Name = "PushDownStackOf{0}")]
    public class PushDownStack<T> : IStack<T>, IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// The limit.
        /// </summary>
        private int limit;

        /// <summary>
        /// The locking object.
        /// </summary>
        private IReaderWriterLock locker;

#pragma warning disable IDE0044 // Add readonly modifier - Cannot do that, as the field is deserializable
        /// <summary>
        /// The internal container.
        /// </summary>
        [DataMember(Name = "Items")]
        private List<T> internalContainer;
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Initializes a new instance of the <see cref="PushDownStack{T}"/> class.
        /// </summary>
        public PushDownStack()
            : this(Constants.DefaultPushDownLimit)
        {
        }

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
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="PushDownStack{T}"/> class.
        /// </summary>
        ~PushDownStack()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the number of elements in the observable stack.
        /// </summary>
        /// <value>The count.</value>
        public int Count => this.InvokeIfNotDisposed((cThis) => cThis.ReadLock((c2This) => c2This.internalContainer.Count, cThis), this);

        /// <summary>
        /// Gets or sets the number of items in the push-down stack.
        /// </summary>
        [DataMember]
        public int Limit
        {
            get => this.limit;
            set
            {
                this.ThrowIfCurrentObjectDisposed();

                if (value < 0)
                {
                    throw new LimitArgumentNegativeException();
                }

                this.WriteLock(
                    (val, cThis) =>
                    {
                        cThis.limit = val;

                        if (val != 0)
                        {
                            while (cThis.internalContainer.Count > val)
                            {
                                cThis.internalContainer.RemoveAt(0);
                            }
                        }
                        else
                        {
                            cThis.internalContainer.Clear();
                        }
                    },
                    value,
                    this);
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
        public void Clear() => this.InvokeIfNotDisposed(() => this.WriteLock(this.internalContainer.Clear));

        /// <summary>
        /// Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><c>true</c> if the item was found, <c>false</c> otherwise.</returns>
        public bool Contains(T item) => this.InvokeIfNotDisposed((itemL2) => this.ReadLock((itemL1) => this.internalContainer.Contains(itemL1), itemL2), item);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(Array array, int index) => this.InvokeIfNotDisposed((arrayL2, indexL2) => this.ReadLock((arrayL1, indexL1) => ((ICollection)this.internalContainer).CopyTo(arrayL1, indexL1), arrayL2, indexL2), array, index);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => this.InvokeIfNotDisposed(() => new AtomicEnumerator<T>(this.internalContainer.GetEnumerator(), () => new ReadOnlySynchronizationLocker(this.locker ?? (this.locker = new ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy.NoRecursion)))));

        /// <summary>
        /// Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek() => this.InvokeIfNotDisposed(() => this.ReadLock(() => (this.internalContainer.Count > 0) ? this.internalContainer[this.internalContainer.Count - 1] : default));

        /// <summary>
        /// Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop() => this.InvokeIfNotDisposed(
            () =>
            {
                if (this.limit == 0)
                {
                    return default;
                }

                return this.WriteLock(
                    () =>
                    {
                        var index = this.internalContainer.Count - 1;

                        if (index < 0)
                        {
                            return default;
                        }

                        T item = this.internalContainer[index];

                        this.internalContainer.RemoveAt(index);

                        return item;
                    });
            });

        /// <summary>
        /// Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item) =>
            this.InvokeIfNotDisposed(
                (itemL2, cThis) =>
                {
                    if (cThis.limit == 0)
                    {
                        return;
                    }

                    cThis.WriteLock(
                        (itemL1, c2This) =>
                        {
                            if (c2This.internalContainer.Count == c2This.limit)
                            {
                                c2This.internalContainer.RemoveAt(0);
                            }

                            c2This.internalContainer.Add(itemL1);
                        },
                        itemL2,
                        cThis);
                },
                item,
                this);

        /// <summary>
        /// Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        public T[] ToArray() => this.InvokeIfNotDisposed(() => this.ReadLock(() => this.internalContainer.ToArray()));

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
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Throws if the current object is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the current object is disposed, this exception will be thrown.</exception>
        protected void ThrowIfCurrentObjectDisposed()
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke();
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>The object returned by the action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TReturn>(Func<TReturn> action)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke();
        }

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected virtual void DisposeManagedContext()
        {
            this.locker?.Dispose();
            this.internalContainer.Clear();
        }

        /// <summary>
        /// Disposes the general (managed and unmanaged) context.
        /// </summary>
        protected virtual void DisposeGeneralContext()
        {
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1>(Action<TParam1> action, TParam1 param1)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TReturn>(Func<TParam1, TReturn> action, TParam1 param1)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2);
        }

        /// <summary>
        /// Invokes an action under a reader lock.
        /// </summary>
        /// <typeparam name="TArgument1">The type of the argument at index 0.</typeparam>
        /// <typeparam name="TArgument2">The type of the argument at index 1.</typeparam>
        /// <param name="lockedAction">The action to invoke under lock.</param>
        /// <param name="argument1">The argument at index 0.</param>
        /// <param name="argument2">The argument at index 1.</param>
        /// <exception cref="TimeoutException">The lock could not be obtained in the timeout period.</exception>
        private void ReadLock<TArgument1, TArgument2>(Action<TArgument1, TArgument2> lockedAction, TArgument1 argument1, TArgument2 argument2)
        {
            if (!(this.locker ?? (this.locker = new ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy.NoRecursion))).TryEnterReadLock(Constants.ConcurrentLockAcquisitionTimeout))
            {
                throw new TimeoutException();
            }

            try
            {
                lockedAction(argument1, argument2);
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
            if (!(this.locker ?? (this.locker = new ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy.NoRecursion))).TryEnterReadLock(Constants.ConcurrentLockAcquisitionTimeout))
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
        /// Invokes an action under a reader lock.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="lockedAction">The action to invoke under lock.</param>
        /// <param name="argument">The argument.</param>
        /// <returns>The invocation result.</returns>
        /// <exception cref="TimeoutException">The lock could not be obtained in the timeout period.</exception>
        private TResult ReadLock<TArgument, TResult>(Func<TArgument, TResult> lockedAction, TArgument argument)
        {
            if (!(this.locker ?? (this.locker = new ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy.NoRecursion))).TryEnterReadLock(Constants.ConcurrentLockAcquisitionTimeout))
            {
                throw new TimeoutException();
            }

            try
            {
                return lockedAction(argument);
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
            if (!(this.locker ?? (this.locker = new ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy.NoRecursion))).TryEnterWriteLock(Constants.ConcurrentLockAcquisitionTimeout))
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
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <typeparam name="TArgument2">The type of the argument2.</typeparam>
        /// <param name="lockedAction">The action to invoke under lock.</param>
        /// <param name="argument">The argument.</param>
        /// <param name="argument2">The argument2.</param>
        /// <exception cref="TimeoutException">The lock could not be obtained in the timeout period.</exception>
        private void WriteLock<TArgument, TArgument2>(Action<TArgument, TArgument2> lockedAction, TArgument argument, TArgument2 argument2)
        {
            if (!(this.locker ?? (this.locker = new ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy.NoRecursion))).TryEnterWriteLock(Constants.ConcurrentLockAcquisitionTimeout))
            {
                throw new TimeoutException();
            }

            try
            {
                lockedAction(argument, argument2);
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
            if (!(this.locker ?? (this.locker = new ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy.NoRecursion))).TryEnterWriteLock(Constants.ConcurrentLockAcquisitionTimeout))
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

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.DisposeManagedContext();
                }

                this.DisposeGeneralContext();

                this.disposedValue = true;
            }
        }
    }
}