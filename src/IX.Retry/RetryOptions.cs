// <copyright file="RetryOptions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.Retry
{
    /// <summary>
    /// Options for the retrying process.
    /// </summary>
    public class RetryOptions
    {
        /// <summary>
        /// Gets the type of the retrying operation.
        /// </summary>
        /// <returns>What type of retry porocedure to use.</returns>
        public RetryType Type { get; internal set; }

        /// <summary>
        /// Gets how many times to retry.
        /// </summary>
        /// <returns>The number of times to attempt retrying.</returns>
        public int RetryTimes { get; internal set; }

        /// <summary>
        /// Gets for how long to retry.
        /// </summary>
        /// <returns>The maxiumum time span to retry.</returns>
        public TimeSpan RetryFor { get; internal set; }

        /// <summary>
        /// Gets the condition method to use when evaluating whether to retry or not.
        /// </summary>
        /// <returns>A delegate that can be invoked.</returns>
        public RetryConditionDelegate RetryUntil { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether whether or not to throw an exception at the end of the retrying process, if it is unsuccessful.
        /// </summary>
        /// <returns><c>true</c> if an exception should be thrown, <c>false</c> otherwise.</returns>
        public bool ThrowExceptionOnLastRetry { get; internal set; }

        /// <summary>
        /// Gets a list of exceptions and optional conditional methods that will be evaluated when deciding whether or not to retry.
        /// </summary>
        /// <returns>A list of exception/delegate tuples.</returns>
        public List<Tuple<Type, Func<Exception, bool>>> RetryOnExceptions { get; } = new List<Tuple<Type, Func<Exception, bool>>>();

        /// <summary>
        /// Gets the type of waiting to do between retries. By default, no waiting between retries is performed.
        /// </summary>
        /// <returns>The type of waiting.</returns>
        public WaitType WaitBetweenRetriesType { get; internal set; }

        /// <summary>
        /// Gets the duration of time to use as a waiting duration between retries, if enabled.
        /// </summary>
        /// <returns>The waiting duration.</returns>
        public TimeSpan? WaitForDuration { get; internal set; }

        /// <summary>
        /// Gets or sets a delegate to define for how long do we need to wait between retries.
        /// </summary>
        /// <returns>A time span to wait for.</returns>
        public RetryWaitDelegate WaitUntilDelegate { get; set; }
    }
}