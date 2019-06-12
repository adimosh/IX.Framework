// <copyright file="TaskFactoryExtensions.Actions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

using IX.StandardExtensions.Contracts;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class containing extension methods for <see cref="TaskFactory"/>, mostly intended for use with <see cref="Task.Factory"/>.
    /// </summary>
    public static partial class TaskFactoryExtensions
    {
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1>(this TaskFactory taskFactory, Action<TParam1> action, TParam1 param1, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1>, Tuple<TParam1>>)rawState;

                    var item1 = innerState.Item2.Item1;

                    innerState.Item1(item1);
                },
                new Tuple<Action<TParam1>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1>(this TaskFactory taskFactory, Action<TParam1> action, TParam1 param1, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1>, Tuple<TParam1>>)rawState;

                    var item1 = innerState.Item2.Item1;

                    innerState.Item1(item1);
                },
                new Tuple<Action<TParam1>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TResult>(this TaskFactory taskFactory, Func<TParam1, TResult> action, TParam1 param1, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TResult>, Tuple<TParam1>>)rawState;

                    var item1 = innerState.Item2.Item1;

                    return innerState.Item1(item1);
                },
                new Tuple<Func<TParam1, TResult>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TResult>(this TaskFactory taskFactory, Func<TParam1, TResult> action, TParam1 param1, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TResult>, Tuple<TParam1>>)rawState;

                    var item1 = innerState.Item2.Item1;

                    return innerState.Item1(item1);
                },
                new Tuple<Func<TParam1, TResult>, Tuple<TParam1>>(action, new Tuple<TParam1>(param1)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1, TParam2>(this TaskFactory taskFactory, Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;

                    innerState.Item1(item1, item2);
                },
                new Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2>(this TaskFactory taskFactory, Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;

                    innerState.Item1(item1, item2);
                },
                new Tuple<Action<TParam1, TParam2>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TParam2, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TResult> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TResult>, Tuple<TParam1, TParam2>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;

                    return innerState.Item1(item1, item2);
                },
                new Tuple<Func<TParam1, TParam2, TResult>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TResult> action, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TResult>, Tuple<TParam1, TParam2>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;

                    return innerState.Item1(item1, item2);
                },
                new Tuple<Func<TParam1, TParam2, TResult>, Tuple<TParam1, TParam2>>(action, new Tuple<TParam1, TParam2>(param1, param2)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;

                    innerState.Item1(item1, item2, item3);
                },
                new Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;

                    innerState.Item1(item1, item2, item3);
                },
                new Tuple<Action<TParam1, TParam2, TParam3>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TResult>, Tuple<TParam1, TParam2, TParam3>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;

                    return innerState.Item1(item1, item2, item3);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TResult>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TResult>, Tuple<TParam1, TParam2, TParam3>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;

                    return innerState.Item1(item1, item2, item3);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TResult>, Tuple<TParam1, TParam2, TParam3>>(action, new Tuple<TParam1, TParam2, TParam3>(param1, param2, param3)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;

                    innerState.Item1(item1, item2, item3, item4);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;

                    innerState.Item1(item1, item2, item3, item4);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;

                    return innerState.Item1(item1, item2, item3, item4);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;

                    return innerState.Item1(item1, item2, item3, item4);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4>(param1, param2, param3, param4)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;

                    innerState.Item1(item1, item2, item3, item4, item5);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;

                    innerState.Item1(item1, item2, item3, item4, item5);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;

                    return innerState.Item1(item1, item2, item3, item4, item5);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;

                    return innerState.Item1(item1, item2, item3, item4, item5);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5>(param1, param2, param3, param4, param5)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;

                    innerState.Item1(item1, item2, item3, item4, item5, item6);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;

                    innerState.Item1(item1, item2, item3, item4, item5, item6);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;

                    return innerState.Item1(item1, item2, item3, item4, item5, item6);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;

                    return innerState.Item1(item1, item2, item3, item4, item5, item6);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(param1, param2, param3, param4, param5, param6)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;

                    innerState.Item1(item1, item2, item3, item4, item5, item6, item7);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;

                    innerState.Item1(item1, item2, item3, item4, item5, item6, item7);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;

                    return innerState.Item1(item1, item2, item3, item4, item5, item6, item7);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;

                    return innerState.Item1(item1, item2, item3, item4, item5, item6, item7);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(param1, param2, param3, param4, param5, param6, param7)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;
                    var item8 = innerState.Item2.Rest;

                    innerState.Item1(item1, item2, item3, item4, item5, item6, item7, item8);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(this TaskFactory taskFactory, Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;
                    var item8 = innerState.Item2.Rest;

                    innerState.Item1(item1, item2, item3, item4, item5, item6, item7, item8);
                },
                new Tuple<Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                true,
                cancellationToken);
        }

        /// <summary>
        /// Starts a task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;
                    var item8 = innerState.Item2.Rest;

                    return innerState.Item1(item1, item2, item3, item4, item5, item6, item7, item8);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                false,
                cancellationToken);
        }

        /// <summary>
        /// Starts a long-running task on a new thread.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="taskFactory">The task factory to extend.</param>
        /// <param name="action">The action to start on a new thread.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="Task"/> that represents the started task.</returns>
        public static Task<TResult> StartLongRunningOnDefaultTaskScheduler<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>(this TaskFactory taskFactory, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(in action, nameof(action));

            return StartWithStateOnDefaultTaskScheduler(
                taskFactory,
                rawState =>
                {
                    var innerState = (Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>)rawState;

                    var item1 = innerState.Item2.Item1;
                    var item2 = innerState.Item2.Item2;
                    var item3 = innerState.Item2.Item3;
                    var item4 = innerState.Item2.Item4;
                    var item5 = innerState.Item2.Item5;
                    var item6 = innerState.Item2.Item6;
                    var item7 = innerState.Item2.Item7;
                    var item8 = innerState.Item2.Rest;

                    return innerState.Item1(item1, item2, item3, item4, item5, item6, item7, item8);
                },
                new Tuple<Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>, Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>>(action, new Tuple<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(param1, param2, param3, param4, param5, param6, param7, param8)),
                true,
                cancellationToken);
        }
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
    }
}