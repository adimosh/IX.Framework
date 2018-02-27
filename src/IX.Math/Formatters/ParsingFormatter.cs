// <copyright file="ParsingFormatter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Globalization;
using System.Linq;
using IX.StandardExtensions.Globalization;

namespace IX.Math.Formatters
{
    internal static class ParsingFormatter
    {
        private const NumberStyles IntegerNumberStyle =
            NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowExponent;

        private const NumberStyles UnsignedIntegerNumberStyle =
            NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowExponent;

        private const NumberStyles FloatNumberStyle =
            NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint;

        private const NumberStyles HexNumberStyle = NumberStyles.AllowHexSpecifier;

        internal static bool ParseNumeric(
            in string expression,
            out object result)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (expression.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase) || expression.StartsWith("&h", StringComparison.CurrentCultureIgnoreCase))
            {
                if (expression.Length > 2)
                {
                    return ParseHexSpecific(expression.Substring(2), out result);
                }
                else
                {
                    result = null;
                    return false;
                }
            }
            else
            {
                return ParseSpecific(expression, out result);
            }

            bool ParseHexSpecific(
                in string hexExpression,
                out object hexResult)
            {
                if (long.TryParse(hexExpression, HexNumberStyle, CultureInfo.CurrentCulture, out long intVal))
                {
                    hexResult = intVal;
                    return true;
                }

                hexResult = null;
                return false;
            }

            bool ParseSpecific(
                in string specificExpression,
                out object specificResult)
            {
                IFormatProvider formatProvider = CultureInfo.CurrentCulture;

                if (long.TryParse(specificExpression, IntegerNumberStyle, formatProvider, out long intVal))
                {
                    specificResult = intVal;
                    return true;
                }
                else if (double.TryParse(specificExpression, FloatNumberStyle, formatProvider, out double doubleVal))
                {
                    specificResult = doubleVal;
                    return true;
                }

                specificResult = null;
                return false;
            }
        }

        internal static bool ParseByteArray(
            in string expression,
            out byte[] result)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (expression.CurrentCultureStartsWithInsensitive("0b"))
            {
                if (expression.Length > 2)
                {
                    return ParseByteArray(expression.Substring(2), out result);
                }
                else
                {
                    result = null;
                    return false;
                }
            }

            result = null;
            return false;

            bool ParseByteArray(
                in string byteArrayExpression,
                out byte[] byteArrayResult)
            {
                var stringLength = byteArrayExpression.Length;
                var byteLength = stringLength / 8;
                if (byteLength < (double)stringLength / 8)
                {
                    byteLength++;
                }

                byte[] bytes = new byte[byteLength];

                try
                {
                    for (var i = byteLength - 1; i >= 0; i -= 1)
                    {
                        var startingIndex = stringLength - ((byteLength - i) * 8);
                        int length;
                        if (startingIndex < 0)
                        {
                            length = 8 + startingIndex;
                            startingIndex = 0;
                        }
                        else
                        {
                            length = 8;
                        }

                        bytes[i] = Convert.ToByte(byteArrayExpression.Substring(startingIndex, length), 2);
                    }

                    byteArrayResult = bytes.Reverse().ToArray();

                    return true;
                }
                catch
                {
                    byteArrayResult = null;
                    return false;
                }
            }
        }
    }
}