// <copyright file="Fire.AndForget.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class that provides methods and extensions to fire events.
    /// </summary>
    public static partial class Fire
    {
        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Action action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Action action, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(action, cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group
            }

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
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Action<CancellationToken> action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Action<CancellationToken> action, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Action<CancellationToken>, CancellationToken>)state;
                    brokenState.Item1(brokenState.Item2);
                },
                new Tuple<Action<CancellationToken>, CancellationToken>(action, cancellationToken),
                cancellationToken);

            if (exceptionHandler != null)
            {
                // No sense in running the continuation if there's nobody listening
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable
                runningTask.ContinueWith(
                    continuationAction: StandardContinuation,
                    state: exceptionHandler,
                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                    scheduler: GetCurrentTaskScheduler(),
                    cancellationToken: cancellationToken);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group
            }

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
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Func<Task> action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Func<Task> action, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<Task>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Task task = brokenState.Item1();

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item2 != null)
                            {
                                // No sense in running the continuation if there's nobody listening, or if it's already finished synchronously
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item2,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item3,
                                    cancellationToken: brokenState.Item4);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // It is possible that the task completes synchronously (i.e. untimely exceptions or bad async coding).
                        // We therefore need to treat this like a possible quick exit and also invoke the exception handler on its original context.
                        if (brokenState.Item2 != null)
                        {
                            // Again, no sense in invoking anything if there is nobody listening
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item2),
                            brokenState.Item4);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item3);
                        }
                    }
                },
                new Tuple<Func<Task>, Action<Exception>, TaskScheduler, CancellationToken>(action, exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
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
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget(Func<CancellationToken, Task> action, Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = new Task(
                (state) =>
                {
                    var brokenState = (Tuple<Func<CancellationToken, Task>, Action<Exception>, TaskScheduler, CancellationToken>)state;
                    try
                    {
                        Task task = brokenState.Item1(brokenState.Item4);

                        if (!task.IsCompleted)
                        {
                            task.ConfigureAwait(false);

                            if (brokenState.Item2 != null)
                            {
                                // No sense in running the continuation if there's nobody listening, or if it's already finished synchronously
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable
                                task.ContinueWith(
                                    continuationAction: StandardContinuation,
                                    state: brokenState.Item2,
                                    continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                                    scheduler: brokenState.Item3,
                                    cancellationToken: brokenState.Item4);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // It is possible that the task completes synchronously (i.e. untimely exceptions or bad async coding).
                        // We therefore need to treat this like a possible quick exit and also invoke the exception handler on its original context.
                        if (brokenState.Item2 != null)
                        {
                            // Again, no sense in invoking anything if there is nobody listening
                            var task = new Task(
                            (state2) =>
                            {
                                var brokenState2 = (Tuple<Exception, Action<Exception>>)state2;

                                brokenState2.Item2(brokenState2.Item1);
                            },
                            new Tuple<Exception, Action<Exception>>(ex, brokenState.Item2),
                            brokenState.Item4);

                            task.ConfigureAwait(false);

                            task.Start(brokenState.Item3);
                        }
                    }
                },
                new Tuple<Func<CancellationToken, Task>, Action<Exception>, TaskScheduler, CancellationToken>(action, exceptionHandler, GetCurrentTaskScheduler(), cancellationToken),
                cancellationToken);

            runningTask.Start(TaskScheduler.Default);

            if (runningTask.IsCompleted)
            {
                return;
            }

            runningTask.ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TaskScheduler GetCurrentTaskScheduler()
        {
            try
            {
                return SynchronizationContext.Current == null ? TaskScheduler.Default : TaskScheduler.FromCurrentSynchronizationContext();
            }
            catch (InvalidOperationException)
            {
                return TaskScheduler.Default;
            }
        }
    }
}