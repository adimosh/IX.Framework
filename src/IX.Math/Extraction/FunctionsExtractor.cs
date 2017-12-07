// <copyright file="FunctionsExtractor.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.Math.ExpressionState;
using IX.Math.Generators;
using IX.Math.Nodes;

namespace IX.Math.Extraction
{
    /// <summary>
    /// A class to handle function extraction.
    /// </summary>
    internal static class FunctionsExtractor
    {
        /// <summary>
        /// Replaces functions calls with expression placeholders.
        /// </summary>
        /// <param name="openParenthesis">The symbol of an open parenthesis.</param>
        /// <param name="closeParenthesis">The symbol of a closed parenthesis.</param>
        /// <param name="parameterSeparator">The symbol of a parameter separator.</param>
        /// <param name="constantsTable">The constants table.</param>
        /// <param name="reverseConstantsTable">The reverse-lookup constants table.</param>
        /// <param name="symbolTable">The symbols table.</param>
        /// <param name="reverseSymbolTable">The reverse-lookup symbols table.</param>
        /// <param name="parametersTable">The parameters table.</param>
        /// <param name="expression">The expression before processing.</param>
        /// <param name="allOperatorsInOrder">All operators, in order.</param>
        /// <param name="allSymbols">All symbols.</param>
        internal static void ReplaceFunctions(
            string openParenthesis,
            string closeParenthesis,
            string parameterSeparator,
            Dictionary<string, ConstantNodeBase> constantsTable,
            Dictionary<string, string> reverseConstantsTable,
            Dictionary<string, ExpressionSymbol> symbolTable,
            Dictionary<string, string> reverseSymbolTable,
            Dictionary<string, ParameterNodeBase> parametersTable,
            string expression,
            string[] allOperatorsInOrder,
            string[] allSymbols)
        {
            ReplaceOneFunction(string.Empty);
            for (var i = 1; i < symbolTable.Count; i++)
            {
                ReplaceOneFunction($"item{i.ToString().PadLeft(4, '0')}");
            }

            void ReplaceOneFunction(string key)
            {
                ExpressionSymbol symbol = symbolTable[key];
                if (symbol.IsFunctionCall)
                {
                    return;
                }

                var replaced = symbol.Expression;
                while (replaced != null)
                {
                    symbolTable[key].Expression = replaced;
                    replaced = ReplaceFunctions(replaced);
                }

                string ReplaceFunctions(string source)
                {
                    var op = -1;
                    var opl = openParenthesis.Length;
                    var cpl = closeParenthesis.Length;

                    while (true)
                    {
                        op = source.IndexOf(openParenthesis, op + opl);

                        if (op == -1)
                        {
                            return null;
                        }

                        if (op == 0)
                        {
                            continue;
                        }

                        var functionHeaderCheck = source.Substring(0, op);

                        if (allSymbols.Any(p => functionHeaderCheck.EndsWith(p)))
                        {
                            continue;
                        }

                        var functionHeader = functionHeaderCheck.Split(allSymbols, StringSplitOptions.None).Last();

                        var oop = source.IndexOf(openParenthesis, op + opl);
                        var cp = source.IndexOf(closeParenthesis, op + cpl);

                        while (oop < cp && oop != -1 && cp != -1)
                        {
                            oop = source.IndexOf(openParenthesis, oop + opl);
                            cp = source.IndexOf(closeParenthesis, cp + cpl);
                        }

                        if (cp == -1)
                        {
                            continue;
                        }

                        var arguments = source.Substring(op + opl, cp - op - opl);
                        var originalArguments = arguments;

                        var q = arguments;
                        while (q != null)
                        {
                            arguments = q;
                            q = ReplaceFunctions(q);
                        }

                        var argPlaceholders = new List<string>();
                        foreach (var s in arguments.Split(new[] { parameterSeparator }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            TablePopulationGenerator.PopulateTables(
                                s,
                                constantsTable,
                                reverseConstantsTable,
                                symbolTable,
                                reverseSymbolTable,
                                parametersTable,
                                expression,
                                openParenthesis,
                                allOperatorsInOrder);

                            // We check whether or not this is actually a constant
                            var sa = ConstantsGenerator.CheckAndAdd(constantsTable, reverseConstantsTable, expression, s);
                            if (sa == null)
                            {
                                // We check whether or not this is actually an already-recognized external parameter
                                if (!parametersTable.ContainsKey(s))
                                {
                                    // Not a constant, and also not an already-recognized external parameter, let's generate a symbol
                                    sa = SymbolExpressionGenerator.GenerateSymbolExpression(symbolTable, reverseSymbolTable, s);
                                }
                                else
                                {
                                    sa = s;
                                }
                            }

                            argPlaceholders.Add(sa);
                        }

                        var functionCallBody = $"{functionHeader}{openParenthesis}{string.Join(parameterSeparator, argPlaceholders)}{closeParenthesis}";
                        var functionCallToReplace = $"{functionHeader}{openParenthesis}{originalArguments}{closeParenthesis}";
                        var functionCallItem = SymbolExpressionGenerator.GenerateSymbolExpression(symbolTable, reverseSymbolTable, functionCallBody, isFunction: true);

                        return source.Replace(
                            functionCallToReplace,
                            functionCallItem);
                    }
                }
            }
        }
    }
}