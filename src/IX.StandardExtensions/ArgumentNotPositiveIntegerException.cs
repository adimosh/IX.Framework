// <copyright file="ArgumentNotPositiveIntegerException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
#if !STANDARD
using System.Runtime.Serialization;
#endif

namespace IX.StandardExtensions
{
    /// <summary>
    /// An argument exception representing an argument not being a positive integer (like a capacity or a count).
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
#if !STANDARD
    [Serializable]
#endif
    public class ArgumentNotPositiveIntegerException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotPositiveIntegerException"/> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        public ArgumentNotPositiveIntegerException(string argumentName)
            : base(string.Format(Resources.ErrorArgumentNotPositiveInteger, argumentName), argumentName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotPositiveIntegerException"/> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="internalException">The internal exception, if any.</param>
        public ArgumentNotPositiveIntegerException(string argumentName, Exception internalException)
            : base(string.Format(Resources.ErrorArgumentNotPositiveInteger, argumentName), argumentName, internalException)
        {
        }

#if !STANDARD
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotPositiveIntegerException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ArgumentNotPositiveIntegerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}