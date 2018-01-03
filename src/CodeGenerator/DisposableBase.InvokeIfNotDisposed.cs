// <copyright file="DisposableBase.InvokeIfNotDisposed.cs" company="Adrian Mos">
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
        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1>(Action<TParam1> action, TParam1 param1)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TReturn>(Func<TParam1, TReturn> action, TParam1 param1)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> action, TParam1 param1, TParam2 param2)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1, param2);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5, param6);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5, param6);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5, param6, param7);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5, param6, param7);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected void InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            this.ThrowIfCurrentObjectDisposed();

            (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
        }

        /// <summary>
        /// Invokes an action if the current instance is not disposed.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TReturn InvokeIfNotDisposed<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (action ?? throw new ArgumentNullException()).Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
        }
    }
}