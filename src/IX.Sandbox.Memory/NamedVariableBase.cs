// <copyright file="NamedVariableBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A base class for named variable containers.
    /// </summary>
    /// <typeparam name="T">The discreet type of data contained in the variable.</typeparam>
    public abstract class NamedVariableBase<T> : VariableBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedVariableBase{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected NamedVariableBase(string name)
        {
            this.InitializeInternalContext(name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedVariableBase{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        protected NamedVariableBase(string name, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.InitializeInternalContext(name);
        }

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        /// <value>The name of the variable.</value>
        public string Name { get; private set; }

        private void InitializeInternalContext(string name)
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(name);
            }

            // Set parameters
            this.Name = name;
        }
    }
}