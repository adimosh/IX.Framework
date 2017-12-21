// <copyright file="CallableMathematicsFunctionAttribute.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Math.Extensibility
{
    /// <summary>
    /// An attribute that will signal a specific class as being a callable function.
    /// </summary>
    /// <seealso cref="global::System.Attribute" />
    public class CallableMathematicsFunctionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallableMathematicsFunctionAttribute"/> class.
        /// </summary>
        /// <param name="names">The names.</param>
        public CallableMathematicsFunctionAttribute(params string[] names)
        {
            this.Names = names;
        }

        /// <summary>
        /// Gets or sets the names.
        /// </summary>
        /// <value>The names.</value>
        public string[] Names { get; set; }
    }
}