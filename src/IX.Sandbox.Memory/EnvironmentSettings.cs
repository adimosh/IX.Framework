// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// Environment settings for memory sandbox.
    /// </summary>
    public static class EnvironmentSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to treat special arrays (such as byte or char arrays) as multiple elements.
        /// </summary>
        /// <value><see langword="true"> to treat special arrays as multiple elements; otherwise, <see langword="false" />.</value>
        public static bool TreatSpecialArraysAsMultipleElements { get; set; }

        /// <summary>
        /// Gets or sets the enumerable separator symbol.
        /// </summary>
        /// <value>The enumerable separator symbol.</value>
        public static string EnumerableSeparatorSymbol { get; set; } = ";";
    }
}