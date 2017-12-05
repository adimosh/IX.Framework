// <copyright file="ExceptionOccurredEventArgs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// Event arguments for an event handler detailing exceptions occurring in different threads.
    /// </summary>
    public class ExceptionOccurredEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionOccurredEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception that has occurred.</param>
        public ExceptionOccurredEventArgs(Exception exception)
        {
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the exception that has occurred.
        /// </summary>
        public Exception Exception { get; private set; }
    }
}