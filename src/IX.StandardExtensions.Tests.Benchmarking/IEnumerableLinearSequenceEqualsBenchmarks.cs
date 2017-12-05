using System;
using System.Diagnostics;
using IX.StandardExtensions.TestUtils;
using Xunit;
using Xunit.Abstractions;

namespace IX.StandardExtensions.Tests.Benchmarking
{
    public class IEnumerableLinearSequenceEqualsBenchmarks
    {
        private readonly ITestOutputHelper output;

        public IEnumerableLinearSequenceEqualsBenchmarks(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "SequenceEquals over an array of integers.")]
        public void BenchmarkSequenceEqualsForIntegers()
        {
            var limit = 200000000;

            int[] array = DataGenerator.RandomIntegerArray(limit);
            int[] array2 = new int[limit];
            Array.Copy(array, array2, limit);

            var sw = new Stopwatch();

            long foreachTime, newTime;

            sw.Start();

            var res1 = Eq1(array, array2);

            sw.Stop();

            foreachTime = sw.ElapsedMilliseconds;

            sw.Reset();

            sw.Start();

            var res2 = Eq2(array, array2);

            sw.Stop();

            newTime = sw.ElapsedMilliseconds;

            this.output.WriteLine($"Benchmark result: for - {foreachTime} ms, new - {newTime} ms");

            Assert.True(foreachTime * 1.1 > newTime);

            Assert.Equal(res1, res2);
        }

        private static bool Eq1(int[] array, int[] array2)
        {
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool Eq2(int[] array, int[] array2) => array.SequenceEquals(array2);
    }
}
