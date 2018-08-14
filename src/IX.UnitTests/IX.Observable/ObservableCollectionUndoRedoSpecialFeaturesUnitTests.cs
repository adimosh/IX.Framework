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
        public static object[][] GeneratePredefinedData()
            => new object[][]
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
        public static object[][] GenerateSupressedUndoContextData()
            => new object[][]
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

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list has its undo context suppressed, then activated, it should not be able to undo or redo in either part of the test.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList with suppressed context, undo/redo does nothing, then activated and also does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest4(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Undo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

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

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list has its undo context suppressed, then activated, it should not be able to undo or redo in either part of the test if there is action before starting.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList with suppressed context and acted on, undo/redo does nothing, then activated and also does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest5(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // ACT
            list.RemoveAt(0);

            // ASSERT
            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.StartUndo();

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list has its undo context suppressed, then activated, it should not be able to undo or redo in either part of the test if there is action before starting, but should do something if htere is action after starting.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList with suppressed context and acted on, undo/redo does nothing, then activated and acted on, does only undo the last act")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest6(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // ACT
            list.RemoveAt(0);

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.StartUndo();

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.RemoveAt(0);

            // ASSERT
            Assert.Equal(3, list.Count);
            Assert.True(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.True(list.CanRedo);

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.True(list.CanRedo);

            list.Redo();

            Assert.Equal(3, list.Count);
            Assert.True(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(3, list.Count);
            Assert.True(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list that has items from within the constructor sets its AutomaticallyCaptureItems property to true, it should change existing items to capture them as well.
        /// </summary>
        /// <param name="list">The list.</param>
        [Fact(DisplayName = "ObservableList activating its captures from constructor")]
        public void UnitTest7()
        {
            // ARRANGE
            var capturingList = new ObservableList<CapturedItem>(new[] { new CapturedItem() });

            Assert.Null(capturingList[0].ParentUndoContext);

            // ACT
            capturingList.AutomaticallyCaptureSubItems = true;

            // ASSERT
            Assert.Equal(capturingList, capturingList[0].ParentUndoContext);
        }

        /// <summary>
        /// When a list that has items from adding sets its AutomaticallyCaptureItems property to true, it should change existing items to capture them as well.
        /// </summary>
        /// <param name="list">The list.</param>
        [Fact(DisplayName = "ObservableList activating its captures from adding")]
        public void UnitTest8()
        {
            // ARRANGE
            var capturingList = new ObservableList<CapturedItem>
            {
                new CapturedItem(),
            };

            Assert.Null(capturingList[0].ParentUndoContext);

            // ACT
            capturingList.AutomaticallyCaptureSubItems = true;

            // ASSERT
            Assert.Equal(capturingList, capturingList[0].ParentUndoContext);
        }

        /// <summary>
        /// When a list (captured) has predefined data (straight from the constructor), it should not be able to undo or redo.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList (captured) with predefined data, undo/redo does nothing")]
        [MemberData(nameof(GeneratePredefinedData))]
        public void UnitTest11(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // Capture into a parent context
            var capturingList = new ObservableList<ObservableList<int>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(list.ParentUndoContext);
            capturingList.Add(list);
            Assert.Equal(capturingList, list.ParentUndoContext);

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
        /// When a list (captured) has its undo context suppressed, it should not be able to undo or redo.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList (captured) with suppressed context, undo/redo does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest12(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // Capture into a parent context
            var capturingList = new ObservableList<ObservableList<int>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(list.ParentUndoContext);
            capturingList.Add(list);
            Assert.Equal(capturingList, list.ParentUndoContext);

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
        /// When a list (captured) has its undo context suppressed, then activated, it should not be able to undo or redo.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList (captured) with suppressed then activated context, undo/redo does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest13(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // Capture into a parent context
            var capturingList = new ObservableList<ObservableList<int>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(list.ParentUndoContext);
            capturingList.Add(list);
            Assert.Equal(capturingList, list.ParentUndoContext);

            // ACT
            list.StartUndo();

            // ASSERT
            list.Undo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list (captured) has its undo context suppressed, then activated, it should not be able to undo or redo in either part of the test.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList (captured) with suppressed context, undo/redo does nothing, then activated and also does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest14(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Undo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // Capture into a parent context
            var capturingList = new ObservableList<ObservableList<int>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(list.ParentUndoContext);
            capturingList.Add(list);
            Assert.Equal(capturingList, list.ParentUndoContext);

            // ACT
            list.StartUndo();

            // ASSERT
            list.Undo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list (captured) has its undo context suppressed, then activated, it should not be able to undo or redo in either part of the test if there is action before starting.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList (captured) with suppressed context and acted on, undo/redo does nothing, then activated and also does nothing")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest15(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // Capture into a parent context
            var capturingList = new ObservableList<ObservableList<int>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(list.ParentUndoContext);
            capturingList.Add(list);
            Assert.Equal(capturingList, list.ParentUndoContext);

            // ACT
            list.RemoveAt(0);

            // ASSERT
            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.StartUndo();

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list (captured) has its undo context suppressed, then activated, it should not be able to undo or redo in either part of the test if there is action before starting, but should do something if htere is action after starting.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList (captured) with suppressed context and acted on, undo/redo does nothing, then activated and acted on, does only undo the last act")]
        [MemberData(nameof(GenerateSupressedUndoContextData))]
        public void UnitTest16(ObservableList<int> list)
        {
            // ARRANGE
            // =======
            // Initial assertions
            Assert.Equal(5, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            // Capture into a parent context
            var capturingList = new ObservableList<ObservableList<int>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(list.ParentUndoContext);
            capturingList.Add(list);
            Assert.Equal(capturingList, list.ParentUndoContext);

            // ACT
            list.RemoveAt(0);

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.StartUndo();

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.False(list.CanRedo);

            list.RemoveAt(0);

            // ASSERT
            Assert.Equal(3, list.Count);
            Assert.True(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.True(list.CanRedo);

            list.Undo();

            Assert.Equal(4, list.Count);
            Assert.False(list.CanUndo);
            Assert.True(list.CanRedo);

            list.Redo();

            Assert.Equal(3, list.Count);
            Assert.True(list.CanUndo);
            Assert.False(list.CanRedo);

            list.Redo();

            Assert.Equal(3, list.Count);
            Assert.True(list.CanUndo);
            Assert.False(list.CanRedo);
        }

        /// <summary>
        /// When a list (captured) that has items from within the constructor sets its AutomaticallyCaptureItems property to true, it should change existing items to capture them as well.
        /// </summary>
        /// <param name="list">The list.</param>
        [Fact(DisplayName = "ObservableList (captured) activating its captures from constructor")]
        public void UnitTest17()
        {
            // ARRANGE
            var capturingList = new ObservableList<CapturedItem>(new[] { new CapturedItem() });

            // Capture into a parent context
            var upperCapturingList = new ObservableList<ObservableList<CapturedItem>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(capturingList.ParentUndoContext);
            upperCapturingList.Add(capturingList);
            Assert.Equal(upperCapturingList, capturingList.ParentUndoContext);

            Assert.Null(capturingList[0].ParentUndoContext);

            // ACT
            capturingList.AutomaticallyCaptureSubItems = true;

            // ASSERT
            Assert.Equal(capturingList, capturingList[0].ParentUndoContext);
        }

        /// <summary>
        /// When a list (captured) that has items from adding sets its AutomaticallyCaptureItems property to true, it should change existing items to capture them as well.
        /// </summary>
        /// <param name="list">The list.</param>
        [Fact(DisplayName = "ObservableList (captured) activating its captures from adding")]
        public void UnitTest18()
        {
            // ARRANGE
            var capturingList = new ObservableList<CapturedItem>
            {
                new CapturedItem(),
            };

            // Capture into a parent context
            var upperCapturingList = new ObservableList<ObservableList<CapturedItem>> { AutomaticallyCaptureSubItems = true };
            Assert.Null(capturingList.ParentUndoContext);
            upperCapturingList.Add(capturingList);
            Assert.Equal(upperCapturingList, capturingList.ParentUndoContext);

            Assert.Null(capturingList[0].ParentUndoContext);

            // ACT
            capturingList.AutomaticallyCaptureSubItems = true;

            // ASSERT
            Assert.Equal(capturingList, capturingList[0].ParentUndoContext);
        }
    }
}