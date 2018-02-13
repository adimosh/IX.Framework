// <copyright file="ComparisonOperationNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math.Nodes.Operations.Binary
{
    internal abstract class ComparisonOperationNodeBase : BinaryOperationNodeBase
    {
        protected ComparisonOperationNodeBase(NodeBase left, NodeBase right)
            : base(left, right)
        {
        }

        public override SupportedValueType ReturnType => SupportedValueType.Boolean;

        protected override void EnsureCompatibleOperands(ref NodeBase left, ref NodeBase right)
        {
            if (left is ParameterNode uLeft)
            {
                switch (right.ReturnType)
                {
                    case SupportedValueType.Boolean:
                        left = uLeft.DetermineBoolean();
                        break;
                    case SupportedValueType.ByteArray:
                        left = uLeft.DetermineByteArray();
                        break;
                    case SupportedValueType.Numeric:
                        left = uLeft.DetermineNumeric();
                        break;
                    case SupportedValueType.String:
                        left = uLeft.DetermineString();
                        break;
                    case SupportedValueType.Unknown:
                        break;
                    default:
                        throw new ExpressionNotValidLogicallyException();
                }
            }

            if (right is ParameterNode uRight)
            {
                switch (left.ReturnType)
                {
                    case SupportedValueType.Boolean:
                        right = uRight.DetermineBoolean();
                        break;
                    case SupportedValueType.ByteArray:
                        right = uRight.DetermineByteArray();
                        break;
                    case SupportedValueType.Numeric:
                        right = uRight.DetermineNumeric();
                        break;
                    case SupportedValueType.String:
                        right = uRight.DetermineString();
                        break;
                    case SupportedValueType.Unknown:
                        break;
                    default:
                        throw new ExpressionNotValidLogicallyException();
                }
            }

            if (left.ReturnType != right.ReturnType)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }
    }
}