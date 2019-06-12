// <copyright file="RetryOptionsExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.Retry
{
    /// <summary>
    ///     Extension methods for the options for the retrying process data object.
    /// </summary>
    [PublicAPI]
    public static class RetryOptionsExtensions
    {
        /// <summary>
        ///     Retry for a number of times.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="times">The number of times to retry. Has to be greater than 0.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions Times(
            [CanBeNull] this RetryOptions options,
            int times)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresPositive(
                in times,
                nameof(times));

            options.Type |= RetryType.Times;
            options.RetryTimes = times;

            return options;
        }

        /// <summary>
        ///     Retry once.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions Once([CanBeNull] this RetryOptions options)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            options.Type |= RetryType.Times;
            options.RetryTimes = 1;

            return options;
        }

        /// <summary>
        ///     Retry twice.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions Twice([CanBeNull] this RetryOptions options)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            options.Type |= RetryType.Times;
            options.RetryTimes = 2;

            return options;
        }

        /// <summary>
        ///     Retry three times.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions ThreeTimes([CanBeNull] this RetryOptions options)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            options.Type |= RetryType.Times;
            options.RetryTimes = 3;

            return options;
        }

        /// <summary>
        ///     Retry five times.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions FiveTimes([CanBeNull] this RetryOptions options)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            options.Type |= RetryType.Times;
            options.RetryTimes = 5;

            return options;
        }

        /// <summary>
        ///     Retries for a specific time span.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="timeSpan">How long to retry, as a time span.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions For(
            [CanBeNull] this RetryOptions options,
            TimeSpan timeSpan)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresPositive(
                in timeSpan,
                nameof(timeSpan));

            options.Type |= RetryType.For;
            options.RetryFor = timeSpan;

            return options;
        }

        /// <summary>
        ///     Retries for a specific number of milliseconds.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="milliseconds">How long to retry, in milliseconds.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions For(
            [CanBeNull] this RetryOptions options,
            int milliseconds)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresPositive(
                in milliseconds,
                nameof(milliseconds));

            options.Type |= RetryType.For;
            options.RetryFor = TimeSpan.FromMilliseconds(milliseconds);

            return options;
        }

        /// <summary>
        ///     Retries until a specific condition is reached.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="conditionMethod">The condition method to evaluate.</param>
        /// <returns>The configured retry options.</returns>
        /// <remarks>
        ///     <para>
        ///         Retrying happens while the <paramref name="conditionMethod" /> method, when executed, returns
        ///         <see langword="true" />.
        ///     </para>
        ///     <para>On first instance that the method return is <see langword="false" />, retrying stops.</para>
        /// </remarks>
        [NotNull]
        public static RetryOptions Until(
            [CanBeNull] this RetryOptions options,
            RetryConditionDelegate conditionMethod)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresNotNull(
                in conditionMethod,
                nameof(conditionMethod));

            options.Type |= RetryType.Until;
            options.RetryUntil = conditionMethod;

            return options;
        }

        /// <summary>
        ///     Configures an exception that, when thrown by the code being retried, prompts a retry.
        /// </summary>
        /// <typeparam name="T">The exception type to configure.</typeparam>
        /// <param name="options">Retry options to configure.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions OnException<T>([CanBeNull] this RetryOptions options)
            where T : Exception
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));

#pragma warning disable HAA0603 // Delegate allocation from a method group - Unavoidable
            options.RetryOnExceptions.RemoveAll(RemovePredicate<T>);
            options.RetryOnExceptions.Add(
                new Tuple<Type, Func<Exception, bool>>(
                    typeof(T),
                    null));
#pragma warning restore HAA0603 // Delegate allocation from a method group

            return options;
        }

        /// <summary>
        ///     Configures an exception that, when thrown by the code being retried, prompts a retry, if the method to test for it
        ///     allows it.
        /// </summary>
        /// <typeparam name="T">The exception type to configure.</typeparam>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="testExceptionFunc">The method to test the exceptions with.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions OnException<T>(
            [CanBeNull] this RetryOptions options,
            Func<Exception, bool> testExceptionFunc)
            where T : Exception
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresNotNull(
                in testExceptionFunc,
                nameof(testExceptionFunc));

#pragma warning disable HAA0603 // Delegate allocation from a method group - Unavoidable
            options.RetryOnExceptions.RemoveAll(RemovePredicate<T>);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            options.RetryOnExceptions.Add(
                new Tuple<Type, Func<Exception, bool>>(
                    typeof(T),
                    testExceptionFunc));

            return options;
        }

        /// <summary>
        ///     Configures retry options to throw an exception at the end of the retrying process, if it was unsuccessful.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions ThrowException([CanBeNull] this RetryOptions options)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            options.ThrowExceptionOnLastRetry = true;

            return options;
        }

        /// <summary>
        ///     Waiting time between retries.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="milliseconds">The number of milliseconds to wait for.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions WaitFor(
            [CanBeNull] this RetryOptions options,
            int milliseconds)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresPositive(
                in milliseconds,
                nameof(milliseconds));

            options.WaitBetweenRetriesType = WaitType.For;
            options.WaitForDuration = TimeSpan.FromMilliseconds(milliseconds);

            return options;
        }

        /// <summary>
        ///     Waiting time between retries.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="timeSpan">The time span to wait between retries.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions WaitFor(
            [CanBeNull] this RetryOptions options,
            TimeSpan timeSpan)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresPositive(
                in timeSpan,
                nameof(timeSpan));

            options.WaitBetweenRetriesType = WaitType.For;
            options.WaitForDuration = timeSpan;

            return options;
        }

        /// <summary>
        ///     Waits a time span that is configured by a given delegate.
        /// </summary>
        /// <param name="options">Retry options to configure.</param>
        /// <param name="waitMethod">The waiting delegate to give the time span.</param>
        /// <returns>The configured retry options.</returns>
        [NotNull]
        public static RetryOptions WaitUntil(
            [CanBeNull] this RetryOptions options,
            RetryWaitDelegate waitMethod)
        {
            Contract.RequiresNotNull(
                in options,
                nameof(options));
            Contract.RequiresNotNull(
                in waitMethod,
                nameof(waitMethod));

            options.WaitBetweenRetriesType = WaitType.Until;
            options.WaitUntilDelegate = waitMethod;

            return options;
        }

        private static bool RemovePredicate<T>(Tuple<Type, Func<Exception, bool>> p) => p.Item1 == typeof(T);
    }
}