// <copyright file="FunctionNoderandom.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Generators;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("random({Parameter})")]
    [CallableMathematicsFunction("rand", "random")]
    internal sealed class FunctionNodeRandom : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeRandom(NodeBase parameter)
            : base(parameter)
        {
        }

        public static double GenerateRandom(double max) => RandomNumberGenerator.Generate(max);

        public override NodeBase Simplify() => this;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeRandom(this.Parameter.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall<FunctionNodeRandom>(nameof(GenerateRandom));
    }
}