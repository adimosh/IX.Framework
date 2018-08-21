// <copyright file="PredictableDataStore{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    /// A data store for storing items in a predictable way, such as any iteration through the store will produce the same output. This class is thread-safe, and its internal items container is immutable.
    /// </summary>
    /// <typeparam name="T">The type of items in the store.</typeparam>
    public class PredictableDataStore<T> : IEnumerable<T>
    {
        private readonly T[] items;

        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictableDataStore{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="generator">The generator.</param>
        public PredictableDataStore(int capacity, Func<T> generator)
            : this(capacity, generator, false)
        {
        }

#pragma warning disable HAA0302 // Display class allocation to capture closure
        /// <summary>
        /// Initializes a new instance of the <see cref="PredictableDataStore{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="parallelGenerate">if set to <c>true</c>, run generation of items in parallel.</param>
        public PredictableDataStore(int capacity, Func<T> generator, bool parallelGenerate)
#pragma warning restore HAA0302 // Display class allocation to capture closure
        {
            this.items = new T[capacity];

            if (parallelGenerate)
            {
                Parallel.For(
                    0,
                    capacity,
#pragma warning disable HAA0301 // Closure Allocation Source
                    index =>
                    {
                        T item = generator();

                        this.items[index] = item;
                    });
#pragma warning restore HAA0301 // Closure Allocation Source
            }
            else
            {
                for (var i = 0; i < capacity; i++)
                {
                    this.items[i] = generator();
                }
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count => this.items.Length;

        /// <summary>
        /// Takes an item from the predictable data store.
        /// </summary>
        /// <returns>T.</returns>
        public T Take()
        {
            T item;

            lock (this.items)
            {
                item = this.items[this.index];
                this.index++;
            }

            return item;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            lock (this.items)
            {
                this.index = 0;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection, from the point in which it currently stands, to the end.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                T item;
                lock (this.items)
                {
                    if (this.index >= this.items.Length)
                    {
                        break;
                    }

                    item = this.items[this.index];
                    this.index++;
                }

                yield return item;
            }
        }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
        /// <summary>
        /// Returns an enumerator that iterates through a collection, from the point in which it currently stands, to the end.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
    }
}