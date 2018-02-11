// <copyright file="ByteArrayNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace IX.Math.Nodes.Constants
{
    /// <summary>
    /// A boolean node. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="ConstantNodeBase" />
    [DebuggerDisplay("{DisplayValue}")]
    public class ByteArrayNode : ConstantNodeBase
    {
        private readonly byte[] value;
        private string cachedDistilledStringValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayNode"/> class.
        /// </summary>
        /// <param name="value">The value of the constant.</param>
        public ByteArrayNode(byte[] value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets the return type of this node.
        /// </summary>
        /// <value>Always <see cref="SupportedValueType.ByteArray"/>.</value>
        public override SupportedValueType ReturnType => SupportedValueType.ByteArray;

        /// <summary>
        /// Gets the value of the node.
        /// </summary>
        public byte[] Value => this.value;

        /// <summary>
        /// Gets the display value.
        /// </summary>
        public string DisplayValue => this.DistillStringValue();

        /// <summary>
        /// Distills the value into a usable constant.
        /// </summary>
        /// <returns>A usable constant.</returns>
        public override object DistillValue() => this.value;

        /// <summary>
        /// Generates the expression that will be compiled into code.
        /// </summary>
        /// <returns>A <see cref="ConstantExpression"/> with a boolean value.</returns>
        public override Expression GenerateCachedExpression() => Expression.Constant(this.value, typeof(byte[]));

        /// <summary>
        /// Generates the expression that will be compiled into code as a string expression.
        /// </summary>
        /// <returns>The string expression.</returns>
        public override Expression GenerateCachedStringExpression() => Expression.Constant(this.DistillStringValue(), typeof(string));

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context) => new ByteArrayNode(this.Value);

        private string DistillStringValue()
        {
            if (this.cachedDistilledStringValue != null)
            {
                return this.cachedDistilledStringValue;
            }

            var bldr = new StringBuilder();

            bldr.Append("0b");

            if (this.value.Length == 0)
            {
                bldr.Append("0");
            }
            else
            {
                foreach (var b in this.value)
                {
                    bldr.Append(Convert.ToString(b, 2));
                }
            }

            return this.cachedDistilledStringValue = bldr.ToString();
        }
    }
}