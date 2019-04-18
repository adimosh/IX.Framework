// <copyright file="Fire.ExecuteOnThreadPool.Void.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
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
            Func<Task> action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
#pragma warning disable HAA0301 // Closure Allocation Source
                (
                    st,
                    ct) =>
#pragma warning restore HAA0301 // Closure Allocation Source
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Func<Task>>(
                        st,
                        nameof(st));

                    ct.ThrowIfCancellationRequested();

#pragma warning disable HAA0302 // Display class allocation to capture closure
                    var tcs = new TaskCompletionSource<int>();
#pragma warning restore HAA0302 // Display class allocation to capture closure
#pragma warning disable HAA0603 // Delegate allocation from a method group
                    ((Func<Task>)st)().ContinueWith(Continuation, TaskContinuationOptions.ExecuteSynchronously);
#pragma warning restore HAA0603 // Delegate allocation from a method group

                    void Continuation(Task completedTask)
                    {
                        if (completedTask.IsCanceled)
                        {
                            tcs.TrySetCanceled();
                        }
                        else if (completedTask.IsFaulted)
                        {
                            tcs.TrySetException(completedTask.Exception?.InnerExceptions ?? (IEnumerable<Exception>)new Exception[0]);
                        }
                        else
                        {
                            tcs.TrySetResult(0);
                        }
                    }

                    return tcs.Task;
                }, action,
                cancellationToken);

        private static Task ExecuteOnThreadPool(
            Func<CancellationToken, Task> action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
#pragma warning disable HAA0301 // Closure Allocation Source
                (
                    st,
                    ct) =>
#pragma warning restore HAA0301 // Closure Allocation Source
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Func<CancellationToken, Task>>(
                        st,
                        nameof(st));

#pragma warning disable HAA0302 // Display class allocation to capture closure
                    var tcs = new TaskCompletionSource<int>();
#pragma warning restore HAA0302 // Display class allocation to capture closure
#pragma warning disable HAA0603 // Delegate allocation from a method group
                    ((Func<CancellationToken, Task>)st)(ct).ContinueWith(Continuation, TaskContinuationOptions.ExecuteSynchronously);
#pragma warning restore HAA0603 // Delegate allocation from a method group

                    void Continuation(Task completedTask)
                    {
                        if (completedTask.IsCanceled)
                        {
                            tcs.TrySetCanceled();
                        }
                        else if (completedTask.IsFaulted)
                        {
                            tcs.TrySetException(completedTask.Exception?.InnerExceptions ?? (IEnumerable<Exception>)new Exception[0]);
                        }
                        else
                        {
                            tcs.TrySetResult(0);
                        }
                    }

                    return tcs.Task;
                }, action,
                cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<Task<TResult>> action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Func<Task<TResult>>>(
                        st,
                        nameof(st));

                    ct.ThrowIfCancellationRequested();

                    return ((Func<Task<TResult>>)st)();
                }, action,
                cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<CancellationToken, Task<TResult>> action,
            CancellationToken cancellationToken = default) =>
            ExecuteOnThreadPool(
                (
                    st,
                    ct) =>
                {
                    Contract.RequiresNotNullPrivate(
                        st,
                        nameof(st));
                    Contract.RequiresArgumentOfTypePrivate<Func<CancellationToken, Task<TResult>>>(
                        st,
                        nameof(st));

                    return ((Func<CancellationToken, Task<TResult>>)st)(ct);
                }, action,
                cancellationToken);

        private static Task ExecuteOnThreadPool(
            Func<object, Task> action,
            object state,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
#pragma warning disable HAA0301 // Closure Allocation Source
            (
                st,
                ct) =>
#pragma warning restore HAA0301 // Closure Allocation Source
            {
                Contract.RequiresNotNullPrivate(
                    st,
                    nameof(st));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Func<object, Task>, object>>(
                    st,
                    nameof(st));

                var innerState = (Tuple<Func<object, Task>, object>)st;
#pragma warning disable HAA0302 // Display class allocation to capture closure
                var tcs = new TaskCompletionSource<int>();
#pragma warning restore HAA0302 // Display class allocation to capture closure
#pragma warning disable HAA0603 // Delegate allocation from a method group
                innerState.Item1(innerState.Item2).ContinueWith(Continuation, TaskContinuationOptions.ExecuteSynchronously);
#pragma warning restore HAA0603 // Delegate allocation from a method group

                void Continuation(Task completedTask)
                {
                    if (completedTask.IsCanceled)
                    {
                        tcs.TrySetCanceled();
                    }
                    else if (completedTask.IsFaulted)
                    {
                        tcs.TrySetException(completedTask.Exception?.InnerExceptions ?? (IEnumerable<Exception>)new Exception[0]);
                    }
                    else
                    {
                        tcs.TrySetResult(0);
                    }
                }

                return tcs.Task;
            }, new Tuple<Func<object, Task>, object>(
                action,
                state), cancellationToken);

        private static Task ExecuteOnThreadPool(
            Func<object, CancellationToken, Task> action,
            object state,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
#pragma warning disable HAA0301 // Closure Allocation Source
            (
                st,
                ct) =>
#pragma warning restore HAA0301 // Closure Allocation Source
            {
                Contract.RequiresNotNullPrivate(
                    st,
                    nameof(st));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Func<object, CancellationToken, Task>, object>>(
                    st,
                    nameof(st));

                var innerState = (Tuple<Func<object, CancellationToken, Task>, object>)st;
#pragma warning disable HAA0302 // Display class allocation to capture closure
                var tcs = new TaskCompletionSource<int>();
#pragma warning restore HAA0302 // Display class allocation to capture closure
#pragma warning disable HAA0603 // Delegate allocation from a method group
                innerState.Item1(
                    innerState.Item2,
                    ct).ContinueWith(Continuation, TaskContinuationOptions.ExecuteSynchronously);
#pragma warning restore HAA0603 // Delegate allocation from a method group

                void Continuation(Task completedTask)
                {
                    if (completedTask.IsCanceled)
                    {
                        tcs.TrySetCanceled();
                    }
                    else if (completedTask.IsFaulted)
                    {
                        tcs.TrySetException(completedTask.Exception?.InnerExceptions ?? (IEnumerable<Exception>)new Exception[0]);
                    }
                    else
                    {
                        tcs.TrySetResult(0);
                    }
                }

                return tcs.Task;
            }, new Tuple<Func<object, CancellationToken, Task>, object>(
                action,
                state), cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<object, Task<TResult>> action,
            object state,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
            (
                st,
                ct) =>
            {
                Contract.RequiresNotNullPrivate(
                    st,
                    nameof(st));
                Contract.RequiresArgumentOfTypePrivate<Tuple<Func<object, Task<TResult>>, object>>(
                    st,
                    nameof(st));

                ct.ThrowIfCancellationRequested();

                var innerState = (Tuple<Func<object, Task<TResult>>, object>)st;

                return innerState.Item1(innerState.Item2);
            }, new Tuple<Func<object, Task<TResult>>, object>(
                action,
                state), cancellationToken);

        private static Task<TResult> ExecuteOnThreadPool<TResult>(
            Func<object, CancellationToken, Task<TResult>> action,
            object state,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullPrivate(
                action,
                nameof(action));

            var taskCompletionSource = new TaskCompletionSource<TResult>(
                TaskCreationOptions.HideScheduler | TaskCreationOptions.PreferFairness);

            if (cancellationToken.IsCancellationRequested)
            {
                taskCompletionSource.TrySetCanceled();
                return taskCompletionSource.Task;
            }

#if NETSTANDARD1_2
            var outerState =
                new Tuple<Func<object, CancellationToken, Task<TResult>>, TaskCompletionSource<TResult>,
                    object, CancellationToken>(
#else
            var outerState =
                new Tuple<Func<object, CancellationToken, Task<TResult>>, CultureInfo, CultureInfo,
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
#elif NETSTANDARD1_3
            ThreadPool.QueueUserWorkItem(
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is expected, and also acceptable
                WorkItem,
#pragma warning restore HAA0603 // Delegate allocation from a method group
                outerState);
#else
            ThreadPool.UnsafeQueueUserWorkItem(
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
                Contract.RequiresArgumentOfTypePrivate<Tuple<Func<object, CancellationToken, Task<TResult>>, TaskCompletionSource<TResult>, object, CancellationToken>>(
                    rawState,
                    nameof(rawState));
#else
                Contract
                    .RequiresArgumentOfTypePrivate<
                        Tuple<Func<object, CancellationToken, Task<TResult>>, CultureInfo, CultureInfo,
                            TaskCompletionSource<TResult>, object, CancellationToken>>(
                        rawState,
                        nameof(rawState));
#endif

#if NETSTANDARD1_2
                var innerState =
                    (Tuple<Func<object, CancellationToken, Task<TResult>>, TaskCompletionSource<TResult>, object, CancellationToken>)rawState;

                var tcs = innerState.Item2;
                var payload = innerState.Item3;
                var ct = innerState.Item4;
#else
                var innerState =
                    (Tuple<Func<object, CancellationToken, Task<TResult>>, CultureInfo, CultureInfo,
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
#pragma warning disable HAA0603 // Delegate allocation from a method group
                    innerState.Item1(
                        payload,
                        ct).ContinueWith(Continuation, TaskContinuationOptions.ExecuteSynchronously);
#pragma warning restore HAA0603 // Delegate allocation from a method group

                    void Continuation(Task<TResult> completedTask)
                    {
                        if (completedTask.IsCanceled)
                        {
                            tcs.TrySetCanceled();
                        }
                        else if (completedTask.IsFaulted)
                        {
                            tcs.TrySetException(completedTask.Exception?.InnerExceptions ?? (IEnumerable<Exception>)new Exception[0]);
                        }
                        else
                        {
                            tcs.TrySetResult(completedTask.Result);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }

            return taskCompletionSource.Task;
        }
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
    }
}