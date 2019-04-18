// <copyright file="FunctionNodeTrim.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using JetBrains.Annotations;

namespace IX.Math.Nodes.Operations.Function.Unary
{
    [DebuggerDisplay("trim({Parameter})")]
    [CallableMathematicsFunction("trim")]
    [UsedImplicitly]
    internal sealed class FunctionNodeTrim : UnaryFunctionNodeBase
    {
        public FunctionNodeTrim(NodeBase parameter)
            : base(parameter)
        {
        }

        public override SupportedValueType ReturnType => SupportedValueType.String;

        public override NodeBase Simplify()
        {
            if (this.Parameter is StringNode stringParam)
            {
                return new StringNode(stringParam.Value.Trim());
            }

            return this;
        }

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeTrim(this.Parameter.DeepClone(context));

        protected override void EnsureCompatibleParameter(ref NodeBase parameter)
        {
            if (parameter is ParameterNode upn)
            {
                parameter = upn.DetermineString();
            }

            if (parameter.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        protected override Expression GenerateExpressionInternal() => this.GenerateParameterMethodCall<string>(nameof(string.Trim));
    }
}