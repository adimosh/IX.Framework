using System;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkDotNet.Reports.Summary summary = BenchmarkRunner.Run<ForEachExtensions>();
        }
    }
}
