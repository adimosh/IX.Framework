// <copyright file="ExpressionNotValidLogicallyException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
#if NET452
using System.Runtime.Serialization;
#endif

namespace IX.Math
{
    /// <summary>
    /// Thrown when an expression is not internally logical or consistent.
    /// </summary>
#if NET452
    [Serializable]
#endif
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

#if NET452
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionNotValidLogicallyException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ExpressionNotValidLogicallyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}