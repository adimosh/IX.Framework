// <copyright file="RightBitwiseExtensionsUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.StandardExtensions.Extensions;
using Xunit;

namespace IX.UnitTests.IX.StandardExtensions
{
    /// <summary>
    /// Unit tet for right bitwise extensions.
    /// </summary>
    public class RightBitwiseExtensionsUnitTests
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
                new byte[] { 0b00000000, 0b00001000 },
            },
            new object[]
            {
                new byte[] { 0b00000000, 0b10000001 },
                3,
                new byte[] { 0b00000000, 0b00001000 },
            },
            new object[]
            {
                new byte[] { 0b00000001, 0b10000001 },
                3,
                new byte[] { 0b00001000, 0b00001000 },
            },
            new object[]
            {
                new byte[] { 0b10000001, 0b10000001 },
                3,
                new byte[] { 0b00001000, 0b00001100 },
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
            var result = data.RightShift(howManyBits);

            Assert.True(expectedResult.SequenceEquals(result));
        }
    }
}