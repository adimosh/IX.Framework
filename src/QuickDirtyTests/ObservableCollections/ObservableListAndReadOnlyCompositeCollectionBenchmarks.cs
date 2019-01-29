using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using IX.Observable;
using IX.StandardExtensions.TestUtils;

namespace QuickDirtyTests.ObservableCollections
{
    internal static class ObservableListAndReadOnlyCompositeCollectionBenchmarks
    {
        [SuppressMessage("ReSharper", "LocalizableElement", Justification = "We're doing quick and dirty tests here.")]
        internal static void Test()
        {
            EnvironmentSettings.DisableUndoable = true;
            IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;

            const int limit = 2_000;

            var sw = new Stopwatch();

            sw.Start();
            var l = new List<string>(limit + 1);

            for (var i = 0; i < limit; i++)
            {
                l.Add(DataGenerator.RandomString(new Random(), 5));
            }

            sw.Stop();

            Console.WriteLine($"Data creation took {sw.ElapsedMilliseconds} to complete.");

            sw.Restart();
            var lb = new ObservableCollection<string>(l);
            lb.CollectionChanged += OnCollectionChanged;
            sw.Stop();
            Console.WriteLine($"ObservableCollection took {sw.ElapsedMilliseconds} to initialize.");
            sw.Restart();
            foreach (var item in lb)
            {
                CollectionDo(lb, item);
            }
            sw.Stop();
            Console.WriteLine($"ObservableCollection took {sw.ElapsedMilliseconds} to act.");

            sw.Restart();
            var l1 = new ConcurrentObservableList<string>(l);
            l1.CollectionChanged += OnCollectionChanged;
            sw.Stop();
            Console.WriteLine($"ConcurrentObservableList took {sw.ElapsedMilliseconds} to initialize.");
            sw.Restart();
            foreach (var item in l1)
            {
                CollectionDo(l1, item);
            }
            sw.Stop();
            Console.WriteLine($"ConcurrentObservableList took {sw.ElapsedMilliseconds} to act.");

            sw.Restart();
            var l2 = new ConcurrentObservableList<string>();
            l2.AddRange(l);
            l2.CollectionChanged += OnCollectionChanged;
            sw.Stop();
            Console.WriteLine($"ConcurrentObservableList_addrange took {sw.ElapsedMilliseconds} to initialize.");
            sw.Restart();
            foreach (var item in l2)
            {
                CollectionDo(l2, item);
            }
            sw.Stop();
            Console.WriteLine($"ConcurrentObservableList_addrange took {sw.ElapsedMilliseconds} to act.");

            sw.Restart();
            var l3 = new ObservableReadOnlyCompositeList<string>();
            l3.SetList(new ObservableCollection<string>());
            l3.SetList(lb);
            l3.CollectionChanged += OnCollectionChanged;
            sw.Stop();
            Console.WriteLine($"ObservableReadOnlyCompositeList took {sw.ElapsedMilliseconds} to initialize.");
            sw.Restart();
            foreach (var item in l3)
            {
                CollectionDo(l3, item);
            }
            sw.Stop();
            Console.WriteLine($"ObservableReadOnlyCompositeList took {sw.ElapsedMilliseconds} to act.");

            sw.Restart();
            var l4 = new ObservableReadOnlyCompositeList<string>();
            l4.SetList(lb);
            l4.CollectionChanged += OnCollectionChanged;
            sw.Stop();
            Console.WriteLine($"Raw_ObservableReadOnlyCompositeList took {sw.ElapsedMilliseconds} to initialize.");
            sw.Restart();
            foreach (var item in l4)
            {
                CollectionDo(l4, item);
            }
            sw.Stop();
            Console.WriteLine($"Raw_ObservableReadOnlyCompositeList took {sw.ElapsedMilliseconds} to act.");
        }

        private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => Thread.SpinWait(1000);

        private static void CollectionDo(IEnumerable<string> originalCollection, string item)
        {
            foreach (var comparisonItem in originalCollection)
            {
                if (comparisonItem == item)
                {
                    Thread.SpinWait(100);
                }
            }
        }
    }
}