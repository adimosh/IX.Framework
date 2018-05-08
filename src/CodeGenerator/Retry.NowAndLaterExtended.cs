// <copyright file="Retry.NowAndLaterExtended.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using IX.Retry.Contexts;

namespace IX.Retry
{
    /// <summary>
    /// A class for containing retry operations.
    /// </summary>
    public static partial class Retry
    {
        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        public static void Now<TParam1>(Action<TParam1> action, TParam1 param1) => Run(action, param1, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1>(Action<TParam1> action, TParam1 param1, CancellationToken cancellationToken = default) => RunAsync(action, param1, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1>(Action<TParam1> action, TParam1 param1, RetryOptions options) => Run(action, param1, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1>(Action<TParam1> action, TParam1 param1, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1>(Action<TParam1> action, TParam1 param1, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1>(Action<TParam1> action, TParam1 param1, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1}(Action{TParam1}, TParam1)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1>(Action<TParam1> action, TParam1 param1) => () => Run(action, param1, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1}(Action{TParam1}, TParam1, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1>(Action<TParam1> action, TParam1 param1, RetryOptions options) => () => Run(action, param1, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1}(Action{TParam1}, TParam1, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1>(Action<TParam1> action, TParam1 param1, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1) => Run(func, param1, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, CancellationToken cancellationToken = default) => RunAsync(func, param1, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, RetryOptions options) => Run(func, param1, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TReturn}(Func{TParam1, TReturn}, TParam1)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1) => () => Run(func, param1, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TReturn}(Func{TParam1, TReturn}, TParam1, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, RetryOptions options) => () => Run(func, param1, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TReturn}(Func{TParam1, TReturn}, TParam1, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        public static void Now<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2) => Run(action, param1, param2, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, RetryOptions options) => Run(action, param1, param2, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, param2, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, param2, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2}(Action{TParam1, TParam2}, TParam1, TParam2)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2) => () => Run(action, param1, param2, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2}(Action{TParam1, TParam2}, TParam1, TParam2, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, RetryOptions options) => () => Run(action, param1, param2, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2}(Action{TParam1, TParam2}, TParam1, TParam2, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, param2, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2) => Run(func, param1, param2, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, RetryOptions options) => Run(func, param1, param2, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, param2, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, param2, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TReturn}(Func{TParam1, TParam2, TReturn}, TParam1, TParam2)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2) => () => Run(func, param1, param2, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TReturn}(Func{TParam1, TParam2, TReturn}, TParam1, TParam2, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, RetryOptions options) => () => Run(func, param1, param2, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TReturn}(Func{TParam1, TParam2, TReturn}, TParam1, TParam2, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, param2, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        public static void Now<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3) => Run(action, param1, param2, param3, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options) => Run(action, param1, param2, param3, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, param2, param3, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, param2, param3, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3}(Action{TParam1, TParam2, TParam3}, TParam1, TParam2, TParam3)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3) => () => Run(action, param1, param2, param3, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3}(Action{TParam1, TParam2, TParam3}, TParam1, TParam2, TParam3, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options) => () => Run(action, param1, param2, param3, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3}(Action{TParam1, TParam2, TParam3}, TParam1, TParam2, TParam3, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, param2, param3, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3) => Run(func, param1, param2, param3, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options) => Run(func, param1, param2, param3, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, param2, param3, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, param2, param3, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TReturn}(Func{TParam1, TParam2, TParam3, TReturn}, TParam1, TParam2, TParam3)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3) => () => Run(func, param1, param2, param3, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TReturn}(Func{TParam1, TParam2, TParam3, TReturn}, TParam1, TParam2, TParam3, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options) => () => Run(func, param1, param2, param3, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TReturn}(Func{TParam1, TParam2, TParam3, TReturn}, TParam1, TParam2, TParam3, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, param2, param3, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) => Run(action, param1, param2, param3, param4, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options) => Run(action, param1, param2, param3, param4, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, param2, param3, param4, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, param2, param3, param4, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4}(Action{TParam1, TParam2, TParam3, TParam4}, TParam1, TParam2, TParam3, TParam4)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) => () => Run(action, param1, param2, param3, param4, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4}(Action{TParam1, TParam2, TParam3, TParam4}, TParam1, TParam2, TParam3, TParam4, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options) => () => Run(action, param1, param2, param3, param4, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4}(Action{TParam1, TParam2, TParam3, TParam4}, TParam1, TParam2, TParam3, TParam4, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, param2, param3, param4, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) => Run(func, param1, param2, param3, param4, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options) => Run(func, param1, param2, param3, param4, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, param2, param3, param4, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, param2, param3, param4, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TReturn}, TParam1, TParam2, TParam3, TParam4)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) => () => Run(func, param1, param2, param3, param4, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TReturn}, TParam1, TParam2, TParam3, TParam4, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options) => () => Run(func, param1, param2, param3, param4, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TReturn}, TParam1, TParam2, TParam3, TParam4, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, param2, param3, param4, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5) => Run(action, param1, param2, param3, param4, param5, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options) => Run(action, param1, param2, param3, param4, param5, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, param2, param3, param4, param5, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, param2, param3, param4, param5, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5}(Action{TParam1, TParam2, TParam3, TParam4, TParam5}, TParam1, TParam2, TParam3, TParam4, TParam5)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5) => () => Run(action, param1, param2, param3, param4, param5, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5}(Action{TParam1, TParam2, TParam3, TParam4, TParam5}, TParam1, TParam2, TParam3, TParam4, TParam5, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options) => () => Run(action, param1, param2, param3, param4, param5, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5}(Action{TParam1, TParam2, TParam3, TParam4, TParam5}, TParam1, TParam2, TParam3, TParam4, TParam5, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, param2, param3, param4, param5, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5) => Run(func, param1, param2, param3, param4, param5, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options) => Run(func, param1, param2, param3, param4, param5, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, param2, param3, param4, param5, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, param2, param3, param4, param5, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5) => () => Run(func, param1, param2, param3, param4, param5, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options) => () => Run(func, param1, param2, param3, param4, param5, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, param2, param3, param4, param5, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6) => Run(action, param1, param2, param3, param4, param5, param6, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, param6, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options) => Run(action, param1, param2, param3, param4, param5, param6, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, param6, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, param2, param3, param4, param5, param6, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, param2, param3, param4, param5, param6, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6) => () => Run(action, param1, param2, param3, param4, param5, param6, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options) => () => Run(action, param1, param2, param3, param4, param5, param6, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, param2, param3, param4, param5, param6, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6) => Run(func, param1, param2, param3, param4, param5, param6, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, param6, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options) => Run(func, param1, param2, param3, param4, param5, param6, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, param6, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, param2, param3, param4, param5, param6, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, param2, param3, param4, param5, param6, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6) => () => Run(func, param1, param2, param3, param4, param5, param6, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options) => () => Run(func, param1, param2, param3, param4, param5, param6, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, param2, param3, param4, param5, param6, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7) => Run(action, param1, param2, param3, param4, param5, param6, param7, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, param6, param7, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options) => Run(action, param1, param2, param3, param4, param5, param6, param7, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, param6, param7, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, param2, param3, param4, param5, param6, param7, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, param2, param3, param4, param5, param6, param7, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7) => () => Run(action, param1, param2, param3, param4, param5, param6, param7, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options) => () => Run(action, param1, param2, param3, param4, param5, param6, param7, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, param2, param3, param4, param5, param6, param7, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7) => Run(func, param1, param2, param3, param4, param5, param6, param7, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, param6, param7, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options) => Run(func, param1, param2, param3, param4, param5, param6, param7, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, param6, param7, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, param2, param3, param4, param5, param6, param7, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, param2, param3, param4, param5, param6, param7, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7) => () => Run(func, param1, param2, param3, param4, param5, param6, param7, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options) => () => Run(func, param1, param2, param3, param4, param5, param6, param7, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, param2, param3, param4, param5, param6, param7, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8) => Run(action, param1, param2, param3, param4, param5, param6, param7, param8, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, param6, param7, param8, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="options">The retry options.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options) => Run(action, param1, param2, param3, param4, param5, param6, param7, param8, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(action, param1, param2, param3, param4, param5, param6, param7, param8, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        public static void Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            Run(action, param1, param2, param3, param4, param5, param6, param7, param8, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(action, param1, param2, param3, param4, param5, param6, param7, param8, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8) => () => Run(action, param1, param2, param3, param4, param5, param6, param7, param8, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options) => () => Run(action, param1, param2, param3, param4, param5, param6, param7, param8, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8}(Action{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Action Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(action, param1, param2, param3, param4, param5, param6, param7, param8, options);
        }

        /// <summary>
        /// Retry now, with a default set of options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8) => Run(func, param1, param2, param3, param4, param5, param6, param7, param8, new RetryOptions());

        /// <summary>
        /// Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, param6, param7, param8, new RetryOptions(), cancellationToken);

        /// <summary>
        /// Retry now, with specified options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options) => Run(func, param1, param2, param3, param4, param5, param6, param7, param8, options);

        /// <summary>
        /// Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options, CancellationToken cancellationToken = default) => RunAsync(func, param1, param2, param3, param4, param5, param6, param7, param8, options, cancellationToken);

        /// <summary>
        /// Retry now, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>A return value, as defined by the invoked method.</returns>
        public static TReturn Now<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return Run(func, param1, param2, param3, param4, param5, param6, param7, param8, options);
        }

        /// <summary>
        /// Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}" /> that can be awaited on, with a result as defined by the invoked method.</returns>
        public static Task<TReturn> NowAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<RetryOptions> optionsSetter, CancellationToken cancellationToken = default)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return RunAsync(func, param1, param2, param3, param4, param5, param6, param7, param8, options, cancellationToken);
        }

        /// <summary>
        /// Retry an action later.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8) => () => Run(func, param1, param2, param3, param4, param5, param6, param7, param8, new RetryOptions());

        /// <summary>
        /// Retry an action later, with retry options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="options">The retry options.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, RetryOptions)"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options) => () => Run(func, param1, param2, param3, param4, param5, param6, param7, param8, options);

        /// <summary>
        /// Retry an action later, with a method to build up options.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the retried method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the retried method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the retried method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the retried method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the retried method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the retried method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the retried method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the retried method at index 7.</typeparam>
        /// <typeparam name="TReturn">The return type of the invoked method.</typeparam>
        /// <param name="func">The action to try and retry.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the retried method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the retried method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the retried method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the retried method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the retried method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the retried method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the retried method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the retried method at index 7.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <returns>An <see cref="System.Func{TResult}" /> that can be executed whenever required.</returns>
        /// <remarks>
        /// <para>Please be advised that, in order to properly function, this method creates a closure.</para>
        /// <para>For now, this is unavoidable.</para>
        /// <para>If you would like to avoid the creation of a closure, please use, instead, the <see cref="Now{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn}(Func{TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn}, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Action{RetryOptions})"/> method and implement the delay of action yourself.</para>
        /// </remarks>
        public static Func<TReturn> Later<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<RetryOptions> optionsSetter)
        {
            if (optionsSetter == null)
            {
                throw new ArgumentNullException(nameof(optionsSetter));
            }

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(func, param1, param2, param3, param4, param5, param6, param7, param8, options);
        }

        private static async Task RunAsync<TParam1>(Action<TParam1> action, TParam1 param1, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith1ParamRetryContext<TParam1>(action, param1, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1>(in Action<TParam1> action, TParam1 param1, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith1ParamRetryContext<TParam1>(action, param1, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TReturn>(Func<TParam1, TReturn> func, TParam1 param1, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith1ParamRetryContext<TParam1, TReturn>(func, param1, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TReturn>(in Func<TParam1, TReturn> func, TParam1 param1, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith1ParamRetryContext<TParam1, TReturn>(func, param1, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }

        private static async Task RunAsync<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith2ParamRetryContext<TParam1, TParam2>(action, param1, param2, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1, TParam2>(in Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith2ParamRetryContext<TParam1, TParam2>(action, param1, param2, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith2ParamRetryContext<TParam1, TParam2, TReturn>(func, param1, param2, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TParam2, TReturn>(in Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith2ParamRetryContext<TParam1, TParam2, TReturn>(func, param1, param2, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }

        private static async Task RunAsync<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith3ParamRetryContext<TParam1, TParam2, TParam3>(action, param1, param2, param3, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1, TParam2, TParam3>(in Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith3ParamRetryContext<TParam1, TParam2, TParam3>(action, param1, param2, param3, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith3ParamRetryContext<TParam1, TParam2, TParam3, TReturn>(func, param1, param2, param3, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TParam2, TParam3, TReturn>(in Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith3ParamRetryContext<TParam1, TParam2, TParam3, TReturn>(func, param1, param2, param3, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }

        private static async Task RunAsync<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith4ParamRetryContext<TParam1, TParam2, TParam3, TParam4>(action, param1, param2, param3, param4, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1, TParam2, TParam3, TParam4>(in Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith4ParamRetryContext<TParam1, TParam2, TParam3, TParam4>(action, param1, param2, param3, param4, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith4ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TReturn>(func, param1, param2, param3, param4, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TParam2, TParam3, TParam4, TReturn>(in Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith4ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TReturn>(func, param1, param2, param3, param4, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }

        private static async Task RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith5ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5>(action, param1, param2, param3, param4, param5, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1, TParam2, TParam3, TParam4, TParam5>(in Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith5ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5>(action, param1, param2, param3, param4, param5, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith5ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(func, param1, param2, param3, param4, param5, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(in Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith5ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(func, param1, param2, param3, param4, param5, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }

        private static async Task RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith6ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(action, param1, param2, param3, param4, param5, param6, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(in Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith6ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(action, param1, param2, param3, param4, param5, param6, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith6ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(func, param1, param2, param3, param4, param5, param6, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(in Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith6ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(func, param1, param2, param3, param4, param5, param6, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }

        private static async Task RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith7ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(action, param1, param2, param3, param4, param5, param6, param7, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(in Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith7ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(action, param1, param2, param3, param4, param5, param6, param7, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith7ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(func, param1, param2, param3, param4, param5, param6, param7, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(in Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith7ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn>(func, param1, param2, param3, param4, param5, param6, param7, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }

        private static async Task RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(action, options, cancellationToken);

            using (var context = new ActionWith8ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(action, param1, param2, param3, param4, param5, param6, param7, param8, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(in Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, in RetryOptions options)
        {
            ValidateRunning(action, options, default);

            using (var context = new ActionWith8ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(action, param1, param2, param3, param4, param5, param6, param7, param8, options))
            {
                context.BeginRetryProcess();
            }
        }

        private static async Task<TReturn> RunAsync<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions options, CancellationToken cancellationToken)
        {
            ValidateRunning(func, options, cancellationToken);

            using (var context = new FuncWith8ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(func, param1, param2, param3, param4, param5, param6, param7, param8, options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);

                return context.GetReturnValue();
            }
        }

        private static TReturn Run<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(in Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, in RetryOptions options)
        {
            ValidateRunning(func, options, default);

            using (var context = new FuncWith8ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn>(func, param1, param2, param3, param4, param5, param6, param7, param8, options))
            {
                context.BeginRetryProcess();

                return context.GetReturnValue();
            }
        }
    }
}