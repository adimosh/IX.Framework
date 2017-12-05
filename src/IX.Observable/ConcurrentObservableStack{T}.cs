// <copyright file="ConcurrentObservableStack{T}.cs" company="Adrian Mos">
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
    /// A thread-safe stack that broadcasts its changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    /// <remarks>
    /// <para>This class is not serializable. In order to serialize / deserialize content, please use the copying methods and serialize the result.</para>
    /// </remarks>
    [DebuggerDisplay("ConcurrentObservableStack, Count = {Count}")]
    [DebuggerTypeProxy(typeof(StackDebugView<>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "Observable{0}Stack", ItemName = "Item")]
    public class ConcurrentObservableStack<T> : ObservableStack<T>
    {
        private ReaderWriterLockSlim locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableStack{T}"/> class.
        /// </summary>
        public ConcurrentObservableStack()
            : base()
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableStack{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the stack.</param>
        public ConcurrentObservableStack(int capacity)
            : base(capacity)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableStack{T}"/> class.
        /// </summary>
        /// <param name="collection">A collection of items to copy into the stack.</param>
        public ConcurrentObservableStack(IEnumerable<T> collection)
            : base(collection)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        public ConcurrentObservableStack(SynchronizationContext context)
            : base(context)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the stack.</param>
        public ConcurrentObservableStack(SynchronizationContext context, int capacity)
            : base(context, capacity)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="collection">A collection of items to copy into the stack.</param>
        public ConcurrentObservableStack(SynchronizationContext context, IEnumerable<T> collection)
            : base(context, collection)
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