// <copyright file="ByteShiftOperationNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Binary
{
    internal abstract class ByteShiftOperationNodeBase : BinaryOperationNodeBase
    {
        protected ByteShiftOperationNodeBase(NodeBase left, NodeBase right)
            : base(left, right)
        {
        }

        public override SupportedValueType ReturnType => this.Left.ReturnType;

        protected override void EnsureCompatibleOperands(ref NodeBase left, ref NodeBase right)
        {
            if (left is UndefinedParameterNode uLeft)
            {
                left = uLeft.IfDeterminedNumericAlsoDetermineInteger();
            }

            switch (right)
            {
                case UndefinedParameterNode uRight:
                    right = uRight.DetermineNumeric().ParameterMustBeInteger();
                    break;

                case NumericNode cRight:
                    // This check is done to ensure that an int can actually be extracted from the right-hand constant
                    cRight.ExtractInt();
                    break;

                case NumericParameterNode pRight:
                    right = pRight.ParameterMustBeInteger();
                    break;

                case OperationNodeBase oRight:
                    if (oRight.ReturnType != SupportedValueType.Numeric && oRight.ReturnType != SupportedValueType.Unknown)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                default:
                    throw new ExpressionNotValidLogicallyException();
            }

            if (left.ReturnType != SupportedValueType.Numeric && left.ReturnType != SupportedValueType.ByteArray && left.ReturnType != SupportedValueType.Unknown)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }
    }
}