// <copyright file="Benchmark.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    /// Benchmark-related utilities.
    /// </summary>
    public static class Benchmark
    {
        /// <summary>
        /// Benchmarks an operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="elapsedMilliseconds">The elapsed milliseconds, regardless of the result.</param>
        public static void Operation(Action operation, out long elapsedMilliseconds)
        {
            var sw = new Stopwatch();

            sw.Start();

            try
            {
                operation();
            }
            finally
            {
                sw.Stop();
            }

            elapsedMilliseconds = sw.ElapsedMilliseconds;
        }
    }
}