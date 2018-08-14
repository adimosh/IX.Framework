// <copyright file="BlockStateChange.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;

namespace IX.Undoable
{
    /// <summary>
    /// State change that serves as a container for multiple state changes.
    /// </summary>
    /// <seealso cref="IX.Undoable.StateChange" />
    public class BlockStateChange : StateChange
    {
        /// <summary>
        /// Gets or sets the state changes.
        /// </summary>
        /// <value>The state changes.</value>
        public List<StateChange> StateChanges { get; set; }
    }
}