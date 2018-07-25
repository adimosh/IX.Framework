// <copyright file="Fire.AndForgetActionsAndFuncs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class that provides methods and extensions to fire events.
    /// </summary>
    public static partial class Fire
    {
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable
#pragma warning disable HeapAnalyzerLambdaInGenericMethodRule // Lambda or anonymous method in a generic method allocates a delegate instance - The lambdas themselves rely on generics
        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Action<TParam1> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Action<TParam1> action, TParam1 param1, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1>, Tuple<TParam1>>)innerState;
                Tuple<TParam1> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Action<TParam1, CancellationToken> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Action<TParam1, CancellationToken> action, TParam1 param1, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, CancellationToken>, Tuple<TParam1>, CancellationToken>(action, new Tuple<TParam1>(param1), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, CancellationToken>, Tuple<TParam1>, CancellationToken>)innerState;
                Tuple<TParam1> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Func<TParam1, Task> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Func<TParam1, Task> action, TParam1 param1, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, Task>, Tuple<TParam1>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, Task>, Tuple<TParam1>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1>(param1), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
        }

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Func<TParam1, CancellationToken, Task> action, TParam1 param1, CancellationToken cancellationToken = default)
            => AndForget(action, param1, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget<TParam1>(Func<TParam1, CancellationToken, Task> action, TParam1 param1, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, CancellationToken, Task>, Tuple<TParam1>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, CancellationToken, Task>, Tuple<TParam1>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1>(param1), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2>(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)innerState;
                Tuple<TParam1, TParam2> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2>(Action<TParam1, TParam2, CancellationToken> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2>(Action<TParam1, TParam2, CancellationToken> action, TParam1 param1, TParam2 param2, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, CancellationToken>, Tuple<TParam1, TParam2>, CancellationToken>(action, new Tuple<TParam1, TParam2>(param1, param2), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, CancellationToken>, Tuple<TParam1, TParam2>, CancellationToken>)innerState;
                Tuple<TParam1, TParam2> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2>(Func<TParam1, TParam2, Task> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2>(Func<TParam1, TParam2, Task> action, TParam1 param1, TParam2 param2, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, Task>, Tuple<TParam1, TParam2>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, Task>, Tuple<TParam1, TParam2>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2>(param1, param2), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2>(Func<TParam1, TParam2, CancellationToken, Task> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2>(Func<TParam1, TParam2, CancellationToken, Task> action, TParam1 param1, TParam2 param2, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, CancellationToken, Task>, Tuple<TParam1, TParam2>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, CancellationToken, Task>, Tuple<TParam1, TParam2>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2>(param1, param2), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)innerState;
                Tuple<TParam1, TParam2, TParam3> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, CancellationToken>, Tuple<TParam1, TParam2, TParam3>, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, CancellationToken>, Tuple<TParam1, TParam2, TParam3>, CancellationToken>)innerState;
                Tuple<TParam1, TParam2, TParam3> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3>(Func<TParam1, TParam2, TParam3, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3>(Func<TParam1, TParam2, TParam3, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, Task>, Tuple<TParam1, TParam2, TParam3>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, Task>, Tuple<TParam1, TParam2, TParam3>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3>(Func<TParam1, TParam2, TParam3, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3>(Func<TParam1, TParam2, TParam3, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4>, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4>, CancellationToken>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Func<TParam1, TParam2, TParam3, TParam4, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Func<TParam1, TParam2, TParam3, TParam4, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4>(Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>, CancellationToken>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, CancellationToken>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, CancellationToken>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8));
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var state = new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8), cancellationToken);
            var runningTask = new Task(ExecuteAction, state, cancellationToken);

            void ExecuteAction(object innerState)
            {
                var unpackedState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, CancellationToken>)innerState;
                Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = unpackedState.Item2;

                unpackedState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest, unpackedState.Item3);
            }

            runningTask.ContinueWith(
                continuationAction: StandardContinuation,
                state: exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: GetCurrentTaskScheduler(),
                cancellationToken: cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
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
        public static void AndForget<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> unpackedParameters = brokenState.Item2;
                        Task task = brokenState.Item1(unpackedParameters.Item1, unpackedParameters.Item2, unpackedParameters.Item3, unpackedParameters.Item4, unpackedParameters.Item5, unpackedParameters.Item6, unpackedParameters.Item7, unpackedParameters.Rest, brokenState.Item5);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item3 != null)
                            {
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item3,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item4,
                                    cancellationToken: brokenState.Item5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (brokenState.Item3 != null)
                        {
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item3),
                            brokenState.Item5);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item4);
                        }
                    }
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, CancellationToken, Task>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Action<Exception>, TaskScheduler, CancellationToken>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8), exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
        }
#pragma warning restore HeapAnalyzerLambdaInGenericMethodRule // Lambda or anonymous method in a generic method allocates a delegate instance
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group

    }
}