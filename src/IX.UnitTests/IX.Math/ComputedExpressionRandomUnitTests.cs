// <copyright file="ComputedExpressionRandomUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.Math;
using Xunit;

namespace IX.UnitTests.IX.Math
{
    /// <summary>
    /// Class ComputedExpressionRandomTests.
    /// </summary>
    public class ComputedExpressionRandomUnitTests
    {
#pragma warning disable ERP023 // Only ex.Message property was observed in exception block! - Not consequential
        /// <summary>
        /// Computes the unary random function call expression, for testing.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Fact(DisplayName = "Tests the unary function \"random\".")]
        public void ComputedUnaryRandomFunctionCallExpression()
        {
            using (var service = new ExpressionParsingService())
            {
                ComputedExpression del;
                try
                {
                    del = service.Interpret("random(x)");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"The generation process should not have thrown an exception, but it threw {ex.GetType()} with message \"{ex.Message}\".");
                }

                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                try
                {
                    object result;
                    try
                    {
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - Not consequential
                        result = del.Compute(100);
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"The method should not have thrown an exception, but it threw {ex.GetType()} with message \"{ex.Message}\".");
                    }

                    Assert.IsType<double>(result);

                    Assert.True(((double)result) < 100);
                }
                finally
                {
                    del.Dispose();
                }
            }
        }

        /// <summary>
        /// Computes the random nonary function call expression, for testing.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated.
        /// </exception>
        [Fact(DisplayName = "Tests the nonary function \"random\".")]
        public void ComputedRandomNonaryFunctionCallExpression()
        {
            using (var service = new ExpressionParsingService())
            {
                ComputedExpression del;
                try
                {
                    del = service.Interpret("random()");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"The generation process should not have thrown an exception, but it threw {ex.GetType()} with message \"{ex.Message}\".");
                }

                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                try
                {
                    object result;
                    try
                    {
                        result = del.Compute();
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"The method should not have thrown an exception, but it threw {ex.GetType()} with message \"{ex.Message}\".");
                    }

                    Assert.IsType<double>(result);
                }
                finally
                {
                    del.Dispose();
                }
            }
        }
#pragma warning restore ERP023 // Only ex.Message property was observed in exception block!
    }
}