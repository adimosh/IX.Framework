// <copyright file="ItemNotCapturedIntoUndoContextException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Undoable
{
    /// <summary>
    /// An exception thrown when the item has not been captured by an undo context.
    /// </summary>
    /// <seealso cref="InvalidOperationException"/>
    /// <seealso cref="IUndoableItem"/>
    public class ItemNotCapturedIntoUndoContextException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotCapturedIntoUndoContextException"/> class.
        /// </summary>
        public ItemNotCapturedIntoUndoContextException()
            : base(Resources.ItemNotCapturedIntoUndoContextException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotCapturedIntoUndoContextException"/> class.
        /// </summary>
        /// <param name="message">The custom message to display.</param>
        public ItemNotCapturedIntoUndoContextException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotCapturedIntoUndoContextException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ItemNotCapturedIntoUndoContextException(Exception innerException)
            : base(Resources.ItemNotCapturedIntoUndoContextException, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotCapturedIntoUndoContextException"/> class.
        /// </summary>
        /// <param name="message">The custom message to display.</param>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ItemNotCapturedIntoUndoContextException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}