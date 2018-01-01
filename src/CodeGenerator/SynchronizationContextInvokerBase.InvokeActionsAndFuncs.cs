// <copyright file="SynchronizationContextInvokerBase.InvokeActionsAndFuncs.cs" company="Adrian Mos">
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
        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        protected void Invoke<TParam1>(Action<TParam1> action, TParam1 param1)
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
                    action(param1);
                }
                else
                {
                    this.FireAndForget(action, param1);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1>, Tuple<TParam1>>(this, action, new Tuple<TParam1>(param1));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1>, Tuple<TParam1>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        protected void FireAndForget<TParam1>(Action<TParam1> action, TParam1 param1)
            => Fire.AndForget<TParam1>(action, param1, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        protected void Invoke<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
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
                    action(param1, param2);
                }
                else
                {
                    this.FireAndForget(action, param1, param2);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(this, action, new Tuple<TParam1, TParam2>(param1, param2));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1, TParam2> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1, unpackedParameters.Item2);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        protected void FireAndForget<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
            => Fire.AndForget<TParam1, TParam2>(action, param1, param2, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        protected void Invoke<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3)
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
                    action(param1, param2, param3);
                }
                else
                {
                    this.FireAndForget(action, param1, param2, param3);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(this, action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1, TParam2, TParam3> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        protected void FireAndForget<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3)
            => Fire.AndForget<TParam1, TParam2, TParam3>(action, param1, param2, param3, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        protected void Invoke<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
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
                    action(param1, param2, param3, param4);
                }
                else
                {
                    this.FireAndForget(action, param1, param2, param3, param4);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(this, action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        protected void FireAndForget<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
            => Fire.AndForget<TParam1, TParam2, TParam3, TParam4>(action, param1, param2, param3, param4, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        protected void Invoke<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
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
                    action(param1, param2, param3, param4, param5);
                }
                else
                {
                    this.FireAndForget(action, param1, param2, param3, param4, param5);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(this, action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        protected void FireAndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
            => Fire.AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(action, param1, param2, param3, param4, param5, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        protected void Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
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
                    action(param1, param2, param3, param4, param5, param6);
                }
                else
                {
                    this.FireAndForget(action, param1, param2, param3, param4, param5, param6);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(this, action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        protected void FireAndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
            => Fire.AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(action, param1, param2, param3, param4, param5, param6, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        protected void Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
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
                    action(param1, param2, param3, param4, param5, param6, param7);
                }
                else
                {
                    this.FireAndForget(action, param1, param2, param3, param4, param5, param6, param7);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(this, action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        protected void FireAndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
            => Fire.AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(action, param1, param2, param3, param4, param5, param6, param7, this.InvokeExceptionOccurredOnSeparateThread);

        /// <summary>
        /// Invokes the specified action using the synchronization context asynchronously, or as fire-and-forget if there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        protected void Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
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
                    action(param1, param2, param3, param4, param5, param6, param7, param8);
                }
                else
                {
                    this.FireAndForget(action, param1, param2, param3, param4, param5, param6, param7, param8);
                }
            }
            else
            {
                var state = new Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(this, action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8));

                if (EnvironmentSettings.InvokeSynchronously)
                {
                    currentSynchronizationContext.Send(
                        SendOrPost,
                        state);
                }
                else
                {
                    currentSynchronizationContext.Post(
                        SendOrPost,
                        state);
                }

                void SendOrPost(object innerState)
                {
                    var unpackedState = (Tuple<SynchronizationContextInvokerBase, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)innerState;
                    SynchronizationContextInvokerBase invoker = unpackedState.Item1;
                    Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = unpackedState.Item3;

                    try
                    {
                        unpackedState.Item2(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);
                    }
                    catch (Exception ex)
                    {
                        unpackedState.Item1.InvokeExceptionOccurredOnSeparateThread(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action and forgets about it, allowing it to run uninterrupted in the background.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        protected void FireAndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
            => Fire.AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(action, param1, param2, param3, param4, param5, param6, param7, param8, this.InvokeExceptionOccurredOnSeparateThread);
    }
}