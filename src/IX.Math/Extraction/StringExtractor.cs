// <copyright file="StringExtractor.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using IX.Math.Generators;
using IX.Math.Nodes;

namespace IX.Math.Extraction
{
    /// <summary>
    /// A class to extract strings.
    /// </summary>
    internal static class StringExtractor
    {
        /// <summary>
        /// Extracts the string constants and replaces them with expression placeholders.
        /// </summary>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="originalExpression">The original expression.</param>
        /// <param name="stringIndicator">The string indicator.</param>
        /// <returns>The new expression.</returns>
        internal static string ExtractStringConstants(
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string originalExpression,
            string stringIndicator)
        {
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