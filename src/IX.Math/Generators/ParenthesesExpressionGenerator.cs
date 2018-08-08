// <copyright file="ParenthesesExpressionGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.Math.ExpressionState;
using IX.StandardExtensions;

namespace IX.Math.Generators
{
    internal static class ParenthesesExpressionGenerator
    {
        internal static void FormatParentheses(
            string openParenthesis,
            string closeParenthesis,
            string parameterSeparator,
            string[] allOperatorsInOrder,
            Dictionary<string, ExpressionSymbol> symbolTable,
            Dictionary<string, string> reverseSymbolTable)
        {
            FormatParenthesis(string.Empty, openParenthesis, closeParenthesis);

#pragma warning disable HeapAnalyzerClosureCaptureRule // Display class allocation to capture closure
            var itemsToProcess = new List<string>();
#pragma warning restore HeapAnalyzerClosureCaptureRule // Display class allocation to capture closure

#pragma warning disable HeapAnalyzerClosureSourceRule // Closure Allocation Source
#pragma warning disable HeapAnalyzerExplicitNewAnonymousObjectRule // Explicit new anonymous object allocation
            var itemToProcess = symbolTable.Where(p => p.Key.StartsWith("item") && !itemsToProcess.Contains(p.Key)).Select(p => new { p.Key, p.Value }).FirstOrDefault();
#pragma warning restore HeapAnalyzerExplicitNewAnonymousObjectRule // Explicit new anonymous object allocation
#pragma warning restore HeapAnalyzerClosureSourceRule // Closure Allocation Source

            while (itemToProcess != default)
            {
                try
                {
                    if (itemToProcess.Value.IsFunctionCall)
                    {
                        continue;
                    }

                    FormatParenthesis(itemToProcess.Key, openParenthesis, closeParenthesis);
                }
                finally
                {
                    itemsToProcess.Add(itemToProcess.Key);

#pragma warning disable HeapAnalyzerClosureSourceRule // Closure Allocation Source
#pragma warning disable HeapAnalyzerExplicitNewAnonymousObjectRule // Explicit new anonymous object allocation
                    itemToProcess = symbolTable.Where(p => p.Key.StartsWith("item") && !itemsToProcess.Contains(p.Key)).Select(p => new { p.Key, p.Value }).FirstOrDefault();
#pragma warning restore HeapAnalyzerExplicitNewAnonymousObjectRule // Explicit new anonymous object allocation
#pragma warning restore HeapAnalyzerClosureSourceRule // Closure Allocation Source
                }
            }

            void FormatParenthesis(
                string key,
                string openParenthesisSymbol,
                string closeParenthesisSymbol)
            {
                ExpressionSymbol symbol = symbolTable[key];
                if (symbol.IsFunctionCall)
                {
                    return;
                }

                var replacedPreviously = string.Empty;
                var replaced = symbol.Expression;
                while (replaced != replacedPreviously)
                {
                    symbolTable[key].Expression = replaced;
                    replacedPreviously = replaced;
                    replaced = ReplaceParanthesis(replaced, openParenthesisSymbol, closeParenthesisSymbol);
                }

                string ReplaceParanthesis(
                    string source,
                    string innerOpenParenthesisSymbol,
                    string innerCloseParenthesisSymbol)
                {
                    var src = source;

                    if (string.IsNullOrWhiteSpace(source))
                    {
                        return string.Empty;
                    }

                    var openingParanthesisLocation = src.IndexOf(innerOpenParenthesisSymbol);
                    var closingParanthesisLocation = src.IndexOf(innerCloseParenthesisSymbol);

                    beginning:
                    if (openingParanthesisLocation != -1)
                    {
                        if (closingParanthesisLocation != -1)
                        {
                            if (openingParanthesisLocation < closingParanthesisLocation)
                            {
                                var resultingSubExpression = ReplaceParanthesis(src.Substring(openingParanthesisLocation + innerOpenParenthesisSymbol.Length), innerOpenParenthesisSymbol, innerCloseParenthesisSymbol);

                                if (openingParanthesisLocation == 0)
                                {
                                    src = resultingSubExpression;
                                }
                                else
                                {
                                    var expr4 = openingParanthesisLocation == 0 ? string.Empty : src.Substring(0, openingParanthesisLocation);

                                    if (!allOperatorsInOrder.Any((p, expr4L1) => expr4L1.EndsWith(p), expr4))
                                    {
                                        // We have a function call
                                        var inx = allOperatorsInOrder.Max(p => expr4.LastIndexOf(p));
                                        var expr5 = inx == -1 ? expr4 : expr4.Substring(inx);
                                        var op1 = allOperatorsInOrder.OrderByDescending(p => p.Length).FirstOrDefault((p, expr5L1) => expr5L1.StartsWith(p), expr5);
                                        var expr6 = op1 == null ? expr5 : expr5.Substring(op1.Length);

                                        var expr2 = SymbolExpressionGenerator.GenerateSymbolExpression(
                                            symbolTable,
                                            reverseSymbolTable,
                                            $"{expr6}{innerOpenParenthesisSymbol}item{symbolTable.Count - 1}{innerCloseParenthesisSymbol}",
                                            false);

                                        if (expr6 == expr4)
                                        {
                                            expr4 = string.Empty;
                                        }
                                        else
                                        {
                                            expr4 = expr4.Substring(0, expr4.Length - expr6.Length);
                                        }

                                        resultingSubExpression = resultingSubExpression.Replace($"item{symbolTable.Count - 1}", $"item{symbolTable.Count}");
                                    }

                                    src = $"{expr4}{resultingSubExpression}";
                                }

                                openingParanthesisLocation = src.IndexOf(innerOpenParenthesisSymbol);
                                closingParanthesisLocation = src.IndexOf(innerCloseParenthesisSymbol);

                                goto beginning;
                            }

                            return ProcessSubExpression(
                                closingParanthesisLocation,
                                innerCloseParenthesisSymbol,
                                src);
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    else
                    {
                        if (closingParanthesisLocation == -1)
                        {
                            return src;
                        }
                        else
                        {
                            return ProcessSubExpression(
                                closingParanthesisLocation,
                                innerCloseParenthesisSymbol,
                                src);
                        }
                    }

                    string ProcessSubExpression(
                        int cp,
                        string innermostCloseParenthesisSymbol,
                        string innerSource)
                    {
                        var expr1 = innerSource.Substring(0, cp);

                        var parameters = expr1.Split(new string[] { parameterSeparator }, StringSplitOptions.None);

                        var parSymbols = new List<string>();
                        foreach (var s in parameters)
                        {
                            var expr2 = SymbolExpressionGenerator.GenerateSymbolExpression(
                                symbolTable,
                                reverseSymbolTable,
                                s,
                                false);
                            parSymbols.Add(expr2);
                        }

                        var k = cp + innermostCloseParenthesisSymbol.Length;
                        return $"{string.Join(parameterSeparator, parSymbols)}{(innerSource.Length == k ? string.Empty : innerSource.Substring(k))}";
                    }
                }
            }
        }
    }
}