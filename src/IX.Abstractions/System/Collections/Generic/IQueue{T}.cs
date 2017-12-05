// <copyright file="IQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Generic;

namespace IX.System.Collections.Generic
{
    /// <summary>
    /// A contract for a queue.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    /// <seealso cref="IEnumerable{T}" />
    /// <seealso cref="ICollection" />
    /// <seealso cref="IReadOnlyCollection{T}" />
    public interface IQueue<T> : IEnumerable<T>, ICollection, IReadOnlyCollection<T>
    {
        /// <summary>
        /// Clears the queue of all elements.
        /// </summary>
        void Clear();

        /// <summary>
        /// Verifies whether or not an item is contained in the queue.
        /// </summary>
        /// <param name="item">The item to verify.</param>
        /// <returns><c>true</c> if the item is queued, <c>false</c> otherwise.</returns>
        bool Contains(T item);

        /// <summary>
        /// Dequeues an item and removes it from the queue.
        /// </summary>
        /// <returns>The item that has been dequeued.</returns>
        T Dequeue();

        /// <summary>
        /// Enqueues an item, adding it to the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        void Enqueue(T item);

        /// <summary>
        /// Peeks at the topmost element in the queue, without removing it.
        /// </summary>
        /// <returns>The item peeked at, if any.</returns>
        T Peek();

        /// <summary>
        /// Copies all elements of the queue into a new array.
        /// </summary>
        /// <returns>The created array with all element of the queue.</returns>
        T[] ToArray();

        /// <summary>
        /// Trims the excess free space from within the queue, reducing the capacity to the actual number of elements.
        /// </summary>
        void TrimExcess();
    }
}