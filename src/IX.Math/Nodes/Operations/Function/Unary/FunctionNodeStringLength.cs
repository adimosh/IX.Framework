// <copyright file="FunctionNodeStringLength.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("strlen({Parameter})")]
    [CallableMathematicsFunction("strlen", "length")]
    internal sealed class FunctionNodeStringLength : UnaryFunctionNodeBase
    {
        public FunctionNodeStringLength(NodeBase parameter)
            : base(parameter)
        {
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        public override NodeBase Simplify()
        {
            if (this.Parameter is StringNode stringParam)
            {
                return new NumericNode(Convert.ToInt64(stringParam.Value.Length));
            }

            return this;
        }

        public override NodeBase DeepClone() => new FunctionNodeStringLength(this.Parameter.DeepClone());

        protected override void EnsureCompatibleParameter(ref NodeBase parameter)
        {
            if (parameter is UndefinedParameterNode upn)
            {
                parameter = upn.DetermineString();
            }

            if (parameter.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        protected override Expression GenerateExpressionInternal() => Expression.Convert(this.GenerateParameterPropertyCall<string>(nameof(string.Length)), typeof(long));
    }
}