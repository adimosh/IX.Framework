// <copyright file="PredictableDataStore{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    ///     A data store for storing items in a predictable way, such as any iteration through the store will produce the same
    ///     output. This class is thread-safe, and its internal items container is immutable.
    /// </summary>
    /// <typeparam name="T">The type of items in the store.</typeparam>
    [PublicAPI]
    public class PredictableDataStore<T> : IReadOnlyList<T>
    {
        private readonly T[] items;

        private int index;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PredictableDataStore{T}" /> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="generator">The generator.</param>
        public PredictableDataStore(
            int capacity,
            Func<T> generator)
            : this(
                capacity,
                generator,
                false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PredictableDataStore{T}" /> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="generator">The sateful generator.</param>
        /// <param name="state">The state.</param>
        public PredictableDataStore(
            int capacity,
            Func<object, T> generator,
            object state)
            : this(
                capacity,
                generator,
                state,
                false)
        {
        }

#pragma warning disable HAA0302 // Display class allocation to capture closure - The closures here are acceptable
#pragma warning disable HAA0301 // Closure Allocation Source
        /// <summary>
        ///     Initializes a new instance of the <see cref="PredictableDataStore{T}" /> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="parallelGenerate">if set to <see langword="true" />, run generation of items in parallel.</param>
        public PredictableDataStore(
            int capacity,
            Func<T> generator,
            bool parallelGenerate)
        {
            this.items = new T[capacity];

            if (parallelGenerate)
            {
                Parallel.For(
                    0,
                    capacity,
                    index =>
                    {
                        T item = generator();

                        this.items[index] = item;
                    });
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
        ///     Initializes a new instance of the <see cref="PredictableDataStore{T}" /> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="state">The state.</param>
        /// <param name="parallelGenerate">if set to <see langword="true" />, run generation of items in parallel.</param>
        public PredictableDataStore(
            int capacity,
            Func<object, T> generator,
            object state,
            bool parallelGenerate)
        {
            this.items = new T[capacity];

            if (parallelGenerate)
            {
                Parallel.For(
                    0,
                    capacity,
                    index =>
                    {
                        T item = generator(state);

                        this.items[index] = item;
                    });
            }
            else
            {
                for (var i = 0; i < capacity; i++)
                {
                    this.items[i] = generator(state);
                }
            }
        }
#pragma warning restore HAA0301 // Closure Allocation Source
#pragma warning restore HAA0302 // Display class allocation to capture closure

        /// <summary>
        ///     Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count => this.items.Length;

        /// <summary>Gets the element at the specified index in the read-only list.</summary>
        /// <param name="index">The zero-based index of the element to get. </param>
        /// <returns>The element at the specified index in the read-only list.</returns>
        public T this[int index] => this.items[index];

        /// <summary>
        ///     Takes an item from the predictable data store.
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
        ///     Resets this instance.
        /// </summary>
        public void Reset()
        {
            lock (this.items)
            {
                this.index = 0;
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection, from the point in which it currently stands, to the
        ///     end.
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
        ///     Returns an enumerator that iterates through a collection, from the point in which it currently stands, to the end.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
    }
}