// <copyright file="NotNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Unary
{
    [DebuggerDisplay("!{Operand}")]
    internal sealed class NotNode : UnaryOperatorNodeBase
    {
        public NotNode(NumericNode operand)
            : base(operand)
        {
        }

        public NotNode(BoolNode operand)
            : base(operand)
        {
        }

        public NotNode(NumericParameterNode operand)
            : base(operand?.ParameterMustBeInteger())
        {
        }

        public NotNode(BoolParameterNode operand)
            : base(operand)
        {
        }

        public NotNode(OperationNodeBase operand)
            : base(operand)
        {
            if (operand?.ReturnType != SupportedValueType.Numeric && operand?.ReturnType != SupportedValueType.Boolean)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public NotNode(UndefinedParameterNode operand)
            : base(operand?.IfDeterminedNumericAlsoDetermineInteger())
        {
        }

        private NotNode(NodeBase operand)
            : base(operand)
        {
        }

        public override SupportedValueType ReturnType => this.Operand?.ReturnType ?? SupportedValueType.Unknown;

        public override NodeBase Simplify()
        {
            switch (this.Operand)
            {
                case NumericNode numericNode:
                    return new NumericNode(~numericNode.ExtractInteger());
                case BoolNode boolNode:
                    return new BoolNode(!boolNode.Value);
                default:
                    return this;
            }
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context) => new NotNode(this.Operand.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => Expression.Not(this.Operand.GenerateExpression());
    }
}