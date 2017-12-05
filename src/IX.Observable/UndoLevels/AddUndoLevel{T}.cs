// <copyright file="AddUndoLevel{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// An undo step for when an item was added.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    /// <seealso cref="IX.Observable.UndoRedoLevel" />
    public class AddUndoLevel<T> : UndoRedoLevel
    {
        /// <summary>
        /// Gets or sets the added item.
        /// </summary>
        /// <value>The added item.</value>
        public T AddedItem { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }
    }
}