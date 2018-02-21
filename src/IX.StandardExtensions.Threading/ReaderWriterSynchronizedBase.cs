// <copyright file="ReaderWriterSynchronizedBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.ComponentModel;
using IX.System.Threading;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A base class for a reader/writer synchronized class.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    public abstract partial class ReaderWriterSynchronizedBase : DisposableBase
    {
        private readonly bool lockInherited;
        private readonly IReaderWriterLock locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterSynchronizedBase"/> class.
        /// </summary>
        protected ReaderWriterSynchronizedBase()
        {
            this.locker = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterSynchronizedBase"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <exception cref="ArgumentNullException">locker</exception>
        protected ReaderWriterSynchronizedBase(IReaderWriterLock locker)
        {
            this.locker = locker ?? throw new ArgumentNullException(nameof(locker));
            this.lockInherited = true;
        }

        /// <summary>
        /// Produces a reader lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected ReadOnlySynchronizationLocker ReadLock()
        {
            this.ThrowIfCurrentObjectDisposed();

            return new ReadOnlySynchronizationLocker(this.locker);
        }

        /// <summary>
        /// Invokes using a reader lock.
        /// </summary>
        /// <param name="action">An action that is called.</param>
        protected void ReadLock(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            using (new ReadOnlySynchronizationLocker(this.locker))
            {
                action();
            }
        }

        /// <summary>
        /// Gets a result from an invoker using a reader lock.
        /// </summary>
        /// <param name="action">An action that is called to get the result.</param>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <returns>A disposable object representing the lock.</returns>
        protected T ReadLock<T>(Func<T> action)
        {
            this.ThrowIfCurrentObjectDisposed();

            using (new ReadOnlySynchronizationLocker(this.locker))
            {
                return action();
            }
        }

        /// <summary>
        /// Produces a writer lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected WriteOnlySynchronizationLocker WriteLock()
        {
            this.ThrowIfCurrentObjectDisposed();

            return new WriteOnlySynchronizationLocker(this.locker);
        }

        /// <summary>
        /// Invokes using a writer lock.
        /// </summary>
        /// <param name="action">An action that is called.</param>
        protected void WriteLock(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            using (new WriteOnlySynchronizationLocker(this.locker))
            {
                action();
            }
        }

        /// <summary>
        /// Produces an upgradeable reader lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected ReadWriteSynchronizationLocker ReadWriteLock()
        {
            this.ThrowIfCurrentObjectDisposed();

            return new ReadWriteSynchronizationLocker(this.locker);
        }

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            if (!this.lockInherited)
            {
                this.locker.Dispose();
            }

            base.DisposeManagedContext();
        }
    }
}