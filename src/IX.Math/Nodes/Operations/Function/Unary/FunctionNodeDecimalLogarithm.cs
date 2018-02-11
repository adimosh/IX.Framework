// <copyright file="FunctionNodeDecimalLogarithm.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("lg({Parameter})")]
    [CallableMathematicsFunction("lg")]
    internal sealed class FunctionNodeDecimalLogarithm : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeDecimalLogarithm(NodeBase parameter)
            : base(parameter)
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(global::System.Math.Log10(stringParam.ExtractFloat()));
            }

            return this;
        }

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeDecimalLogarithm(this.Parameter.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Log10));
    }
}