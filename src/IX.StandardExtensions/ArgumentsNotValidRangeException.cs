// <copyright file="ArgumentsNotValidRangeException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.Serialization;

namespace IX.StandardExtensions
{
    /// <summary>
    /// An exception representing that a certain set of arguments do not form a valid range of values.
    /// </summary>
#if !STANDARD
    [Serializable]
#endif
    public class ArgumentsNotValidRangeException : ArgumentsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsNotValidRangeException"/> class.
        /// </summary>
        /// <param name="argumentNames">The names of the arguments that form an invalid value range.</param>
        public ArgumentsNotValidRangeException(params string[] argumentNames)
            : base(Resources.TheProvidedArgumentsDoNotFormAValidRangeOfValuesArguments, argumentNames)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsNotValidRangeException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="argumentNames">The names of the arguments that form an invalid value range.</param>
        public ArgumentsNotValidRangeException(Exception innerException, params string[] argumentNames)
            : base(Resources.TheProvidedArgumentsDoNotFormAValidRangeOfValuesArguments, innerException, argumentNames)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsNotValidRangeException"/> class.
        /// </summary>
        /// <param name="message">The custom exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="argumentNames">The names of the arguments that form an invalid value range.</param>
        public ArgumentsNotValidRangeException(string message, Exception innerException, params string[] argumentNames)
            : base(message, innerException, argumentNames)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsNotValidRangeException"/> class.
        /// </summary>
        /// <param name="message">The custom exception message.</param>
        /// <param name="argumentNames">The names of the arguments that form an invalid value range.</param>
        protected ArgumentsNotValidRangeException(string message, params string[] argumentNames)
            : base(message, argumentNames)
        {
        }

#if !STANDARD
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsNotValidRangeException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected ArgumentsNotValidRangeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}