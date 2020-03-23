// <copyright file="PushingCollectionBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Threading;
using IX.Abstractions.Collections;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Threading;
using JetBrains.Annotations;
using Constants = IX.Abstractions.Collections.Constants;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    /// A base class for pushing collections.
    /// </summary>
    /// <typeparam name="T">The type of item in the pushing collection.</typeparam>
    /// <seealso cref="ReaderWriterSynchronizedBase" />
    /// <seealso cref="ICollection" />
    [DataContract(
        Namespace = Constants.DataContractNamespace,
        Name = "PushOutQueueOf{0}")]
    [PublicAPI]
    public abstract class PushingCollectionBase<T> : ReaderWriterSynchronizedBase, ICollection
    {
        /// <summary>
        ///     The internal container.
        /// </summary>
        [DataMember(Name = "Items")]
        private List<T> internalContainer;

        /// <summary>
        ///     The limit.
        /// </summary>
        private int limit;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PushingCollectionBase{T}" /> class.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <exception cref="LimitArgumentNegativeException">
        ///     <paramref name="limit" /> is a negative
        ///     integer.
        /// </exception>
        protected PushingCollectionBase(int limit)
        {
            if (limit < 0)
            {
                throw new LimitArgumentNegativeException(nameof(limit));
            }

            this.limit = limit;

            this.internalContainer = new List<T>();
        }

        /// <summary>
        ///     Gets the number of elements in the push-out queue.
        /// </summary>
        /// <value>The current element count.</value>
        public int Count
        {
            get
            {
                this.RequiresNotDisposed();

                using (this.ReadLock())
                {
                    return this.internalContainer.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this pushing bollection is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this pushing collection is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty => this.Count == 0;

        /// <summary>
        ///     Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized
        ///     (thread safe).
        /// </summary>
        /// <value><see langword="true" /> if this instance is synchronized; otherwise, <see langword="false" />.</value>
        bool ICollection.IsSynchronized => ((ICollection)this.internalContainer).IsSynchronized;

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The synchronize root.</value>
        object ICollection.SyncRoot => ((ICollection)this.internalContainer).SyncRoot;

        /// <summary>
        ///     Gets or sets the number of items in the push-down stack.
        /// </summary>
        [DataMember]
        public int Limit
        {
            get => this.limit;
            set
            {
                this.RequiresNotDisposed();

                if (value < 0)
                {
                    throw new LimitArgumentNegativeException();
                }

                using (this.WriteLock())
                {
                    this.limit = value;

                    if (value != 0)
                    {
                        while (this.internalContainer.Count > value)
                        {
                            this.internalContainer.RemoveAt(0);
                        }
                    }
                    else
                    {
                        this.internalContainer.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the internal container.
        /// </summary>
        /// <value>
        /// The internal container.
        /// </value>
        protected List<T> InternalContainer => this.internalContainer;

        /// <summary>
        ///     Clears the observable stack.
        /// </summary>
        public void Clear()
        {
            this.RequiresNotDisposed();

            using (this.WriteLock())
            {
                this.internalContainer.Clear();
            }
        }

        /// <summary>
        ///     Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><see langword="true" /> if the item was found, <see langword="false" /> otherwise.</returns>
        public bool Contains(T item)
        {
            this.RequiresNotDisposed();

            using (this.ReadLock())
            {
                return this.internalContainer.Contains(item);
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        [SuppressMessage(
            "Performance",
            "HAA0603:Delegate allocation from a method group",
            Justification = "We need this allocation here.")]
        [SuppressMessage(
            "Performance",
            "HAA0401:Possible allocation of reference type enumerator",
            Justification = "We're returning a class enumerator, so we're expecting an allocation anyway.")]
        public IEnumerator<T> GetEnumerator() =>
            AtomicEnumerator<T>.FromCollection(
                this.internalContainer,
                this.ReadLock);

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        [SuppressMessage(
            "Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "Unavoidable.")]
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        ///     Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        public T[] ToArray()
        {
            this.RequiresNotDisposed();

            using (this.ReadLock())
            {
                return this.internalContainer.ToArray();
            }
        }

        /// <summary>
        ///     Copies the elements of the <see cref="PushingCollectionBase{T}" /> to an <see cref="T:System.Array" />,
        ///     starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="PushingCollectionBase{T}" />. The <see cref="T:System.Array" /> must have zero-based
        ///     indexing.
        /// </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(
            Array array,
            int index)
        {
            this.RequiresNotDisposed();

            using (this.ReadLock())
            {
                ((ICollection)this.internalContainer).CopyTo(
                    array,
                    index);
            }
        }

        /// <summary>
        ///     Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            base.DisposeManagedContext();

            Interlocked.Exchange(
                ref this.internalContainer,
                null)?.Clear();
        }

        /// <summary>
        /// Appends the specified item to this pushing collection.
        /// </summary>
        /// <param name="item">The item to append.</param>
        protected void Append(T item)
        {
            this.RequiresNotDisposed();

            if (this.Limit == 0)
            {
                return;
            }

            using (this.WriteLock())
            {
                if (this.InternalContainer.Count == this.Limit)
                {
                    this.InternalContainer.RemoveAt(0);
                }

                this.InternalContainer.Add(item);
            }
        }

        /// <summary>
        /// Appends the specified items to this pushing collection.
        /// </summary>
        /// <param name="items">The items to append.</param>
        protected void Append(T[] items)
        {
            // Validate input
            this.RequiresNotDisposed();
            Contract.RequiresNotNull(in items, nameof(items));

            // Check disabled collection
            if (this.Limit == 0)
            {
                return;
            }

            // Lock on write
            using (this.WriteLock())
            {
                foreach (var item in items)
                {
                    this.InternalContainer.Add(item);

                    if (this.InternalContainer.Count == this.Limit + 1)
                    {
                        this.InternalContainer.RemoveAt(0);
                    }
                }
            }
        }

        /// <summary>
        /// Appends the specified items to the pushing collection.
        /// </summary>
        /// <param name="items">The items to append.</param>
        /// <param name="startIndex">The start index in the array to begin taking items from.</param>
        /// <param name="count">The number of items to append.</param>
        protected void Append(
            T[] items,
            int startIndex,
            int count)
        {
            // Validate input
            this.RequiresNotDisposed();
            Contract.RequiresNotNull(in items, nameof(items));
            Contract.RequiresValidArrayRange(in startIndex, in count, in items, nameof(items));

            // Check disabled collection
            int innerLimit = this.Limit;
            if (innerLimit == 0)
            {
                return;
            }

            ReadOnlySpan<T> copiedItems = new ReadOnlySpan<T>(items, startIndex, count);

            // Lock on write
            using (this.WriteLock())
            {
                // Add all items
                var innerInternalContainer = this.InternalContainer;
                foreach (var item in copiedItems)
                {
                    innerInternalContainer.Add(item);

                    if (innerInternalContainer.Count == innerLimit + 1)
                    {
                        innerInternalContainer.RemoveAt(0);
                    }
                }
            }
        }
    }
}