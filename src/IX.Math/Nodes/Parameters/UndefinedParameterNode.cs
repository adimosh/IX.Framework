// <copyright file="UndefinedParameterNode.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using IX.Math.Generators;
using IX.Math.Registration;

namespace IX.Math.Nodes.Parameters
{
    /// <summary>
    /// An undefined parameter. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="ParameterNodeBase" />
    [DebuggerDisplay("{Name} (undefined)")]
    public sealed class UndefinedParameterNode : ParameterNodeBase
    {
        /// <summary>
        /// The parameters registry.
        /// </summary>
        private IParameterRegistry parametersRegistry;

        /// <summary>
        /// The determine float.
        /// </summary>
        private bool? determineFloat;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndefinedParameterNode"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parametersRegistry">The parameters registry.</param>
        internal UndefinedParameterNode(string parameterName, IParameterRegistry parametersRegistry)
            : base(parameterName)
        {
            this.parametersRegistry = parametersRegistry;
        }

        /// <summary>
        /// Gets the return type of this node.
        /// </summary>
        /// <value>The node return type.</value>
        public override SupportedValueType ReturnType => SupportedValueType.Unknown;

        /// <summary>
        /// Transforms this undefined parameter into a numeric parameter.
        /// </summary>
        /// <returns>A numeric parameter node.</returns>
        public NumericParameterNode DetermineNumeric()
        {
            var para = this.parametersRegistry.RegisterParameter(this.Name, SupportedValueType.Numeric) as NumericParameterNode;

            if (para == null)
            {
                throw new InvalidOperationException(Resources.ParameterRegistryReturnedNull);
            }

            if (this.determineFloat == true)
            {
                para = para.ParameterMustBeFloat();
            }
            else if (this.determineFloat == false)
            {
                para = para.ParameterMustBeInteger();
            }

            return para;
        }

        /// <summary>
        /// If the parameter is later determined to be numeric, also determine it to be floating-point.
        /// </summary>
        /// <returns>Reflexive return.</returns>
        public UndefinedParameterNode IfDeterminedNumericAlsoDetermineInteger()
        {
            this.determineFloat = false;
            return this;
        }

        /// <summary>
        /// If the parameter is later determined to be numeric, also determine it to be integer.
        /// </summary>
        /// <returns>Reflexive return.</returns>
        public UndefinedParameterNode IfDeterminedNumericAlsoDetermineFloat()
        {
            this.determineFloat = true;
            return this;
        }

        /// <summary>
        /// Transforms this undefined parameter into a boolean parameter.
        /// </summary>
        /// <returns>A boolean parameter node.</returns>
        public BoolParameterNode DetermineBool()
        {
            var para = this.parametersRegistry.RegisterParameter(this.Name, SupportedValueType.Boolean) as BoolParameterNode;

            if (para == null)
            {
                throw new InvalidOperationException(Resources.ParameterRegistryReturnedNull);
            }

            return para;
        }

        /// <summary>
        /// Transforms this undefined parameter into a byte array parameter.
        /// </summary>
        /// <returns>A byte array parameter node.</returns>
        public ByteArrayParameterNode DetermineByteArray()
        {
            var para = this.parametersRegistry.RegisterParameter(this.Name, SupportedValueType.ByteArray) as ByteArrayParameterNode;

            if (para == null)
            {
                throw new InvalidOperationException(Resources.ParameterRegistryReturnedNull);
            }

            return para;
        }

        /// <summary>
        /// Transforms this undefined parameter into a string parameter.
        /// </summary>
        /// <returns>A string parameter node.</returns>
        public StringParameterNode DetermineString()
        {
            var para = this.parametersRegistry.RegisterParameter(this.Name, SupportedValueType.String) as StringParameterNode;

            if (para == null)
            {
                throw new InvalidOperationException(Resources.ParameterRegistryReturnedNull);
            }

            return para;
        }

        /// <summary>
        /// Generates the expression that will be compiled into code as a string expression.
        /// </summary>
        /// <returns>Nothing, as this method always throws an exception.</returns>
        /// <exception cref="global::System.InvalidOperationException">This node cannot be compiled, as it's supposed to be determined beforehand.</exception>
        public override Expression GenerateCachedStringExpression() => throw new InvalidOperationException();

        /// <summary>
        /// Refreshes all the parameters recursively.
        /// </summary>
        /// <returns>A reference to the determined parameter.</returns>
        public override NodeBase RefreshParametersRecursive() => this.parametersRegistry.RegisterParameter(this.Name);

        /// <summary>
        /// Generates the expression that will be compiled into code.
        /// </summary>
        /// <returns>Nothing, as this method always throws an exception.</returns>
        /// <exception cref="global::System.InvalidOperationException">This node cannot be compiled, as it's supposed to be determined beforehand.</exception>
        public override Expression GenerateCachedExpression() => throw new InvalidOperationException();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        protected override ParameterNodeBase DeepCloneInternal(NodeCloningContext context)
        {
            var para = context.ParameterRegistry.RegisterParameter(this.Name) as UndefinedParameterNode;

            if (para == null)
            {
                throw new InvalidOperationException(Resources.ParameterRegistryReturnedNull);
            }

            para.determineFloat = this.determineFloat;

            return para;
        }
    }
}