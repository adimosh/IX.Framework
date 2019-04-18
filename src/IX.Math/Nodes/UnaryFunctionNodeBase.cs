// <copyright file="UnaryFunctionNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using System.Reflection;
using IX.StandardExtensions;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for a unary function (a function with only one parameter).
    /// </summary>
    /// <seealso cref="FunctionNodeBase" />
    public abstract class UnaryFunctionNodeBase : FunctionNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryFunctionNodeBase"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="ArgumentNullException">parameter.</exception>
        protected UnaryFunctionNodeBase(NodeBase parameter)
        {
            NodeBase parameterTemp = parameter ?? throw new ArgumentNullException(nameof(parameter));

            this.EnsureCompatibleParameter(ref parameterTemp);

            this.Parameter = parameterTemp.Simplify();
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public NodeBase Parameter { get; private set; }

        /// <summary>
        /// Ensures that the parameter that is received is compatible with the function, optionally allowing the parameter reference to change.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected abstract void EnsureCompatibleParameter(ref NodeBase parameter);

        /// <summary>
        /// Generates a static unary function call.
        /// </summary>
        /// <typeparam name="T">The type to call the method from.</typeparam>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>An expression representing the static function call.</returns>
        /// <exception cref="ArgumentException"><paramref name="functionName"/> represents a function that cannot be found.</exception>
        protected Expression GenerateStaticUnaryFunctionCall<T>(string functionName) =>
            this.GenerateStaticUnaryFunctionCall(typeof(T), functionName);

        /// <summary>
        /// Generates a static unary function call.
        /// </summary>
        /// <param name="t">The type to call the method from.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>An expression representing the static function call.</returns>
        /// <exception cref="ArgumentException"><paramref name="functionName"/> represents a function that cannot be found.</exception>
        protected Expression GenerateStaticUnaryFunctionCall(Type t, string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
            }

            Type parameterType = ParameterTypeFromParameter(this.Parameter);

            MethodInfo mi = t.GetMethodWithExactParameters(functionName, parameterType);

            if (mi == null)
            {
                parameterType = typeof(double);

                mi = t.GetMethodWithExactParameters(functionName, parameterType);

                if (mi == null)
                {
                    parameterType = typeof(long);

                    mi = t.GetMethodWithExactParameters(functionName, parameterType);

                    if (mi == null)
                    {
                        parameterType = typeof(int);

                        mi = t.GetMethodWithExactParameters(functionName, parameterType) ??
                            throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
                    }
                }
            }

            Expression e = this.Parameter.GenerateExpression();

            if (e.Type != parameterType)
            {
                e = Expression.Convert(e, parameterType);
            }

            return Expression.Call(mi, e);
        }

        /// <summary>
        /// Generates a property call on the parameter.
        /// </summary>
        /// <typeparam name="T">The type to call the property from.</typeparam>
        /// <param name="propertyName">Name of the parameter.</param>
        /// <returns>An expression representing a property call.</returns>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> represents a property that cannot be found.</exception>
        protected Expression GenerateParameterPropertyCall<T>(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, propertyName), nameof(propertyName));
            }

            PropertyInfo pi = typeof(T).GetRuntimeProperty(propertyName) ??
                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, propertyName), nameof(propertyName));

            return Expression.Property(this.Parameter.GenerateExpression(), pi);
        }

        /// <summary>
        /// Generates a property call on the parameter.
        /// </summary>
        /// <typeparam name="T">The type to call the property from.</typeparam>
        /// <param name="methodName">Name of the parameter.</param>
        /// <returns>An expression representing a property call.</returns>
        /// <exception cref="ArgumentException"><paramref name="methodName"/> represents a property that cannot be found.</exception>
        protected Expression GenerateParameterMethodCall<T>(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, methodName), nameof(methodName));
            }

            MethodInfo mi = typeof(T).GetRuntimeMethod(methodName, new Type[0]) ??
                              throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, methodName), nameof(methodName));

            return Expression.Call(this.Parameter.GenerateExpression(), mi);
        }
    }
}