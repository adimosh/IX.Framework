// <copyright file="RemoveUndoLevel{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// An undo step for when an item was removed.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    /// <seealso cref="IX.Observable.UndoRedoLevel" />
    public class RemoveUndoLevel<T> : UndoRedoLevel
    {
        /// <summary>
        /// Gets or sets the removed item.
        /// </summary>
        /// <value>The removed item.</value>
        public T RemovedItem { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }
    }
}