// <copyright file="DisposableBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.Serialization;
using System.Threading;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// An abstract base class for correctly implementing the disposable pattern.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    [DataContract]
    public abstract partial class DisposableBase : IDisposable
    {
        private volatile int disposeSignaled;

        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableBase"/> class.
        /// </summary>
        ~DisposableBase()
        {
            this.Dispose(false);
        }

        internal bool Disposed { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Interlocked.Exchange(ref this.disposeSignaled, 1) != 0)
            {
                return;
            }

            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Throws if the current object is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the current object is disposed, this exception will be thrown.</exception>
        protected void ThrowIfCurrentObjectDisposed()
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke();
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>The object returned by the action.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TReturn>(Func<TReturn> action)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke();
        }

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected virtual void DisposeManagedContext()
        {
        }

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected virtual void DisposeGeneralContext()
        {
        }

        private void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    this.DisposeManagedContext();
                }

                this.DisposeGeneralContext();

                this.Disposed = true;
            }
        }
    }
}