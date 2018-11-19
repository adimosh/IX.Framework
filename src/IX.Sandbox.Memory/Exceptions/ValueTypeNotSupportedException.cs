// <copyright file="ValueTypeNotSupportedException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
#if !STANDARD
using System.Runtime.Serialization;
#endif

namespace IX.Sandbox.Memory.Exceptions
{
    /// <summary>
    /// Class ValueTypeNotSupportedException.
    /// </summary>
    /// <seealso cref="InvalidOperationException" />
#if !STANDARD
    [Serializable]
#endif
    public class ValueTypeNotSupportedException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTypeNotSupportedException"/> class.
        /// </summary>
        public ValueTypeNotSupportedException()
            : this(Resources.TheContainedValueTypeIsNotSupported)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTypeNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ValueTypeNotSupportedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTypeNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
        public ValueTypeNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !STANDARD
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTypeNotSupportedException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ValueTypeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}