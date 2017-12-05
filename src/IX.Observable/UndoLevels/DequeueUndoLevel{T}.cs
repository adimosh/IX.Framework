// <copyright file="DequeueUndoLevel{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// An undo step for when an item was dequeued.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    /// <seealso cref="IX.Observable.UndoRedoLevel" />
    public class DequeueUndoLevel<T> : UndoRedoLevel
    {
        /// <summary>
        /// Gets or sets the dequeued item.
        /// </summary>
        /// <value>The dequeued item.</value>
        public T DequeuedItem { get; set; }
    }
}