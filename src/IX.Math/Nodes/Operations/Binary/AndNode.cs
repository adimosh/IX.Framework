// <copyright file="AndNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Binary
{
    [DebuggerDisplay("{Left} & {Right}")]
    internal sealed class AndNode : LogicalOperationNodeBase
    {
        public AndNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Left is NumericNode nnLeft && this.Right is NumericNode nnRight)
            {
                return new NumericNode(nnLeft.ExtractInteger() & nnRight.ExtractInteger());
            }
            else if (this.Left is BoolNode bnLeft && this.Right is BoolNode bnRight)
            {
                return new BoolNode(bnLeft.Value & bnRight.Value);
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
        public override NodeBase DeepClone(NodeCloningContext context) => new AndNode(this.Left.DeepClone(context), this.Right.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => Expression.And(this.Left.GenerateExpression(), this.Right.GenerateExpression());
    }
}