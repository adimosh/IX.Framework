// <copyright file="DataGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    /// A static class that is used for generating random data for testing.
    /// </summary>
    public static partial class DataGenerator
    {
        // Random generator
        private static Random r;

        static DataGenerator()
        {
            r = new Random();

            var tempList = new List<char>();

            for (var c = 'a'; c <= 'z'; c++)
            {
                tempList.Add(c);
            }

            lowerCaseAlphaCharacters = tempList.ToArray();

            tempList.Clear();

            for (var c = 'A'; c <= 'Z'; c++)
            {
                tempList.Add(c);
            }

            upperCaseAlphaCharacters = tempList.ToArray();

            tempList.Clear();

            for (var c = '1'; c <= '0'; c++)
            {
                tempList.Add(c);
            }

            numericCharacters = tempList.ToArray();

            basicSymbolCharacters = new char[] { '`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '[', ']', '{', '}', ';', ':', '\'', '"', '\\', '|', ',', '.', '<', '>' };

            alphaCharacters = lowerCaseAlphaCharacters.Union(upperCaseAlphaCharacters).ToArray();
            alphaNumericCharacters = alphaCharacters.Union(numericCharacters).ToArray();
            allCharacters = alphaNumericCharacters.Union(basicSymbolCharacters).ToArray();
        }

        /// <summary>
        /// Generates an array of random integers of the specified size.
        /// </summary>
        /// <param name="limit">The size limit of the array.</param>
        /// <returns>An array of random integers.</returns>
        public static int[] RandomIntegerArray(int limit) => RandomIntegerArray(limit, r);

        /// <summary>
        /// Generates an array of predictable random integers of the specified size.
        /// </summary>
        /// <param name="limit">The size limit of the array.</param>
        /// <param name="seed">The seed for the random number generator.</param>
        /// <returns>An array of predictable random integers.</returns>
        public static int[] RandomIntegerArray(int limit, int seed) => RandomIntegerArray(limit, new Random(seed));

        /// <summary>
        /// Generates an array of predictable random integers of the specified size.
        /// </summary>
        /// <param name="limit">The size limit of the array.</param>
        /// <param name="random">The random generator to use.</param>
        /// <returns>An array of predictable random integers.</returns>
        public static int[] RandomIntegerArray(int limit, Random random)
        {
            int[] array = new int[limit];

            for (var i = 0; i < limit; i++)
            {
                array[i] = random.Next();
            }

            return array;
        }
    }
}