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
        public FunctionNodeStringLength(StringNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeStringLength(StringParameterNode parameter)
            : base(parameter)
        {
        }

        public FunctionNodeStringLength(UndefinedParameterNode parameter)
            : base(parameter?.DetermineString())
        {
        }

        public FunctionNodeStringLength(OperationNodeBase parameter)
            : base(parameter?.Simplify())
        {
            if (parameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        public override NodeBase Simplify()
        {
            StringNode stringParam;
            if ((stringParam = this.Parameter as StringNode) != null)
            {
                return new NumericNode(Convert.ToInt64(stringParam.Value.Length));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal() => Expression.Convert(this.GenerateParameterPropertyCall<string>(nameof(string.Length)), typeof(long));
    }
}