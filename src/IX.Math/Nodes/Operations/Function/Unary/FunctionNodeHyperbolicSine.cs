// <copyright file="FunctionNodeHyperbolicSine.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("sinh({Parameter})")]
    [CallableMathematicsFunction("sinh")]
    internal sealed class FunctionNodeHyperbolicSine : UnaryFunctionNodeBase
    {
        public FunctionNodeHyperbolicSine(NumericNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeHyperbolicSine(NumericParameterNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeHyperbolicSine(UndefinedParameterNode parameter)
            : base(parameter?.DetermineNumeric())
        {
        }

        public FunctionNodeHyperbolicSine(OperationNodeBase parameter)
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
                return new NumericNode(global::System.Math.Sinh(stringParam.ExtractFloat()));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Sinh));
    }
}