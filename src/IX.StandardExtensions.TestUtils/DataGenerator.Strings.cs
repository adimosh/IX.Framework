// <copyright file="DataGenerator.Strings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    ///     A static class that is used for generating random data for testing.
    /// </summary>
    public static partial class DataGenerator
    {
        // Character classes
        [NotNull]
        private static readonly char[] LowerCaseAlphaCharacters;

        [NotNull]
        private static readonly char[] UpperCaseAlphaCharacters;

        [NotNull]
        private static readonly char[] NumericCharacters;

        [NotNull] private static readonly char[] BasicSymbolCharacters;

        // Complex character classes
        [NotNull]
        private static readonly char[] AlphaCharacters;

        [NotNull]
        private static readonly char[] AlphaNumericCharacters;

        [NotNull]
        private static readonly char[] AllCharacters;

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString()
        {
            int length;

            lock (R)
            {
                length = R.Next();
            }

            return RandomString(
                R,
                length,
                LowerCaseAlphaCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString(Random random)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                LowerCaseAlphaCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString(int length) => RandomString(
            R,
            length,
            LowerCaseAlphaCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomLowercaseString(
            Random random,
            int length) => RandomString(
            random,
            length,
            LowerCaseAlphaCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString()
        {
            int length;

            lock (R)
            {
                length = R.Next();
            }

            return RandomString(
                R,
                length,
                UpperCaseAlphaCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString(Random random)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                UpperCaseAlphaCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString(int length) => RandomString(
            R,
            length,
            UpperCaseAlphaCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomUppercaseString(
            Random random,
            int length) => RandomString(
            random,
            length,
            UpperCaseAlphaCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomNumericString()
        {
            int length;

            lock (R)
            {
                length = R.Next();
            }

            return RandomString(
                R,
                length,
                NumericCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomNumericString(Random random)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                NumericCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomNumericString(int length) => RandomString(
            R,
            length,
            NumericCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomNumericString(
            Random random,
            int length) => RandomString(
            random,
            length,
            NumericCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString()
        {
            int length;

            lock (R)
            {
                length = R.Next();
            }

            return RandomString(
                R,
                length,
                BasicSymbolCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString(Random random)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                BasicSymbolCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString(int length) => RandomString(
            R,
            length,
            BasicSymbolCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomSymbolString(
            Random random,
            int length) => RandomString(
            random,
            length,
            BasicSymbolCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString()
        {
            int length;

            lock (R)
            {
                length = R.Next();
            }

            return RandomString(
                R,
                length,
                AlphaCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString(Random random)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                AlphaCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString(int length) => RandomString(
            R,
            length,
            AlphaCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphaString(
            Random random,
            int length) => RandomString(
            random,
            length,
            AlphaCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString()
        {
            int length;

            lock (R)
            {
                length = R.Next();
            }

            return RandomString(
                R,
                length,
                AlphaNumericCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString(Random random)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                AlphaNumericCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString(int length) => RandomString(
            R,
            length,
            AlphaNumericCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomAlphanumericString(
            Random random,
            int length) => RandomString(
            random,
            length,
            AlphaNumericCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public static string RandomString()
        {
            int length;

            lock (R)
            {
                length = R.Next();
            }

            return RandomString(
                R,
                length,
                AllCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(Random random)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                AllCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(int length) => RandomString(
            R,
            length,
            AllCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(
            Random random,
            int length) => RandomString(
            random,
            length,
            AllCharacters);

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="fromCharacters">The array of characters from which to generate the string.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(
            Random random,
            char[] fromCharacters)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            int length;

            lock (localRandom)
            {
                length = localRandom.Next();
            }

            return RandomString(
                localRandom,
                length,
                fromCharacters);
        }

        /// <summary>
        ///     Generates a random string.
        /// </summary>
        /// <param name="random">The random generator to use.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <param name="fromCharacters">The array of characters from which to generate the string.</param>
        /// <returns>A random string.</returns>
        public static string RandomString(
            Random random,
            int length,
            char[] fromCharacters)
        {
            var localRandom = Requires.NotNull(
                random,
                nameof(random));
            var localFromCharacters = Requires.NotNull(
                fromCharacters,
                nameof(fromCharacters));
            var randomString = new char[length];

            for (var i = 0; i < length; i++)
            {
                int position;
                lock (localRandom)
                {
                    position = localRandom.Next(localFromCharacters.Length);
                }

                randomString[i] = localFromCharacters[position];
            }

            return new string(randomString);
        }
    }
}