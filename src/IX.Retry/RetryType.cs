// <copyright file="RetryType.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Retry
{
    /// <summary>
    ///     The type of retry procedure.
    /// </summary>
    [Flags]
    public enum RetryType
    {
        /// <summary>
        ///     No retry.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Retry a specific number of times.
        /// </summary>
        Times = 1,

        /// <summary>
        ///     Retry until a specific condition is met.
        /// </summary>
        Until = 2,

        /// <summary>
        ///     Retry for a specific period of time.
        /// </summary>
        For = 4,
    }
}