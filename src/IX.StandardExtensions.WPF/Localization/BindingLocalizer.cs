// <copyright file="BindingLocalizer.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Globalization;
using IX.StandardExtensions.WPF.ValueConverters;

namespace IX.StandardExtensions.WPF.Localization
{
    /// <summary>
    /// A localizer converter.
    /// </summary>
    /// <seealso cref="ValueConverterBase" />
    public class BindingLocalizer : ValueConverterBase
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is used.</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string))
            {
                throw new ArgumentInvalidTypeException(nameof(parameter));
            }

            if (this.IsInDesignMode)
            {
                return parameter;
            }

            if (!(value is BindingAssemblyResourceReader))
            {
                throw new ArgumentInvalidTypeException(nameof(parameter));
            }

            var key = parameter as string;
            var reader = value as BindingAssemblyResourceReader;

            return reader.GetLocalizedResource(key);
        }
    }
}