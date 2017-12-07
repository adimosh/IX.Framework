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

namespace IX.Math.Nodes.Operations.Function.Ternary
{
    [DebuggerDisplay("substring({FirstParameter}, {SecondParameter}, {ThirdParameter})")]
    [CallableMathematicsFunction("substr", "substring")]
    internal sealed class FunctionNodeSubstring : TernaryFunctionNodeBase
    {
        public FunctionNodeSubstring(StringNode stringParameter, NumericNode numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter, numericParameter, secondNumericParameter)
        {
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericNode numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter, numericParameter, secondNumericParameter)
        {
        }

        public FunctionNodeSubstring(StringNode stringParameter, NumericParameterNode numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger(), secondNumericParameter)
        {
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericParameterNode numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger(), secondNumericParameter)
        {
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericNode numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter, secondNumericParameter)
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, OperationNodeBase numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter, numericParameter?.Simplify(), secondNumericParameter)
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringNode stringParameter, OperationNodeBase numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter, numericParameter?.Simplify(), secondNumericParameter)
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericParameterNode numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.ParameterMustBeInteger(), secondNumericParameter)
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, OperationNodeBase numericParameter, NumericNode secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify(), secondNumericParameter)
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

        public FunctionNodeSubstring(StringNode stringParameter, NumericNode numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter, numericParameter, secondNumericParameter?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericNode numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter, numericParameter, secondNumericParameter?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(StringNode stringParameter, NumericParameterNode numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger(), secondNumericParameter?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericParameterNode numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger(), secondNumericParameter?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericNode numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter, secondNumericParameter?.ParameterMustBeInteger())
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, OperationNodeBase numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter, numericParameter?.Simplify(), secondNumericParameter?.ParameterMustBeInteger())
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringNode stringParameter, OperationNodeBase numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter, numericParameter?.Simplify(), secondNumericParameter?.ParameterMustBeInteger())
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericParameterNode numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.ParameterMustBeInteger(), secondNumericParameter?.ParameterMustBeInteger())
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, OperationNodeBase numericParameter, NumericParameterNode secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify(), secondNumericParameter?.ParameterMustBeInteger())
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

        public FunctionNodeSubstring(StringNode stringParameter, NumericNode numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter, numericParameter, secondNumericParameter?.Simplify())
        {
            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericNode numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter, numericParameter, secondNumericParameter?.Simplify())
        {
            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringNode stringParameter, NumericParameterNode numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger(), secondNumericParameter?.Simplify())
        {
            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, NumericParameterNode numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter, numericParameter?.ParameterMustBeInteger(), secondNumericParameter?.Simplify())
        {
            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericNode numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter, secondNumericParameter?.Simplify())
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringParameterNode stringParameter, OperationNodeBase numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter, numericParameter?.Simplify(), secondNumericParameter?.Simplify())
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(StringNode stringParameter, OperationNodeBase numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter, numericParameter?.Simplify(), secondNumericParameter?.Simplify())
        {
            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, NumericParameterNode numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.ParameterMustBeInteger(), secondNumericParameter?.Simplify())
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(OperationNodeBase stringParameter, OperationNodeBase numericParameter, OperationNodeBase secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify(), secondNumericParameter?.Simplify())
        {
            if (this.FirstParameter?.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.SecondParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.ThirdParameter?.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }

        public FunctionNodeSubstring(UndefinedParameterNode stringParameter, UndefinedParameterNode numericParameter, UndefinedParameterNode secondNumericParameter)
            : base(stringParameter?.DetermineString(), numericParameter?.DetermineNumeric()?.ParameterMustBeInteger(), secondNumericParameter?.DetermineNumeric()?.ParameterMustBeInteger())
        {
        }

        public FunctionNodeSubstring(NodeBase stringParameter, NodeBase numericParameter, UndefinedParameterNode secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.Simplify(), secondNumericParameter?.DetermineNumeric()?.ParameterMustBeInteger())
        {
            if (this.FirstParameter.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.SecondParameter.ReturnType != SupportedValueType.Numeric)
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

        public FunctionNodeSubstring(NodeBase stringParameter, UndefinedParameterNode numericParameter, NodeBase secondNumericParameter)
            : base(stringParameter?.Simplify(), numericParameter?.DetermineNumeric()?.ParameterMustBeInteger(), secondNumericParameter?.Simplify())
        {
            if (this.FirstParameter.ReturnType != SupportedValueType.String)
            {
                throw new ExpressionNotValidLogicallyException();
            }

            if (this.ThirdParameter.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
            else
            {
                if (this.ThirdParameter is NumericParameterNode npn)
                {
                    npn.ParameterMustBeInteger();
                }
            }
        }

        public FunctionNodeSubstring(UndefinedParameterNode stringParameter, NodeBase numericParameter, NodeBase secondNumericParameter)
            : base(stringParameter?.DetermineString(), numericParameter?.Simplify(), secondNumericParameter?.Simplify())
        {
            if (this.SecondParameter.ReturnType != SupportedValueType.Numeric)
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

            if (this.ThirdParameter.ReturnType != SupportedValueType.Numeric)
            {
                throw new ExpressionNotValidLogicallyException();
            }
            else
            {
                if (this.ThirdParameter is NumericParameterNode npn)
                {
                    npn.ParameterMustBeInteger();
                }
            }
        }

        public override SupportedValueType ReturnType => SupportedValueType.String;

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

            MethodInfo mi = typeof(string).GetTypeMethod(functionName, secondParameterType, thirdParameterType);

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