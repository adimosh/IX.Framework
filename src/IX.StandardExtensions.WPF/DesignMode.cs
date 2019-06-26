// <copyright file="DesignMode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.ComponentModel;
using System.Windows;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF
{
    /// <summary>
    ///     Helpers related to discovering whether or not we are in design mode.
    /// </summary>
    [PublicAPI]
    public static class DesignMode
    {
        private static Lazy<bool> isInDesignMode = new Lazy<bool>(
            () => (bool)DependencyPropertyDescriptor.FromProperty(
                DesignerProperties.IsInDesignModeProperty,
                typeof(FrameworkElement)).Metadata.DefaultValue);

        /// <summary>
        ///     Gets a value indicating whether the assembly was loaded in design mode or not.
        /// </summary>
        /// <value><see langword="true" /> if the assembly is in design mode; otherwise, <see langword="false" />.</value>
        public static bool IsInDesignMode => isInDesignMode.Value;
    }
}