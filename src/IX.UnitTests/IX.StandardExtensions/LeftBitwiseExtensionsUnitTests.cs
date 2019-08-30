// <copyright file="LeftBitwiseExtensionsUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.StandardExtensions.Extensions;
using Xunit;

namespace IX.UnitTests.IX.StandardExtensions
{
    /// <summary>
    /// Unit tests for left bitwise extensions.
    /// </summary>
    public class LeftBitwiseExtensionsUnitTests
    {
        /// <summary>
        /// Generates data for tests.
        /// </summary>
        /// <returns>The data, as a jagged array.</returns>
        public static object[][] TestDataGenerator() => new[]
        {
            new object[]
            {
                new byte[] { 0b00000000, 0b00000001 },
                3,
                new byte[] { 0b00100000, 0b00000000 },
            },
            new object[]
            {
                new byte[] { 0b00000000, 0b10000001 },
                3,
                new byte[] { 0b00100000, 0b00010000 },
            },
            new object[]
            {
                new byte[] { 0b00000001, 0b10000001 },
                3,
                new byte[] { 0b00100000, 0b00010000 },
            },
            new object[]
            {
                new byte[] { 0b10000000, 0b10000001 },
                3,
                new byte[] { 0b00110000, 0b00010000 },
            },
        };

        /// <summary>
        /// Tests the bitwise extensions.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="howManyBits">How many bits.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory(DisplayName = "Left bitwise extension test")]
        [MemberData(nameof(TestDataGenerator))]
        public void TestBitwiseExtensions(byte[] data, int howManyBits, byte[] expectedResult)
        {
            var result = data.LeftShift(howManyBits);

            Assert.True(expectedResult.SequenceEquals(result));
        }
    }
}