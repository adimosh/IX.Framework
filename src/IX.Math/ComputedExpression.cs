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

        private readonly string defaultStringFormat;
        private readonly string initialExpression;
        private NodeBase body;
        private bool disposedValue;

        internal ComputedExpression(string initialExpression, NodeBase body, bool isRecognized, IParameterRegistry parameterRegistry, string defaultStringFormat)
        {
            this.parametersRegistry = parameterRegistry;

            this.initialExpression = initialExpression;
            this.body = body;
            this.RecognizedCorrectly = isRecognized;
            this.IsConstant = body?.IsConstant ?? false;
            this.defaultStringFormat = string.IsNullOrWhiteSpace(defaultStringFormat) ? defaultStringFormat : null;
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
                // Expression was not recognized correctly.
                return this.initialExpression;
            }

            var convertedArguments = FormatArgumentsAccordingToParameters(arguments, this.parametersRegistry.Dump());

            object[] FormatArgumentsAccordingToParameters(
                object[] parameterValues,
                ParameterContext[] parameters)
            {
                if (parameterValues.Length != parameters.Length)
                {
                    return null;
                }

                var finalValues = new object[parameterValues.Length];

                var i = 0;

                object paramValue = null;

                while (i < finalValues.Length)
                {
                    ParameterContext paraContext = parameters[i];

                    // If there was no continuation, initialize parameter with value.
                    if (paramValue == null)
                    {
                        paramValue = parameterValues[i];
                    }

                    // Initial filtration
                    switch (paramValue)
                    {
                        case byte convertedParam:
                            paramValue = Convert.ToInt64(convertedParam);
                            continue;

                        case sbyte convertedParam:
                            paramValue = Convert.ToInt64(convertedParam);
                            continue;

                        case short convertedParam:
                            paramValue = Convert.ToInt64(convertedParam);
                            continue;

                        case ushort convertedParam:
                            paramValue = Convert.ToInt64(convertedParam);
                            continue;

                        case int convertedParam:
                            paramValue = Convert.ToInt64(convertedParam);
                            continue;

                        case uint convertedParam:
                            paramValue = Convert.ToInt64(convertedParam);
                            continue;

                        case long convertedParam:
                            paramValue = convertedParam;
                            break;

                        case ulong convertedParam:
                            paramValue = Convert.ToDouble(convertedParam);
                            continue;

                        case float convertedParam:
                            paramValue = Convert.ToDouble(convertedParam);
                            continue;

                        case double convertedParam:
                            paramValue = convertedParam;
                            break;

                        case string convertedParam:
                            paramValue = convertedParam;
                            break;

                        case bool convertedParam:
                            paramValue = convertedParam;
                            break;

                        case byte[] convertedParam:
                            paramValue = convertedParam;
                            break;

                        case Func<byte> convertedParam:
                            paramValue = new Func<long>(() => Convert.ToInt64(convertedParam()));
                            continue;

                        case Func<sbyte> convertedParam:
                            paramValue = new Func<long>(() => Convert.ToInt64(convertedParam()));
                            continue;

                        case Func<short> convertedParam:
                            paramValue = new Func<long>(() => Convert.ToInt64(convertedParam()));
                            continue;

                        case Func<ushort> convertedParam:
                            paramValue = new Func<long>(() => Convert.ToInt64(convertedParam()));
                            continue;

                        case Func<int> convertedParam:
                            paramValue = new Func<long>(() => Convert.ToInt64(convertedParam()));
                            continue;

                        case Func<uint> convertedParam:
                            paramValue = new Func<long>(() => Convert.ToInt64(convertedParam()));
                            continue;

                        case Func<long> convertedParam:
                            paramValue = convertedParam;
                            break;

                        case Func<ulong> convertedParam:
                            paramValue = new Func<double>(() => Convert.ToDouble(convertedParam()));
                            continue;

                        case Func<float> convertedParam:
                            paramValue = new Func<double>(() => Convert.ToDouble(convertedParam()));
                            continue;

                        case Func<double> convertedParam:
                            paramValue = convertedParam;
                            break;

                        case Func<string> convertedParam:
                            paramValue = convertedParam;
                            break;

                        case Func<bool> convertedParam:
                            paramValue = convertedParam;
                            break;

                        case Func<byte[]> convertedParam:
                            paramValue = convertedParam;
                            break;

                        default:
                            // Argument type is not (yet) supported
                            return null;
                    }

                    // Secondary filtration
                    switch (paramValue)
                    {
                        case long convertedParam:
                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValue(paraContext, convertedParam != 0);
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValue(paraContext, BitConverter.GetBytes(convertedParam));
                                    break;

                                case SupportedValueType.Numeric:
                                    {
                                        if (paraContext.IsFloat == true)
                                        {
                                            paramValue = CreateValue(paraContext, Convert.ToDouble(convertedParam));
                                        }
                                        else if (paraContext.IsFloat == false)
                                        {
                                            paramValue = CreateValue(paraContext, convertedParam);
                                        }
                                        else
                                        {
                                            paraContext.DetermineInteger();
                                            continue;
                                        }
                                    }

                                    break;

                                case SupportedValueType.String:
                                    {
                                        if (this.defaultStringFormat == null)
                                        {
                                            paramValue = CreateValue(paraContext, convertedParam.ToString());
                                        }
                                        else
                                        {
                                            paramValue = CreateValue(paraContext, convertedParam.ToString(this.defaultStringFormat));
                                        }
                                    }

                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.Numeric);
                                    continue;
                            }

                            break;

                        case double convertedParam:
                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValue(paraContext, convertedParam != 0);
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValue(paraContext, BitConverter.GetBytes(convertedParam));
                                    break;

                                case SupportedValueType.Numeric:
                                    {
                                        if (paraContext.IsFloat == true)
                                        {
                                            paramValue = CreateValue(paraContext, convertedParam);
                                        }
                                        else if (paraContext.IsFloat == false)
                                        {
                                            paramValue = CreateValue(paraContext, Convert.ToInt64(convertedParam));
                                        }
                                        else
                                        {
                                            paraContext.DetermineFloat();
                                            continue;
                                        }
                                    }

                                    break;

                                case SupportedValueType.String:
                                    {
                                        if (this.defaultStringFormat == null)
                                        {
                                            paramValue = CreateValue(paraContext, convertedParam.ToString());
                                        }
                                        else
                                        {
                                            paramValue = CreateValue(paraContext, convertedParam.ToString(this.defaultStringFormat));
                                        }
                                    }

                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.Numeric);
                                    continue;
                            }

                            break;

                        case bool convertedParam:
                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValue(paraContext, convertedParam);
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValue(paraContext, BitConverter.GetBytes(convertedParam));
                                    break;

                                case SupportedValueType.Numeric:
                                    return null;

                                case SupportedValueType.String:
                                    paramValue = CreateValue(paraContext, convertedParam.ToString());
                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.Boolean);
                                    continue;
                            }

                            break;

                        case string convertedParam:
                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValue(paraContext, bool.Parse(convertedParam));
                                    break;

                                case SupportedValueType.ByteArray:
                                    {
                                        if (ParsingFormatter.ParseByteArray(convertedParam, out var byteArrayResult))
                                        {
                                            paramValue = CreateValue(paraContext, byteArrayResult);
                                        }
                                        else
                                        {
                                            // Cannot parse byte array.
                                            return null;
                                        }
                                    }

                                    break;

                                case SupportedValueType.Numeric:
                                    {
                                        if (ParsingFormatter.ParseNumeric(convertedParam, out var numericResult))
                                        {
                                            if (numericResult is long integerResult)
                                            {
                                                paramValue = CreateValue(paraContext, integerResult);
                                            }
                                            else if (numericResult is double floatResult)
                                            {
                                                paramValue = CreateValue(paraContext, floatResult);
                                            }
                                            else
                                            {
                                                // Numeric type unknown.
                                                return null;
                                            }
                                        }
                                        else
                                        {
                                            // Cannot parse numeric type.
                                            return null;
                                        }
                                    }

                                    break;

                                case SupportedValueType.String:
                                    paramValue = CreateValue(paraContext, convertedParam);
                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.String);
                                    continue;
                            }

                            break;

                        case byte[] convertedParam:
                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValue(paraContext, BitConverter.ToBoolean(convertedParam, 0));
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValue(paraContext, convertedParam);
                                    break;

                                case SupportedValueType.Numeric:
                                    if (paraContext.IsFloat == true)
                                    {
                                        paramValue = CreateValue(paraContext, BitConverter.ToDouble(convertedParam, 0));
                                    }
                                    else if (paraContext.IsFloat == false)
                                    {
                                        paramValue = CreateValue(paraContext, BitConverter.ToInt64(convertedParam, 0));
                                    }
                                    else
                                    {
                                        paraContext.DetermineFloat();
                                        continue;
                                    }

                                    break;

                                case SupportedValueType.String:
                                    paramValue = CreateValue(paraContext, BitConverter.ToString(convertedParam));
                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.ByteArray);
                                    continue;
                            }

                            break;

                        case Func<long> convertedParam:
                            paraContext.DetermineFunc();

                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValueFromFunc(paraContext, () => convertedParam() == 0);
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValueFromFunc(paraContext, () => BitConverter.GetBytes(convertedParam()));
                                    break;

                                case SupportedValueType.Numeric:
                                    if (paraContext.IsFloat == true)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => Convert.ToDouble(convertedParam()));
                                    }
                                    else if (paraContext.IsFloat == false)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, convertedParam);
                                    }
                                    else
                                    {
                                        paraContext.DetermineInteger();
                                        continue;
                                    }

                                    break;

                                case SupportedValueType.String:
                                    if (this.defaultStringFormat == null)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => convertedParam().ToString());
                                    }
                                    else
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => convertedParam().ToString(this.defaultStringFormat));
                                    }

                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.Numeric);
                                    continue;
                            }

                            break;

                        case Func<double> convertedParam:
                            paraContext.DetermineFunc();

                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValueFromFunc(paraContext, () => convertedParam() == 0);
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValueFromFunc(paraContext, () => BitConverter.GetBytes(convertedParam()));
                                    break;

                                case SupportedValueType.Numeric:
                                    if (paraContext.IsFloat == true)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, convertedParam);
                                    }
                                    else if (paraContext.IsFloat == false)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => Convert.ToInt64(convertedParam()));
                                    }
                                    else
                                    {
                                        paraContext.DetermineFloat();
                                        continue;
                                    }

                                    break;

                                case SupportedValueType.String:
                                    if (this.defaultStringFormat == null)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => convertedParam().ToString());
                                    }
                                    else
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => convertedParam().ToString(this.defaultStringFormat));
                                    }

                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.Numeric);
                                    continue;
                            }

                            break;

                        case Func<bool> convertedParam:
                            paraContext.DetermineFunc();

                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValueFromFunc(paraContext, convertedParam);
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValueFromFunc(paraContext, () => BitConverter.GetBytes(convertedParam()));
                                    break;

                                case SupportedValueType.Numeric:
                                    return null;

                                case SupportedValueType.String:
                                    paramValue = CreateValueFromFunc(paraContext, () => convertedParam().ToString());
                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.Boolean);
                                    continue;
                            }

                            break;

                        case Func<string> convertedParam:
                            paraContext.DetermineFunc();

                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValueFromFunc(paraContext, () => bool.Parse(convertedParam()));
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValueFromFunc(paraContext, () =>
                                    {
                                        if (ParsingFormatter.ParseByteArray(convertedParam(), out var baResult))
                                        {
                                            return baResult;
                                        }
                                        else
                                        {
                                            return null;
                                        }
                                    });

                                    break;

                                case SupportedValueType.Numeric:
                                    if (paraContext.IsFloat == true)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () =>
                                        {
                                            var str = convertedParam();
                                            if (ParsingFormatter.ParseNumeric(str, out var numericResult))
                                            {
                                                return Convert.ToDouble(numericResult);
                                            }
                                            else
                                            {
                                                throw new ArgumentInvalidTypeException(paraContext.Name);
                                            }
                                        });
                                    }
                                    else if (paraContext.IsFloat == false)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () =>
                                        {
                                            var str = convertedParam();
                                            if (ParsingFormatter.ParseNumeric(str, out var numericResult))
                                            {
                                                return Convert.ToInt64(numericResult);
                                            }
                                            else
                                            {
                                                throw new ArgumentInvalidTypeException(paraContext.Name);
                                            }
                                        });
                                    }
                                    else
                                    {
                                        paraContext.DetermineFloat();
                                        continue;
                                    }

                                    break;

                                case SupportedValueType.String:
                                    paramValue = CreateValueFromFunc(paraContext, convertedParam);
                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.String);
                                    continue;
                            }

                            break;

                        case Func<byte[]> convertedParam:
                            paraContext.DetermineFunc();

                            switch (paraContext.ReturnType)
                            {
                                case SupportedValueType.Boolean:
                                    paramValue = CreateValueFromFunc(paraContext, () => BitConverter.ToBoolean(convertedParam(), 0));
                                    break;

                                case SupportedValueType.ByteArray:
                                    paramValue = CreateValueFromFunc(paraContext, convertedParam);
                                    break;

                                case SupportedValueType.Numeric:
                                    if (paraContext.IsFloat == true)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => BitConverter.ToDouble(convertedParam(), 0));
                                    }
                                    else if (paraContext.IsFloat == false)
                                    {
                                        paramValue = CreateValueFromFunc(paraContext, () => BitConverter.ToInt64(convertedParam(), 0));
                                    }
                                    else
                                    {
                                        paraContext.DetermineFloat();
                                        continue;
                                    }

                                    break;

                                case SupportedValueType.String:
                                    paramValue = CreateValueFromFunc(paraContext, () => BitConverter.ToString(convertedParam()));
                                    break;

                                case SupportedValueType.Unknown:
                                    paraContext.DetermineType(SupportedValueType.ByteArray);
                                    continue;
                            }

                            break;

                        default:
                            // Argument type is not (yet) supported
                            return null;
                    }

                    object CreateValue<T>(ParameterContext parameterContext, T value)
                    {
                        if (parameterContext.FuncParameter)
                        {
                            return new Func<T>(() => value);
                        }
                        else
                        {
                            return value;
                        }
                    }

                    object CreateValueFromFunc<T>(ParameterContext parameterContext, Func<T> value)
                    {
                        if (parameterContext.FuncParameter)
                        {
                            return value;
                        }
                        else
                        {
                            return value();
                        }
                    }

                    if (paramValue == null)
                    {
                        return null;
                    }

                    finalValues[i] = paramValue;

                    paramValue = null;

                    i++;
                }

                return finalValues;
            }

            if (convertedArguments == null)
            {
                // The arguments could not be correctly converted.
                return this.initialExpression;
            }

            Delegate del;
            try
            {
                LambdaExpression lambda = Expression.Lambda(
                    this.body.GenerateExpression(),
                    this.parametersRegistry.Dump().Select(p => p.ParameterExpression));

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
                // Delegate could not be compiled with the given arguments.
                return this.initialExpression;
            }

            try
            {
                return del.DynamicInvoke(convertedArguments);
            }
            catch
            {
                // Dynamic invocation of generated expression failed.
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
                if (!dataFinder.TryGetData(p.Name, out var data))
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

            return new ComputedExpression(this.initialExpression, this.body.DeepClone(context), this.RecognizedCorrectly, registry, this.defaultStringFormat);
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