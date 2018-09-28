// <copyright file="AddNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Nodes.Constants;
using IX.StandardExtensions;

namespace IX.Math.Nodes.Operations.Binary
{
    [DebuggerDisplay("{Left} + {Right}")]
    internal sealed class AddNode : BinaryOperationNodeBase
    {
        public AddNode(NodeBase left, NodeBase right)
            : base(left?.Simplify(), right?.Simplify())
        {
        }

        public override SupportedValueType ReturnType =>
            (this.Left.ReturnType == SupportedValueType.Unknown && this.Right.ReturnType == SupportedValueType.Unknown) ?
                SupportedValueType.Unknown :
                ((this.Left.ReturnType == SupportedValueType.String || this.Right.ReturnType == SupportedValueType.String) ?
                    SupportedValueType.String :
                    SupportedValueType.Numeric);

        public override NodeBase Simplify()
        {
            if (this.Left.IsConstant != this.Right.IsConstant)
            {
                return this;
            }

            if (!this.Left.IsConstant)
            {
                return this;
            }

            if (this.Left is NumericNode nn1Left && this.Right is NumericNode nn1Right)
            {
                return NumericNode.Add(nn1Left, nn1Right);
            }
            else if (this.Left is StringNode sn1Left && this.Right is StringNode sn1Right)
            {
                return new StringNode(sn1Left.Value + sn1Right.Value);
            }
            else if (this.Left is NumericNode nn2Left && this.Right is StringNode sn2Right)
            {
                return new StringNode($"{nn2Left.Value}{sn2Right.Value}");
            }
            else if (this.Left is StringNode sn3Left && this.Right is NumericNode nn3Right)
            {
                return new StringNode($"{sn3Left.Value}{nn3Right.Value}");
            }
            else if (this.Left is BoolNode bn4Left && this.Right is StringNode sn4Right)
            {
                return new StringNode($"{bn4Left.Value.ToString()}{sn4Right.Value}");
            }
            else if (this.Left is StringNode sn5Left && this.Right is BoolNode bn5Right)
            {
                return new StringNode($"{sn5Left.Value}{bn5Right.Value.ToString()}");
            }

            return this;
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context) => new AddNode(this.Left.DeepClone(context), this.Right.DeepClone(context));

        protected override void EnsureCompatibleOperands(ref NodeBase left, ref NodeBase right)
        {
            if (left is ParameterNode uLeft && uLeft.ReturnType == SupportedValueType.Unknown)
            {
                switch (right.ReturnType)
                {
                    case SupportedValueType.Numeric:
                        left = uLeft.DetermineNumeric();
                        break;

                    case SupportedValueType.Boolean:
                    case SupportedValueType.ByteArray:
                    case SupportedValueType.String:
                        left = uLeft.DetermineString();
                        break;

                    case SupportedValueType.Unknown:
                        break;

                    default:
                        throw new ExpressionNotValidLogicallyException();
                }
            }

            if (right is ParameterNode uRight && uRight.ReturnType == SupportedValueType.Unknown)
            {
                switch (left.ReturnType)
                {
                    case SupportedValueType.Numeric:
                        right = uRight.DetermineNumeric();
                        break;

                    case SupportedValueType.Boolean:
                    case SupportedValueType.ByteArray:
                    case SupportedValueType.String:
                        right = uRight.DetermineString();
                        break;

                    case SupportedValueType.Unknown:
                        break;

                    default:
                        throw new ExpressionNotValidLogicallyException();
                }
            }

            switch (left.ReturnType)
            {
                case SupportedValueType.Numeric:
                    if (right.ReturnType != SupportedValueType.Numeric && right.ReturnType != SupportedValueType.String)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.Boolean:
                case SupportedValueType.ByteArray:
                    if (right.ReturnType != SupportedValueType.String)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.String:
                case SupportedValueType.Unknown:
                    break;

                default:
                    throw new ExpressionNotValidLogicallyException();
            }

            switch (right.ReturnType)
            {
                case SupportedValueType.Numeric:
                    if (left.ReturnType != SupportedValueType.Numeric && left.ReturnType != SupportedValueType.String)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.Boolean:
                case SupportedValueType.ByteArray:
                    if (left.ReturnType != SupportedValueType.String)
                    {
                        throw new ExpressionNotValidLogicallyException();
                    }

                    break;

                case SupportedValueType.String:
                case SupportedValueType.Unknown:
                    break;

                default:
                    throw new ExpressionNotValidLogicallyException();
            }
        }

        protected override Expression GenerateExpressionInternal()
        {
            Tuple<Expression, Expression> pars = this.GetExpressionsOfSameTypeFromOperands();

            if (this.ReturnType == SupportedValueType.String)
            {
                global::System.Reflection.MethodInfo mi = typeof(string).GetMethodWithExactParameters(nameof(string.Concat), typeof(string), typeof(string));
                return Expression.Call(mi, pars.Item1, pars.Item2);
            }
            else
            {
                return Expression.Add(pars.Item1, pars.Item2);
            }
        }
    }
}