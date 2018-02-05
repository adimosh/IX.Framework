// <copyright file="IConstantsExtractor.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using IX.Math.Nodes;

namespace IX.Math.Extraction
{
    /// <summary>
    /// A service contract for extractors of constant values from the expression.
    /// </summary>
    public interface IConstantsExtractor
    {
        /// <summary>
        /// Extracts all constants, replacing them from the original expression.
        /// </summary>
        /// <param name="originalExpression">The original expression.</param>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse constants table.</param>
        /// <param name="separators">The separators to use, if any.</param>
        /// <returns>The expression, after replacement.</returns>
        string ExtractAllConstants(
                string originalExpression,
                IDictionary<string, ConstantNodeBase> constantsTable,
                IDictionary<string, string> reverseConstantsTable,
                params string[] separators);
    }
}