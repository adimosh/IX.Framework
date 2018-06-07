// <copyright file="Fire.cs" company="Adrian Mos">
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

#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable
            runningTask.ContinueWith(
                StandardContinuation,
                exceptionHandler,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group

            runningTask.Start();
        }

        private static void StandardContinuation(Task task, object innerState) => (innerState as Action<Exception>)?.Invoke(task.Exception.GetBaseException());
    }
}