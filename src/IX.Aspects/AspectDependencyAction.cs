// <copyright file="AspectDependencyAction.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Aspects
{
    /// <summary>
    /// Aspect dependency actions.
    /// </summary>
    /// <remarks>
    /// <para>This code is imported and adapted from <a href="https://github.com/vescon">VESCON GmbH</a> under the provisions of the MIT license.</para>
    /// <para>The original project for this adapted code is <a href="https://github.com/vescon/MethodBoundaryAspect.Fody">MethodBoundaryAspect.Fody</a>.</para>
    /// <para>All the code that has been adapted, as well as the documentation and any other pertinent information, is located at the above addresses.</para>
    /// <para>These addresses have been verified and updated today, 3rd August 2018. Please signal incorrect information, change of license and broken addresses as an issue on this project.</para>
    /// </remarks>
    public enum AspectDependencyAction
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Order.
        /// </summary>
        Order,
    }
}