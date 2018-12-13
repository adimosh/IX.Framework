// <copyright file="ArgumentsException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
#if !STANDARD
using System.Runtime.Serialization;
#endif

namespace IX.StandardExtensions
{
    /// <summary>
    /// An exception representing something wrong with a set of arguments as a whole, rather than just one.
    /// </summary>
#if !STANDARD
    [Serializable]
#endif
    public class ArgumentsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsException"/> class.
        /// </summary>
        /// <param name="argumentNames">The names of the arguments that have an invalid value.</param>
        public ArgumentsException(params string[] argumentNames)
            : base(string.Format(Resources.AnInvalidSetOfArgumentsWasSpecifiedArgumentNames, string.Join(", ", argumentNames)))
        {
            this.ArgumentNames = argumentNames ?? throw new ArgumentNullException(nameof(argumentNames));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="argumentNames">The names of the arguments that have an invalid value.</param>
        public ArgumentsException(Exception innerException, params string[] argumentNames)
            : base(string.Format(Resources.AnInvalidSetOfArgumentsWasSpecifiedArgumentNames, string.Join(", ", argumentNames)), innerException)
        {
            this.ArgumentNames = argumentNames ?? throw new ArgumentNullException(nameof(argumentNames));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsException"/> class.
        /// </summary>
        /// <param name="message">The custom exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="argumentNames">The names of the arguments that have an invalid value.</param>
        public ArgumentsException(string message, Exception innerException, params string[] argumentNames)
            : base(string.Format(message, string.Join(", ", argumentNames)), innerException)
        {
            this.ArgumentNames = argumentNames ?? throw new ArgumentNullException(nameof(argumentNames));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsException"/> class.
        /// </summary>
        /// <param name="message">The custom exception message.</param>
        /// <param name="argumentNames">The names of the arguments that have an invalid value.</param>
        protected ArgumentsException(string message, params string[] argumentNames)
            : base(string.Format(message, string.Join(", ", argumentNames)))
        {
            this.ArgumentNames = argumentNames ?? throw new ArgumentNullException(nameof(argumentNames));
        }

#if !STANDARD
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected ArgumentsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.ArgumentNames = (string[])info.GetValue(nameof(this.ArgumentNames), typeof(string[]));
        }
#endif

        /// <summary>
        /// Gets the argument names.
        /// </summary>
        public string[] ArgumentNames { get; private set; }

#if !STANDARD
        /// <summary>
        /// Sets the <see cref="SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(this.ArgumentNames), this.ArgumentNames, typeof(string[]));
        }
#endif
    }
}