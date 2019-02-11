// <copyright file="ManualResetEventSlim.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.System.Threading
{
    /// <summary>
    /// A set/reset event class that implements methods to block and unblock threads based on manual signal interaction.
    /// </summary>
    /// <seealso cref="IX.System.Threading.ISetResetEvent" />
    [PublicAPI]
    public class ManualResetEventSlim : ISetResetEvent
    {
        private readonly bool eventLocal;

#pragma warning disable IDISP008 // Don't assign member with injected and created disposables. - This is how this class works
        /// <summary>
        /// The manual reset event.
        /// </summary>
        private readonly global::System.Threading.ManualResetEventSlim sre;
#pragma warning restore IDISP008 // Don't assign member with injected and created disposables.

        /// <summary>
        /// A value that is used to detect redundant calls to <see cref="Dispose()"/>.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualResetEventSlim"/> class.
        /// </summary>
        public ManualResetEventSlim()
        {
            this.sre = new global::System.Threading.ManualResetEventSlim();
            this.eventLocal = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualResetEventSlim" /> class.
        /// </summary>
        /// <param name="initialState">The initial signal state.</param>
        public ManualResetEventSlim(bool initialState)
        {
            this.sre = new global::System.Threading.ManualResetEventSlim(initialState);
            this.eventLocal = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualResetEventSlim"/> class.
        /// </summary>
        /// <param name="manualResetEvent">The manual reset event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="manualResetEvent"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public ManualResetEventSlim(global::System.Threading.ManualResetEventSlim manualResetEvent)
        {
            this.sre = manualResetEvent ?? throw new ArgumentNullException(nameof(manualResetEvent));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ManualResetEventSlim"/> class.
        /// </summary>
        ~ManualResetEventSlim()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(false);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ManualResetEventSlim"/> to <see cref="global::System.Threading.ManualResetEventSlim"/>.
        /// </summary>
        /// <param name="manualResetEvent">The manual reset event.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator global::System.Threading.ManualResetEventSlim(ManualResetEventSlim manualResetEvent) => manualResetEvent.sre;

        /// <summary>
        /// Performs an implicit conversion from <see cref="global::System.Threading.ManualResetEventSlim"/> to <see cref="ManualResetEventSlim"/>.
        /// </summary>
        /// <param name="manualResetEvent">The manual reset event.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ManualResetEventSlim(global::System.Threading.ManualResetEventSlim manualResetEvent) => new ManualResetEventSlim(manualResetEvent);

        /// <summary>
        /// Sets the state of this event instance to non-signaled. Any thread entering a wait from this point will block.
        /// </summary>
        /// <returns><see langword="true"/> if the signal has been reset, <see langword="false"/> otherwise.</returns>
        public bool Reset()
        {
            this.sre.Reset();
            return true;
        }

        /// <summary>
        /// Sets the state of this event instance to signaled. Any waiting thread will unblock.
        /// </summary>
        /// <returns><see langword="true"/> if the signal has been set, <see langword="false"/> otherwise.</returns>
        public bool Set()
        {
            this.sre.Set();
            return true;
        }

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        public void WaitOne() => this.sre.Wait();

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(int millisecondsTimeout) => this.sre.Wait(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(double millisecondsTimeout) => this.sre.Wait(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(TimeSpan timeout) => this.sre.Wait(timeout);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <see langword="true"/>, the synchronization domain is exited before the call.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(int millisecondsTimeout, bool exitSynchronizationDomain) =>
            this.sre.Wait(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <see langword="true"/>, the synchronization domain is exited before the call.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(double millisecondsTimeout, bool exitSynchronizationDomain) =>
            this.sre.Wait(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <param name="exitSynchronizationDomain">If set to <see langword="true"/>, the synchronization domain is exited before the call.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(TimeSpan timeout, bool exitSynchronizationDomain) =>
            this.sre.Wait(timeout);

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
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue)
            {
                return;
            }

            if (this.eventLocal)
            {
#pragma warning disable IDISP007 // Don't dispose injected. - We do the injection check above
                this.sre.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.
            }

            this.disposedValue = true;
        }
    }
}