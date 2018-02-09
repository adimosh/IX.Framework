// <copyright file="FunctionNodeAbsolute.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("abs({Parameter})")]
    [CallableMathematicsFunction("abs", "absolute")]
    internal sealed class FunctionNodeAbsolute : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeAbsolute(NodeBase parameter)
            : base(parameter?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(global::System.Math.Abs(stringParam.ExtractFloat()));
            }

            return this;
        }

        public override NodeBase DeepClone() => new FunctionNodeAbsolute(this.Parameter.DeepClone());

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Abs));
    }
}