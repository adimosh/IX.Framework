// <copyright file="FunctionsListingUnitTest.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Math;
using Xunit;

namespace IX.UnitTests.IX.Math
{
    /// <summary>
    /// Tests for function listings.
    /// </summary>
    public class FunctionsListingUnitTest
    {
        /// <summary>
        /// Test functionality that gets the available functions test.
        /// </summary>
        [Fact(DisplayName = "Test getting available functions and parameters.")]
        public void GetAvailableFunctionsTest()
        {
            using (var service = new ExpressionParsingService())
            {
                var functions = service.GetRegisteredFunctions();

                Assert.True(functions.Length > 0);
            }
        }
    }
}