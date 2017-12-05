// <copyright file="ItemIsInEditModeException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Undoable
{
    /// <summary>
    /// An exception thrown when the item is in edit mode and it shouldn't be.
    /// </summary>
    /// <seealso cref="InvalidOperationException"/>
    /// <seealso cref="ITransactionEditableItem"/>
    public class ItemIsInEditModeException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemIsInEditModeException"/> class.
        /// </summary>
        public ItemIsInEditModeException()
            : base(Resources.ItemIsInEditModeExceptionDefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemIsInEditModeException"/> class.
        /// </summary>
        /// <param name="message">The custom message to display.</param>
        public ItemIsInEditModeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemIsInEditModeException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ItemIsInEditModeException(Exception innerException)
            : base(Resources.ItemIsInEditModeExceptionDefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemIsInEditModeException"/> class.
        /// </summary>
        /// <param name="message">The custom message to display.</param>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ItemIsInEditModeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}