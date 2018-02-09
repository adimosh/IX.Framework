// <copyright file="FunctionNodeTangent.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("tan({Parameter})")]
    [CallableMathematicsFunction("tan", "tangent")]
    internal sealed class FunctionNodeTangent : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeTangent(NodeBase parameter)
            : base(parameter)
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(global::System.Math.Tan(stringParam.ExtractFloat()));
            }

            return this;
        }

        public override NodeBase DeepClone() => new FunctionNodeTangent(this.Parameter.DeepClone());

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Tan));
    }
}