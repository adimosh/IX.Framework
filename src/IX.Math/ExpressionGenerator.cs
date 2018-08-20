// <copyright file="ExpressionGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using System.Reflection;
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
#if DEBUG
            if (workingSet == null)
            {
                throw new ArgumentNullException(nameof(workingSet));
            }
#endif

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // Extract constants
            foreach (Type extractorType in workingSet.Extractors.KeysByLevel.OrderBy(p => p.Key).SelectMany(p => p.Value))
            {
                workingSet.Expression = workingSet.Extractors[extractorType].ExtractAllConstants(
                    workingSet.Expression,
                    workingSet.ConstantsTable,
                    workingSet.ReverseConstantsTable,
                    workingSet.Definition);

                if (extractorType == typeof(StringExtractor))
                {
                    workingSet.Expression = SubExpressionFormatter.Cleanup(workingSet.Expression);
                }

                workingSet.CancellationToken.ThrowIfCancellationRequested();
            }

            // Start preparing expression
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
                workingSet.ParameterRegistry,
                workingSet.Expression,
                workingSet.AllOperatorsInOrder,
                workingSet.AllSymbols);

            workingSet.CancellationToken.ThrowIfCancellationRequested();

            // We save a split expression for determining parameter order
            var splitExpression = workingSet.Expression.Split(workingSet.AllSymbols.ToArray(), StringSplitOptions.RemoveEmptyEntries);

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
                    workingSet.ParameterRegistry,
                    workingSet.Expression,
                    workingSet.Definition.Parentheses.Item1,
                    workingSet.AllSymbols);
            }

            // For each parameter from the table we've just populated, see where it's first used, and fill in that index as the order
            foreach (Registration.ParameterContext paramForOrdering in workingSet.ParameterRegistry.Dump())
            {
                paramForOrdering.Order = Array.IndexOf(splitExpression, paramForOrdering.Name);
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
                if (workingSet.ParameterRegistry.Populated)
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
            else if (workingSet.Body is ParameterNode)
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

            if (workingSet.ReverseConstantsTable.TryGetValue(expression, out var c2))
            {
                if (workingSet.ConstantsTable.TryGetValue(c2, out ConstantNodeBase c3))
                {
                    return c3;
                }
            }

            // Check whether expression is an external parameter
            if (workingSet.ParameterRegistry.Exists(expression))
            {
                return new ParameterNode(expression, workingSet.ParameterRegistry);
            }

            // Check whether the expression already exists in the symbols table
            if (workingSet.SymbolTable.TryGetValue(expression, out ExpressionSymbol e1))
            {
                return GenerateExpression(e1.Expression, workingSet);
            }

            if (workingSet.ReverseSymbolTable.TryGetValue(expression, out var e2))
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
                    WorkingExpressionSet innerWorkingSet,
                    string s,
                    int position,
                    string op)
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

                    if (position == 0)
                    {
                        // We certainly have an unary operator if the operator is at the beginning of the expression. We therefore cannot continue with binary
                        return null;
                    }

                    innerWorkingSet.CancellationToken.ThrowIfCancellationRequested();

                    if (innerWorkingSet.BinaryOperators.TryGetValue(op, out Type t))
                    {
                        // We have a binary operator found
                        NodeBase left, right;

                        // We have a normal, regular binary
                        var eee = s.Substring(0, position);
                        if (string.IsNullOrWhiteSpace(eee))
                        {
                            // Empty space before operator. Normally, this should never be hit.
                            return null;
                        }

                        left = GenerateExpression(eee, innerWorkingSet);
                        if (left == null)
                        {
                            // Left expression is invalid.
                            return null;
                        }

                        eee = s.Substring(position + op.Length);
                        if (string.IsNullOrWhiteSpace(eee))
                        {
                            // Empty space after operator. Normally, this should never be hit.
                            return null;
                        }

                        right = GenerateExpression(eee, innerWorkingSet);
                        if (right == null)
                        {
                            // Right expression is invalid.
                            return null;
                        }

                        try
                        {
                            // TODO: Change Activator.CreateInstance to something offering higher performance and less TargetInvocationExceptions
                            return ((BinaryOperationNodeBase)Activator.CreateInstance(t, left, right))?.Simplify();
                        }
                        catch (MissingMemberException)
                        {
                            // The binary operator does not support the type of expression that would constitute the operand.
                            return null;
                        }
                        catch (TargetInvocationException)
                        {
                            // The constructor has thrown an exception when trying to construct the node. It is possible that the binary operator might not be a good fit.
                            return null;
                        }
                    }
                    else
                    {
                        // Binary operator not actually found.
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
                    WorkingExpressionSet innerWorkingSet,
                    string s,
                    string op)
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

                    if (s.StartsWith(op) && innerWorkingSet.UnaryOperators.TryGetValue(op, out Type t))
                    {
                        // We have a valid unary operator and the expression starts with it.
                        NodeBase expr;

                        var eee = s.Substring(op.Length);
                        expr = GenerateExpression(string.IsNullOrWhiteSpace(eee) ? null : eee, innerWorkingSet);
                        if (expr == null)
                        {
                            // The operand expression was not valid.
                            return null;
                        }

                        try
                        {
                            // TODO: Change Activator.CreateInstance to something offering higher performance and less TargetInvocationExceptions
                            return ((UnaryOperatorNodeBase)Activator.CreateInstance(t, expr))?.Simplify();
                        }
                        catch (MissingMemberException)
                        {
                            // The unary operator does not support the type of expression that would constitute the operand.
                            return null;
                        }
                        catch (TargetInvocationException)
                        {
                            // The constructor has thrown an exception when trying to construct the node. It is possible that the unary operator might not be a good fit.
                            return null;
                        }
                    }

                    // The unary operator is not valid.
                    return null;
                }

                if (exp != null)
                {
                    // We have found a valid unary operator expression.
                    return exp;
                }
            }

            return null;

            NodeBase GenerateFunctionCallExpression(string possibleFunctionCallExpression, WorkingExpressionSet innerWorkingSet)
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