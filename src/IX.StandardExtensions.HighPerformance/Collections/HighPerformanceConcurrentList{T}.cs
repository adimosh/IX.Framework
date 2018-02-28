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
    public class HighPerformanceConcurrentList<T> : ReaderWriterSynchronizedBase, IList<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
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

        /// <summary>
        /// Returns an <see cref="AtomicEnumerator{T}"/> that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => new AtomicEnumerator<T>(this.items.GetEnumerator(), this.ReadLock);

        /// <summary>
        /// Determines the index of a specific item in the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentList{T}" />.</param>
        /// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item) => this.ReadLock(itemL1 => this.items.IndexOf(itemL1), item);

        /// <summary>
        /// Inserts an item to the <see cref="HighPerformanceConcurrentList{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="HighPerformanceConcurrentList{T}" />.</param>
        public void Insert(int index, T item) => this.WriteLock((indexL1, itemL1) => this.items.Insert(indexL1, itemL1), index, item);

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="HighPerformanceConcurrentList{T}" />.</param>
        /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="HighPerformanceConcurrentList{T}" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original
        /// <see cref="HighPerformanceConcurrentList{T}" />.</returns>
        public bool Remove(T item) => this.WriteLock(itemL1 => this.items.Remove(itemL1), item);

        /// <summary>
        /// Removes the <see cref="HighPerformanceConcurrentList{T}" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index) => this.WriteLock(indexL1 => this.items.RemoveAt(indexL1), index);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the <see cref="HighPerformanceConcurrentList{T}" />. The collection itself cannot be null, but it can contain elements that are null,
        /// if type T is a reference type.</param>
        public void AddRange(IEnumerable<T> collection) => this.WriteLock(collectionL1 => this.items.AddRange(collectionL1), collection);

        /// <summary>
        /// Searches a range of elements in the sorted <see cref="HighPerformanceConcurrentList{T}" /> for an element using the specified comparer and returns the zero-based index of the element.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to search.</param>
        /// <param name="count">The length of the range to search.</param>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <param name="comparer">The <see cref="global::System.Collections.Generic.Comparer{T}"/> implementation to use when comparing elements, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of item in the sorted <see cref="HighPerformanceConcurrentList{T}" />, if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is
        /// larger than item or, if there is no larger element, the bitwise complement of <see cref="HighPerformanceConcurrentList{T}.Count" />.</returns>
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer) => this.ReadLock((indexL1, countL1, itemL1, comparerL1) => this.items.BinarySearch(indexL1, countL1, itemL1, comparerL1), index, count, item, comparer);

        /// <summary>
        /// Searches the entire sorted <see cref="HighPerformanceConcurrentList{T}" /> for an element using the default comparer and returns the zero-based index of the element.
        /// </summary>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <returns>The zero-based index of item in the sorted <see cref="HighPerformanceConcurrentList{T}" />, if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is
        /// larger than item or, if there is no larger element, the bitwise complement of <see cref="HighPerformanceConcurrentList{T}.Count" /></returns>
        public int BinarySearch(T item) => this.ReadLock(itemL1 => this.items.BinarySearch(itemL1), item);

        /// <summary>
        /// Searches the entire sorted <see cref="HighPerformanceConcurrentList{T}" /> for an element using the specified comparer and returns the zero-based index of the element.
        /// </summary>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <param name="comparer">The <see cref="global::System.Collections.Generic.Comparer{T}"/> implementation to use when comparing elements, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of item in the sorted <see cref="HighPerformanceConcurrentList{T}" />, if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is
        /// larger than item or, if there is no larger element, the bitwise complement of <see cref="HighPerformanceConcurrentList{T}.Count" /></returns>
        public int BinarySearch(T item, IComparer<T> comparer) => this.ReadLock((itemL1, comparerL1) => this.items.BinarySearch(itemL1, comparerL1), item, comparer);

        /// <summary>
        /// Copies a range of elements from the <see cref="HighPerformanceConcurrentList{T}" /> to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="index">The zero-based index in the source <see cref="HighPerformanceConcurrentList{T}" /> at which copying begins.</param>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied from <see cref="HighPerformanceConcurrentList{T}" />. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <param name="count">The number of elements to copy.</param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count) => this.ReadLock((indexL1, arrayL1, arrayIndexL1, countL1) => this.items.CopyTo(indexL1, arrayL1, arrayIndexL1, countL1), index, array, arrayIndex, count);

        /// <summary>
        /// Copies the entire <see cref="HighPerformanceConcurrentList{T}" /> to a compatible one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied from <see cref="HighPerformanceConcurrentList{T}" />. The System.Array must have zero-based indexing.</param>
        public void CopyTo(T[] array) => this.ReadLock(arrayL1 => this.items.CopyTo(arrayL1), array);

        /// <summary>
        /// Determines whether the <see cref="HighPerformanceConcurrentList{T}" /> contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the elements to search for.</param>
        /// <returns>true if the <see cref="HighPerformanceConcurrentList{T}" /> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.</returns>
        public bool Exists(Predicate<T> match) => this.ReadLock(matchL1 => this.items.Exists(matchL1), match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.</returns>
        public T Find(Predicate<T> match) => this.ReadLock(matchL1 => this.items.Find(matchL1), match);

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the elements to search for.</param>
        /// <returns>A <see cref="HighPerformanceConcurrentList{T}" /> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="HighPerformanceConcurrentList{T}" />.</returns>
        public List<T> FindAll(Predicate<T> match) => this.ReadLock(matchL1 => this.items.FindAll(matchL1), match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        public int FindIndex(Predicate<T> match) => this.ReadLock(matchL1 => this.items.FindIndex(matchL1), match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that extends
        /// from the specified index to the last element.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        public int FindIndex(int startIndex, Predicate<T> match) => this.ReadLock((startIndexL1, matchL1) => this.items.FindIndex(startIndexL1, matchL1), startIndex, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts
        /// at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        public int FindIndex(int startIndex, int count, Predicate<T> match) => this.ReadLock((startIndexL1, countL1, matchL1) => this.items.FindIndex(startIndexL1, countL1, matchL1), startIndex, count, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.</returns>
        public T FindLast(Predicate<T> match) => this.ReadLock(matchL1 => this.items.FindLast(matchL1), match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        public int FindLastIndex(Predicate<T> match) => this.ReadLock(matchL1 => this.items.FindLastIndex(matchL1), match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that extends
        /// from the specified index to the last element.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        public int FindLastIndex(int startIndex, Predicate<T> match) => this.ReadLock((startIndexL1, matchL1) => this.items.FindLastIndex(startIndexL1, matchL1), startIndex, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts
        /// at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the element to search for.</param>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        public int FindLastIndex(int startIndex, int count, Predicate<T> match) => this.ReadLock((startIndexL1, countL1, matchL1) => this.items.FindLastIndex(startIndexL1, countL1, matchL1), startIndex, count, match);

        /// <summary>
        /// Performs the specified action on each element of the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="action">The System.Action`1 delegate to perform on each element of the <see cref="HighPerformanceConcurrentList{T}" />.</param>
        public void ForEach(Action<T> action) => this.GetEnumerator().ForEach(action);

        /// <summary>
        /// Creates a shallow copy of a range of elements in the source <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="index">The zero-based <see cref="HighPerformanceConcurrentList{T}" /> index at which the range starts.</param>
        /// <param name="count">The number of elements in the range.</param>
        /// <returns>A shallow copy of a range of elements in the source <see cref="HighPerformanceConcurrentList{T}" />.</returns>
        public List<T> GetRange(int index, int count) => this.ReadLock((indexL1, countL1) => this.items.GetRange(indexL1, countL1), index, count);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentList{T}" />. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        public int IndexOf(T item, int index, int count) => this.ReadLock((itemL1, indexL1, countL1) => this.items.IndexOf(itemL1, indexL1, countL1), item, index, count);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at the specified index to the last element.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentList{T}" />. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
        /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        public int IndexOf(T item, int index) => this.ReadLock((itemL1, indexL1) => this.items.IndexOf(itemL1, indexL1), item, index);

        /// <summary>
        /// Inserts the elements of a collection into the <see cref="HighPerformanceConcurrentList{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection whose elements should be inserted into the <see cref="HighPerformanceConcurrentList{T}" />. The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        public void InsertRange(int index, IEnumerable<T> collection) => this.WriteLock((indexL1, collectionL1) => this.items.InsertRange(indexL1, collectionL1), index, collection);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the last occurrence within the entire <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentList{T}" />. The value can be null for reference types.</param>
        /// <returns>The zero-based index of the last occurrence of item within the entire the <see cref="HighPerformanceConcurrentList{T}" />, if found; otherwise, –1.</returns>
        public int LastIndexOf(T item) => this.ReadLock(itemL1 => this.items.LastIndexOf(itemL1), item);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentList{T}" />. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        public int LastIndexOf(T item, int index, int count) => this.ReadLock((itemL1, indexL1, countL1) => this.items.LastIndexOf(itemL1, indexL1, countL1), item, index, count);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at the specified index to the last element.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentList{T}" />. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
        /// <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="HighPerformanceConcurrentList{T}" /> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        public int LAstIndexOf(T item, int index) => this.ReadLock((itemL1, indexL1) => this.items.LastIndexOf(itemL1, indexL1), item, index);

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions of the elements to remove.</param>
        /// <returns>The number of elements removed from the <see cref="HighPerformanceConcurrentList{T}" />.</returns>
        public int RemoveAll(Predicate<T> match) => this.WriteLock(matchL1 => this.items.RemoveAll(matchL1), match);

        /// <summary>
        /// Removes a range of elements from the <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        public void RemoveRange(int index, int count) => this.WriteLock((indexL1, countL1) => this.items.RemoveRange(indexL1, countL1), index, count);

        /// <summary>
        /// Reverses the order of the elements in the specified range.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to reverse.</param>
        /// <param name="count">The number of elements in the range to reverse.</param>
        public void Reverse(int index, int count) => this.WriteLock((indexL1, countL1) => this.items.Reverse(indexL1, countL1), index, count);

        /// <summary>
        /// Reverses the order of the elements in the entire <see cref="HighPerformanceConcurrentList{T}" />.
        /// </summary>
        public void Reverse() => this.WriteLock(this.items.Reverse);

        /// <summary>
        /// Sorts the elements in a range of elements in <see cref="HighPerformanceConcurrentList{T}" /> using the specified comparer.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to sort.</param>
        /// <param name="count">The length of the range to sort.</param>
        /// <param name="comparer">The System.Collections.Generic.IComparer`1 implementation to use when comparing elements, or null to use the default comparer System.Collections.Generic.Comparer`1.Default.</param>
        public void Sort(int index, int count, IComparer<T> comparer) => this.WriteLock((indexL1, countL1, comparerL1) => this.items.Sort(indexL1, countL1, comparerL1), index, count, comparer);

        /// <summary>
        /// Sorts the elements in the entire <see cref="HighPerformanceConcurrentList{T}" /> using the specified System.Comparison`1.
        /// </summary>
        /// <param name="comparison">The System.Comparison`1 to use when comparing elements.</param>
        public void Sort(Comparison<T> comparison) => this.WriteLock(comparisonL1 => this.items.Sort(comparisonL1), comparison);

        /// <summary>
        /// Sorts the elements in the entire <see cref="HighPerformanceConcurrentList{T}" /> using the default comparer.
        /// </summary>
        public void Sort() => this.WriteLock(this.items.Sort);

        /// <summary>
        /// Sorts the elements in the entire <see cref="HighPerformanceConcurrentList{T}" /> using the specified comparer.
        /// </summary>
        /// <param name="comparer">The System.Collections.Generic.IComparer`1 implementation to use when comparing elements, or null to use the default comparer System.Collections.Generic.Comparer`1.Default.</param>
        public void Sort(IComparer<T> comparer) => this.WriteLock(comparerL1 => this.items.Sort(comparerL1), comparer);

        /// <summary>
        /// Copies the elements of the <see cref="HighPerformanceConcurrentList{T}" /> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="HighPerformanceConcurrentList{T}" />.</returns>
        public T[] ToArray() => this.ReadLock(this.items.ToArray);

        /// <summary>
        /// Sets the capacity to the actual number of elements in the <see cref="HighPerformanceConcurrentList{T}" />, if that number is less than a threshold value.
        /// </summary>
        public void TrimExcess() => this.WriteLock(this.items.TrimExcess);

        /// <summary>
        /// Determines whether every element in the <see cref="HighPerformanceConcurrentList{T}" /> matches the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The System.Predicate`1 delegate that defines the conditions to check against the elements.</param>
        /// <returns>true if every element in the <see cref="HighPerformanceConcurrentList{T}" /> matches the conditions defined by the specified predicate; otherwise, false. If the list has no elements, the return value is true.</returns>
        public bool TrueForAll(Predicate<T> match) => this.ReadLock(matchL1 => this.items.TrueForAll(matchL1), match);
    }
}