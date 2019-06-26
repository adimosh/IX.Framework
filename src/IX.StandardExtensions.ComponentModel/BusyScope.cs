// <copyright file="BusyScope.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    ///     A scope of operations that can be marked as busy or idle.
    /// </summary>
    [PublicAPI]
    public class BusyScope : SynchronizationContextInvokerBase
    {
        private readonly string initialDescription;

        private int busyCount;
        private string description;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BusyScope" /> class.
        /// </summary>
        public BusyScope()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BusyScope" /> class.
        /// </summary>
        /// <param name="description">The scope description.</param>
        /// <exception cref="ArgumentNullException"><paramref name="description" /> is <see langword="null" />.</exception>
        public BusyScope(string description)
        {
            Contract.RequiresNotNull(
                ref this.initialDescription,
                description,
                nameof(description));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BusyScope" /> class.
        /// </summary>
        /// <param name="initialBusyCount">The initial busy count.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialBusyCount" /> is an integer value less than 0.</exception>
        public BusyScope(int initialBusyCount)
        {
            Contract.RequiresNonNegative(
                ref this.busyCount,
                in initialBusyCount,
                nameof(initialBusyCount));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BusyScope" /> class.
        /// </summary>
        /// <param name="initialBusyCount">The initial busy count.</param>
        /// <param name="description">The scope description.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialBusyCount" /> is an integer value less than 0.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="description" /> is <see langword="null" />.</exception>
        public BusyScope(
            int initialBusyCount,
            string description)
        {
            Contract.RequiresNonNegative(
                ref this.busyCount,
                in initialBusyCount,
                nameof(initialBusyCount));
            Contract.RequiresNotNull(
                ref this.initialDescription,
                description,
                nameof(description));
        }

        /// <summary>
        ///     Occurs when the busy status of the scope has changed.
        /// </summary>
        public event EventHandler BusyScopeChanged;

        /// <summary>
        ///     Gets the busy count.
        /// </summary>
        /// <value>The busy count.</value>
        public int BusyCount => this.busyCount;

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description => this.description ?? this.initialDescription ?? string.Empty;

        /// <summary>
        ///     Increments the busy scope.
        /// </summary>
        /// <param name="description">The description for the topmost busy operation.</param>
        // ReSharper disable once ParameterHidesMember - We know, that is the point
        public void IncrementBusyScope(string description = null)
        {
            Interlocked.Increment(ref this.busyCount);
            this.description = description;

            if (this.BusyScopeChanged != null)
            {
                this.Invoke(
                    thisL1 => thisL1.BusyScopeChanged.Invoke(
                        thisL1,
                        EventArgs.Empty), this);
            }
        }

        /// <summary>
        ///     Decrements the busy scope.
        /// </summary>
        /// <exception cref="InvalidOperationException">The scope is idle.</exception>
        public void DecrementBusyScope()
        {
            if (this.BusyCount == 0)
            {
                throw new InvalidOperationException();
            }

            Interlocked.Decrement(ref this.busyCount);

            if (this.BusyScopeChanged != null)
            {
                this.Invoke(
                    thisL1 => thisL1.BusyScopeChanged.Invoke(
                        thisL1,
                        EventArgs.Empty), this);
            }
        }
    }
}