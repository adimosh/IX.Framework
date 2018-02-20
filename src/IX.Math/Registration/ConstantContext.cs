// <copyright file="ConstantContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.Math.Extraction;

namespace IX.Math.Registration
{
    /// <summary>
    /// A context for a constant
    /// </summary>
    public class ConstantContext
    {
        private readonly IConstantsExtractor sourceExtractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantContext" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="sourceExtractor">The source extractor.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/>
        /// or
        /// <paramref name="value"/>
        /// or
        /// <paramref name="sourceExtractor"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </exception>
        public ConstantContext(string name, string value, IConstantsExtractor sourceExtractor)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;

            this.OriginalValue = value ?? throw new ArgumentNullException(nameof(value));
            this.sourceExtractor = sourceExtractor ?? throw new ArgumentNullException(nameof(sourceExtractor));
        }

        /// <summary>
        /// Gets the name of the constant.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the original value of the constant.
        /// </summary>
        /// <value>The original value.</value>
        public string OriginalValue { get; }
    }
}