// <copyright file="FirePeriodicallyContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.StandardExtensions.ComponentModel;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A ticker delegate.
    /// </summary>
    /// <param name="tick">The tick index.</param>
    /// <param name="interruptor">The interruptor object, should the periodical firing mechanism be requested to be interrupted.</param>
    /// <param name="state">The state.</param>
    public delegate void FirePeriodicallyTicker(int tick, IInterruptible interruptor, object state);

    internal sealed class FirePeriodicallyContext : DisposableBase, IInterruptible
    {
        private readonly object state;
        private readonly TimeSpan timeSpan;

#pragma warning disable IDISP002 // Dispose member.
#pragma warning disable IDISP006 // Implement IDisposable.
        private Timer timer;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.

        private int iteration;

        internal FirePeriodicallyContext(FirePeriodicallyTicker tickerDelegate, object state, int milliseconds)
            : this(tickerDelegate, state, TimeSpan.FromMilliseconds(milliseconds))
        {
        }

        internal FirePeriodicallyContext(FirePeriodicallyTicker tickerDelegate, object state, TimeSpan timeSpan)
            : this(tickerDelegate, state, TimeSpan.Zero, timeSpan)
        {
        }

        internal FirePeriodicallyContext(FirePeriodicallyTicker tickerDelegate, object state, TimeSpan initialDelay, TimeSpan timeSpan)
        {
            this.state = state;
            this.timeSpan = timeSpan;
            this.timer = new Timer(this.TimerTick, tickerDelegate ?? throw new ArgumentNullException(nameof(tickerDelegate)), initialDelay, timeSpan);
        }

        /// <summary>
        /// Interrupts this instance.
        /// </summary>
        public void Interrupt() => this.timer.Change(Timeout.Infinite, Timeout.Infinite);

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume() => this.timer.Change(TimeSpan.Zero, this.timeSpan);

        protected override void DisposeGeneralContext()
        {
            base.DisposeGeneralContext();

            Interlocked.Exchange(ref this.timer, null)?.Dispose();
        }

        private void TimerTick(object stateObject)
        {
            var ticker = (FirePeriodicallyTicker)stateObject;

            Interlocked.Increment(ref this.iteration);

            ticker(this.iteration, this, this.state);
        }
    }
}