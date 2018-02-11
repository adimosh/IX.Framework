// <copyright file="GreaterThanNode.cs" company="Adrian Mos">
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
    [DebuggerDisplay("{Left} > {Right}")]
    internal sealed class GreaterThanNode : ComparisonOperationNodeBase
    {
        public GreaterThanNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Left is NumericNode nnLeft && this.Right is NumericNode nnRight)
            {
                return new BoolNode(Convert.ToDouble(nnLeft.Value) > Convert.ToDouble(nnRight.Value));
            }
            else if (this.Left is StringNode snLeft && this.Right is StringNode snRight)
            {
                return new BoolNode(snLeft.Value.CompareTo(snRight.Value) > 0);
            }
            else if (this.Left is BoolNode bnLeft && this.Right is BoolNode bnRight)
            {
                return new BoolNode(bnLeft.Value ? !bnRight.Value : false);
            }
            else if (this.Left is ByteArrayNode baLeft && this.Right is ByteArrayNode baRight)
            {
                return new BoolNode(baLeft.Value.SequenceCompareWithMsb(baRight.Value) > 0);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context) => new GreaterThanNode(this.Left.DeepClone(context), this.Right.DeepClone(context));

        protected override Expression GenerateExpressionInternal()
        {
            Tuple<Expression, Expression> pars = this.GetExpressionsOfSameTypeFromOperands();
            if (pars.Item1.Type == typeof(string))
            {
                MethodInfo mi = typeof(string).GetTypeMethod(nameof(string.Compare), typeof(string), typeof(string));
                return Expression.GreaterThan(
                    Expression.Call(mi, this.Left.GenerateStringExpression(), this.Right.GenerateStringExpression()),
                    Expression.Constant(0, typeof(int)));
            }
            else if (this.Left.ReturnType == SupportedValueType.Boolean || this.Right.ReturnType == SupportedValueType.Boolean)
            {
                return Expression.Condition(
                    Expression.Equal(pars.Item1, Expression.Constant(true, typeof(bool))),
                    Expression.Negate(pars.Item2),
                    Expression.Constant(false, typeof(bool)));
            }
            else if (this.Left.ReturnType == SupportedValueType.ByteArray || this.Right.ReturnType == SupportedValueType.ByteArray)
            {
                return Expression.GreaterThan(
                    Expression.Call(
                        typeof(ArraySequenceCompareWithMsbExtensions).GetTypeMethod(nameof(ArraySequenceCompareWithMsbExtensions.SequenceCompareWithMsb), typeof(byte[]), typeof(byte[])),
                        pars.Item1,
                        pars.Item2),
                    Expression.Constant(0, typeof(int)));
            }
            else
            {
                return Expression.GreaterThan(pars.Item1, pars.Item2);
            }
        }
    }
}