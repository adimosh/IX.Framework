// <copyright file="AspectSkipPropertiesAttribute.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Aspects
{
    /// <summary>
    /// Marks for skipping during weaving.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <remarks>
    /// <para>This code is imported and adapted from <a href="https://github.com/vescon">VESCON GmbH</a> under the provisions of the MIT license.</para>
    /// <para>The original project for this adapted code is <a href="https://github.com/vescon/MethodBoundaryAspect.Fody">MethodBoundaryAspect.Fody</a>.</para>
    /// <para>All the code that has been adapted, as well as the documentation and any other pertinent information, is located at the above addresses.</para>
    /// <para>These addresses have been verified and updated today, 3rd August 2018. Please signal incorrect information, change of license and broken addresses as an issue on this project.</para>
    /// </remarks>
    public class AspectSkipPropertiesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspectSkipPropertiesAttribute"/> class.
        /// </summary>
        /// <param name="skipProperties">if set to <c>true</c>, skip properties when weaving.</param>
        public AspectSkipPropertiesAttribute(bool skipProperties)
        {
            this.SkipProperties = skipProperties;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to skip properties when weaving.
        /// </summary>
        /// <value><c>true</c> if properties should be skipped when weaving; otherwise, <c>false</c>.</value>
        public bool SkipProperties { get; set; }
    }
}