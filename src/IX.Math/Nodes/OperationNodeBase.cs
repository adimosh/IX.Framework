// <copyright file="OperationNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using IX.StandardExtensions;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for a node representing an operation.
    /// </summary>
    /// <seealso cref="IX.Math.Nodes.NodeBase" />
    public abstract class OperationNodeBase : CachedExpressionNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationNodeBase"/> class.
        /// </summary>
        protected OperationNodeBase()
            : base()
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not this node is actually a constant.
        /// </summary>
        /// <value><see langword="true"/> if the node is a constant, <see langword="false"/> otherwise.</value>
        public override bool IsConstant => false;

        /// <summary>
        /// Generates an expression that will be cached before being compiled.
        /// </summary>
        /// <returns>The generated <see cref="T:System.Linq.Expressions.Expression" /> to be cached.</returns>
        /// <remarks>
        /// <para>This method works by first attempting to simplify this node.</para>
        /// <para>If the node can be simplified, <see cref="M:GenerateExpression"/> is called on the new node and returned in lieu of this expression.</para>
        /// <para>If this node cannot be simplified, or its simplification method returns reflexively, <see cref="GenerateExpressionInternal"/> is called.</para>
        /// </remarks>
        public override Expression GenerateCachedExpression()
        {
            NodeBase simplifiedExpression = this.Simplify();

            if (simplifiedExpression != this)
            {
                return simplifiedExpression.GenerateExpression();
            }
            else
            {
                return this.GenerateExpressionInternal();
            }
        }

        /// <summary>
        /// Generates the cached string expression.
        /// </summary>
        /// <returns>System.Linq.Expressions.Expression.</returns>
        /// <remarks>Since it is not possible for this node to be a constant node, the function <see cref="object.ToString"/> is called in whatever the node outputs.</remarks>
        public override Expression GenerateCachedStringExpression() => Expression.Call(this.GenerateExpression(), typeof(object).GetMethodWithExactParameters(
            nameof(object.ToString),
#if !STANDARD && !NET45
            Array.Empty<Type>()));
#else
            new Type[0]));
#endif

        /// <summary>
        /// Generates the expression that will be compiled into code.
        /// </summary>
        /// <returns>The expression.</returns>
        protected abstract Expression GenerateExpressionInternal();
    }
}