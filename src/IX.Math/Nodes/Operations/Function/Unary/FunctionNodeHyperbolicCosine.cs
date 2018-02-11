// <copyright file="FunctionNodeHyperbolicCosine.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("cosh({Parameter})")]
    [CallableMathematicsFunction("cosh")]
    internal sealed class FunctionNodeHyperbolicCosine : NumericUnaryFunctionNodeBase
    {
        public FunctionNodeHyperbolicCosine(NodeBase parameter)
            : base(parameter)
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Parameter is NumericNode stringParam)
            {
                return new NumericNode(global::System.Math.Cosh(stringParam.ExtractFloat()));
            }

            return this;
        }

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeHyperbolicCosine(this.Parameter.DeepClone(context));

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticUnaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Cosh));
    }
}