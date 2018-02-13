// <copyright file="NumericUnaryFunctionNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math.Nodes.Operations.Function.Unary
{
    internal abstract class NumericUnaryFunctionNodeBase : UnaryFunctionNodeBase
    {
        public NumericUnaryFunctionNodeBase(NodeBase parameter)
            : base(parameter)
        {
        }

        public sealed override SupportedValueType ReturnType => SupportedValueType.Numeric;

        protected sealed override void EnsureCompatibleParameter(ref NodeBase parameter)
        {
            if (parameter is ParameterNode up)
            {
                parameter = up.DetermineNumeric();
            }

            if (parameter.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }
    }
}