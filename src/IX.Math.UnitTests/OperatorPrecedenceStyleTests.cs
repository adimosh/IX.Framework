// <copyright file="OperatorPrecedenceStyleTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using Xunit;

namespace IX.Math.UnitTests
{
    /// <summary>
    /// Unit tests for operator precedence styles
    /// </summary>
    public class OperatorPrecedenceStyleTests
    {
        /// <summary>
        /// Tests mathematical and C-style operator precedence style.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// </exception>
        [Fact(DisplayName = "Operator precedence style test")]
        public void Test1()
        {
            var expression = "true&true|false&false";

            object result1, result2;

            using (var service = new ExpressionParsingService())
            {
                ComputedExpression del = service.Interpret(expression);

                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                result1 = del.Compute();
            }

            using (var service = new ExpressionParsingService(new MathDefinition
            {
                Parentheses = new Tuple<string, string>("(", ")"),
                SpecialSymbolIndicators = new Tuple<string, string>("[", "]"),
                StringIndicator = "\"",
                ParameterSeparator = ",",
                AddSymbol = "+",
                AndSymbol = "&",
                DivideSymbol = "/",
                NotEqualsSymbol = "!=",
                EqualsSymbol = "=",
                MultiplySymbol = "*",
                NotSymbol = "!",
                OrSymbol = "|",
                PowerSymbol = "^",
                SubtractSymbol = "-",
                XorSymbol = "#",
                GreaterThanOrEqualSymbol = ">=",
                GreaterThanSymbol = ">",
                LessThanOrEqualSymbol = "<=",
                LessThanSymbol = "<",
                RightShiftSymbol = ">>",
                LeftShiftSymbol = "<<",
                OperatorPrecedenceStyle = OperatorPrecedenceStyle.CStyle,
            }))
            {
                ComputedExpression del = service.Interpret(expression);

                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                result2 = del.Compute();
            }

            Assert.False((bool)result1);
            Assert.True((bool)result2);
        }
    }
}