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

namespace IX.Math.Nodes.Operations.Function.Ternary
{
    [DebuggerDisplay("substring({FirstParameter}, {SecondParameter}, {ThirdParameter})")]
    [CallableMathematicsFunction("substr", "substring")]
    internal sealed class FunctionNodeSubstring : TernaryFunctionNodeBase
    {
        public FunctionNodeSubstring(NodeBase stringParameter, NodeBase numericParameter, NodeBase secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify(), secondNumericParameter?.Simplify())
        {
            if (stringParameter is ParameterNode pn)
            {
                pn.DetermineString();
            }

            if (numericParameter is ParameterNode np1)
            {
                np1.DetermineNumeric().DetermineInteger();
            }

            if (secondNumericParameter is ParameterNode np2)
            {
                np2.DetermineNumeric().DetermineInteger();
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.String;

        public override NodeBase DeepClone(NodeCloningContext context) => new FunctionNodeSubstring(this.FirstParameter.DeepClone(context), this.SecondParameter.DeepClone(context), this.ThirdParameter.DeepClone(context));

        public override NodeBase Simplify()
        {
            if (this.FirstParameter is StringNode stringParam && this.SecondParameter is NumericNode numericParam && this.ThirdParameter is NumericNode secondNumericParam)
            {
                return new StringNode(stringParam.Value.Substring(numericParam.ExtractInt(), secondNumericParam.ExtractInt()));
            }

            return this;
        }

        protected override Expression GenerateExpressionInternal()
        {
            Type firstParameterType = typeof(string);
            Type secondParameterType = typeof(int);
            Type thirdParameterType = typeof(int);
            var functionName = nameof(string.Substring);

            MethodInfo mi = typeof(string).GetMethodWithExactParameters(functionName, secondParameterType, thirdParameterType);

            if (mi == null)
            {
                throw new InvalidOperationException(string.Format(Resources.FunctionCouldNotBeFound, functionName));
            }

            Expression e1 = this.FirstParameter.GenerateExpression();
            Expression e2 = this.SecondParameter.GenerateExpression();
            Expression e3 = this.ThirdParameter.GenerateExpression();

            if (e1.Type != firstParameterType)
            {
                e1 = Expression.Convert(e1, firstParameterType);
            }

            if (e2.Type != secondParameterType)
            {
                e2 = Expression.Convert(e2, secondParameterType);
            }

            if (e3.Type != thirdParameterType)
            {
                e3 = Expression.Convert(e3, thirdParameterType);
            }

            return Expression.Call(e1, mi, e2, e3);
        }
    }
}