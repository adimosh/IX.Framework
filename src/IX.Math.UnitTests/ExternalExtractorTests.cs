// <copyright file="ExternalExtractorTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Reflection;
using Xunit;

namespace IX.Math.UnitTests
{
    /// <summary>
    /// A class containing tests for external library support in IX.Math
    /// </summary>
    public class ExternalExtractorTests
    {
        /// <summary>
        /// Tests extractors from external libraries.
        /// </summary>
        [Fact(DisplayName = "Test extractors from external libraries")]
        public void Test1()
        {
            var eps = new ExpressionParsingService();
            eps.RegisterFunctionsAssembly(typeof(ExternalExtractorTests).GetTypeInfo().Assembly);

            ComputedExpression interpreted = eps.Interpret("1+silly+3");

            Assert.NotNull(interpreted);
            Assert.True(interpreted.RecognizedCorrectly);
            Assert.Contains(interpreted.ParameterNames, p => p == "stupid");
            Assert.DoesNotContain(interpreted.ParameterNames, p => p == "silly");
        }
    }
}