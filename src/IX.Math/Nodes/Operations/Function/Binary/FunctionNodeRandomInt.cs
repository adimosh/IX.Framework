// <copyright file="FunctionNodeRandomInt.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Generators;

namespace IX.Math.Nodes.Operations.Function.Binary
{
    [DebuggerDisplay("randomint({FirstParameter}, {SecondParameter})")]
    [CallableMathematicsFunction("randomint")]
    internal sealed class FunctionNodeRandomInt : NumericBinaryFunctionNodeBase
    {
        public FunctionNodeRandomInt(NodeBase firstParameter, NodeBase secondParameter)
            : base(firstParameter?.Simplify(), secondParameter?.Simplify())
        {
            if (firstParameter is ParameterNode up1)
            {
                up1.DetermineInteger();
            }

            if (secondParameter is ParameterNode up2)
            {
                up2.DetermineInteger();
            }
        }

        public static long GenerateRandom(long min, long max) => RandomNumberGenerator.GenerateInt(min, max);

        public override NodeBase Simplify() => this;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeRandomInt(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticBinaryFunctionCall<FunctionNodeRandomInt>(nameof(GenerateRandom));
    }
}