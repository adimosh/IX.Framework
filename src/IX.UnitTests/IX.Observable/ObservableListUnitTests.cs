// <copyright file="ObservableListUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Observable;
using Xunit;

namespace IX.UnitTests.IX.Observable
{
    /// <summary>
    /// ObservableList unit tests.
    /// </summary>
    public class ObservableListUnitTests
    {
        /// <summary>
        /// Generates the test data.
        /// </summary>
        /// <returns>The test data.</returns>
        public static object[][] GenerateData() => new object[][]
                    {
                        new object[]
                        {
                            new ObservableList<int>
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
                            new ConcurrentObservableList<int>
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
        /// Tests Undo after Add on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo with Add")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest1(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT
            list.Add(6);

            Assert.True(list.Contains(6));

            list.Undo();

            // ASSERT
            Assert.False(list.Contains(6));
        }

        /// <summary>
        /// Tests Undo, then Redo, after Add on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Redo with undone Add")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest2(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.Add(6);
            Assert.True(list.Contains(6));
            list.Undo();
            Assert.False(list.Contains(6));

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list.Contains(6));
        }

        /// <summary>
        /// Tests Undo after AddRange on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo with AddRange")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest3(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.AddRange(new int[] { 6, 5, 2 });

            Assert.True(list.Contains(6));
            Assert.True(list.Contains(5));
            Assert.True(list.Contains(2));

            // ACT
            list.Undo();

            // ASSERT
            Assert.False(list.Contains(6));
            Assert.False(list.Contains(5));
            Assert.False(list.Contains(2));
            Assert.Equal(5, list.Count);
        }

        /// <summary>
        /// Tests Undo, then Redo, after AddRange on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Redo with undone AddRange")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest4(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.AddRange(new int[] { 6, 5, 2 });
            Assert.True(list.Contains(6));
            Assert.True(list.Contains(5));
            Assert.True(list.Contains(2));
            list.Undo();
            Assert.False(list.Contains(6));
            Assert.False(list.Contains(5));
            Assert.False(list.Contains(2));
            Assert.Equal(5, list.Count);

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list.Contains(6));
            Assert.True(list.Contains(5));
            Assert.True(list.Contains(2));
            Assert.Equal(8, list.Count);
        }

        /// <summary>
        /// Tests Undo after Clear on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo with Clear")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest5(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.Clear();

            Assert.False(list.Contains(1));
            Assert.False(list.Contains(7));
            Assert.False(list.Contains(19));
            Assert.False(list.Contains(23));
            Assert.False(list.Contains(4));

            // ACT
            list.Undo();

            // ASSERT
            Assert.True(list.Contains(1));
            Assert.True(list.Contains(7));
            Assert.True(list.Contains(19));
            Assert.True(list.Contains(23));
            Assert.True(list.Contains(4));
        }

        /// <summary>
        /// Tests Undo, then Redo, after Clear on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Redo with undone Clear")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest6(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.Clear();

            list.Undo();

            Assert.True(list.Contains(1));
            Assert.True(list.Contains(7));
            Assert.True(list.Contains(19));
            Assert.True(list.Contains(23));
            Assert.True(list.Contains(4));

            // ACT
            list.Redo();

            // ASSERT
            Assert.False(list.Contains(1));
            Assert.False(list.Contains(7));
            Assert.False(list.Contains(19));
            Assert.False(list.Contains(23));
            Assert.False(list.Contains(4));
        }

        /// <summary>
        /// Tests Undo after Insert on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo with Insert")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest7(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT
            list.Insert(2, 6);

            Assert.Equal(6, list[2]);
            Assert.Equal(19, list[3]);

            list.Undo();

            // ASSERT
            Assert.False(list.Contains(6));
            Assert.Equal(19, list[2]);
        }

        /// <summary>
        /// Tests Undo, then Redo, after Insert on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Redo with undone Insert")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest8(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.Insert(2, 6);
            Assert.True(list.Contains(6));
            list.Undo();
            Assert.False(list.Contains(6));

            // ACT
            list.Redo();

            // ASSERT
            Assert.Equal(6, list[2]);
            Assert.Equal(19, list[3]);
        }

        /// <summary>
        /// Tests Undo after RemoveAt on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo with RemoveAt")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest9(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT
            list.RemoveAt(2);

            Assert.Equal(23, list[2]);
            Assert.Equal(4, list[3]);

            list.Undo();

            // ASSERT
            Assert.Equal(19, list[2]);
            Assert.Equal(23, list[3]);
        }

        /// <summary>
        /// Tests Undo, then Redo, after RemoveAt on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Redo with undone RemoveAt")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest10(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.RemoveAt(2);
            Assert.Equal(23, list[2]);
            list.Undo();
            Assert.Equal(19, list[2]);

            // ACT
            list.Redo();

            // ASSERT
            Assert.Equal(23, list[2]);
        }

        /// <summary>
        /// Tests Undo after multiple operations on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo with multiple operations")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest11(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.Add(18);
            list.RemoveAt(1);
            list.Insert(3, 5);
            list.Clear();
            list.Add(7);

            // Act & Assert groups
            Assert.Single(list);
            Assert.Equal(7, list[0]);

            // Level one
            list.Undo();
            Assert.Empty(list);

            // Level two
            list.Undo();
            Assert.Equal(6, list.Count);
            Assert.Equal(5, list[3]);

            // Level three
            list.Undo();
            Assert.Equal(5, list.Count);
            Assert.Equal(4, list[3]);

            // Level four
            list.Undo();
            Assert.Equal(6, list.Count);
            Assert.Equal(7, list[1]);

            // Level two
            list.Undo();
            Assert.Equal(5, list.Count);
            Assert.False(list.Contains(18));

            // Redo
            list.Redo();
            list.Redo();
            list.Redo();
            list.Redo();
            Assert.Empty(list);
        }

        /// <summary>
        /// Tests Undo after limit breached on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo with undo operations past the limit")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest12(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.HistoryLevels = 3;

            list.Add(15);
            list.Add(89);
            list.Add(3);
            list.Add(2);
            list.Add(57);

            // ACT
            list.Undo();
            list.Undo();
            list.Undo();
            list.Undo();
            list.Undo();
            list.Undo();

            // ASSERT
            Assert.True(list.Contains(89));
            Assert.False(list.Contains(57));
            Assert.False(list.Contains(2));
            Assert.False(list.Contains(3));
        }

        /// <summary>
        /// Tests redo cut-off after limit breach on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Redo cut-off")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest13(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            list.Add(15);
            list.Add(89);
            list.Add(3);
            list.Add(2);
            list.Add(57);

            // ACT
            list.Undo();
            list.Undo();
            list.Undo();
            list.Redo();

            list.Add(74);

            list.Redo();
            list.Redo();
            list.Redo();

            // ASSERT
            Assert.True(list.Contains(3));
            Assert.False(list.Contains(57));
            Assert.False(list.Contains(2));
            Assert.True(list.Contains(74));
        }

        /// <summary>
        /// Tests undo and redo with RemoveRange (explicit items) on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo/Redo with range of epxlicit items")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest14(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT

            // Remove range
            list.RemoveRange(1, 19, 4);

            // Assert intermediary state
            Assert.Equal(7, list[0]);
            Assert.Equal(23, list[1]);
            Assert.DoesNotContain(1, list);
            Assert.DoesNotContain(19, list);
            Assert.DoesNotContain(4, list);

            // Undo
            list.Undo();

            // Assert intermediary state
            Assert.Equal(1, list[0]);
            Assert.Equal(7, list[1]);
            Assert.Equal(19, list[2]);
            Assert.Equal(23, list[3]);
            Assert.Equal(4, list[4]);

            // Redo
            list.Redo();

            // Assert intermediary state
            Assert.Equal(7, list[0]);
            Assert.Equal(23, list[1]);
            Assert.DoesNotContain(1, list);
            Assert.DoesNotContain(19, list);
            Assert.DoesNotContain(4, list);
        }

        /// <summary>
        /// Tests undo and redo with RemoveRange (starting index) on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo/Redo with range of items by start index")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest15(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT

            // Remove range
            list.RemoveRange(2);

            // Assert intermediary state
            Assert.Equal(1, list[0]);
            Assert.Equal(7, list[1]);
            Assert.DoesNotContain(19, list);
            Assert.DoesNotContain(23, list);
            Assert.DoesNotContain(4, list);

            // Undo
            list.Undo();

            // Assert intermediary state
            Assert.Equal(1, list[0]);
            Assert.Equal(7, list[1]);
            Assert.Equal(19, list[2]);
            Assert.Equal(23, list[3]);
            Assert.Equal(4, list[4]);

            // Redo
            list.Redo();

            // Assert intermediary state
            Assert.Equal(1, list[0]);
            Assert.Equal(7, list[1]);
            Assert.DoesNotContain(19, list);
            Assert.DoesNotContain(23, list);
            Assert.DoesNotContain(4, list);
        }

        /// <summary>
        /// Tests undo and redo with RemoveRange (starting index and length) on an ObservableList.
        /// </summary>
        /// <param name="list">The list.</param>
        [Theory(DisplayName = "ObservableList, Undo/Redo with range of items by start index and length")]
        [MemberData(nameof(GenerateData))]
        public void UnitTest16(ObservableList<int> list)
        {
            // ARRANGE
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.InvokeSynchronouslyOnCurrentThread = true;
            global::IX.StandardExtensions.ComponentModel.EnvironmentSettings.AlwaysSuppressCurrentSynchronizationContext = true;

            // ACT

            // Remove range
            list.RemoveRange(2, 2);

            // Assert intermediary state
            Assert.Equal(1, list[0]);
            Assert.Equal(7, list[1]);
            Assert.DoesNotContain(19, list);
            Assert.DoesNotContain(23, list);
            Assert.Equal(4, list[2]);

            // Undo
            list.Undo();

            // Assert intermediary state
            Assert.Equal(1, list[0]);
            Assert.Equal(7, list[1]);
            Assert.Equal(19, list[2]);
            Assert.Equal(23, list[3]);
            Assert.Equal(4, list[4]);

            // Redo
            list.Redo();

            // Assert intermediary state
            Assert.Equal(1, list[0]);
            Assert.Equal(7, list[1]);
            Assert.DoesNotContain(19, list);
            Assert.DoesNotContain(23, list);
            Assert.Equal(4, list[2]);
        }
    }
}