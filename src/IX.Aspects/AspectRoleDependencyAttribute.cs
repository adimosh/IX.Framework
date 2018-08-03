// <copyright file="AspectRoleDependencyAttribute.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Aspects
{
    /// <summary>
    /// An attribute that defines an aspect role dependency.
    /// </summary>
    /// <remarks>
    /// <para>This code is imported and adapted from <a href="https://github.com/vescon">VESCON GmbH</a> under the provisions of the MIT license.</para>
    /// <para>The original project for this adapted code is <a href="https://github.com/vescon/MethodBoundaryAspect.Fody">MethodBoundaryAspect.Fody</a>.</para>
    /// <para>All the code that has been adapted, as well as the documentation and any other pertinent information, is located at the above addresses.</para>
    /// <para>These addresses have been verified and updated today, 3rd August 2018. Please signal incorrect information, change of license and broken addresses as an issue on this project.</para>
    /// </remarks>
    /// <seealso cref="System.Attribute" />
    public class AspectRoleDependencyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspectRoleDependencyAttribute"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="position">The position.</param>
        /// <param name="role">The role.</param>
        public AspectRoleDependencyAttribute(
            AspectDependencyAction action,
            AspectDependencyPosition position,
            string role)
        {
            this.Action = action;
            this.Position = position;
            this.Role = role;
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public AspectDependencyAction Action { get; set; }

        /// <summary>
        /// Gets or sets the relative position.
        /// </summary>
        /// <value>The position.</value>
        public AspectDependencyPosition Position { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }
    }
}