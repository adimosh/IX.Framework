// <copyright file="ExpressionNotValidLogicallyException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Math
{
    /// <summary>
    /// Thrown when an expression is not internally logical or consistent.
    /// </summary>
    public class ExpressionNotValidLogicallyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionNotValidLogicallyException"/> class.
        /// </summary>
        public ExpressionNotValidLogicallyException()
            : base(Resources.NotValidInternally)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionNotValidLogicallyException"/> class.
        /// </summary>
        /// <param name="message">A custom message for the thrown exception.</param>
        public ExpressionNotValidLogicallyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionNotValidLogicallyException"/> class.
        /// </summary>
        /// <param name="internalException">The internal exception, if any.</param>
        public ExpressionNotValidLogicallyException(Exception internalException)
            : base(Resources.NotValidInternally, internalException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionNotValidLogicallyException"/> class.
        /// </summary>
        /// <param name="message">A custom message for the thrown exception.</param>
        /// <param name="internalException">The internal exception, if any.</param>
        public ExpressionNotValidLogicallyException(string message, Exception internalException)
            : base(message, internalException)
        {
        }
    }
}