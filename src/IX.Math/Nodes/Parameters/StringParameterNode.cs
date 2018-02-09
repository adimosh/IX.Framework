// <copyright file="StringParameterNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace IX.Math.Nodes.Parameters
{
    /// <summary>
    /// A string parameter node. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="ParameterNodeBase" />
    [DebuggerDisplay("{Name} (string)")]
    public sealed class StringParameterNode : ParameterNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringParameterNode"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        internal StringParameterNode(string parameterName)
            : base(parameterName)
        {
        }

        /// <summary>
        /// Gets the return type.
        /// </summary>
        /// <value><see cref="SupportedValueType.String"/>.</value>
        public override SupportedValueType ReturnType => SupportedValueType.String;

        /// <summary>
        /// Generates a string expression that will be cached before being compiled.
        /// </summary>
        /// <returns>The generated <see cref="T:System.Linq.Expressions.Expression" /> to be cached.</returns>
        public override Expression GenerateCachedStringExpression() => this.GenerateExpression();

        /// <summary>
        /// Generates an expression that will be cached before being compiled.
        /// </summary>
        /// <returns>The generated <see cref="T:System.Linq.Expressions.Expression" /> to be cached.</returns>
        public override Expression GenerateCachedExpression() => Expression.Parameter(typeof(string), this.Name);

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected override ParameterNodeBase DeepCloneInternal() => new StringParameterNode(this.Name);
    }
}