// <copyright file="ValueConverterBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.ValueConverters
{
    /// <summary>
    ///     A base class for value converters.
    /// </summary>
    /// <seealso cref="IValueConverter" />
    [PublicAPI]
    public abstract class ValueConverterBase : IValueConverter
    {
        /// <summary>
        ///     Gets a value indicating whether this value converter is in design mode.
        /// </summary>
        /// <value><see langword="true" /> if this value converter is in design mode; otherwise, <see langword="false" />.</value>
        [Browsable(false)]
        public bool IsInDesignMode => DesignMode.IsInDesignMode;

        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is
        ///     used.
        /// </returns>
        /// <exception cref="IX.StandardExtensions.ArgumentInvalidTypeException">
        ///     <paramref name="value" /> is not
        ///     <see cref="bool" /> or Nullable&lt;<see cref="bool" />&gt;.
        /// </exception>
        public virtual object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture) => throw new NotImplementedByDesignException();

        /// <summary>
        ///     Converts a value back.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is
        ///     used.
        /// </returns>
        public virtual object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture) => throw new NotImplementedByDesignException();
    }
}