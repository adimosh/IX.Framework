// <copyright file="TaskFactoryExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using IX.StandardExtensions.Contracts;

using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class containing extension methods for <see cref="TaskFactory"/>, mostly intended for use with <see cref="Task.Factory"/>.
    /// </summary>
    [PublicAPI]
    public static partial class TaskFactoryExtensions
    {
        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler(
            this TaskFactory taskFactory,
            Action action,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState => ((Action)rawState)(),
                action,
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started long-running task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler(
            this TaskFactory taskFactory,
            Action action,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState => ((Action)rawState)(),
                action,
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TResult>(
            this TaskFactory taskFactory,
            Func<TResult> action,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState => ((Func<TResult>)rawState)(),
                action,
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started long-running task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TResult>(
            this TaskFactory taskFactory,
            Func<TResult> action,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState => ((Func<TResult>)rawState)(),
                action,
                true,
                cancellationToken);
        }

        internal static Task StartWithStateOnDefaultTaskScheduler(
            TaskFactory taskFactory,
            Action<object> action,
            object state,
            bool longRunning,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullPrivate(in taskFactory, nameof(taskFactory));
            Contract.RequiresNotNullPrivate(in action, nameof(action));

            var creationOptions = TaskCreationOptions.HideScheduler | (longRunning ? TaskCreationOptions.LongRunning : TaskCreationOptions.PreferFairness);

            return taskFactory.StartNew(
#if !NETSTANDARD1_2
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is expected
                StartAction,
#pragma warning restore HAA0603 // Delegate allocation from a method group
                new Tuple<Action<object>, CultureInfo, CultureInfo, object>(action, CultureInfo.CurrentCulture, CultureInfo.CurrentUICulture, state),
#else
                action,
                state,
#endif
                cancellationToken,
                creationOptions,
                TaskScheduler.Default);

#if !NETSTANDARD1_2
            void StartAction(object rawState)
            {
                Contract.RequiresNotNullPrivate(in rawState, nameof(rawState));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Action<object>, CultureInfo, CultureInfo, object>>(rawState, nameof(rawState));

                var innerState = (Tuple<Action<object>, CultureInfo, CultureInfo, object>)rawState;

#if FRAMEWORK_GT_451 || STANDARD_GT_12
                CultureInfo.CurrentCulture = innerState.Item2;
                CultureInfo.CurrentUICulture = innerState.Item3;
#elif FRAMEWORK
#pragma warning disable DE0008 // API is deprecated - This is an acceptable use, since we're writing on what's guaranteed to be the current thread
                Thread.CurrentThread.CurrentCulture = innerState.Item2;
                Thread.CurrentThread.CurrentUICulture = innerState.Item3;
#pragma warning restore DE0008 // API is deprecated
#endif

                innerState.Item1(innerState.Item4);
            }
#endif
        }

        internal static Task<TResult> StartWithStateOnDefaultTaskScheduler<TResult>(
            TaskFactory taskFactory,
            Func<object, TResult> action,
            object state,
            bool longRunning,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullPrivate(in taskFactory, nameof(taskFactory));
            Contract.RequiresNotNullPrivate(in action, nameof(action));

            var creationOptions = TaskCreationOptions.HideScheduler | (longRunning ? TaskCreationOptions.LongRunning : TaskCreationOptions.PreferFairness);

            return taskFactory.StartNew(
#if !NETSTANDARD1_2
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is expected
                StartAction,
#pragma warning restore HAA0603 // Delegate allocation from a method group
                new Tuple<Func<object, TResult>, CultureInfo, CultureInfo, object>(action, CultureInfo.CurrentCulture, CultureInfo.CurrentUICulture, state),
#else
                action,
                state,
#endif
                cancellationToken,
                creationOptions,
                TaskScheduler.Default);

#if !NETSTANDARD1_2
            TResult StartAction(object rawState)
            {
                Contract.RequiresNotNullPrivate(in rawState, nameof(rawState));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Func<object, TResult>, CultureInfo, CultureInfo, object>>(rawState, nameof(rawState));

                var innerState = (Tuple<Func<object, TResult>, CultureInfo, CultureInfo, object>)rawState;

#if FRAMEWORK_GT_451 || STANDARD_GT_12
                CultureInfo.CurrentCulture = innerState.Item2;
                CultureInfo.CurrentUICulture = innerState.Item3;
#elif FRAMEWORK
#pragma warning disable DE0008 // API is deprecated - This is an acceptable use, since we're writing on what's guaranteed to be the current thread
                Thread.CurrentThread.CurrentCulture = innerState.Item2;
                Thread.CurrentThread.CurrentUICulture = innerState.Item3;
#pragma warning restore DE0008 // API is deprecated
#endif

                return innerState.Item1(innerState.Item4);
            }
#endif
        }
    }
}