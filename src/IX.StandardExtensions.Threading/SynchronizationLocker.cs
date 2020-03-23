// <copyright file="SynchronizationLocker.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.System.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    ///     A synchronization locker base class.
    /// </summary>
    /// <seealso cref="IDisposable" />
    [PublicAPI]
    [global::System.Diagnostics.CodeAnalysis.SuppressMessage(
        "IDisposableAnalyzers.Correctness",
        "IDISP025:Class with no virtual dispose method should be sealed.",
        Justification = "This class has an abstract Dispose method and we want it that way.")]
    public abstract class SynchronizationLocker : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SynchronizationLocker" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        internal SynchronizationLocker(IReaderWriterLock locker)
        {
            this.Locker = locker;
        }

        /// <summary>
        ///     Gets the reader/writer lock to use. This property can be <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </summary>
        protected IReaderWriterLock Locker { get; }

        /// <summary>
        ///     Releases the currently-held lock.
        /// </summary>
        public abstract void Dispose();
    }
}