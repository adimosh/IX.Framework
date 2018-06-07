//// <copyright file="IEnumerableLinearForEachBenchmarks.cs" company="Adrian Mos">
//// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
//// </copyright>

//using System.Diagnostics;
//using IX.StandardExtensions.TestUtils;
//using Xunit;
//using Xunit.Abstractions;

//namespace Benchmarks
//{
//    /// <summary>
//    /// Linear ForEach benchmarks.
//    /// </summary>
//    public class IEnumerableLinearForEachBenchmarks
//    {
//        private readonly ITestOutputHelper output;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="IEnumerableLinearForEachBenchmarks"/> class.
//        /// </summary>
//        /// <param name="output">The output.</param>
//        public IEnumerableLinearForEachBenchmarks(ITestOutputHelper output)
//        {
//            this.output = output;
//        }

//        /// <summary>
//        /// Benchmarks for each on array with insignificant load.
//        /// </summary>
//        [Fact(DisplayName = "ForEach on array, insignificant load.")]
//        public void BenchmarkForEachOnArrayWithInsignificantLoad()
//        {
//            var limit = 200000000;

//            var array = DataGenerator.RandomIntegerArray(limit);

//            var sw = new Stopwatch();

//            long foreachTime, newTime;
//            var k = 0;

//            sw.Start();

//            void Act() => k++;

//            foreach (var i in array)
//            {
//                Act();
//            }

//            sw.Stop();

//            foreachTime = sw.ElapsedMilliseconds;

//            sw.Reset();

//            k = 0;

//            sw.Start();

//            array.ForEach((item) => k++);

//            sw.Stop();

//            newTime = sw.ElapsedMilliseconds;

//            this.output.WriteLine($"Benchmark result: foreach - {foreachTime} ms, new - {newTime} ms");

//            Assert.True(foreachTime * 2.5 > newTime);
//        }

//        /// <summary>
//        /// Benchmarks for each on array with light load.
//        /// </summary>
//        [Fact(DisplayName = "ForEach on array, light load.")]
//        public void BenchmarkForEachOnArrayWithLightLoad()
//        {
//            var limit = 1000;

//            var array = DataGenerator.RandomIntegerArray(limit);

//            var sw = new Stopwatch();

//            long foreachTime, newTime;

//            sw.Start();

//            foreach (var i in array)
//            {
//                Delays.DelayByTenMilliseconds();
//            }

//            sw.Stop();

//            foreachTime = sw.ElapsedMilliseconds;

//            sw.Reset();

//            sw.Start();

//            array.ForEach((item) => Delays.DelayByTenMilliseconds());

//            sw.Stop();

//            newTime = sw.ElapsedMilliseconds;

//            this.output.WriteLine($"Benchmark result: foreach - {foreachTime} ms, new - {newTime} ms");

//            Assert.True(foreachTime * 1.1 > newTime);
//        }

//        /// <summary>
//        /// Benchmarks for each on array with medium load.
//        /// </summary>
//        [Fact(DisplayName = "ForEach on array, medium load.")]
//        public void BenchmarkForEachOnArrayWithMediumLoad()
//        {
//            var limit = 1000;

//            var array = DataGenerator.RandomIntegerArray(limit);

//            var sw = new Stopwatch();

//            long foreachTime, newTime;

//            sw.Start();

//            foreach (var i in array)
//            {
//                Delays.DelayByOneHundredMilliseconds();
//            }

//            sw.Stop();

//            foreachTime = sw.ElapsedMilliseconds;

//            sw.Reset();

//            sw.Start();

//            array.ForEach((item) => Delays.DelayByOneHundredMilliseconds());

//            sw.Stop();

//            newTime = sw.ElapsedMilliseconds;

//            this.output.WriteLine($"Benchmark result: foreach - {foreachTime} ms, new - {newTime} ms");

//            Assert.True(foreachTime * 1.1 > newTime);
//        }

//        /// <summary>
//        /// Benchmarks for each on array with heavy load.
//        /// </summary>
//        [Fact(DisplayName = "ForEach on array, heavy load.")]
//        public void BenchmarkForEachOnArrayWithHeavyLoad()
//        {
//            var limit = 100;

//            var array = DataGenerator.RandomIntegerArray(limit);

//            var sw = new Stopwatch();

//            long foreachTime, newTime;

//            sw.Start();

//            foreach (var i in array)
//            {
//                Delays.DelayByOneThousandMilliseconds();
//            }

//            sw.Stop();

//            foreachTime = sw.ElapsedMilliseconds;

//            sw.Reset();

//            sw.Start();

//            array.ForEach((item) => Delays.DelayByOneThousandMilliseconds());

//            sw.Stop();

//            newTime = sw.ElapsedMilliseconds;

//            this.output.WriteLine($"Benchmark result: foreach - {foreachTime} ms, new - {newTime} ms");

//            Assert.True(foreachTime * 1.1 > newTime);
//        }
//    }
//}