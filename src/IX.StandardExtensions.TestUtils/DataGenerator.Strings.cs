// <copyright file="DataGenerator.Strings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    /// A static class that is used for generating random data for testing.
    /// </summary>
    public static partial class DataGenerator
    {
        // Character classes
        private static char[] lowerCaseAlphaCharacters;
        private static char[] upperCaseAlphaCharacters;
        private static char[] numericCharacters;
        private static char[] basicSymbolCharacters;

        // Complex character classes
        private static char[] alphaCharacters;
        private static char[] alphaNumericCharacters;
        private static char[] allCharacters;

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString() => RandomString(r, r.Next(), lowerCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString(Random random) => RandomString(random, random.Next(), lowerCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString(int length) => RandomString(r, length, lowerCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString(Random random, int length) => RandomString(random, length, lowerCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString() => RandomString(r, r.Next(), upperCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString(Random random) => RandomString(random, random.Next(), upperCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString(int length) => RandomString(r, length, upperCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString(Random random, int length) => RandomString(random, length, upperCaseAlphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomNumericString() => RandomString(r, r.Next(), numericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomNumericString(Random random) => RandomString(random, random.Next(), numericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomNumericString(int length) => RandomString(r, length, numericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomNumericString(Random random, int length) => RandomString(random, length, numericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString() => RandomString(r, r.Next(), basicSymbolCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString(Random random) => RandomString(random, random.Next(), basicSymbolCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString(int length) => RandomString(r, length, basicSymbolCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString(Random random, int length) => RandomString(random, length, basicSymbolCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString() => RandomString(r, r.Next(), alphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString(Random random) => RandomString(random, random.Next(), alphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString(int length) => RandomString(r, length, alphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString(Random random, int length) => RandomString(random, length, alphaCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString() => RandomString(r, r.Next(), alphaNumericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString(Random random) => RandomString(random, random.Next(), alphaNumericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString(int length) => RandomString(r, length, alphaNumericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString(Random random, int length) => RandomString(random, length, alphaNumericCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomString() => RandomString(r, r.Next(), allCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(Random random) => RandomString(random, random.Next(), allCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(int length) => RandomString(r, length, allCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(Random random, int length) => RandomString(random, length, allCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="fromCharacters">The array of characters from which to generate the string.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(Random random, char[] fromCharacters) => RandomString(random, random.Next(), fromCharacters);

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <param name="fromCharacters">The array of characters from which to generate the string.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(Random random, int length, char[] fromCharacters)
        {
            char[] randomString = new char[length];
            for (var i = 0; i < length; i++)
            {
                randomString[i] = fromCharacters[random.Next(fromCharacters.Length)];
            }

            return new string(randomString);
        }
    }
}