// <copyright file="RetryContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Retry
{
    internal class RetryContext : IDisposable
    {
        private bool disposedValue;

        private RetryOptions options;
        private Action actionToRetry;

        public RetryContext(RetryOptions options, Action actionToRetry)
        {
            this.options = options;
            this.actionToRetry = actionToRetry;
        }

        ~RetryContext()
        {
            this.Dispose(false);
        }

        public async Task BeginRetryProcessAsync(CancellationToken cancellationToken = default)
        {
            this.CheckDisposed();

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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginRetryProcess()
        {
            this.CheckDisposed();

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

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                this.options = null;
                this.actionToRetry = null;

                this.disposedValue = true;
            }
        }

        private bool RunOnce(IList<Exception> exceptions, DateTime now, ref int retries)
        {
            var shouldRetry = true;

            try
            {
                this.actionToRetry();
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

        private void CheckDisposed()
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(typeof(RetryContext).FullName);
            }
        }
    }
}