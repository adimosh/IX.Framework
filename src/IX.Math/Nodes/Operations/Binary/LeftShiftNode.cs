// <copyright file="LeftShiftNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;
using IX.Math.PlatformMitigation;
using IX.StandardExtensions;

namespace IX.Math.Nodes.Operations.Binary
{
    [DebuggerDisplay("{Left} << {Right}")]
    internal sealed class LeftShiftNode : ByteShiftOperationNodeBase
    {
        public LeftShiftNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override NodeBase Simplify()
        {
            if (this.Left is NumericNode nLeft && this.Right is NumericNode nRight)
            {
                return NumericNode.LeftShift(nLeft, nRight);
            }
            else if (this.Left is ByteArrayNode baLeft && this.Right is NumericNode baRight)
            {
                return new ByteArrayNode(baLeft.Value.LeftShift(baRight.ExtractInt()));
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone() => new LeftShiftNode(this.Left.DeepClone(), this.Right.DeepClone());

        protected override Expression GenerateExpressionInternal()
        {
            Expression rightExpression = Expression.Convert(this.Right.GenerateExpression(), typeof(int));
            if (this.Left.ReturnType == SupportedValueType.Numeric)
            {
                return Expression.LeftShift(this.Left.GenerateExpression(), rightExpression);
            }
            else if (this.Left.ReturnType == SupportedValueType.ByteArray)
            {
                return Expression.Call(
                    typeof(BitwiseExtensions).GetTypeMethod(nameof(BitwiseExtensions.LeftShift), typeof(byte[]), typeof(int)),
                    this.Left.GenerateExpression(),
                    rightExpression);
            }
            else
            {
                throw new ExpressionNotValidLogicallyException();
            }
        }
    }
}