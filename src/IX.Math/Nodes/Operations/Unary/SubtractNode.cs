// <copyright file="SubtractNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Unary
{
    [DebuggerDisplay("-{Operand}")]
    internal sealed class SubtractNode : UnaryOperatorNodeBase
    {
        public SubtractNode(NumericNode operand)
            : base(operand)
        {
        }

        public SubtractNode(ParameterNode operand)
            : base(operand?.DetermineNumeric())
        {
        }

        public SubtractNode(OperationNodeBase operand)
            : base(operand?.Simplify())
        {
            if (operand?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        private SubtractNode(NodeBase operand)
            : base(operand)
        {
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        public override NodeBase Simplify()
        {
            switch (this.Operand)
            {
                case NumericNode numericNode:
                    return NumericNode.Subtract(new NumericNode(0), numericNode);
                default:
                    return this;
            }
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context) => new SubtractNode(this.Operand.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => Expression.Subtract(Expression.Constant(0, typeof(long)), this.Operand.GenerateExpression());
    }
}