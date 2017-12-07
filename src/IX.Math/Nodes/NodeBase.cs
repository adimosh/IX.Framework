// <copyright file="NodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Linq.Expressions;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for mathematics nodes.
    /// </summary>
    public abstract class NodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeBase"/> class.
        /// </summary>
        protected NodeBase()
        {
        }

        /// <summary>
        /// Gets the return type of this node.
        /// </summary>
        /// <value>The node return type.</value>
        public abstract SupportedValueType ReturnType { get; }

        /// <summary>
        /// Gets a value indicating whether or not this node is actually a constant.
        /// </summary>
        /// <value><c>true</c> if the node is a constant, <c>false</c> otherwise.</value>
        public abstract bool IsConstant { get; }

        /// <summary>
        /// Generates the expression that will be compiled into code.
        /// </summary>
        /// <returns>The generated <see cref="Expression"/>.</returns>
        public abstract Expression GenerateExpression();

        /// <summary>
        /// Generates the expression that will be compiled into code as a string expression.
        /// </summary>
        /// <returns>The generated <see cref="Expression"/> that gives the values as a string.</returns>
        public abstract Expression GenerateStringExpression();

        /// <summary>
        /// Refreshes all the parameters recursively.
        /// </summary>
        /// <returns>A reference to the same conceptual node, but possibly a different instance.</returns>
        public abstract NodeBase RefreshParametersRecursive();

        /// <summary>
        /// Simplifies this node, if possible, reflexively returns otherwise.
        /// </summary>
        /// <returns>A simplified node, or this instance.</returns>
        public abstract NodeBase Simplify();
    }
}