// <copyright file="DisposableBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// An abstract base class for correctly implementing the disposable pattern.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract partial class DisposableBase : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableBase"/> class.
        /// </summary>
        ~DisposableBase()
        {
            this.Dispose(false);
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
        /// Throws if the current object is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the current object is disposed, this exception will be thrown.</exception>
        protected void ThrowIfCurrentObjectDisposed()
        {
            if (this.disposedValue)
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
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.DisposeManagedContext();
                }

                this.DisposeGeneralContext();

                this.disposedValue = true;
            }
        }
    }
}