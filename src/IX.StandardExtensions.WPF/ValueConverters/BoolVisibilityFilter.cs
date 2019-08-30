// <copyright file="BoolVisibilityFilter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Windows;

namespace IX.StandardExtensions.WPF.ValueConverters
{
    /// <summary>
    ///     Filter values for <see cref="bool" /> to <see cref="Visibility" /> converter.
    /// </summary>
    public enum BoolVisibilityFilter
    {
        /// <summary>
        ///     Filter Collapsed values and Hidden values.
        /// </summary>
        Collapsed = 0,

        /// <summary>
        ///     Filter only for Hidden values.
        /// </summary>
        Hidden = 1
    }
}