// <copyright file="PropertyStateChange.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

namespace IX.Undoable
{
    /// <summary>
    /// A state change of a property.
    /// </summary>
    public abstract class PropertyStateChange : StateChange
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; set; }
    }
}