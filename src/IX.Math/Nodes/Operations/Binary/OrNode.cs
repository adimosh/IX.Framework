// <copyright file="OrNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Binary
{
    [DebuggerDisplay("{Left} | {Right}")]
    internal sealed class OrNode : LogicalOperationNodeBase
    {
        public OrNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Left is NumericNode nnLeft && this.Right is NumericNode nnRight)
            {
                return new NumericNode(nnLeft.ExtractInteger() | nnRight.ExtractInteger());
            }
            else if (this.Left is BoolNode bnLeft && this.Right is BoolNode bnRight)
            {
                return new BoolNode(bnLeft.Value | bnRight.Value);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone() => new OrNode(this.Left.DeepClone(), this.Right.DeepClone());

        protected override Expression GenerateExpressionInternal() => Expression.Or(this.Left.GenerateExpression(), this.Right.GenerateExpression());
    }
}