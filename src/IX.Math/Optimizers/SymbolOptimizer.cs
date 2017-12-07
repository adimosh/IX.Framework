// <copyright file="SymbolOptimizer.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IX.Math.ExpressionState;
using IX.Math.Nodes;

namespace IX.Math.Optimizers
{
    internal class SymbolOptimizer// : ISymbolOptimizer
    {
#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly
#pragma warning disable SA1009 // Closing parenthesis must be spaced correctly
        public List<NodeBase> OptimizeSymbols(
            Dictionary<string, ExpressionSymbol> symbols,
            Dictionary<string, string> reverseSymbols,
            Dictionary<string, ConstantNodeBase> constantsTable,
            Dictionary<string, string> reverseConstants,
            Func<ExpressionSymbol, NodeBase> expressionGenerator,
            CancellationToken cancellationToken = default)
        {
            var bodyExpressions = new List<NodeBase>();
            var variableExpressions = new List<ParameterNodeBase>();

            while (symbols.Count > 1)
            {
                KeyValuePair<string, ExpressionSymbol>[] countedSymbols
                    = CountSymbols(symbols).Where(q => q.Value.Contains == 0).ToArray();

                if (countedSymbols.Length == 0)
                {
                    break;
                }

                foreach (KeyValuePair<string, ExpressionSymbol> p in countedSymbols)
                {
                    NodeBase expression = expressionGenerator(p.Value)?.Simplify();

                    if (expression == null)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    if (expression is ConstantNodeBase constantExpression)
                    {
                        symbols.Remove(p.Key);
                        reverseSymbols.Remove(p.Value.Expression);

                        constantsTable.Add(p.Key, constantExpression);
                        reverseConstants.Add(p.Value.Expression, p.Key);
                    }
                    else
                    {
                        ParameterNodeBase resultingBodyExpression;
                        switch (expression.ReturnType)
                        {
                            case SupportedValueType.Boolean:
                                resultingBodyExpression = new Nodes.Parameters.BoolVariableNode(p.Key, expression);
                                break;
                            case SupportedValueType.Numeric:
                                resultingBodyExpression = new Nodes.Parameters.NumericVariableNode(p.Key, expression);
                                break;
                            case SupportedValueType.String:
                                resultingBodyExpression = new Nodes.Parameters.StringVariableNode(p.Key, expression);
                                break;
                            default:
                                throw new ExpressionNotValidLogicallyException();
                        }

                        symbols.Remove(p.Key);
                        reverseSymbols.Remove(p.Value.Expression);

                        bodyExpressions.Add(resultingBodyExpression);
                    }
                }
            }

            return bodyExpressions;

            Dictionary<string, ExpressionSymbol> CountSymbols(Dictionary<string, ExpressionSymbol> symb)
            {
                foreach (KeyValuePair<string, ExpressionSymbol> p in symb)
                {
                    foreach (KeyValuePair<string, ExpressionSymbol> q in symb.Where(z => z.Key != p.Key))
                    {
                        if (q.Value.Expression.Contains(p.Key))
                        {
                            p.Value.ContainedIn++;
                            q.Value.Contains++;
                        }
                    }
                }

                return symb;
            }
        }
#pragma warning restore SA1008 // Opening parenthesis must be spaced correctly
#pragma warning restore SA1009 // Closing parenthesis must be spaced correctly
    }
}