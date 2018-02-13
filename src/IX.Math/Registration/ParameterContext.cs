// <copyright file="ParameterContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using IX.Math.PlatformMitigation;
using IX.StandardExtensions;

namespace IX.Math.Registration
{
    /// <summary>
    /// A data model for a parameter's context of existence.
    /// </summary>
    public class ParameterContext : IDeepCloneable<ParameterContext>
    {
        private bool alreadyCompiled;
        private ParameterExpression expression;
        private Expression stringExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterContext"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">name</exception>
        public ParameterContext(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <value>The name of the parameter.</value>
        public string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this parameter is float.
        /// </summary>
        /// <value><c>null</c> if the setting is not defined, <c>true</c> if this parameter is float; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// <para>This setting only has an effect if the parameter is already numeric.</para>
        /// <para>Otherwise, it will determine the parameter to be a float or an integer only if switched to numeric.</para>
        /// <para>This setting is usable independent to the parameter type definition in order to maintain backwards compatibility with the UndefinedParameterNode obsolete class.</para>
        /// </remarks>
        public bool? IsFloat { get; private set; }

        /// <summary>
        /// Gets the return type of the parameter.
        /// </summary>
        /// <value>The return type of the parameter.</value>
        public SupportedValueType ReturnType { get; private set; }

        /// <summary>
        /// Determines the type of the parameter.
        /// </summary>
        /// <param name="newType">The new type.</param>
        /// <exception cref="ArgumentException">The new type is not valid.</exception>
        public void DetermineType(SupportedValueType newType)
        {
            switch (newType)
            {
                case SupportedValueType.Boolean:
                    ChangeReturnType(SupportedValueType.Boolean);

                    return;

                case SupportedValueType.ByteArray:
                    ChangeReturnType(SupportedValueType.ByteArray);

                    return;

                case SupportedValueType.String:
                    ChangeReturnType(SupportedValueType.String);

                    return;

                case SupportedValueType.Numeric:
                    ChangeReturnType(SupportedValueType.Numeric);

                    return;

                default:
                    throw new ArgumentException(string.Format(Resources.ParameterTypeNotRecognized, this.Name));
            }

            void ChangeReturnType(SupportedValueType newReturnType)
            {
                if (this.ReturnType == newReturnType)
                {
                    return;
                }
                else if (this.ReturnType == SupportedValueType.Unknown)
                {
                    if (this.alreadyCompiled)
                    {
                        throw new InvalidOperationException(string.Format(Resources.ParameterAlreadyCompiled, this.Name));
                    }

                    this.ReturnType = newReturnType;
                }
                else
                {
                    throw new ExpressionNotValidLogicallyException(string.Format(Resources.ParameterRequiredOfMismatchedTypes, this.Name));
                }
            }
        }

        /// <summary>
        /// Determines that, if this parameter is going to be numeric, it should be a float.
        /// </summary>
        public void DetermineFloat()
        {
            if (this.IsFloat == true)
            {
                return;
            }
            else if (this.IsFloat == null)
            {
                this.IsFloat = true;
            }
            else
            {
                throw new ExpressionNotValidLogicallyException(string.Format(Resources.ParameterMustBeFloatButAlreadyRequiredToBeInteger, this.Name));
            }
        }

        /// <summary>
        /// Determines that, if this parameter is going to be numeric, it should be an integer.
        /// </summary>
        public void DetermineInteger()
        {
            if (this.IsFloat == false)
            {
                return;
            }
            else if (this.IsFloat == null)
            {
                this.IsFloat = false;
            }
            else
            {
                throw new ExpressionNotValidLogicallyException(string.Format(Resources.ParameterMustBeIntegerButAlreadyRequiredToBeFloat, this.Name));
            }
        }

        /// <summary>
        /// Compiles this instance.
        /// </summary>
        /// <returns>A LINQ expression representing the parameter.</returns>
        /// <exception cref="InvalidOperationException">The parameter was already compiled, but it is <c>null</c>.</exception>
        /// <exception cref="IX.Math.ExpressionNotValidLogicallyException">The parameter is still undefined.</exception>
        public ParameterExpression Compile()
        {
            if (this.alreadyCompiled)
            {
                return this.expression ?? throw new InvalidOperationException();
            }

            Type type;

            switch (this.ReturnType)
            {
                case SupportedValueType.Boolean:
                    type = typeof(bool);
                    break;
                case SupportedValueType.ByteArray:
                    type = typeof(byte[]);
                    break;
                case SupportedValueType.String:
                    type = typeof(string);
                    break;
                case SupportedValueType.Numeric:
                    if (this.IsFloat == false)
                    {
                        type = typeof(long);
                    }
                    else
                    {
                        type = typeof(double);
                    }

                    break;
                default:
                    throw new ExpressionNotValidLogicallyException();
            }

            ParameterExpression expression = Expression.Parameter(type, this.Name);

            this.expression = expression;

            this.stringExpression =
                (this.ReturnType == SupportedValueType.String) ?
                (Expression)expression :
                Expression.Call(expression, typeof(object).GetTypeMethod(
                    nameof(object.ToString),
#if NETSTANDARD2_0
                    Array.Empty<Type>()));
#else
                    new Type[0]));
#endif

            this.alreadyCompiled = true;
            return expression;
        }

        /// <summary>
        /// Compiles this instance as a string result.
        /// </summary>
        /// <returns>A LINQ expression representing the parameter, as a string.</returns>
        public Expression CompileString()
        {
            if (this.alreadyCompiled)
            {
                return this.stringExpression ?? throw new InvalidOperationException();
            }

            this.Compile();

            return this.stringExpression ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        /// <remarks>
        /// <para>Warning!</para>
        /// <para>This method does not copy the compilation result!</para>
        /// <para>When called on a compiled expression, the resulting context will not be itself compiled.</para>
        /// </remarks>
        public ParameterContext DeepClone() => new ParameterContext(this.Name)
        {
            IsFloat = this.IsFloat,
            ReturnType = this.ReturnType,
        };
    }
}