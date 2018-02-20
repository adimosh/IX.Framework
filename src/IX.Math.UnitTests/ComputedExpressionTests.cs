// <copyright file="ComputedExpressionTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using Moq;
using Xunit;

namespace IX.Math.UnitTests
{
    /// <summary>
    /// Tests computed expressions.
    /// </summary>
    public class ComputedExpressionTests : IClassFixture<CachedExpressionProviderFixture>
    {
        private CachedExpressionProviderFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputedExpressionTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public ComputedExpressionTests(CachedExpressionProviderFixture fixture)
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
                    "3+6",
                    new object[0],
                    9L,
                },
                new object[]
                {
                    "8-9",
                    new object[0],
                    -1L,
                },
                new object[]
                {
                    "0=0",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "\"some string\"=\"some string\"",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "true=true",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "0=1",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "\"some string\"=\"spppng\"",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "false=true",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "0!=0",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "\"some string\"!=\"skskskg\"",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "false!=true",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "6-2",
                    new object[0],
                    4L,
                },
                new object[]
                {
                    "3*6",
                    new object[0],
                    18L,
                },
                new object[]
                {
                    "3/6",
                    new object[0],
                    0.5,
                },
                new object[]
                {
                    "6/3",
                    new object[0],
                    2L,
                },
                new object[]
                {
                    "6^3",
                    new object[0],
                    216L,
                },
                new object[]
                {
                    @"""3""+6",
                    new object[0],
                    "36",
                },
                new object[]
                {
                    @"""3""+""6""",
                    new object[0],
                    "36",
                },
                new object[]
                {
                    @"""3+6""",
                    new object[0],
                    "3+6",
                },
                new object[]
                {
                    "3+6-2*4",
                    new object[0],
                    1L,
                },
                new object[]
                {
                    "3+(6-2)*2",
                    new object[0],
                    11L,
                },
                new object[]
                {
                    "3+(6-2*2)",
                    new object[0],
                    5L,
                },
                new object[]
                {
                    "1<<2",
                    new object[0],
                    4L,
                },
                new object[]
                {
                    "3-6+1<<2",
                    new object[0],
                    1L,
                },
                new object[]
                {
                    "x&y",
                    new object[2] { 5, 49 },
                    1L,
                },
                new object[]
                {
                    "x|y",
                    new object[2] { 5, 49 },
                    53L,
                },
                new object[]
                {
                    "x#y",
                    new object[2] { 5, 49 },
                    52L,
                },
                new object[]
                {
                    "x&y",
                    new object[2] { true, false },
                    false,
                },
                new object[]
                {
                    "x&y",
                    new object[2] { true, true },
                    true,
                },
                new object[]
                {
                    "x|y",
                    new object[2] { true, false },
                    true,
                },
                new object[]
                {
                    "x|(1>2)",
                    new object[1] { true },
                    true,
                },
                new object[]
                {
                    "x|y",
                    new object[2] { false, false },
                    false,
                },
                new object[]
                {
                    "x#y",
                    new object[2] { true, true },
                    false,
                },
                new object[]
                {
                    "x#y",
                    new object[2] { true, false },
                    true,
                },
                new object[]
                {
                    "x<<y",
                    new object[2] { 3, 2 },
                    12L,
                },
                new object[]
                {
                    "x>>y",
                    new object[2] { 3, 1 },
                    1L,
                },
                new object[]
                {
                    "0x1123>>8",
                    new object[0],
                    17L,
                },
                new object[]
                {
                    "2<<2+1<<2",
                    new object[0],
                    12L,
                },
                new object[]
                {
                    "1<<1<<2",
                    new object[0],
                    8L,
                },
                new object[]
                {
                    "1<<2>>2",
                    new object[0],
                    1L,
                },
                new object[]
                {
                    "((2+3)*2-1)*2",
                    new object[0],
                    18L,
                },
                new object[]
                {
                    "  3         +        6      ",
                    new object[0],
                    9L,
                },
                new object[]
                {
                    "3=6",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "((2+3)*2-1)*2 - x",
                    new object[] { 12 },
                    6D,
                },
                new object[]
                {
                    "x^2",
                    new object[] { 2 },
                    4.0,
                },
                new object[]
                {
                    "x^3",
                    new object[] { 3 },
                    27.0,
                },
                new object[]
                {
                    "x",
                    new object[] { 12 },
                    12D,
                },
                new object[]
                {
                    "2*x-7*y",
                    new object[] { 12, 2 },
                    10D,
                },
                new object[]
                {
                    "x-y",
                    new object[] { 12, 2 },
                    10D,
                },
                new object[]
                {
                    "textparam = 12",
                    new object[] { 13 },
                    false,
                },
                new object[]
                {
                    "7+14+79<3+(7*12)",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "-1.00<-1",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "1<<1",
                    new object[0],
                    2L,
                },
                new object[]
                {
                    "7/2",
                    new object[0],
                    3.5,
                },
                new object[]
                {
                    "1<<1 + 2 << 1",
                    new object[0],
                    6L,
                },
                new object[]
                {
                    "((1+1)-(1-1))+((1-1)-(1+1))",
                    new object[0],
                    0L,
                },
                new object[]
                {
                    "((6-3)*(3+3))-1",
                    new object[0],
                    17L,
                },
                new object[]
                {
                    "2+sqrt(4)+2",
                    new object[0],
                    6L,
                },
                new object[]
                {
                    "2.0*x-7*y",
                    new object[] { 12.5D, 2 },
                    11.0D,
                },
                new object[]
                {
                    "!x",
                    new object[] { 32768 },
                    -32769L,
                },
                new object[]
                {
                    "strlen(x)",
                    new object[] { "alabala" },
                    7L,
                },
                new object[]
                {
                    "21*3-17",
                    new object[0],
                    46L,
                },
                new object[]
                {
                    "(1+1)*2-3",
                    new object[0],
                    1L,
                },
                new object[]
                {
                    "sqrt(4)",
                    new object[0],
                    2L,
                },
                new object[]
                {
                    "sqrt(4.0)",
                    new object[0],
                    2L,
                },
                new object[]
                {
                    "sqrt(0.49)",
                    new object[0],
                    0.7,
                },
                new object[]
                {
                    "!4+4",
                    new object[0],
                    -1L,
                },
                new object[]
                {
                    "212",
                    new object[0],
                    212L,
                },
                new object[]
                {
                    "String is wonderful",
                    new object[0],
                    "String is wonderful",
                },
                new object[]
                {
                    "212=String again",
                    new object[0],
                    "212=String again",
                },
                new object[]
                {
                    "0x10+26",
                    new object[0],
                    42L,
                },
                new object[]
                {
                    "e",
                    new object[0],
                    global::System.Math.E,
                },
                new object[]
                {
                    "[pi]",
                    new object[0],
                    global::System.Math.PI,
                },
                new object[]
                {
                    "e*[pi]",
                    new object[0],
                    global::System.Math.E * global::System.Math.PI,
                },
                new object[]
                {
                    "min(2,17)",
                    new object[0],
                    2L,
                },
                new object[]
                {
                    "max(2,17)+1",
                    new object[0],
                    18L,
                },
                new object[]
                {
                    "(max(2,17)+1)/2",
                    new object[0],
                    9L,
                },
                new object[]
                {
                    "max(2,17)+max(3,1)",
                    new object[0],
                    20L,
                },
                new object[]
                {
                    "(sqrt(16)+1)*4-max(20,13)+(27*5-27*4 - sqrt(49))",
                    new object[0],
                    20L,
                },
                new object[]
                {
                    "strlen(\"This that those\")",
                    new object[0],
                    15L,
                },
                new object[]
                {
                    "5+strlen(\"This that those\")-10",
                    new object[0],
                    10L,
                },
                new object[]
                {
                    "min(max(10,5),max(25,10))",
                    new object[0],
                    10L,
                },
                new object[]
                {
                    "min(max(10,5)+40,3*max(25,10))",
                    new object[0],
                    50L,
                },
                new object[]
                {
                    "min(max(5+strlen(\"This that those\")-10,5)+40,3*max(25,10))",
                    new object[0],
                    50L,
                },
                new object[]
                {
                    "1--2",
                    new object[0],
                    3L,
                },
                new object[]
                {
                    "x+y",
                    new object[2] { 1, -2 },
                    -1D,
                },
                new object[]
                {
                    "1*-2",
                    new object[0],
                    -2L,
                },
                new object[]
                {
                    "(x=0) & (y=1)",
                    new object[2] { 0, 1 },
                    true,
                },
                new object[]
                {
                    "(x=0) | (y=1)",
                    new object[2] { 0, 0 },
                    true,
                },
                new object[]
                {
                    "(x=0) & (y=1)",
                    new object[2] { 0, 2 },
                    false,
                },
                new object[]
                {
                    "(x>0) & (y<1)",
                    new object[2] { 1, 0 },
                    true,
                },
                new object[]
                {
                    "abs(x)",
                    new object[] { -1 },
                    1D,
                },
                new object[]
                {
                    "abs(0x1)",
                    new object[0],
                    1L,
                },
                new object[]
                {
                    "sqrt(x)",
                    new object[] { 2 },
                    1.4142135623730951,
                },
                new object[]
                {
                    "sqrt(x)",
                    new object[] { 9 },
                    3D,
                },
                new object[]
                {
                    "ceil(x)",
                    new object[] { 2.2 },
                    3D,
                },
                new object[]
                {
                    "floor(x)",
                    new object[] { 4.9 },
                    4D,
                },
                new object[]
                {
                    "round(x)",
                    new object[] { 3.5 },
                    4D,
                },
                new object[]
                {
                    "round(x)",
                    new object[] { 2.49 },
                    2D,
                },
                new object[]
                {
                    "(2*max(x,500)-y)/pow(x,2)",
                    new object[] { 217, 323 },
                    0.014377030729045,
                },
                new object[]
                {
                    "min(max(x,y),10)",
                    new object[] { 5, 3 },
                    5D,
                },
                new object[]
                {
                    "min(max(x,y),max(y,500)*2-min(995,pow(x,200)))",
                    new object[] { 5, 3 },
                    5D,
                },
                new object[]
                {
                    "max(max(x,y),max(y,500)*2-995)",
                    new object[] { 5.5, 3 },
                    5.5,
                },
                new object[]
                {
                    "substr(x,y)",
                    new object[] { "aaabbb", 3 },
                    "bbb",
                },
                new object[]
                {
                    "substr(x,y,z)",
                    new object[] { "aaabbb", 3, 2 },
                    "bb",
                },
                new object[]
                {
                    "strlen(substr(x,y,z))",
                    new object[] { "aaabbb", 3, 2 },
                    2L,
                },
                new object[]
                {
                    "substr(x,y,z)+substr(q,y,z)",
                    new object[] { "aaabbb", 3, 2, "ccccddd" },
                    "bbcd",
                },
                new object[]
                {
                    "\"aaa\" + \"bbb\"",
                    new object[0],
                    "aaabbb",
                },
                new object[]
                {
                    "\"aaa\" + substr(\"bbbbbb\", 1, 1)",
                    new object[0],
                    "aaab",
                },
                new object[]
                {
                    "\"aaa\" > \"bbb\"",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "\"aaa\" > x",
                    new object[1] { "z" },
                    false,
                },
                new object[]
                {
                    "\"aaa\" < \"bbb\"",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "\"aaa\" < x",
                    new object[1] { "aa" },
                    false,
                },
                new object[]
                {
                    "\"aaa\" >= \"bbb\"",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "\"aaa\" >= x",
                    new object[1] { "z" },
                    false,
                },
                new object[]
                {
                    "\"aaa\" <= \"bbb\"",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "\"aaa\" <= x",
                    new object[1] { "aa" },
                    false,
                },
                new object[]
                {
                    "\"aaa\" <= \"aaa\"",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "\"aaa\" >= \"aaa\"",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "tempVariable1=2",
                    new object[1] { 2 },
                    true,
                },
                new object[]
                {
                    "6/2*3",
                    new object[0],
                    9L,
                },
                new object[]
                {
                    "0b1001010111010110110010000000010010101110101=0b1001010111010110110010000000010010101110101",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "0b1001010111010110110011111000010010101110101=0b1001010111010110110010000000011111101110101",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "x=0b1001010111010110110010000000010010101110101",
                    new object[1] { BitConverter.GetBytes(0b1001010111010110110010000000010010101110101) },
                    true,
                },
                new object[]
                {
                    "0b1001010111010110110010000000010010101110101!=0b1001010111010110110010000000010010101110101",
                    new object[0],
                    false,
                },
                new object[]
                {
                    "0b1001010111010110110011111000010010101110101!=0b1001010111010110110010000000011111101110101",
                    new object[0],
                    true,
                },
                new object[]
                {
                    "x!=0b1001010111010110110010000000010010101110101",
                    new object[1] { BitConverter.GetBytes(0b1001010111010110110010000000010010101110101) },
                    false,
                },
                new object[]
                {
                    "x*(x+1)*(x+2)",
                    new object[1] { 5 },
                    210D,
                },
                new object[]
                {
                    "tempVar1+tempVar1",
                    new object[1] { 5 },
                    10D,
                },
#if false
                new object[]
                {
                    "2.12+6.274E+1",
                    new object[0],
                    64.84D,
                },
                new object[]
                {
                    "2.12+6.274E1",
                    new object[0],
                    64.84D,
                },
#endif
            };

        /// <summary>
        /// Tests the computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// or
        /// </exception>
        [Theory(DisplayName = "EPSPara")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithParameters(string expression, object[] parameters, object expectedResult)
        {
            using (var service = new ExpressionParsingService())
            {
                ComputedExpression del = service.Interpret(expression);

                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                var result = del.Compute(parameters);

                Assert.Equal(expectedResult, result);
            }
        }

        /// <summary>
        /// Tests a computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// or
        /// </exception>
        [Theory(DisplayName = "EPSFindr")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithFinder(string expression, object[] parameters, object expectedResult)
        {
            using (var service = new ExpressionParsingService())
            {
                var finder = new Mock<IDataFinder>(MockBehavior.Loose);

                ComputedExpression del = service.Interpret(expression);

                if (del == null)
                {
                    throw new InvalidOperationException("No computed expression was generated!");
                }

                for (var i = 0; i < global::System.Math.Min(del.ParameterNames.Length, parameters.Length); i++)
                {
                    var valueName = del.ParameterNames[i];
                    var outValue = parameters[i];

                    finder.Setup(p => p.TryGetData(valueName, out outValue)).Returns(true);
                }

                var result = del.Compute(finder.Object);

                Assert.Equal(expectedResult, result);
            }
        }

        /// <summary>
        /// Tests the cached computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// or
        /// </exception>
        [Theory(DisplayName = "CEPSPara")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithParameters(string expression, object[] parameters, object expectedResult)
        {
            ComputedExpression del = this.fixture.Service.Interpret(expression);

            if (del == null)
            {
                throw new InvalidOperationException("No computed expression was generated!");
            }

            var result = del.Compute(parameters);

            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Tests a cached computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        /// No computed expression was generated!
        /// or
        /// </exception>
        [Theory(DisplayName = "CEPSFindr")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithFinder(string expression, object[] parameters, object expectedResult)
        {
            var finder = new Mock<IDataFinder>(MockBehavior.Loose);

            ComputedExpression del = this.fixture.Service.Interpret(expression);

            if (del == null)
            {
                throw new InvalidOperationException("No computed expression was generated!");
            }

            for (var i = 0; i < global::System.Math.Min(del.ParameterNames.Length, parameters.Length); i++)
            {
                var valueName = del.ParameterNames[i];
                var outValue = parameters[i];

                finder.Setup(p => p.TryGetData(valueName, out outValue)).Returns(true);
            }

            var result = del.Compute(finder.Object);

            Assert.Equal(expectedResult, result);
        }
    }
}