// <copyright file="RetryContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IX.StandardExtensions;
using IX.StandardExtensions.ComponentModel;
using IX.StandardExtensions.Contracts;

namespace IX.Retry.Contexts
{
    internal abstract class RetryContext : DisposableBase
    {
        private RetryOptions options;

        internal RetryContext(RetryOptions options)
        {
            Contract.RequiresNotNullPrivate(
                in options,
                nameof(options));

            this.options = options;
        }

        /// <summary>
        ///     Begins the retry process asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task.</returns>
        internal async Task BeginRetryProcessAsync(CancellationToken cancellationToken)
        {
            this.RequiresNotDisposedPrivate();

            DateTime now = DateTime.UtcNow;
            var retries = 0;
            var exceptions = new List<Exception>();
            bool shouldRetry;

            do
            {
                shouldRetry = this.RunOnce(
                    exceptions,
                    now,
                    ref retries);

                cancellationToken.ThrowIfCancellationRequested();

                if (!shouldRetry || this.options.WaitBetweenRetriesType == WaitType.None)
                {
                    continue;
                }

                TimeSpan waitFor = this.GetRetryTimeSpan(
                    retries,
                    now);
                await Task.Delay(
                    (int)waitFor.TotalMilliseconds,
                    cancellationToken).ConfigureAwait(false);
            }
            while (shouldRetry);

            if (this.options.ThrowExceptionOnLastRetry)
            {
                throw new AggregateException(exceptions);
            }
        }

        /// <summary>
        ///     Begins the retry process.
        /// </summary>
        internal void BeginRetryProcess(CancellationToken cancellationToken)
        {
            this.RequiresNotDisposedPrivate();

            DateTime now = DateTime.UtcNow;
            var retries = 0;
            var exceptions = new List<Exception>();
            bool shouldRetry;

            do
            {
                shouldRetry = this.RunOnce(
                    exceptions,
                    now,
                    ref retries);

                cancellationToken.ThrowIfCancellationRequested();

                if (!shouldRetry || this.options.WaitBetweenRetriesType == WaitType.None)
                {
                    continue;
                }

                TimeSpan waitFor = this.GetRetryTimeSpan(
                    retries,
                    now);
#if NETSTANDARD1_2
                Task.Factory.StartNew(async state => await Task.Delay((int)state).ConfigureAwait(false), waitFor.TotalMilliseconds, cancellationToken, TaskCreationOptions.HideScheduler | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).ConfigureAwait(false);
#else
                Thread.Sleep((int)waitFor.TotalMilliseconds);
#endif
            }
            while (shouldRetry);

            if (this.options.ThrowExceptionOnLastRetry)
            {
                throw new AggregateException(exceptions);
            }
        }

        /// <summary>
        ///     Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            this.options = null;

            base.DisposeGeneralContext();
        }

        /// <summary>
        ///     Invokes the method that needs retrying.
        /// </summary>
        private protected abstract void Invoke();

        private bool RunOnce(
            ICollection<Exception> exceptions,
            DateTime now,
            ref int retries)
        {
            Contract.RequiresNotNullPrivate(
                in exceptions,
                nameof(exceptions));

            var shouldRetry = true;

            try
            {
                this.Invoke();

                shouldRetry = false;
            }
            catch (StopRetryingException)
            {
                shouldRetry = false;
            }
            catch (Exception ex)
            {
                List<Tuple<Type, Func<Exception, bool>>> retryOnExceptions = this.options.RetryOnExceptions;
                if (retryOnExceptions.Count > 0 && !this.options.RetryOnExceptions.Any(
                        (
                                p,
                                exL1) => p.Item1 == exL1.GetType() && (p.Item2 == null || p.Item2(exL1)), ex))
                {
                    throw;
                }

                exceptions.Add(ex);

                if ((this.options.Type & RetryType.Times) != 0 && retries >= this.options.RetryTimes - 1)
                {
                    shouldRetry = false;
                }

                if (shouldRetry && (this.options.Type & RetryType.For) != 0 &&
                    DateTime.UtcNow - now > this.options.RetryFor)
                {
                    shouldRetry = false;
                }

                if (shouldRetry && (this.options.Type & RetryType.Until) != 0 && this.options.RetryUntil(
                        retries,
                        now,
                        exceptions,
                        this.options))
                {
                    shouldRetry = false;
                }

                if (shouldRetry)
                {
                    retries++;
                }
            }

            return shouldRetry;
        }

        private TimeSpan GetRetryTimeSpan(
            int retries,
            DateTime now)
        {
            switch (this.options.WaitBetweenRetriesType)
            {
                case WaitType.For when this.options.WaitForDuration.HasValue:
                    return this.options.WaitForDuration.Value;

                case WaitType.Until:
                    return this.options.WaitUntilDelegate.Invoke(
                        retries,
                        now,
                        this.options);

                default:
                    return TimeSpan.Zero;
            }
        }
    }
}