// <copyright file="ManualResetEvent.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.System.Threading
{
    /// <summary>
    /// A set/reset event class that implements methods to block and unblock threads based on manual signal interaction.
    /// </summary>
    /// <seealso cref="IX.System.Threading.ISetResetEvent" />
    public class ManualResetEvent : ISetResetEvent
    {
        /// <summary>
        /// The manual reset event.
        /// </summary>
        private global::System.Threading.ManualResetEvent sre;

        /// <summary>
        /// A value that is used to detect redundant calls to <see cref="Dispose()"/>.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualResetEvent"/> class.
        /// </summary>
        public ManualResetEvent()
            : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualResetEvent" /> class.
        /// </summary>
        /// <param name="initialState">The initial signal state.</param>
        public ManualResetEvent(bool initialState)
        {
            this.sre = new global::System.Threading.ManualResetEvent(initialState);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ManualResetEvent"/> class.
        /// </summary>
        ~ManualResetEvent()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(false);
        }

        /// <summary>
        /// Sets the state of this event instance to non-signaled. Any thread entering a wait from this point will block.
        /// </summary>
        /// <returns><c>true</c> if the signal has been reset, <c>false</c> otherwise.</returns>
        public bool Reset() => this.sre.Reset();

        /// <summary>
        /// Sets the state of this event instance to signaled. Any waiting thread will unblock.
        /// </summary>
        /// <returns><c>true</c> if the signal has been set, <c>false</c> otherwise.</returns>
        public bool Set() => this.sre.Set();

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        public void WaitOne() => this.sre.WaitOne();

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        public void WaitOne(int millisecondsTimeout) => this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        public void WaitOne(double millisecondsTimeout) => this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        public void WaitOne(TimeSpan timeout) => this.sre.WaitOne(timeout);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <c>true</c>, the synchronization domain is exited before the call.</param>
        public void WaitOne(int millisecondsTimeout, bool exitSynchronizationDomain) =>
#if NET45
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout), exitSynchronizationDomain);
#else
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));
#endif

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <c>true</c>, the synchronization domain is exited before the call.</param>
        public void WaitOne(double millisecondsTimeout, bool exitSynchronizationDomain) =>
#if NET45
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout), exitSynchronizationDomain);
#else
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));
#endif

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <param name="exitSynchronizationDomain">If set to <c>true</c>, the synchronization domain is exited before the call.</param>
        public void WaitOne(TimeSpan timeout, bool exitSynchronizationDomain) =>
#if NET45
            this.sre.WaitOne(timeout, exitSynchronizationDomain);
#else
            this.sre.WaitOne(timeout);
#endif

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                this.sre.Dispose();

                this.disposedValue = true;
            }
        }
    }
}