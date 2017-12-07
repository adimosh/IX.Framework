﻿// <copyright file="FunctionNodeExponential.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("exp({Parameter})")]
    [CallableMathematicsFunction("exp", "exponential")]
    internal sealed class FunctionNodeExponential : UnaryFunctionNodeBase
    {
        public FunctionNodeExponential(NumericNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeExponential(NumericParameterNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeExponential(UndefinedParameterNode parameter)
            : base(parameter?.DetermineNumeric())
        {
        }

        public FunctionNodeExponential(OperationNodeBase parameter)
            : base(parameter?.Simplify())
        {
            if (this.Parameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        public override NodeBase Simplify()
        {
            NumericNode stringParam;
            if ((stringParam = this.Parameter as NumericNode) != null)
            {
                return new NumericNode(System.Math.Exp(stringParam.ExtractFloat()));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(System.Math), nameof(System.Math.Exp));
    }
}