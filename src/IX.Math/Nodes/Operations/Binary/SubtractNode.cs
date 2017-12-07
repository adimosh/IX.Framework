﻿// <copyright file="SubtractNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Binary
{
    [DebuggerDisplay("{Left} - {Right}")]
    internal sealed class SubtractNode : SimpleMathematicalOperationNodeBase
    {
        public SubtractNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Left is NumericNode nnLeft && this.Right is NumericNode nnRight)
            {
                return NumericNode.Subtract(nnLeft, nnRight);
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() =>
            Expression.Subtract(Expression.Convert(this.Left.GenerateExpression(), typeof(double)), Expression.Convert(this.Right.GenerateExpression(), typeof(double)));
    }
}