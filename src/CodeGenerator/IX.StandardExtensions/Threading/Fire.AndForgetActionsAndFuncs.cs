// <copyright file="Fire.AndForgetActionsAndFuncs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

using IX.StandardExtensions.Contracts;

using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class that provides methods and extensions to fire events.
    /// </summary>
    public static partial class Fire
    {
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - The lambdas themselves rely on generics
        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Action<TParam1> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Action<TParam1> action, TParam1 param1, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1>, Tuple<TParam1>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1>, Tuple<TParam1>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1);

                    return 0;
                },
                new Tuple<Action<TParam1>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Action<TParam1, CancellationToken> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Action<TParam1, CancellationToken> action, TParam1 param1, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, CancellationToken>, Tuple<TParam1>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, CancellationToken>, Tuple<TParam1>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, CancellationToken>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Func<TParam1, Task> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Func<TParam1, Task> action, TParam1 param1, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, Task>, Tuple<TParam1>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, Task>, Tuple<TParam1>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1);
                },
                new Tuple<Func<TParam1, Task>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Func<TParam1, CancellationToken, Task> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>([CanBeNull] Func<TParam1, CancellationToken, Task> action, TParam1 param1, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, CancellationToken, Task>, Tuple<TParam1>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, CancellationToken, Task>, Tuple<TParam1>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, ct);
                },
                new Tuple<Func<TParam1, CancellationToken, Task>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Action<TParam1, TParam2, CancellationToken> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Action<TParam1, TParam2, CancellationToken> action, TParam1 param1, TParam2 param2, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, CancellationToken>, Tuple<TParam1, TParam2>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, CancellationToken>, Tuple<TParam1, TParam2>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, CancellationToken>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Func<TParam1, TParam2, Task> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Func<TParam1, TParam2, Task> action, TParam1 param1, TParam2 param2, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, Task>, Tuple<TParam1, TParam2>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, Task>, Tuple<TParam1, TParam2>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2);
                },
                new Tuple<Func<TParam1, TParam2, Task>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Func<TParam1, TParam2, CancellationToken, Task> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2>([CanBeNull] Func<TParam1, TParam2, CancellationToken, Task> action, TParam1 param1, TParam2 param2, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, CancellationToken, Task>, Tuple<TParam1, TParam2>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, CancellationToken, Task>, Tuple<TParam1, TParam2>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, ct);
                },
                new Tuple<Func<TParam1, TParam2, CancellationToken, Task>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Action<TParam1, TParam2, TParam3, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Action<TParam1, TParam2, TParam3, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, CancellationToken>, Tuple<TParam1, TParam2, TParam3>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, CancellationToken>, Tuple<TParam1, TParam2, TParam3>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, CancellationToken>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Func<TParam1, TParam2, TParam3, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Func<TParam1, TParam2, TParam3, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, Task>, Tuple<TParam1, TParam2, TParam3>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, Task>, Tuple<TParam1, TParam2, TParam3>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, Task>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Func<TParam1, TParam2, TParam3, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3>([CanBeNull] Func<TParam1, TParam2, TParam3, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, ct);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, ct);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, ct);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, ct);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, ct);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, param8, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, param8, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>>(st, nameof(st));

                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest, ct);

                    return 0;
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, param8, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
            => AndForget(action, param1, param2, param3, param4, param5, param6, param7, param8, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
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
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>([CanBeNull] Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(st, nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>>(st, nameof(st));

                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)st;
                    var actionL1 = innerState.Item1;
                    var unpackedParameters = innerState.Item2;

                    ct.ThrowIfCancellationRequested();

                    return actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest, ct);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HAA0603 // Delegate allocation from a method group
            }
        }
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance

    }
}