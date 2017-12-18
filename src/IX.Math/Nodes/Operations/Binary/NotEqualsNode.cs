﻿// <copyright file="NotEqualsNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;
using IX.Math.PlatformMitigation;
using IX.StandardExtensions;

namespace IX.Math.Nodes.Operations.Binary
{
    [DebuggerDisplay("{Left} != {Right}")]
    internal sealed class NotEqualsNode : ComparisonOperationNodeBase
    {
        public NotEqualsNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Left is NumericNode nnLeft && this.Right is NumericNode nnRight)
            {
                return new BoolNode(Convert.ToDouble(nnLeft.Value) != Convert.ToDouble(nnRight.Value));
            }
            else if (this.Left is StringNode snLeft && this.Right is StringNode snRight)
            {
                return new BoolNode(snLeft.Value != snRight.Value);
            }
            else if (this.Left is BoolNode bnLeft && this.Right is BoolNode bnRight)
            {
                return new BoolNode(bnLeft.Value != bnRight.Value);
            }
            else if (this.Left is ByteArrayNode baLeft && this.Right is ByteArrayNode baRight)
            {
                return new BoolNode(!baLeft.Value.SequenceEqualsWithMsb(baRight.Value));
            }
            else
            {
                return this;
            }
        }

        protected override Expression GenerateExpressionInternal()
        {
            Tuple<Expression, Expression> pars = this.GetExpressionsOfSameTypeFromOperands();

            if (this.Left.ReturnType == SupportedValueType.ByteArray || this.Right.ReturnType == SupportedValueType.ByteArray)
            {
                return Expression.Equal(
                    Expression.Call(
                        typeof(ArraySequenceEqualsWithMsbExtensions).GetTypeMethod(nameof(ArraySequenceEqualsWithMsbExtensions.SequenceEqualsWithMsb), typeof(byte[]), typeof(byte[])),
                        pars.Item1,
                        pars.Item2),
                    Expression.Constant(false, typeof(bool)));
            }
            else
            {
                return Expression.NotEqual(pars.Item1, pars.Item2);
            }
        }
    }
}