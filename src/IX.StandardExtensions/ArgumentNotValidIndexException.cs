// <copyright file="ArgumentNotValidIndexException.cs" company="Adrian Mos">
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
    ///     An argument exception representing a value given that cannot be used as an index.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
#if !STANDARD
    [Serializable]
#endif
    [PublicAPI]
    public class ArgumentNotValidIndexException : ArgumentOutOfRangeException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNotValidIndexException" /> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        public ArgumentNotValidIndexException(string argumentName)
            : base(
                argumentName,
                string.Format(
                    Resources.ErrorArgumentNotValidIndex,
                    argumentName))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNotValidIndexException" /> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="internalException">The internal exception, if any.</param>
        public ArgumentNotValidIndexException(
            string argumentName,
            Exception internalException)
            : base(
                string.Format(
                    Resources.ErrorArgumentNotValidIndex,
                    argumentName),
                internalException)
        {
        }

#if !STANDARD
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNotValidIndexException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
        ///     data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected ArgumentNotValidIndexException(
            SerializationInfo info,
            StreamingContext context)
            : base(
                info,
                context)
        {
        }
#endif
    }
}