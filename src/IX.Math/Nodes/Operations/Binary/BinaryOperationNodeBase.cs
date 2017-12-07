// <copyright file="BinaryOperationNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IX.Math.Nodes.Operations.Binary
{
    internal abstract class BinaryOperationNodeBase : OperationNodeBase
    {
        protected BinaryOperationNodeBase(NodeBase left, NodeBase right)
        {
            NodeBase leftTemp = left ?? throw new ArgumentNullException(nameof(left));
            NodeBase rightTemp = right ?? throw new ArgumentNullException(nameof(right));

            this.EnsureCompatibleOperands(ref leftTemp, ref rightTemp);

            this.Left = leftTemp?.Simplify();
            this.Right = rightTemp?.Simplify();
        }

        public NodeBase Left { get; protected set; }

        public NodeBase Right { get; protected set; }

        public override NodeBase RefreshParametersRecursive()
        {
            this.Left = this.Left.RefreshParametersRecursive();
            this.Right = this.Right.RefreshParametersRecursive();

            return this;
        }

        protected abstract void EnsureCompatibleOperands(ref NodeBase left, ref NodeBase right);

        protected Tuple<Expression, Expression> GetExpressionsOfSameTypeFromOperands()
        {
            if (this.Left.ReturnType == SupportedValueType.String || this.Right.ReturnType == SupportedValueType.String)
            {
                return new Tuple<Expression, Expression>(this.Left.GenerateStringExpression(), this.Right.GenerateStringExpression());
            }

            Expression le = this.Left.GenerateExpression();
            Expression re = this.Right.GenerateExpression();

            if (le.Type == typeof(double) && re.Type == typeof(long))
            {
                return new Tuple<Expression, Expression>(le, Expression.Convert(re, typeof(double)));
            }
            else if (le.Type == typeof(long) && re.Type == typeof(double))
            {
                return new Tuple<Expression, Expression>(Expression.Convert(le, typeof(double)), re);
            }

            return new Tuple<Expression, Expression>(le, re);
        }
    }
}