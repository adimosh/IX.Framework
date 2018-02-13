// <copyright file="FunctionNodeRandom.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Generators;

namespace IX.Math.Nodes.Operations.Function.Binary
{
    [DebuggerDisplay("random({FirstParameter}, {SecondParameter})")]
    [CallableMathematicsFunction("rand", "random")]
    internal sealed class FunctionNodeRandom : NumericBinaryFunctionNodeBase
    {
        public FunctionNodeRandom(NodeBase firstParameter, NodeBase secondParameter)
            : base(firstParameter?.Simplify(), secondParameter?.Simplify())
        {
        }

        public static double GenerateRandom(double min, double max) => RandomNumberGenerator.Generate(min, max);

        public override NodeBase Simplify() => this;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeRandom(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticBinaryFunctionCall<FunctionNodeRandom>(nameof(GenerateRandom));
    }
}