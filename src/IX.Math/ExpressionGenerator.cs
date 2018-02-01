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
            in WorkingExpressionSet workingSet)
        {
#if DEBUG
            if (workingSet == null)
            {
                throw new ArgumentNullException(nameof(workingSet));
            }
#endif

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Strings
            workingSet.Expression = workingSet.Extractors[typeof(StringExtractor)].ExtractAllConstants(
                workingSet.Expression,
                workingSet.ConstantsTable,
                workingSet.ReverseConstantsTable,
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
            in string expression,
            in WorkingExpressionSet workingSet)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (workingSet == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
#endif

            // Expression might be an already-defined constant
            if (workingSet.ConstantsTable.TryGetValue(expression, out ConstantNodeBase c1))
            {
                return c1;
            }

            if (workingSet.ReverseConstantsTable.TryGetValue(expression, out string c2))
            {
                if (workingSet.ConstantsTable.TryGetValue(c2, out ConstantNodeBase c3))
                {
                    return c3;
                }
            }

            // Check whether expression is an external parameter
            if (workingSet.ParametersTable.TryGetValue(expression, out ParameterNodeBase parameterResult))
            {
                return parameterResult;
            }

            // Check whether the expression already exists in the symbols table
            if (workingSet.SymbolTable.TryGetValue(expression, out ExpressionSymbol e1))
            {
                return GenerateExpression(e1.Expression, workingSet);
            }

            if (workingSet.ReverseSymbolTable.TryGetValue(expression, out string e2))
            {
                if (workingSet.SymbolTable.TryGetValue(e2, out ExpressionSymbol e3))
                {
                    if (e3.Expression != expression)
                    {
                        return GenerateExpression(e3.Expression, workingSet);
                    }
                }
            }

            // Check whether the expression is a function call
            if (expression.Contains(workingSet.Definition.Parentheses.Item1) && expression.Contains(workingSet.Definition.Parentheses.Item2))
            {
                return GenerateFunctionCallExpression(expression, workingSet);
            }

            // Check whether the expression is a binary operator
            foreach (Tuple<int, int, string> operatorPosition in OperatorSequenceGenerator.GetOperatorsInOrderInExpression(expression, workingSet.BinaryOperators).OrderBy(p => p.Item1).ThenByDescending(p => p.Item2))
            {
                NodeBase exp = ExpressionByBinaryOperator(workingSet, expression, operatorPosition.Item2, operatorPosition.Item3);

                NodeBase ExpressionByBinaryOperator(
                    in WorkingExpressionSet innerWorkingSet,
                    in string s,
                    in int position,
                    in string op)
                {
#if DEBUG
                    if (innerWorkingSet == null)
                    {
                        throw new ArgumentNullException(nameof(workingSet));
                    }

                    if (string.IsNullOrWhiteSpace(s))
                    {
                        throw new ArgumentNullException(nameof(s));
                    }
#endif

                    innerWorkingSet.CancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        NodeBase left;
                        NodeBase right;

                        // We have a normal, regular binary
                        var eee = s.Substring(0, position);
                        if (string.IsNullOrWhiteSpace(eee))
                        {
                            eee = null;
                        }

                        left = GenerateExpression(eee, innerWorkingSet);
                        if (left == null)
                        {
                            return null;
                        }

                        eee = s.Substring(position + op.Length);
                        if (string.IsNullOrWhiteSpace(eee))
                        {
                            eee = null;
                        }

                        right = GenerateExpression(eee, innerWorkingSet);
                        if (right == null)
                        {
                            return null;
                        }

                        if (innerWorkingSet.BinaryOperators.TryGetValue(op, out Type t))
                        {
                            return ((BinaryOperationNodeBase)Activator.CreateInstance(t, left, right))?.Simplify();
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

                if (exp != null)
                {
                    // We have found a valid binary operator expression
                    return exp;
                }
            }

            // Check whether the expression is a unary operator
            foreach (Tuple<int, int, string> operatorPosition in OperatorSequenceGenerator.GetOperatorsInOrderInExpression(expression, workingSet.UnaryOperators).OrderBy(p => p.Item1).ThenByDescending(p => p.Item2))
            {
                NodeBase exp = ExpressionByUnaryOperator(workingSet, expression, operatorPosition.Item3);

                NodeBase ExpressionByUnaryOperator(
                    in WorkingExpressionSet innerWorkingSet,
                    in string s,
                    in string op)
                {
#if DEBUG
                    if (innerWorkingSet == null)
                    {
                        throw new ArgumentNullException(nameof(workingSet));
                    }

                    if (string.IsNullOrWhiteSpace(s))
                    {
                        throw new ArgumentNullException(nameof(s));
                    }
#endif

                    innerWorkingSet.CancellationToken.ThrowIfCancellationRequested();

                    if (s.StartsWith(op))
                    {
                        try
                        {
                            var eee = s.Substring(op.Length);
                            NodeBase expr = GenerateExpression(string.IsNullOrWhiteSpace(eee) ? null : eee, innerWorkingSet);
                            if (expr == null)
                            {
                                return null;
                            }

                            if (innerWorkingSet.UnaryOperators.TryGetValue(op, out Type t))
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
                    // We have found a valid unary operator expression
                    return exp;
                }
            }

            return null;

            NodeBase GenerateFunctionCallExpression(in string possibleFunctionCallExpression, in WorkingExpressionSet innerWorkingSet)
            {
#if DEBUG
                if (innerWorkingSet == null)
                {
                    throw new ArgumentNullException(nameof(workingSet));
                }

                if (string.IsNullOrWhiteSpace(possibleFunctionCallExpression))
                {
                    throw new ArgumentNullException(nameof(possibleFunctionCallExpression));
                }
#endif

                Match match = innerWorkingSet.FunctionRegex.Match(possibleFunctionCallExpression);

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
                                .Split(new[] { innerWorkingSet.Definition.ParameterSeparator }, StringSplitOptions.None)
                                .Select(p => string.IsNullOrWhiteSpace(p) ? null : p)
                                .ToArray();
                        }

                        switch (parameterExpressions.Length)
                        {
                            case 0:
                                if (innerWorkingSet.NonaryFunctions.TryGetValue(functionName, out Type t))
                                {
                                    return ((NonaryFunctionNodeBase)Activator.CreateInstance(t))?.Simplify();
                                }

                                return null;
                            case 1:
                                if (innerWorkingSet.UnaryFunctions.TryGetValue(functionName, out Type t1))
                                {
                                    return ((UnaryFunctionNodeBase)Activator.CreateInstance(
                                        t1,
                                        GenerateExpression(parameterExpressions[0], innerWorkingSet)))?.Simplify();
                                }

                                return null;
                            case 2:
                                if (innerWorkingSet.BinaryFunctions.TryGetValue(functionName, out Type t2))
                                {
                                    return ((BinaryFunctionNodeBase)Activator.CreateInstance(
                                        t2,
                                        GenerateExpression(parameterExpressions[0], innerWorkingSet),
                                        GenerateExpression(parameterExpressions[1], innerWorkingSet)))?.Simplify();
                                }

                                return null;
                            case 3:
                                if (innerWorkingSet.TernaryFunctions.TryGetValue(functionName, out Type t3))
                                {
                                    return ((TernaryFunctionNodeBase)Activator.CreateInstance(
                                        t3,
                                        GenerateExpression(parameterExpressions[0], innerWorkingSet),
                                        GenerateExpression(parameterExpressions[1], innerWorkingSet),
                                        GenerateExpression(parameterExpressions[2], innerWorkingSet)))?.Simplify();
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