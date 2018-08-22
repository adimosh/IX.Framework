// <copyright file="ObservableListCapturedItemsUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Specialized;
using IX.Observable;
using Xunit;

namespace IX.UnitTests.IX.Observable
{
    /// <summary>
    /// Captured items test for ObservableList.
    /// </summary>
    public class ObservableListCapturedItemsUnitTests
    {
        /// <summary>
        /// ObservableList captured item undo/redo.
        /// </summary>
        [Fact(DisplayName = "ObservableList captured item undo/redo")]
        public void UnitTest1()
        {
            // ARRANGE
            var item1 = new CapturedItem();

            var list = new ObservableList<CapturedItem>
            {
                AutomaticallyCaptureSubItems = true,
            };

            // ACT
            list.Add(item1);

            item1.TestProperty = "aaa";
            item1.TestProperty = "bbb";
            item1.TestProperty = "ccc";

            list.Undo();

            // ASSERT
            Assert.Equal("bbb", item1.TestProperty);
        }

        /// <summary>
        /// ObservableList non-captured item undo/redo.
        /// </summary>
        [Fact(DisplayName = "ObservableList non-captured item undo/redo")]
        public void UnitTest2()
        {
            // ARRANGE
            var item1 = new CapturedItem();

            var list = new ObservableList<CapturedItem>
            {
                AutomaticallyCaptureSubItems = false,
            };

            // ACT
            list.Add(item1);

            item1.TestProperty = "aaa";
            item1.TestProperty = "bbb";
            item1.TestProperty = "ccc";

            list.Undo();

            // ASSERT
            Assert.Equal("ccc", item1.TestProperty);
            Assert.Empty(list);
        }

        /// <summary>
        /// ObservableList captured item undo/redo and further use.
        /// </summary>
        [Fact(DisplayName = "ObservableList captured item undo/redo and further use")]
        public void UnitTest3()
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            var list = new ObservableList<CapturedItem>
            {
#pragma warning disable IDE0009 // Member access should be qualified. - It shouldn't, but there is a bug in the analyzer
                new CapturedItem { TestProperty = "1" },
                new CapturedItem { TestProperty = "2" },
                new CapturedItem { TestProperty = "3" },
                new CapturedItem { TestProperty = "4" },
                new CapturedItem { TestProperty = "5" },
#pragma warning restore IDE0009 // Member access should be qualified.
            };

            list.AutomaticallyCaptureSubItems = true;

            NotifyCollectionChangedAction cca = NotifyCollectionChangedAction.Add;

            list.CollectionChanged += (sender, e) => Assert.Equal(cca, e.Action);

            // ACT
            list.AddRange(
                new[]
                {
#pragma warning disable IDE0009 // Member access should be qualified. - It shouldn't, but there is a bug in the analyzer
                    new CapturedItem { TestProperty = "6" },
                    new CapturedItem { TestProperty = "7" },
                    new CapturedItem { TestProperty = "8" },
                    new CapturedItem { TestProperty = "9" },
#pragma warning restore IDE0009 // Member access should be qualified.
                });

            // ASSERT
            cca = NotifyCollectionChangedAction.Remove;

            list.Undo();

            Assert.Equal(5, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");

            cca = NotifyCollectionChangedAction.Add;

            list.Redo();

            Assert.Equal(9, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");
            Assert.True(list[5].TestProperty == "6");
            Assert.True(list[6].TestProperty == "7");
            Assert.True(list[7].TestProperty == "8");
            Assert.True(list[8].TestProperty == "9");

            cca = NotifyCollectionChangedAction.Remove;
            list.RemoveAt(6);

            Assert.Equal(8, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");
            Assert.True(list[5].TestProperty == "6");
            Assert.True(list[6].TestProperty == "8");
            Assert.True(list[7].TestProperty == "9");

            list[7].TestProperty = "10";

            Assert.True(list[7].TestProperty == "10");

            // No collection changed here
            list.Undo();

            Assert.Equal(8, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");
            Assert.True(list[5].TestProperty == "6");
            Assert.True(list[6].TestProperty == "8");
            Assert.True(list[7].TestProperty == "9");

            cca = NotifyCollectionChangedAction.Remove;
            list.RemoveRange(2, 4);

            Assert.Equal(4, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "8");
            Assert.True(list[3].TestProperty == "9");

            cca = NotifyCollectionChangedAction.Reset;
            list.Undo();

            Assert.Equal(8, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");
            Assert.True(list[5].TestProperty == "6");
            Assert.True(list[6].TestProperty == "8");
            Assert.True(list[7].TestProperty == "9");

            cca = NotifyCollectionChangedAction.Add;
            CapturedItem[] items = new[]
                {
                    new CapturedItem { TestProperty = "a" },
                    new CapturedItem { TestProperty = "b" },
                };

            list.InsertRange(5, items);

            Assert.Equal(10, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");
            Assert.True(list[5].TestProperty == "a");
            Assert.True(list[6].TestProperty == "b");
            Assert.True(list[7].TestProperty == "6");
            Assert.True(list[8].TestProperty == "8");
            Assert.True(list[9].TestProperty == "9");

            cca = NotifyCollectionChangedAction.Remove;
            list.Undo();

            Assert.Equal(8, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");
            Assert.True(list[5].TestProperty == "6");
            Assert.True(list[6].TestProperty == "8");
            Assert.True(list[7].TestProperty == "9");

            cca = NotifyCollectionChangedAction.Add;
            list.Redo();

            Assert.Equal(10, list.Count);
            Assert.True(list[0].TestProperty == "1");
            Assert.True(list[1].TestProperty == "2");
            Assert.True(list[2].TestProperty == "3");
            Assert.True(list[3].TestProperty == "4");
            Assert.True(list[4].TestProperty == "5");
            Assert.True(list[5].TestProperty == "a");
            Assert.True(list[6].TestProperty == "b");
            Assert.True(list[7].TestProperty == "6");
            Assert.True(list[8].TestProperty == "8");
            Assert.True(list[9].TestProperty == "9");
        }
    }
}