// <copyright file="SubItemStateChange.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

namespace IX.Undoable
{
    /// <summary>
    /// A state change belonging to a sub-object.
    /// </summary>
    public class SubItemStateChange : StateChange
    {
        /// <summary>
        /// Gets or sets the instance of the sub-object.
        /// </summary>
        public object SubObject { get; set; }

        /// <summary>
        /// Gets or sets the state changes belonging to the sub-object.
        /// </summary>
        public StateChange[] StateChanges { get; set; }
    }
}