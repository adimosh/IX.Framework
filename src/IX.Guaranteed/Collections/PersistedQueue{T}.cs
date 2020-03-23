// <copyright file="PersistedQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using IX.StandardExtensions;
using IX.System.IO;
using JetBrains.Annotations;

namespace IX.Guaranteed.Collections
{
    /// <summary>
    ///     A queue that guarantees delivery within disaster recovery scenarios.
    /// </summary>
    /// <typeparam name="T">The type of object in the queue.</typeparam>
    /// <remarks>
    ///     This persisted queue type does not hold anything in memory. All operations are done directly on disk, and,
    ///     therefore, do not negatively impact RAM memory.
    /// </remarks>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    /// <seealso cref="IX.System.Collections.Generic.IQueue{T}" />
    [PublicAPI]
    public class PersistedQueue<T> : PersistedQueueBase<T>, IPersistedQueue<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PersistedQueue{T}" /> class.
        /// </summary>
        /// <param name="persistenceFolderPath">The persistence folder path.</param>
        /// <param name="fileShim">The file shim.</param>
        /// <param name="directoryShim">The directory shim.</param>
        /// <param name="pathShim">The path shim.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="persistenceFolderPath" />
        ///     or
        ///     <paramref name="fileShim" />
        ///     or
        ///     <paramref name="directoryShim" />
        ///     or
        ///     <paramref name="pathShim" />
        ///     is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentInvalidPathException">
        ///     The folder at <paramref name="persistenceFolderPath" /> does not exist,
        ///     or is not accessible.
        /// </exception>
        public PersistedQueue(
            string persistenceFolderPath,
            IFile fileShim,
            IDirectory directoryShim,
            IPath pathShim)
            : this(
                persistenceFolderPath,
                EnvironmentSettings.PersistedCollectionsLockTimeout,
                fileShim,
                directoryShim,
                pathShim)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PersistedQueue{T}" /> class.
        /// </summary>
        /// <param name="persistenceFolderPath">The persistence folder path.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="fileShim">The file shim.</param>
        /// <param name="directoryShim">The directory shim.</param>
        /// <param name="pathShim">The path shim.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="persistenceFolderPath" />
        ///     or
        ///     <paramref name="fileShim" />
        ///     or
        ///     <paramref name="directoryShim" />
        ///     or
        ///     <paramref name="pathShim" />
        ///     is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentInvalidPathException">
        ///     The folder at <paramref name="persistenceFolderPath" /> does not exist,
        ///     or is not accessible.
        /// </exception>
        public PersistedQueue(
            string persistenceFolderPath,
            TimeSpan timeout,
            IFile fileShim,
            IDirectory directoryShim,
            IPath pathShim)
            : base(
                persistenceFolderPath,
                fileShim,
                directoryShim,
                pathShim,
                new DataContractSerializer(typeof(T)),
                timeout)
        {
        }

        /// <summary>
        ///     Gets the number of elements contained in the persisted queue.
        /// </summary>
        /// <value>The count.</value>
        /// <remarks>
        ///     <para>
        ///         This property is not synchronized in any way, and therefore might not reflect the true count, should there be
        ///         many threads accessing it in parallel.
        ///     </para>
        /// </remarks>
        public override int Count
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                using (this.ReadLock())
                {
                    return this.GetPossibleDataFiles().Length;
                }
            }
        }

        /// <summary>
        ///     Clears the queue of all elements.
        /// </summary>
        public override void Clear() => this.ClearData();

        /// <summary>
        ///     This method should not be called, as it will always throw an <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="item">The item to verify.</param>
        /// <returns><see langword="true" /> if the item is queued, <see langword="false" /> otherwise.</returns>
        /// <exception cref="InvalidOperationException">Operation not supported on a persisted queue.</exception>
        public override bool Contains(T item) => throw new InvalidOperationException();

        /// <summary>
        ///     This method should not be called, as it will always throw an <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="PersistedQueue{T}" />. The <see cref="T:System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="InvalidOperationException">Operation not supported on a persisted queue.</exception>
        public override void CopyTo(
            Array array,
            int index) => throw new InvalidOperationException();

        /// <summary>
        ///     De-queues an item and removes it from the queue.
        /// </summary>
        /// <returns>The item that has been de-queued.</returns>
        public override T Dequeue() => this.LoadTopmostItem();

        /// <summary>
        ///     Attempts to de-queue an item and to remove it from queue.
        /// </summary>
        /// <param name="item">The item that has been de-queued, default if unsuccessful.</param>
        /// <returns>
        ///     <see langword="true" /> if an item is de-queued successfully, <see langword="false" /> otherwise, or if the
        ///     queue is empty.
        /// </returns>
        public override bool TryDequeue(out T item)
        {
            try
            {
                item = this.LoadTopmostItem();
                return true;
            }
            catch (Exception)
            {
                item = default;
#pragma warning disable ERP022 // Unobserved exception in generic exception handler - Yes, that's the point of the Try... method
                return false;
#pragma warning restore ERP022 // Unobserved exception in generic exception handler
            }
        }

        /// <summary>
        ///     Tries to load the topmost item and execute an action on it, deleting the topmost object data if the operation is
        ///     successful.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to send to the action.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the invoked action.</param>
        /// <returns>The number of items that have been de-queued.</returns>
        /// <remarks>
        ///     <para>
        ///         Warning! This method has the potential of overrunning its read/write lock timeouts. Please ensure that the
        ///         <paramref name="predicate" /> method
        ///         filters out items in a way that limits the amount of data passing through.
        ///     </para>
        /// </remarks>
        public int DequeueWhilePredicateWithAction<TState>(
            Func<TState, T, bool> predicate,
            Action<TState, IEnumerable<T>> actionToInvoke,
            TState state) => this.TryLoadWhilePredicateWithAction(
            predicate,
            actionToInvoke,
            state);

        /// <summary>
        ///     Tries asynchronously to load the topmost item and execute an action on it, deleting the topmost object data if the
        ///     operation is successful.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to send to the action.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the invoked action.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>The number of items that have been dequeued.</returns>
        /// <remarks>
        ///     <para>
        ///         Warning! This method has the potential of overrunning its read/write lock timeouts. Please ensure that the
        ///         <paramref name="predicate" /> method
        ///         filters out items in a way that limits the amount of data passing through.
        ///     </para>
        /// </remarks>
        public async Task<int> DequeueWhilePredicateWithActionAsync<TState>(
            Func<TState, T, bool> predicate,
            Action<TState, IEnumerable<T>> actionToInvoke,
            TState state,
            CancellationToken cancellationToken = default)
        {
            await Task.Yield();

            cancellationToken.ThrowIfCancellationRequested();

            return this.TryLoadWhilePredicateWithAction(
                predicate,
                actionToInvoke,
                state);
        }

        /// <summary>
        ///     Tries asynchronously to load the topmost item and execute an action on it, deleting the topmost object data if the
        ///     operation is successful.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to send to the action.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the invoked action.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>The number of items that have been dequeued.</returns>
        /// <remarks>
        ///     <para>
        ///         Warning! This method has the potential of overrunning its read/write lock timeouts. Please ensure that the
        ///         <paramref name="predicate" /> method
        ///         filters out items in a way that limits the amount of data passing through.
        ///     </para>
        /// </remarks>
        public Task<int> DequeueWhilePredicateWithActionAsync<TState>(
            Func<TState, T, Task<bool>> predicate,
            Action<TState, IEnumerable<T>> actionToInvoke,
            TState state,
            CancellationToken cancellationToken = default) => this.TryLoadWhilePredicateWithActionAsync(
            predicate,
            actionToInvoke,
            state,
            cancellationToken);

        /// <summary>
        ///     Tries asynchronously to load the topmost item and execute an action on it, deleting the topmost object data if the
        ///     operation is successful.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to send to the action.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the invoked action.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>The number of items that have been dequeued.</returns>
        /// <remarks>
        ///     <para>
        ///         Warning! This method has the potential of overrunning its read/write lock timeouts. Please ensure that the
        ///         <paramref name="predicate" /> method
        ///         filters out items in a way that limits the amount of data passing through.
        ///     </para>
        /// </remarks>
        public Task<int> DequeueWhilePredicateWithActionAsync<TState>(
            Func<TState, T, bool> predicate,
            Func<TState, IEnumerable<T>, Task> actionToInvoke,
            TState state,
            CancellationToken cancellationToken = default) => this.TryLoadWhilePredicateWithActionAsync(
            predicate,
            actionToInvoke,
            state,
            cancellationToken);

        /// <summary>
        ///     Tries asynchronously to load the topmost item and execute an action on it, deleting the topmost object data if the
        ///     operation is successful.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to send to the action.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the invoked action.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>The number of items that have been dequeued.</returns>
        /// <remarks>
        ///     <para>
        ///         Warning! This method has the potential of overrunning its read/write lock timeouts. Please ensure that the
        ///         <paramref name="predicate" /> method
        ///         filters out items in a way that limits the amount of data passing through.
        ///     </para>
        /// </remarks>
        public Task<int> DequeueWhilePredicateWithActionAsync<TState>(
            Func<TState, T, Task<bool>> predicate,
            Func<TState, IEnumerable<T>, Task> actionToInvoke,
            TState state,
            CancellationToken cancellationToken = default) => this.TryLoadWhilePredicateWithActionAsync(
            predicate,
            actionToInvoke,
            state,
            cancellationToken);

        /// <summary>
        ///     De-queues an item from the queue, and executes the specified action on it.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to pass to the action.</typeparam>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the action.</param>
        /// <returns>
        ///     <see langword="true" /> if the de-queuing is successful, and the action performed, <see langword="false" />
        ///     otherwise.
        /// </returns>
        public bool DequeueWithAction<TState>(
            Action<TState, T> actionToInvoke,
            TState state) => this.TryLoadTopmostItemWithAction(
            actionToInvoke,
            state);

        /// <summary>
        ///     Asynchronously de-queues an item from the queue, and executes the specified action on it.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to pass to the action.</typeparam>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the action.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>
        ///     <see langword="true" /> if the dequeuing is successful, and the action performed, <see langword="false" />
        ///     otherwise.
        /// </returns>
        public async Task<bool> DequeueWithActionAsync<TState>(
            Action<TState, T> actionToInvoke,
            TState state,
            CancellationToken cancellationToken = default)
        {
            await Task.Yield();

            cancellationToken.ThrowIfCancellationRequested();

            return this.DequeueWithAction(
                actionToInvoke,
                state);
        }

        /// <summary>
        ///     Asynchronously de-queues an item from the queue, and executes the specified action on it.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to pass to the action.</typeparam>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the action.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>
        ///     <see langword="true" /> if the dequeuing is successful, and the action performed, <see langword="false" />
        ///     otherwise.
        /// </returns>
        public Task<bool> DequeueWithActionAsync<TState>(
            Func<TState, T, Task> actionToInvoke,
            TState state,
            CancellationToken cancellationToken = default) => this.TryLoadTopmostItemWithActionAsync(
            actionToInvoke,
            state,
            cancellationToken);

        /// <summary>
        ///     Queues an item, adding it to the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public override void Enqueue(T item) => this.SaveNewItem(item);

        /// <summary>
        ///     This method should not be called, as it will always throw an <see cref="InvalidOperationException" />.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        /// <exception cref="InvalidOperationException">Operation not supported on a persisted queue.</exception>
        public override IEnumerator<T> GetEnumerator() => throw new InvalidOperationException();

        /// <summary>
        ///     Peeks at the topmost element in the queue, without removing it.
        /// </summary>
        /// <returns>The item peeked at, if any.</returns>
        public override T Peek() => this.PeekTopmostItem();

        /// <summary>
        /// Attempts to peek at the current queue and return the item that is next in line to be dequeued.
        /// </summary>
        /// <param name="item">The item, or default if unsuccessful.</param>
        /// <returns>
        ///   <see langword="true" /> if an item is found, <see langword="false" /> otherwise, or if the queue is empty.
        /// </returns>
        public override bool TryPeek(out T item) => this.TryPeekTopmostItem(out item);

        /// <summary>
        ///     This method should not be called, as it will always throw an <see cref="InvalidOperationException" />.
        /// </summary>
        /// <returns>
        ///     The created array with all element of the queue.
        /// </returns>
        /// <exception cref="InvalidOperationException">Operation not supported on a persisted queue.</exception>
        public override T[] ToArray() => throw new InvalidOperationException();
    }
}