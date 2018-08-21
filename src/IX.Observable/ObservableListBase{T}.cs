// <copyright file="ObservableListBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IX.Guaranteed;
using IX.Observable.Adapters;
using IX.Observable.UndoLevels;
using IX.StandardExtensions;
using IX.StandardExtensions.Threading;
using IX.Undoable;

namespace IX.Observable
{
    /// <summary>
    /// A base class for lists that are observable.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <seealso cref="IX.Observable.ObservableCollectionBase{T}" />
    /// <seealso cref="IList" />
    /// <seealso cref="global::System.Collections.Generic.IList{T}" />
    /// <seealso cref="global::System.Collections.Generic.IReadOnlyList{T}" />
    public abstract class ObservableListBase<T> : ObservableCollectionBase<T>, IList<T>, IReadOnlyList<T>, IList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableListBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container.</param>
        protected ObservableListBase(IListAdapter<T> internalContainer)
            : base(internalContainer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableListBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container.</param>
        /// <param name="context">The context.</param>
        protected ObservableListBase(IListAdapter<T> internalContainer, SynchronizationContext context)
            : base(internalContainer, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableListBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        protected ObservableListBase(IListAdapter<T> internalContainer, bool suppressUndoable)
            : base(internalContainer, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableListBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container.</param>
        /// <param name="context">The context.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        protected ObservableListBase(IListAdapter<T> internalContainer, SynchronizationContext context, bool suppressUndoable)
            : base(internalContainer, context, suppressUndoable)
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not this list is of a fixed size.
        /// </summary>
        public virtual bool IsFixedSize => this.InvokeIfNotDisposed((thisL1) => thisL1.ReadLock((thisL2) => thisL2.InternalContainer?.IsFixedSize ?? false, thisL1), this);

        /// <summary>
        /// Gets the internal list container.
        /// </summary>
        /// <value>
        /// The internal list container.
        /// </value>
        protected new ListAdapter<T> InternalContainer => (ListAdapter<T>)base.InternalContainer;

        /// <summary>
        /// Gets the count after an add operation. Used internally.
        /// </summary>
        /// <value>
        /// The count after add.
        /// </value>
        protected virtual int CountAfterAdd => this.Count;

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The item at the specified index.</returns>
        public virtual T this[int index]
        {
            get => this.InvokeIfNotDisposed((indexL1, thisL1) => thisL1.ReadLock((indexL2, thisL2) => thisL2.InternalContainer[indexL2], indexL1, thisL1), index, this);

            set
            {
                // PRECONDITIONS

                // Current object not disposed
                this.ThrowIfCurrentObjectDisposed();

                // ACTION
                T oldValue;

                // Inside a read/write lock
                using (ReadWriteSynchronizationLocker lockContext = this.ReadWriteLock())
                {
                    // Verify if we are within bounds in a read lock
                    if (index >= this.InternalContainer.Count)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    // Upgrade to a write lock
                    lockContext.Upgrade();

                    // Get the old value
                    oldValue = this.InternalContainer[index];

                    // Two undo/redo transactions
                    using (OperationTransaction tc1 = this.CheckItemAutoCapture(value))
                    {
                        using (OperationTransaction tc2 = this.CheckItemAutoRelease(oldValue))
                        {
                            // Replace with new value
                            this.InternalContainer[index] = value;

                            // Push the undo level
                            this.PushUndoLevel(new ChangeAtUndoLevel<T> { Index = index, OldValue = oldValue, NewValue = value });

                            // Mark the second transaction as successful
                            tc2.Success();
                        }

                        // Mark the first transaction as successful
                        tc1.Success();
                    }
                }

                // NOTIFICATION

                // Collection changed
                this.RaiseCollectionChangedChanged(oldValue, value, index);

                // Property changed
                this.RaisePropertyChanged(nameof(this.Count));

                // Contents may have changed
                this.ContentsMayHaveChanged();
            }
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The item at the specified index.</returns>
        object IList.this[int index]
        {
            get => this[index];
            set
            {
                if (value is T v)
                {
                    this[index] = v;

                    return;
                }
                else
                {
                    throw new ArgumentInvalidTypeException();
                }
            }
        }

        /// <summary>
        /// Determines the index of a specific item, if any.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index of the item, or <c>-1</c> if not found.</returns>
        public virtual int IndexOf(T item) => this.InvokeIfNotDisposed(
            (itemL1, thisL1) => thisL1.ReadLock(
                (itemL2, thisL2) => thisL2.InternalContainer.IndexOf(itemL2),
                itemL1,
                thisL1),
            item,
            this);

        /// <summary>
        /// Adds an item to the <see cref="ObservableCollectionBase{T}" />.
        /// </summary>
        /// <param name="items">The objects to add to the <see cref="ObservableCollectionBase{T}" />.</param>
        /// <remarks>
        /// <para>On concurrent collections, this method is write-synchronized.</para>
        /// </remarks>
        public virtual void AddRange(IEnumerable<T> items)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            T[] itemsList = items.ToArray();

            // ACTION
            int newIndex;

            // Inside a write lock
            using (this.WriteLock())
            {
                // Use an undo/redo transaction
                using (OperationTransaction tc = this.CheckItemAutoCapture(itemsList))
                {
                    // Actually add the items
                    newIndex = this.InternalContainer.AddRange(itemsList);

                    // Push an undo level
                    this.PushUndoLevel(new AddMultipleUndoLevel<T> { AddedItems = itemsList, Index = newIndex });

                    // Mark the transaction as successful
                    tc.Success();
                }
            }

            // NOTIFICATION

            // Collection changed
            if (newIndex == -1)
            {
                this.RaiseCollectionReset();
            }
            else
            {
                this.RaiseCollectionChangedAddMultiple(itemsList, newIndex);
            }

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="item">The item.</param>
        public virtual void Insert(int index, T item)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION

            // Inside a write lock
            using (this.WriteLock())
            {
                // Inside an undo/redo transaction
                using (OperationTransaction tc = this.CheckItemAutoCapture(item))
                {
                    // Actually insert
                    this.InternalContainer.Insert(index, item);

                    // Push undo level
                    this.PushUndoLevel(new AddUndoLevel<T> { AddedItem = item, Index = index });

                    // Mark transaction as successful
                    tc.Success();
                }
            }

            // NOTIFICATION

            // Collection changed
            this.RaiseCollectionChangedAdd(item, index);

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();
        }

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to remove an item from.</param>
        public virtual void RemoveAt(int index)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            T item;

            // Inside a read/write lock
            using (ReadWriteSynchronizationLocker lockContext = this.ReadWriteLock())
            {
                // Check to see if we are in range
                if (index >= this.InternalContainer.Count)
                {
                    return;
                }

                // Upgrade the lock to a write lock
                lockContext.Upgrade();

                item = this.InternalContainer[index];

                // Using an undo/redo transaction
                using (OperationTransaction tc = this.CheckItemAutoRelease(item))
                {
                    // Actually do the removal
                    this.InternalContainer.RemoveAt(index);

                    // Push an undo level
                    this.PushUndoLevel(new RemoveUndoLevel<T> { Index = index, RemovedItem = item });

                    // Mark the transaction as successful
                    tc.Success();
                }
            }

            // NOTIFICATION

            // Collection changed
            this.RaiseCollectionChangedRemove(item, index);

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();
        }

        /// <summary>
        /// Adds an item to the <see cref="ObservableListBase{T}" />.
        /// </summary>
        /// <param name="value">The object to add to the <see cref="ObservableListBase{T}" />.</param>
        /// <returns>The index at which the item was added.</returns>
        int IList.Add(object value)
        {
            if (value is T v)
            {
                this.Add(v);

                return this.CountAfterAdd - 1;
            }

            throw new ArgumentInvalidTypeException(nameof(value));
        }

        /// <summary>
        /// Determines whether the <see cref="ObservableListBase{T}" /> contains a specific value.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="ObservableListBase{T}" />.</param>
        /// <returns>
        /// true if <paramref name="value" /> is found in the <see cref="ObservableListBase{T}" />; otherwise, false.
        /// </returns>
        bool IList.Contains(object value)
        {
            if (value is T v)
            {
                return this.Contains(v);
            }

            return false;
        }

        /// <summary>
        /// Determines the index of a specific item, if any.
        /// </summary>
        /// <param name="value">The item value.</param>
        /// <returns>The index of the item, or <c>-1</c> if not found.</returns>
        int IList.IndexOf(object value)
        {
            if (value is T v)
            {
                return this.IndexOf(v);
            }

            return -1;
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="value">The item value.</param>
        void IList.Insert(int index, object value)
        {
            if (value is T v)
            {
                this.Insert(index, v);

                return;
            }

            throw new ArgumentInvalidTypeException(nameof(value));
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ObservableListBase{T}" />.
        /// </summary>
        /// <param name="value">The object value to remove from the <see cref="ObservableListBase{T}" />.</param>
        void IList.Remove(object value)
        {
            if (value is T v)
            {
                this.Remove(v);
            }
        }

        /// <summary>
        /// Called when items are added to a collection.
        /// </summary>
        /// <param name="addedItems">The added items.</param>
        /// <param name="index">The index.</param>
        protected virtual void RaiseCollectionChangedAddMultiple(IEnumerable<T> addedItems, int index)
            => this.RaiseCollectionAdd(index, addedItems);

        /// <summary>
        /// Called when items are removed from a collection.
        /// </summary>
        /// <param name="removedItems">The removed items.</param>
        /// <param name="index">The index.</param>
        protected virtual void RaiseCollectionChangedRemoveMultiple(IEnumerable<T> removedItems, int index)
            => this.RaiseCollectionRemove(index, removedItems);

        /// <summary>
        /// Has the last operation undone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <param name="state">The state object to pass to the invocation.</param>
        /// <returns><c>true</c> if the undo was successful, <c>false</c> otherwise.</returns>
        protected override bool UndoInternally(StateChange undoRedoLevel, out Action<object> toInvokeOutsideLock, out object state)
        {
            if (base.UndoInternally(undoRedoLevel, out toInvokeOutsideLock, out state))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<T> aul:
                    {
                        var index = aul.Index;

                        this.InternalContainer.RemoveAt(index);

                        T item = aul.AddedItem;

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            ul.IsCapturedIntoUndoContext &&
                            ul.ParentUndoContext == this)
                        {
                            ul.ReleaseFromUndoContext();
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, T, int>;

                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };

                        state = new Tuple<ObservableListBase<T>, T, int>(this, item, index);

                        break;
                    }

                case AddMultipleUndoLevel<T> amul:
                    {
                        var index = amul.Index;

                        for (var i = 0; i < amul.AddedItems.Length; i++)
                        {
                            this.InternalContainer.RemoveAt(index);
                        }

                        IEnumerable<T> items = amul.AddedItems;

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - currently unavoidable
                            foreach (IUndoableItem ul in items.Cast<IUndoableItem>().Where((p, thisL1) => p.IsCapturedIntoUndoContext && p.ParentUndoContext == thisL1, this))
                            {
                                ul.ReleaseFromUndoContext();
                            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, IEnumerable<T>, int>;

                            convertedState.Item1.RaiseCollectionChangedRemoveMultiple(convertedState.Item2, convertedState.Item3);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };

                        state = new Tuple<ObservableListBase<T>, IEnumerable<T>, int>(this, items, index);

                        break;
                    }

                case RemoveUndoLevel<T> rul:
                    {
                        T item = rul.RemovedItem;
                        var index = rul.Index;

                        this.InternalContainer.Insert(index, item);

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            !ul.IsCapturedIntoUndoContext)
                        {
                            ul.CaptureIntoUndoContext(this);
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, T, int>;

                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };

                        state = new Tuple<ObservableListBase<T>, T, int>(this, item, index);

                        break;
                    }

                case ClearUndoLevel<T> cul:
                    {
                        foreach (T t in cul.OriginalItems)
                        {
                            this.InternalContainer.Add(t);
                        }

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
                            foreach (IUndoableItem ul in cul.OriginalItems.Cast<IUndoableItem>().Where(p => !p.IsCapturedIntoUndoContext))
                            {
                                ul.CaptureIntoUndoContext(this);
                            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as ObservableListBase<T>;

                            convertedState.RaiseCollectionReset();
                            convertedState.RaisePropertyChanged(nameof(convertedState.Count));
                            convertedState.ContentsMayHaveChanged();
                        };

                        state = this;

                        break;
                    }

                case ChangeAtUndoLevel<T> caul:
                    {
                        T oldItem = caul.NewValue;
                        T newItem = caul.OldValue;
                        var index = caul.Index;

                        this.InternalContainer[index] = newItem;

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
                            if (newItem is IUndoableItem ul &&
                                !ul.IsCapturedIntoUndoContext)
                            {
                                ul.CaptureIntoUndoContext(this);
                            }

                            if (oldItem is IUndoableItem ol &&
                                ol.IsCapturedIntoUndoContext &&
                                ol.ParentUndoContext == this)
                            {
                                ol.ReleaseFromUndoContext();
                            }
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, T, T, int>;

                            convertedState.Item1.RaiseCollectionChangedChanged(convertedState.Item2, convertedState.Item3, convertedState.Item4);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };

                        state = new Tuple<ObservableListBase<T>, T, T, int>(this, oldItem, newItem, index);

                        break;
                    }

                default:
                    {
                        toInvokeOutsideLock = null;
                        state = null;

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
        /// <param name="state">The state object to pass to the invocation.</param>
        /// <returns><c>true</c> if the redo was successful, <c>false</c> otherwise.</returns>
        protected override bool RedoInternally(StateChange undoRedoLevel, out Action<object> toInvokeOutsideLock, out object state)
        {
            if (base.RedoInternally(undoRedoLevel, out toInvokeOutsideLock, out state))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<T> aul:
                    {
                        var index = aul.Index;
                        T item = aul.AddedItem;

                        this.InternalContainer.Insert(index, item);

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            !ul.IsCapturedIntoUndoContext)
                        {
                            ul.CaptureIntoUndoContext(this);
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, T, int>;

                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };

                        state = new Tuple<ObservableListBase<T>, T, int>(this, item, index);

                        break;
                    }

                case AddMultipleUndoLevel<T> amul:
                    {
                        var index = amul.Index;
                        IEnumerable<T> items = amul.AddedItems;

                        items.Reverse().ForEach((p, thisL1, indexL1) => thisL1.InternalContainer.Insert(indexL1, p), this, index);

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
                            foreach (IUndoableItem ul in amul.AddedItems.Cast<IUndoableItem>().Where(p => !p.IsCapturedIntoUndoContext))
                            {
                                ul.CaptureIntoUndoContext(this);
                            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, IEnumerable<T>, int>;

                            convertedState.Item1.RaiseCollectionChangedAddMultiple(convertedState.Item2, convertedState.Item3);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };

                        state = new Tuple<ObservableListBase<T>, IEnumerable<T>, int>(this, items, index);

                        break;
                    }

                case RemoveUndoLevel<T> rul:
                    {
                        T item = rul.RemovedItem;
                        var index = rul.Index;

                        this.InternalContainer.RemoveAt(index);

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            ul.IsCapturedIntoUndoContext &&
                            ul.ParentUndoContext == this)
                        {
                            ul.ReleaseFromUndoContext();
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, T, int>;

                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };

                        state = new Tuple<ObservableListBase<T>, T, int>(this, item, index);

                        break;
                    }

                case ClearUndoLevel<T> cul:
                    {
                        this.InternalContainer.Clear();

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
                            foreach (IUndoableItem ul in cul.OriginalItems.Cast<IUndoableItem>().Where((p, thisL1) => p.IsCapturedIntoUndoContext && p.ParentUndoContext == thisL1, this))
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                            {
                                ul.ReleaseFromUndoContext();
                            }
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as ObservableListBase<T>;

                            convertedState.RaiseCollectionReset();
                            convertedState.RaisePropertyChanged(nameof(convertedState.Count));
                            convertedState.ContentsMayHaveChanged();
                        };

                        state = this;

                        break;
                    }

                case ChangeAtUndoLevel<T> caul:
                    {
                        T oldItem = caul.OldValue;
                        T newItem = caul.NewValue;
                        var index = caul.Index;

                        this.InternalContainer[index] = newItem;

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
                            if (newItem is IUndoableItem ul &&
                                !ul.IsCapturedIntoUndoContext)
                            {
                                ul.CaptureIntoUndoContext(this);
                            }

                            if (oldItem is IUndoableItem ol &&
                                ol.IsCapturedIntoUndoContext &&
                                ol.ParentUndoContext == this)
                            {
                                ol.ReleaseFromUndoContext();
                            }
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableListBase<T>, T, T, int>;

                            convertedState.Item1.RaiseCollectionChangedChanged(convertedState.Item2, convertedState.Item3, convertedState.Item4);
                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.ContentsMayHaveChanged();
                        };
                        state = new Tuple<ObservableListBase<T>, T, T, int>(this, oldItem, newItem, index);

                        break;
                    }

                default:
                    {
                        toInvokeOutsideLock = null;
                        state = null;

                        return false;
                    }
            }

            return true;
        }
    }
}