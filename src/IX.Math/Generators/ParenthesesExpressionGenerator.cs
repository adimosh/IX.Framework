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
            string openParenthesis,
            string closeParenthesis,
            string parameterSeparator,
            string[] allOperatorsInOrder,
            Dictionary<string, ExpressionSymbol> symbolTable,
            Dictionary<string, string> reverseSymbolTable)
        {
            FormatParenthesis(string.Empty);
            string[] names = symbolTable.Keys.Where(p => p.StartsWith("item")).ToArray();
            foreach (var name in names)
            {
                FormatParenthesis(name);
            }

            void FormatParenthesis(string key)
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
                    replaced = ReplaceParanthesis(replaced);
                }

                string ReplaceParanthesis(string source)
                {
                    if (string.IsNullOrWhiteSpace(source))
                    {
                        return string.Empty;
                    }

                    var openingParanthesisLocation = source.IndexOf(openParenthesis);
                    var closingParanthesisLocation = source.IndexOf(closeParenthesis);

                    beginning:
                    if (openingParanthesisLocation != -1)
                    {
                        if (closingParanthesisLocation != -1)
                        {
                            if (openingParanthesisLocation < closingParanthesisLocation)
                            {
                                var resultingSubExpression = ReplaceParanthesis(source.Substring(openingParanthesisLocation + openParenthesis.Length));

                                if (openingParanthesisLocation == 0)
                                {
                                    source = resultingSubExpression;
                                }
                                else
                                {
                                    var expr4 = openingParanthesisLocation == 0 ? string.Empty : source.Substring(0, openingParanthesisLocation);

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
                                            $"{expr6}{openParenthesis}item{symbolTable.Count - 1}{closeParenthesis}");

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

                                    source = $"{expr4}{resultingSubExpression}";
                                }

                                openingParanthesisLocation = source.IndexOf(openParenthesis);
                                closingParanthesisLocation = source.IndexOf(closeParenthesis);

                                goto beginning;
                            }

                            return ProcessSubExpression(closingParanthesisLocation);
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
                            return source;
                        }
                        else
                        {
                            return ProcessSubExpression(closingParanthesisLocation);
                        }
                    }

                    string ProcessSubExpression(
                        int cp)
                    {
                        var expr1 = source.Substring(0, cp);

                        string[] parameters = expr1.Split(new string[] { parameterSeparator }, StringSplitOptions.None);

                        var parSymbols = new List<string>();
                        foreach (var s in parameters)
                        {
                            var expr2 = SymbolExpressionGenerator.GenerateSymbolExpression(
                                symbolTable,
                                reverseSymbolTable,
                                s);
                            parSymbols.Add(expr2);
                        }

                        var k = cp + closeParenthesis.Length;
                        return $"{string.Join(parameterSeparator, parSymbols)}{(source.Length == k ? string.Empty : source.Substring(k))}";
                    }
                }
            }
        }
    }
}