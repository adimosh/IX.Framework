// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using JetBrains.Annotations;

namespace IX.Abstractions.Logging
{
    /// <summary>
    /// The environment settings for logging.
    /// </summary>
    [PublicAPI]
    public class EnvironmentSettings
    {
        /// <summary>
        /// Gets or sets the default logging provider (if any).
        /// </summary>
        public ILog DefaultLoggingProvider { get; set; }
    }
}