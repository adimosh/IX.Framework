// <copyright file="ComputedExpressionUnitTests.cs" company="Adrian Mos">
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
    ///     Tests computed expressions.
    /// </summary>
    public class ComputedExpressionUnitTests : IClassFixture<CachedExpressionProviderFixture>
    {
        private readonly CachedExpressionProviderFixture fixture;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComputedExpressionUnitTests" /> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public ComputedExpressionUnitTests(CachedExpressionProviderFixture fixture)
        {
            this.fixture = fixture;
        }

        /// <summary>
        ///     Provides the data for theory.
        /// </summary>
        /// <returns>Theory data.</returns>
        public static object[][] ProvideDataForTheory() => new[]
        {
            new object[]
            {
                "3+6",
                null,
                9L,
            },
            new object[]
            {
                "8-9",
                null,
                -1L,
            },
            new object[]
            {
                "0=0",
                null,
                true,
            },
            new object[]
            {
                "\"some string\"=\"some string\"",
                null,
                true,
            },
            new object[]
            {
                "true=true",
                null,
                true,
            },
            new object[]
            {
                "0=1",
                null,
                false,
            },
            new object[]
            {
                "\"some string\"=\"spppng\"",
                null,
                false,
            },
            new object[]
            {
                "false=true",
                null,
                false,
            },
            new object[]
            {
                "0!=0",
                null,
                false,
            },
            new object[]
            {
                "\"some string\"!=\"skskskg\"",
                null,
                true,
            },
            new object[]
            {
                "false!=true",
                null,
                true,
            },
            new object[]
            {
                "6-2",
                null,
                4L,
            },
            new object[]
            {
                "3*6",
                null,
                18L,
            },
            new object[]
            {
                "3/6",
                null,
                0.5,
            },
            new object[]
            {
                "6/3",
                null,
                2L,
            },
            new object[]
            {
                "6^3",
                null,
                216L,
            },
            new object[]
            {
                @"""3""+6",
                null,
                "36",
            },
            new object[]
            {
                @"""3""+""6""",
                null,
                "36",
            },
            new object[]
            {
                @"""3+6""",
                null,
                "3+6",
            },
            new object[]
            {
                "3+6-2*4",
                null,
                1L,
            },
            new object[]
            {
                "3+(6-2)*2",
                null,
                11L,
            },
            new object[]
            {
                "3+(6-2*2)",
                null,
                5L,
            },
            new object[]
            {
                "1<<2",
                null,
                4L,
            },
            new object[]
            {
                "3-6+1<<2",
                null,
                1L,
            },
            new object[]
            {
                "x&y",
                new Dictionary<string, object>
                {
                    ["x"] = 5,
                    ["y"] = 49,
                },
                1L,
            },
            new object[]
            {
                "x|y",
                new Dictionary<string, object>
                {
                    ["x"] = 5,
                    ["y"] = 49,
                },
                53L,
            },
            new object[]
            {
                "x#y",
                new Dictionary<string, object>
                {
                    ["x"] = 5,
                    ["y"] = 49,
                },
                52L,
            },
            new object[]
            {
                "x&y",
                new Dictionary<string, object>
                {
                    ["x"] = true,
                    ["y"] = false,
                },
                false,
            },
            new object[]
            {
                "x&y",
                new Dictionary<string, object>
                {
                    ["x"] = true,
                    ["y"] = true,
                },
                true,
            },
            new object[]
            {
                "x|y",
                new Dictionary<string, object>
                {
                    ["x"] = true,
                    ["y"] = false,
                },
                true,
            },
            new object[]
            {
                "x|(1>2)",
                new Dictionary<string, object>
                {
                    ["x"] = true,
                },
                true,
            },
            new object[]
            {
                "x|y",
                new Dictionary<string, object>
                {
                    ["x"] = false,
                    ["y"] = false,
                },
                false,
            },
            new object[]
            {
                "x#y",
                new Dictionary<string, object>
                {
                    ["x"] = true,
                    ["y"] = true,
                },
                false,
            },
            new object[]
            {
                "x#y",
                new Dictionary<string, object>
                {
                    ["x"] = true,
                    ["y"] = false,
                },
                true,
            },
            new object[]
            {
                "x<<y",
                new Dictionary<string, object>
                {
                    ["x"] = 3,
                    ["y"] = 2,
                },
                12L,
            },
            new object[]
            {
                "x>>y",
                new Dictionary<string, object>
                {
                    ["x"] = 3,
                    ["y"] = 1,
                },
                1L,
            },
            new object[]
            {
                "0x1123>>8",
                null,
                17L,
            },
            new object[]
            {
                "2<<2+1<<2",
                null,
                12L,
            },
            new object[]
            {
                "1<<1<<2",
                null,
                8L,
            },
            new object[]
            {
                "1<<2>>2",
                null,
                1L,
            },
            new object[]
            {
                "((2+3)*2-1)*2",
                null,
                18L,
            },
            new object[]
            {
                "  3         +        6      ",
                null,
                9L,
            },
            new object[]
            {
                "3=6",
                null,
                false,
            },
            new object[]
            {
                "((2+3)*2-1)*2 - x",
                new Dictionary<string, object>
                {
                    ["x"] = 12,
                },
                6D,
            },
            new object[]
            {
                "x^2",
                new Dictionary<string, object>
                {
                    ["x"] = 2,
                },
                4.0,
            },
            new object[]
            {
                "x^3",
                new Dictionary<string, object>
                {
                    ["x"] = 3,
                },
                27.0,
            },
            new object[]
            {
                "x",
                new Dictionary<string, object>
                {
                    ["x"] = 12,
                },
                12L,
            },
            new object[]
            {
                "2*x-7*y",
                new Dictionary<string, object>
                {
                    ["x"] = 12,
                    ["y"] = 2,
                },
                10D,
            },
            new object[]
            {
                "x-y",
                new Dictionary<string, object>
                {
                    ["x"] = 12,
                    ["y"] = 2,
                },
                10D,
            },
            new object[]
            {
                "textparam = 12",
                new Dictionary<string, object>
                {
                    ["textparam"] = 13,
                },
                false,
            },
            new object[]
            {
                "7+14+79<3+(7*12)",
                null,
                false,
            },
            new object[]
            {
                "-1.00<-1",
                null,
                false,
            },
            new object[]
            {
                "1<<1",
                null,
                2L,
            },
            new object[]
            {
                "7/2",
                null,
                3.5,
            },
            new object[]
            {
                "1<<1 + 2 << 1",
                null,
                6L,
            },
            new object[]
            {
                "((1+1)-(1-1))+((1-1)-(1+1))",
                null,
                0L,
            },
            new object[]
            {
                "((6-3)*(3+3))-1",
                null,
                17L,
            },
            new object[]
            {
                "2+sqrt(4)+2",
                null,
                6L,
            },
            new object[]
            {
                "2.0*x-7*y",
                new Dictionary<string, object>
                {
                    ["x"] = 12.5D,
                    ["y"] = 2,
                },
                11.0D,
            },
            new object[]
            {
                "!x",
                new Dictionary<string, object>
                {
                    ["x"] = 32768,
                },
                -32769L,
            },
            new object[]
            {
                "strlen(x)",
                new Dictionary<string, object>
                {
                    ["x"] = "alabala",
                },
                7L,
            },
            new object[]
            {
                "21*3-17",
                null,
                46L,
            },
            new object[]
            {
                "(1+1)*2-3",
                null,
                1L,
            },
            new object[]
            {
                "sqrt(4)",
                null,
                2L,
            },
            new object[]
            {
                "sqrt(4.0)",
                null,
                2L,
            },
            new object[]
            {
                "sqrt(0.49)",
                null,
                0.7,
            },
            new object[]
            {
                "!4+4",
                null,
                -1L,
            },
            new object[]
            {
                "212",
                null,
                212L,
            },
            new object[]
            {
                "String is wonderful",
                null,
                "String is wonderful",
            },
            new object[]
            {
                "212=String again",
                null,
                "212=String again",
            },
            new object[]
            {
                "0x10+26",
                null,
                42L,
            },
            new object[]
            {
                "e",
                null,
                global::System.Math.E,
            },
            new object[]
            {
                "[pi]",
                null,
                global::System.Math.PI,
            },
            new object[]
            {
                "e*[pi]",
                null,
                global::System.Math.E * global::System.Math.PI,
            },
            new object[]
            {
                "min(2,17)",
                null,
                2L,
            },
            new object[]
            {
                "max(2,17)+1",
                null,
                18L,
            },
            new object[]
            {
                "(max(2,17)+1)/2",
                null,
                9L,
            },
            new object[]
            {
                "max(2,17)+max(3,1)",
                null,
                20L,
            },
            new object[]
            {
                "(sqrt(16)+1)*4-max(20,13)+(27*5-27*4 - sqrt(49))",
                null,
                20L,
            },
            new object[]
            {
                "strlen(\"This that those\")",
                null,
                15L,
            },
            new object[]
            {
                "5+strlen(\"This that those\")-10",
                null,
                10L,
            },
            new object[]
            {
                "min(max(10,5),max(25,10))",
                null,
                10L,
            },
            new object[]
            {
                "min(max(10,5)+40,3*max(25,10))",
                null,
                50L,
            },
            new object[]
            {
                "min(max(5+strlen(\"This that those\")-10,5)+40,3*max(25,10))",
                null,
                50L,
            },
            new object[]
            {
                "1--2",
                null,
                3L,
            },
            new object[]
            {
                "x+y",
                new Dictionary<string, object>
                {
                    ["x"] = 1,
                    ["y"] = -2,
                },
                -1L,
            },
            new object[]
            {
                "1*-2",
                null,
                -2L,
            },
            new object[]
            {
                "(x=0) & (y=1)",
                new Dictionary<string, object>
                {
                    ["x"] = 0,
                    ["y"] = 1,
                },
                true,
            },
            new object[]
            {
                "(x=0) | (y=1)",
                new Dictionary<string, object>
                {
                    ["x"] = 0,
                    ["y"] = 0,
                },
                true,
            },
            new object[]
            {
                "(x=0) & (y=1)",
                new Dictionary<string, object>
                {
                    ["x"] = 0,
                    ["y"] = 2,
                },
                false,
            },
            new object[]
            {
                "(x>0) & (y<1)",
                new Dictionary<string, object>
                {
                    ["x"] = 1,
                    ["y"] = 0,
                },
                true,
            },
            new object[]
            {
                "abs(x)",
                new Dictionary<string, object>
                {
                    ["x"] = -1,
                },
                1L,
            },
            new object[]
            {
                "abs(x)",
                new Dictionary<string, object>
                {
                    ["x"] = -1D,
                },
                1D,
            },
            new object[]
            {
                "abs(0x1)",
                null,
                1L,
            },
            new object[]
            {
                "sqrt(x)",
                new Dictionary<string, object>
                {
                    ["x"] = 2,
                },
                global::System.Math.Sqrt(2),
            },
            new object[]
            {
                "sqrt(x)",
                new Dictionary<string, object>
                {
                    ["x"] = 9,
                },
                3D,
            },
            new object[]
            {
                "ceil(x)",
                new Dictionary<string, object>
                {
                    ["x"] = 2.2,
                },
                3D,
            },
            new object[]
            {
                "floor(x)",
                new Dictionary<string, object>
                {
                    ["x"] = 4.9,
                },
                4D,
            },
            new object[]
            {
                "round(x)",
                new Dictionary<string, object>
                {
                    ["x"] = 3.5,
                },
                4D,
            },
            new object[]
            {
                "round(x)",
                new Dictionary<string, object>
                {
                    ["x"] = 2.49,
                },
                2D,
            },
            new object[]
            {
                "(2*max(x,500)-y)/pow(x,2)",
                new Dictionary<string, object>
                {
                    ["x"] = 217,
                    ["y"] = 323,
                },
                0.014377030729045,
            },
            new object[]
            {
                "min(max(x,y),10)",
                new Dictionary<string, object>
                {
                    ["x"] = 5,
                    ["y"] = 3,
                },
                5D,
            },
            new object[]
            {
                "min(max(x,y),max(y,500)*2-min(995,pow(x,200)))",
                new Dictionary<string, object>
                {
                    ["x"] = 5,
                    ["y"] = 3,
                },
                5D,
            },
            new object[]
            {
                "max(max(x,y),max(y,500)*2-995)",
                new Dictionary<string, object>
                {
                    ["x"] = 5.5,
                    ["y"] = 3,
                },
                5.5,
            },
            new object[]
            {
                "substr(x,y)",
                new Dictionary<string, object>
                {
                    ["x"] = "aaabbb",
                    ["y"] = 3,
                },
                "bbb",
            },
            new object[]
            {
                "substr(x,y,z)",
                new Dictionary<string, object>
                {
                    ["x"] = "aaabbb",
                    ["y"] = 3,
                    ["z"] = 2,
                },
                "bb",
            },
            new object[]
            {
                "strlen(substr(x,y,z))",
                new Dictionary<string, object>
                {
                    ["x"] = "aaabbb",
                    ["y"] = 3,
                    ["z"] = 2,
                },
                2L,
            },
            new object[]
            {
                "abs((1-17)+3) + abs(14-(1*4))",
                null,
                23L,
            },
            new object[]
            {
                "substr(x,y,z)+substr(q,y,z)",
                new Dictionary<string, object>
                {
                    ["x"] = "aaabbb",
                    ["y"] = 3,
                    ["z"] = 2,
                    ["q"] = "ccccddd",
                },
                "bbcd",
            },
            new object[]
            {
                "\"aaa\" + \"bbb\"",
                null,
                "aaabbb",
            },
            new object[]
            {
                "\"aaa\" + substr(\"bbbbbb\", 1, 1)",
                null,
                "aaab",
            },
            new object[]
            {
                "\"aaa\" > \"bbb\"",
                null,
                false,
            },
            new object[]
            {
                "\"aaa\" > x",
                new Dictionary<string, object>
                {
                    ["x"] = "z",
                },
                false,
            },
            new object[]
            {
                "\"aaa\" < \"bbb\"",
                null,
                true,
            },
            new object[]
            {
                "\"aaa\" < x",
                new Dictionary<string, object>
                {
                    ["x"] = "aa",
                },
                false,
            },
            new object[]
            {
                "\"aaa\" >= \"bbb\"",
                null,
                false,
            },
            new object[]
            {
                "\"aaa\" >= x",
                new Dictionary<string, object>
                {
                    ["x"] = "z",
                },
                false,
            },
            new object[]
            {
                "\"aaa\" <= \"bbb\"",
                null,
                true,
            },
            new object[]
            {
                "\"aaa\" <= x",
                new Dictionary<string, object>
                {
                    ["x"] = "aa",
                },
                false,
            },
            new object[]
            {
                "\"aaa\" <= \"aaa\"",
                null,
                true,
            },
            new object[]
            {
                "\"aaa\" >= \"aaa\"",
                null,
                true,
            },
            new object[]
            {
                "tempVariable1=2",
                new Dictionary<string, object>
                {
                    ["tempVariable1"] = 2,
                },
                true,
            },
            new object[]
            {
                "6/2*3",
                null,
                9L,
            },
            new object[]
            {
                "x=\" \"",
                new Dictionary<string, object>
                {
                    ["x"] = " ",
                },
                true,
            },
            new object[]
            {
                "x=\"\"",
                new Dictionary<string, object>
                {
                    ["x"] = string.Empty,
                },
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101=0b1001010111010110110010000000010010101110101",
                null,
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101>0b1010111010110110010000000010010101110101",
                null,
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100<0b1001010111010110110010000000010010101110101",
                null,
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101>=0b1010111010110110010000000010010101110101",
                null,
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100<=0b1001010111010110110010000000010010101110101",
                null,
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101<0b1010111010110110010000000010010101110101",
                null,
                false,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100>0b1001010111010110110010000000010010101110101",
                null,
                false,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101<=0b1010111010110110010000000010010101110101",
                null,
                false,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100>=0b1001010111010110110010000000010010101110101",
                null,
                false,
            },
            new object[]
            {
                "0b1001010111010110110011111000010010101110101=0b1001010111010110110010000000011111101110101",
                null,
                false,
            },
            new object[]
            {
                "x=0b1001010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101!=0b1001010111010110110010000000010010101110101",
                null,
                false,
            },
            new object[]
            {
                "0b1001010111010110110011111000010010101110101!=0b1001010111010110110010000000011111101110101",
                null,
                true,
            },
            new object[]
            {
                "x!=0b1001010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                false,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101>x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1010111010110110010000000010010101110101),
                },
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100<x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101>=x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1010111010110110010000000010010101110101),
                },
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100<=x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                true,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101<x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1010111010110110010000000010010101110101),
                },
                false,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100>x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                false,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110101<=x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1010111010110110010000000010010101110101),
                },
                false,
            },
            new object[]
            {
                "0b1001010111010110110010000000010010101110100>=x",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                false,
            },
            new object[]
            {
                "x>0b1010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                true,
            },
            new object[]
            {
                "x<0b1001010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                true,
            },
            new object[]
            {
                "x>=0b1010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                true,
            },
            new object[]
            {
                "x<=0b1001010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                true,
            },
            new object[]
            {
                "x<0b1010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                false,
            },
            new object[]
            {
                "x>0b1001010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                false,
            },
            new object[]
            {
                "x<=0b1010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                false,
            },
            new object[]
            {
                "x>=0b1001010111010110110010000000010010101110101",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                false,
            },
            new object[]
            {
                "x>=y",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                    ["y"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                true,
            },
            new object[]
            {
                "x<=y",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                    ["y"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                true,
            },
            new object[]
            {
                "x>y",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                    ["y"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                false,
            },
            new object[]
            {
                "x<y",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                    ["y"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110100),
                },
                false,
            },
            new object[]
            {
                "x>y",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b11111111_11111111_11111111),
                    ["y"] = BitConverter.GetBytes(0b11111111_11111111),
                },
                true,
            },
            new object[]
            {
                "x>y",
                new Dictionary<string, object>
                {
                    ["x"] = BitConverter.GetBytes(0b11111111_11111111_00000000),
                    ["y"] = BitConverter.GetBytes(0b00000000_11111111_11111111),
                },
                true,
            },
            new object[]
            {
                "0b11111111_11111111_00000000>0b00000000_11111111_11111111",
                null,
                true,
            },
            new object[]
            {
                "x*(x+1)*(x+2)",
                new Dictionary<string, object>
                {
                    ["x"] = 5,
                },
                210D,
            },
            new object[]
            {
                "tempVar1+tempVar1",
                new Dictionary<string, object>
                {
                    ["tempVar1"] = 5,
                },
                10L,
            },
            new object[]
            {
                "tempVar1+tempVar2",
                new Dictionary<string, object>
                {
                    ["tempVar1"] = 5,
                    ["tempVar2"] = 5D,
                },
                10D,
            },
            new object[]
            {
                "tempVar1",
                new Dictionary<string, object>
                {
                    ["tempVar1"] = 5D,
                },
                5D,
            },
            new object[]
            {
                "tempVar1",
                new Dictionary<string, object>
                {
                    ["tempVar1"] = "aaa",
                },
                "aaa",
            },
            new object[]
            {
                "tempVar1",
                new Dictionary<string, object>
                {
                    ["tempVar1"] = 5L,
                },
                5L,
            },
            new object[]
            {
                "tempVar1",
                new Dictionary<string, object>
                {
                    ["tempVar1"] = true,
                },
                true,
            },
            new object[]
            {
                "tempVar1",
                new Dictionary<string, object>
                {
                    ["tempVar1"] = BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
                },
                BitConverter.GetBytes(0b1001010111010110110010000000010010101110101),
            },
            new object[]
            {
                "2.12+6.274E+1",
                null,
                64.86D,
            },
            new object[]
            {
                "2.12+6.274E1",
                null,
                64.86D,
            },
            new object[]
            {
                "2.12+627.4E-2",
                null,
                8.394D,
            },
            new object[]
            {
                "2.12+6.274e+1",
                null,
                64.86D,
            },
            new object[]
            {
                "2.12+6.274e1",
                null,
                64.86D,
            },
            new object[]
            {
                "2.12+627.4e-2",
                null,
                8.394D,
            },
        };

        private static object GenerateFuncOutOfParameterValue(object tempParameter)
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

        /// <summary>
        ///     Tests the computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "EPSPara")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithParameters(
            string expression,
            Dictionary<string, object> parameters,
            object expectedResult)
        {
            using (var service = new ExpressionParsingService())
            {
                using (ComputedExpression del = service.Interpret(expression))
                {
                    if (del == null)
                    {
                        throw new InvalidOperationException("No computed expression was generated!");
                    }

                    object result = del.Compute(parameters?.Values.ToArray() ?? new object[0]);

                    Assert.Equal(
                        expectedResult,
                        result);
                }
            }
        }

        /// <summary>
        ///     Tests a computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "EPSFindr")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithFinder(
            string expression,
            Dictionary<string, object> parameters,
            object expectedResult)
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
                            object value = parameter.Value;
                            finder.Setup(
                                p => p.TryGetData(
                                    key,
                                    out value)).Returns(true);
                        }
                    }

                    object result = del.Compute(finder.Object);

                    Assert.Equal(
                        expectedResult,
                        result);
                }
            }
        }

        /// <summary>
        ///     Tests the cached computed expression with parameters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "CEPSPara")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithParameters(
            string expression,
            Dictionary<string, object> parameters,
            object expectedResult)
        {
            ComputedExpression del = this.fixture.Service.Interpret(expression);
            if (del == null)
            {
                throw new InvalidOperationException("No computed expression was generated!");
            }

            object result = del.Compute(parameters?.Values.ToArray() ?? new object[0]);

            Assert.Equal(
                expectedResult,
                result);
        }

        /// <summary>
        ///     Tests a cached computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "CEPSFindr")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithFinder(
            string expression,
            Dictionary<string, object> parameters,
            object expectedResult)
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
                    object value = parameter.Value;
                    finder.Setup(
                        p => p.TryGetData(
                            key,
                            out value)).Returns(true);
                }
            }

            object result = del.Compute(finder.Object);

            Assert.Equal(
                expectedResult,
                result);
        }

        /// <summary>
        ///     Tests a computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "EPSFindrFunc")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void ComputedExpressionWithFunctionFinder(
            string expression,
            Dictionary<string, object> parameters,
            object expectedResult)
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
                            object value = GenerateFuncOutOfParameterValue(parameter.Value);
                            finder.Setup(
                                p => p.TryGetData(
                                    key,
                                    out value)).Returns(true);
                        }
                    }

                    object result = del.Compute(finder.Object);

                    Assert.Equal(
                        expectedResult,
                        result);
                }
            }
        }

        /// <summary>
        ///     Tests a cached computed expression with finder.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated!.
        /// </exception>
        [Theory(DisplayName = "CEPSFindrFunc")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithFunctionFinder(
            string expression,
            Dictionary<string, object> parameters,
            object expectedResult)
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
                    object value = GenerateFuncOutOfParameterValue(parameter.Value);
                    finder.Setup(
                        p => p.TryGetData(
                            key,
                            out value)).Returns(true);
                }
            }

            object result = del.Compute(finder.Object);

            Assert.Equal(
                expectedResult,
                result);
        }

        /// <summary>
        ///     Tests a cached computed expression with finder returning functions repeatedly.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <exception cref="InvalidOperationException">
        ///     No computed expression was generated.
        /// </exception>
        [Theory(DisplayName = "CEPSFindrFuncRepeated")]
        [MemberData(nameof(ProvideDataForTheory))]
        public void CachedComputedExpressionWithFunctionFinderRepeated(
            string expression,
            Dictionary<string, object> parameters,
            object expectedResult)
        {
            var indexLimit = DataGenerator.RandomInteger(
                3,
                5);
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
                        object value = GenerateFuncOutOfParameterValue(parameter.Value);
                        finder.Setup(
                            p => p.TryGetData(
                                key,
                                out value)).Returns(true);
                    }
                }

                object result = del.Compute(finder.Object);

                Assert.Equal(
                    expectedResult,
                    result);
            }
        }
    }
}