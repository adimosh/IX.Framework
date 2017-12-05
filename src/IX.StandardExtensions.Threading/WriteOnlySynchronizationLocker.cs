// <copyright file="WriteOnlySynchronizationLocker.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A write-only synchronization locker.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.Threading.SynchronizationLocker" />
    public class WriteOnlySynchronizationLocker : SynchronizationLocker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteOnlySynchronizationLocker"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public WriteOnlySynchronizationLocker(ReaderWriterLockSlim locker)
            : this(locker, EnvironmentSettings.LockAcquisitionTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteOnlySynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="lockAcquisitionTimeout">The express lock acquisition timeout.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public WriteOnlySynchronizationLocker(ReaderWriterLockSlim locker, int lockAcquisitionTimeout)
            : this(locker, TimeSpan.FromMilliseconds(lockAcquisitionTimeout))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteOnlySynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="lockAcquisitionTimeoutMilliseconds">The express lock acquisition timeout.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public WriteOnlySynchronizationLocker(ReaderWriterLockSlim locker, double lockAcquisitionTimeoutMilliseconds)
            : this(locker, TimeSpan.FromMilliseconds(lockAcquisitionTimeoutMilliseconds))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteOnlySynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="lockAcquisitionTimespan">The lock acquisition timespan.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public WriteOnlySynchronizationLocker(ReaderWriterLockSlim locker, TimeSpan lockAcquisitionTimespan)
            : base(locker)
        {
            if (!locker?.TryEnterWriteLock(lockAcquisitionTimespan) ?? false)
            {
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// Releases the currently-held lock.
        /// </summary>
        public override void Dispose()
        {
            if (this.Locker != null)
            {
                if (this.Locker.IsWriteLockHeld)
                {
                    this.Locker.ExitWriteLock();
                }
            }
        }
    }
}