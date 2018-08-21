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

            if (EnvironmentSettings.InvokeSynchronouslyOnCurrentThread)
            {
                action();
                return;
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
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable
                        SendOrPost,
#pragma warning restore HAA0603 // Delegate allocation from a method group
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is acceptable
                        (this, action));
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
                }
                else
                {
                    currentSynchronizationContext.Post(
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable
                        SendOrPost,
#pragma warning restore HAA0603 // Delegate allocation from a method group
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is acceptable
                        (this, action));
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
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

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void FireAndForget(Action action) => Fire.AndForget(action, this.InvokeExceptionOccurredOnSeparateThread);
#pragma warning restore HAA0603 // Delegate allocation from a method group

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            Interlocked.Exchange(ref this.synchronizationContext, null);

            base.DisposeGeneralContext();
        }

        /// <summary>
        /// Invokes the <see cref="ExceptionOccurredOnSeparateThread"/> event in a safe manner, while ignoring any processing exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected void InvokeExceptionOccurredOnSeparateThread(Exception ex) => this.ExceptionOccurredOnSeparateThread?.Invoke(this, new ExceptionOccurredEventArgs(ex));
    }
}