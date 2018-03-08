// <copyright file="ConstantsGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.Math.Formatters;
using IX.Math.Nodes;
using IX.Math.Nodes.Constants;

namespace IX.Math.Generators
{
    /// <summary>
    /// A generator for constant values and their like.
    /// </summary>
    public static class ConstantsGenerator
    {
        /// <summary>
        /// Generates a string constant.
        /// </summary>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="originalExpression">The original expression.</param>
        /// <param name="stringIndicator">The string indicator.</param>
        /// <param name="content">The content.</param>
        /// <returns>The name of the new constant.</returns>
        public static string GenerateStringConstant(
                    in IDictionary<string, ConstantNodeBase> constantsTable,
                    in IDictionary<string, string> reverseConstantsTable,
                    in string originalExpression,
                    in string stringIndicator,
                    in string content)
        {
            if (string.IsNullOrWhiteSpace(originalExpression))
            {
                throw new ArgumentNullException(nameof(originalExpression));
            }

            if (constantsTable == null)
            {
                throw new ArgumentNullException(nameof(constantsTable));
            }

            if (reverseConstantsTable == null)
            {
                throw new ArgumentNullException(nameof(reverseConstantsTable));
            }

            if (string.IsNullOrWhiteSpace(stringIndicator))
            {
                throw new ArgumentNullException(nameof(stringIndicator));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (reverseConstantsTable.TryGetValue(content, out var key))
            {
                return key;
            }
            else
            {
                var name = GenerateName(constantsTable.Keys, originalExpression);
                constantsTable.Add(name, new StringNode(content.Substring(stringIndicator.Length, content.Length - stringIndicator.Length)));
                reverseConstantsTable.Add(content, name);
                return name;
            }
        }

        /// <summary>
        /// Checks the constant to see if there isn't one already, then tries to guess what type it is, finally adding it to the constants table if one suitable type is found.
        /// </summary>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="originalExpression">The original expression.</param>
        /// <param name="content">The content.</param>
        /// <returns>The name of the new constant, or <c>null</c> (<c>Nothing</c> in Visual Basic) if a suitable type is not found.</returns>
        public static string CheckAndAdd(
                    in IDictionary<string, ConstantNodeBase> constantsTable,
                    in IDictionary<string, string> reverseConstantsTable,
                    in string originalExpression,
                    in string content)
        {
            if (string.IsNullOrWhiteSpace(originalExpression))
            {
                throw new ArgumentNullException(nameof(originalExpression));
            }

            if (constantsTable == null)
            {
                throw new ArgumentNullException(nameof(constantsTable));
            }

            if (reverseConstantsTable == null)
            {
                throw new ArgumentNullException(nameof(reverseConstantsTable));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            if (reverseConstantsTable.TryGetValue(content, out var key))
            {
                return key;
            }
            else
            {
                if (ParsingFormatter.ParseNumeric(content, out var n))
                {
                    var name = GenerateName(constantsTable.Keys, originalExpression);
                    constantsTable.Add(name, new NumericNode(n));
                    reverseConstantsTable.Add(content, name);
                    return name;
                }
                else if (ParsingFormatter.ParseByteArray(content, out var ba))
                {
                    var name = GenerateName(constantsTable.Keys, originalExpression);
                    constantsTable.Add(name, new ByteArrayNode(ba));
                    reverseConstantsTable.Add(content, name);
                    return name;
                }
                else if (bool.TryParse(content, out var b))
                {
                    var name = GenerateName(constantsTable.Keys, originalExpression);
                    constantsTable.Add(name, new BoolNode(b));
                    reverseConstantsTable.Add(content, name);
                    return name;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Generates a named numeric symbol.
        /// </summary>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="alternateNames">The alternate names.</param>
        public static void GenerateNamedNumericSymbol(
                    in IDictionary<string, ConstantNodeBase> constantsTable,
                    in IDictionary<string, string> reverseConstantsTable,
                    in string name,
                    in double value,
                    params string[] alternateNames)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (constantsTable == null)
            {
                throw new ArgumentNullException(nameof(constantsTable));
            }

            if (reverseConstantsTable == null)
            {
                throw new ArgumentNullException(nameof(reverseConstantsTable));
            }

            if (reverseConstantsTable.TryGetValue(name, out var key))
            {
                return;
            }
            else
            {
                constantsTable.Add(name, new NumericNode(value));
                reverseConstantsTable.Add(value.ToString(), name);

                foreach (var alternateName in alternateNames)
                {
                    reverseConstantsTable.Add(alternateName, name);
                }
            }
        }

        private static string GenerateName(
            in IEnumerable<string> keys,
            in string originalExpression)
        {
            var index = int.Parse(keys.Where(p => p.StartsWith("Const") && p.Length > 5).LastOrDefault()?.Substring(5) ?? "0");

            do
            {
                index++;
            }
            while (originalExpression.Contains($"Const{index}"));

            return $"Const{index}";
        }
    }
}