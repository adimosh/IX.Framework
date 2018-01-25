// <copyright file="ConcurrentObservableList{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using IX.Observable.DebugAide;
using IX.System.Threading;
using GlobalThreading = System.Threading;

namespace IX.Observable
{
    /// <summary>
    /// A concurrent observable list.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <seealso cref="IX.Observable.ObservableList{T}" />
    [DebuggerDisplay("ObservableList, Count = {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView<>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "ConcurrentObservable{0}List", ItemName = "Item")]
    public class ConcurrentObservableList<T> : ObservableList<T>
    {
        private ReaderWriterLockSlim locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        public ConcurrentObservableList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public ConcurrentObservableList(IEnumerable<T> source)
            : base(source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        public ConcurrentObservableList(GlobalThreading.SynchronizationContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="context">The context.</param>
        public ConcurrentObservableList(IEnumerable<T> source, GlobalThreading.SynchronizationContext context)
            : base(source, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableList(bool suppressUndoable)
            : base(suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableList(IEnumerable<T> source, bool suppressUndoable)
            : base(source, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableList(GlobalThreading.SynchronizationContext context, bool suppressUndoable)
            : base(context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="context">The context.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableList(IEnumerable<T> source, GlobalThreading.SynchronizationContext context, bool suppressUndoable)
            : base(source, context, suppressUndoable)
        {
        }

        /// <summary>
        /// Gets a synchronization lock item to be used when trying to synchronize read/write operations between threads.
        /// </summary>
        protected override IReaderWriterLock SynchronizationLock => this.locker ?? (this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion));

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            this.locker?.Dispose();

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Disposes the general context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            this.locker = null;

            base.DisposeGeneralContext();
        }
    }
}