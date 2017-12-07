// <copyright file="WaitType.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Retry
{
    /// <summary>
    /// The type of wait to use.
    /// </summary>
    public enum WaitType
    {
        /// <summary>
        /// No wait.
        /// </summary>
        None = 0,

        /// <summary>
        /// Wait for a specific period.
        /// </summary>
        For = 1,

        /// <summary>
        /// Wait until something happens.
        /// </summary>
        Until = 2,
    }
}