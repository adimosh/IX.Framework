// <copyright file="NumericParameterNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace IX.Math.Nodes.Parameters
{
    /// <summary>
    /// A numeric parameter node.
    /// </summary>
    /// <seealso cref="ParameterNodeBase" />
    [DebuggerDisplay("{Name} (numeric)")]
    public class NumericParameterNode : ParameterNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericParameterNode" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        internal NumericParameterNode(string parameterName)
                    : base(parameterName)
        {
        }

        /// <summary>
        /// Gets or sets whether this parameter is required to be float.
        /// </summary>
        /// <value>
        /// <c>true</c> if this parameter has to be a floating-point number,
        /// <c>false</c> if this parameter must not be a floating-point number and
        /// <c>null</c>, if it doesn't matter what type of numeric parameter this is.
        /// </value>
        public bool? RequireFloat { get; set; }

        /// <summary>
        /// Gets the return type.
        /// </summary>
        /// <value><see cref="SupportedValueType.Numeric"/>.</value>
        public override SupportedValueType ReturnType => SupportedValueType.Numeric;

        /// <summary>
        /// Generates an expression that will be cached before being compiled.
        /// </summary>
        /// <returns>The generated <see cref="T:System.Linq.Expressions.Expression" /> to be cached.</returns>
        public override Expression GenerateCachedExpression() => (this.RequireFloat ?? true) ?
                        Expression.Parameter(typeof(double), this.Name) :
                        Expression.Parameter(typeof(long), this.Name);

        /// <summary>
        /// Sets this parameter as an obligatory floating-point parameter.
        /// </summary>
        /// <returns>Reflexive return.</returns>
        public NumericParameterNode ParameterMustBeFloat()
        {
            if (this.RequireFloat != null)
            {
                if (!this.RequireFloat.Value)
                {
                    throw new InvalidOperationException(string.Format(Resources.ParameterMustBeFloatButAlreadyRequiredToBeInteger, this.Name));
                }
            }
            else
            {
                this.RequireFloat = true;
            }

            return this;
        }

        /// <summary>
        /// Sets this parameter as an obligatory integer parameter.
        /// </summary>
        /// <returns>Reflexive return.</returns>
        public NumericParameterNode ParameterMustBeInteger()
        {
            if (this.RequireFloat != null)
            {
                if (this.RequireFloat.Value)
                {
                    throw new InvalidOperationException(string.Format(Resources.ParameterMustBeIntegerButAlreadyRequiredToBeFloat, this.Name));
                }
            }
            else
            {
                this.RequireFloat = false;
            }

            return this;
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected override ParameterNodeBase DeepCloneInternal() => new NumericParameterNode(this.Name) { RequireFloat = this.RequireFloat };
    }
}