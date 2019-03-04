// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.Guaranteed.Collections
{
    /// <summary>
    /// Environment settings for guaranteed collections.
    /// </summary>
    [PublicAPI]
    public static class EnvironmentSettings
    {
        /// <summary>
        /// Gets or sets the persisted collections lock timeout.
        /// </summary>
        /// <value>The persisted collections lock timeout.</value>
        public static TimeSpan PersistedCollectionsLockTimeout { get; set; } = TimeSpan.FromSeconds(1);
    }
}