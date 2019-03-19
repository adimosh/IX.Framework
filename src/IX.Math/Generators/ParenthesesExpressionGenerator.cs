// <copyright file="ParenthesesExpressionGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.Math.ExpressionState;
using IX.StandardExtensions;
using IX.StandardExtensions.Contracts;

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
            Contract.RequiresNotNullOrWhitespacePrivate(openParenthesis, nameof(openParenthesis));
            Contract.RequiresNotNullOrWhitespacePrivate(closeParenthesis, nameof(closeParenthesis));
            Contract.RequiresNotNullOrWhitespacePrivate(parameterSeparator, nameof(parameterSeparator));
            Contract.RequiresNotNullPrivate(allOperatorsInOrder, nameof(allOperatorsInOrder));
            Contract.RequiresNotNullPrivate(symbolTable, nameof(symbolTable));
            Contract.RequiresNotNullPrivate(reverseSymbolTable, nameof(reverseSymbolTable));

            FormatParenthesis(string.Empty, openParenthesis, closeParenthesis, parameterSeparator, allOperatorsInOrder, symbolTable, reverseSymbolTable);

            var itemsToProcess = new List<string>();

            var itemToProcess = symbolTable.Where((p, itemsToProcessL1) => p.Key.StartsWith("item") && !itemsToProcessL1.Contains(p.Key), itemsToProcess).Select(p => new { p.Key, p.Value }).FirstOrDefault();

            while (itemToProcess != default)
            {
                try
                {
                    if (itemToProcess.Value.IsFunctionCall)
                    {
                        continue;
                    }

                    FormatParenthesis(itemToProcess.Key, openParenthesis, closeParenthesis, parameterSeparator, allOperatorsInOrder, symbolTable, reverseSymbolTable);
                }
                finally
                {
                    itemsToProcess.Add(itemToProcess.Key);

                    itemToProcess = symbolTable.Where((p, itemsToProcessL1) => p.Key.StartsWith("item") && !itemsToProcessL1.Contains(p.Key), itemsToProcess).Select(p => new { p.Key, p.Value }).FirstOrDefault();
                }
            }

            void FormatParenthesis(
                string key,
                string openParenthesisL1,
                string closeParenthesisL1,
                string parameterSeparatorL1,
                string[] allOperatorsInOrderL1,
                Dictionary<string, ExpressionSymbol> symbolTableL1,
                Dictionary<string, string> reverseSymbolTableL1)
            {
                Contract.RequiresNotNullPrivate(key, nameof(key));
                Contract.RequiresNotNullOrWhitespacePrivate(openParenthesisL1, nameof(openParenthesisL1));
                Contract.RequiresNotNullOrWhitespacePrivate(closeParenthesisL1, nameof(closeParenthesisL1));
                Contract.RequiresNotNullOrWhitespacePrivate(parameterSeparatorL1, nameof(parameterSeparatorL1));
                Contract.RequiresNotNullPrivate(allOperatorsInOrderL1, nameof(allOperatorsInOrderL1));
                Contract.RequiresNotNullPrivate(symbolTableL1, nameof(symbolTableL1));
                Contract.RequiresNotNullPrivate(reverseSymbolTableL1, nameof(reverseSymbolTableL1));

                ExpressionSymbol symbol = symbolTableL1[key];
                if (symbol.IsFunctionCall)
                {
                    return;
                }

                var replacedPreviously = string.Empty;
                var replaced = symbol.Expression;
                while (replaced != replacedPreviously)
                {
                    symbolTableL1[key].Expression = replaced;
                    replacedPreviously = replaced;
                    replaced = ReplaceParanthesis(replaced, openParenthesisL1, closeParenthesisL1, parameterSeparatorL1, allOperatorsInOrderL1, symbolTableL1, reverseSymbolTableL1);
                }

                string ReplaceParanthesis(
                    string source,
                    string openParenthesisL2,
                    string closeParenthesisL2,
                    string parameterSeparatorSymbolL2,
                    string[] allOperatorsInOrderSymbolsL2,
                    Dictionary<string, ExpressionSymbol> symbolTableL2,
                    Dictionary<string, string> reverseSymbolTableL2)
                {
                    Contract.RequiresNotNullOrWhitespacePrivate(openParenthesisL2, nameof(openParenthesisL2));
                    Contract.RequiresNotNullOrWhitespacePrivate(closeParenthesisL2, nameof(closeParenthesisL2));
                    Contract.RequiresNotNullOrWhitespacePrivate(parameterSeparatorSymbolL2, nameof(parameterSeparatorSymbolL2));
                    Contract.RequiresNotNullPrivate(allOperatorsInOrderSymbolsL2, nameof(allOperatorsInOrderSymbolsL2));
                    Contract.RequiresNotNullPrivate(symbolTableL2, nameof(symbolTableL2));
                    Contract.RequiresNotNullPrivate(reverseSymbolTableL2, nameof(reverseSymbolTableL2));

                    if (string.IsNullOrWhiteSpace(source))
                    {
                        return string.Empty;
                    }

                    var src = source;

                    var openingParanthesisLocation = src.IndexOf(openParenthesisL2, StringComparison.Ordinal);
                    var closingParanthesisLocation = src.IndexOf(closeParenthesisL2, StringComparison.Ordinal);

                    beginning:
                    if (openingParanthesisLocation != -1)
                    {
                        if (closingParanthesisLocation != -1)
                        {
                            if (openingParanthesisLocation < closingParanthesisLocation)
                            {
                                var resultingSubExpression = ReplaceParanthesis(
                                    src.Substring(openingParanthesisLocation + openParenthesisL2.Length),
                                    openParenthesisL2,
                                    closeParenthesisL2,
                                    parameterSeparatorSymbolL2,
                                    allOperatorsInOrderSymbolsL2,
                                    symbolTableL2,
                                    reverseSymbolTableL2);

                                if (openingParanthesisLocation == 0)
                                {
                                    src = resultingSubExpression;
                                }
                                else
                                {
                                    var expr4 = openingParanthesisLocation == 0 ? string.Empty : src.Substring(0, openingParanthesisLocation);

                                    if (!allOperatorsInOrderSymbolsL2.Any((p, expr4L1) => expr4L1.EndsWith(p), expr4))
                                    {
                                        // We have a function call
#pragma warning disable HAA0603 // Delegate allocation from a method group - Acceptable here
                                        var inx = allOperatorsInOrderSymbolsL2.Max(expr4.LastIndexOf);
#pragma warning restore HAA0603 // Delegate allocation from a method group
                                        var expr5 = inx == -1 ? expr4 : expr4.Substring(inx);
                                        var op1 = allOperatorsInOrderSymbolsL2.OrderByDescending(p => p.Length).FirstOrDefault((p, expr5L1) => expr5L1.StartsWith(p), expr5);
                                        var expr6 = op1 == null ? expr5 : expr5.Substring(op1.Length);

                                        var _ = SymbolExpressionGenerator.GenerateSymbolExpression(
                                            symbolTableL2,
                                            reverseSymbolTableL2,
                                            $"{expr6}{openParenthesisL2}item{(symbolTableL2.Count - 1).ToString()}{closeParenthesisL2}",
                                            false);

                                        expr4 = expr6 == expr4 ? string.Empty : expr4.Substring(0, expr4.Length - expr6.Length);

                                        resultingSubExpression = resultingSubExpression.Replace($"item{(symbolTableL2.Count - 1).ToString()}", $"item{symbolTableL2.Count.ToString()}");
                                    }

                                    src = $"{expr4}{resultingSubExpression}";
                                }

                                openingParanthesisLocation = src.IndexOf(openParenthesisL2, StringComparison.Ordinal);
                                closingParanthesisLocation = src.IndexOf(closeParenthesisL2, StringComparison.Ordinal);

                                goto beginning;
                            }

                            return ProcessSubExpression(
                                closingParanthesisLocation,
                                closeParenthesisL2,
                                src,
                                parameterSeparatorSymbolL2,
                                symbolTableL2,
                                reverseSymbolTableL2);
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }

                    if (closingParanthesisLocation == -1)
                    {
                        return src;
                    }

                    return ProcessSubExpression(
                        closingParanthesisLocation,
                        closeParenthesisL2,
                        src,
                        parameterSeparatorSymbolL2,
                        symbolTableL2,
                        reverseSymbolTableL2);

                    string ProcessSubExpression(
                        int cp,
                        string closeParenthesisL3,
                        string sourceL3,
                        string parameterSeparatorL3,
                        Dictionary<string, ExpressionSymbol> symbolTableL3,
                        Dictionary<string, string> reverseSymbolTableL3)
                    {
                        var expr1 = sourceL3.Substring(0, cp);

                        var parameters = expr1.Split(new[] { parameterSeparatorL3 }, StringSplitOptions.None);

                        var parSymbols = new List<string>();
                        foreach (var s in parameters)
                        {
                            parSymbols.Add(SymbolExpressionGenerator.GenerateSymbolExpression(
                                symbolTableL3,
                                reverseSymbolTableL3,
                                s,
                                false));
                        }

                        var k = cp + closeParenthesisL3.Length;
                        return $"{string.Join(parameterSeparatorL3, parSymbols)}{(sourceL3.Length == k ? string.Empty : sourceL3.Substring(k))}";
                    }
                }
            }
        }
    }
}