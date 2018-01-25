// <copyright file="ObservableDictionary{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using IX.Observable.Adapters;
using IX.Observable.DebugAide;
using IX.Observable.UndoLevels;
using IX.StandardExtensions.Threading;
using IX.Undoable;

namespace IX.Observable
{
    /// <summary>
    /// A dictionary that broadcasts its changes.
    /// </summary>
    /// <typeparam name="TKey">The data key type.</typeparam>
    /// <typeparam name="TValue">The data value type.</typeparam>
    [DebuggerDisplay("ObservableDictionary, Count = {Count}")]
    [DebuggerTypeProxy(typeof(DictionaryDebugView<,>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "Observable{1}DictionaryBy{0}", ItemName = "Entry", KeyName = "Key", ValueName = "Value")]
    public class ObservableDictionary<TKey, TValue> : ObservableCollectionBase<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        public ObservableDictionary()
            : base(new DictionaryCollectionAdapter<TKey, TValue>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        public ObservableDictionary(int capacity)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ObservableDictionary(IEqualityComparer<TKey> equalityComparer)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(equalityComparer)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ObservableDictionary(int capacity, IEqualityComparer<TKey> equalityComparer)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity, equalityComparer)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary, comparer)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        public ObservableDictionary(SynchronizationContext context)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        public ObservableDictionary(SynchronizationContext context, int capacity)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ObservableDictionary(SynchronizationContext context, IEqualityComparer<TKey> equalityComparer)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(equalityComparer)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        public ObservableDictionary(SynchronizationContext context, int capacity, IEqualityComparer<TKey> equalityComparer)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity, equalityComparer)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        public ObservableDictionary(SynchronizationContext context, IDictionary<TKey, TValue> dictionary)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        public ObservableDictionary(SynchronizationContext context, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary, comparer)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(int capacity, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(equalityComparer)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(int capacity, IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity, equalityComparer)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary, comparer)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(SynchronizationContext context, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(SynchronizationContext context, int capacity, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(SynchronizationContext context, IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(equalityComparer)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="equalityComparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(SynchronizationContext context, int capacity, IEqualityComparer<TKey> equalityComparer, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(capacity, equalityComparer)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(SynchronizationContext context, IDictionary<TKey, TValue> dictionary, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="dictionary">A dictionary of items to copy from.</param>
        /// <param name="comparer">A comparer object to use for equality comparison.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableDictionary(SynchronizationContext context, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer, bool suppressUndoable)
            : base(new DictionaryCollectionAdapter<TKey, TValue>(new Dictionary<TKey, TValue>(dictionary, comparer)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Gets the collection of keys in this dictionary.
        /// </summary>
        public ICollection<TKey> Keys => this.CheckDisposed(() => this.ReadLock(() => ((DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer).dictionary.Keys));

        /// <summary>
        /// Gets the collection of keys in this dictionary.
        /// </summary>
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => this.Keys;

        /// <summary>
        /// Gets the collection of values in this dictionary.
        /// </summary>
        public ICollection<TValue> Values => this.CheckDisposed(() => this.ReadLock(() =>
            ((DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer).dictionary.Values));

        /// <summary>
        /// Gets the collection of values in this dictionary.
        /// </summary>
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => this.Values;

        /// <summary>
        /// Gets or sets the value associated with a specific key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value associated with the specified key.</returns>
        public TValue this[TKey key]
        {
            get => this.CheckDisposed(
                (keyL1) => this.ReadLock(
                    (keyL2) => ((DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer).dictionary[keyL2],
                    keyL1),
                key);

            set
            {
                // PRECONDITIONS

                // Current object not disposed
                this.ThrowIfCurrentObjectDisposed();

                // ACTION
                Dictionary<TKey, TValue> dictionary = ((DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer).dictionary;

                // Within a write lock
                using (this.WriteLock())
                {
                    if (dictionary.TryGetValue(key, out TValue val))
                    {
                        // Set the new item
                        dictionary[key] = value;

                        // Push a change undo level
                        this.PushUndoLevel(new DictionaryChangeUndoLevel<TKey, TValue> { Key = key, OldValue = val, NewValue = value });
                    }
                    else
                    {
                        // Add the new item
                        dictionary.Add(key, value);

                        // Push an add undo level
                        this.PushUndoLevel(new DictionaryAddUndoLevel<TKey, TValue> { Key = key, Value = value });
                    }
                }

                // NOTIFICATION
                this.BroadcastChange();
            }
        }

        /// <summary>
        /// Adds an item to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            int newIndex;

            // Under write lock
            using (this.WriteLock())
            {
                // Add the item
                newIndex = ((DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer).Add(key, value);

                // Push the undo level
                this.PushUndoLevel(new AddUndoLevel<KeyValuePair<TKey, TValue>> { AddedItem = new KeyValuePair<TKey, TValue>(key, value), Index = newIndex });
            }

            // NOTIFICATIONS
            this.BroadcastChange();
        }

        /// <summary>
        /// Determines whether the dictionary contains a specific key.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns><c>true</c> whether a key has been found, <c>false</c> otherwise.</returns>
        public bool ContainsKey(TKey key) => this.CheckDisposed(
            (keyL1) => this.ReadLock(
                (keyL2) => ((DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer).dictionary.ContainsKey(keyL2),
                keyL1),
            key);

        /// <summary>
        /// Attempts to remove all info related to a key from the dictionary.
        /// </summary>
        /// <param name="key">The key to remove data from.</param>
        /// <returns><c>true</c> if the removal was successful, <c>false</c> otherwise.</returns>
        public bool Remove(TKey key)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            bool result;
            var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

            // Within a read/write lock
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                // Find out if there's anything to remove
                if (!container.TryGetValue(key, out TValue value))
                {
                    return false;
                }

                // Upgrade the locker to a write lock
                locker.Upgrade();

                // Do the actual removal
                result = container.Remove(key);

                // Push undo level
                if (result)
                {
                    this.PushUndoLevel(new DictionaryRemoveUndoLevel<TKey, TValue> { Key = key, Value = value });
                }
            }

            // NOTIFICATION AND RETURN
            if (result)
            {
                this.BroadcastChange();
            }

            return result;
        }

        /// <summary>
        /// Attempts to fetch a value for a specific key, indicating whether it has been found or not.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value was successfully fetched, <c>false</c> otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.ReadLock())
            {
                return ((DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer).dictionary.TryGetValue(key, out value);
            }
        }

        /// <summary>
        /// Called when contents of this dictionary may have changed.
        /// </summary>
        protected override void ContentsMayHaveChanged()
        {
            this.RaisePropertyChanged(nameof(this.Keys));
            this.RaisePropertyChanged(nameof(this.Values));
            this.RaisePropertyChanged(Constants.ItemsName);
        }

        /// <summary>
        /// Has the last operation undone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <returns><c>true</c> if the undo was successful, <c>false</c> otherwise.</returns>
        protected override bool UndoInternally(StateChange undoRedoLevel, out Action toInvokeOutsideLock)
        {
            if (base.UndoInternally(undoRedoLevel, out toInvokeOutsideLock))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<KeyValuePair<TKey, TValue>> aul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Remove(aul.AddedItem.Key);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case RemoveUndoLevel<KeyValuePair<TKey, TValue>> rul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Add(rul.RemovedItem.Key, rul.RemovedItem.Value);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case ClearUndoLevel<KeyValuePair<TKey, TValue>> cul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        foreach (KeyValuePair<TKey, TValue> item in cul.OriginalItems)
                        {
                            container.Add(item.Key, item.Value);
                        }

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case DictionaryAddUndoLevel<TKey, TValue> daul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Remove(daul.Key);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case DictionaryRemoveUndoLevel<TKey, TValue> raul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Add(raul.Key, raul.Value);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case DictionaryChangeUndoLevel<TKey, TValue> caul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container[caul.Key] = caul.OldValue;

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                default:
                    {
                        toInvokeOutsideLock = null;

                        return false;
                    }
            }

            return true;
        }

        /// <summary>
        /// Has the last undone operation redone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <returns><c>true</c> if the redo was successful, <c>false</c> otherwise.</returns>
        protected override bool RedoInternally(StateChange undoRedoLevel, out Action toInvokeOutsideLock)
        {
            if (base.RedoInternally(undoRedoLevel, out toInvokeOutsideLock))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<KeyValuePair<TKey, TValue>> aul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Add(aul.AddedItem.Key, aul.AddedItem.Value);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case RemoveUndoLevel<KeyValuePair<TKey, TValue>> rul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Remove(rul.RemovedItem.Key);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case ClearUndoLevel<KeyValuePair<TKey, TValue>> cul:
                    {
                        this.InternalContainer.Clear();

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case DictionaryAddUndoLevel<TKey, TValue> daul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Add(daul.Key, daul.Value);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case DictionaryRemoveUndoLevel<TKey, TValue> raul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container.Remove(raul.Key);

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                case DictionaryChangeUndoLevel<TKey, TValue> caul:
                    {
                        var container = (DictionaryCollectionAdapter<TKey, TValue>)this.InternalContainer;

                        container[caul.Key] = caul.NewValue;

                        toInvokeOutsideLock = () => this.BroadcastChange();

                        break;
                    }

                default:
                    {
                        toInvokeOutsideLock = null;

                        return false;
                    }
            }

            return true;
        }

        private void BroadcastChange()
        {
            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Keys));
            this.RaisePropertyChanged(nameof(this.Values));
            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
        }
    }
}