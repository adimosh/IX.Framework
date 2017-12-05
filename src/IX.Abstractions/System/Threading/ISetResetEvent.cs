// <copyright file="ISetResetEvent.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.System.Threading
{
    /// <summary>
    /// An abstraction contract for set/reset events.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface ISetResetEvent : IDisposable
    {
        /// <summary>
        /// Sets the state of this event instance to signaled. Any waiting thread will unblock.
        /// </summary>
        /// <returns><c>true</c> if the signal has been set, <c>false</c> otherwise.</returns>
        bool Set();

        /// <summary>
        /// Sets the state of this event instance to non-signaled. Any thread entering a wait from this point will block.
        /// </summary>
        /// <returns><c>true</c> if the signal has been reset, <c>false</c> otherwise.</returns>
        bool Reset();

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        void WaitOne();

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        void WaitOne(int millisecondsTimeout);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        void WaitOne(double millisecondsTimeout);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        void WaitOne(TimeSpan timeout);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <c>true</c>, the synchronization domain is exited before the call.</param>
        void WaitOne(int millisecondsTimeout, bool exitSynchronizationDomain);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <c>true</c>, the synchronization domain is exited before the call.</param>
        void WaitOne(double millisecondsTimeout, bool exitSynchronizationDomain);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <param name="exitSynchronizationDomain">If set to <c>true</c>, the synchronization domain is exited before the call.</param>
        void WaitOne(TimeSpan timeout, bool exitSynchronizationDomain);
    }
}