// <copyright file="NumericBinaryFunctionNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math.Nodes.Operations.Function.Binary
{
    internal abstract class NumericBinaryFunctionNodeBase : BinaryFunctionNodeBase
    {
        public NumericBinaryFunctionNodeBase(NodeBase firstParameter, NodeBase secondParameter)
            : base(firstParameter, secondParameter)
        {
        }

        public sealed override SupportedValueType ReturnType => SupportedValueType.Numeric;

        protected sealed override void EnsureCompatibleParameters(ref NodeBase firstParameter, ref NodeBase secondParameter)
        {
            if (firstParameter is ParameterNode up1)
            {
                firstParameter = up1.DetermineNumeric();
            }

            if (secondParameter is ParameterNode up2)
            {
                secondParameter = up2.DetermineNumeric();
            }

            if (firstParameter.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (secondParameter.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }
    }
}