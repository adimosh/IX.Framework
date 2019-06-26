// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    ///     Environment settings for the standard extensions.
    /// </summary>
    [PublicAPI]
    public static class EnvironmentSettings
    {
        /// <summary>
        ///     Gets or sets the lock acquisition timeout.
        /// </summary>
        /// <value>The lock acquisition timeout.</value>
        /// <remarks>
        ///     <para>This timeout is generally applied to synchronization lockers, in absence of a specified value.</para>
        /// </remarks>
        public static TimeSpan LockAcquisitionTimeout { get; set; } =
            TimeSpan.FromMilliseconds(Constants.DefaultLockAcquisitionTimeout);
    }
}