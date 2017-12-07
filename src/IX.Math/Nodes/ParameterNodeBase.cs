// <copyright file="ParameterNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using IX.Math.PlatformMitigation;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for a parameter node.
    /// </summary>
    /// <seealso cref="IX.Math.Nodes.CachedExpressionNodeBase" />
    public abstract class ParameterNodeBase : CachedExpressionNodeBase
    {
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterNodeBase"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        internal ParameterNodeBase(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            this.name = parameterName;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <value>The name of the parameter.</value>
        public string Name => this.name;

        /// <summary>
        /// Gets a value indicating whether or not this node is actually a constant.
        /// </summary>
        /// <value><c>true</c> if the node is a constant, <c>false</c> otherwise.</value>
        public override bool IsConstant => false;

        /// <summary>
        /// Refreshes all the parameters recursively.
        /// </summary>
        /// <returns>A reflexive reference.</returns>
        public override NodeBase RefreshParametersRecursive() => this;

        /// <summary>
        /// Simplifies this node, if possible, reflexively returns otherwise.
        /// </summary>
        /// <returns>A reflexive reference.</returns>
        public override NodeBase Simplify() => this;

        /// <summary>
        /// Generates a string expression that will be cached before being compiled.
        /// </summary>
        /// <returns>The generated <see cref="Expression" /> to be cached.</returns>
        public override Expression GenerateCachedStringExpression() => Expression.Call(this.GenerateExpression(), typeof(object).GetTypeMethod(nameof(object.ToString)));
    }
}