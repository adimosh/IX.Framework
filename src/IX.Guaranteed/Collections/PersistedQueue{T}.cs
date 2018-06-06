// <copyright file="PersistedQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Linq;
using System.Runtime.Serialization;
using global::System;
using IX.StandardExtensions;
using IX.StandardExtensions.ComponentModel;
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
    public class PersistedQueue<T> : ReaderWriterSynchronizedBase, IQueue<T>
    {
        private readonly IDirectory directoryShim;
        private readonly IFile fileShim;
        private readonly IPath pathShim;

        private readonly Queue<PersistedQueueItem> internalQueue;
        private readonly string dataFolderPath;
        private readonly string poisonFolderPath;
        private readonly DataContractSerializer serializer;

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
        {
            // Parameter validation
            if (string.IsNullOrWhiteSpace(persistenceFolderPath))
            {
                throw new ArgumentNullException(nameof(persistenceFolderPath));
            }

            if (this.fileShim == null)
            {
                throw new ArgumentNullException(nameof(fileShim));
            }

            if (this.directoryShim == null)
            {
                throw new ArgumentNullException(nameof(directoryShim));
            }

            if (this.pathShim == null)
            {
                throw new ArgumentNullException(nameof(pathShim));
            }

            if (!directoryShim.Exists(persistenceFolderPath))
            {
                throw new ArgumentInvalidPathException(nameof(persistenceFolderPath));
            }

            // Dependent state
            this.fileShim = fileShim;
            this.directoryShim = directoryShim;
            this.pathShim = pathShim;

            // Internal state
            this.serializer = new DataContractSerializer(typeof(T));
            this.internalQueue = new Queue<PersistedQueueItem>();

            // Persistence folder paths
            var dataFolderPath = pathShim.Combine(persistenceFolderPath, "Data");
            this.dataFolderPath = dataFolderPath;
            var poisonFolderPath = pathShim.Combine(persistenceFolderPath, "Poison");
            this.poisonFolderPath = poisonFolderPath;

            // Initialize folder paths
            if (!directoryShim.Exists(dataFolderPath))
            {
                directoryShim.CreateDirectory(dataFolderPath);
            }

            if (!directoryShim.Exists(poisonFolderPath))
            {
                directoryShim.CreateDirectory(poisonFolderPath);
            }

            // Initialize objects
            this.LoadExistingItems();
        }

        /// <summary>
        /// Gets the number of elements contained in the persisted queue.
        /// </summary>
        /// <value>The count.</value>
        /// <remarks>
        /// <para>This property is not synchronized in any way, and therefore might not reflect the true count, should there be many threads accessing it in parallel.</para>
        /// </remarks>
        public int Count => this.InvokeIfNotDisposed((reference) => reference.ReadLock((referenceL2) => referenceL2.internalQueue.Count, reference), this);

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The synchronize root.</value>
        object ICollection.SyncRoot => ((ICollection)this.internalQueue).SyncRoot;

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        /// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
        bool ICollection.IsSynchronized => ((ICollection)this.internalQueue).IsSynchronized;

        /// <summary>
        /// Clears the queue of all elements.
        /// </summary>
        public void Clear() => this.InvokeIfNotDisposed(reference => reference.WriteLock((referenceL2) => referenceL2.internalQueue.Clear(), reference), this);

        /// <summary>
        /// Verifies whether or not an item is contained in the queue.
        /// </summary>
        /// <param name="item">The item to verify.</param>
        /// <returns><c>true</c> if the item is queued, <c>false</c> otherwise.</returns>
        public bool Contains(T item) =>
            this.InvokeIfNotDisposed(
                (reference, itemL1) => reference.WriteLock(
                    (referenceL2, itemL2) => referenceL2.internalQueue.Any(p => p.ObjectReference.Equals(itemL2)),
                    reference,
                    itemL1),
                this,
                item);

        /// <summary>
        /// Copies the elements of the <see cref="PersistedQueue{T}" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="PersistedQueue{T}" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        void ICollection.CopyTo(Array array, int index) =>
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
        public T Dequeue() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.WriteLock(
                    (referenceL2) => referenceL2.DequeuePrivate(),
                    referenceL1),
                this);

        /// <summary>
        /// Enqueues an item, adding it to the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item) =>
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
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public global::System.Collections.Generic.IEnumerator<T> GetEnumerator() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.ReadLock(
                    (referenceL2) => referenceL2.internalQueue.Select(p => p.ObjectReference).ToList().GetEnumerator(),
                    referenceL1),
                this);

        /// <summary>
        /// Peeks at the topmost element in the queue, without removing it.
        /// </summary>
        /// <returns>The item peeked at, if any.</returns>
        public T Peek() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.ReadLock(
                    (referenceL2) => referenceL2.internalQueue.Peek().ObjectReference,
                    referenceL1),
                this);

        /// <summary>
        /// Copies all elements of the queue into a new array.
        /// </summary>
        /// <returns>The created array with all element of the queue.</returns>
        public T[] ToArray() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.ReadLock(
                    (referenceL2) => referenceL2.internalQueue.Select(p => p.ObjectReference).ToArray(),
                    referenceL1),
                this);

        /// <summary>
        /// Trims the excess free space from within the queue, reducing the capacity to the actual number of elements.
        /// </summary>
        public void TrimExcess() =>
            this.InvokeIfNotDisposed(
                (referenceL1) => referenceL1.WriteLock(
                    (referenceL2) => referenceL2.internalQueue.TrimExcess(),
                    referenceL1),
                this);

#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator - Unavoidable here
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator

        private void EnqueuePrivate(T item)
        {
            var i = 1;
            string filePath = null;

            DateTime now = DateTime.UtcNow;

            do
            {
                filePath = this.pathShim.Combine(this.dataFolderPath, $"{now.ToString("yyyy.MM.dd.HH.mm.ss.fffffff")}.{i}.dat");
                i++;

                if (i == int.MaxValue)
                {
                    throw new InvalidOperationException();
                }
            }
            while (this.fileShim.Exists(filePath));

            using (global::System.IO.Stream stream = this.fileShim.Create(filePath))
            {
                this.serializer.WriteObject(stream, item);
            }

            this.internalQueue.Enqueue(new PersistedQueueItem(filePath, item));
        }

        private void LoadExistingItems()
        {
#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator - Unavoidable for now
            foreach (var filePath in this.directoryShim.EnumerateFiles(this.dataFolderPath, "*.dat"))
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
            {
                try
                {
                    using (global::System.IO.Stream stream = this.fileShim.OpenRead(filePath))
                    {
                        var item = (T)this.serializer.ReadObject(stream);
                        this.internalQueue.Enqueue(new PersistedQueueItem(filePath, item));
                    }
                }
                catch (SerializationException)
                {
                    var newFilePath = this.pathShim.Combine(this.poisonFolderPath, this.pathShim.GetFileName(filePath));

                    if (this.fileShim.Exists(newFilePath))
                    {
                        this.fileShim.Delete(newFilePath);
                    }

                    this.fileShim.Move(filePath, newFilePath);
                }
            }
        }

        private T DequeuePrivate()
        {
            PersistedQueueItem queueItem = this.internalQueue.Dequeue();

            this.fileShim.Delete(queueItem.Path);

            return queueItem.ObjectReference;
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