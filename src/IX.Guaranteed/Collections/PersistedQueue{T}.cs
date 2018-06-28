// <copyright file="PersistedQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Linq;
using System.Runtime.Serialization;
using global::System;
using IX.StandardExtensions;
using IX.StandardExtensions.Threading;
using IX.System.Collections.Generic;
using IX.System.IO;

namespace IX.Guaranteed.Collections
{
    /// <summary>
    /// A queue that guarantees delivery within disaster recovery scenarios.
    /// </summary>
    /// <typeparam name="T">The type of object in the queue.</typeparam>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    /// <seealso cref="IX.System.Collections.Generic.IQueue{T}" />
    public class PersistedQueue<T> : PersistedQueueBase<T>
    {
        private readonly Queue<string> internalQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedQueue{T}"/> class.
        /// </summary>
        /// <param name="persistenceFolderPath">The persistence folder path.</param>
        /// <param name="fileShim">The file shim.</param>
        /// <param name="directoryShim">The directory shim.</param>
        /// <param name="pathShim">The path shim.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="persistenceFolderPath"/>
        /// or
        /// <paramref name="fileShim" />
        /// or
        /// <paramref name="directoryShim"/>
        /// or
        /// <paramref name="pathShim"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentInvalidPathException">The folder at <paramref name="persistenceFolderPath"/> does not exist, or is not accessible.</exception>
        public PersistedQueue(string persistenceFolderPath, IFile fileShim, IDirectory directoryShim, IPath pathShim)
            : base(persistenceFolderPath, fileShim, directoryShim, pathShim, new DataContractSerializer(typeof(T)))
        {
            // Internal state
            this.internalQueue = new Queue<string>();

            // Initialize objects
#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator - Unavoidable
            foreach (Tuple<T, string> item in this.LoadValidItemObjectHandles())
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
            {
                this.internalQueue.Enqueue(item.Item2);
            }

            GC.Collect();
        }

        /// <summary>
        /// Gets the number of elements contained in the persisted queue.
        /// </summary>
        /// <value>The count.</value>
        /// <remarks>
        /// <para>This property is not synchronized in any way, and therefore might not reflect the true count, should there be many threads accessing it in parallel.</para>
        /// </remarks>
        public override int Count => this.InvokeIfNotDisposed((reference) => reference.ReadLock((referenceL2) => referenceL2.internalQueue.Count, reference), this);

        /// <summary>
        /// Clears the queue of all elements.
        /// </summary>
        public override void Clear() =>
            this.InvokeIfNotDisposed(
                reference => reference.WriteLock(
                    (referenceL2) =>
                    {
                        referenceL2.internalQueue.Clear();
                        referenceL2.ClearData();
                    }, reference),
                this);

        /// <summary>
        /// This method should not be called, as it will always throw an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="item">The item to verify.</param>
        /// <returns><c>true</c> if the item is queued, <c>false</c> otherwise.</returns>
        public override bool Contains(T item) => throw new InvalidOperationException();

        /// <summary>
        /// Copies the elements of the <see cref="PersistedQueue{T}" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="PersistedQueue{T}" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public override void CopyTo(Array array, int index) =>
            this.InvokeIfNotDisposed(
                (referenceL1, arrayL1, indexL1) => referenceL1.ReadLock(
                    (referenceL2, arrayL2, indexL2) => ((ICollection)referenceL2.internalQueue).CopyTo(arrayL2, indexL2),
                    referenceL1,
                    arrayL1,
                    indexL1),
                this,
                array,
                index);

        /// <summary>
        /// Dequeues an item and removes it from the queue.
        /// </summary>
        /// <returns>The item that has been dequeued.</returns>
        public override T Dequeue() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.WriteLock(
                    (referenceL2) => referenceL2.LoadTopmostItem(),
                    referenceL1),
                this);

        /// <summary>
        /// Enqueues an item, adding it to the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public override void Enqueue(T item) =>
            this.InvokeIfNotDisposed(
                (referenceL1, itemL1) => referenceL1.WriteLock(
                    (referenceL2, itemL2) =>
                    {
                        referenceL2.EnqueuePrivate(itemL2);
                    },
                    referenceL1,
                    itemL1),
                this,
                item);

        /// <summary>
        /// This method should not be called, as it will always throw an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public override global::System.Collections.Generic.IEnumerator<T> GetEnumerator() => throw new InvalidOperationException();

        /// <summary>
        /// Peeks at the topmost element in the queue, without removing it.
        /// </summary>
        /// <returns>The item peeked at, if any.</returns>
        public override T Peek() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.ReadLock(
                    (referenceL2) => referenceL2.PeekTopmostItem(),
                    referenceL1),
                this);

        /// <summary>
        /// This method should not be called, as it will always throw an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>The created array with all element of the queue.</returns>
        public override T[] ToArray() => throw new InvalidOperationException();

        /// <summary>
        /// Trims the excess free space from within the queue, reducing the capacity to the actual number of elements.
        /// </summary>
        public override void TrimExcess() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.WriteLock(
                    (referenceL2) => referenceL2.internalQueue.TrimExcess(),
                    referenceL1),
                this);

        private void EnqueuePrivate(T item)
        {
            var filePath = this.SaveNewItem(item);

            this.internalQueue.Enqueue(filePath);
        }

        private readonly struct PersistedQueueItem
        {
            internal readonly string Path;
            internal readonly T ObjectReference;

            internal PersistedQueueItem(string path, T objectReference)
            {
#if DEBUG
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException(nameof(path));
                }
#endif

                this.Path = path;
                this.ObjectReference = objectReference;
            }
        }
    }
}