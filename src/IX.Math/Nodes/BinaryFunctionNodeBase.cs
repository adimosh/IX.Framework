// <copyright file="BinaryFunctionNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using System.Reflection;
using IX.StandardExtensions;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for a function that takes two parameters.
    /// </summary>
    /// <seealso cref="FunctionNodeBase" />
    public abstract class BinaryFunctionNodeBase : FunctionNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFunctionNodeBase"/> class.
        /// </summary>
        /// <param name="firstParameter">The first parameter.</param>
        /// <param name="secondParameter">The second parameter.</param>
        /// <exception cref="global::System.ArgumentNullException">
        /// <paramref name="firstParameter"/>
        /// or
        /// <paramref name="secondParameter"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        protected BinaryFunctionNodeBase(NodeBase firstParameter, NodeBase secondParameter)
        {
            NodeBase firstParameterTemp = firstParameter ?? throw new ArgumentNullException(nameof(firstParameter));
            NodeBase secondParameterTemp = secondParameter ?? throw new ArgumentNullException(nameof(secondParameter));

            this.EnsureCompatibleParameters(ref firstParameter, ref secondParameter);

            this.FirstParameter = firstParameterTemp.Simplify();
            this.SecondParameter = secondParameterTemp.Simplify();
        }

        /// <summary>
        /// Gets or sets the first parameter.
        /// </summary>
        /// <value>The first parameter.</value>
        public NodeBase FirstParameter { get; protected set; }

        /// <summary>
        /// Gets or sets the second parameter.
        /// </summary>
        /// <value>The second parameter.</value>
        public NodeBase SecondParameter { get; protected set; }

        /// <summary>
        /// Ensures that the parameters that are received are compatible with the function, optionally allowing the parameter references to change.
        /// </summary>
        /// <param name="firstParameter">The first parameter.</param>
        /// <param name="secondParameter">The second parameter.</param>
        protected abstract void EnsureCompatibleParameters(ref NodeBase firstParameter, ref NodeBase secondParameter);

        /// <summary>
        /// Generates a static binary function call expression.
        /// </summary>
        /// <typeparam name="T">The type to call on.</typeparam>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>An expression representing the call.</returns>
        protected Expression GenerateStaticBinaryFunctionCall<T>(string functionName) => this.GenerateStaticBinaryFunctionCall(typeof(T), functionName);

        /// <summary>
        /// Generates a static binary function call expression.
        /// </summary>
        /// <param name="t">The type to call on.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>Expression.</returns>
        /// <exception cref="global::System.ArgumentException">The function name is invalid.</exception>
        protected Expression GenerateStaticBinaryFunctionCall(Type t, string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
            }

            Type firstParameterType = ParameterTypeFromParameter(this.FirstParameter);
            Type secondParameterType = ParameterTypeFromParameter(this.SecondParameter);

            MethodInfo mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType);

            if (mi == null)
            {
                firstParameterType = typeof(double);
                secondParameterType = typeof(double);

                mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType);

                if (mi == null)
                {
                    firstParameterType = typeof(long);
                    secondParameterType = typeof(long);

                    mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType);

                    if (mi == null)
                    {
                        firstParameterType = typeof(int);
                        secondParameterType = typeof(int);

                        mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType) ??
                            throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
                    }
                }
            }

            Expression e1 = this.FirstParameter.GenerateExpression();
            Expression e2 = this.SecondParameter.GenerateExpression();

            if (e1.Type != firstParameterType)
            {
                e1 = Expression.Convert(e1, firstParameterType);
            }

            if (e2.Type != secondParameterType)
            {
                e2 = Expression.Convert(e2, secondParameterType);
            }

            return Expression.Call(mi, e1, e2);
        }

        /// <summary>
        /// Generates a static binary function call expression.
        /// </summary>
        /// <typeparam name="T">The type to call on.</typeparam>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>An expression representing the call.</returns>
        protected Expression GenerateBinaryFunctionCallFirstParameterInstance<T>(string functionName) => this.GenerateBinaryFunctionCallFirstParameterInstance(typeof(T), functionName);

        /// <summary>
        /// Generates a static binary function call expression.
        /// </summary>
        /// <param name="t">The type to call on.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>Expression.</returns>
        /// <exception cref="global::System.ArgumentException">The function name is invalid.</exception>
        protected Expression GenerateBinaryFunctionCallFirstParameterInstance(Type t, string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
            }

            Type firstParameterType = ParameterTypeFromParameter(this.FirstParameter);
            Type secondParameterType = ParameterTypeFromParameter(this.SecondParameter);

            MethodInfo mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType);

            if (mi == null)
            {
                if ((firstParameterType == typeof(long) && secondParameterType == typeof(double)) ||
                    (firstParameterType == typeof(double) && secondParameterType == typeof(long)))
                {
                    firstParameterType = typeof(double);
                    secondParameterType = typeof(double);

                    mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType);

                    if (mi == null)
                    {
                        firstParameterType = typeof(long);
                        secondParameterType = typeof(long);

                        mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType);

                        if (mi == null)
                        {
                            firstParameterType = typeof(int);
                            secondParameterType = typeof(int);

                            mi = t.GetMethodWithExactParameters(functionName, firstParameterType, secondParameterType) ??
                                throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
                        }
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(Resources.FunctionCouldNotBeFound, functionName), nameof(functionName));
                }
            }

            Expression e1 = this.FirstParameter.GenerateExpression();
            Expression e2 = this.SecondParameter.GenerateExpression();

            if (e1.Type != firstParameterType)
            {
                e1 = Expression.Convert(e1, firstParameterType);
            }

            if (e2.Type != secondParameterType)
            {
                e2 = Expression.Convert(e2, secondParameterType);
            }

            return Expression.Call(e1, mi, e2);
        }
    }
}