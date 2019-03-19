// <copyright file="ParsingFormatter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Globalization;

namespace IX.Math.Formatters
{
    internal static class ParsingFormatter
    {
        private const NumberStyles IntegerNumberStyle = NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands |
                                                        NumberStyles.AllowExponent | NumberStyles.AllowExponent;

        private const NumberStyles FloatNumberStyle = NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands |
                                                      NumberStyles.AllowExponent | NumberStyles.AllowExponent |
                                                      NumberStyles.AllowDecimalPoint;

        private const NumberStyles HexNumberStyle = NumberStyles.AllowHexSpecifier;

        private static readonly Regex BitRepresentationRegex = new Regex("^[01]{8}$");

        internal static bool ParseNumeric(
            string expression,
            out object result)
        {
            Contract.RequiresNotNullPrivate(
                expression,
                nameof(expression));

            if (!expression.StartsWith(
                    "0x",
                    StringComparison.CurrentCultureIgnoreCase) && !expression.StartsWith(
                    "&h",
                    StringComparison.CurrentCultureIgnoreCase))
            {
                return ParseSpecific(
                    expression,
                    out result);
            }

            if (expression.Length > 2)
            {
                return ParseHexSpecific(
                    expression.Substring(2),
                    out result);
            }

            result = null;
            return false;

            bool ParseHexSpecific(
                string hexExpression,
                out object hexResult)
            {
                if (long.TryParse(
                    hexExpression,
                    HexNumberStyle,
                    CultureInfo.CurrentCulture,
                    out var intVal))
                {
                    hexResult = intVal;
                    return true;
                }

                hexResult = null;
                return false;
            }

            bool ParseSpecific(
                string specificExpression,
                out object specificResult)
            {
                Contract.RequiresNotNullPrivate(
                    specificExpression,
                    nameof(specificExpression));

                IFormatProvider formatProvider = CultureInfo.CurrentCulture;

                if (long.TryParse(
                    specificExpression,
                    IntegerNumberStyle,
                    formatProvider,
                    out var intVal))
                {
                    specificResult = intVal;
                    return true;
                }

                if (double.TryParse(
                    specificExpression,
                    FloatNumberStyle,
                    formatProvider,
                    out var doubleVal))
                {
                    specificResult = doubleVal;
                    return true;
                }

                specificResult = null;
                return false;
            }
        }

        internal static bool ParseByteArray(
            string expression,
            out byte[] result)
        {
            Contract.RequiresNotNullPrivate(
                expression,
                nameof(expression));

            if (expression.CurrentCultureStartsWithInsensitive("0b"))
            {
                if (expression.Length > 2)
                {
                    return ParseByteArray(
                        expression.Substring(2),
                        out result);
                }

                result = null;
                return false;
            }

            result = null;
            return false;

            bool ParseByteArray(
                string byteArrayExpression,
                out byte[] byteArrayResult)
            {
                Contract.RequiresNotNullPrivate(
                    byteArrayExpression,
                    nameof(byteArrayExpression));

                byteArrayExpression = byteArrayExpression.Replace(
                    "_",
                    string.Empty);
                var stringLength = byteArrayExpression.Length;
                var byteLength = stringLength / 8;
                if (byteLength < (double)stringLength / 8)
                {
                    byteLength++;
                }

                stringLength = byteLength * 8;
                if (byteArrayExpression.Length < stringLength)
                {
                    byteArrayExpression = byteArrayExpression.PadLeft(
                        stringLength,
                        '0');
                }

                var bytes = new byte[byteLength];

                for (var i = byteLength - 1; i >= 0; i -= 1)
                {
                    var startingIndex = stringLength - (byteLength - i) * 8;

                    var currentByteExpression = byteArrayExpression.Substring(
                        startingIndex,
                        8);

                    if (!BitRepresentationRegex.IsMatch(currentByteExpression))
                    {
                        byteArrayResult = null;
                        return false;
                    }

                    bytes[i] = Convert.ToByte(
                        currentByteExpression,
                        2);
                }

                Array.Reverse(bytes);
                byteArrayResult = bytes;

                return true;
            }
        }
    }
}