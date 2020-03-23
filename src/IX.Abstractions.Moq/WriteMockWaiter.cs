// <copyright file="WriteMockWaiter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using JetBrains.Annotations;

namespace IX.Abstractions.Moq
{
    /// <summary>
    ///     A mock waiter class that allows one to read a self-closing stream data after the stream has closed.
    /// </summary>
    /// <remarks>
    ///     <para>Please do not attempt to use this class for high-capacity streams, as it is unsuitable for such a purpose.</para>
    /// </remarks>
    /// <seealso cref="T:System.IDisposable" />
    [PublicAPI]
    public class WriteMockWaiter : IDisposable
    {
        private byte[] data;
        private bool isAwaited;
        private bool isDisposed;
        private SaveWhenDisposingMemoryStream memoryStream;
        private ManualResetEvent mre = new ManualResetEvent(false);

        /// <summary>
        ///     Initializes a new instance of the <see cref="WriteMockWaiter" /> class.
        /// </summary>
        internal WriteMockWaiter()
        {
            this.mre.Reset();

            this.memoryStream = new SaveWhenDisposingMemoryStream(ActOnSavedData);

            void ActOnSavedData(byte[] savedData)
            {
                this.data = savedData;
                this.isAwaited = true;
                this.mre.Set();
            }
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="WriteMockWaiter" /> class.
        /// </summary>
        ~WriteMockWaiter()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(false);
        }

        /// <summary>
        ///     Gets the data.
        /// </summary>
        /// <value>
        ///     The data.
        /// </value>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     Occurs when the object has already been disposed and should no
        ///     longer be used.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     Occurs if anyone tries to load data before it has successfully
        ///     read from the stream.
        /// </exception>
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
        ///     Gets the memory stream.
        /// </summary>
        /// <value>
        ///     The memory stream.
        /// </value>
        public SaveWhenDisposingMemoryStream MemoryStream => this.memoryStream;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Waits for the write operation to finish.
        /// </summary>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     Occurs when the object has already been disposed and should no
        ///     longer be used.
        /// </exception>
        public void WaitForWriteFinished() => this.WaitForWriteFinished(Timeout.InfiniteTimeSpan);

        /// <summary>
        ///     Waits for the write operation to finish.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        /// <returns>
        ///     <see langword="true" /> if the wait was terminated successfully, <see langword="false" /> if there was a
        ///     timeout.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     Occurs when the object has already been disposed and should no
        ///     longer be used.
        /// </exception>
        public bool WaitForWriteFinished(TimeSpan timeToWait)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(WriteMockWaiter));
            }

            return this.mre.WaitOne(timeToWait);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <see langword="true" /> to release both managed and unmanaged resources;
        ///     <see langword="false" /> to release only unmanaged resources.
        /// </param>
        [SuppressMessage(
            "IDisposableAnalyzers.Correctness",
            "IDISP003:Dispose previous before re-assigning.",
            Justification = "We are doing exactly that, but the analyzer can't tell.")]
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                Interlocked.Exchange(
                        ref this.memoryStream,
                        null)
                    ?.Dispose();
                Interlocked.Exchange(
                        ref this.mre,
                        null)
                    ?.Dispose();
            }

            this.data = null;

            this.isDisposed = true;
        }
    }
}