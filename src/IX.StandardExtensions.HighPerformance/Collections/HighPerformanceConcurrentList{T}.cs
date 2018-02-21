// <copyright file="HighPerformanceConcurrentList{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using IX.StandardExtensions.Threading;
using IX.System.Threading;

namespace IX.StandardExtensions.HighPerformance.Collections
{
    /// <summary>
    /// A high-performance concurrent list.
    /// </summary>
    /// <typeparam name="T">The type of item in the list.</typeparam>
    /// <seealso cref="ReaderWriterSynchronizedBase" />
    /// <seealso cref="global::System.Collections.Generic.IList{T}" />
    public class HighPerformanceConcurrentList<T> : ReaderWriterSynchronizedBase, IList<T>
    {
        private readonly List<T> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentList{T}"/> class.
        /// </summary>
        public HighPerformanceConcurrentList()
        {
            this.items = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentList{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">capacity</exception>
        public HighPerformanceConcurrentList(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(nameof(capacity));
            }

            this.items = new List<T>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentList{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <exception cref="ArgumentNullException">collection</exception>
        public HighPerformanceConcurrentList(IEnumerable<T> collection)
        {
            this.items = new List<T>(collection ?? throw new ArgumentNullException(nameof(collection)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentList{T}"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentList(IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentList{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="locker">The locker.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">capacity</exception>
        public HighPerformanceConcurrentList(int capacity, IReaderWriterLock locker)
            : base(locker)
        {
            if (capacity <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(nameof(capacity));
            }

            this.items = new List<T>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentList{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="locker">The locker.</param>
        /// <exception cref="ArgumentNullException">collection</exception>
        public HighPerformanceConcurrentList(IEnumerable<T> collection, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new List<T>(collection ?? throw new ArgumentNullException(nameof(collection)));
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <value>The count.</value>
        public int Count => this.ReadLock(() => this.items.Count);

        /// <summary>
        /// Gets a value indicating whether this is a read-only <see cref="HighPerformanceConcurrentList{T}" />. Always returns <c>false</c>.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        bool ICollection<T>.IsReadOnly => false;

        /// <summary>
        /// Gets or sets the item <typeparamref name="T" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The item at the specified index.</returns>
        public T this[int index]
        {
            get => this.ReadLock((indexL1) => this.items[indexL1], index);
            set => this.WriteLock((indexL1, valueL1) => this.items[indexL1] = valueL1, index, value);
        }

        /// <summary>
        /// Adds an item to the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="HighPerformanceConcurrentList{T}" />.</param>
        public void Add(T item) => this.WriteLock(itemL1 => this.items.Add(itemL1), item);

        /// <summary>
        /// Removes all items from the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        public void Clear() => this.WriteLock(() => this.items.Clear());

        /// <summary>
        /// Determines whether the <see cref="HighPerformanceConcurrentList{T}" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentList{T}" />.</param>
        /// <returns>true if <paramref name="item" /> is found in the <see cref="HighPerformanceConcurrentList{T}" />; otherwise, false.</returns>
        public bool Contains(T item) => this.ReadLock(itemL1 => this.items.Contains(itemL1), item);

        /// <summary>
        /// Copies the elements of the <see cref="HighPerformanceConcurrentList{T}" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="HighPerformanceConcurrentList{T}" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex) => this.ReadLock((arrayL1, indexL1) => this.items.CopyTo(arrayL1, indexL1), array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();

        public int IndexOf(T item) => throw new NotImplementedException();

        public void Insert(int index, T item) => throw new NotImplementedException();

        public bool Remove(T item) => throw new NotImplementedException();

        public void RemoveAt(int index) => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}