// <copyright file="PooledObject{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.Efficiency
{
    /// <summary>
    /// A pooled object.
    /// </summary>
    /// <typeparam name="T">The type of class instance in the pool.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public class PooledObject<T> : IDisposable
        where T : class
    {
        private readonly T value;
        private readonly ObjectPool<T> pool;

        private bool abort;

        internal PooledObject(ObjectPool<T> pool, T value)
        {
            this.value = value;
            this.pool = pool;
        }

        /// <summary>
        /// Gets the value contained.
        /// </summary>
        /// <value>The value.</value>
        public T Value => this.value;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PooledObject{T}"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="source">The source pooled object.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator T(PooledObject<T> source) => source.value;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!this.abort)
            {
                this.pool.Release(this.value);
            }
        }

        /// <summary>
        /// Aborts this pooled object, not returning its value to the pool.
        /// </summary>
        public void Abort() => this.abort = true;
    }
}