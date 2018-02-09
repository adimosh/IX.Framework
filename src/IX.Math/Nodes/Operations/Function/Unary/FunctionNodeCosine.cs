// <copyright file="FunctionNodeCosine.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("cos({Parameter})")]
    [CallableMathematicsFunction("cos", "cosine")]
    internal sealed class FunctionNodeCosine : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeCosine(NodeBase parameter)
            : base(parameter)
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(global::System.Math.Cos(stringParam.ExtractFloat()));
            }

            return this;
        }

        public override NodeBase DeepClone() => new FunctionNodeCosine(this.Parameter.DeepClone());

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Cos));
    }
}