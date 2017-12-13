// <copyright file="RetryContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IX.StandardExtensions.ComponentModel;

namespace IX.Retry
{
    internal class RetryContext : DisposableBase, IDisposable
    {
        private RetryOptions options;
        private Action actionToRetry;

        public RetryContext(RetryOptions options, Action actionToRetry)
        {
            this.options = options;
            this.actionToRetry = actionToRetry;
        }

        /// <summary>
        /// Begins the retry process asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public async Task BeginRetryProcessAsync(CancellationToken cancellationToken = default)
        {
            this.ThrowIfCurrentObjectDisposed();

            DateTime now = DateTime.UtcNow;
            var retries = 0;
            var exceptions = new List<Exception>();
            var shouldRetry = true;

            do
            {
                shouldRetry = this.RunOnce(exceptions, now, ref retries);

                if (shouldRetry && this.options.WaitBetweenRetriesType != WaitType.None)
                {
                    TimeSpan waitFor = this.GetRetryTimeSpan(retries, now);
                    await Task.Delay((int)waitFor.TotalMilliseconds, cancellationToken);
                }
            }
            while (shouldRetry);

            this.ThrowExceptions(shouldRetry, exceptions);
        }

        /// <summary>
        /// Begins the retry process.
        /// </summary>
        public void BeginRetryProcess()
        {
            this.ThrowIfCurrentObjectDisposed();

            DateTime now = DateTime.UtcNow;
            var retries = 0;
            var exceptions = new List<Exception>();
            var shouldRetry = true;

            do
            {
                shouldRetry = this.RunOnce(exceptions, now, ref retries);

                if (shouldRetry && this.options.WaitBetweenRetriesType != WaitType.None)
                {
                    TimeSpan waitFor = this.GetRetryTimeSpan(retries, now);
                    Task.Delay((int)waitFor.TotalMilliseconds).Wait();
                }
            }
            while (shouldRetry);

            this.ThrowExceptions(shouldRetry, exceptions);
        }

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            this.options = null;
            this.actionToRetry = null;

            base.DisposeGeneralContext();
        }

        private bool RunOnce(IList<Exception> exceptions, DateTime now, ref int retries)
        {
            var shouldRetry = true;

            try
            {
                this.actionToRetry();

                shouldRetry = false;
            }
            catch (Exception ex)
            {
                if (this.options.RetryOnExceptions.Count > 0 &&
                    !this.options.RetryOnExceptions.Any(p => p.Item1 == ex.GetType() && p.Item2(ex)))
                {
                    throw;
                }

                exceptions.Add(ex);

                if (this.options.Type.HasFlag(RetryType.Times) && retries >= this.options.RetryTimes - 1)
                {
                    shouldRetry = false;
                }

                if (shouldRetry && this.options.Type.HasFlag(RetryType.For) && (DateTime.UtcNow - now) > this.options.RetryFor)
                {
                    shouldRetry = false;
                }

                if (shouldRetry && this.options.Type.HasFlag(RetryType.Until) && this.options.RetryUntil(retries, now, exceptions, this.options))
                {
                    shouldRetry = false;
                }

                retries++;
            }

            return shouldRetry;
        }

        private TimeSpan GetRetryTimeSpan(int retries, DateTime now)
        {
            TimeSpan waitFor;
            if (this.options.WaitBetweenRetriesType == WaitType.For && this.options.WaitForDuration.HasValue)
            {
                waitFor = this.options.WaitForDuration.Value;
            }
            else if (this.options.WaitBetweenRetriesType == WaitType.Until)
            {
                waitFor = this.options.WaitUntilDelegate.Invoke(retries, now, this.options);
            }
            else
            {
                waitFor = TimeSpan.Zero;
            }

            return waitFor;
        }

        private void ThrowExceptions(bool shouldRetry, IEnumerable<Exception> exceptions)
        {
            if (!shouldRetry && this.options.ThrowExceptionOnLastRetry)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}