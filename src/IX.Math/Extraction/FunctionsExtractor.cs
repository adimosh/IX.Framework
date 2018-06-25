// <copyright file="FunctionsExtractor.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.Math.ExpressionState;
using IX.Math.Generators;
using IX.Math.Nodes;
using IX.Math.Registration;

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
            IParameterRegistry parametersTable,
            string expression,
            string[] allOperatorsInOrder,
            string[] allSymbols)
        {
            ReplaceOneFunction(
                string.Empty,
                openParenthesis,
                closeParenthesis,
                parameterSeparator,
                constantsTable,
                reverseConstantsTable,
                symbolTable,
                reverseSymbolTable,
                parametersTable,
                expression,
                allOperatorsInOrder,
                allSymbols);
            for (var i = 1; i < symbolTable.Count; i++)
            {
                ReplaceOneFunction(
                    $"item{i.ToString().PadLeft(4, '0')}",
                    openParenthesis,
                    closeParenthesis,
                    parameterSeparator,
                    constantsTable,
                    reverseConstantsTable,
                    symbolTable,
                    reverseSymbolTable,
                    parametersTable,
                    expression,
                    allOperatorsInOrder,
                    allSymbols);
            }

            void ReplaceOneFunction(
                string key,
                string outerOpenParanthesisSymbol,
                string outerCloseParanthesisSymbol,
                string outerParameterSeparatorSymbol,
                Dictionary<string, ConstantNodeBase> outerConstantsTableReference,
                Dictionary<string, string> outerReverseConstantsTableReference,
                Dictionary<string, ExpressionSymbol> outerSymbolTableReference,
                Dictionary<string, string> outerReverseSymbolTableRefeerence,
                IParameterRegistry outerParametersTableReference,
                string outerExpressionSymbol,
                string[] outerAllOperatorsInOrderSymbols,
                string[] outerAllSymbolsSymbols)
            {
                ExpressionSymbol symbol = outerSymbolTableReference[key];
                if (symbol.IsFunctionCall)
                {
                    return;
                }

                var replaced = symbol.Expression;
                while (replaced != null)
                {
                    outerSymbolTableReference[key].Expression = replaced;
                    replaced = ReplaceFunctions(
                        replaced,
                        outerOpenParanthesisSymbol,
                        outerCloseParanthesisSymbol,
                        outerParameterSeparatorSymbol,
                        outerConstantsTableReference,
                        outerReverseConstantsTableReference,
                        outerSymbolTableReference,
                        outerReverseSymbolTableRefeerence,
                        outerParametersTableReference,
                        outerExpressionSymbol,
                        outerAllOperatorsInOrderSymbols,
                        outerAllSymbolsSymbols);
                }

                string ReplaceFunctions(
                    string source,
                    string openParanthesisSymbol,
                    string closeParanthesisSymbol,
                    string parameterSeparatorSymbol,
                    Dictionary<string, ConstantNodeBase> constantsTableReference,
                    Dictionary<string, string> reverseConstantsTableReference,
                    Dictionary<string, ExpressionSymbol> symbolTableReference,
                    Dictionary<string, string> reverseSymbolTableReference,
                    IParameterRegistry parametersTableReference,
                    string expressionSymbol,
                    string[] allOperatorsInOrderSymbols,
                    string[] allSymbolsSymbols)
                {
                    var op = -1;
                    var opl = openParanthesisSymbol.Length;
                    var cpl = closeParanthesisSymbol.Length;

                    while (true)
                    {
                        op = source.IndexOf(openParanthesisSymbol, op + opl);

                        if (op == -1)
                        {
                            return null;
                        }

                        if (op == 0)
                        {
                            continue;
                        }

                        var functionHeaderCheck = source.Substring(0, op);

                        if (allSymbolsSymbols.Any(p => functionHeaderCheck.EndsWith(p)))
                        {
                            continue;
                        }

                        var functionHeader = functionHeaderCheck.Split(allSymbolsSymbols, StringSplitOptions.None).Last();

                        bool FunctionHeaderCheck(string src, int ops, string allsmb, out fncHeader)
                        {
                            var fhc = src.Substring(0, ops);

                            foreach (var p in allsmb)
                            {
                                if (fhc.EndsWith(p))
                            }
                            if (allSymbolsSymbols.Any(p => functionHeaderCheck.EndsWith(p)))
                            {
                                continue;
                            }

                            var functionHeader = functionHeaderCheck.Split(allSymbolsSymbols, StringSplitOptions.None).Last();

                        }

                        var oop = source.IndexOf(openParanthesisSymbol, op + opl);
                        var cp = source.IndexOf(closeParanthesisSymbol, op + cpl);

                        while (oop < cp && oop != -1 && cp != -1)
                        {
                            oop = source.IndexOf(openParanthesisSymbol, oop + opl);
                            cp = source.IndexOf(closeParanthesisSymbol, cp + cpl);
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
                            q = ReplaceFunctions(
                                q,
                                openParanthesisSymbol,
                                closeParanthesisSymbol,
                                parameterSeparatorSymbol,
                                constantsTableReference,
                                reverseConstantsTableReference,
                                symbolTableReference,
                                reverseSymbolTableReference,
                                parametersTableReference,
                                expressionSymbol,
                                allOperatorsInOrderSymbols,
                                allSymbolsSymbols);
                        }

                        var argPlaceholders = new List<string>();
                        foreach (var s in arguments.Split(new[] { parameterSeparatorSymbol }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            TablePopulationGenerator.PopulateTables(
                                s,
                                constantsTableReference,
                                reverseConstantsTableReference,
                                symbolTableReference,
                                reverseSymbolTableReference,
                                parametersTableReference,
                                expressionSymbol,
                                openParanthesisSymbol,
                                allOperatorsInOrderSymbols);

                            // We check whether or not this is actually a constant
                            var sa = ConstantsGenerator.CheckAndAdd(constantsTableReference, reverseConstantsTableReference, expressionSymbol, s);
                            if (sa == null)
                            {
                                // We check whether or not this is actually an already-recognized external parameter
                                if (!parametersTableReference.Exists(s))
                                {
                                    // Not a constant, and also not an already-recognized external parameter, let's generate a symbol
                                    sa = SymbolExpressionGenerator.GenerateSymbolExpression(symbolTableReference, reverseSymbolTableReference, s, isFunction: false);
                                }
                                else
                                {
                                    sa = s;
                                }
                            }

                            argPlaceholders.Add(sa);
                        }

                        var functionCallBody = $"{functionHeader}{openParanthesisSymbol}{string.Join(parameterSeparatorSymbol, argPlaceholders)}{closeParanthesisSymbol}";
                        var functionCallToReplace = $"{functionHeader}{openParanthesisSymbol}{originalArguments}{closeParanthesisSymbol}";
                        var functionCallItem = SymbolExpressionGenerator.GenerateSymbolExpression(symbolTableReference, reverseSymbolTableReference, functionCallBody, isFunction: true);

                        return source.Replace(
                            functionCallToReplace,
                            functionCallItem);
                    }
                }
            }
        }
    }
}