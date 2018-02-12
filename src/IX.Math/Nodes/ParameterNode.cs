// <copyright file="ParameterNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using IX.Math.Registration;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A node representing a parameter.
    /// </summary>
    /// <seealso cref="IX.Math.Nodes.NodeBase" />
    public class ParameterNode : NodeBase
    {
        private readonly IParameterRegistry parametersRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterNode" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parametersRegistry">The parameters registry.</param>
        /// <exception cref="ArgumentNullException">parameterName</exception>
        internal ParameterNode(string parameterName, IParameterRegistry parametersRegistry)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            this.Name = parameterName;

            this.parametersRegistry = parametersRegistry ?? throw new ArgumentNullException(nameof(parametersRegistry));

            this.parametersRegistry.AdvertiseParameter(parameterName);
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <value>The name of the parameter.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the return type of this node.
        /// </summary>
        /// <value>The node return type.</value>
        public override SupportedValueType ReturnType => this.parametersRegistry.AdvertiseParameter(this.Name).ReturnType;

        /// <summary>
        /// Gets a value indicating whether or not this node is actually a constant.
        /// </summary>
        /// <value><c>true</c> if the node is a constant, <c>false</c> otherwise.</value>
        public override bool IsConstant => false;

        /// <summary>
        /// Gets a value indicating whether this instance is float.
        /// </summary>
        /// <value><c>null</c> if not set, <c>true</c> if is float; otherwise, <c>false</c>.</value>
        public bool? IsFloat => this.parametersRegistry.AdvertiseParameter(this.Name).IsFloat;

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        public override NodeBase DeepClone(NodeCloningContext context)
        {
            context.ParameterRegistry.CloneFrom(this.parametersRegistry.AdvertiseParameter(this.Name));

            return new ParameterNode(this.Name, context.ParameterRegistry);
        }

        /// <summary>
        /// Generates the expression that will be compiled into code.
        /// </summary>
        /// <returns>The generated <see cref="T:System.Linq.Expressions.Expression" />.</returns>
        public override Expression GenerateExpression() => this.parametersRegistry.AdvertiseParameter(this.Name).Compile();

        /// <summary>
        /// Generates the expression that will be compiled into code as a string expression.
        /// </summary>
        /// <returns>The generated <see cref="T:System.Linq.Expressions.Expression" /> that gives the values as a string.</returns>
        public override Expression GenerateStringExpression() => this.parametersRegistry.AdvertiseParameter(this.Name).CompileString();

        /// <summary>
        /// Simplifies this node, if possible, reflexively returns otherwise.
        /// </summary>
        /// <returns>This instance.</returns>
        public override NodeBase Simplify() => this;
    }
}