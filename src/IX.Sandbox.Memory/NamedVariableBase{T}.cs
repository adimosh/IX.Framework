// <copyright file="NamedVariableBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.Abstractions.Memory;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A base class for named variable containers.
    /// </summary>
    /// <typeparam name="T">The discreet type of data contained in the variable.</typeparam>
    public abstract class NamedVariableBase<T> : VariableBase<T>, INamedVariable<T>
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

        /// <summary>
        /// Compares this instance of a named variable to another.
        /// </summary>
        /// <param name="other">The named variable to compare to.</param>
        /// <returns>An integer value showing the difference between the two, or 0 if they are identical.</returns>
        public abstract int CompareTo(INamedVariable<T> other);

        /// <summary>
        /// Equates this instance of a named variable to another.
        /// </summary>
        /// <param name="other">The named variable to equate to.</param>
        /// <returns><c>true</c> if the variables are identical, <c>false</c> otherwise.</returns>
        public abstract bool Equals(INamedVariable<T> other);

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