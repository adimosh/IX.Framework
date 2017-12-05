// <copyright file="UndoLevelStateChange.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Undoable;

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// State changes with undo/redo levels.
    /// </summary>
    /// <seealso cref="IX.Undoable.StateChange" />
    public class UndoLevelStateChange : StateChange
    {
        /// <summary>
        /// Gets or sets the levels.
        /// </summary>
        /// <value>The levels.</value>
        public UndoRedoLevel[] Levels { get; set; }
    }
}