// <copyright file="QueueCollectionAdapter{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IX.Observable.Adapters
{
    /// <summary>
    /// A collection adapter for a queue.
    /// </summary>
    /// <typeparam name="T">The type of item in the queue.</typeparam>
    /// <seealso cref="IX.Observable.Adapters.CollectionAdapter{T}" />
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "QueueAdapterOf{0}", ItemName = "Item")]
    internal class QueueCollectionAdapter<T> : CollectionAdapter<T>
    {
        /// <summary>
        /// The base queue.
        /// </summary>
        private readonly Queue<T> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueCollectionAdapter{T}"/> class.
        /// </summary>
        internal QueueCollectionAdapter()
        {
            this.queue = new Queue<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueCollectionAdapter{T}"/> class.
        /// </summary>
        /// <param name="queue">The queue.</param>
        internal QueueCollectionAdapter(Queue<T> queue)
        {
            this.queue = new Queue<T>(queue);
        }

        /// <summary>
        /// Gets the number of items.
        /// </summary>
        /// <value>The number of items.</value>
        public override int Count => this.queue.Count;

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public override bool IsReadOnly => false;

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index of the freshly-added item.</returns>
        public override int Add(T item)
        {
            this.queue.Enqueue(item);
            return this.queue.Count - 1;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear() => this.queue.Clear();

        /// <summary>
        /// Determines whether the container list contains the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the container list contains the specified item; otherwise, <c>false</c>.</returns>
        public override bool Contains(T item) => this.queue.Contains(item);

        /// <summary>
        /// Copies the contents of the container to an array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public override void CopyTo(T[] array, int arrayIndex) => this.queue.CopyTo(array, arrayIndex);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index of the removed item, or <c>-1</c> if removal was not successful.</returns>
        public override int Remove(T item) => -1;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - Unavoidable here
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public override IEnumerator<T> GetEnumerator() => this.queue.GetEnumerator();
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

        /// <summary>
        /// Dequeues an item from the queue.
        /// </summary>
        /// <returns>An item.</returns>
        public T Dequeue() => this.queue.Dequeue();

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item) => this.queue.Enqueue(item);

        /// <summary>
        /// Peeks at the top item in the queue.
        /// </summary>
        /// <returns>An item.</returns>
        public T Peek() => this.queue.Peek();

        /// <summary>
        /// Converts all items in the stack to an array.
        /// </summary>
        /// <returns>The array of items.</returns>
        public T[] ToArray() => this.queue.ToArray();

        /// <summary>
        /// Trims the excess space in the stack.
        /// </summary>
        public void TrimExcess() => this.queue.TrimExcess();
    }
}