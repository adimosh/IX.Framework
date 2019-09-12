// <copyright file="IReaderWriterLock.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.System.Threading
{
    /// <summary>
    /// A service contract for a reader/writer lock.
    /// </summary>
    /// <seealso cref="IDisposable" />
    [PublicAPI]
    public interface IReaderWriterLock : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the current thread has a write lock held.
        /// </summary>
        /// <value><see langword="true"/> if the current thread has a write lock held; otherwise, <see langword="false"/>.</value>
        bool IsWriteLockHeld { get; }

        /// <summary>
        /// Gets a value indicating whether the current thread has an upgradeable lock held.
        /// </summary>
        /// <value><see langword="true"/> if the current thread has an upgradeable lock held; otherwise, <see langword="false"/>.</value>
        bool IsUpgradeableReadLockHeld { get; }

        /// <summary>
        /// Gets a value indicating whether the current thread has a read lock held.
        /// </summary>
        /// <value><see langword="true"/> if the current thread has a read lock held; otherwise, <see langword="false"/>.</value>
        bool IsReadLockHeld { get; }

        /// <summary>
        /// Enters a read lock.
        /// </summary>
        void EnterReadLock();

        /// <summary>
        /// Enters an upgradeable read lock.
        /// </summary>
        void EnterUpgradeableReadLock();

        /// <summary>
        /// Enters the write lock.
        /// </summary>
        void EnterWriteLock();

        /// <summary>
        /// Exits a read lock.
        /// </summary>
        void ExitReadLock();

        /// <summary>
        /// Exits an upgradeable read lock.
        /// </summary>
        void ExitUpgradeableReadLock();

        /// <summary>
        /// Exits a write lock.
        /// </summary>
        void ExitWriteLock();

        /// <summary>
        /// Tries to enter a read lock.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout, in milliseconds.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        bool TryEnterReadLock(int millisecondsTimeout);

        /// <summary>
        /// Tries to enter a read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        bool TryEnterReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter an upgradeable read lock.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout, in milliseconds.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        bool TryEnterUpgradeableReadLock(int millisecondsTimeout);

        /// <summary>
        /// Tries to enter an upgradeable read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        bool TryEnterUpgradeableReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter a write lock.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout, in milliseconds.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        bool TryEnterWriteLock(int millisecondsTimeout);

        /// <summary>
        /// Tries to enter a write lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        bool TryEnterWriteLock(TimeSpan timeout);
    }
}