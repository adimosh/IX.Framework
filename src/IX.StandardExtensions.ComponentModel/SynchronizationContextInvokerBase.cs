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
    /// <seealso cref="DisposableBase" />
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
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void Invoke(Action action) => this.Invoke(
            p => ((Action)p)(),
            (object)action);

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable
        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="state">The state object to pass on to the action.</param>
        protected void Invoke(
            [NotNull] Action<object> action,
            object state)
        {
            this.ThrowIfCurrentObjectDisposed();

            Contract.RequiresNotNull(
                in action,
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
                var outerState = new Tuple<Action<object>, object>(
                    action,
                    state);
                if (EnvironmentSettings.InvokeAsynchronously)
                {
                    currentSynchronizationContext.Post(
                        this.SendOrPost,
                        outerState);
                }
                else
                {
                    currentSynchronizationContext.Send(
                        this.SendOrPost,
                        outerState);
                }
            }
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void InvokePost(Action action) => this.InvokePost(
            p => ((Action)p)(),
            (object)action);

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable
        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="state">The state object to pass on to the action.</param>
        protected void InvokePost(
            [NotNull] Action<object> action,
            object state)
        {
            this.ThrowIfCurrentObjectDisposed();

            Contract.RequiresNotNull(
                in action,
                nameof(action));

            SynchronizationContext currentSynchronizationContext =
                this.synchronizationContext ?? EnvironmentSettings.GetUsableSynchronizationContext();

            if (currentSynchronizationContext == null)
            {
                Fire.AndForget(
                    action,
                    state);
            }
            else
            {
                var outerState = new Tuple<Action<object>, object>(
                    action,
                    state);

                currentSynchronizationContext.Post(
                    this.SendOrPost,
                    outerState);
            }
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        protected void InvokeSend(Action action) => this.InvokeSend(
            p => ((Action)p)(),
            (object)action);

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable
        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="state">The state object to pass on to the action.</param>
        protected void InvokeSend(
            [NotNull] Action<object> action,
            object state)
        {
            this.ThrowIfCurrentObjectDisposed();

            Contract.RequiresNotNull(
                in action,
                nameof(action));

            SynchronizationContext currentSynchronizationContext =
                this.synchronizationContext ?? EnvironmentSettings.GetUsableSynchronizationContext();

            if (currentSynchronizationContext == null)
            {
                action(state);
            }
            else
            {
                var outerState = new Tuple<Action<object>, object>(
                    action,
                    state);

                currentSynchronizationContext.Send(
                        this.SendOrPost,
                        outerState);
            }
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group

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

        private void SendOrPost(object innerState)
        {
            (Action<object> actionL1, object stateL1) = (Tuple<Action<object>, object>)innerState;

            try
            {
                actionL1(stateL1);
            }
            catch (Exception ex)
            {
                this.InvokeExceptionOccurredOnSeparateThread(ex);
            }
        }
    }
}