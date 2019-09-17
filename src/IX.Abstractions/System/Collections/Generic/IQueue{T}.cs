// <copyright file="IQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace IX.System.Collections.Generic
{
    /// <summary>
    ///     A contract for a queue.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    /// <seealso cref="IEnumerable{T}" />
    /// <seealso cref="ICollection" />
    /// <seealso cref="IReadOnlyCollection{T}" />
    [PublicAPI]
    [SuppressMessage(
        "ReSharper",
        "PossibleInterfaceMemberAmbiguity",
        Justification = "Member ambiguity is unavoidable when implementing ICollection")]
    public interface IQueue<T> : ICollection, IReadOnlyCollection<T>
    {
        /// <summary>
        ///     Clears the queue of all elements.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Verifies whether or not an item is contained in the queue.
        /// </summary>
        /// <param name="item">The item to verify.</param>
        /// <returns><see langword="true" /> if the item is queued, <see langword="false" /> otherwise.</returns>
        bool Contains(T item);

        /// <summary>
        ///     De-queues an item and removes it from the queue.
        /// </summary>
        /// <returns>The item that has been de-queued.</returns>
        T Dequeue();

        /// <summary>
        ///     Attempts to de-queue an item and to remove it from queue.
        /// </summary>
        /// <param name="item">The item that has been de-queued, default if unsuccessful.</param>
        /// <returns>
        ///     <see langword="true" /> if an item is de-queued successfully, <see langword="false" /> otherwise, or if the
        ///     queue is empty.
        /// </returns>
        bool TryDequeue([CanBeNull] out T item);

        /// <summary>
        ///     Queues an item, adding it to the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        void Enqueue(T item);

        /// <summary>
        ///     Peeks at the topmost element in the queue, without removing it.
        /// </summary>
        /// <returns>The item peeked at, if any.</returns>
        T Peek();

        /// <summary>
        ///     Copies all elements of the queue into a new array.
        /// </summary>
        /// <returns>The created array with all element of the queue.</returns>
        [NotNull]
        T[] ToArray();

        /// <summary>
        ///     Trims the excess free space from within the queue, reducing the capacity to the actual number of elements.
        /// </summary>
        void TrimExcess();
    }
}