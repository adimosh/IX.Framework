// <copyright file="OneDataSetComputedExpressionUnitTest.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.Math;
using IX.StandardExtensions.TestUtils;
using Moq;
using Xunit;

namespace IX.UnitTests.IX.Math
{
    /// <summary>
    /// Tests computed expressions.
    /// </summary>
    public class OneDataSetComputedExpressionUnitTest : IClassFixture<CachedExpressionProviderFixture>
    {
        private readonly CachedExpressionProviderFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneDataSetComputedExpressionUnitTest"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public OneDataSetComputedExpressionUnitTest(CachedExpressionProviderFixture fixture)
        {
            this.fixture = fixture;
        }

        /// <summary>
        /// Provides the data for theory.
        /// </summary>
        /// <returns>Theory data.</returns>
        public static object[][] ProvideDataForTheory() => new object[][]
            {
                new object[]
                {
                    "abs((1-17)+3) + abs(14-(1*4))",
                    null,
                    23L,
                },
            };

        /// <summary>
        /// Tests the computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "EPSPara")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithParameters(string expression, Dictionary<string, object> parameters, object expectedResult)
        {
            using (var service = new ExpressionParsingService())
            {
                using (ComputedExpression del = service.Interpret(expression))
                {
                    if (del == null)
                    {
                        throw new InvalidOperationException("No computed expression was generated!");
                    }

                    var result = del.Compute(parameters?.Values?.ToArray() ?? new object[0]);

                    Assert.Equal(expectedResult, result);
                }
            }
        }

        /// <summary>
        /// Tests a computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "EPSFindr")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithFinder(string expression, Dictionary<string, object> parameters, object expectedResult)
        {
            using (var service = new ExpressionParsingService())
            {
                var finder = new Mock<IDataFinder>(MockBehavior.Loose);

                using (ComputedExpression del = service.Interpret(expression))
                {
                    if (del == null)
                    {
                        throw new InvalidOperationException("No computed expression was generated!");
                    }

                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            var key = parameter.Key;
                            var value = parameter.Value;
                            finder.Setup(p => p.TryGetData(key, out value)).Returns(true);
                        }
                    }

                    var result = del.Compute(finder.Object);

                    Assert.Equal(expectedResult, result);
                }
            }
        }

        /// <summary>
        /// Tests the cached computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "CEPSPara")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithParameters(string expression, Dictionary<string, object> parameters, object expectedResult)
        {
            ComputedExpression del = this.fixture.Service.Interpret(expression);
            if (del == null)
            {
                throw new InvalidOperationException("No computed expression was generated!");
            }

            var result = del.Compute(parameters?.Values?.ToArray() ?? new object[0]);

            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Tests a cached computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "CEPSFindr")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithFinder(string expression, Dictionary<string, object> parameters, object expectedResult)
        {
            var finder = new Mock<IDataFinder>(MockBehavior.Loose);

            ComputedExpression del = this.fixture.Service.Interpret(expression);
            if (del == null)
            {
                throw new InvalidOperationException("No computed expression was generated!");
            }

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    var key = parameter.Key;
                    var value = parameter.Value;
                    finder.Setup(p => p.TryGetData(key, out value)).Returns(true);
                }
            }

            var result = del.Compute(finder.Object);

            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Tests a computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "EPSFindrFunc")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithFunctionFinder(string expression, Dictionary<string, object> parameters, object expectedResult)
        {
            using (var service = new ExpressionParsingService())
            {
                var finder = new Mock<IDataFinder>(MockBehavior.Loose);

                using (ComputedExpression del = service.Interpret(expression))
                {
                    if (del == null)
                    {
                        throw new InvalidOperationException("No computed expression was generated!");
                    }

                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            var key = parameter.Key;
                            var value = this.GenerateFuncOutOfParameterValue(parameter.Value);
                            finder.Setup(p => p.TryGetData(key, out value)).Returns(true);
                        }
                    }

                    var result = del.Compute(finder.Object);

                    Assert.Equal(expectedResult, result);
                }
            }
        }

        /// <summary>
        /// Tests a cached computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "CEPSFindrFunc")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithFunctionFinder(string expression, Dictionary<string, object> parameters, object expectedResult)
        {
            var finder = new Mock<IDataFinder>(MockBehavior.Loose);

            ComputedExpression del = this.fixture.Service.Interpret(expression);
            if (del == null)
            {
                throw new InvalidOperationException("No computed expression was generated!");
            }

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    var key = parameter.Key;
                    var value = this.GenerateFuncOutOfParameterValue(parameter.Value);
                    finder.Setup(p => p.TryGetData(key, out value)).Returns(true);
                }
            }

            var result = del.Compute(finder.Object);

            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Tests a cached computed expression with finder returning functions repeatedly.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "CEPSFindrFuncRepeated")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithFunctionFinderRepeated(string expression, Dictionary<string, object> parameters, object expectedResult)
        {
            var indexLimit = DataGenerator.RandomInteger(3, 5);
            for (var index = 0; index < indexLimit; index++)
            {
                var finder = new Mock<IDataFinder>(MockBehavior.Loose);

                ComputedExpression del = this.fixture.Service.Interpret(expression);
                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        var key = parameter.Key;
                        var value = this.GenerateFuncOutOfParameterValue(parameter.Value);
                        finder.Setup(p => p.TryGetData(key, out value)).Returns(true);
                    }
                }

                var result = del.Compute(finder.Object);

                Assert.Equal(expectedResult, result);
            }
        }

        private object GenerateFuncOutOfParameterValue(object tempParameter)
        {
            switch (tempParameter)
            {
                case byte convertedValue:
                    return new Func<byte>(() => convertedValue);
                case sbyte convertedValue:
                    return new Func<sbyte>(() => convertedValue);
                case short convertedValue:
                    return new Func<short>(() => convertedValue);
                case ushort convertedValue:
                    return new Func<ushort>(() => convertedValue);
                case int convertedValue:
                    return new Func<int>(() => convertedValue);
                case uint convertedValue:
                    return new Func<uint>(() => convertedValue);
                case long convertedValue:
                    return new Func<long>(() => convertedValue);
                case ulong convertedValue:
                    return new Func<ulong>(() => convertedValue);
                case float convertedValue:
                    return new Func<float>(() => convertedValue);
                case double convertedValue:
                    return new Func<double>(() => convertedValue);
                case byte[] convertedValue:
                    return new Func<byte[]>(() => convertedValue);
                case string convertedValue:
                    return new Func<string>(() => convertedValue);
                case bool convertedValue:
                    return new Func<bool>(() => convertedValue);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}