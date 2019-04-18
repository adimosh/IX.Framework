// <copyright file="Fire.ExecuteOnThreadPool.Void.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using IX.StandardExtensions.Contracts;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    ///     A class that provides methods and extensions to fire events.
    /// </summary>
    public partial class Fire
    {
        private static Task ExecuteOnThreadPool(
            Action action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Action>(
                        st,
                        nameof(st));

                    ct.ThrowIfCancellationRequested();

                    ((Action)st)();

                    return 0;
                }, action,
                cancellationToken);

        private static Task ExecuteOnThreadPool(
            Action<CancellationToken> action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Action<CancellationToken>>(
                        st,
                        nameof(st));

                    ((Action<CancellationToken>)st)(ct);

                    return 0;
                }, action,
                cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<TResult> action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Func<TResult>>(
                        st,
                        nameof(st));

                    ct.ThrowIfCancellationRequested();

                    return ((Func<TResult>)st)();
                }, action,
                cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<CancellationToken, TResult> action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Func<CancellationToken, TResult>>(
                        st,
                        nameof(st));

                    return ((Func<CancellationToken, TResult>)st)(ct);
                }, action,
                cancellationToken);

        private static Task ExecuteOnThreadPool(
            Action<object> action,
            object state,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
            (
                st,
                ct) =>
            {
                Contract.RequiresNotNullPrivate(
                    st,
                    nameof(st));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Action<object>, object>>(
                    st,
                    nameof(st));

                var innerState = (Tuple<Action<object>, object>)st;
                innerState.Item1(innerState.Item2);
                return 0;
            }, new Tuple<Action<object>, object>(
                action,
                state), cancellationToken);

        private static Task ExecuteOnThreadPool(
            Action<object, CancellationToken> action,
            object state,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
            (
                st,
                ct) =>
            {
                Contract.RequiresNotNullPrivate(
                    st,
                    nameof(st));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Action<object, CancellationToken>, object>>(
                    st,
                    nameof(st));

                var innerState = (Tuple<Action<object, CancellationToken>, object>)st;
                innerState.Item1(
                    innerState.Item2,
                    ct);
                return 0;
            }, new Tuple<Action<object, CancellationToken>, object>(
                action,
                state), cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<object, TResult> action,
            object state,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
            (
                st,
                ct) =>
            {
                Contract.RequiresNotNullPrivate(
                    st,
                    nameof(st));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Func<object, TResult>, object>>(
                    st,
                    nameof(st));

                ct.ThrowIfCancellationRequested();

                var innerState = (Tuple<Func<object, TResult>, object>)st;

                return innerState.Item1(innerState.Item2);
            }, new Tuple<Func<object, TResult>, object>(
                action,
                state), cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<object, CancellationToken, TResult> action,
            object state,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullPrivate(
                action,
                nameof(action));

            var taskCompletionSource = new TaskCompletionSource<TResult>();

            if (cancellationToken.IsCancellationRequested)
            {
                taskCompletionSource.TrySetCanceled();
                return taskCompletionSource.Task;
            }

#if NETSTANDARD1_2
            var outerState =
                new Tuple<Func<object, CancellationToken, TResult>, TaskCompletionSource<TResult>,
                    object, CancellationToken>(
#else
            var outerState =
                new Tuple<Func<object, CancellationToken, TResult>, CultureInfo, CultureInfo,
                    TaskCompletionSource<TResult>, object, CancellationToken>(
#endif
#pragma warning disable SA1114 // Parameter list should follow declaration
                    action,
#pragma warning restore SA1114 // Parameter list should follow declaration
#if !NETSTANDARD1_2
                    CultureInfo.CurrentCulture,
                    CultureInfo.CurrentUICulture,
#endif
                    taskCompletionSource,
                    state,
                    cancellationToken);

#if NETSTANDARD1_2
            Task.Factory.StartNew(
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is expected, and also acceptable
                WorkItem,
#pragma warning restore HAA0603 // Delegate allocation from a method group
                outerState,
                TaskCreationOptions.HideScheduler | TaskCreationOptions.LongRunning);
#else
            ThreadPool.QueueUserWorkItem(
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is expected, and also acceptable
                WorkItem,
#pragma warning restore HAA0603 // Delegate allocation from a method group
                outerState);
#endif

            void WorkItem(object rawState)
            {
                Contract.RequiresNotNullPrivate(
                    rawState,
                    nameof(rawState));
#if NETSTANDARD1_2
                Contract.RequiresArgumentOfTypePrivate<Tuple<Func<object, CancellationToken, TResult>, TaskCompletionSource<TResult>, object, CancellationToken>>(
                    rawState,
                    nameof(rawState));
#else
                Contract
                    .RequiresArgumentOfTypePrivate<
                        Tuple<Func<object, CancellationToken, TResult>, CultureInfo, CultureInfo,
                            TaskCompletionSource<TResult>, object, CancellationToken>>(
                        rawState,
                        nameof(rawState));
#endif

#if NETSTANDARD1_2
                var innerState =
                    (Tuple<Func<object, CancellationToken, TResult>, TaskCompletionSource<TResult>, object, CancellationToken>)rawState;

                var tcs = innerState.Item2;
                var payload = innerState.Item3;
                var ct = innerState.Item4;
#else
                var innerState =
                    (Tuple<Func<object, CancellationToken, TResult>, CultureInfo, CultureInfo,
                        TaskCompletionSource<TResult>, object, CancellationToken>)rawState;

                TaskCompletionSource<TResult> tcs = innerState.Item4;
                object payload = innerState.Item5;
                CancellationToken ct = innerState.Item6;
#endif

                if (ct.IsCancellationRequested)
                {
                    tcs.TrySetCanceled();
                    return;
                }

#if FRAMEWORK_GT_451 || STANDARD_GT_12
                CultureInfo.CurrentCulture = innerState.Item2;
                CultureInfo.CurrentUICulture = innerState.Item3;
#elif FRAMEWORK
#pragma warning disable DE0008 // API is deprecated - This is an acceptable use, since we're writing on what's guaranteed to be the current thread
                Thread.CurrentThread.CurrentCulture = innerState.Item2;
                Thread.CurrentThread.CurrentUICulture = innerState.Item3;
#pragma warning restore DE0008 // API is deprecated
#endif

                if (ct.IsCancellationRequested)
                {
                    tcs.TrySetCanceled();
                    return;
                }

                try
                {
                    TResult innerResult = innerState.Item1(
                        payload,
                        ct);

                    tcs.TrySetResult(innerResult);
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
            }

            return taskCompletionSource.Task;
        }
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
    }
}