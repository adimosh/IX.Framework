// <copyright file="ArgumentOfWrongTypeException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System;

namespace IX.Observable
{
    /// <summary>
    /// An exception raised when a boxed argument is of the wrong type.
    /// </summary>
    public class ArgumentOfWrongTypeException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentOfWrongTypeException"/> class.
        /// </summary>
        public ArgumentOfWrongTypeException()
            : base(Resources.ArgumentOfWrongTypeException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentOfWrongTypeException"/> class.
        /// </summary>
        /// <param name="parameterName">The name of the parameter of the wrong type.</param>
        public ArgumentOfWrongTypeException(string parameterName)
            : base(string.Format(Resources.ArgumentOfWrongTypeExceptionWithParameter, parameterName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentOfWrongTypeException"/> class.
        /// </summary>
        /// <param name="innerException">The exception that is causing this exception.</param>
        public ArgumentOfWrongTypeException(Exception innerException)
            : base(Resources.ArgumentOfWrongTypeException, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentOfWrongTypeException"/> class.
        /// </summary>
        /// <param name="message">A custom message for this exception.</param>
        /// <param name="innerException">The exception that is causing this exception.</param>
        public ArgumentOfWrongTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}