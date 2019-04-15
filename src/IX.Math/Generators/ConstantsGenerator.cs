// <copyright file="ConstantsGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IX.Math.Formatters;
using IX.Math.Nodes;
using IX.Math.Nodes.Constants;
using IX.StandardExtensions.Contracts;

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
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string originalExpression,
            string stringIndicator,
            string content)
        {
            Contract.RequiresNotNullOrWhitespace(originalExpression, nameof(originalExpression));
            Contract.RequiresNotNull(constantsTable, nameof(constantsTable));
            Contract.RequiresNotNull(reverseConstantsTable, nameof(reverseConstantsTable));
            Contract.RequiresNotNullOrWhitespace(stringIndicator, nameof(stringIndicator));
            Contract.RequiresNotNullOrWhitespace(content, nameof(content));

            if (reverseConstantsTable.TryGetValue(content, out var key))
            {
                return key;
            }

            var name = GenerateName(constantsTable.Keys, originalExpression);
            constantsTable.Add(name, new StringNode(content.Substring(stringIndicator.Length, content.Length - stringIndicator.Length)));
            reverseConstantsTable.Add(content, name);
            return name;
        }

        /// <summary>
        /// Generates a numeric constant out of a string.
        /// </summary>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="originalExpression">The original expression.</param>
        /// <param name="content">The content.</param>
        /// <returns>The name of the new constant.</returns>
        public static string GenerateNumericConstant(
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string originalExpression,
            string content)
        {
            Contract.RequiresNotNullOrWhitespace(originalExpression, nameof(originalExpression));
            Contract.RequiresNotNull(constantsTable, nameof(constantsTable));
            Contract.RequiresNotNull(reverseConstantsTable, nameof(reverseConstantsTable));
            Contract.RequiresNotNullOrWhitespace(content, nameof(content));

            if (reverseConstantsTable.TryGetValue(content, out var key))
            {
                return key;
            }

            if (!double.TryParse(content, out var result))
            {
                return null;
            }

            var name = GenerateName(constantsTable.Keys, originalExpression);
            constantsTable.Add(name, new NumericNode(result));
            reverseConstantsTable.Add(content, name);
            return name;
        }

        /// <summary>
        /// Checks the constant to see if there isn't one already, then tries to guess what type it is, finally adding it to the constants table if one suitable type is found.
        /// </summary>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="originalExpression">The original expression.</param>
        /// <param name="content">The content.</param>
        /// <returns>The name of the new constant, or <see langword="null"/> (<see langword="Nothing"/> in Visual Basic) if a suitable type is not found.</returns>
        public static string CheckAndAdd(
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string originalExpression,
            string content)
        {
            Contract.RequiresNotNullOrWhitespace(originalExpression, nameof(originalExpression));
            Contract.RequiresNotNull(constantsTable, nameof(constantsTable));
            Contract.RequiresNotNull(reverseConstantsTable, nameof(reverseConstantsTable));

            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            if (reverseConstantsTable.TryGetValue(content, out var key))
            {
                return key;
            }

            if (ParsingFormatter.ParseNumeric(content, out var n))
            {
                var name = GenerateName(constantsTable.Keys, originalExpression);
                constantsTable.Add(name, new NumericNode(n));
                reverseConstantsTable.Add(content, name);
                return name;
            }

            if (ParsingFormatter.ParseByteArray(content, out var ba))
            {
                var name = GenerateName(constantsTable.Keys, originalExpression);
                constantsTable.Add(name, new ByteArrayNode(ba));
                reverseConstantsTable.Add(content, name);
                return name;
            }

            if (bool.TryParse(content, out var b))
            {
                var name = GenerateName(constantsTable.Keys, originalExpression);
                constantsTable.Add(name, new BoolNode(b));
                reverseConstantsTable.Add(content, name);
                return name;
            }

            return null;
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
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string name,
            double value,
            params string[] alternateNames)
        {
            Contract.RequiresNotNullOrWhitespace(name, nameof(name));
            Contract.RequiresNotNull(constantsTable, nameof(constantsTable));
            Contract.RequiresNotNull(reverseConstantsTable, nameof(reverseConstantsTable));

            if (reverseConstantsTable.TryGetValue(
                name,
                out _))
            {
                return;
            }

            constantsTable.Add(
                name,
                new NumericNode(value));
            reverseConstantsTable.Add(
                value.ToString(CultureInfo.CurrentCulture),
                name);

            foreach (var alternateName in alternateNames)
            {
                reverseConstantsTable.Add(
                    alternateName,
                    name);
            }
        }

        private static string GenerateName(
            IEnumerable<string> keys,
            string originalExpression)
        {
            Contract.RequiresNotNullPrivate(
                keys,
                nameof(keys));

            var index = int.Parse(keys.Where(p => p.StartsWith("Const") && p.Length > 5).LastOrDefault()?.Substring(5) ?? "0");

            do
            {
                index++;
            }
            while (originalExpression.Contains($"Const{index.ToString()}"));

            return $"Const{index.ToString()}";
        }
    }
}