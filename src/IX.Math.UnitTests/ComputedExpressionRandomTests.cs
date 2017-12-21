// <copyright file="ComputedExpressionRandomTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using Xunit;

namespace IX.Math.UnitTests
{
    /// <summary>
    /// Class ComputedExpressionRandomTests.
    /// </summary>
    public class ComputedExpressionRandomTests
    {
        /// <summary>
        /// Computes the unary random function call expression, for testing.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// or
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

                object result;
                try
                {
                    result = del.Compute(100);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"The method should not have thrown an exception, but it threw {ex.GetType()} with message \"{ex.Message}\".");
                }

                Assert.IsType<double>(result);

                Assert.True(((double)result) < 100);
            }
        }

        /// <summary>
        /// Computes the random nonary function call expression, for testing.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// or
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
        }
    }
}