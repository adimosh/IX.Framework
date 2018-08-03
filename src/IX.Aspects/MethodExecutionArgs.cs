// <copyright file="MethodExecutionArgs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Reflection;

namespace IX.Aspects
{
    /// <summary>
    /// Method execution arguments that are passed between weaved stages of methods.
    /// </summary>
    /// <remarks>
    /// <para>This code is imported and adapted from <a href="https://github.com/vescon">VESCON GmbH</a> under the provisions of the MIT license.</para>
    /// <para>The original project for this adapted code is <a href="https://github.com/vescon/MethodBoundaryAspect.Fody">MethodBoundaryAspect.Fody</a>.</para>
    /// <para>All the code that has been adapted, as well as the documentation and any other pertinent information, is located at the above addresses.</para>
    /// <para>These addresses have been verified and updated today, 3rd August 2018. Please signal incorrect information, change of license and broken addresses as an issue on this project.</para>
    /// </remarks>
    public class MethodExecutionArgs
    {
        /// <summary>
        /// Gets or sets the instance of the object that the weaved method is executing on.
        /// </summary>
        /// <value>The instance.</value>
        public object Instance { get; set; }

        /// <summary>
        /// Gets or sets the method that is currently executed.
        /// </summary>
        /// <value>The method.</value>
        public MethodBase Method { get; set; }

        /// <summary>
        /// Gets or sets the arguments that the method was called with.
        /// </summary>
        /// <value>The arguments.</value>
        public object[] Arguments { get; set; }

        /// <summary>
        /// Gets or sets the return value of the method, shoud there be any.
        /// </summary>
        /// <value>The return value.</value>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Gets or sets the exception that was thrown by the method, should there be any.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the method execution tag, which is carried over through all weaving stages.
        /// </summary>
        /// <value>The method execution tag.</value>
        public object MethodExecutionTag { get; set; }
    }
}