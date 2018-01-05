// <copyright file="SynchronizationContextInvokerBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.StandardExtensions.Threading;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// An abstract base class for a synchronization context invoker.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    public abstract partial class SynchronizationContextInvokerBase : DisposableBase, INotifyThreadException
    {
        private SynchronizationContext synchronizationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationContextInvokerBase"/> class.
        /// </summary>
        protected SynchronizationContextInvokerBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationContextInvokerBase"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected SynchronizationContextInvokerBase(SynchronizationContext synchronizationContext)
        {
            this.synchronizationContext = synchronizationContext;
        }

        /// <summary>
        /// Triggered when an exception has occurred on a different thread.
        /// </summary>
        public event EventHandler<ExceptionOccurredEventArgs> ExceptionOccurredOnSeparateThread;

        /// <summary>
        /// Gets the synchronization context used by this object, if any.
        /// </summary>
        /// <value>The synchronization context.</value>
        public SynchronizationContext SynchronizationContext => this.synchronizationContext;

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or synchronously on this thread if there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void Invoke(Action action)
        {
            this.ThrowIfCurrentObjectDisposed();

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            SynchronizationContext currentSynchronizationContext = this.synchronizationContext ?? EnvironmentSettings.GetUsableSynchronizationContext();

            if (currentSynchronizationContext == null)
            {
                if (EnvironmentSettings.InvokeSynchronously)
                {
                    action();
                }
                else
                {
                    this.FireAndForget(action);
                }
            }
            else
            {
                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        (this, action));
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        (this, action));
                }

                void SendOrPost(object innerState)
                {
                    var arguments = ((SynchronizationContextInvokerBase, Action))innerState;

                    try
                    {
                        arguments.Item2();
                    }
                    catch (Exception ex)
                    {
                        arguments.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void FireAndForget(Action action) => Fire.AndForget(action, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            this.synchronizationContext = null;

            base.DisposeGeneralContext();
        }

        /// <summary>
        /// Invokes the <see cref="ExceptionOccurredOnSeparateThread"/> event in a safe manner, while ignoring any processing exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected void InvokeExceptionOccurredOnSeparateThread(Exception ex) => this.ExceptionOccurredOnSeparateThread?.Invoke(this, new ExceptionOccurredEventArgs(ex));
    }
}