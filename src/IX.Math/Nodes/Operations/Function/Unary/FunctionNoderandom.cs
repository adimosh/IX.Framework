// <copyright file="FunctionNoderandom.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Generators;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("random({Parameter})")]
    [CallableMathematicsFunction("rand", "random")]
    internal sealed class FunctionNodeRandom : UnaryFunctionNodeBase
    {
        public FunctionNodeRandom(NumericNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeRandom(NumericParameterNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeRandom(UndefinedParameterNode parameter)
            : base(parameter?.DetermineNumeric())
        {
        }

        public FunctionNodeRandom(OperationNodeBase parameter)
            : base(parameter?.Simplify())
        {
            if (this.Parameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        public static double GenerateRandom(double max) => RandomNumberGenerator.Generate(max);

        public override NodeBase Simplify()
        {
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(GenerateRandom(stringParam.ExtractFloat()));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall<FunctionNodeRandom>(nameof(GenerateRandom));
    }
}