// <copyright file="EnqueueUndoLevel{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Undoable;

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// An undo step for when an item was enqueued.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    /// <seealso cref="IX.Undoable.StateChange" />
    public class EnqueueUndoLevel<T> : StateChange
    {
        /// <summary>
        /// Gets or sets the enqueued item.
        /// </summary>
        /// <value>The enqueued item.</value>
        public T EnqueuedItem { get; set; }
    }
}