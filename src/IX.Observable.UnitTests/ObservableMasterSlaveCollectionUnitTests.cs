﻿// <copyright file="ObservableMasterSlaveCollectionUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using Xunit;

namespace IX.Observable.UnitTests
{
    /// <summary>
    /// ObservableMasterSlaveCollection tests.
    /// </summary>
    public class ObservableMasterSlaveCollectionUnitTests
    {
        /// <summary>
        /// Observables the master slave collection undo at add.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Undo with Add")]
        public void ObservableMasterSlaveCollectionUndoAtAdd()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection redo at add.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Redo with undone Add")]
        public void ObservableMasterSlaveCollectionRedoAtAdd()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection undo at clear.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Undo with Clear")]
        public void ObservableMasterSlaveCollectionUndoAtClear()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection redo at clear.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Redo with undone Clear")]
        public void ObservableMasterSlaveCollectionRedoAtClear()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection undo at insert.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Undo with Insert")]
        public void ObservableMasterSlaveCollectionUndoAtInsert()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection redo at insert.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Redo with undone Insert")]
        public void ObservableMasterSlaveCollectionRedoAtInsert()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection undo at remove at.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Undo with RemoveAt")]
        public void ObservableMasterSlaveCollectionUndoAtRemoveAt()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection redo at remove at.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Redo with undone RemoveAt")]
        public void ObservableMasterSlaveCollectionRedoAtRemoveAt()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection undo multiple operations.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Undo with multiple operations")]
        public void ObservableMasterSlaveCollectionUndoMultipleOperations()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection multiple undo operations.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Undo with undo operations past the limit")]
        public void ObservableMasterSlaveCollectionMultipleUndoOperations()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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
        /// Observables the master slave collection multiple redo cutoff.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection, Redo cut-off")]
        public void ObservableMasterSlaveCollectionMultipleRedoCutoff()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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

        /// <summary>
        /// Observables the master slave collection undo at add with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Undo with Add")]
        public void ObservableMasterSlaveCollectionUndoAtAddWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

            // ACT
            list.Add(6);

            Assert.True(list.Contains(6));

            list.Undo();

            // ASSERT
            Assert.False(list.Contains(6));

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection redo at add with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Redo with undone Add")]
        public void ObservableMasterSlaveCollectionRedoAtAddWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

            list.Add(6);
            Assert.True(list.Contains(6));
            list.Undo();
            Assert.False(list.Contains(6));

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list.Contains(6));

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection undo at clear with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Undo with Clear")]
        public void ObservableMasterSlaveCollectionUndoAtClearWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

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

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection redo at clear with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Redo with undone Clear")]
        public void ObservableMasterSlaveCollectionRedoAtClearWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

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

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection undo at insert with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Undo with Insert")]
        public void ObservableMasterSlaveCollectionUndoAtInsertWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

            // ACT
            list.Insert(2, 6);

            Assert.True(list[2] == 6);
            Assert.True(list[3] == 19);

            list.Undo();

            // ASSERT
            Assert.False(list.Contains(6));
            Assert.True(list[2] == 19);

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection redo at insert with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Redo with undone Insert")]
        public void ObservableMasterSlaveCollectionRedoAtInsertWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
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

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list[2] == 6);
            Assert.True(list[3] == 19);

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection undo at remove at with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Undo with RemoveAt")]
        public void ObservableMasterSlaveCollectionUndoAtRemoveAtWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

            // ACT
            list.RemoveAt(2);

            Assert.True(list[2] == 23);
            Assert.True(list[3] == 4);

            list.Undo();

            // ASSERT
            Assert.True(list[2] == 19);
            Assert.True(list[3] == 23);

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection redo at remove at with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Redo with undone RemoveAt")]
        public void ObservableMasterSlaveCollectionRedoAtRemoveAtWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

            list.RemoveAt(2);
            Assert.True(list[2] == 23);
            list.Undo();
            Assert.True(list[2] == 19);

            // ACT
            list.Redo();

            // ASSERT
            Assert.True(list[2] == 23);

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection undo multiple operations with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Undo with multiple operations")]
        public void ObservableMasterSlaveCollectionUndoMultipleOperationsWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

            list.Add(18);
            list.RemoveAt(1);
            list.Insert(3, 5);
            list.Clear();
            list.Add(7);

            // Act & Assert groups
            Assert.True(list.Count == 4);
            Assert.True(list[0] == 7);

            // Level one
            list.Undo();
            Assert.True(list.Count == 3);

            // Level two
            list.Undo();
            Assert.True(list.Count == 9);
            Assert.True(list[3] == 5);

            // Level three
            list.Undo();
            Assert.True(list.Count == 8);
            Assert.True(list[3] == 4);

            // Level four
            list.Undo();
            Assert.True(list.Count == 9);
            Assert.True(list[1] == 7);

            // Level two
            list.Undo();
            Assert.True(list.Count == 8);
            Assert.False(list.Contains(18));

            // Redo
            list.Redo();
            list.Redo();
            list.Redo();
            list.Redo();
            Assert.True(list.Count == 3);

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection multiple undo operations with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Undo with undo operations past the limit")]
        public void ObservableMasterSlaveCollectionMultipleUndoOperationsWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

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

            Assert.True(list.Contains(-5));
        }

        /// <summary>
        /// Observables the master slave collection multiple redo cutoff with slave.
        /// </summary>
        [Fact(DisplayName = "ObservableMasterSlaveCollection with a slave, Redo cut-off")]
        public void ObservableMasterSlaveCollectionMultipleRedoCutoffWithSlave()
        {
            // ARRANGE
            var list = new ObservableMasterSlaveCollection<int>
            {
                1,
                7,
                19,
                23,
                4,
            };

            var slaveCollection = new ObservableList<int>
            {
                -1,
                -5,
                -12,
            };

            list.SetSlaveList(slaveCollection);

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

            Assert.True(list.Contains(-5));
        }
    }
}