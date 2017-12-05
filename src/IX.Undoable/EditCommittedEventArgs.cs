// <copyright file="EditCommittedEventArgs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System;

namespace IX.Undoable
{
    /// <summary>
    /// Event arguments for edit committed.
    /// </summary>
    public class EditCommittedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditCommittedEventArgs"/> class.
        /// </summary>
        /// <param name="stateChanges">The state changes that have been committed.</param>
        public EditCommittedEventArgs(StateChange[] stateChanges)
        {
            this.StateChanges = stateChanges;
        }

        /// <summary>
        /// Gets the state changes that have been committed.
        /// </summary>
        public StateChange[] StateChanges { get; private set; }
    }
}