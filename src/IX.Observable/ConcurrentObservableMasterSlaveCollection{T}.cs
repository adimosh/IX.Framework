// <copyright file="ConcurrentObservableMasterSlaveCollection{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;
using IX.Observable.DebugAide;
using IX.System.Threading;
using GlobalThreading = System.Threading;

namespace IX.Observable
{
    /// <summary>
    /// An observable collection created from a master collection (to which updates go) and many slave, read-only collections.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <seealso cref="IX.Observable.ObservableCollectionBase{TItem}" />
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView<>))]
    public class ConcurrentObservableMasterSlaveCollection<T> : ObservableMasterSlaveCollection<T>
    {
        private ReaderWriterLockSlim locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        public ConcurrentObservableMasterSlaveCollection()
            : base()
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        public ConcurrentObservableMasterSlaveCollection(GlobalThreading.SynchronizationContext context)
            : base(context)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableMasterSlaveCollection(bool suppressUndoable)
            : base(suppressUndoable)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableMasterSlaveCollection(GlobalThreading.SynchronizationContext context, bool suppressUndoable)
            : base(context, suppressUndoable)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Gets a synchronization lock item to be used when trying to synchronize read/write operations between threads.
        /// </summary>
        protected override IReaderWriterLock SynchronizationLock => this.locker;

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            GlobalThreading.Interlocked.Exchange(ref this.locker, null)?.Dispose();

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Disposes the general context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            GlobalThreading.Interlocked.Exchange(ref this.locker, null);

            base.DisposeGeneralContext();
        }
    }
}