// <copyright file="PushOutQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Runtime.Serialization;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;
using Constants = IX.Abstractions.Collections.Constants;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    ///     A queue that pushes out extra items above a certain limit.
    /// </summary>
    /// <typeparam name="T">The type of items in the queue.</typeparam>
    /// <seealso cref="IX.System.Collections.Generic.PushingCollectionBase{T}" />
    /// <seealso cref="IX.System.Collections.Generic.IQueue{T}" />
    [DataContract(
        Namespace = Constants.DataContractNamespace,
        Name = "PushOutQueueOf{0}")]
    [PublicAPI]
    public class PushOutQueue<T> : PushingCollectionBase<T>, IQueue<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PushOutQueue{T}" /> class.
        /// </summary>
        public PushOutQueue()
            : this(Constants.DefaultPushDownLimit)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PushOutQueue{T}" /> class.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <exception cref="IX.Abstractions.Collections.LimitArgumentNegativeException">
        ///     <paramref name="limit" /> is a negative
        ///     integer.
        /// </exception>
        public PushOutQueue(int limit)
        : base(limit)
        {
        }

        /// <summary>
        /// Dequeues an item from this push-out queue.
        /// </summary>
        /// <returns>The item.</returns>
        public T Dequeue() => this.InvokeIfNotDisposed(
            reference =>
            {
                if (reference.Limit == 0)
                {
                    return default;
                }

                return reference.WriteLock(
                    referenceL2 =>
                    {
                        if (referenceL2.InternalContainer.Count == 0)
                        {
                            return default;
                        }

                        T item = referenceL2.InternalContainer[0];

                        referenceL2.InternalContainer.RemoveAt(0);

                        return item;
                    }, reference);
            }, this);

        /// <summary>
        /// Attempts to dequeue an item from this push-out queue.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if the dequeue was successful, <c>false</c> otherwise.
        /// </returns>
        public bool TryDequeue(out T item)
        {
            this.RequiresNotDisposed();

            if (this.Limit == 0)
            {
                item = default;
                return false;
            }

            T localItem = default;
            var returnValue = this.WriteLock(
                referenceL2 =>
                {
                    if (referenceL2.InternalContainer.Count == 0)
                    {
                        return default;
                    }

                    localItem = referenceL2.InternalContainer[0];

                    referenceL2.InternalContainer.RemoveAt(0);

                    return true;
                }, this);

            item = localItem;
            return returnValue;
        }

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item) =>
            this.InvokeIfNotDisposed(
                (
                    itemL2,
                    cThis) =>
                {
                    if (cThis.Limit == 0)
                    {
                        return;
                    }

                    cThis.WriteLock(
                        (
                            itemL1,
                            c2This) =>
                        {
                            if (c2This.InternalContainer.Count == c2This.Limit)
                            {
                                c2This.InternalContainer.RemoveAt(0);
                            }

                            c2This.InternalContainer.Add(itemL1);
                        }, itemL2,
                        cThis);
                }, item,
                this);

        /// <summary>
        ///     Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek() =>
            this.InvokeIfNotDisposed(
                reference => reference.ReadLock(
                    referenceL2 => referenceL2.InternalContainer.Count > 0
                        ? referenceL2.InternalContainer[referenceL2.InternalContainer.Count - 1]
                        : default, reference), this);

        /// <summary>
        ///     This method does nothing.
        /// </summary>
        void IQueue<T>.TrimExcess()
        {
        }
    }
}