// <copyright file="OnMethodBoundaryAspect.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Reflection;

namespace IX.Aspects
{
    /// <summary>
    /// A base class for method boundary aspects.
    /// </summary>
    /// <remarks>
    /// <para>This code is imported and adapted from <a href="https://github.com/vescon">VESCON GmbH</a> under the provisions of the MIT license.</para>
    /// <para>The original project for this adapted code is <a href="https://github.com/vescon/MethodBoundaryAspect.Fody">MethodBoundaryAspect.Fody</a>.</para>
    /// <para>All the code that has been adapted, as well as the documentation and any other pertinent information, is located at the above addresses.</para>
    /// <para>These addresses have been verified and updated today, 3rd August 2018. Please signal incorrect information, change of license and broken addresses as an issue on this project.</para>
    /// </remarks>
    /// <seealso cref="System.Attribute" />
#if FULLDOTNET
    [Serializable]
#endif
    public abstract class OnMethodBoundaryAspect : Attribute
    {
        /// <summary>
        /// Called when the weaved method is called.
        /// </summary>
        /// <param name="arg">The method execution arguments.</param>
        public virtual void OnEntry(MethodExecutionArgs arg)
        {
        }

        /// <summary>
        /// Called when the weaved method has exited normally.
        /// </summary>
        /// <param name="arg">The method execution arguments.</param>
        public virtual void OnExit(MethodExecutionArgs arg)
        {
        }

        /// <summary>
        /// Called when the weaved method terminated with an exception.
        /// </summary>
        /// <param name="arg">The method execution arguments.</param>
        public virtual void OnException(MethodExecutionArgs arg)
        {
        }

        /// <summary>
        /// Makes compile-time validation of the method that is being weaved.
        /// </summary>
        /// <param name="method">The method being weaved.</param>
        /// <returns><c>true</c> if the method is valid at compile-time, <c>false</c> otherwise.</returns>
        public virtual bool CompileTimeValidate(MethodBase method) => true;
    }
}