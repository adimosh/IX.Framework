// <copyright file="ObservableListUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using Xunit;

namespace IX.Observable.UnitTests
{
    /// <summary>
    /// ObservableList unit tests.
    /// </summary>
    public class ObservableListUnitTests
    {
        /// <summary>
        /// Observables the list undo at add.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Undo with Add")]
        public void ObservableListUndoAtAdd()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            // ACT
            list.Add(6);

            Assert.True(list.Contains(6));

            list.Undo();

            // ASSERT
            Assert.False(list.Contains(6));
        }

        /// <summary>
        /// Observables the list redo at add.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Redo with undone Add")]
        public void ObservableListRedoAtAdd()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

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
        /// Observables the list undo at add.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Undo with AddRange")]
        public void ObservableListUndoAtAddRange()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

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
            Assert.True(list.Count == 5);
        }

        /// <summary>
        /// Observables the list redo at add.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Redo with undone AddRange")]
        public void ObservableListRedoAtAddRange()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            list.AddRange(new int[] { 6, 5, 2 });
            Assert.True(list.Contains(6));
            Assert.True(list.Contains(5));
            Assert.True(list.Contains(2));
            list.Undo();
            Assert.False(list.Contains(6));
            Assert.False(list.Contains(5));
            Assert.False(list.Contains(2));
            Assert.True(list.Count == 5);

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list.Contains(6));
            Assert.True(list.Contains(5));
            Assert.True(list.Contains(2));
            Assert.True(list.Count == 8);
        }

        /// <summary>
        /// Observables the list undo at clear.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Undo with Clear")]
        public void ObservableListUndoAtClear()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

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
        /// Observables the list redo at clear.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Redo with undone Clear")]
        public void ObservableListRedoAtClear()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

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
        /// Observables the list undo at insert.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Undo with Insert")]
        public void ObservableListUndoAtInsert()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            // ACT
            list.Insert(2, 6);

            Assert.True(list[2] == 6);
            Assert.True(list[3] == 19);

            list.Undo();

            // ASSERT
            Assert.False(list.Contains(6));
            Assert.True(list[2] == 19);
        }

        /// <summary>
        /// Observables the list redo at insert.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Redo with undone Insert")]
        public void ObservableListRedoAtInsert()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            list.Insert(2, 6);
            Assert.True(list.Contains(6));
            list.Undo();
            Assert.False(list.Contains(6));

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list[2] == 6);
            Assert.True(list[3] == 19);
        }

        /// <summary>
        /// Observables the list undo at remove at.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Undo with RemoveAt")]
        public void ObservableListUndoAtRemoveAt()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            // ACT
            list.RemoveAt(2);

            Assert.True(list[2] == 23);
            Assert.True(list[3] == 4);

            list.Undo();

            // ASSERT
            Assert.True(list[2] == 19);
            Assert.True(list[3] == 23);
        }

        /// <summary>
        /// Observables the list redo at remove at.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Redo with undone RemoveAt")]
        public void ObservableListRedoAtRemoveAt()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            list.RemoveAt(2);
            Assert.True(list[2] == 23);
            list.Undo();
            Assert.True(list[2] == 19);

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list[2] == 23);
        }

        /// <summary>
        /// Observables the list undo multiple operations.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Undo with multiple operations")]
        public void ObservableListUndoMultipleOperations()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            list.Add(18);
            list.RemoveAt(1);
            list.Insert(3, 5);
            list.Clear();
            list.Add(7);

            // Act & Assert groups
            Assert.True(list.Count == 1);
            Assert.True(list[0] == 7);

            // Level one
            list.Undo();
            Assert.True(list.Count == 0);

            // Level two
            list.Undo();
            Assert.True(list.Count == 6);
            Assert.True(list[3] == 5);

            // Level three
            list.Undo();
            Assert.True(list.Count == 5);
            Assert.True(list[3] == 4);

            // Level four
            list.Undo();
            Assert.True(list.Count == 6);
            Assert.True(list[1] == 7);

            // Level two
            list.Undo();
            Assert.True(list.Count == 5);
            Assert.False(list.Contains(18));

            // Redo
            list.Redo();
            list.Redo();
            list.Redo();
            list.Redo();
            Assert.True(list.Count == 0);
        }

        /// <summary>
        /// Observables the list multiple undo operations.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Undo with undo operations past the limit")]
        public void ObservableListMultipleUndoOperations()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

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
        /// Observables the list multiple redo cutoff.
        /// </summary>
        [Fact(DisplayName = "ObservableList, Redo cut-off")]
        public void ObservableListMultipleRedoCutoff()
        {
            // ARRANGE
            var list = new ObservableList<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

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
    }
}