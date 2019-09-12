// <copyright file="StopRetryingException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;
#if !STANDARD
using System.Runtime.Serialization;
#endif

namespace IX.StandardExtensions
{
    /// <summary>
    /// An exception that, when thrown, signals the thread it's on to stop retrying an operation.
    /// </summary>
    /// <seealso cref="System.Exception" />
#if !STANDARD
    [Serializable]
#endif
    [PublicAPI]
    public class StopRetryingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopRetryingException"/> class.
        /// </summary>
        public StopRetryingException()
            : base(Resources.ErrorStopRetrying)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopRetryingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public StopRetryingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopRetryingException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public StopRetryingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !STANDARD
        /// <summary>
        /// Initializes a new instance of the <see cref="StopRetryingException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected StopRetryingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}