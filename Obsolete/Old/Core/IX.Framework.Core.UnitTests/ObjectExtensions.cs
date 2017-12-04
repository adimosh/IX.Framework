using Xunit;

namespace IX.Framework.Core.UnitTests
{
    public class ObjectExtensions
    {
        [Fact]
        public void Chain_Fact()
        {
            int k = 25;

            int counter = 0;

            var result = k.Chain(p => counter += k).Chain(p => counter *= k);

            Assert.Equal(25, result);
            Assert.Equal(625, counter);
        }
    }
}