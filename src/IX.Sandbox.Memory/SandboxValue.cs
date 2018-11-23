// <copyright file="SandboxValue.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.Sandbox.Memory.Exceptions;
using IX.StandardExtensions.ComponentModel;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A value that is placed in the sandbox.
    /// </summary>
    /// <remarks>
    /// <para>All values in the sandbox are treated as heap objects.</para>
    /// <para>When implementing a new kind of sandbox value, please keep in mind that the value lives a single entity, and cannot be split (say, when simulating an array).</para>
    /// </remarks>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    public abstract class SandboxValue : DisposableBase
    {
        private int timesCaptured;

        /// <summary>
        /// Initializes a new instance of the <see cref="SandboxValue"/> class.
        /// </summary>
        protected SandboxValue()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets or sets the raw object that is represented by this value.
        /// </summary>
        /// <value>The raw object.</value>
        public abstract object RawObject { get; protected set; }

        /// <summary>
        /// Gets or sets the string representation of this value value.
        /// </summary>
        /// <value>The string representation.</value>
        public abstract string StringRepresentation { get; protected set; }

        internal int TimesCaptured => this.timesCaptured;

        internal bool MarkedForCollection { get; set; }

        internal bool CertainForCollection { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString() => this.StringRepresentation;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SandboxValue sv)
            {
                return sv?.Id == this.Id;
            }

            return false;
        }

        internal int CaptureOne()
        {
            if (this.CertainForCollection)
            {
                throw new ObjectMarkedForCollectionException();
            }

            this.MarkedForCollection = false;
            return Interlocked.Increment(ref this.timesCaptured);
        }

        internal int ReleaseOne()
        {
            if (this.CertainForCollection)
            {
                throw new ObjectMarkedForCollectionException();
            }

            return Interlocked.Decrement(ref this.timesCaptured);
        }
    }
}