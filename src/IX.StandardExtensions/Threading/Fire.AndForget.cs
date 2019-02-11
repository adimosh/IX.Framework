// <copyright file="Fire.AndForget.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using IX.StandardExtensions.Contracts;

using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class that provides methods and extensions to fire events.
    /// </summary>
    [PublicAPI]
    public static partial class Fire
    {
        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Action action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Action action, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            var runningTask = Task.Factory.StartOnNewThread(
                action,
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
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Action<CancellationToken> action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Action<CancellationToken> action, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            var runningTask = Task.Factory.StartOnNewThread(
                action,
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
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Func<Task> action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Func<Task> action, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke();
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
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
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Func<CancellationToken, Task> action, CancellationToken cancellationToken = default) => AndForget(action, EnvironmentSettings.DefaultFireAndForgetUnhandledExceptionHandler, cancellationToken);

        /// <summary>
        /// Fires a method on a separate thread, and forgets about it completely, only invoking a continuation if there was an exception.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="exceptionHandler">The exception handler. This parameter can be null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void AndForget([CanBeNull] Func<CancellationToken, Task> action, [CanBeNull] Action<Exception> exceptionHandler, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(action, nameof(action));

            // We invoke our task-yielding operation in a different thread, guaranteed
            var runningTask = Task.Factory.StartOnNewThread(
                (actionL1, cancellationTokenL1) =>
                {
                    var innerTask = actionL1.Invoke(cancellationTokenL1);
                    innerTask.Wait(cancellationTokenL1);
                },
                action,
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
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