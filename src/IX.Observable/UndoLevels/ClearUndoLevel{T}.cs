// <copyright file="ClearUndoLevel{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// An undo step for when a collection was cleared.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    /// <seealso cref="IX.Observable.UndoRedoLevel" />
    public class ClearUndoLevel<T> : UndoRedoLevel
    {
        /// <summary>
        /// Gets or sets the original items.
        /// </summary>
        /// <value>The original items.</value>
        public T[] OriginalItems { get; set; }
    }
}