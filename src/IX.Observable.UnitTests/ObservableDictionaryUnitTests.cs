// <copyright file="ObservableDictionaryUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.StandardExtensions.TestUtils;
using Xunit;

namespace IX.Observable.UnitTests
{
    /// <summary>
    /// ObservableDictionary unit tests.
    /// </summary>
    public class ObservableDictionaryUnitTests
    {
        /// <summary>
        /// Observables the dictionary count.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary ctor, Indexer and Count")]
        public void ObservableDictionaryCount()
        {
            // Arrange
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var numberOfItems = DataGenerator.RandomInteger(UnitTestConstants.TestsGeneralMagnitude);
            int[] items = new int[numberOfItems];

            for (var i = 0; i < numberOfItems; i++)
            {
                items[i] = DataGenerator.RandomInteger(numberOfItems);
            }

            var numberOfItemsToCheck = DataGenerator.RandomInteger(numberOfItems);
            int[] itemsToCheck = new int[numberOfItemsToCheck];

            for (var i = 0; i < numberOfItemsToCheck; i++)
            {
                itemsToCheck[i] = DataGenerator.RandomInteger(numberOfItems);
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
        [Fact(DisplayName = "ObservableDictionary, Undo with Add")]
        public void ObservableDictionaryUndoAtAdd()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            // ACT
            list.Add(6, 6);

            Assert.True(list.ContainsKey(6));

            list.Undo();

            // ASSERT
            Assert.False(list.ContainsKey(6));
        }

        /// <summary>
        /// Observables the dictionary redo at add.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Redo with undone Add")]
        public void ObservableDictionaryRedoAtAdd()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            list.Add(6, 6);
            Assert.True(list.ContainsKey(6));
            list.Undo();
            Assert.False(list.ContainsKey(6));

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list.ContainsKey(6));
        }

        /// <summary>
        /// Observables the dictionary undo at clear.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Undo with Clear")]
        public void ObservableDictionaryUndoAtClear()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            list.Clear();

            Assert.False(list.ContainsKey(1));
            Assert.False(list.ContainsKey(7));
            Assert.False(list.ContainsKey(19));
            Assert.False(list.ContainsKey(23));
            Assert.False(list.ContainsKey(4));

            // ACT
            list.Undo();

            // ASSERT
            Assert.True(list.ContainsKey(1));
            Assert.True(list.ContainsKey(7));
            Assert.True(list.ContainsKey(19));
            Assert.True(list.ContainsKey(23));
            Assert.True(list.ContainsKey(4));
        }

        /// <summary>
        /// Observables the dictionary redo at clear.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Redo with undone Clear")]
        public void ObservableDictionaryRedoAtClear()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            list.Clear();

            list.Undo();

            Assert.True(list.ContainsKey(1));
            Assert.True(list.ContainsKey(7));
            Assert.True(list.ContainsKey(19));
            Assert.True(list.ContainsKey(23));
            Assert.True(list.ContainsKey(4));

            // ACT
            list.Redo();

            // ASSERT
            Assert.False(list.ContainsKey(1));
            Assert.False(list.ContainsKey(7));
            Assert.False(list.ContainsKey(19));
            Assert.False(list.ContainsKey(23));
            Assert.False(list.ContainsKey(4));
        }

        /// <summary>
        /// Observables the dictionary undo at remove.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Undo with Remove")]
        public void ObservableDictionaryUndoAtRemove()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            // ACT
            list.Remove(7);

            Assert.False(list.ContainsKey(7));

            list.Undo();

            // ASSERT
            Assert.True(list.ContainsKey(7));
        }

        /// <summary>
        /// Observables the dictionary redo at remove.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Redo with undone Remove")]
        public void ObservableDictionaryRedoAtRemove()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            list.Remove(7);
            Assert.False(list.ContainsKey(7));
            list.Undo();
            Assert.True(list.ContainsKey(7));

            // ACT
            list.Redo();

            // ASSERT
            Assert.False(list.ContainsKey(7));
        }

        /// <summary>
        /// Observables the dictionary undo multiple operations.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Undo with multiple operations")]
        public void ObservableDictionaryUndoMultipleOperations()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            list.Add(18, 18);
            list.Remove(7);
            list.Add(5, 5);
            list.Clear();
            list.Add(7, 7);

            // Act & Assert groups
            Assert.True(list.Count == 1);
            Assert.True(list[7] == 7);

            // Level one
            list.Undo();
            Assert.True(list.Count == 0);

            // Level two
            list.Undo();
            Assert.True(list.Count == 6);
            Assert.True(list[5] == 5);

            // Level three
            list.Undo();
            Assert.True(list.Count == 5);
            Assert.True(list[4] == 4);

            // Level four
            list.Undo();
            Assert.True(list.Count == 6);
            Assert.True(list[7] == 7);

            // Level two
            list.Undo();
            Assert.True(list.Count == 5);
            Assert.False(list.ContainsKey(18));

            // Redo
            list.Redo();
            list.Redo();
            list.Redo();
            list.Redo();
            Assert.True(list.Count == 0);
        }

        /// <summary>
        /// Observables the dictionary multiple undo operations.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Undo with undo operations past the limit")]
        public void ObservableDictionaryMultipleUndoOperations()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            list.HistoryLevels = 3;

            list.Add(15, 15);
            list.Add(89, 89);
            list.Add(3, 3);
            list.Add(2, 2);
            list.Add(57, 57);

            // ACT
            list.Undo();
            list.Undo();
            list.Undo();
            list.Undo();
            list.Undo();
            list.Undo();

            // ASSERT
            Assert.True(list.ContainsKey(89));
            Assert.False(list.ContainsKey(57));
            Assert.False(list.ContainsKey(2));
            Assert.False(list.ContainsKey(3));
        }

        /// <summary>
        /// Observables the dictionary multiple redo cutoff.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary, Redo cut-off")]
        public void ObservableDictionaryMultipleRedoCutoff()
        {
            // ARRANGE
            StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronously = true;

            var list = new ObservableDictionary<int, int>
            {
                [1] = 1,
                [7] = 7,
                [19] = 19,
                [23] = 23,
                [4] = 4,
            };

            list.Add(15, 15);
            list.Add(89, 89);
            list.Add(3, 3);
            list.Add(2, 2);
            list.Add(57, 57);

            // ACT
            list.Undo();
            list.Undo();
            list.Undo();
            list.Redo();

            list.Add(74, 74);

            list.Redo();
            list.Redo();
            list.Redo();

            // ASSERT
            Assert.True(list.ContainsKey(3));
            Assert.False(list.ContainsKey(57));
            Assert.False(list.ContainsKey(2));
            Assert.True(list.ContainsKey(74));
        }
    }
}