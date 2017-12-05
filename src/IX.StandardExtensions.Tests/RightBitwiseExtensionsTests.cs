using Xunit;

namespace IX.StandardExtensions.Tests
{
    public class RightBitwiseExtensionsTests
    {
        public static object[][] TestDataGenerator() => new object[][]
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

        [Theory(DisplayName = "Left bitwise extension test")]
        [MemberData(nameof(TestDataGenerator))]
        public void TestBitwiseExtensions(byte[] data, int howManyBits, byte[] expectedResult)
        {
            byte[] result = data.RightShift(howManyBits);

            Assert.True(expectedResult.SequenceEquals(result));
        }
    }
}