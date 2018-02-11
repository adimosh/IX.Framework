// <copyright file="FunctionNodeSubstring.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using IX.Math.Extensibility;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;
using IX.Math.PlatformMitigation;

namespace IX.Math.Nodes.Operations.Function.Binary
{
    [DebuggerDisplay("substring({FirstParameter}, {SecondParameter})")]
    [CallableMathematicsFunction("substr", "substring")]
    internal sealed class FunctionNodeSubstring : BinaryFunctionNodeBase
    {
        public FunctionNodeSubstring(StringNode stringParameter, NumericNode numericParameter)
            : base(stringParameter, numericParameter)
        {
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericNode numericParameter)
            : base(stringParameter, numericParameter)
        {
        }

        public FunctionNodeSubstring(StringNode stringParameter, NumericParameterNode numericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericParameterNode numericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericNode numericParameter)
            : base(stringParameter?.Simplify(), numericParameter)
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, OperationNodeBase numericParameter)
            : base(stringParameter, numericParameter?.Simplify())
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringNode stringParameter, OperationNodeBase numericParameter)
            : base(stringParameter, numericParameter?.Simplify())
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericParameterNode numericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.ParameterMustBeInteger())
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, OperationNodeBase numericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify())
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(UndefinedParameterNode stringParameter, UndefinedParameterNode numericParameter)
            : base(stringParameter?.DetermineString(), numericParameter?.DetermineNumeric()?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(NodeBase stringParameter, UndefinedParameterNode numericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.DetermineNumeric()?.ParameterMustBeInteger())
        {
            if (this.FirstParameter.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(UndefinedParameterNode stringParameter, NodeBase numericParameter)
            : base(stringParameter?.DetermineString(), numericParameter?.Simplify())
        {
            if (this.FirstParameter.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
            else
            {
                if (this.SecondParameter is NumericParameterNode npn)
                {
                    npn.ParameterMustBeInteger();
                }
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.String;

        public override NodeBase DeepClone(NodeCloningContext context) => throw new NotImplementedException();

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
            if (firstParameter is UndefinedParameterNode fp)
            {
                firstParameter = fp.DetermineString();
            }

            if (secondParameter is UndefinedParameterNode sp)
            {
                secondParameter = sp.DetermineNumeric().ParameterMustBeInteger();
            }
        }

        protected override Expression GenerateExpressionInternal()
        {
            Type firstParameterType = typeof(string);
            Type secondParameterType = typeof(int);
            var functionName = nameof(string.Substring);

            MethodInfo mi = typeof(string).GetTypeMethod(functionName, secondParameterType);

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