// <copyright file="ComputedExpression.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IX.Math.Formatters;
using IX.Math.Nodes;
using IX.Math.Registration;
using IX.StandardExtensions;

namespace IX.Math
{
    /// <summary>
    /// A representation of a computed expression, resulting from a string expression.
    /// </summary>
    public class ComputedExpression : IDeepCloneable<ComputedExpression>, IDisposable
    {
        private readonly IParameterRegistry parametersRegistry;

        private readonly string initialExpression;
        private NodeBase body;
        private bool disposedValue;

        internal ComputedExpression(in string initialExpression, in NodeBase body, in bool isRecognized, in IParameterRegistry parameterRegistry)
        {
            this.parametersRegistry = parameterRegistry;

            this.initialExpression = initialExpression;
            this.body = body;
            this.RecognizedCorrectly = isRecognized;
            this.IsConstant = body?.IsConstant ?? false;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ComputedExpression"/> class.
        /// </summary>
        ~ComputedExpression()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether or not the expression was actually recognized. <c>true</c> can possibly return an actual expression or a static value.
        /// </summary>
        /// <value><c>true</c> if the expression is recognized correctly, <c>false</c> otherwise.</value>
        public bool RecognizedCorrectly { get; }

        /// <summary>
        /// Gets a value indicating whether or not the expression is constant.
        /// </summary>
        /// <value><c>true</c> if the expression is constant, <c>false</c> otherwise.</value>
        public bool IsConstant { get; }

        /// <summary>
        /// Gets a value indicating whether or not the expression has undefined parameters.
        /// </summary>
        /// <value><c>true</c> if the expression has undefined parameters, <c>false</c> otherwise.</value>
        public bool HasUndefinedParameters => this.parametersRegistry.Dump().Any(p => p.ReturnType == SupportedValueType.Unknown);

        /// <summary>
        /// Gets the names of the parameters this expression requires, if any.
        /// </summary>
        public string[] ParameterNames => this.parametersRegistry.Dump().Select(p => p.Name).ToArray();

        /// <summary>
        /// Disposes an instance of the <see cref="ComputedExpression"/> class.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Computes the expression and returns a result.
        /// </summary>
        /// <param name="arguments">The arguments with which to invoke the execution of the expression.</param>
        /// <returns>The computed result, or, if the expression is not recognized correctly, the expression as a <see cref="string"/>.</returns>
        public object Compute(params object[] arguments)
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(nameof(ComputedExpression));
            }

            if (!this.RecognizedCorrectly)
            {
                return this.initialExpression;
            }

            object[] convertedArguments = FormatArgumentsAccordingToParameters(arguments, this.parametersRegistry.Dump());

            object[] FormatArgumentsAccordingToParameters(
                in object[] parameterValues,
                in ParameterContext[] parameters)
            {
                if (parameterValues.Length != parameterValues.Length)
                {
                    throw new InvalidOperationException();
                }

                object[] finalValues = new object[parameterValues.Length];

                var i = 0;

                while (i < finalValues.Length)
                {
                    ParameterContext para = parameters[i];
                    switch (parameters[i].ReturnType)
                    {
                        case SupportedValueType.Numeric:
                            switch (parameterValues[i])
                            {
                                case string se:
                                    if (ParsingFormatter.ParseNumeric(se, out object val))
                                    {
                                        if (para.IsFloat == false)
                                        {
                                            finalValues[i] = Convert.ToInt64(val);
                                        }
                                        else
                                        {
                                            finalValues[i] = Convert.ToDouble(val);
                                        }
                                    }
                                    else
                                    {
                                        throw new InvalidCastException();
                                    }

                                    break;
                                default:
                                    if (para.IsFloat == false)
                                    {
                                        finalValues[i] = Convert.ToInt64(parameterValues[i]);
                                    }
                                    else
                                    {
                                        finalValues[i] = Convert.ToDouble(parameterValues[i]);
                                    }

                                    break;
                            }

                            break;
                        case SupportedValueType.String:
                            finalValues[i] = parameterValues[i].ToString();
                            break;
                        case SupportedValueType.Boolean:
                            switch (parameterValues[i])
                            {
                                case bool be:
                                    finalValues[i] = be;
                                    break;
                                case byte bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case sbyte bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case short bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case char bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case ushort bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case int bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case uint bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case long bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case ulong bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case float bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case double bbe:
                                    finalValues[i] = bbe != 0;
                                    break;
                                case string se:
                                    if (bool.TryParse(se, out bool bbb3))
                                    {
                                        finalValues[i] = bbb3;
                                    }
                                    else
                                    {
                                        throw new InvalidCastException();
                                    }

                                    break;
                                default:
                                    throw new InvalidCastException();
                            }

                            break;
                        case SupportedValueType.ByteArray:
                            switch (parameterValues[i])
                            {
                                case bool be:
                                    finalValues[i] = BitConverter.GetBytes(be);
                                    break;
                                case byte bbe:
                                    finalValues[i] = new byte[1] { bbe };
                                    break;
                                case sbyte bbe:
                                    finalValues[i] = new byte[1] { (byte)bbe };
                                    break;
                                case short bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case char bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case ushort bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case int bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case uint bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case long bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case ulong bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case float bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case double bbe:
                                    finalValues[i] = BitConverter.GetBytes(bbe);
                                    break;
                                case byte[] ba:
                                    finalValues[i] = ba;
                                    break;
                                case string se:
                                    if (ParsingFormatter.ParseByteArray(se, out byte[] val))
                                    {
                                        finalValues[i] = val;
                                    }
                                    else
                                    {
                                        throw new InvalidCastException();
                                    }

                                    break;
                                default:
                                    throw new InvalidCastException();
                            }

                            break;
                        case SupportedValueType.Unknown:
                            switch (parameterValues[i])
                            {
                                case bool be:
                                    para.DetermineType(SupportedValueType.Boolean);
                                    break;
                                case byte bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case sbyte bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case short bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case char bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case ushort bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case int bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case uint bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case long bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case ulong bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case float bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case double bbe:
                                    para.DetermineType(SupportedValueType.Numeric);
                                    break;
                                case byte[] ba:
                                    para.DetermineType(SupportedValueType.ByteArray);
                                    break;
                                default:
                                    para.DetermineType(SupportedValueType.String);
                                    break;
                            }

                            continue;
                        default:
                            throw new InvalidCastException();
                    }

                    i++;
                }

                return finalValues;
            }

            Delegate del;
            try
            {
                LambdaExpression lambda = Expression.Lambda(
                    this.body.GenerateExpression(),
                    this.parametersRegistry.Dump().Select(p => p.Compile()));

                if (lambda == null)
                {
                    del = null;
                }
                else
                {
                    del = lambda?.Compile();
                }
            }
            catch
            {
                del = null;
            }

            if (del == null)
            {
                return this.initialExpression;
            }

            try
            {
                return del.DynamicInvoke(convertedArguments);
            }
            catch
            {
                return this.initialExpression;
            }
        }

        /// <summary>
        /// Computes the expression and returns a result.
        /// </summary>
        /// <param name="dataFinder">The data finder for the arguments with which to invoke execution of the expression.</param>
        /// <returns>The computed result, or, if the expression is not recognized correctly, the expression as a <see cref="string"/>.</returns>
        public object Compute(IDataFinder dataFinder)
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(nameof(ComputedExpression));
            }

            if (!this.RecognizedCorrectly)
            {
                return this.initialExpression;
            }

            var pars = new List<object>();

            foreach (ParameterContext p in this.parametersRegistry.Dump())
            {
                if (!dataFinder.TryGetData(p.Name, out object data))
                {
                    data = null;
                }

                pars.Add(data);
            }

            if (pars.Any(p => p == null))
            {
                return this.initialExpression;
            }

            return this.Compute(pars.ToArray());
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public ComputedExpression DeepClone()
        {
            var registry = new StandardParameterRegistry();
            var context = new NodeCloningContext { ParameterRegistry = registry };

            this.parametersRegistry.Dump().ForEach(p => registry.CloneFrom(p));

            return new ComputedExpression(this.initialExpression, this.body.DeepClone(context), this.RecognizedCorrectly, registry);
        }

        /// <summary>
        /// Disposes an instance of the <see cref="ComputedExpression"/> class.
        /// </summary>
        /// <param name="disposing">Indicates whether or not disposal is a result of a normal dispose usage.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                this.body = null;

                this.disposedValue = true;
            }
        }
    }
}