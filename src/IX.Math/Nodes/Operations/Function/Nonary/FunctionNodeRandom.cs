﻿// <copyright file="FunctionNodeRandom.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Generators;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Nonary
{
    [DebuggerDisplay("random()")]
    [CallableMathematicsFunction("rand", "random")]
    internal sealed class FunctionNodeRandom : NonaryFunctionNodeBase
    {
        public FunctionNodeRandom()
            : base()
        {
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        public static double GenerateRandom() => RandomNumberGenerator.Generate();

        public override NodeBase Simplify() => new NumericNode(GenerateRandom());

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticNonaryFunctionCall<FunctionNodeRandom>(nameof(GenerateRandom));
    }
}