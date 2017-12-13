// <copyright file="BasicExpressionsTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.StandardExtensions.TestUtils;
using Xunit;

namespace IX.Math.UnitTests
{
    /// <summary>
    /// Tests for basic expressions.
    /// </summary>
    public class BasicExpressionsTests
    {
        /// <summary>
        /// Provides the data for theory.
        /// </summary>
        /// <returns>System.Object[][].</returns>
        public static object[][] ProvideDataForTheory()
        {
            var tests = new List<object[]>();

            #region STAGE 1: Positive integers

            // +
            {
                var limit = int.MaxValue / 2;
                double leftOperand = DataGenerator.RandomNonNegativeInteger(limit);
                double rightOperand = DataGenerator.RandomNonNegativeInteger(limit);
                var expectedResult = leftOperand + rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}+{rightOperand}",
                        new object[0],
                        (long)expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand+rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand+{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}+rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
            }

            // -
            {
                double leftOperand = DataGenerator.RandomNonNegativeInteger(int.MaxValue);
                double rightOperand = DataGenerator.RandomNonNegativeInteger(int.MaxValue);
                var expectedResult = leftOperand - rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}-{rightOperand}",
                        new object[0],
                        (long)expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand-rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}-rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand-{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
            }

            // *
            {
                var limit = (int)System.Math.Sqrt(int.MaxValue);
                double leftOperand = DataGenerator.RandomNonNegativeInteger(limit);
                double rightOperand = DataGenerator.RandomNonNegativeInteger(limit);
                var expectedResult = leftOperand * rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}*{rightOperand}",
                        new object[0],
                        (long)expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand*rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}*rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand*{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
            }

            // /
            {
                double leftOperand = DataGenerator.RandomNonNegativeInteger(int.MaxValue);
                double rightOperand = DataGenerator.RandomNonNegativeInteger(int.MaxValue);
                var expectedResult = leftOperand / rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}/{rightOperand}",
                        new object[0],
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand/rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand/{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}/rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
            }
            #endregion

            #region STAGE 1: Negative integers

            // +
            {
                var limit = int.MinValue / 2;
                double leftOperand = DataGenerator.RandomInteger(limit, 1);
                double rightOperand = DataGenerator.RandomInteger(limit, 1);
                var expectedResult = leftOperand + rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}+{rightOperand}",
                        new object[0],
                        (long)expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand+rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand+{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}+rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
            }

            // -
            {
                double leftOperand = DataGenerator.RandomInteger(int.MinValue, 1);
                double rightOperand = DataGenerator.RandomInteger(int.MinValue, 1);
                var expectedResult = leftOperand - rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}-{rightOperand}",
                        new object[0],
                        (long)expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand-rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}-rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand-{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
            }

            // *
            {
                var limit = 0 - (int)System.Math.Sqrt(int.MaxValue);
                double leftOperand = DataGenerator.RandomInteger(limit, 1);
                double rightOperand = DataGenerator.RandomInteger(limit, 1);
                var expectedResult = leftOperand * rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}*{rightOperand}",
                        new object[0],
                        (long)expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand*rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}*rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand*{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
            }

            // /
            {
                double leftOperand = DataGenerator.RandomInteger(int.MinValue, 1);
                double rightOperand = DataGenerator.RandomInteger(int.MinValue, 1);
                var expectedResult = leftOperand / rightOperand;

                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}/{rightOperand}",
                        new object[0],
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        "leftOperand/rightOperand",
                        new object[2] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand/{rightOperand}",
                        new object[1] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}/rightOperand",
                        new object[1] { rightOperand },
                        expectedResult,
                    });
            }
            #endregion

            return tests.ToArray();
        }

        /// <summary>
        /// Tests computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// or
        /// </exception>
        [Theory(DisplayName = "Basic expressions with random data")]
        [MemberData(nameof(ProvideDataForTheory), DisableDiscoveryEnumeration = true)]
        public void ComputedExpressionWithParameters(string expression, object[] parameters, object expectedResult)
        {
            using (var service = new ExpressionParsingService())
            {
                ComputedExpression del;
                try
                {
                    del = service.Interpret(expression);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"The generation process should not have thrown an exception, but it threw {ex.GetType()} with message \"{ex.Message}\".");
                }

                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                object result;
                try
                {
                    result = del.Compute(parameters);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"The method should not have thrown an exception, but it threw {ex.GetType()} with message \"{ex.Message}\".");
                }

                Assert.Equal(expectedResult, result);
            }
        }
    }
}