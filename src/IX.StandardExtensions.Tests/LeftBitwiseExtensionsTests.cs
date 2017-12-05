using Xunit;

namespace IX.StandardExtensions.Tests
{
    public class LeftBitwiseExtensionsTests
    {
        public static object[][] TestDataGenerator() => new object[][]
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

        [Theory(DisplayName = "Left bitwise extension test")]
        [MemberData(nameof(TestDataGenerator))]
        public void TestBitwiseExtensions(byte[] data, int howManyBits, byte[] expectedResult)
        {
            byte[] result = data.LeftShift(howManyBits);

            Assert.True(expectedResult.SequenceEquals(result));
        }
    }
}