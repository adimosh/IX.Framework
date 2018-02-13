// <copyright file="LogicalOperationNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math.Nodes.Operations.Binary
{
    internal abstract class LogicalOperationNodeBase : BinaryOperationNodeBase
    {
        protected LogicalOperationNodeBase(NodeBase left, NodeBase right)
            : base(left, right)
        {
        }

        public override SupportedValueType ReturnType => this.Left.ReturnType;

        protected override void EnsureCompatibleOperands(ref NodeBase left, ref NodeBase right)
        {
            if (left is ParameterNode uLeft && uLeft.ReturnType == SupportedValueType.Unknown)
            {
                if (right is ParameterNode uRightInternal && uRightInternal.ReturnType == SupportedValueType.Unknown)
                {
                    left = uLeft.DetermineInteger();
                    right = uRightInternal.DetermineInteger();
                }
                else
                {
                    switch (right.ReturnType)
                    {
                        case SupportedValueType.Numeric:
                            left = uLeft.DetermineNumeric().DetermineInteger();
                            break;

                        case SupportedValueType.Boolean:
                            left = uLeft.DetermineBoolean();
                            break;

                        case SupportedValueType.Unknown:
                            break;

                        default:
                            throw new ExpressionNotValidLogicallyException();
                    }
                }
            }

            if (right is ParameterNode uRight && uRight.ReturnType == SupportedValueType.Unknown)
            {
                switch (left.ReturnType)
                {
                    case SupportedValueType.Numeric:
                        right = uRight.DetermineNumeric().DetermineInteger();
                        break;

                    case SupportedValueType.Boolean:
                        right = uRight.DetermineBoolean();
                        break;

                    case SupportedValueType.Unknown:
                        break;

                    default:
                        throw new ExpressionNotValidLogicallyException();
                }
            }

            switch (left.ReturnType)
            {
                case SupportedValueType.Unknown:
                    if (right.ReturnType != SupportedValueType.Numeric && right.ReturnType != SupportedValueType.Boolean && right.ReturnType != SupportedValueType.Unknown)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.Numeric:
                    if (right.ReturnType != SupportedValueType.Numeric && right.ReturnType != SupportedValueType.Unknown)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.Boolean:
                    if (right.ReturnType != SupportedValueType.Boolean && right.ReturnType != SupportedValueType.Unknown)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                default:
                    throw new ExpressionNotValidLogicallyException();
            }

            switch (right.ReturnType)
            {
                case SupportedValueType.Unknown:
                    if (left.ReturnType != SupportedValueType.Numeric && left.ReturnType != SupportedValueType.Boolean && left.ReturnType != SupportedValueType.Unknown)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.Numeric:
                    if (left.ReturnType != SupportedValueType.Numeric && left.ReturnType != SupportedValueType.Unknown)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.Boolean:
                    if (left.ReturnType != SupportedValueType.Boolean && left.ReturnType != SupportedValueType.Unknown)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                default:
                    throw new ExpressionNotValidLogicallyException();
            }
        }
    }
}