// <copyright file="BoolNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;

namespace IX.Math.Nodes.Constants
{
    /// <summary>
    /// A boolean node. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="ConstantNodeBase" />
    [DebuggerDisplay("{Value}")]
    public sealed class BoolNode : ConstantNodeBase
    {
        /// <summary>
        /// The value.
        /// </summary>
        private readonly bool value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolNode"/> class.
        /// </summary>
        /// <param name="value">The node's boolean value.</param>
        public BoolNode(bool value)
        {
            this.value = value;
        }

#pragma warning disable SA1623 // Property summary documentation must match accessors
        /// <summary>
        /// Gets a value indicating this <see cref="BoolNode"/>'s value.
        /// </summary>
        /// <value>The node's value.</value>
        public bool Value => this.value;
#pragma warning restore SA1623 // Property summary documentation must match accessors

        /// <summary>
        /// Gets the return type of this node.
        /// </summary>
        /// <value>Always <see cref="SupportedValueType.Boolean"/>.</value>
        public override SupportedValueType ReturnType => SupportedValueType.Boolean;

        /// <summary>
        /// Distills the value into a usable constant.
        /// </summary>
        /// <returns>A usable constant.</returns>
        public override object DistillValue() => this.value;

        /// <summary>
        /// Generates the expression that will be compiled into code.
        /// </summary>
        /// <returns>A <see cref="ConstantExpression"/> with a boolean value.</returns>
        public override Expression GenerateCachedExpression() => Expression.Constant(this.value, typeof(bool));

        /// <summary>
        /// Generates the expression that will be compiled into code as a string expression.
        /// </summary>
        /// <returns>The string expression.</returns>
        public override Expression GenerateCachedStringExpression() => Expression.Constant(this.value ? "true" : "false", typeof(string));

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context) => new BoolNode(this.Value);
    }
}