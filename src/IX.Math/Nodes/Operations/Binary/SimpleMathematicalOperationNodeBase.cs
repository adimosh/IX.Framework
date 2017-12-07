// <copyright file="SimpleMathematicalOperationNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Binary
{
    internal abstract class SimpleMathematicalOperationNodeBase : BinaryOperationNodeBase
    {
        protected SimpleMathematicalOperationNodeBase(NodeBase left, NodeBase right)
            : base(left, right)
        {
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        protected override void EnsureCompatibleOperands(ref NodeBase left, ref NodeBase right)
        {
            if (left is UndefinedParameterNode uLeft)
            {
                left = uLeft.DetermineNumeric();
            }

            if (right is UndefinedParameterNode uRight)
            {
                right = uRight.DetermineNumeric();
            }

            if (left.ReturnType != SupportedValueType.Numeric && left.ReturnType != SupportedValueType.Unknown)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (right.ReturnType != SupportedValueType.Numeric && right.ReturnType != SupportedValueType.Unknown)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }
    }
}