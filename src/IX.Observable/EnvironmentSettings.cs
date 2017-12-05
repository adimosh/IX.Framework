// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System.Threading;

namespace IX.Observable
{
    /// <summary>
    /// Environment settings for observable collections.
    /// </summary>
    public static class EnvironmentSettings
    {
        /// <summary>
        /// Gets or sets the specific synchronization context.
        /// </summary>
        /// <value>The specific synchronization context.</value>
        public static SynchronizationContext SpecificSynchronizationContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to always suppress the default synchronization context.
        /// </summary>
        /// <value><c>true</c> if observable collections should always suppress the default synchronization context; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// <para>This setting will not suppress any explicit synchronization contexts set on observable collections.</para>
        /// <para>If this setting is set to <c>true</c>, then <see cref="SpecificSynchronizationContext"/> is ignored.</para>
        /// </remarks>
        public static bool AlwaysSuppressDefaultSynchronizationContext { get; set; }
    }
}