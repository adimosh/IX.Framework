// <copyright file="ByteArrayVariableNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace IX.Math.Nodes.Parameters
{
    /// <summary>
    /// A byte array variable node.
    /// </summary>
    /// <seealso cref="ParameterNodeBase" />
    [DebuggerDisplay("{Name} (byte[] variable)")]
    public class ByteArrayVariableNode : ByteArrayParameterNode
    {
        private Expression cachedBodyExpression;
        private ParameterExpression cachedVariableExpression;
        private NodeBase referenceNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariableNode" /> class.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="referenceNode">The reference node.</param>
        /// <exception cref="ArgumentNullException"><paramref name="referenceNode" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        internal ByteArrayVariableNode(string variableName, NodeBase referenceNode)
            : base(variableName)
        {
            this.referenceNode = referenceNode?.Simplify() ?? throw new ArgumentNullException(nameof(referenceNode));
        }

        /// <summary>
        /// Gets the reference node for this variable.
        /// </summary>
        /// <value>The reference node.</value>
        public NodeBase ReferenceNode => this.referenceNode;

        /// <summary>
        /// Generates an expression that will be cached before being compiled.
        /// </summary>
        /// <returns>The generated <see cref="Expression" /> to be cached.</returns>
        public override Expression GenerateCachedExpression()
        {
            if (this.cachedBodyExpression == null)
            {
                this.cachedBodyExpression = Expression.Assign(this.GenerateVariableExpression(), this.referenceNode.GenerateExpression());
            }

            return this.cachedBodyExpression;
        }

        /// <summary>
        /// Generates a string expression that will be cached before being compiled.
        /// </summary>
        /// <returns>The generated <see cref="Expression" /> to be cached.</returns>
        public override Expression GenerateCachedStringExpression() => this.referenceNode.GenerateStringExpression();

        /// <summary>
        /// Refreshes all the parameters recursively.
        /// </summary>
        /// <returns>A reference to the same conceptual node, but possibly a different instance.</returns>
        public sealed override NodeBase RefreshParametersRecursive() => this.referenceNode.RefreshParametersRecursive();

        /// <summary>
        /// Simplifies this node, if possible, reflexively returns otherwise.
        /// </summary>
        /// <returns>A simplified node, or this instance.</returns>
        public override NodeBase Simplify() => this;

        /// <summary>
        /// Generates the expression that represents the variable itself.
        /// </summary>
        /// <returns>A <see cref="ParameterExpression"/> representing the variable.</returns>
        public ParameterExpression GenerateVariableExpression()
        {
            if (this.cachedVariableExpression == null)
            {
                this.cachedVariableExpression = Expression.Variable(typeof(byte[]), this.Name);
            }

            return this.cachedVariableExpression;
        }
    }
}