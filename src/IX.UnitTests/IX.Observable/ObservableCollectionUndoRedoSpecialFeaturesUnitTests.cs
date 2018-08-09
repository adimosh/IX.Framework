// <copyright file="ObservableCollectionUndoRedoSpecialFeaturesUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Observable;
using Xunit;

namespace IX.UnitTests.IX.Observable
{
    /// <summary>
    /// Special undo/redo features test for observable collections.
    /// </summary>
    public class ObservableCollectionUndoRedoSpecialFeaturesUnitTests
    {
        /// <summary>
        /// Generates the test data.
        /// </summary>
        /// <returns>The test data.</returns>
        public static object[][] GeneratePredefinedData() => new object[][]
                    {
                        new object[]
                        {
                            new ObservableList<int>(new[]
                            {
                                1,
                                7,
                                19,
                                23,
                                4,
                            }),
                        },
                        new object[]
                        {
                            new ConcurrentObservableList<int>(new[]
                            {
                                1,
                                7,
                                19,
                                23,
                                4,
                            }),
                        },
                    };

        /// <summary>
        /// Generates the test data.
        /// </summary>
        /// <returns>The test data.</returns>
        public static object[][] GenerateSupressedUndoContextData() => new object[][]
                    {
                        new object[]
                        {
                            new ObservableList<int>(true)
                            {
                                1,
                                7,
                                19,
                                23,
                                4,
                            },
                        },
                        new object[]
                        {
                            new ConcurrentObservableList<int>(true)
                            {
                                1,
                                7,
                                19,
                                23,
                                4,
                            },
                        },
                    };

        /// <summary>
        /// When a list has predefined data (straight from the constructor), it should not be able to undo or redo.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList with predefined data, undo/redo does nothing")]
        [MemberData(nameof(GeneratePredefinedData))]
        public void UnitTest1(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // ACT 1
            list.Undo();

            // ASSERT 1
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // ACT 2
            list.Redo();

            // ASSERT 2
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list has its undo context suppressed, it should not be able to undo or redo.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList with suppressed context, undo/redo does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest2(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // ACT 1
            list.Undo();

            // ASSERT 1
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // ACT 2
            list.Redo();

            // ASSERT 2
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list has its undo context suppressed, then activated, it should not be able to undo or redo.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList with suppressed then activated context, undo/redo does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest3(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // ACT
            list.StartUndo();

            // ASSERT
            list.Undo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            // ASSERT 2
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }
    }
}