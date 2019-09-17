// <copyright file="RepeatableQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    ///     A queue that is able to accurately repeat the sequence of items that has been dequeued from it.
    /// </summary>
    /// <typeparam name="T">The type of items contained in this queue.</typeparam>
    /// <seealso cref="IQueue{T}" />
    [PublicAPI]
    public class RepeatableQueue<T> : IQueue<T>
    {
        private readonly IQueue<T> internalQueue;
        private readonly IQueue<T> internalRepeatingQueue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepeatableQueue{T}" /> class.
        /// </summary>
        public RepeatableQueue()
        {
            this.internalQueue = new Queue<T>();
            this.internalRepeatingQueue = new Queue<T>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepeatableQueue{T}" /> class.
        /// </summary>
        /// <param name="originalQueue">The original queue.</param>
        public RepeatableQueue(IQueue<T> originalQueue)
        {
            Contract.RequiresNotNull(
                ref this.internalQueue,
                originalQueue,
                nameof(originalQueue));
            this.internalRepeatingQueue = new Queue<T>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepeatableQueue{T}" /> class.
        /// </summary>
        /// <param name="originalData">The original data.</param>
        public RepeatableQueue(IEnumerable<T> originalData)
        {
            Contract.RequiresNotNull(
                in originalData,
                nameof(originalData));

            this.internalQueue = new Queue<T>(originalData);
            this.internalRepeatingQueue = new Queue<T>();
        }

        /// <summary>Gets the number of elements in the collection.</summary>
        /// <returns>The number of elements in the collection. </returns>
        public int Count => ((IReadOnlyCollection<T>)this.internalQueue).Count;

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
        int ICollection.Count => this.Count;

        /// <summary>
        ///     Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized
        ///     (thread safe).
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread
        ///     safe); otherwise, <see langword="false" />.
        /// </returns>
        bool ICollection.IsSynchronized => this.internalQueue.IsSynchronized;

        /// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
        /// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
        object ICollection.SyncRoot => this.internalQueue.SyncRoot;

        /// <summary>
        ///     Clears the queue of all elements.
        /// </summary>
        public void Clear()
        {
            this.internalQueue.Clear();
            this.internalRepeatingQueue.Clear();
        }

        /// <summary>
        ///     Verifies whether or not an item is contained in the queue.
        /// </summary>
        /// <param name="item">The item to verify.</param>
        /// <returns><see langword="true" /> if the item is queued, <see langword="false" /> otherwise.</returns>
        public bool Contains(T item) => this.internalQueue.Contains(item);

        /// <summary>
        ///     De-queues an item and removes it from the queue.
        /// </summary>
        /// <returns>The item that has been de-queued.</returns>
        public T Dequeue()
        {
            T dequeuedItem = this.internalQueue.Dequeue();
            this.internalRepeatingQueue.Enqueue(dequeuedItem);
            return dequeuedItem;
        }

        /// <summary>
        ///     Attempts to de-queue an item and to remove it from queue.
        /// </summary>
        /// <param name="item">The item that has been de-queued, default if unsuccessful.</param>
        /// <returns>
        ///     <see langword="true" /> if an item is de-queued successfully, <see langword="false" /> otherwise, or if the
        ///     queue is empty.
        /// </returns>
        public bool TryDequeue(out T item)
        {
            if (!this.internalQueue.TryDequeue(out item))
            {
                return false;
            }

            this.internalRepeatingQueue.Enqueue(item);
            return true;
        }

        /// <summary>
        ///     Queues an item, adding it to the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item) => this.internalQueue.Enqueue(item);

        /// <summary>
        ///     Peeks at the topmost element in the queue, without removing it.
        /// </summary>
        /// <returns>The item peeked at, if any.</returns>
        public T Peek() => this.internalQueue.Peek();

        /// <summary>
        ///     Copies all elements of the queue into a new array.
        /// </summary>
        /// <returns>The created array with all element of the queue.</returns>
        public T[] ToArray() => this.internalQueue.ToArray();

        /// <summary>
        ///     Trims the excess free space from within the queue, reducing the capacity to the actual number of elements.
        /// </summary>
        public void TrimExcess() => this.internalQueue.TrimExcess();

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        [SuppressMessage(
            "Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "Unavoidable.")]
        public IEnumerator<T> GetEnumerator() => this.internalQueue.GetEnumerator();

        /// <summary>
        ///     Copies the elements of the <see cref="RepeatableQueue{T}" /> to an <see cref="T:System.Array" />, starting at
        ///     a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="RepeatableQueue{T}" />. The <see cref="T:System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="array" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than zero.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="array" /> is multidimensional.-or- The number of elements in the source
        ///     <see cref="RepeatableQueue{T}" /> is greater than the available space from <paramref name="index" /> to the end of
        ///     the destination <paramref name="array" />.-or-The type of the source <see cref="RepeatableQueue{T}" /> cannot be
        ///     cast automatically to the type of the destination <paramref name="array" />.
        /// </exception>
        public void CopyTo(
            Array array,
            int index) =>
            this.internalQueue.CopyTo(
                array,
                index);

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        [SuppressMessage(
            "Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "Unavoidable.")]
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        ///     Gets a repeat of the sequence of elements dequeued from this instance.
        /// </summary>
        /// <returns>A repeating queue.</returns>
        public IQueue<T> Repeat() => new Queue<T>(this.internalRepeatingQueue.ToArray());
    }
}