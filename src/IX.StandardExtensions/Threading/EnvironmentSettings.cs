// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// Environment settings for the standard extensions.
    /// </summary>
    public static class EnvironmentSettings
    {
        /// <summary>
        /// Gets or sets a default unhandled exception handler for fire-and-forget scenarios.
        /// </summary>
        /// <value>The default unhandled exception handler.</value>
        public static Action<Exception> DefaultFireAndForgetUnhandledExceptionHandler { get; set; }
    }
}