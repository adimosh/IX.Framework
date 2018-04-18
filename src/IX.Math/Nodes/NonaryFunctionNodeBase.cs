// <copyright file="NonaryFunctionNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using System.Reflection;
using IX.StandardExtensions;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for functions that take no parameters.
    /// </summary>
    /// <seealso cref="FunctionNodeBase" />
    public abstract class NonaryFunctionNodeBase : FunctionNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonaryFunctionNodeBase"/> class.
        /// </summary>
        protected NonaryFunctionNodeBase()
            : base()
        {
        }

        /// <summary>
        /// Generates a static function call for a function with no parameters.
        /// </summary>
        /// <typeparam name="T">The type to call the function on.</typeparam>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>An expression representing the function call.</returns>
        /// <exception cref="global::System.ArgumentException">The function name is invalid.</exception>
        protected Expression GenerateStaticNonaryFunctionCall<T>(string functionName) =>
            this.GenerateStaticNonaryFunctionCall(typeof(T), functionName);

        /// <summary>
        /// Generates a static function call for a function with no parameters.
        /// </summary>
        /// <param name="t">The type to call the function on.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>An expression representing the function call.</returns>
        /// <exception cref="global::System.ArgumentException">The function name is invalid.</exception>
        protected Expression GenerateStaticNonaryFunctionCall(Type t, string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
            }

            MethodInfo mi = t.GetMethodWithExactParameters(
                functionName,
#if NETSTANDARD2_0
                Array.Empty<Type>())
#else
                new Type[0])
#endif
                ?? throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));

            return Expression.Call(mi);
        }
    }
}