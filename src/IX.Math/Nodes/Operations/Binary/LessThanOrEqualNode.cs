// <copyright file="LessThanOrEqualNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using IX.Math.Nodes.Constants;
using IX.Math.PlatformMitigation;
using IX.StandardExtensions;

namespace IX.Math.Nodes.Operations.Binary
{
    [DebuggerDisplay("{Left} <= {Right}")]
    internal sealed class LessThanOrEqualNode : ComparisonOperationNodeBase
    {
        public LessThanOrEqualNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Left is NumericNode nnLeft && this.Right is NumericNode nnRight)
            {
                return new BoolNode(Convert.ToDouble(nnLeft.Value) <= Convert.ToDouble(nnRight.Value));
            }
            else if (this.Left is StringNode snLeft && this.Right is StringNode snRight)
            {
                return new BoolNode(snLeft.Value.CompareTo(snRight.Value) <= 0);
            }
            else if (this.Left is BoolNode bnLeft && this.Right is BoolNode bnRight)
            {
                return new BoolNode(bnLeft.Value ? bnRight.Value : true);
            }
            else if (this.Left is ByteArrayNode baLeft && this.Right is ByteArrayNode baRight)
            {
                return new BoolNode(baLeft.Value.SequenceCompareWithMsb(baRight.Value) <= 0);
            }
            else
            {
                return this;
            }
        }

        protected override Expression GenerateExpressionInternal()
        {
            Tuple<Expression, Expression> pars = this.GetExpressionsOfSameTypeFromOperands();
            if (pars.Item1.Type == typeof(string))
            {
                MethodInfo mi = typeof(string).GetTypeMethod(nameof(string.Compare), typeof(string), typeof(string));
                return Expression.LessThanOrEqual(
                    Expression.Call(mi, this.Left.GenerateStringExpression(), this.Right.GenerateStringExpression()),
                    Expression.Constant(0, typeof(int)));
            }
            else if (this.Left.ReturnType == SupportedValueType.Boolean || this.Right.ReturnType == SupportedValueType.Boolean)
            {
                return Expression.Condition(
                    Expression.Equal(pars.Item1, Expression.Constant(true, typeof(bool))),
                    pars.Item2,
                    Expression.Constant(true, typeof(bool)));
            }
            else if (this.Left.ReturnType == SupportedValueType.ByteArray || this.Right.ReturnType == SupportedValueType.ByteArray)
            {
                return Expression.LessThanOrEqual(
                    Expression.Call(
                        typeof(ArraySequenceCompareWithMsbExtensions).GetTypeMethod(nameof(ArraySequenceCompareWithMsbExtensions.SequenceCompareWithMsb), typeof(byte[]), typeof(byte[])),
                        pars.Item1,
                        pars.Item2),
                    Expression.Constant(0, typeof(int)));
            }
            else
            {
                return Expression.LessThanOrEqual(pars.Item1, pars.Item2);
            }
        }
    }
}