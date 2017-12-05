// <copyright file="ChangeAtUndoLevel{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// A change at a specified index.
    /// </summary>
    /// <typeparam name="T">The type of the item changed.</typeparam>
    /// <seealso cref="IX.Observable.UndoRedoLevel" />
    public class ChangeAtUndoLevel<T> : UndoRedoLevel
    {
        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        /// <value>The old value.</value>
        public T OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        /// <value>The new value.</value>
        public T NewValue { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }
    }
}