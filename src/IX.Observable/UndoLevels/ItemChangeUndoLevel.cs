﻿// <copyright file="ItemChangeUndoLevel.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using IX.Undoable;

namespace IX.Observable.UndoLevels
{
    /// <summary>
    /// An undo level related to changes in a sub-item.
    /// </summary>
    /// <seealso cref="UndoRedoLevel" />
    public class ItemChangeUndoLevel : UndoRedoLevel
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public IUndoableItem Instance { get; set; }

        /// <summary>
        /// Gets or sets the state changes.
        /// </summary>
        /// <value>The state changes.</value>
        public StateChange[] StateChanges { get; set; }
    }
}