// <copyright file="TablePopulationGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.Math.ExpressionState;
using IX.Math.Nodes;
using IX.Math.Registration;

namespace IX.Math.Generators
{
    /// <summary>
    ///     A class to handle table population.
    /// </summary>
    internal static class TablePopulationGenerator
    {
        /// <summary>
        ///     Populates tables according to the currently-processed expression.
        /// </summary>
        /// <param name="processedExpression">The expression that is being processed.</param>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse-lookup constants table.</param>
        /// <param name="symbolTable">The symbols table.</param>
        /// <param name="reverseSymbolTable">The reverse-lookup symbols table.</param>
        /// <param name="parameterRegistry">The parameters registry.</param>
        /// <param name="expression">The expression before processing.</param>
        /// <param name="openParenthesis">The symbol of an open parenthesis.</param>
        /// <param name="allSymbols">All symbols on which to split, in order.</param>
        internal static void PopulateTables(
            string processedExpression,
            Dictionary<string, ConstantNodeBase> constantsTable,
            Dictionary<string, string> reverseConstantsTable,
            Dictionary<string, ExpressionSymbol> symbolTable,
            Dictionary<string, string> reverseSymbolTable,
            IParameterRegistry parameterRegistry,
            string expression,
            string openParenthesis,
            string[] allSymbols)
        {
            string[] expressions = processedExpression.Split(
                allSymbols,
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var exp in expressions)
            {
                if (constantsTable.ContainsKey(exp))
                {
                    continue;
                }

                if (reverseConstantsTable.ContainsKey(exp))
                {
                    continue;
                }

                if (parameterRegistry.Exists(exp))
                {
                    continue;
                }

                if (symbolTable.ContainsKey(exp))
                {
                    continue;
                }

                if (reverseSymbolTable.ContainsKey(exp))
                {
                    continue;
                }

                if (exp.Contains(openParenthesis))
                {
                    continue;
                }

                if (ConstantsGenerator.CheckAndAdd(
                        constantsTable,
                        reverseConstantsTable,
                        expression,
                        exp) != null)
                {
                    continue;
                }

                parameterRegistry.AdvertiseParameter(exp);
            }
        }
    }
}