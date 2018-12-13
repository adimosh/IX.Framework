// <copyright file="FunctionNodeRandomInt.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Generators;

namespace IX.Math.Nodes.Operations.Function.Nonary
{
    [DebuggerDisplay("randomint()")]
    [CallableMathematicsFunction("randomint")]
    internal sealed class FunctionNodeRandomInt : NonaryFunctionNodeBase
    {
        public FunctionNodeRandomInt()
            : base()
        {
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        public static long GenerateRandom() => RandomNumberGenerator.GenerateInt();

        public override NodeBase Simplify() => this;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeRandomInt();

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticNonaryFunctionCall<FunctionNodeRandomInt>(nameof(GenerateRandom));
    }
}