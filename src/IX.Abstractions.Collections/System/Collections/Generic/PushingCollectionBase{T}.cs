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
using IX.StandardExtensions.Threading;
using JetBrains.Annotations;

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
        public int Count => this.InvokeIfNotDisposed(
            cThis => cThis.ReadLock(
                c2This => c2This.internalContainer.Count,
                cThis), this);

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
                this.ThrowIfCurrentObjectDisposed();

                if (value < 0)
                {
                    throw new LimitArgumentNegativeException();
                }

                this.WriteLock(
                    (
                        val,
                        cThis) =>
                    {
                        cThis.limit = val;

                        if (val != 0)
                        {
                            while (cThis.internalContainer.Count > val)
                            {
                                cThis.internalContainer.RemoveAt(0);
                            }
                        }
                        else
                        {
                            cThis.internalContainer.Clear();
                        }
                    }, value,
                    this);
            }
        }

        /// <summary>
        /// Gets the internal container.
        /// </summary>
        /// <value>
        /// The internal container.
        /// </value>
        protected IList<T> InternalContainer => this.internalContainer;

        /// <summary>
        ///     Clears the observable stack.
        /// </summary>
        [SuppressMessage(
            "Performance",
            "HAA0603:Delegate allocation from a method group",
            Justification = "We ned to get a reference to this method to the locker")]
        public void Clear() =>
            this.InvokeIfNotDisposed(
                thisL1 => thisL1.WriteLock(thisL1.internalContainer.Clear),
                this);

        /// <summary>
        ///     Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><see langword="true" /> if the item was found, <see langword="false" /> otherwise.</returns>
        public bool Contains(T item) =>
            this.InvokeIfNotDisposed(
                (
                    itemL2,
                    thisL2) => thisL2.ReadLock(
                    (
                        itemL1,
                        thisL1) => thisL1.internalContainer.Contains(itemL1), itemL2,
                    thisL2), item,
                this);

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        [SuppressMessage(
            "IDisposableAnalyzers.Correctness",
            "IDISP004:Don't ignore return value of type IDisposable.",
            Justification = "We're not.")]
        [SuppressMessage(
            "Performance",
            "HAA0401:Possible allocation of reference type enumerator",
            Justification = "That's expected for an atomic enumerator.")]
        public IEnumerator<T> GetEnumerator() =>
            this.SpawnAtomicEnumerator<T, List<T>.Enumerator>(this.internalContainer.GetEnumerator());

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
        public T[] ToArray() => this.InvokeIfNotDisposed(
            reference => reference.ReadLock(
                ref2 => ref2.internalContainer.ToArray(),
                reference), this);

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
            int index) =>
            this.InvokeIfNotDisposed(
                (
                    arrayL2,
                    indexL2,
                    referenceL2) => referenceL2.ReadLock(
                    (
                        arrayL1,
                        indexL1,
                        referenceL1) => ((ICollection)referenceL1.internalContainer).CopyTo(
                        arrayL1,
                        indexL1), arrayL2,
                    indexL2,
                    referenceL2), array,
                index,
                this);

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
    }
}