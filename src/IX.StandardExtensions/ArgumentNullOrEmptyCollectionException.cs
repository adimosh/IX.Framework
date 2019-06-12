// <copyright file="ArgumentNullOrEmptyCollectionException.cs" company="Adrian Mos">
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
    ///     An argument exception representing a string argument being <c>null</c> (<c>Nothing</c> in Visual Basic) or empty.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
#if !STANDARD
    [Serializable]
#endif
    [PublicAPI]
    public class ArgumentNullOrEmptyCollectionException : ArgumentException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullOrEmptyCollectionException" /> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        public ArgumentNullOrEmptyCollectionException(string argumentName)
            : base(
                string.Format(
                    Resources.ErrorArgumentNullOrEmptyCollection,
                    argumentName), argumentName)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullOrEmptyCollectionException" /> class.
        /// </summary>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="internalException">The internal exception, if any.</param>
        public ArgumentNullOrEmptyCollectionException(
            string argumentName,
            Exception internalException)
            : base(
                string.Format(
                    Resources.ErrorArgumentNullOrEmptyCollection,
                    argumentName), argumentName,
                internalException)
        {
        }

#if !STANDARD
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullOrEmptyCollectionException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being
        ///     thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="StreamingContext" /> that contains contextual information about the source or
        ///     destination.
        /// </param>
        protected ArgumentNullOrEmptyCollectionException(
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