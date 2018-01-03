// <copyright file="ObservableListBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        protected ObservableListBase(ListAdapter<T> internalContainer)
            : base(internalContainer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableListBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container.</param>
        /// <param name="context">The context.</param>
        protected ObservableListBase(ListAdapter<T> internalContainer, SynchronizationContext context)
            : base(internalContainer, context)
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not this list is of a fixed size.
        /// </summary>
        public virtual bool IsFixedSize => this.CheckDisposed(() => this.ReadLock(() => this.InternalListContainer?.IsFixedSize ?? false));

        /// <summary>
        /// Gets the internal list container.
        /// </summary>
        /// <value>
        /// The internal list container.
        /// </value>
        protected ListAdapter<T> InternalListContainer => (ListAdapter<T>)this.InternalContainer;

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
            get => this.CheckDisposed(() => this.ReadLock(() => this.InternalListContainer[index]));

            set
            {
                // PRECONDITIONS

                // Current object not disposed
                this.ThrowIfCurrentObjectDisposed();

                // Current item to be set is not part of a different undo/redo context
                if (this.ItemsAreUndoable &&
                    this.AutomaticallyCaptureSubItems &&
                    value is IUndoableItem undoRedoContextCheckItem &&
                    undoRedoContextCheckItem.IsCapturedIntoUndoContext &&
                    undoRedoContextCheckItem.ParentUndoContext != this)
                {
                    throw new ItemAlreadyCapturedIntoUndoContextException();
                }

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
                    oldValue = this.InternalListContainer[index];

                    // Replace with new value
                    this.InternalListContainer[index] = value;

                    // Push the undo level
                    this.PushUndoLevel(new ChangeAtUndoLevel<T> { Index = index, OldValue = oldValue, NewValue = value });

                    // Release old item and capture new item into undo/redo context, if capturing automatically
                    if (this.ItemsAreUndoable && this.AutomaticallyCaptureSubItems)
                    {
                        if (oldValue is IUndoableItem ui)
                        {
                            ui.ReleaseFromUndoContext();
                        }

                        if (value is IUndoableItem ni)
                        {
                            ni.CaptureIntoUndoContext(this);
                        }
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
        public virtual int IndexOf(T item) => this.CheckDisposed(
            (itemL1) => this.ReadLock(
                (itemL2) => this.InternalListContainer.IndexOf(itemL2),
                itemL1),
            item);

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

            // Items are not bound to a different context
            if (this.ItemsAreUndoable &&
                this.AutomaticallyCaptureSubItems &&
                itemsList.Any(p =>
                {
                    var ui = (IUndoableItem)p;
                    return ui.IsCapturedIntoUndoContext && ui.ParentUndoContext != this;
                }))
            {
                throw new ItemAlreadyCapturedIntoUndoContextException();
            }

            // ACTION
            int newIndex;

            // Inside a write lock
            using (this.WriteLock())
            {
                newIndex = ((ListAdapter<T>)this.InternalContainer).AddRange(itemsList);
                this.PushUndoLevel(new AddMultipleUndoLevel<T> { AddedItems = itemsList, Index = newIndex });

                itemsList.Cast<IUndoableItem>().ForEach(p => p.CaptureIntoUndoContext(this));
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

            // Items aren't caught in a different undo context
            if (this.ItemsAreUndoable &&
                this.AutomaticallyCaptureSubItems &&
                item is IUndoableItem uiTest &&
                uiTest.IsCapturedIntoUndoContext &&
                uiTest.ParentUndoContext != this)
            {
                throw new ItemAlreadyCapturedIntoUndoContextException();
            }

            // ACTION

            // Inside a write lock
            using (this.WriteLock())
            {
                // Actually insert
                this.InternalListContainer.Insert(index, item);

                // Push undo level
                this.PushUndoLevel(new AddUndoLevel<T> { AddedItem = item, Index = index });

                // Capture sub-item, if configured
                if (this.ItemsAreUndoable &&
                    this.AutomaticallyCaptureSubItems &&
                    item is IUndoableItem ui &&
                    !ui.IsCapturedIntoUndoContext)
                {
                    ui.CaptureIntoUndoContext(this);
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

                // Actually do the removal
                item = this.InternalListContainer[index];
                this.InternalListContainer.RemoveAt(index);

                // Push an undo level
                this.PushUndoLevel(new RemoveUndoLevel<T> { Index = index, RemovedItem = item });

                // If configured, release the item from the undo/redo context
                if (this.ItemsAreUndoable &&
                    this.AutomaticallyCaptureSubItems &&
                    item is IUndoableItem ui &&
                    ui.IsCapturedIntoUndoContext)
                {
                    ui.ReleaseFromUndoContext();
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
        /// <returns><c>true</c> if the undo was successful, <c>false</c> otherwise.</returns>
        protected override bool UndoInternally(UndoRedoLevel undoRedoLevel, out Action toInvokeOutsideLock)
        {
            if (base.UndoInternally(undoRedoLevel, out toInvokeOutsideLock))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<T> aul:
                    {
                        var index = aul.Index;

                        this.InternalListContainer.RemoveAt(index);

                        T item = aul.AddedItem;

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            ul.IsCapturedIntoUndoContext &&
                            ul.ParentUndoContext == this)
                        {
                            ul.ReleaseFromUndoContext();
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedRemove(item, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case AddMultipleUndoLevel<T> amul:
                    {
                        var index = amul.Index;

                        for (var i = 0; i < amul.AddedItems.Length; i++)
                        {
                            this.InternalListContainer.RemoveAt(index);
                        }

                        IEnumerable<T> items = amul.AddedItems;

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
                            foreach (IUndoableItem ul in items.Cast<IUndoableItem>().Where(p => p.IsCapturedIntoUndoContext && p.ParentUndoContext == this))
                            {
                                ul.ReleaseFromUndoContext();
                            }
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedRemoveMultiple(items, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case RemoveUndoLevel<T> rul:
                    {
                        T item = rul.RemovedItem;
                        var index = rul.Index;

                        this.InternalListContainer.Insert(index, item);

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            !ul.IsCapturedIntoUndoContext)
                        {
                            ul.CaptureIntoUndoContext(this);
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedAdd(item, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case ClearUndoLevel<T> cul:
                    {
                        foreach (T t in cul.OriginalItems)
                        {
                            this.InternalListContainer.Add(t);
                        }

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
                            foreach (IUndoableItem ul in cul.OriginalItems.Cast<IUndoableItem>().Where(p => !p.IsCapturedIntoUndoContext))
                            {
                                ul.CaptureIntoUndoContext(this);
                            }
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionReset();
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case ChangeAtUndoLevel<T> caul:
                    {
                        T oldItem = caul.NewValue;
                        T newItem = caul.OldValue;
                        var index = caul.Index;

                        this.InternalListContainer[index] = newItem;

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

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedChanged(oldItem, newItem, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

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
        protected override bool RedoInternally(UndoRedoLevel undoRedoLevel, out Action toInvokeOutsideLock)
        {
            if (base.RedoInternally(undoRedoLevel, out toInvokeOutsideLock))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<T> aul:
                    {
                        var index = aul.Index;
                        T item = aul.AddedItem;

                        this.InternalListContainer.Insert(index, item);

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            !ul.IsCapturedIntoUndoContext)
                        {
                            ul.CaptureIntoUndoContext(this);
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedAdd(item, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case AddMultipleUndoLevel<T> amul:
                    {
                        var index = amul.Index;
                        IEnumerable<T> items = amul.AddedItems;

                        items.Reverse().ForEach(p => this.InternalListContainer.Insert(index, p));

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
                            foreach (IUndoableItem ul in amul.AddedItems.Cast<IUndoableItem>().Where(p => !p.IsCapturedIntoUndoContext))
                            {
                                ul.CaptureIntoUndoContext(this);
                            }
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedAddMultiple(items, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case RemoveUndoLevel<T> rul:
                    {
                        T item = rul.RemovedItem;
                        var index = rul.Index;

                        this.InternalListContainer.RemoveAt(index);

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems &&
                            item is IUndoableItem ul &&
                            ul.IsCapturedIntoUndoContext &&
                            ul.ParentUndoContext == this)
                        {
                            ul.ReleaseFromUndoContext();
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedRemove(item, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case ClearUndoLevel<T> cul:
                    {
                        this.InternalContainer.Clear();

                        if (this.ItemsAreUndoable &&
                            this.AutomaticallyCaptureSubItems)
                        {
                            foreach (IUndoableItem ul in cul.OriginalItems.Cast<IUndoableItem>().Where(p => p.IsCapturedIntoUndoContext && p.ParentUndoContext == this))
                            {
                                ul.ReleaseFromUndoContext();
                            }
                        }

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionReset();
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

                        break;
                    }

                case ChangeAtUndoLevel<T> caul:
                    {
                        T oldItem = caul.OldValue;
                        T newItem = caul.NewValue;
                        var index = caul.Index;

                        this.InternalListContainer[index] = newItem;

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

                        toInvokeOutsideLock = () =>
                        {
                            this.RaiseCollectionChangedChanged(oldItem, newItem, index);
                            this.RaisePropertyChanged(nameof(this.Count));
                            this.ContentsMayHaveChanged();
                        };

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
    }
}