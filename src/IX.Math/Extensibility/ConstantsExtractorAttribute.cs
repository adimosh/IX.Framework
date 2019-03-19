// <copyright file="ConstantsExtractorAttribute.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Math.Extensibility
{
    /// <summary>
    ///     An attribute that will signal a specific class as containing a constants extraction.
    /// </summary>
    /// <seealso cref="Attribute" />
    public class ConstantsExtractorAttribute : Attribute
    {
        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }
    }
}