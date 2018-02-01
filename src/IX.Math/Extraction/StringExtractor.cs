// <copyright file="StringExtractor.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.Math.Generators;
using IX.Math.Nodes;

namespace IX.Math.Extraction
{
    /// <summary>
    /// A class to extract strings.
    /// </summary>
    public sealed class StringExtractor : IConstantsExtractor
    {
        /// <summary>
        /// Extracts the string constants and replaces them with expression placeholders.
        /// </summary>
        /// <param name="originalExpression">The original expression.</param>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="separators">The string indicator.</param>
        /// <returns>The expression, after replacement.</returns>
        public string ExtractAllConstants(string originalExpression, IDictionary<string, ConstantNodeBase> constantsTable, IDictionary<string, string> reverseConstantsTable, params string[] separators)
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

            if ((separators?.Length ?? 0) == 0)
            {
                throw new ArgumentNullException(nameof(separators));
            }

            var stringIndicator = separators[0];

            var process = originalExpression;

            while (true)
            {
                var op = process.IndexOf(stringIndicator);

                if (op == -1)
                {
                    break;
                }

                var cp = process.IndexOf(stringIndicator, op + stringIndicator.Length);

                escapeRoute:
                if (cp == -1 || (cp + stringIndicator.Length) > process.Length)
                {
                    break;
                }

                if (process.Substring(cp + stringIndicator.Length).StartsWith(stringIndicator))
                {
                    cp = process.IndexOf(stringIndicator, cp + (stringIndicator.Length * 2));
                    goto escapeRoute;
                }

                var itemName = ConstantsGenerator.GenerateStringConstant(
                    constantsTable,
                    reverseConstantsTable,
                    process,
                    stringIndicator,
                    process.Substring(op, cp - op));

                process = $"{process.Substring(0, op)}{itemName}{process.Substring(cp + stringIndicator.Length)}";
            }

            return process;
        }
    }
}