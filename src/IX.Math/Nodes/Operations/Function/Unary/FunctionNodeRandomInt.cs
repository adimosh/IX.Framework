// <copyright file="FunctionNodeRandomInt.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Generators;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("randomint({Parameter})")]
    [CallableMathematicsFunction("randomint")]
    internal sealed class FunctionNodeRandomInt : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeRandomInt(NodeBase parameter)
            : base(parameter)
        {
            if (parameter is ParameterNode firstParameter)
            {
                firstParameter.DetermineInteger();
            }
        }

        public static long GenerateRandom(long max) => RandomNumberGenerator.GenerateInt(max);

        public override NodeBase Simplify() => this;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeRandomInt(this.Parameter.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall<FunctionNodeRandomInt>(nameof(GenerateRandom));
    }
}