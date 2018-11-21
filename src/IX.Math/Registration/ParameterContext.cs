// <copyright file="ParameterContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq.Expressions;
using IX.StandardExtensions;

namespace IX.Math.Registration
{
    /// <summary>
    /// A data model for a parameter's context of existence.
    /// </summary>
    public class ParameterContext : IDeepCloneable<ParameterContext>, IEquatable<ParameterContext>
    {
        private bool alreadyCompiled;
        private ParameterExpression parameterDefinitionExpression;
        private Expression expression;
        private Expression stringExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterContext"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">name.</exception>
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
        /// <value><see langword="null"/> if the setting is not defined, <see langword="true"/> if this parameter is float; otherwise, <see langword="false"/>.</value>
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
        /// Gets a value indicating whether the parameter is a function.
        /// </summary>
        /// <value><see langword="true"/> if the parameter is a function; otherwise, <see langword="false"/>.</value>
        public bool FuncParameter { get; private set; }

        /// <summary>
        /// Gets or sets the order in which this parameter has appeared in the expression.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets the parameter expression.
        /// </summary>
        /// <value>The parameter expression.</value>
        public ParameterExpression ParameterExpression
        {
            get
            {
                if (!this.alreadyCompiled)
                {
                    this.Compile();
                }

                return this.parameterDefinitionExpression;
            }
        }

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
        /// Determines the parameter to be a function.
        /// </summary>
        public void DetermineFunc() => this.FuncParameter = true;

        /// <summary>
        /// Compiles this instance.
        /// </summary>
        /// <returns>A LINQ expression representing the parameter.</returns>
        /// <exception cref="InvalidOperationException">The parameter was already compiled, but it is <see langword="null"/>.</exception>
        /// <exception cref="IX.Math.ExpressionNotValidLogicallyException">The parameter is still undefined.</exception>
        public Expression Compile()
        {
            if (this.alreadyCompiled)
            {
                return this.expression ?? throw new InvalidOperationException(string.Format(Resources.ParameterAlreadyCompiledButCompilationIsNull, this.Name));
            }

            Type type;

            switch (this.ReturnType)
            {
                case SupportedValueType.Boolean:
                    type = this.FuncParameter ? typeof(Func<bool>) : typeof(bool);
                    break;
                case SupportedValueType.ByteArray:
                    type = this.FuncParameter ? typeof(Func<byte[]>) : typeof(byte[]);
                    break;
                case SupportedValueType.String:
                    type = this.FuncParameter ? typeof(Func<string>) : typeof(string);
                    break;
                case SupportedValueType.Numeric:
                    if (this.IsFloat == false)
                    {
                        type = this.FuncParameter ? typeof(Func<long>) : typeof(long);
                    }
                    else
                    {
                        type = this.FuncParameter ? typeof(Func<double>) : typeof(double);
                    }

                    break;
                default:
                    throw new ExpressionNotValidLogicallyException();
            }

            ParameterExpression expression = Expression.Parameter(type, this.Name);

            this.parameterDefinitionExpression = expression;

            if (this.FuncParameter)
            {
                this.expression = Expression.Invoke(expression);
            }
            else
            {
                this.expression = expression;
            }

            this.stringExpression =
                (this.ReturnType == SupportedValueType.String) ?
                this.expression :
                Expression.Call(this.expression, typeof(object).GetMethodWithExactParameters(
                    nameof(object.ToString),
#if STANDARD2 || NET461
                    Array.Empty<Type>()));
#else
                    new Type[0]));
#endif

            this.alreadyCompiled = true;
            return this.expression;
        }

        /// <summary>
        /// Compiles this instance as a string result.
        /// </summary>
        /// <returns>A LINQ expression representing the parameter, as a string.</returns>
        public Expression CompileString()
        {
            if (this.alreadyCompiled)
            {
                return this.stringExpression ?? throw new InvalidOperationException(string.Format(Resources.ParameterAlreadyCompiledButCompilationIsNull, this.Name));
            }

            this.Compile();

            return this.stringExpression ?? throw new InvalidOperationException(string.Format(Resources.ParameterAlreadyCompiledButCompilationIsNull, this.Name));
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        /// <remarks>
        /// <para>Warning! This method does not copy the compilation result.</para>
        /// <para>When called on a compiled expression, the resulting context will not be itself compiled.</para>
        /// </remarks>
        public ParameterContext DeepClone() => new ParameterContext(this.Name)
        {
            IsFloat = this.IsFloat,
            ReturnType = this.ReturnType,
            FuncParameter = this.FuncParameter,
            Order = this.Order,
        };

        /// <summary>
        /// Indicates whether the current context is equal to another <see cref="ParameterContext"/>.
        /// </summary>
        /// <param name="other">A parameter context to compare with this context.</param>
        /// <returns>true if the current context is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(ParameterContext other)
        {
            if (other == null)
            {
                return false;
            }

            return
                other.IsFloat == this.IsFloat &&
                other.Name == this.Name &&
                other.FuncParameter == this.FuncParameter &&
                other.Order == this.Order &&
                other.ReturnType == this.ReturnType;
        }
    }
}