﻿// <copyright file="ObservableBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System;
using System.Threading;
using IX.StandardExtensions.ComponentModel;
using IX.StandardExtensions.Threading;

namespace IX.Observable
{
    /// <summary>
    /// A base class for collections that are observable.
    /// </summary>
    /// <seealso cref="NotifyCollectionChangedInvokerBase" />
    public abstract class ObservableBase : NotifyCollectionChangedInvokerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableBase"/> class.
        /// </summary>
        protected ObservableBase()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableBase"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        protected ObservableBase(SynchronizationContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets a synchronization lock item to be used when trying to synchronize read/write operations between threads.
        /// </summary>
        /// <remarks>
        /// <para>On non-concurrent collections, this should be left <c>null</c> (<c>Nothing</c> in Visual Basic).</para>
        /// <para>On concurrent collections, this should be overridden to return an instance. All read/write operations on the underlying constructs should rely on
        /// the same instance of <see cref="ReaderWriterLockSlim"/> that is returned here to synchronize.</para>
        /// </remarks>
        protected virtual ReaderWriterLockSlim SynchronizationLock => null;

        /// <summary>
        /// Produces a reader lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected ReadOnlySynchronizationLocker ReadLock() => new ReadOnlySynchronizationLocker(this.SynchronizationLock);

        /// <summary>
        /// Invokes using a reader lock.
        /// </summary>
        /// <param name="invoker">An invoker that is called.</param>
        protected void ReadLock(Action invoker)
        {
            using (new ReadOnlySynchronizationLocker(this.SynchronizationLock))
            {
                invoker();
            }
        }

        /// <summary>
        /// Gets a result from an invoker using a reader lock.
        /// </summary>
        /// <param name="invoker">An invoker that is called to get the result.</param>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <returns>A disposable object representing the lock.</returns>
        protected T ReadLock<T>(Func<T> invoker)
        {
            using (new ReadOnlySynchronizationLocker(this.SynchronizationLock))
            {
                return invoker();
            }
        }

        /// <summary>
        /// Produces a writer lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected WriteOnlySynchronizationLocker WriteLock() => new WriteOnlySynchronizationLocker(this.SynchronizationLock);

        /// <summary>
        /// Invokes using a writer lock.
        /// </summary>
        /// <param name="invoker">An invoker that is called.</param>
        protected void WriteLock(Action invoker)
        {
            using (new WriteOnlySynchronizationLocker(this.SynchronizationLock))
            {
                invoker();
            }
        }

        /// <summary>
        /// Produces an upgradeable reader lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected ReadWriteSynchronizationLocker ReadWriteLock() => new ReadWriteSynchronizationLocker(this.SynchronizationLock);

        /// <summary>
        /// Checks whether or not this object is disposed and throws an <see cref="ObjectDisposedException" />, and, if not, invokes an action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ObjectDisposedException">This instance is disposed.</exception>
        protected void CheckDisposed(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            action();
        }

        /// <summary>
        /// Checks whether or not this object is disposed and throws an <see cref="ObjectDisposedException" />, and, if not, invokes an action and returns its result.
        /// </summary>
        /// <typeparam name="T">The return type of the action.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>The result from the action.</returns>
        /// <exception cref="ObjectDisposedException">This instance is disposed.</exception>
        protected T CheckDisposed<T>(Func<T> action)
        {
            this.ThrowIfCurrentObjectDisposed();

            return action();
        }
    }
}