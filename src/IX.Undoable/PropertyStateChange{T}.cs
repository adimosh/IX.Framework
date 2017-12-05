// <copyright file="PropertyStateChange{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

namespace IX.Undoable
{
    /// <summary>
    /// A state change of a property of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of object in the state change.</typeparam>
    public class PropertyStateChange<T> : PropertyStateChange
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
    }
}