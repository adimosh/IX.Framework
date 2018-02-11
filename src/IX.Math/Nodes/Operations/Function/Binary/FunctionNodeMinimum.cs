// <copyright file="FunctionNodeMinimum.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Function.Binary
{
    [DebuggerDisplay("min({FirstParameter}, {SecondParameter})")]
    [CallableMathematicsFunction("min", "minimum")]
    internal sealed class FunctionNodeMinimum : NumericBinaryFunctionNodeBase
    {
        public FunctionNodeMinimum(NodeBase firstParameter, NodeBase secondParameter)
            : base(firstParameter?.Simplify(), secondParameter?.Simplify())
        {
        }

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeMinimum(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context));

        public override NodeBase Simplify()
        {
            NumericNode firstParam, secondParam;
            if ((firstParam = this.FirstParameter as NumericNode) != null &&
                (secondParam = this.SecondParameter as NumericNode) != null)
            {
                return new NumericNode(global::System.Math.Min(firstParam.ExtractFloat(), secondParam.ExtractFloat()));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() => this.GenerateStaticBinaryFunctionCall(typeof(global::System.Math), nameof(global::System.Math.Min));
    }
}