using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Configs;
using IX.StandardExtensions;

namespace Benchmarks
{
    [ClrJob, CoreJob]
    [MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByJob)]
    [HtmlExporter]
    public class ForEachExtensions
    {
        private byte[] data;

        [Params(100, 200, 500, 1000, 5000, 10000, 100000, 1000000)]
        public int n;

        [GlobalSetup]
        public void Setup()
        {
            var r = new Random();

            this.data = new byte[this.n];

            r.NextBytes(this.data);
        }

        [Benchmark(Baseline = true)]
        public long ForKeyword()
        {
            long result = 0;
            for (var i = 0; i < this.n; i++)
            {
                result += AddOne(this.data[i]);
            }
            return result;
        }

        [Benchmark]
        public long ForEachKeyword()
        {
            long result = 0;
            foreach (var b in this.data)
            {
                result += AddOne(b);
            }
            return result;
        }

        [Benchmark]
        public long ForEachStaticMethod()
        {
            long result = 0;
            Array.ForEach(this.data, b => result += AddOne(b));
            return result;
        }

        [Benchmark]
        public long ForEachStandardMethod()
        {
            long result = 0;
            this.data.ForEach(b => result += AddOne(b));
            return result;
        }

        [Benchmark]
        public long ForEachStandardParallelMethod()
        {
            long result = 0;
            this.data.ParallelForEach(b => result += AddOne(b));
            return result;
        }

        private static int AddOne(byte b)
        {
            byte[] bArray = new byte[20];
            new Random().NextBytes(bArray);
            Array.Sort(bArray);

            int a = 5;
            double d = 20D;

            int c = (int)(d / a);

            double q = 1;
            q *= Math.Sqrt(a);

            return b + 1;
        }
    }
}
