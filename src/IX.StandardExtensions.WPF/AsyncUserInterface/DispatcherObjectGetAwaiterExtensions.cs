// <copyright file="DispatcherObjectGetAwaiterExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Windows.Threading;

namespace IX.StandardExtensions.WPF.AsyncUserInterface
{
    /// <summary>
    /// Asynchronous support extensions for <see cref="DispatcherObject"/>.
    /// </summary>
    public static class DispatcherObjectGetAwaiterExtensions
    {
        /// <summary>
        /// Gets an awaiter for any <see cref="DispatcherObject"/> that invokes a continuation on the dispatcher thread.
        /// </summary>
        /// <param name="dispatcherObject">The dispatcher object.</param>
        public static void GetAwaiter(this DispatcherObject dispatcherObject) =>
            new DispatcherObjectAwaiter(dispatcherObject);
    }
}