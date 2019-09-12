// <copyright file="NotImplementedByDesignException.cs" company="Adrian Mos">
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
    /// The exception that is thrown when a requested method or operation is not implemented.
    /// </summary>
    /// <seealso cref="System.NotImplementedException" />
#if !STANDARD
    [Serializable]
#endif
    [PublicAPI]
    public class NotImplementedByDesignException : NotImplementedException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotImplementedByDesignException"/> class.
        /// </summary>
        public NotImplementedByDesignException()
            : this(Resources.ErrorNotImplementedByDesign, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotImplementedByDesignException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public NotImplementedByDesignException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotImplementedByDesignException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        public NotImplementedByDesignException(string message, Exception inner)
            : base(message, inner)
        {
        }

#if !STANDARD
        /// <summary>
        /// Initializes a new instance of the <see cref="NotImplementedByDesignException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected NotImplementedByDesignException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}