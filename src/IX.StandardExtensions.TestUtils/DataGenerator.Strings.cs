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
        public static string RandomLowercaseString()
        {
            int length;

            lock (r)
            {
                length = r.Next();
            }

            return RandomString(r, length, lowerCaseAlphaCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString(Random random)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, lowerCaseAlphaCharacters);
        }

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
        public static string RandomUppercaseString()
        {
            int length;

            lock (r)
            {
                length = r.Next();
            }

            return RandomString(r, length, upperCaseAlphaCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString(Random random)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, upperCaseAlphaCharacters);
        }

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
        public static string RandomNumericString()
        {
            int length;

            lock (r)
            {
                length = r.Next();
            }

            return RandomString(r, length, numericCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomNumericString(Random random)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, numericCharacters);
        }

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
        public static string RandomSymbolString()
        {
            int length;

            lock (r)
            {
                length = r.Next();
            }

            return RandomString(r, length, basicSymbolCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString(Random random)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, basicSymbolCharacters);
        }

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
        public static string RandomAlphaString()
        {
            int length;

            lock (r)
            {
                length = r.Next();
            }

            return RandomString(r, length, alphaCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString(Random random)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, alphaCharacters);
        }

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
        public static string RandomAlphanumericString()
        {
            int length;

            lock (r)
            {
                length = r.Next();
            }

            return RandomString(r, length, alphaNumericCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString(Random random)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, alphaNumericCharacters);
        }

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
        public static string RandomString()
        {
            int length;

            lock (r)
            {
                length = r.Next();
            }

            return RandomString(r, length, allCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(Random random)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, allCharacters);
        }

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
        public static string RandomString(Random random, char[] fromCharacters)
        {
            int length;

            lock (random)
            {
                length = random.Next();
            }

            return RandomString(random, length, fromCharacters);
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <param name="fromCharacters">The array of characters from which to generate the string.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(Random random, int length, char[] fromCharacters)
        {
            var randomString = new char[length];
            int position;

            for (var i = 0; i < length; i++)
            {
                lock (r)
                {
                    position = random.Next(fromCharacters.Length);
                }

                randomString[i] = fromCharacters[position];
            }

            return new string(randomString);
        }
    }
}