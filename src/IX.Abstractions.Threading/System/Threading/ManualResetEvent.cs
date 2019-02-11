// <copyright file="ManualResetEvent.cs" company="Adrian Mos">
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
    public class ManualResetEvent : ISetResetEvent
    {
        private readonly bool eventLocal;

#pragma warning disable IDISP008 // Don't assign member with injected and created disposables. - This is how this class works
        /// <summary>
        /// The manual reset event.
        /// </summary>
        private global::System.Threading.ManualResetEvent sre;
#pragma warning restore IDISP008 // Don't assign member with injected and created disposables.

        /// <summary>
        /// A value that is used to detect redundant calls to <see cref="Dispose()"/>.
        /// </summary>
        private bool disposedValue;

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
            this.eventLocal = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualResetEvent"/> class.
        /// </summary>
        /// <param name="manualResetEvent">The manual reset event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="manualResetEvent"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public ManualResetEvent(global::System.Threading.ManualResetEvent manualResetEvent)
        {
#pragma warning disable IDISP003 // Dispose previous before re-assigning. - Analyzer bug - this is the constructor
            this.sre = manualResetEvent ?? throw new ArgumentNullException(nameof(manualResetEvent));
#pragma warning restore IDISP003 // Dispose previous before re-assigning.
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
        /// Performs an implicit conversion from <see cref="ManualResetEvent"/> to <see cref="global::System.Threading.ManualResetEvent"/>.
        /// </summary>
        /// <param name="manualResetEvent">The manual reset event.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator global::System.Threading.ManualResetEvent(ManualResetEvent manualResetEvent) => manualResetEvent.sre;

        /// <summary>
        /// Performs an implicit conversion from <see cref="global::System.Threading.ManualResetEvent"/> to <see cref="ManualResetEvent"/>.
        /// </summary>
        /// <param name="manualResetEvent">The manual reset event.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ManualResetEvent(global::System.Threading.ManualResetEvent manualResetEvent) => new ManualResetEvent(manualResetEvent);

        /// <summary>
        /// Sets the state of this event instance to non-signaled. Any thread entering a wait from this point will block.
        /// </summary>
        /// <returns><see langword="true"/> if the signal has been reset, <see langword="false"/> otherwise.</returns>
        public bool Reset() => this.sre.Reset();

        /// <summary>
        /// Sets the state of this event instance to signaled. Any waiting thread will unblock.
        /// </summary>
        /// <returns><see langword="true"/> if the signal has been set, <see langword="false"/> otherwise.</returns>
        public bool Set() => this.sre.Set();

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        public void WaitOne() => this.sre.WaitOne();

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(int millisecondsTimeout) => this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(double millisecondsTimeout) => this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(TimeSpan timeout) => this.sre.WaitOne(timeout);

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <see langword="true"/>, the synchronization domain is exited before the call.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(int millisecondsTimeout, bool exitSynchronizationDomain) =>
#if !STANDARD
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout), exitSynchronizationDomain);
#else
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));
#endif

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout period, in milliseconds.</param>
        /// <param name="exitSynchronizationDomain">If set to <see langword="true"/>, the synchronization domain is exited before the call.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(double millisecondsTimeout, bool exitSynchronizationDomain) =>
#if !STANDARD
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout), exitSynchronizationDomain);
#else
            this.sre.WaitOne(TimeSpan.FromMilliseconds(millisecondsTimeout));
#endif

        /// <summary>
        /// Enters a wait period and, should there be no signal set, blocks the thread calling.
        /// </summary>
        /// <param name="timeout">The timeout period.</param>
        /// <param name="exitSynchronizationDomain">If set to <see langword="true"/>, the synchronization domain is exited before the call.</param>
        /// <returns><see langword="true" /> if the event is set within the timeout period, <see langword="false" /> if the timeout is reached.</returns>
        public bool WaitOne(TimeSpan timeout, bool exitSynchronizationDomain) =>
#if !STANDARD
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
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
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
}