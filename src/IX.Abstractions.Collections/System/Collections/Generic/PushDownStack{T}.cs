// <copyright file="PushDownStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using IX.Abstractions.Collections;
using IX.StandardExtensions.Threading;

namespace IX.System.Collections.Generic
{
    /// <summary>
    /// A stack that pushes down extra items above a certain limit.
    /// </summary>
    /// <typeparam name="T">The stack item type.</typeparam>
    /// <seealso cref="IX.StandardExtensions.Threading.ReaderWriterSynchronizedBase" />
    /// <seealso cref="IX.System.Collections.Generic.IStack{T}" />
    /// <seealso cref="global::System.IDisposable" />
    /// <seealso cref="IStack{T}" />
    [DataContract(Namespace = Constants.DataContractNamespace, Name = "PushDownStackOf{0}")]
    public class PushDownStack<T> : ReaderWriterSynchronizedBase, IStack<T>, IDisposable
    {
        /// <summary>
        /// The limit.
        /// </summary>
        private int limit;

#pragma warning disable IDE0044 // Add readonly modifier - Cannot do that, as the field is deserializable
        /// <summary>
        /// The internal container.
        /// </summary>
        [DataMember(Name = "Items")]
        private List<T> internalContainer;
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Initializes a new instance of the <see cref="PushDownStack{T}"/> class.
        /// </summary>
        public PushDownStack()
            : this(Constants.DefaultPushDownLimit)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushDownStack{T}"/> class.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <exception cref="IX.Abstractions.Collections.LimitArgumentNegativeException"><paramref name="limit"/> is a negative integer.</exception>
        public PushDownStack(int limit)
        {
            if (limit < 0)
            {
                throw new LimitArgumentNegativeException(nameof(limit));
            }

            this.limit = limit;

            this.internalContainer = new List<T>();
        }

        /// <summary>
        /// Gets the number of elements in the observable stack.
        /// </summary>
        /// <value>The current element count.</value>
        public int Count => this.InvokeIfNotDisposed((cThis) => cThis.ReadLock((c2This) => c2This.internalContainer.Count, cThis), this);

        /// <summary>
        /// Gets or sets the number of items in the push-down stack.
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
                    (val, cThis) =>
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
                    },
                    value,
                    this);
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        /// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
        public bool IsSynchronized => ((ICollection)this.internalContainer).IsSynchronized;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The synchronize root.</value>
        public object SyncRoot => ((ICollection)this.internalContainer).SyncRoot;

        /// <summary>
        /// Clears the observable stack.
        /// </summary>
        public void Clear() =>
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - Unavoidable at this point
            this.InvokeIfNotDisposed(
                (thisL1) => thisL1.WriteLock(thisL1.internalContainer.Clear),
                this);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group

        /// <summary>
        /// Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><c>true</c> if the item was found, <c>false</c> otherwise.</returns>
        public bool Contains(T item) =>
            this.InvokeIfNotDisposed(
                (itemL2, thisL2) =>
                    thisL2.ReadLock(
                        (itemL1, thisL1) =>
                            thisL1.internalContainer.Contains(itemL1),
                        itemL2,
                        thisL2),
                item,
                this);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(Array array, int index) =>
            this.InvokeIfNotDisposed(
                (arrayL2, indexL2, referenceL2) => referenceL2.ReadLock(
                    (arrayL1, indexL1, referenceL1) => ((ICollection)referenceL1.internalContainer).CopyTo(arrayL1, indexL1),
                    arrayL2,
                    indexL2,
                    referenceL2),
                array,
                index,
                this);

#pragma warning disable HeapAnalyzerBoxingRule // Value type to reference type conversion causing boxing allocation - Unavoidable due to how the atomic enumerator works
#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator - Unavoidable here

        // TODO: #68 - Eliminate boxing from IEnumerable implementations
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => this.SpawnAtomicEnumerator(this.internalContainer.GetEnumerator());
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
#pragma warning restore HeapAnalyzerBoxingRule // Value type to reference type conversion causing boxing allocation

        /// <summary>
        /// Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek() =>
            this.InvokeIfNotDisposed(
                (reference) => reference.ReadLock(
                    (referenceL2) => (referenceL2.internalContainer.Count > 0) ? referenceL2.internalContainer[referenceL2.internalContainer.Count - 1] : default,
                    reference),
                this);

        /// <summary>
        /// Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop() => this.InvokeIfNotDisposed(
            (reference) =>
            {
                if (reference.limit == 0)
                {
                    return default;
                }

                return reference.WriteLock(
                    (referenceL2) =>
                    {
                        var index = referenceL2.internalContainer.Count - 1;

                        if (index < 0)
                        {
                            return default;
                        }

                        T item = referenceL2.internalContainer[index];

                        referenceL2.internalContainer.RemoveAt(index);

                        return item;
                    },
                    reference);
            },
            this);

        /// <summary>
        /// Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item) =>
            this.InvokeIfNotDisposed(
                (itemL2, cThis) =>
                {
                    if (cThis.limit == 0)
                    {
                        return;
                    }

                    cThis.WriteLock(
                        (itemL1, c2This) =>
                        {
                            if (c2This.internalContainer.Count == c2This.limit)
                            {
                                c2This.internalContainer.RemoveAt(0);
                            }

                            c2This.internalContainer.Add(itemL1);
                        },
                        itemL2,
                        cThis);
                },
                item,
                this);

        /// <summary>
        /// Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        public T[] ToArray() => this.InvokeIfNotDisposed((reference) => reference.ReadLock((ref2) => ref2.internalContainer.ToArray(), reference), this);

        /// <summary>
        /// Sets the capacity to the actual number of elements in the stack if that number is less than 90 percent of current capacity.
        /// </summary>
        public void TrimExcess()
        {
        }

#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator - Unavoidable at this point

        // TODO: #68 - Eliminate boxing from IEnumerable implementations
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            base.DisposeManagedContext();

            Interlocked.Exchange(ref this.internalContainer, null)?.Clear();
        }
    }
}