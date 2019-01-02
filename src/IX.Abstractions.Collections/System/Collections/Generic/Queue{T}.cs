// <copyright file="Queue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using JetBrains.Annotations;
using GlobalCollectionsGeneric = System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    /// Represents a variable-size first-in first-out (FIFO) collection of instances of the same specified type.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    /// <seealso cref="GlobalCollectionsGeneric.Queue{T}" />
    /// <seealso cref="IQueue{T}" />
    public class Queue<T> : GlobalCollectionsGeneric.Queue<T>, IQueue<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        public Queue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="Queue{T}" /> can contain.</param>
        public Queue(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public Queue([NotNull]GlobalCollectionsGeneric.IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Attempts to de-queue an item and to remove it from queue.
        /// </summary>
        /// <param name="item">The item that has been de-queued, default if unsuccessful.</param>
        /// <returns><see langword="true" /> if an item is de-queued successfully, <see langword="false"/> otherwise, or if the queue is empty.</returns>
        public bool TryDequeue(out T item)
        {
            if (this.Count == 0)
            {
                item = default;
                return false;
            }

            item = this.Dequeue();
            return true;
        }
    }
}