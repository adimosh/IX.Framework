// <copyright file="FunctionCallNotValidLogicallyException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Math
{
    /// <summary>
    /// Thrown when an expression is not internally logical or consistent.
    /// </summary>
    public class FunctionCallNotValidLogicallyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionCallNotValidLogicallyException"/> class.
        /// </summary>
        public FunctionCallNotValidLogicallyException()
            : base(Resources.FunctionCallNotValid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionCallNotValidLogicallyException"/> class.
        /// </summary>
        /// <param name="message">A custom message for the thrown exception.</param>
        public FunctionCallNotValidLogicallyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionCallNotValidLogicallyException"/> class.
        /// </summary>
        /// <param name="internalException">The internal exception, if any.</param>
        public FunctionCallNotValidLogicallyException(Exception internalException)
            : base(Resources.FunctionCallNotValid, internalException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionCallNotValidLogicallyException"/> class.
        /// </summary>
        /// <param name="message">A custom message for the thrown exception.</param>
        /// <param name="internalException">The internal exception, if any.</param>
        public FunctionCallNotValidLogicallyException(string message, Exception internalException)
            : base(message, internalException)
        {
        }
    }
}