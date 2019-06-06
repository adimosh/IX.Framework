// <copyright file="Fire.OnThreadPool.cs" company="Adrian Mos">
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
    public partial class Fire
    {
        /// <summary>
        /// Starts an action on a thread in the thread pool.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="cancellationToken">The optional cancellation token for the operation.</param>
        /// <returns>The task representing the current asynchronous operation.</returns>
        /// <remarks><para>On .NET Standard 1.2, due to the way the task scheduler works, it is not a guarantee that the method will run on a separate thread.</para></remarks>
        public static Task OnThreadPool(
            Action action,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
                action,
                cancellationToken);

        /// <summary>
        /// Starts an action on a thread in the thread pool.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="cancellationToken">The optional cancellation token for the operation.</param>
        /// <returns>The task representing the current asynchronous operation.</returns>
        /// <remarks><para>On .NET Standard 1.2, due to the way the task scheduler works, it is not a guarantee that the method will run on a separate thread.</para></remarks>
        public static Task OnThreadPool(
            Action<CancellationToken> action,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
                action,
                cancellationToken);

        /// <summary>
        /// Starts an action on a thread in the thread pool.
        /// </summary>
        /// <typeparam name="TResult">The type of result expected from this method.</typeparam>
        /// <param name="action">The action to execute.</param>
        /// <param name="cancellationToken">The optional cancellation token for the operation.</param>
        /// <returns>The task representing the current asynchronous operation.</returns>
        /// <remarks><para>On .NET Standard 1.2, due to the way the task scheduler works, it is not a guarantee that the method will run on a separate thread.</para></remarks>
        public static Task<TResult> OnThreadPool<TResult>(
            Func<TResult> action,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
                action,
                cancellationToken);

        /// <summary>
        /// Starts an action on a thread in the thread pool.
        /// </summary>
        /// <typeparam name="TResult">The type of result expected from this method.</typeparam>
        /// <param name="action">The action to execute.</param>
        /// <param name="cancellationToken">The optional cancellation token for the operation.</param>
        /// <returns>The task representing the current asynchronous operation.</returns>
        /// <remarks><para>On .NET Standard 1.2, due to the way the task scheduler works, it is not a guarantee that the method will run on a separate thread.</para></remarks>
        public static Task<TResult> OnThreadPool<TResult>(
            Func<CancellationToken, TResult> action,
            CancellationToken cancellationToken = default) => ExecuteOnThreadPool(
                action,
                cancellationToken);
    }
}