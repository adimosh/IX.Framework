// <copyright file="AddMultipleUndoLevel{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// An undo step for when some items were added.
    /// </summary>
    /// <typeparam name="T">The type of items.</typeparam>
    /// <seealso cref="UndoRedoLevel" />
    public class AddMultipleUndoLevel<T> : UndoRedoLevel
    {
        /// <summary>
        /// Gets or sets the added items.
        /// </summary>
        /// <value>The added items.</value>
        public T[] AddedItems { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }
    }
}