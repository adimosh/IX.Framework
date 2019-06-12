// <copyright file="Fire.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading.Tasks;
using IX.StandardExtensions.Contracts;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class that provides methods and extensions to fire events.
    /// </summary>
    public static partial class Fire
    {
        /// <summary>
        /// Runs the specified ticker delegate periodically.
        /// </summary>
        /// <param name="tickerDelegate">The ticker delegate.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>An interruptible handle for external control of the ticker.</returns>
        public static IInterruptible Periodically(FirePeriodicallyTicker tickerDelegate, int milliseconds) => new FirePeriodicallyContext(tickerDelegate, null, milliseconds);

        /// <summary>
        /// Runs the specified ticker delegate periodically.
        /// </summary>
        /// <param name="tickerDelegate">The ticker delegate.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>An interruptible handle for external control of the ticker.</returns>
        public static IInterruptible Periodically(FirePeriodicallyTicker tickerDelegate, TimeSpan timeSpan) => new FirePeriodicallyContext(tickerDelegate, null, timeSpan);

        /// <summary>
        /// Runs the specified ticker delegate periodically.
        /// </summary>
        /// <param name="tickerDelegate">The ticker delegate.</param>
        /// <param name="initialDelay">The initial delay.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>An interruptible handle for external control of the ticker.</returns>
        public static IInterruptible Periodically(FirePeriodicallyTicker tickerDelegate, TimeSpan initialDelay, TimeSpan timeSpan) => new FirePeriodicallyContext(tickerDelegate, null, initialDelay, timeSpan);

        /// <summary>
        /// Runs the specified ticker delegate periodically.
        /// </summary>
        /// <param name="tickerDelegate">The ticker delegate.</param>
        /// <param name="payload">The payload.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>An interruptible handle for external control of the ticker.</returns>
        public static IInterruptible Periodically(FirePeriodicallyTicker tickerDelegate, object payload, int milliseconds) => new FirePeriodicallyContext(tickerDelegate, payload, milliseconds);

        /// <summary>
        /// Runs the specified ticker delegate periodically.
        /// </summary>
        /// <param name="tickerDelegate">The ticker delegate.</param>
        /// <param name="payload">The payload.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>An interruptible handle for external control of the ticker.</returns>
        public static IInterruptible Periodically(FirePeriodicallyTicker tickerDelegate, object payload, TimeSpan timeSpan) => new FirePeriodicallyContext(tickerDelegate, payload, timeSpan);

        /// <summary>
        /// Runs the specified ticker delegate periodically.
        /// </summary>
        /// <param name="tickerDelegate">The ticker delegate.</param>
        /// <param name="payload">The payload.</param>
        /// <param name="initialDelay">The initial delay.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>An interruptible handle for external control of the ticker.</returns>
        public static IInterruptible Periodically(FirePeriodicallyTicker tickerDelegate, object payload, TimeSpan initialDelay, TimeSpan timeSpan) => new FirePeriodicallyContext(tickerDelegate, payload, initialDelay, timeSpan);

        private static void StandardContinuation(
            Task task,
            object innerState)
        {
            Contract.RequiresNotNullPrivate(in task, nameof(task));
            var exception = task.Exception;
            Contract.RequiresNotNullPrivate(in exception, nameof(task));
            Contract.RequiresArgumentOfTypePrivate<Action<Exception>>(innerState, nameof(innerState));

            (innerState as Action<Exception>)?.Invoke(exception.GetBaseException());
        }
    }
}