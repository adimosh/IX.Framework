// <copyright file="FunctionNodeNaturalLogarithm.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("ln({Parameter})")]
    [CallableMathematicsFunction("ln")]
    internal sealed class FunctionNodeNaturalLogarithm : UnaryFunctionNodeBase
    {
        public FunctionNodeNaturalLogarithm(NumericNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeNaturalLogarithm(NumericParameterNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeNaturalLogarithm(UndefinedParameterNode parameter)
            : base(parameter?.DetermineNumeric())
        {
        }

        public FunctionNodeNaturalLogarithm(OperationNodeBase parameter)
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
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(global::System.Math.Log(stringParam.ExtractFloat()));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Log));
    }
}