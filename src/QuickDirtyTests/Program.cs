using System;
using QuickDirtyTests.ObservableCollections;

namespace QuickDirtyTests
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ObservableListAndReadOnlyCompositeCollectionBenchmarks.Test();

            Console.ReadLine();
        }
    }
}