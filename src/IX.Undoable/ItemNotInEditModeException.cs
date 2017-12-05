// <copyright file="ItemNotInEditModeException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System;

namespace IX.Undoable
{
    /// <summary>
    /// An exception thrown when the item is not in edit mode and it should be.
    /// </summary>
    /// <seealso cref="InvalidOperationException"/>
    /// <seealso cref="ITransactionEditableItem"/>
    public class ItemNotInEditModeException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotInEditModeException"/> class.
        /// </summary>
        public ItemNotInEditModeException()
            : base(Resources.ItemNotInEditModeExceptionDefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotInEditModeException"/> class.
        /// </summary>
        /// <param name="message">The custom message to display.</param>
        public ItemNotInEditModeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotInEditModeException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ItemNotInEditModeException(Exception innerException)
            : base(Resources.ItemNotInEditModeExceptionDefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotInEditModeException"/> class.
        /// </summary>
        /// <param name="message">The custom message to display.</param>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ItemNotInEditModeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}