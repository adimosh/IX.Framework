// <copyright file="ParenthesesExpressionGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.Math.ExpressionState;

namespace IX.Math.Generators
{
    internal static class ParenthesesExpressionGenerator
    {
        internal static void FormatParentheses(
            in string openParenthesis,
            in string closeParenthesis,
            string parameterSeparator,
            string[] allOperatorsInOrder,
            Dictionary<string, ExpressionSymbol> symbolTable,
            Dictionary<string, string> reverseSymbolTable)
        {
            FormatParenthesis(string.Empty, openParenthesis, closeParenthesis);
            string[] names = symbolTable.Keys.Where(p => p.StartsWith("item")).ToArray();
            foreach (var name in names)
            {
                FormatParenthesis(name, openParenthesis, closeParenthesis);
            }

            void FormatParenthesis(
                in string key,
                in string openParenthesisSymbol,
                in string closeParenthesisSymbol)
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
                    in string source,
                    in string innerOpenParenthesisSymbol,
                    in string innerCloseParenthesisSymbol)
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

                                    if (!allOperatorsInOrder.Any(p => expr4.EndsWith(p)))
                                    {
                                        // We have a function call
                                        var inx = allOperatorsInOrder.Max(p => expr4.LastIndexOf(p));
                                        var expr5 = inx == -1 ? expr4 : expr4.Substring(inx);
                                        var op1 = allOperatorsInOrder.OrderByDescending(p => p.Length).FirstOrDefault(p => expr5.StartsWith(p));
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
                        in int cp,
                        in string innermostCloseParenthesisSymbol,
                        in string innerSource)
                    {
                        var expr1 = innerSource.Substring(0, cp);

                        string[] parameters = expr1.Split(new string[] { parameterSeparator }, StringSplitOptions.None);

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