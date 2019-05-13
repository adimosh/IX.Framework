// <copyright file="SynchronizationContextInvokerBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    ///     An abstract base class for a synchronization context invoker.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    [PublicAPI]
    public abstract partial class SynchronizationContextInvokerBase : DisposableBase, INotifyThreadException
    {
        private SynchronizationContext synchronizationContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SynchronizationContextInvokerBase" /> class.
        /// </summary>
        protected SynchronizationContextInvokerBase()
            : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SynchronizationContextInvokerBase" /> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected SynchronizationContextInvokerBase([CanBeNull] SynchronizationContext synchronizationContext)
        {
            this.synchronizationContext = synchronizationContext;
        }

        /// <summary>
        ///     Triggered when an exception has occurred on a different thread.
        /// </summary>
        public event EventHandler<ExceptionOccurredEventArgs> ExceptionOccurredOnSeparateThread;

        /// <summary>
        ///     Gets the synchronization context used by this object, if any.
        /// </summary>
        /// <value>The synchronization context.</value>
        public SynchronizationContext SynchronizationContext => this.synchronizationContext;

        /// <summary>
        ///     Invokes the specified action using the synchronization context asynchronously, or synchronously on this thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void Invoke(Action action) => this.Invoke(
            p => ((Action)p)(),
            (object)action);

        /// <summary>
        ///     Invokes the specified action using the synchronization context asynchronously, or synchronously on this thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="state">The state object to pass on to the action.</param>
        protected void Invoke(
            Action<object> action,
            object state)
        {
            this.ThrowIfCurrentObjectDisposed();

            Contract.RequiresNotNull(
                action,
                nameof(action));

            SynchronizationContext currentSynchronizationContext =
                this.synchronizationContext ?? EnvironmentSettings.GetUsableSynchronizationContext();

            if (currentSynchronizationContext == null)
            {
                if (EnvironmentSettings.InvokeAsynchronously)
                {
                    Fire.AndForget(
                        action,
                        state);
                }
                else
                {
                    action(state);
                }
            }
            else
            {
                var outerState = new Tuple<SynchronizationContextInvokerBase, Action<object>, object>(
                    this,
                    action,
                    state);
                if (EnvironmentSettings.InvokeAsynchronously)
                {
                    currentSynchronizationContext.Post(
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable
                        SendOrPost,
#pragma warning restore HAA0603 // Delegate allocation from a method group
                        outerState);
                }
                else
                {
                    currentSynchronizationContext.Send(
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable
                        SendOrPost,
#pragma warning restore HAA0603 // Delegate allocation from a method group
                        outerState);
                }

                void SendOrPost(object innerState)
                {
                    var arguments = (Tuple<SynchronizationContextInvokerBase, Action<object>, object>)innerState;

                    try
                    {
                        arguments.Item2(arguments.Item3);
                    }
                    catch (Exception ex)
                    {
                        arguments.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        ///     Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            Interlocked.Exchange(
                ref this.synchronizationContext,
                null);

            base.DisposeGeneralContext();
        }

        /// <summary>
        ///     Invokes the <see cref="ExceptionOccurredOnSeparateThread" /> event in a safe manner, while ignoring any processing
        ///     exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected void InvokeExceptionOccurredOnSeparateThread(Exception ex) =>
            this.ExceptionOccurredOnSeparateThread?.Invoke(
                this,
                new ExceptionOccurredEventArgs(ex));
    }
}