// <copyright file="FunctionNodeNaturalLogarithm.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("ln({Parameter})")]
    [CallableMathematicsFunction("ln")]
    internal sealed class FunctionNodeNaturalLogarithm : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeNaturalLogarithm(NodeBase parameter)
            : base(parameter)
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(global::System.Math.Log(stringParam.ExtractFloat()));
            }

            return this;
        }

        public override NodeBase DeepClone() => new FunctionNodeNaturalLogarithm(this.Parameter.DeepClone());

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Log));
    }
}