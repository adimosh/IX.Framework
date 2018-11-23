// <copyright file="ReaderWriterSynchronizedBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IX.StandardExtensions.ComponentModel;
using IX.System.Threading;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A base class for a reader/writer synchronized class.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    [DataContract(Namespace = Constants.DataContractNamespace)]
    public abstract partial class ReaderWriterSynchronizedBase : DisposableBase
    {
        private readonly bool lockInherited;

#pragma warning disable IDISP008 // Don't assign member with injected and created disposables. - This is the specific working case of this class
#pragma warning disable IDISP002 // Dispose member.
#pragma warning disable IDISP006 // Implement IDisposable.
        private IReaderWriterLock locker;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.
#pragma warning restore IDISP008 // Don't assign member with injected and created disposables.

        [DataMember]
        private TimeSpan lockerTimeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterSynchronizedBase"/> class.
        /// </summary>
        protected ReaderWriterSynchronizedBase()
        {
            this.locker = new ReaderWriterLockSlim();
            this.lockerTimeout = EnvironmentSettings.LockAcquisitionTimeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterSynchronizedBase"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <exception cref="ArgumentNullException"><paramref name="locker"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        protected ReaderWriterSynchronizedBase(IReaderWriterLock locker)
        {
            this.locker = locker ?? throw new ArgumentNullException(nameof(locker));
            this.lockInherited = true;
            this.lockerTimeout = EnvironmentSettings.LockAcquisitionTimeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterSynchronizedBase" /> class.
        /// </summary>
        /// <param name="timeout">The lock timeout duration.</param>
        protected ReaderWriterSynchronizedBase(TimeSpan timeout)
        {
            this.locker = new ReaderWriterLockSlim();
            this.lockerTimeout = timeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterSynchronizedBase"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="timeout">The lock timeout duration.</param>
        /// <exception cref="ArgumentNullException"><paramref name="locker"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        protected ReaderWriterSynchronizedBase(IReaderWriterLock locker, TimeSpan timeout)
        {
            this.locker = locker ?? throw new ArgumentNullException(nameof(locker));
            this.lockInherited = true;
            this.lockerTimeout = timeout;
        }

        /// <summary>
        /// Called when the object is being deserialized, in order to set the locker to a new value.
        /// </summary>
        /// <param name="context">The streaming context.</param>
        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
            => global::System.Threading.Interlocked.Exchange(ref this.locker, new ReaderWriterLockSlim());

        /// <summary>
        /// Produces a reader lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected ReadOnlySynchronizationLocker ReadLock()
        {
            this.ThrowIfCurrentObjectDisposed();

            return new ReadOnlySynchronizationLocker(this.locker, this.lockerTimeout);
        }

        /// <summary>
        /// Invokes using a reader lock.
        /// </summary>
        /// <param name="action">An action that is called.</param>
        protected void ReadLock(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            using (new ReadOnlySynchronizationLocker(this.locker, this.lockerTimeout))
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

            using (new ReadOnlySynchronizationLocker(this.locker, this.lockerTimeout))
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

            return new WriteOnlySynchronizationLocker(this.locker, this.lockerTimeout);
        }

        /// <summary>
        /// Invokes using a writer lock.
        /// </summary>
        /// <param name="action">An action that is called.</param>
        protected void WriteLock(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            using (new WriteOnlySynchronizationLocker(this.locker, this.lockerTimeout))
            {
                action();
            }
        }

        /// <summary>
        /// Invokes using a writer lock.
        /// </summary>
        /// <typeparam name="T">The type of item to return.</typeparam>
        /// <param name="action">An action that is called.</param>
        /// <returns>The generated item.</returns>
        protected T WriteLock<T>(Func<T> action)
        {
            this.ThrowIfCurrentObjectDisposed();

            using (new WriteOnlySynchronizationLocker(this.locker, this.lockerTimeout))
            {
                return action();
            }
        }

        /// <summary>
        /// Produces an upgradeable reader lock in concurrent collections.
        /// </summary>
        /// <returns>A disposable object representing the lock.</returns>
        protected ReadWriteSynchronizationLocker ReadWriteLock()
        {
            this.ThrowIfCurrentObjectDisposed();

            return new ReadWriteSynchronizationLocker(this.locker, this.lockerTimeout);
        }

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            if (!this.lockInherited)
            {
#pragma warning disable IDISP007 // Don't dispose injected. - Injection is checked in the line above.
                this.locker.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.
            }

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Spawns an atomic enumerator tied to this instance's locking mechanisms.
        /// </summary>
        /// <typeparam name="TItem">The type of the item within the atomic enumerator.</typeparam>
        /// <typeparam name="TEnumerator">The type of the enumerator to spawn the atomic enumerator from.</typeparam>
        /// <param name="sourceEnumerator">The source enumerator.</param>
        /// <returns>An tied-in atomic enumerator.</returns>
        protected AtomicEnumerator<TItem, TEnumerator> SpawnAtomicEnumerator<TItem, TEnumerator>(in TEnumerator sourceEnumerator)
            where TEnumerator : IEnumerator<TItem>
        {
            this.ThrowIfCurrentObjectDisposed();

            return new AtomicEnumerator<TItem, TEnumerator>(sourceEnumerator, this.ReadLock);
        }
    }
}