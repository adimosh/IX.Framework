// <copyright file="FunctionNodeTrim.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.StandardExtensions;

namespace IX.Math.Nodes.Operations.Function.Binary
{
    [DebuggerDisplay("trim({FirstParameter}, {SecondParameter})")]
    [CallableMathematicsFunction("trim")]
    internal sealed class FunctionNodeTrim : BinaryFunctionNodeBase
    {
        public FunctionNodeTrim(NodeBase stringParameter, NodeBase numericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify())
        {
            if (stringParameter is ParameterNode sp)
            {
                sp.DetermineString();
            }

            if (numericParameter is ParameterNode np)
            {
                np.DetermineString();
            }

            if (stringParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (numericParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.String;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeTrim(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context));

        public override NodeBase Simplify()
        {
            if (this.FirstParameter is StringNode stringParam && this.SecondParameter is StringNode charParam)
            {
                return new StringNode(stringParam.Value.Trim(charParam.Value.ToCharArray()));
            }

            return this;
        }

        protected override void EnsureCompatibleParameters(ref NodeBase firstParameter, ref NodeBase secondParameter)
        {
            // Nothing needs to be done here
        }

        protected override Expression GenerateExpressionInternal()
        {
            MethodInfo mia = typeof(string).GetMethodWithExactParameters(
                nameof(string.ToCharArray),
                new Type[0]);

            if (mia == null)
            {
                throw new InvalidOperationException(string.Format(Resources.FunctionCouldNotBeFound, nameof(string.ToCharArray)));
            }

            MethodInfo mi = typeof(string).GetMethodWithExactParameters(
                nameof(string.Trim),
                typeof(char[]));

            if (mi == null)
            {
                throw new InvalidOperationException(string.Format(Resources.FunctionCouldNotBeFound, nameof(string.Trim)));
            }

            Expression e1 = this.FirstParameter.GenerateExpression();
            Expression e2 = this.SecondParameter.GenerateExpression();

            if (e1.Type != typeof(string))
            {
                e1 = Expression.Convert(e1, typeof(string));
            }

            if (e2.Type != typeof(string))
            {
                e2 = Expression.Convert(e2, typeof(string));
            }

            return Expression.Call(e1, mi, Expression.Call(e2, mia));
        }
    }
}