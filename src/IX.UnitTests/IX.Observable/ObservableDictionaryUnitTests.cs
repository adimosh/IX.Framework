// <copyright file="ObservableDictionaryUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Observable;
using IX.StandardExtensions.TestUtils;
using Xunit;

namespace IX.UnitTests.IX.Observable
{
    /// <summary>
    /// ObservableDictionary unit tests.
    /// </summary>
    public class ObservableDictionaryUnitTests
    {
        /// <summary>
        /// Generates the test data.
        /// </summary>
        /// <returns>The test data.</returns>
        public static object[][] GenerateData() => new object[][]
                    {
                        new object[]
                        {
                            new ObservableDictionary<int, int>
                            {
                                [1] = 1,
                                [7] = 7,
                                [19] = 19,
                                [23] = 23,
                                [4] = 4,
                            },
                        },
                        new object[]
                        {
                            new ConcurrentObservableDictionary<int, int>
                            {
                                [1] = 1,
                                [7] = 7,
                                [19] = 19,
                                [23] = 23,
                                [4] = 4,
                            },
                        },
                    };

        /// <summary>
        /// Observables the dictionary count.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary ctor, Indexer and Count")]
        public void ObservableDictionaryCount()
        {
            // Arrange
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            var numberOfItems = DataGenerator.RandomNonNegativeInteger(UnitTestConstants.TestsGeneralMagnitude);
            var items = new int[numberOfItems];

            for (var i = 0; i < numberOfItems; i++)
            {
                items[i] = DataGenerator.RandomNonNegativeInteger(numberOfItems);
            }

            var numberOfItemsToCheck = DataGenerator.RandomNonNegativeInteger(numberOfItems);
            var itemsToCheck = new int[numberOfItemsToCheck];

            for (var i = 0; i < numberOfItemsToCheck; i++)
            {
                itemsToCheck[i] = DataGenerator.RandomNonNegativeInteger(numberOfItems);
            }

            // Act
            var x = new ObservableDictionary<int, int>(numberOfItems);

            for (var i = 0; i < numberOfItems; i++)
            {
                x.Add(i, items[i]);
            }

            // Assert
            Assert.True(x.Count == numberOfItems);

            foreach (var i in itemsToCheck)
            {
                Assert.Equal(x[i], items[i]);
            }
        }

        /// <summary>
        /// Observables the dictionary undo at add.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Undo with Add")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryUndoAtAdd(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT
            dict.Add(6, 6);

            Assert.True(dict.ContainsKey(6), "Element not found: 6");

            dict.Undo();

            // ASSERT
            Assert.False(dict.ContainsKey(6), "Element found: 6");
        }

        /// <summary>
        /// Observables the dictionary redo at add.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Redo with undone Add")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryRedoAtAdd(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            dict.Add(6, 6);
            Assert.True(dict.ContainsKey(6), "Element not found: 6");
            dict.Undo();
            Assert.False(dict.ContainsKey(6), "Element found: 6");

            // ACT
            dict.Redo();

            // ASSERT
            Assert.True(dict.ContainsKey(6), "Element not found: 6");
        }

        /// <summary>
        /// Observables the dictionary undo at clear.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Undo with Clear")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryUndoAtClear(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            dict.Clear();

            Assert.False(dict.ContainsKey(1), "Element not found: 1");
            Assert.False(dict.ContainsKey(7), "Element not found: 7");
            Assert.False(dict.ContainsKey(19), "Element not found: 19");
            Assert.False(dict.ContainsKey(23), "Element not found: 23");
            Assert.False(dict.ContainsKey(4), "Element not found: 4");

            // ACT
            dict.Undo();

            // ASSERT
            Assert.True(dict.ContainsKey(1), "Element found: 1");
            Assert.True(dict.ContainsKey(7), "Element found: 7");
            Assert.True(dict.ContainsKey(19), "Element found: 19");
            Assert.True(dict.ContainsKey(23), "Element found: 23");
            Assert.True(dict.ContainsKey(4), "Element found: 4");
        }

        /// <summary>
        /// Observables the dictionary redo at clear.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Redo with undone Clear")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryRedoAtClear(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            dict.Clear();

            dict.Undo();

            Assert.True(dict.ContainsKey(1), "Element not found: 1");
            Assert.True(dict.ContainsKey(7), "Element not found: 7");
            Assert.True(dict.ContainsKey(19), "Element not found: 19");
            Assert.True(dict.ContainsKey(23), "Element not found: 23");
            Assert.True(dict.ContainsKey(4), "Element not found: 4");

            // ACT
            dict.Redo();

            // ASSERT
            Assert.False(dict.ContainsKey(1), "Element found: 1");
            Assert.False(dict.ContainsKey(7), "Element found: 7");
            Assert.False(dict.ContainsKey(19), "Element found: 19");
            Assert.False(dict.ContainsKey(23), "Element found: 23");
            Assert.False(dict.ContainsKey(4), "Element found: 4");
        }

        /// <summary>
        /// Observables the dictionary undo at remove.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Undo with Remove")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryUndoAtRemove(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT
            dict.Remove(7);

            Assert.False(dict.ContainsKey(7), "Element found: 7");

            dict.Undo();

            // ASSERT
            Assert.True(dict.ContainsKey(7), "Element not found: 7");
        }

        /// <summary>
        /// Observables the dictionary redo at remove.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Redo with undone Remove")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryRedoAtRemove(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            dict.Remove(7);
            Assert.False(dict.ContainsKey(7), "Element found: 7");
            dict.Undo();
            Assert.True(dict.ContainsKey(7), "Element not found: 7");

            // ACT
            dict.Redo();

            // ASSERT
            Assert.False(dict.ContainsKey(7));
        }

        /// <summary>
        /// Observables the dictionary undo multiple operations.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Undo with multiple operations")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryUndoMultipleOperations(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            dict.Add(18, 18);
            dict.Remove(7);
            dict.Add(5, 5);
            dict.Clear();
            dict.Add(7, 7);

            // Act & Assert groups
            Assert.Single(dict);
            Assert.Equal(7, dict[7]);

            // Level one
            dict.Undo();
            Assert.Empty(dict);

            // Level two
            dict.Undo();
            Assert.Equal(6, dict.Count);
            Assert.Equal(5, dict[5]);

            // Level three
            dict.Undo();
            Assert.Equal(5, dict.Count);
            Assert.Equal(4, dict[4]);

            // Level four
            dict.Undo();
            Assert.Equal(6, dict.Count);
            Assert.Equal(7, dict[7]);

            // Level two
            dict.Undo();
            Assert.Equal(5, dict.Count);
            Assert.False(dict.ContainsKey(18));

            // Redo
            dict.Redo();
            dict.Redo();
            dict.Redo();
            dict.Redo();
            Assert.True(dict.Count == 0);
        }

        /// <summary>
        /// Observables the dictionary multiple undo operations.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Undo with undo operations past the limit")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryMultipleUndoOperations(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            dict.HistoryLevels = 3;

            dict.Add(15, 15);
            dict.Add(89, 89);
            dict.Add(3, 3);
            dict.Add(2, 2);
            dict.Add(57, 57);

            // ACT
            dict.Undo();
            dict.Undo();
            dict.Undo();
            dict.Undo();
            dict.Undo();
            dict.Undo();

            // ASSERT
            Assert.True(dict.ContainsKey(89), "Element not found: 89");
            Assert.False(dict.ContainsKey(57), "Element found: 57");
            Assert.False(dict.ContainsKey(2), "Element found: 2");
            Assert.False(dict.ContainsKey(3), "Element found: 3");
        }

        /// <summary>
        /// Observables the dictionary multiple redo cutoff.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        [Theory(DisplayName = "ObservableDictionary, Redo cut-off")]
        [MemberData(nameof(GenerateData))]
        public void ObservableDictionaryMultipleRedoCutoff(ObservableDictionary<int, int> dict)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            dict.Add(15, 15);
            dict.Add(89, 89);
            dict.Add(3, 3);
            dict.Add(2, 2);
            dict.Add(57, 57);

            // ACT
            dict.Undo();
            dict.Undo();
            dict.Undo();
            dict.Redo();

            dict.Add(74, 74);

            dict.Redo();
            dict.Redo();
            dict.Redo();

            // ASSERT
            Assert.True(dict.ContainsKey(3), "Element not found: 3");
            Assert.False(dict.ContainsKey(57), "Element found: 57");
            Assert.False(dict.ContainsKey(2), "Element found: 2");
            Assert.True(dict.ContainsKey(74), "Element not found: 74");
        }
    }
}