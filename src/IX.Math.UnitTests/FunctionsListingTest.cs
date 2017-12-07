using Xunit;

namespace IX.Math.UnitTests
{
    public class FunctionsListingTest
    {
        [Fact(DisplayName = "Test getting available functions and parameters.")]
        public void GetAvailableFunctionsTest()
        {
            using (var service = new ExpressionParsingService())
            {
                string[] functions = service.GetRegisteredFunctions();

                Assert.True(functions.Length > 0);
            }
        }
    }
}