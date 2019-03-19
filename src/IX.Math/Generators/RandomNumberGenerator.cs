// <copyright file="RandomNumberGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions;

namespace IX.Math.Generators
{
    /// <summary>
    ///     A simple random number generator.
    /// </summary>
    internal static class RandomNumberGenerator
    {
        private static readonly Random RandomGenerator = new Random();

        public static double Generate() => Generate(
            0D,
            double.MaxValue);

        public static double Generate(double max) => Generate(
            0D,
            max);

        public static double Generate(
            double min,
            double max)
        {
            if (max <= min)
            {
                throw new ArgumentsNotValidRangeException(
                    nameof(min),
                    nameof(max));
            }

            if (max <= 0D)
            {
                throw new ArgumentNotPositiveException(nameof(max));
            }

            if (min < 0D)
            {
                throw new ArgumentNotPositiveException(nameof(min));
            }

            return min + (max - min) * RandomGenerator.NextDouble();
        }

        public static long GenerateInt() => GenerateInt(
            0,
            long.MaxValue);

        public static long GenerateInt(long max) => GenerateInt(
            0,
            max);

        public static long GenerateInt(
            long min,
            long max)
        {
            if (max <= min)
            {
                throw new ArgumentsNotValidRangeException(
                    nameof(min),
                    nameof(max));
            }

            if (max <= 0L)
            {
                throw new ArgumentNotPositiveException(nameof(max));
            }

            if (min < 0L)
            {
                throw new ArgumentNotPositiveException(nameof(min));
            }

            return min + (long)(((double)max - min) * RandomGenerator.NextDouble());
        }
    }
}