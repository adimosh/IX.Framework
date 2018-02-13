// <copyright file="FunctionNodePower.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;

namespace IX.Math.Nodes.Operations.Function.Binary
{
    [DebuggerDisplay("pow({FirstParameter}, {SecondParameter})")]
    [CallableMathematicsFunction("pow", "power")]
    internal sealed class FunctionNodePower : NumericBinaryFunctionNodeBase
    {
        public FunctionNodePower(NodeBase firstParameter, NodeBase secondParameter)
            : base(firstParameter?.Simplify(), secondParameter?.Simplify())
        {
        }

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodePower(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context));

        public override NodeBase Simplify()
        {
            NumericNode firstParam, secondParam;
            if ((firstParam = this.FirstParameter as NumericNode) != null &&
                (secondParam = this.SecondParameter as NumericNode) != null)
            {
                return new NumericNode(global::System.Math.Pow(firstParam.ExtractFloat(), secondParam.ExtractFloat()));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticBinaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Pow));
    }
}