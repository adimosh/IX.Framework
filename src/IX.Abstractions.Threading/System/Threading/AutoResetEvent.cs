// <copyright file="AutoResetEvent.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.ComponentModel;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.System.Threading
{
    /// <summary>
    ///     A set/reset event class that implements methods to block threads and unblock automatically.
    /// </summary>
    /// <seealso cref="IX.System.Threading.ISetResetEvent" />
    [PublicAPI]
    public class AutoResetEvent : DisposableBase, ISetResetEvent
    {
        private readonly bool eventLocal;

#pragma warning disable IDISP002 // Dispose member. - It is
#pragma warning disable IDISP006 // Implement IDisposable.
        /// <summary>
        ///     The manual reset event.
        /// </summary>
        private readonly global::System.Threading.AutoResetEvent sre;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoResetEvent" /> class.
        /// </summary>
        public AutoResetEvent()
            : this(false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoResetEvent" /> class.
        /// </summary>
        /// <param name="initialState">The initial signal state.</param>
        public AutoResetEvent(bool initialState)
        {
            this.sre = new global::System.Threading.AutoResetEvent(initialState);
            this.eventLocal = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoResetEvent" /> class.
        /// </summary>
        /// <param name="autoResetEvent">The automatic reset event to wrap around.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="autoResetEvent" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public AutoResetEvent(global::System.Threading.AutoResetEvent autoResetEvent)
        {
            Contract.RequiresNotNull(
                ref this.sre,
                autoResetEvent,
                nameof(autoResetEvent));
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="T:System.Threading.AutoResetEvent" /> to
        ///     <see cref="AutoResetEvent" />.
        /// </summary>
        /// <param name="autoResetEvent">The automatic reset event.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator AutoResetEvent(global::System.Threading.AutoResetEvent autoResetEvent) =>
            new AutoResetEvent(autoResetEvent);

        /// <summary>
        ///     Performs an implicit conversion from <see cref="AutoResetEvent" /> to
        ///     <see cref="T:System.Threading.AutoResetEvent" />.
        /// </summary>
        /// <param name="autoResetEvent">The automatic reset event.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator global::System.Threading.AutoResetEvent(AutoResetEvent autoResetEvent) =>
            autoResetEvent.sre;

        /// <summary>
        ///     Sets the state of this event instance to non-signaled. Any thread entering a wait from this point will block.
        /// </summary>
        /// <returns><see langword="true" /> if the signal has been reset, <see langword="false" /> otherwise.</returns>
        public bool Reset() => this.sre.Reset();

        /// <summary>
        ///     Sets the state of this event instance to signaled. Any waiting thread will unblock.
        /// </summary>
        /// <returns><see langword="true" /> if the signal has been set, <see langword="false" /> otherwise.</returns>
        public bool Set() => this.sre.Set();

        /// <summary>
        ///     Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        public void WaitOne() => this.sre.WaitOne();

        /// <summary>
        ///     Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <returns>
        ///     <see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout
        ///     is reached.
        /// </returns>
        public bool WaitOne(int millisecondsTimeout) =>
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        ///     Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <returns>
        ///     <see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout
        ///     is reached.
        /// </returns>
        public bool WaitOne(double millisecondsTimeout) =>
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        ///     Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>
        ///     <see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout
        ///     is reached.
        /// </returns>
        public bool WaitOne(TimeSpan timeout) => this.sre.WaitOne(timeout);

        /// <summary>
        ///     Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">
        ///     If set to <see langword="true" />, the synchronization domain is exited before
        ///     the call.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout
        ///     is reached.
        /// </returns>
        public bool WaitOne(
            int millisecondsTimeout,
            bool exitSynchronizationDomain) =>
#if !STANDARD
            this.sre.WaitOne(
                TimeSpan.FromMilliseconds(millisecondsTimeout),
                exitSynchronizationDomain);
#else
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));
#endif

        /// <summary>
        ///     Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">
        ///     If set to <see langword="true" />, the synchronization domain is exited before
        ///     the call.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout
        ///     is reached.
        /// </returns>
        public bool WaitOne(
            double millisecondsTimeout,
            bool exitSynchronizationDomain) =>
#if !STANDARD
            this.sre.WaitOne(
                TimeSpan.FromMilliseconds(millisecondsTimeout),
                exitSynchronizationDomain);
#else
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));
#endif

        /// <summary>
        ///     Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <param name="exitSynchronizationDomain">
        ///     If set to <see langword="true" />, the synchronization domain is exited before
        ///     the call.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout
        ///     is reached.
        /// </returns>
        public bool WaitOne(
            TimeSpan timeout,
            bool exitSynchronizationDomain) =>
#if !STANDARD
            this.sre.WaitOne(
                timeout,
                exitSynchronizationDomain);
#else
            this.sre.WaitOne(timeout);
#endif

        /// <summary>
        ///     Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            base.DisposeManagedContext();

            if (this.eventLocal)
            {
                this.sre.Dispose();
            }
        }
    }
}