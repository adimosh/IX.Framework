// <copyright file="ReadOnlySynchronizationLocker.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.System.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    ///     A read-only synchronization locker.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.Threading.SynchronizationLocker" />
    [PublicAPI]
    public class ReadOnlySynchronizationLocker : SynchronizationLocker
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReadOnlySynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public ReadOnlySynchronizationLocker(IReaderWriterLock locker)
            : this(
                locker,
                EnvironmentSettings.LockAcquisitionTimeout)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReadOnlySynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="lockAcquisitionTimeout">The express lock acquisition timeout.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public ReadOnlySynchronizationLocker(
            IReaderWriterLock locker,
            int lockAcquisitionTimeout)
            : this(
                locker,
                TimeSpan.FromMilliseconds(lockAcquisitionTimeout))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReadOnlySynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="lockAcquisitionTimeoutMilliseconds">The express lock acquisition timeout.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public ReadOnlySynchronizationLocker(
            IReaderWriterLock locker,
            double lockAcquisitionTimeoutMilliseconds)
            : this(
                locker,
                TimeSpan.FromMilliseconds(lockAcquisitionTimeoutMilliseconds))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReadOnlySynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="lockAcquisitionTimespan">The lock acquisition timespan.</param>
        /// <exception cref="TimeoutException">The lock could not be acquired in time.</exception>
        public ReadOnlySynchronizationLocker(
            IReaderWriterLock locker,
            TimeSpan lockAcquisitionTimespan)
            : base(locker)
        {
            if (!locker?.TryEnterReadLock(lockAcquisitionTimespan) ?? false)
            {
                throw new TimeoutException();
            }
        }

        /// <summary>
        ///     Releases the currently-held lock.
        /// </summary>
        public override void Dispose()
        {
            if (this.Locker == null)
            {
                return;
            }

            if (this.Locker.IsReadLockHeld)
            {
                this.Locker.ExitReadLock();
            }
        }
    }
}