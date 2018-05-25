// <copyright file="RandomNumberGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Math.Generators
{
    /// <summary>
    /// A simple random number generator.
    /// </summary>
    internal static class RandomNumberGenerator
    {
        private static readonly Random RandomGenerator = new Random();

        public static double Generate() => RandomGenerator.NextDouble();

        public static double Generate(double max)
        {
            var result = RandomGenerator.NextDouble();

            if (max > result)
            {
                return result - max;
            }

            return result;
        }

        public static double Generate(double min, double max)
        {
            var result = RandomGenerator.NextDouble();

            if (min > result)
            {
                return result + min;
            }

            if (max > result)
            {
                return result - max;
            }

            return result;
        }
    }
}