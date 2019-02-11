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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1),
                action,
                new Tuple<TParam1>(param1),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1>(param1),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1>(param1),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1>(param1),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1, unpackedParameters.Item2),
                action,
                new Tuple<TParam1, TParam2>(param1, param2),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2>(param1, param2),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2>(param1, param2),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2>(param1, param2),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3),
                action,
                new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4),
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5),
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6),
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7),
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters) => actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest),
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8),
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    actionL1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest, cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8),
                cancellationToken,
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
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, unpackedParameters, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest, cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
                new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8),
                cancellationToken,
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