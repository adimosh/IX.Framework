// <copyright file="WriteMockWaiter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;

namespace IX.Abstractions.Moq
{
    /// <summary>
    /// A mock waiter class that allows one to read a self-closing stream data after the stream has closed.
    /// </summary>
    /// <remarks>
    /// <para>Please do not attempt to use this class for high-capacity streams, as it is unsuitable for such a purpose.</para>
    /// </remarks>
    /// <seealso cref="T:System.IDisposable" />
    public class WriteMockWaiter : IDisposable
    {
        private ManualResetEvent mre = new ManualResetEvent(false);
        private bool isDisposed;
        private bool isAwaited;

        private byte[] data;

        internal WriteMockWaiter()
        {
            this.mre.Reset();

            this.MemoryStream = new SaveWhenDisposingMemoryStream((savedData) =>
            {
                this.data = savedData;
                this.isAwaited = true;
                this.mre.Set();
            });
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="WriteMockWaiter"/> class.
        /// </summary>
        ~WriteMockWaiter()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        /// <exception cref="T:System.ObjectDisposedException">Occurs when the object has already been disposed and should no longer be used.</exception>
        /// <exception cref="T:System.InvalidOperationException">Occurs if anyone tries to load data before it has successfully read from the stream.</exception>
        public byte[] Data
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(WriteMockWaiter));
                }

                if (!this.isAwaited)
                {
                    throw new InvalidOperationException();
                }

                return this.data;
            }
        }

        /// <summary>
        /// Gets the memory stream.
        /// </summary>
        /// <value>
        /// The memory stream.
        /// </value>
        public SaveWhenDisposingMemoryStream MemoryStream { get; private set; }

        /// <summary>
        /// Waits for the write operation to finish.
        /// </summary>
        /// <exception cref="T:System.ObjectDisposedException">Occurs when the object has already been disposed and should no longer be used.</exception>
        public void WaitForWriteFinished()
        {
            this.WaitForWriteFinished(Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// Waits for the write operation to finish.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        /// <returns><c>true</c> if the wait was terminated successfully, <c>false</c> if there was a timeout.</returns>
        /// <exception cref="T:System.ObjectDisposedException">Occurs when the object has already been disposed and should no longer be used.</exception>
        public bool WaitForWriteFinished(TimeSpan timeToWait)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(WriteMockWaiter));
            }

            return this.mre.WaitOne(timeToWait);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.MemoryStream.Dispose();
                    this.mre.Dispose();
                }

                this.MemoryStream = null;
                this.mre = null;
                this.data = null;

                this.isDisposed = true;
            }
        }
    }
}