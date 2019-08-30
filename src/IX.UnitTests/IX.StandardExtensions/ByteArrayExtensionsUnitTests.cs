// <copyright file="ByteArrayExtensionsUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.StandardExtensions.Extensions;
using Xunit;

namespace IX.UnitTests.IX.StandardExtensions
{
    /// <summary>
    /// Unit tests for ByteArrayExtensions.
    /// </summary>
    public class ByteArrayExtensionsUnitTests
    {
        /// <summary>
        /// Generates data for tests.
        /// </summary>
        /// <returns>The data, as a jagged array.</returns>
        public static object[][] TestDataGenerator() => new[]
            {
                new object[]
                {
                    new byte[]
                    {
                        2,
                        1,
                        7,
                        15,
                        177,
                        0,
                    },
                    new byte[]
                    {
                        2,
                        1,
                        7,
                        15,
                        177,
                    },
                    0,
                },
                new object[]
                {
                    new byte[]
                    {
                        2,
                        1,
                        7,
                        15,
                        177,
                    },
                    new byte[]
                    {
                        2,
                        1,
                        7,
                        15,
                        177,
                    },
                    0,
                },
                new object[]
                {
                    new byte[]
                    {
                        2,
                        1,
                        7,
                        15,
                        178,
                        0,
                    },
                    new byte[]
                    {
                        2,
                        1,
                        7,
                        15,
                        177,
                    },
                    1,
                },
                new object[]
                {
                    new byte[]
                    {
                        0,
                        0,
                        0,
                        0,
                        178,
                        0,
                    },
                    new byte[]
                    {
                        255,
                        255,
                        255,
                        255,
                        177,
                    },
                    1,
                },
            };

        /// <summary>
        /// Tests the byte array comparison with MSB.
        /// </summary>
        /// <param name="b1">Left-side array to compare.</param>
        /// <param name="b2">Right-side array to compare.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory(DisplayName = "Comparison with MSB test")]
        [MemberData(nameof(TestDataGenerator))]
        public void TestByteArrayComparisonWithMsb(byte[] b1, byte[] b2, int expectedResult) => Assert.Equal(expectedResult, b1.SequenceCompareWithMsb(b2));

        /// <summary>
        /// Tests the byte array equality with MSB.
        /// </summary>
        /// <param name="b1">Left-side array to compare.</param>
        /// <param name="b2">Right-side array to compare.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory(DisplayName = "Equality with MSB test")]
        [MemberData(nameof(TestDataGenerator))]
        public void TestByteArrayEqualityWithMsb(byte[] b1, byte[] b2, int expectedResult) => Assert.Equal(expectedResult == 0, b1.SequenceEqualsWithMsb(b2));
    }
}