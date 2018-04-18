// <copyright file="FunctionNodeSubstring.cs" company="Adrian Mos">
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
    [DebuggerDisplay("substring({FirstParameter}, {SecondParameter})")]
    [CallableMathematicsFunction("substr", "substring")]
    internal sealed class FunctionNodeSubstring : BinaryFunctionNodeBase
    {
        public FunctionNodeSubstring(NodeBase stringParameter, NodeBase numericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify())
        {
            if (stringParameter is ParameterNode sp)
            {
                sp.DetermineString();
            }

            if (numericParameter is ParameterNode np)
            {
                np?.DetermineNumeric().DetermineInteger();
            }

            if (stringParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (numericParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.String;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeSubstring(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context));

        public override NodeBase Simplify()
        {
            if (this.FirstParameter is StringNode stringParam && this.SecondParameter is NumericNode numericParam)
            {
                return new StringNode(stringParam.Value.Substring(numericParam.ExtractInt()));
            }

            return this;
        }

        protected override void EnsureCompatibleParameters(ref NodeBase firstParameter, ref NodeBase secondParameter)
        {
            // Nothing needs to be done here
        }

        protected override Expression GenerateExpressionInternal()
        {
            Type firstParameterType = typeof(string);
            Type secondParameterType = typeof(int);
            var functionName = nameof(string.Substring);

            MethodInfo mi = typeof(string).GetMethodWithExactParameters(functionName, secondParameterType);

            if (mi == null)
            {
                throw new InvalidOperationException(string.Format(Resources.FunctionCouldNotBeFound, functionName));
            }

            Expression e1 = this.FirstParameter.GenerateExpression();
            Expression e2 = this.SecondParameter.GenerateExpression();

            if (e1.Type != firstParameterType)
            {
                e1 = Expression.Convert(e1, firstParameterType);
            }

            if (e2.Type != secondParameterType)
            {
                e2 = Expression.Convert(e2, secondParameterType);
            }

            return Expression.Call(e1, mi, e2);
        }
    }
}