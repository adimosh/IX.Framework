// <copyright file="BusyScope.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// A scope of operations that can be marked as busy or idle.
    /// </summary>
    public class BusyScope : SynchronizationContextInvokerBase
    {
        private readonly string initialDescription;

        private int busyCount;
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusyScope"/> class.
        /// </summary>
        public BusyScope()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusyScope"/> class.
        /// </summary>
        /// <param name="description">The scope description.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="description"/> is <c>null</c>.</exception>
        public BusyScope(string description)
        {
            this.initialDescription = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusyScope"/> class.
        /// </summary>
        /// <param name="initialBusyCount">The initial busy count.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="initialBusyCount"/> is an integer value less than 0.</exception>
        public BusyScope(int initialBusyCount)
        {
            if (initialBusyCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialBusyCount));
            }

            this.busyCount = initialBusyCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusyScope"/> class.
        /// </summary>
        /// <param name="initialBusyCount">The initial busy count.</param>
        /// <param name="description">The scope description.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="initialBusyCount"/> is an integer value less than 0.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="description"/> is <c>null</c>.</exception>
        public BusyScope(int initialBusyCount, string description)
        {
            if (initialBusyCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialBusyCount));
            }

            this.initialDescription = description ?? throw new ArgumentNullException(nameof(description));
            this.busyCount = initialBusyCount;
        }

        /// <summary>
        /// Occurs when the busy status of the scope has changed.
        /// </summary>
        public event Action BusyScopeChanged;

        /// <summary>
        /// Gets the busy count.
        /// </summary>
        /// <value>The busy count.</value>
        public int BusyCount => this.busyCount;

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description => this.description ?? this.initialDescription ?? string.Empty;

        /// <summary>
        /// Increments the busy scope.
        /// </summary>
        /// <param name="description">The description for the topmost busy operation.</param>
        public void IncrementBusyScope(string description = null)
        {
            Interlocked.Increment(ref this.busyCount);
            this.description = description;

            if (this.BusyScopeChanged != null)
            {
                this.Invoke(this.BusyScopeChanged.Invoke);
            }
        }

        /// <summary>
        /// Decrements the busy scope.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The scope is idle.</exception>
        public void DecrementBusyScope()
        {
            if (this.BusyCount == 0)
            {
                throw new InvalidOperationException();
            }

            Interlocked.Decrement(ref this.busyCount);

            if (this.BusyScopeChanged != null)
            {
                this.Invoke(this.BusyScopeChanged.Invoke);
            }
        }
    }
}