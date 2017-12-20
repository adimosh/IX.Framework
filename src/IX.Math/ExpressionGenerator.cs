// <copyright file="ExpressionGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using System.Text.RegularExpressions;
using IX.Math.ExpressionState;
using IX.Math.Extraction;
using IX.Math.Formatters;
using IX.Math.Generators;
using IX.Math.Nodes;
using IX.Math.Nodes.Operations.Binary;
using IX.Math.Nodes.Operations.Unary;

namespace IX.Math
{
    internal static class ExpressionGenerator
    {
        internal static void CreateBody(
            WorkingExpressionSet workingSet)
        {
            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Strings
            workingSet.Expression = StringExtractor.ExtractStringConstants(
                workingSet.ConstantsTable,
                workingSet.ReverseConstantsTable,
                workingSet.Expression,
                workingSet.Definition.StringIndicator);

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            workingSet.Expression = SubExpressionFormatter.Cleanup(workingSet.Expression);

            workingSet.SymbolTable.Add(
                string.Empty,
                ExpressionSymbol.GenerateSymbol(string.Empty, workingSet.Expression));

            // Prepares expression and takes care of operators to ensure that they are all OK and usable
            workingSet.Initialize();

            workingSet.SymbolTable[string.Empty].Expression = workingSet.Expression;

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Break expression based on function calls
            FunctionsExtractor.ReplaceFunctions(
                workingSet.Definition.Parentheses.Item1,
                workingSet.Definition.Parentheses.Item2,
                workingSet.Definition.ParameterSeparator,
                workingSet.ConstantsTable,
                workingSet.ReverseConstantsTable,
                workingSet.SymbolTable,
                workingSet.ReverseSymbolTable,
                workingSet.ParametersTable,
                workingSet.Expression,
                workingSet.AllOperatorsInOrder,
                workingSet.AllSymbols);

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Break by parentheses
            ParenthesesExpressionGenerator.FormatParentheses(
                workingSet.Definition.Parentheses.Item1,
                workingSet.Definition.Parentheses.Item2,
                workingSet.Definition.ParameterSeparator,
                workingSet.AllOperatorsInOrder,
                workingSet.SymbolTable,
                workingSet.ReverseSymbolTable);

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Populating symbol tables
            foreach (var p in workingSet.SymbolTable.Where(p => !p.Value.IsFunctionCall).Select(p => p.Value.Expression))
            {
                TablePopulationGenerator.PopulateTables(
                    p,
                    workingSet.ConstantsTable,
                    workingSet.ReverseConstantsTable,
                    workingSet.SymbolTable,
                    workingSet.ReverseSymbolTable,
                    workingSet.ParametersTable,
                    workingSet.Expression,
                    workingSet.Definition.Parentheses.Item1,
                    workingSet.AllOperatorsInOrder);
            }

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Generate expressions
            try
            {
                workingSet.Body = GenerateExpression(workingSet.SymbolTable[string.Empty].Expression, workingSet);
            }
            catch
            {
                workingSet.Body = null;
            }

            if (workingSet.Body == null)
            {
                return;
            }

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Set success values and possibly constant values
            if (workingSet.Body is ConstantNodeBase)
            {
                if (workingSet.ParametersTable.Count > 0)
                {
                    // Cannot have external parameters if the expression is itself constant; something somewhere doesn't make sense
                    return;
                }
                else
                {
                    workingSet.ValueIfConstant = ((ConstantNodeBase)workingSet.Body).DistillValue();
                    workingSet.Constant = true;
                }
            }
            else if (workingSet.Body is ParameterNodeBase)
            {
                workingSet.PossibleString = true;
            }

            workingSet.InternallyValid = true;
            workingSet.Success = true;
        }

        private static NodeBase GenerateExpression(
            string expression,
            WorkingExpressionSet workingSet)
        {
            var s = expression;

            // Expression might be an already-defined constant
            if (workingSet.ConstantsTable.TryGetValue(s, out var c1))
            {
                return c1;
            }

            if (workingSet.ReverseConstantsTable.TryGetValue(s, out var c2))
            {
                if (workingSet.ConstantsTable.TryGetValue(c2, out var c3))
                {
                    return c3;
                }
            }

            // Check whether expression is an external parameter
            if (workingSet.ParametersTable.TryGetValue(s, out var parameterResult))
            {
                return parameterResult;
            }

            // Check whether the expression already exists in the symbols table
            if (workingSet.SymbolTable.TryGetValue(s, out var e1))
            {
                return GenerateExpression(e1.Expression, workingSet);
            }

            if (workingSet.ReverseSymbolTable.TryGetValue(s, out var e2))
            {
                if (workingSet.SymbolTable.TryGetValue(e2, out var e3))
                {
                    if (e3.Expression != s)
                    {
                        return GenerateExpression(e3.Expression, workingSet);
                    }
                }
            }

            // Check whether the expression is a function call
            if (s.Contains(workingSet.Definition.Parentheses.Item1) && s.Contains(workingSet.Definition.Parentheses.Item2))
            {
                return GenerateFunctionCallExpression(s);
            }

            // Check whether the expression is a binary operator
            foreach (var op in workingSet.BinaryOperatorsInOrder.Where(p => s.Contains(p)))
            {
                NodeBase exp = ExpressionByBinaryOperator();

                NodeBase ExpressionByBinaryOperator()
                {
                    workingSet.CancellationToken.ThrowIfCancellationRequested();

                    var index = s.Length;
                    while (true)
                    {
                        index = s.LastIndexOf(op, index - 1);

                        if (index < 1)
                        {
                            // We are having a unary operator - until this is treated differently, let's simply return null
                            // Or, at this point, no expression makes sense with this split
                            return null;
                        }
                        else
                        {
                            // We are having a binary operator
                            try
                            {
                                NodeBase left;
                                NodeBase right;

                                // We have a normal, regular binary
                                var eee = s.Substring(0, index);
                                if (string.IsNullOrWhiteSpace(eee))
                                {
                                    eee = null;
                                }

                                left = GenerateExpression(eee, workingSet);
                                if (left == null)
                                {
                                    continue;
                                }

                                eee = s.Substring(index + op.Length);
                                if (string.IsNullOrWhiteSpace(eee))
                                {
                                    eee = null;
                                }

                                right = GenerateExpression(eee, workingSet);
                                if (right == null)
                                {
                                    continue;
                                }

                                if (workingSet.BinaryOperators.TryGetValue(op, out Type t))
                                {
                                    return ((BinaryOperationNodeBase)Activator.CreateInstance(t, left, right))?.Simplify();
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }

                if (exp != null)
                {
                    return exp;
                }
            }

            // Check whether the expression is a unary operator
            foreach (var op in workingSet.UnaryOperatorsInOrder.Where(p => s.Contains(p)))
            {
                NodeBase exp = ExpressionByUnaryOperator();

                NodeBase ExpressionByUnaryOperator()
                {
                    workingSet.CancellationToken.ThrowIfCancellationRequested();

                    if (s.StartsWith(op))
                    {
                        try
                        {
                            var eee = s.Substring(op.Length);
                            NodeBase expr = GenerateExpression(string.IsNullOrWhiteSpace(eee) ? null : eee, workingSet);
                            if (expr == null)
                            {
                                return null;
                            }

                            if (workingSet.UnaryOperators.TryGetValue(op, out Type t))
                            {
                                return ((UnaryOperatorNodeBase)Activator.CreateInstance(t, expr))?.Simplify();
                            }
                            else
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            return null;
                        }
                    }

                    return null;
                }

                if (exp != null)
                {
                    return exp;
                }
            }

            return null;

            NodeBase GenerateFunctionCallExpression(string possibleFunctionCallExpression)
            {
                Match match = workingSet.FunctionRegex.Match(possibleFunctionCallExpression);

                try
                {
                    if (match.Success)
                    {
                        var functionName = match.Groups["functionName"].Value;
                        var expressionValue = match.Groups["expression"].Value;

                        string[] parameterExpressions;

                        if (string.IsNullOrWhiteSpace(expressionValue))
                        {
                            parameterExpressions = new string[0];
                        }
                        else
                        {
                            parameterExpressions = match.Groups["expression"].Value
                                .Split(new[] { workingSet.Definition.ParameterSeparator }, StringSplitOptions.None)
                                .Select(p => string.IsNullOrWhiteSpace(p) ? null : p)
                                .ToArray();
                        }

                        switch (parameterExpressions.Length)
                        {
                            case 0:
                                if (workingSet.NonaryFunctions.TryGetValue(functionName, out Type t))
                                {
                                    return ((NonaryFunctionNodeBase)Activator.CreateInstance(t))?.Simplify();
                                }

                                return null;
                            case 1:
                                if (workingSet.UnaryFunctions.TryGetValue(functionName, out Type t1))
                                {
                                    return ((UnaryFunctionNodeBase)Activator.CreateInstance(
                                        t1,
                                        GenerateExpression(parameterExpressions[0], workingSet)))?.Simplify();
                                }

                                return null;
                            case 2:
                                if (workingSet.BinaryFunctions.TryGetValue(functionName, out Type t2))
                                {
                                    return ((BinaryFunctionNodeBase)Activator.CreateInstance(
                                        t2,
                                        GenerateExpression(parameterExpressions[0], workingSet),
                                        GenerateExpression(parameterExpressions[1], workingSet)))?.Simplify();
                                }

                                return null;
                            case 3:
                                if (workingSet.TernaryFunctions.TryGetValue(functionName, out Type t3))
                                {
                                    return ((TernaryFunctionNodeBase)Activator.CreateInstance(
                                        t3,
                                        GenerateExpression(parameterExpressions[0], workingSet),
                                        GenerateExpression(parameterExpressions[1], workingSet),
                                        GenerateExpression(parameterExpressions[2], workingSet)))?.Simplify();
                                }

                                return null;
                            default:
                                return null;
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }

                return null;
            }
        }
    }
}