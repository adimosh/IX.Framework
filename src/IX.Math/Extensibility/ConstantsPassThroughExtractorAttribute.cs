// <copyright file="ConstantsPassThroughExtractorAttribute.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.Math.Extensibility
{
    /// <summary>
    ///     An attribute that will signal a specific class as containing a constants extraction.
    /// </summary>
    /// <seealso cref="Attribute" />
    [PublicAPI]
    public class ConstantsPassThroughExtractorAttribute : Attribute
    {
        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }
    }
}