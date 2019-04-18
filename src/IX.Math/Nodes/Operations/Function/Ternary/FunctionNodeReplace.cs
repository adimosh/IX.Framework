// <copyright file="FunctionNodeReplace.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.StandardExtensions;

namespace IX.Math.Nodes.Operations.Function.Ternary
{
    [DebuggerDisplay("replace({FirstParameter}, {SecondParameter}, {ThirdParameter})")]
    [CallableMathematicsFunction("repl", "replace")]
    internal sealed class FunctionNodeReplace : TernaryFunctionNodeBase
    {
        public FunctionNodeReplace(NodeBase stringParameter, NodeBase numericParameter, NodeBase secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify(), secondNumericParameter?.Simplify())
        {
            if (stringParameter is ParameterNode pn)
            {
                pn.DetermineString();
            }

            if (numericParameter is ParameterNode np1)
            {
                np1.DetermineString();
            }

            if (secondNumericParameter is ParameterNode np2)
            {
                np2.DetermineString();
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.String;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeReplace(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context), this.ThirdParameter.DeepClone(context));

        public override NodeBase Simplify()
        {
            if (this.FirstParameter is StringNode stringParam && this.SecondParameter is StringNode numericParam && this.ThirdParameter is StringNode secondNumericParam)
            {
                return new StringNode(stringParam.Value.Replace(numericParam.Value, secondNumericParam.Value));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal()
        {
            MethodInfo mi = typeof(string).GetMethodWithExactParameters(
                nameof(string.Replace),
                typeof(string),
                typeof(string));

            if (mi == null)
            {
                throw new InvalidOperationException(string.Format(Resources.FunctionCouldNotBeFound, nameof(string.Replace)));
            }

            Expression e1 = this.FirstParameter.GenerateExpression();
            Expression e2 = this.SecondParameter.GenerateExpression();
            Expression e3 = this.ThirdParameter.GenerateExpression();

            if (e1.Type != typeof(string))
            {
                e1 = Expression.Convert(e1, typeof(string));
            }

            if (e2.Type != typeof(string))
            {
                e2 = Expression.Convert(e2, typeof(string));
            }

            if (e3.Type != typeof(string))
            {
                e3 = Expression.Convert(e3, typeof(string));
            }

            return Expression.Call(e1, mi, e2, e3);
        }
    }
}