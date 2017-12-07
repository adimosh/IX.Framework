// <copyright file="FunctionNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for a function node.
    /// </summary>
    /// <seealso cref="OperationNodeBase" />
    public abstract class FunctionNodeBase : OperationNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionNodeBase"/> class.
        /// </summary>
        protected FunctionNodeBase()
        {
        }

        /// <summary>
        /// Gets the concrete type of a parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The parameter type.</returns>
        /// <exception cref="System.InvalidOperationException">The parameter could not be correctly recognized, or is undefined.</exception>
        protected static Type ParameterTypeFromParameter(NodeBase parameter)
        {
            Type parameterType;

            switch (parameter.ReturnType)
            {
                case SupportedValueType.Numeric:
                    {
                        switch (parameter)
                        {
                            case NumericParameterNode nn:
                                if (nn.RequireFloat == false)
                                {
                                    parameterType = typeof(long);
                                }
                                else
                                {
                                    parameterType = typeof(double);
                                }

                                break;
                            case NumericNode cn:
                                parameterType = cn.Value.GetType();
                                break;
                            default:
                                parameterType = typeof(double);
                                break;
                        }
                    }

                    break;
                case SupportedValueType.Boolean:
                    parameterType = typeof(bool);
                    break;
                case SupportedValueType.String:
                    parameterType = typeof(string);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return parameterType;
        }
    }
}