// <copyright file="SynchronizationContextInvokerBase.InvokeActionsAndFuncs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.ComponentModel
{
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - nope, it is generic too
#pragma warning disable SA1116 // Split parameters should start on line after declaration - we really don't care about this in auto-generated code
    /// <summary>
    /// An abstract base class for a synchronization context invoker.
    /// </summary>
    public abstract partial class SynchronizationContextInvokerBase
    {
        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        protected void Invoke<TParam1>(Action<TParam1> action, TParam1 param1)
        {
            var outerState = new Tuple<Action<TParam1>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1>, Tuple<TParam1>>)state;
                Tuple<TParam1> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        protected void InvokePost<TParam1>(Action<TParam1> action, TParam1 param1)
        {
            var outerState = new Tuple<Action<TParam1>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1>, Tuple<TParam1>>)state;
                Tuple<TParam1> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        protected void InvokeSend<TParam1>(Action<TParam1> action, TParam1 param1)
        {
            var outerState = new Tuple<Action<TParam1>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1>, Tuple<TParam1>>)state;
                Tuple<TParam1> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        protected void Invoke<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
        {
            var outerState = new Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)state;
                Tuple<TParam1, TParam2> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        protected void InvokePost<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
        {
            var outerState = new Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)state;
                Tuple<TParam1, TParam2> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        protected void InvokeSend<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
        {
            var outerState = new Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)state;
                Tuple<TParam1, TParam2> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
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
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)state;
                Tuple<TParam1, TParam2, TParam3> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        protected void InvokePost<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)state;
                Tuple<TParam1, TParam2, TParam3> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        protected void InvokeSend<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)state;
                Tuple<TParam1, TParam2, TParam3> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
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
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
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
        protected void InvokePost<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
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
        protected void InvokeSend<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
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
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
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
        protected void InvokePost<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
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
        protected void InvokeSend<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
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
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
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
        protected void InvokePost<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
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
        protected void InvokeSend<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
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
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
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
        protected void InvokePost<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
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
        protected void InvokeSend<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action using the synchronization context, or on either this thread or a separate thread if
        ///     there is no synchronization context available.
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
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8));

            this.Invoke(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action by posting on the synchronization context, or on a separate thread if
        ///     there is no synchronization context available.
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
        protected void InvokePost<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8));

            this.InvokePost(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);
            }, (object)outerState);
        }

        /// <summary>
        ///     Invokes the specified action synchronously using the synchronization context, or on this thread if
        ///     there is no synchronization context available.
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
        protected void InvokeSend<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            var outerState = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8));

            this.InvokeSend(state =>
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)state;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = unpackedState.Item2;
                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);
            }, (object)outerState);
        }
    }
#pragma warning restore SA1116 // Split parameters should start on line after declaration
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
}