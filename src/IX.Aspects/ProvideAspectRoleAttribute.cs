// <copyright file="ProvideAspectRoleAttribute.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Aspects
{
    /// <summary>
    /// Class ProvideAspectRoleAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <remarks>
    /// <para>This code is imported and adapted from <a href="https://github.com/vescon">VESCON GmbH</a> under the provisions of the MIT license.</para>
    /// <para>The original project for this adapted code is <a href="https://github.com/vescon/MethodBoundaryAspect.Fody">MethodBoundaryAspect.Fody</a>.</para>
    /// <para>All the code that has been adapted, as well as the documentation and any other pertinent information, is located at the above addresses.</para>
    /// <para>These addresses have been verified and updated today, 3rd August 2018. Please signal incorrect information, change of license and broken addresses as an issue on this project.</para>
    /// </remarks>
    public class ProvideAspectRoleAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvideAspectRoleAttribute"/> class.
        /// </summary>
        /// <param name="aspectRole">The aspect role.</param>
        public ProvideAspectRoleAttribute(string aspectRole)
        {
            this.AspectRole = aspectRole;
        }

        /// <summary>
        /// Gets or sets the aspect role.
        /// </summary>
        /// <value>The aspect role.</value>
        public string AspectRole { get; set; }
    }
}