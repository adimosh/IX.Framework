// <copyright file="ArgumentInvalidPathException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
#if FULLDOTNET
using System.Runtime.Serialization;
#endif

namespace IX.StandardExtensions
{
    /// <summary>
    /// An argument exception representing a path argument that is invalid.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
#if FULLDOTNET
    [Serializable]
#endif
    public class ArgumentInvalidPathException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidPathException"/> class.
        /// </summary>
        public ArgumentInvalidPathException()
            : base(Resources.ErrorWrongArgumentType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidPathException"/> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        public ArgumentInvalidPathException(string argumentName)
            : base(Resources.ErrorWrongArgumentType, argumentName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidPathException"/> class.
        /// </summary>
        /// <param name="message">A custom message for the thrown exception.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public ArgumentInvalidPathException(string message, string argumentName)
            : base(message, argumentName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidPathException"/> class.
        /// </summary>
        /// <param name="internalException">The internal exception, if any.</param>
        public ArgumentInvalidPathException(Exception internalException)
            : base(Resources.ErrorWrongArgumentType, internalException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidPathException"/> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="internalException">The internal exception, if any.</param>
        public ArgumentInvalidPathException(string argumentName, Exception internalException)
            : base(Resources.ErrorWrongArgumentType, argumentName, internalException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidPathException"/> class.
        /// </summary>
        /// <param name="message">A custom message for the thrown exception.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="internalException">The internal exception, if any.</param>
        public ArgumentInvalidPathException(string message, string argumentName, Exception internalException)
            : base(message, internalException)
        {
        }

#if FULLDOTNET
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidPathException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ArgumentInvalidPathException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}