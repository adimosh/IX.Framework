// <copyright file="ObjectPool{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Efficiency
{
    /// <summary>
    ///     A pool of on-demand objects that keeps growing based on demand.
    /// </summary>
    /// <typeparam name="T">The class type to hold in the pool.</typeparam>
    [PublicAPI]
    public class ObjectPool<T>
        where T : class
    {
        // Pool collections
        private readonly Queue<T> availableObjects;

        // Interlocker
        private readonly object locker;

        // Object factory
        private readonly Func<T> objectFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectPool{T}" /> class.
        /// </summary>
        /// <param name="objectFactory">An object factory for when objects need to be created.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="objectFactory" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public ObjectPool([NotNull] Func<T> objectFactory)
        {
            Contract.RequiresNotNull(
                ref this.objectFactory,
                objectFactory,
                nameof(objectFactory));

            this.locker = new object();
            this.availableObjects = new Queue<T>();
        }

        /// <summary>
        ///     Gets the count of unused objects in the pool.
        /// </summary>
        /// <value>The currently-unused object count.</value>
        /// <remarks>
        ///     <para>
        ///         If this property returns 0, there are no more unused objects in the pool. If one is requested, a new one will
        ///         most likely be created.
        ///     </para>
        ///     <para>
        ///         You should not make decisions on new instances based on this value, as it is not synchronized with the actual
        ///         pool. It is possible to get a value of 0 here and
        ///         a new item to not be created, should an object be returned to the pool between the property being accessed and
        ///         the <see cref="Get" /> method called.
        ///     </para>
        /// </remarks>
        // ReSharper disable once InconsistentlySynchronizedField - We don't really care, we don't need this to be exact
        public int Count => this.availableObjects.Count;

        /// <summary>
        ///     Gets an object from the pool.
        /// </summary>
        /// <returns>A pooled object, either new, or unused.</returns>
        public PooledObject<T> Get()
        {
            T @object;

            lock (this.locker)
            {
                @object = this.availableObjects.Count > 0 ? this.availableObjects.Dequeue() : this.objectFactory();
            }

            return new PooledObject<T>(
                this,
                @object);
        }

        internal void Release(T @object)
        {
            lock (this.locker)
            {
                this.availableObjects.Enqueue(@object);
            }
        }
    }
}