// <copyright file="ConcurrentObservableDictionary{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using IX.Observable.DebugAide;
using IX.StandardExtensions.Threading;
using IX.System.Threading;
using GlobalThreading = System.Threading;

namespace IX.Observable
{
    /// <summary>
    /// A dictionary that broadcasts its changes.
    /// </summary>
    /// <typeparam name="TKey">The data key type.</typeparam>
    /// <typeparam name="TValue">The data value type.</typeparam>
    [DebuggerDisplay("ConcurrentObservableDictionary, Count = {Count}")]
    [DebuggerTypeProxy(typeof(DictionaryDebugView<,>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "ConcurrentObservable{1}DictionaryBy{0}", ItemName = "Entry", KeyName = "Key", ValueName = "Value")]
    public partial class ConcurrentObservableDictionary<TKey, TValue> : ObservableDictionary<TKey, TValue>
    {
        private ReaderWriterLockSlim locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        public ConcurrentObservableDictionary()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        public ConcurrentObservableDictionary(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ConcurrentObservableDictionary(IEqualityComparer<TKey> equalityComparer)
            : base(equalityComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ConcurrentObservableDictionary(int capacity, IEqualityComparer<TKey> equalityComparer)
            : base(capacity, equalityComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        public ConcurrentObservableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        public ConcurrentObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, int capacity)
            : base(context, capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, IEqualityComparer<TKey> equalityComparer)
            : base(context, equalityComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, int capacity, IEqualityComparer<TKey> equalityComparer)
            : base(context, capacity, equalityComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, IDictionary<TKey, TValue> dictionary)
            : base(context, dictionary)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(context, dictionary, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(bool suppressUndoable)
            : base(suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(int capacity, bool suppressUndoable)
            : base(capacity, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(equalityComparer, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(int capacity, IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(capacity, equalityComparer, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(IDictionary<TKey, TValue> dictionary, bool suppressUndoable)
            : base(dictionary, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer, bool suppressUndoable)
            : base(dictionary, comparer, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, bool suppressUndoable)
            : base(context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, int capacity, bool suppressUndoable)
            : base(context, capacity, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(context, equalityComparer, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, int capacity, IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(context, capacity, equalityComparer, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, IDictionary<TKey, TValue> dictionary, bool suppressUndoable)
            : base(context, dictionary, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ConcurrentObservableDictionary(GlobalThreading.SynchronizationContext context, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer, bool suppressUndoable)
            : base(context, dictionary, comparer, suppressUndoable)
        {
        }

        /// <summary>
        /// Gets a synchronization lock item to be used when trying to synchronize read/write operations between threads.
        /// </summary>
        protected override IReaderWriterLock SynchronizationLock => this.locker;

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd(TKey key, Func<TValue> valueGenerator)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            int newIndex;
            TValue value;

            // Under read/write lock
            using (ReadWriteSynchronizationLocker rwl = this.ReadWriteLock())
            {
                if (this.InternalContainer.TryGetValue(key, out value))
                {
                    // Within read lock, if the key is found, return the value.
                    return value;
                }
                else
                {
                    rwl.Upgrade();

                    if (this.InternalContainer.TryGetValue(key, out value))
                    {
                        // Re-check within a write lock, to ensure that something else hasn't already added it.
                        return value;
                    }

                    // Generate the value
                    value = valueGenerator();

                    // Add the item
                    newIndex = this.InternalContainer.Add(key, value);
                }
            }

            // NOTIFICATIONS

            // Collection changed
            if (newIndex == -1)
            {
                // If no index could be found for an item (Dictionary add)
                this.RaiseCollectionReset();
            }
            else
            {
                // If index was added at a specific index
                this.RaiseCollectionChangedAdd(new KeyValuePair<TKey, TValue>(key, value), newIndex);
            }

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();

            return value;
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState(TKey key, Func<TValue> valueGenerator, Action<TValue> valueAction)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            int newIndex;
            TValue value;

            // Under read/write lock
            using (ReadWriteSynchronizationLocker rwl = this.ReadWriteLock())
            {
                if (this.InternalContainer.TryGetValue(key, out value))
                {
                    // Within read lock, if the key is found, return the value.
                    valueAction(value);
                    return value;
                }
                else
                {
                    rwl.Upgrade();

                    if (this.InternalContainer.TryGetValue(key, out value))
                    {
                        // Re-check within a write lock, to ensure that something else hasn't already added it.
                        valueAction(value);
                        return value;
                    }

                    // Generate the value
                    value = valueGenerator();

                    // Add the item
                    newIndex = this.InternalContainer.Add(key, value);
                }
            }

            // NOTIFICATIONS

            // Collection changed
            if (newIndex == -1)
            {
                // If no index could be found for an item (Dictionary add)
                this.RaiseCollectionReset();
            }
            else
            {
                // If index was added at a specific index
                this.RaiseCollectionChangedAdd(new KeyValuePair<TKey, TValue>(key, value), newIndex);
            }

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();

            return value;
        }

        /// <summary>
        /// Removes a key from the dictionary, then acts on its resulting value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if the variable was successfully removed, <c>false</c> otherwise.</returns>
        public bool RemoveThenAct(TKey key, Action<TValue> action)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            int oldIndex;
            TValue value;

            // Under read/write lock
            using (ReadWriteSynchronizationLocker rwl = this.ReadWriteLock())
            {
                if (this.InternalContainer.TryGetValue(key, out value))
                {
                    rwl.Upgrade();

                    if (this.InternalContainer.TryGetValue(key, out value))
                    {
                        // Re-check within a write lock, to ensure that something else hasn't already removed it.
                        oldIndex = this.InternalContainer.Remove(new KeyValuePair<TKey, TValue>(key, value));

                        action(value);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            // NOTIFICATIONS

            // Collection changed
            if (oldIndex == -1)
            {
                // If no index could be found for an item (Dictionary remove)
                this.RaiseCollectionReset();
            }
            else
            {
                // If index was added at a specific index
                this.RaiseCollectionChangedRemove(new KeyValuePair<TKey, TValue>(key, value), oldIndex);
            }

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();

            return true;
        }

        /// <summary>
        /// Called when the object is being deserialized, in order to set the locker to a new value.
        /// </summary>
        /// <param name="context">The streaming context.</param>
        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
            => GlobalThreading.Interlocked.Exchange(ref this.locker, new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion));

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