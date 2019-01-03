// <copyright file="DisasterRecoveryPersistedQueue.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Threading;
using IX.System.IO;

using JetBrains.Annotations;

namespace IX.Guaranteed.Collections
{
    /// <summary>
    /// A queue used in situations where a regular in-memory queue would be used, except in disaster conditions, where the queue becomes persisted.
    /// </summary>
    /// <typeparam name="T">The type of items in the queue.</typeparam>
    /// <remarks>
    /// <para>Please be advised that the disaster and recovery scenarios that this queue can handle are all application-level error conditions.</para>
    /// <para>This queue cannot handle system faults, such as a power outage or a general operating system failure.</para>
    /// <para>If you need those types of errors also handled, and you need data persistence across those as well, please use <see cref="PersistedQueue{T}"/> instead.</para>
    /// </remarks>
    /// <seealso cref="IX.StandardExtensions.Threading.ReaderWriterSynchronizedBase" />
    /// <seealso cref="IX.Guaranteed.Collections.IPersistedQueue{T}" />
    public class DisasterRecoveryPersistedQueue<T> : ReaderWriterSynchronizedBase, IPersistedQueue<T>
    {
        private System.Collections.Generic.Queue<T> queue;
        private PersistedQueue<T> persistedQueue;

        private int isInDisasterMode;

        private string persistenceFolderPath;
        private IFile fileShim;
        private IDirectory directoryShim;
        private IPath pathShim;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisasterRecoveryPersistedQueue{T}"/> class.
        /// </summary>
        /// <param name="persistenceFolderPath">
        /// The persistence folder path. The path must evaluate to an existing and accessible path.
        /// </param>
        /// <param name="fileShim">
        /// The file shim.
        /// </param>
        /// <param name="directoryShim">
        /// The folder shim.
        /// </param>
        /// <param name="pathShim">
        /// The path shim.
        /// </param>
        public DisasterRecoveryPersistedQueue([NotNull] string persistenceFolderPath, [NotNull] IFile fileShim, [NotNull] IDirectory directoryShim, [NotNull] IPath pathShim)
            : base(Timeout.InfiniteTimeSpan)
        {
            // Validate parameters
            Contract.RequiresNotNullOrWhitespace(persistenceFolderPath, nameof(persistenceFolderPath));
            Contract.RequiresNotNull(fileShim, nameof(fileShim));
            Contract.RequiresNotNull(directoryShim, nameof(directoryShim));
            Contract.RequiresNotNull(pathShim, nameof(pathShim));

            // External state
            this.persistenceFolderPath = persistenceFolderPath;

            // Dependencies
            this.fileShim = fileShim;
            this.directoryShim = directoryShim;
            this.pathShim = pathShim;

#if !STANDARD
            // Automatic disaster detection logic - Only for .NET Framework and .NET Standard 2.0
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainOnUnhandledException;
#endif

            // Initialize queues
            this.queue = new System.Collections.Generic.Queue<T>();

            using (var existingQueue = new PersistedQueue<T>(persistenceFolderPath, Timeout.InfiniteTimeSpan, fileShim, directoryShim, pathShim))
            {
                while (existingQueue.TryDequeue(out T transferItem))
                {
                    this.queue.Enqueue(transferItem);
                }
            }
        }

        /// <summary>
        /// Gets the current items count from the queue.
        /// </summary>
        int ICollection.Count => this.Count;

        /// <summary>
        /// Gets the sync root.
        /// </summary>
        object ICollection.SyncRoot { get; } = new object();

        /// <summary>
        /// Gets a value indicating whether this queue is synchronized.
        /// </summary>
        bool ICollection.IsSynchronized => true;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <exception cref="InvalidOperationException">The queue is currently in disaster mode, enumeration operations are disabled by design.</exception>
        public int Count
        {
            get
            {
                using (this.ReadLock())
                {
                    if (this.isInDisasterMode != 0)
                    {
                        throw new InvalidOperationException(Resources.ErrorTheQueueIsCurrentlyInDisasterMode);
                    }

                    return this.queue.Count;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="InvalidOperationException">The queue is currently in disaster mode, enumeration operations are disabled by design.</exception>
        [NotNull]
        public IEnumerator<T> GetEnumerator()
        {
            using (this.ReadLock())
            {
                if (this.isInDisasterMode == 0)
                {
                    // Not in disaster mode - we can spawn an atomic enumerator
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - unavoidable at this point
                    return this.SpawnAtomicEnumerator<T, Queue<T>.Enumerator>(this.queue.GetEnumerator());
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                }

                // Disaster mode - enumeration disabled
                throw new InvalidOperationException(
                    Resources.ErrorTheQueueIsCurrentlyInDisasterMode);
            }
        }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - unavoidable at this point.
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        [NotNull]
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="InvalidOperationException">The queue is currently in disaster mode, enumeration operations are disabled by design.</exception>
        public void CopyTo([NotNull] Array array, int index)
        {
            using (this.ReadLock())
            {
                if (this.isInDisasterMode == 0)
                {
                    // Not in disaster mode
                    ((ICollection)this.queue).CopyTo(array, index);
                }
                else
                {
                    // Disaster mode - enumeration disabled
                    throw new InvalidOperationException(Resources.ErrorTheQueueIsCurrentlyInDisasterMode);
                }
            }
        }

        /// <summary>
        /// Clears the queue of all elements.
        /// </summary>
        public void Clear()
        {
            using (this.WriteLock())
            {
                if (this.isInDisasterMode == 0)
                {
                    // Not in disaster mode
                    this.queue.Clear();
                }
                else
                {
                    // Disaster mode
                    this.persistedQueue.Clear();
                }
            }
        }

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="item">The item to verify.</param>
        /// <returns>
        ///   <see langword="true" /> if the item is queued, <see langword="false" /> otherwise.
        /// </returns>
        /// <exception cref="InvalidOperationException">The queue is currently in disaster mode, enumeration operations are disabled by design.</exception>
        public bool Contains(T item)
        {
            using (this.ReadLock())
            {
                if (this.isInDisasterMode == 0)
                {
                    // Not in disaster mode
                    return this.queue.Contains(item);
                }
            }

            // Disaster mode
            throw new InvalidOperationException(
                Resources.ErrorTheQueueIsCurrentlyInDisasterMode);
        }

        /// <summary>
        /// De-queues an item and removes it from the queue.
        /// </summary>
        /// <returns>
        /// The item that has been de-queued.
        /// </returns>
        public T Dequeue()
        {
            using (this.WriteLock())
            {
                return this.isInDisasterMode == 0 ? this.queue.Dequeue() : this.persistedQueue.Dequeue();
            }
        }

        /// <summary>
        /// Attempts to de-queue an item and to remove it from queue.
        /// </summary>
        /// <param name="item">The item that has been de-queued, default if unsuccessful.</param>
        /// <returns><see langword="true" /> if an item is de-queued successfully, <see langword="false"/> otherwise, or if the queue is empty.</returns>
        public bool TryDequeue([CanBeNull] out T item)
        {
            using (this.WriteLock())
            {
                return this.isInDisasterMode == 0 ? this.queue.TryDequeue(out item) : this.persistedQueue.TryDequeue(out item);
            }
        }

        /// <summary>
        /// Queues an item, adding it to the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item)
        {
            using (this.WriteLock())
            {
                if (this.isInDisasterMode == 0)
                {
                    // Not in disaster mode
                    this.queue.Enqueue(item);
                }
                else
                {
                    // Disaster mode
                    this.persistedQueue.Enqueue(item);
                }
            }
        }

        /// <summary>
        /// Peeks at the topmost element in the queue, without removing it.
        /// </summary>
        /// <returns>
        /// The item peeked at, if any.
        /// </returns>
        public T Peek()
        {
            using (this.ReadLock())
            {
                return this.isInDisasterMode == 0 ? this.queue.Peek() : this.persistedQueue.Peek();
            }
        }

        /// <summary>
        /// Converts to array.
        /// </summary>
        /// <returns>
        /// The created array with all element of the queue.
        /// </returns>
        /// <exception cref="InvalidOperationException">The queue is currently in disaster mode, enumeration operations are disabled by design.</exception>
        public T[] ToArray()
        {
            using (this.ReadLock())
            {
                if (this.isInDisasterMode == 0)
                {
                    // Not in disaster mode
                    return this.queue.ToArray();
                }
            }

            // Disaster mode
            throw new InvalidOperationException(
                Resources.ErrorTheQueueIsCurrentlyInDisasterMode);
        }

        /// <summary>
        /// Trims the excess free space from within the queue, reducing the capacity to the actual number of elements.
        /// </summary>
        public void TrimExcess()
        {
            using (this.WriteLock())
            {
                if (this.isInDisasterMode == 0)
                {
                    // Not in disaster mode
                    this.queue.TrimExcess();
                }
            }

            // Disaster mode - no trimming!
        }

        /// <summary>
        /// Tries the load topmost item and execute an action on it, deleting the topmost object data if the operation is successful.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to send to the action.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the invoked action.</param>
        /// <returns>
        /// The number of items that have been de-queued.
        /// </returns>
        /// <remarks>
        /// Warning! This method has the potential of overrunning its read/write lock timeouts. Please ensure that the <paramref name="predicate" /> method
        /// filters out items in a way that limits the amount of data passing through.
        /// </remarks>
        public int DequeueWhilePredicateWithAction<TState>(Func<TState, T, bool> predicate, Action<TState, IEnumerable<T>> actionToInvoke, TState state)
        {
            Contract.RequiresNotNull(predicate, nameof(predicate));
            Contract.RequiresNotNull(actionToInvoke, nameof(actionToInvoke));

            using (this.WriteLock())
            {
                if (this.isInDisasterMode != 0)
                {
                    // Disaster mode
                    return this.persistedQueue.DequeueWhilePredicateWithAction(predicate, actionToInvoke, state);
                }

                // Normal mode
                if (this.queue.Count == 0)
                {
                    return 0;
                }

                if (!predicate(state, this.queue.Peek()))
                {
                    return 0;
                }

                int index = 1;
                var items = this.queue.ToArray();
                for (; index < items.Length; index++)
                {
                    if (!predicate(state, items[index]))
                    {
                        break;
                    }
                }

                try
                {
                    actionToInvoke(state, items.Take(index));
                }
                catch (Exception)
                {
#pragma warning disable ERP022 // Unobserved exception in generic exception handler - We know, that's the point of the method.
                    return 0;
#pragma warning restore ERP022 // Unobserved exception in generic exception handler
                }

                for (int i = 0; i < index; i++)
                {
                    this.queue.Dequeue();
                }

                return index;
            }
        }

        /// <summary>
        /// De-queues an item from the queue, and executes the specified action on it.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to pass to the action.</typeparam>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the action.</param>
        /// <returns>
        ///   <see langword="true" /> if the de-queuing is successful, and the action performed, <see langword="false" /> otherwise.
        /// </returns>
        public bool DequeueWithAction<TState>(Action<TState, T> actionToInvoke, TState state)
        {
            Contract.RequiresNotNull(actionToInvoke, nameof(actionToInvoke));

            using (this.WriteLock())
            {
                if (this.isInDisasterMode != 0)
                {
                    // Disaster mode
                    return this.persistedQueue.DequeueWithAction(actionToInvoke, state);
                }

                // Normal mode
                if (this.queue.Count == 0)
                {
                    return false;
                }

                var item = this.queue.Peek();

                try
                {
                    actionToInvoke(state, item);
                }
                catch (Exception)
                {
#pragma warning disable ERP022 // Unobserved exception in generic exception handler - Acceptable
                    return false;
#pragma warning restore ERP022 // Unobserved exception in generic exception handler
                }

                this.queue.Dequeue();
                return true;
            }
        }

        /// <summary>
        /// Puts the queue in disaster mode, indicating that there is a major fault and that the queue contents need to be persisted to disk.
        /// </summary>
        public void Disaster()
        {
            if (Interlocked.CompareExchange(ref this.isInDisasterMode, 1, 0) != 0)
            {
                return;
            }

            using (this.WriteLock())
            {
                PersistedQueue<T> transferQueue;
                try
                {
                    transferQueue = new PersistedQueue<T>(
                        this.persistenceFolderPath,
                        Timeout.InfiniteTimeSpan,
                        this.fileShim,
                        this.directoryShim,
                        this.pathShim);
                }
                catch (Exception)
                {
                    Interlocked.Exchange(ref this.isInDisasterMode, 0);
                    throw;
                }

                Interlocked.Exchange(ref this.persistedQueue, transferQueue);

                try
                {
                    while (this.queue.TryDequeue(out var transferItem))
                    {
                        transferQueue.Enqueue(transferItem);
                    }
                }
                catch (Exception)
                {
                    transferQueue.Dispose();
                    Interlocked.Exchange(ref this.isInDisasterMode, 0);
                    throw;
                }

                Interlocked.Exchange(ref this.queue, null);
            }
        }

        /// <summary>
        /// Recovers after the disaster situation.
        /// </summary>
        public void Recovery()
        {
            if (Interlocked.CompareExchange(ref this.isInDisasterMode, 0, 1) != 1)
            {
                return;
            }

            using (this.WriteLock())
            {
                var transferQueue = new System.Collections.Generic.Queue<T>();

                Interlocked.Exchange(ref this.queue, transferQueue);

                try
                {
                    while (this.persistedQueue.TryDequeue(out var transferItem))
                    {
                        transferQueue.Enqueue(transferItem);
                    }
                }
                catch (Exception)
                {
                    this.persistedQueue.Dispose();
                    Interlocked.Exchange(ref this.isInDisasterMode, 0);
                    throw;
                }

                Interlocked.Exchange(ref this.persistedQueue, null);
            }
        }

#if !STANDARD
        /// <summary>
        /// Invoked when there is an unhandled exception in the current application domain. Puts the queue in disaster mode.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Contract.RequiresNotNullPrivate(e, nameof(e));

            if (e.IsTerminating)
            {
                this.Disaster();
            }
        }
#endif
    }
}