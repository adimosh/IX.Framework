// <copyright file="FunctionsListingTest.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using Xunit;

namespace IX.Math.UnitTests
{
    /// <summary>
    /// Tests for function listings.
    /// </summary>
    public class FunctionsListingTest
    {
        /// <summary>
        /// Test functionality that gets the available functions test.
        /// </summary>
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