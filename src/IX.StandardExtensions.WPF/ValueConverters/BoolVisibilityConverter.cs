// <copyright file="BoolVisibilityConverter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IX.StandardExtensions.WPF.ValueConverters
{
    /// <summary>
    /// A value converter between <see cref="bool"/> and <see cref="Visibility"/>.
    /// </summary>
    /// <seealso cref="IValueConverter" />
    public class BoolVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="IX.StandardExtensions.ArgumentInvalidTypeException"><paramref name="value"/> is not <see cref="bool"/> or Nullable&lt;<see cref="bool"/>&gt;.</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result;
            BoolVisibilityFilter filter;

            if (value is bool)
            {
                result = (bool)value;
            }
            else if (value is bool?)
            {
                result = (value as bool?) == true;
            }
            else
            {
                throw new ArgumentInvalidTypeException(nameof(value));
            }

            if (parameter is BoolVisibilityFilter)
            {
                filter = (BoolVisibilityFilter)parameter;
            }
            else
            {
                filter = BoolVisibilityFilter.Collapsed;
            }

            return result ? Visibility.Visible : ((filter == BoolVisibilityFilter.Hidden) ? Visibility.Hidden : Visibility.Collapsed);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="IX.StandardExtensions.ArgumentInvalidTypeException"><paramref name="value"/> is not <see cref="Visibility"/> or Nullable&lt;<see cref="Visibility"/>&gt;.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            BoolVisibilityFilter filter;

            if (value is Visibility)
            {
                result = (Visibility)value;
            }
            else if (value is Visibility?)
            {
                var tempResult = value as Visibility?;

                if (tempResult == null)
                {
                    return false;
                }
                else
                {
                    result = tempResult.Value;
                }
            }
            else
            {
                throw new ArgumentInvalidTypeException(nameof(value));
            }

            if (parameter is BoolVisibilityFilter)
            {
                filter = (BoolVisibilityFilter)parameter;
            }
            else
            {
                filter = BoolVisibilityFilter.Collapsed;
            }

            return (filter == BoolVisibilityFilter.Hidden) ? (result == Visibility.Visible || result == Visibility.Collapsed) : (result == Visibility.Visible);
        }
    }
}