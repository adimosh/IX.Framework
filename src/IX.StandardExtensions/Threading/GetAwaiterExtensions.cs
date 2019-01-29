// <copyright file="GetAwaiterExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// Asynchronous support extension methods for various objects that would be useful to <see langword="await" /> on.
    /// </summary>
    public static class GetAwaiterExtensions
    {
        /// <summary>
        /// Gets a delay awaiter.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>A <see cref="TaskAwaiter"/> that can be awaited on.</returns>
        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan) => Task.Delay(timeSpan).GetAwaiter();

        /// <summary>
        /// Gets a delay awaiter.
        /// </summary>
        /// <param name="duration">The duration, in milliseconds.</param>
        /// <returns>A <see cref="TaskAwaiter"/> that can be awaited on.</returns>
        public static TaskAwaiter GetAwaiter(this int duration) => Task.Delay(duration).GetAwaiter();

        /// <summary>
        /// Gets a delay awaiter.
        /// </summary>
        /// <param name="dateTimeOffset">The offset.</param>
        /// <returns>A <see cref="TaskAwaiter"/> that can be awaited on.</returns>
        public static TaskAwaiter GetAwaiter(this DateTimeOffset dateTimeOffset) => (dateTimeOffset - DateTimeOffset.UtcNow).GetAwaiter();
    }
}