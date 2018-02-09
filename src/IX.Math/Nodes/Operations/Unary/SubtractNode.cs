// <copyright file="SubtractNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Unary
{
    [DebuggerDisplay("-{Operand}")]
    internal sealed class SubtractNode : UnaryOperatorNodeBase
    {
        public SubtractNode(NumericNode operand)
            : base(operand)
        {
        }

        public SubtractNode(NumericParameterNode operand)
            : base(operand)
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

        public SubtractNode(UndefinedParameterNode operand)
            : base(operand?.DetermineNumeric())
        {
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
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone() => new SubtractNode(this.Operand.DeepClone());

        protected override Expression GenerateExpressionInternal() => Expression.Subtract(Expression.Constant(0, typeof(long)), this.Operand.GenerateExpression());
    }
}