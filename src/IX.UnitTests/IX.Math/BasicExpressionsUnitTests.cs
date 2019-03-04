// <copyright file="BasicExpressionsUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.Math;
using IX.StandardExtensions.TestUtils;
using Xunit;

namespace IX.UnitTests.IX.Math
{
    /// <summary>
    ///     Tests for basic expressions.
    /// </summary>
    public class BasicExpressionsUnitTests
    {
        /// <summary>
        ///     Provides the data for theory.
        /// </summary>
        /// <returns>System.Object[][].</returns>
        public static object[][] ProvideDataForTheory()
        {
            var tests = new List<object[]>();

#pragma warning disable SA1123 // Do not place regions within elements

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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand+{rightOperand}",
                        new object[] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}+rightOperand",
                        new object[] { rightOperand },
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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}-rightOperand",
                        new object[] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand-{rightOperand}",
                        new object[] { leftOperand },
                        expectedResult,
                    });
            }

            // *
            {
                var limit = (int)global::System.Math.Sqrt(int.MaxValue);
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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}*rightOperand",
                        new object[] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand*{rightOperand}",
                        new object[] { leftOperand },
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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand/{rightOperand}",
                        new object[] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}/rightOperand",
                        new object[] { rightOperand },
                        expectedResult,
                    });
            }

#endregion

#region STAGE 1: Negative integers

            // +
            {
                var limit = int.MinValue / 2;
                double leftOperand = DataGenerator.RandomInteger(
                    limit,
                    1);
                double rightOperand = DataGenerator.RandomInteger(
                    limit,
                    1);
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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand+{rightOperand}",
                        new object[] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}+rightOperand",
                        new object[] { rightOperand },
                        expectedResult,
                    });
            }

            // -
            {
                double leftOperand = DataGenerator.RandomInteger(
                    int.MinValue,
                    1);
                double rightOperand = DataGenerator.RandomInteger(
                    int.MinValue,
                    1);
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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}-rightOperand",
                        new object[] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand-{rightOperand}",
                        new object[] { leftOperand },
                        expectedResult,
                    });
            }

            // *
            {
                var limit = 0 - (int)global::System.Math.Sqrt(int.MaxValue);
                double leftOperand = DataGenerator.RandomInteger(
                    limit,
                    1);
                double rightOperand = DataGenerator.RandomInteger(
                    limit,
                    1);
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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}*rightOperand",
                        new object[] { rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand*{rightOperand}",
                        new object[] { leftOperand },
                        expectedResult,
                    });
            }

            // /
            {
                double leftOperand = DataGenerator.RandomInteger(
                    int.MinValue,
                    1);
                double rightOperand = DataGenerator.RandomInteger(
                    int.MinValue,
                    1);
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
                        new object[] { leftOperand, rightOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"leftOperand/{rightOperand}",
                        new object[] { leftOperand },
                        expectedResult,
                    });
                tests.Add(
                    new object[]
                    {
                        $"{leftOperand}/rightOperand",
                        new object[] { rightOperand },
                        expectedResult,
                    });
            }

#endregion

#pragma warning restore SA1123 // Do not place regions within elements

            return tests.ToArray();
        }

        /// <summary>
        ///     Tests computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "Basic expressions with random data")]
        [MemberData(nameof(ProvideDataForTheory), DisableDiscoveryEnumeration = true)]
        public void ComputedExpressionWithParameters(
            string expression,
            object[] parameters,
            object expectedResult)
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
                    throw new InvalidOperationException(
                        "The generation process should not have thrown an exception, but it did.",
                        ex);
                }

                try
                {
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
                        throw new InvalidOperationException(
                            "The method should not have thrown an exception, but it did.",
                            ex);
                    }

                    Assert.Equal(
                        expectedResult,
                        result);
                }
                finally
                {
                    del?.Dispose();
                }
            }
        }
    }
}