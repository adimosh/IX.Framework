// <copyright file="SynchronizationLocker.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A synchronization locker base class.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public abstract class SynchronizationLocker : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationLocker"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        internal SynchronizationLocker(ReaderWriterLockSlim locker)
        {
            this.Locker = locker;
        }

        /// <summary>
        /// Gets the reader/writer lock to use. This property can be <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </summary>
        protected ReaderWriterLockSlim Locker { get; private set; }

        /// <summary>
        /// Releases the currently-held lock.
        /// </summary>
        public abstract void Dispose();
    }
}