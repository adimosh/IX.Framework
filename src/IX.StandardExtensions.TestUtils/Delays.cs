// <copyright file="Delays.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    /// A static class with functions for introducing delays in execution.
    /// </summary>
    public static class Delays
    {
        /// <summary>
        /// Delays by ten milliseconds.
        /// </summary>
        public static void DelayByTenMilliseconds() => Thread.Sleep(10);

        /// <summary>
        /// Delays by ten milliseconds, asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task" /> which can be awaited.</returns>
        public static Task DelayByTenMillisecondsAsync(CancellationToken cancellationToken = default) => Task.Delay(10, cancellationToken);

        /// <summary>
        /// Delays by ten milliseconds.
        /// </summary>
        /// <typeparam name="T">The type of the state parameter.</typeparam>
        /// <param name="state">The state.</param>
        public static void DelayByTenMilliseconds<T>(T state) => Thread.Sleep(10);

        /// <summary>
        /// Delays by ten milliseconds, asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the state parameter.</typeparam>
        /// <param name="state">The state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task" /> which can be awaited.</returns>
        public static Task DelayByTenMillisecondsAsync<T>(T state, CancellationToken cancellationToken = default) => Task.Delay(10, cancellationToken);

        /// <summary>
        /// Delays by one hundred milliseconds.
        /// </summary>
        public static void DelayByOneHundredMilliseconds() => Thread.Sleep(100);

        /// <summary>
        /// Delays by one hundred milliseconds, asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task" /> which can be awaited.</returns>
        public static Task DelayByOneHundredMillisecondsAsync(CancellationToken cancellationToken = default) => Task.Delay(100, cancellationToken);

        /// <summary>
        /// Delays by one hundred milliseconds.
        /// </summary>
        /// <typeparam name="T">The type of the state parameter.</typeparam>
        /// <param name="state">The state.</param>
        public static void DelayByOneHundredMilliseconds<T>(T state) => Thread.Sleep(100);

        /// <summary>
        /// Delays by one hundred milliseconds, asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the state parameter.</typeparam>
        /// <param name="state">The state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task" /> which can be awaited.</returns>
        public static Task DelayByOneHundredMillisecondsAsync<T>(T state, CancellationToken cancellationToken = default) => Task.Delay(100, cancellationToken);

        /// <summary>
        /// Delays by one thousand milliseconds.
        /// </summary>
        public static void DelayByOneThousandMilliseconds() => Thread.Sleep(1000);

        /// <summary>
        /// Delays by one thousand milliseconds, asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task" /> which can be awaited.</returns>
        public static Task DelayByOneThousandMillisecondsAsync(CancellationToken cancellationToken = default) => Task.Delay(1000, cancellationToken);

        /// <summary>
        /// Delays by one thousand milliseconds.
        /// </summary>
        /// <typeparam name="T">The type of the state parameter.</typeparam>
        /// <param name="state">The state.</param>
        public static void DelayByOneThousandMilliseconds<T>(T state) => Thread.Sleep(1000);

        /// <summary>
        /// Delays by one thousand milliseconds, asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the state parameter.</typeparam>
        /// <param name="state">The state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task" /> which can be awaited.</returns>
        public static Task DelayByOneThousandMillisecondsAsync<T>(T state, CancellationToken cancellationToken = default) => Task.Delay(1000, cancellationToken);

        /// <summary>
        /// Delays by ten milliseconds.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to delay.</param>
        public static void DelayByAnAmount(int milliseconds) => Thread.Sleep(milliseconds);

        /// <summary>
        /// Delays by ten milliseconds, asynchronously.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to delay.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task" /> which can be awaited.</returns>
        public static Task DelayByAnAmountAsync(int milliseconds, CancellationToken cancellationToken = default) => Task.Delay(milliseconds, cancellationToken);
    }
}