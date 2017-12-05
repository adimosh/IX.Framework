﻿// <copyright file="ConcurrentObservableList{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using IX.Observable.DebugAide;

namespace IX.Observable
{
    /// <summary>
    /// A concurrent observable list.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <seealso cref="IX.Observable.ObservableList{T}" />
    [DebuggerDisplay("ObservableList, Count = {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView<>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "Observable{0}List", ItemName = "Item")]
    public class ConcurrentObservableList<T> : ObservableList<T>
    {
        private ReaderWriterLockSlim locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        public ConcurrentObservableList()
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public ConcurrentObservableList(IEnumerable<T> source)
            : base(source)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        public ConcurrentObservableList(SynchronizationContext context)
            : base(context)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="context">The context.</param>
        public ConcurrentObservableList(IEnumerable<T> source, SynchronizationContext context)
            : base(source, context)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Gets a synchronization lock item to be used when trying to synchronize read/write operations between threads.
        /// </summary>
        protected override ReaderWriterLockSlim SynchronizationLock => this.locker;

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            this.locker.Dispose();

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