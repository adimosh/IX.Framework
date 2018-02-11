// <copyright file="SubtractNode.cs" company="Adrian Mos">
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

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context) => new SubtractNode(this.Left.DeepClone(context), this.Right.DeepClone(context));

        protected override Expression GenerateExpressionInternal() =>
            Expression.Subtract(Expression.Convert(this.Left.GenerateExpression(), typeof(double)), Expression.Convert(this.Right.GenerateExpression(), typeof(double)));
    }
}