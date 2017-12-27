// <copyright file="ReaderWriterLockSlim.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.StandardExtensions.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace IX.System.Threading
{
    /// <summary>
    /// A wrapper over <see cref="global::System.Threading.ReaderWriterLockSlim"/>, compatible with <see cref="IReaderWriterLock"/>.
    /// </summary>
    /// <seealso cref="IX.System.Threading.IReaderWriterLock" />
    public class ReaderWriterLockSlim : DisposableBase, IReaderWriterLock
    {
        private global::System.Threading.ReaderWriterLockSlim locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterLockSlim"/> class.
        /// </summary>
        public ReaderWriterLockSlim()
        {
            this.locker = new global::System.Threading.ReaderWriterLockSlim();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterLockSlim"/> class.
        /// </summary>
        /// <param name="lockRecursionPolicy">The lock recursion policy.</param>
        public ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy lockRecursionPolicy)
        {
            this.locker = new global::System.Threading.ReaderWriterLockSlim(lockRecursionPolicy);
        }

        /// <summary>
        /// Gets a value indicating whether the current thread has a write lock held.
        /// </summary>
        /// <value><c>true</c> if the current thread has a write lock held; otherwise, <c>false</c>.</value>
        public bool IsWriteLockHeld => this.InvokeIfNotDisposed((lck) => lck.IsReadLockHeld, this.locker);

        /// <summary>
        /// Gets a value indicating whether the current thread has an upgradeable lock held.
        /// </summary>
        /// <value><c>true</c> if the current thread has an upgradeable lock held; otherwise, <c>false</c>.</value>
        public bool IsUpgradeableReadLockHeld => this.InvokeIfNotDisposed((lck) => lck.IsUpgradeableReadLockHeld, this.locker);

        public bool IsReadLockHeld => throw new NotImplementedException();

        public void EnterReadLock() => throw new NotImplementedException();
        public void EnterUpgradeableReadLock() => throw new NotImplementedException();
        public void EnterWriteLock() => throw new NotImplementedException();
        public void ExitReadLock() => throw new NotImplementedException();
        public void ExitUpgradeableReadLock() => throw new NotImplementedException();
        public void ExitWriteLock() => throw new NotImplementedException();
        public bool TryEnterReadLock(int millisecondsTimeout) => throw new NotImplementedException();
        public bool TryEnterReadLock(TimeSpan timeout) => throw new NotImplementedException();
        public bool TryEnterUpgradeableReadLock(int millisecondsTimeout) => throw new NotImplementedException();
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout) => throw new NotImplementedException();
        public bool TryEnterWriteLock(int millisecondsTimeout) => throw new NotImplementedException();
        public bool TryEnterWriteLock(TimeSpan timeout) => throw new NotImplementedException();
    }
}