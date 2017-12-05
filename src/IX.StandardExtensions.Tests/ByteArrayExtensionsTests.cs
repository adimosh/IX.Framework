using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IX.StandardExtensions.Tests
{
    public class ByteArrayExtensionsTests
    {
        public static object[][] TestDataGenerator() => new object[][]
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
                    0
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
                    0
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
                    1
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
                    1
                },
            };

        [Theory(DisplayName = "Comparison with MSB test")]
        [MemberData(nameof(TestDataGenerator))]
        public void TestByteArrayComparisonWithMsb(byte[] b1, byte[] b2, int expectedResult) => Assert.Equal(expectedResult, b1.SequenceCompareWithMsb(b2));

        [Theory(DisplayName = "Equality with MSB test")]
        [MemberData(nameof(TestDataGenerator))]
        public void TestByteArrayEqualityWithMsb(byte[] b1, byte[] b2, int expectedResult) => Assert.Equal((expectedResult == 0), b1.SequenceEqualsWithMsb(b2));
    }
}
