// <copyright file="SandboxSymbol.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.ComponentModel;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A sandbox symbol.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    public abstract class SandboxSymbol : DisposableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SandboxSymbol"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">name
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        protected SandboxSymbol(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the symbol.
        /// </summary>
        /// <value>The symbol name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the raw object that is represented by this value.
        /// </summary>
        /// <value>The raw object.</value>
        public abstract object RawObject { get; set; }

        /// <summary>
        /// Gets or sets the string representation of this value value.
        /// </summary>
        /// <value>The string representation.</value>
        public abstract string StringRepresentation { get; set; }
    }
}