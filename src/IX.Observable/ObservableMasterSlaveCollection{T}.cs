// <copyright file="ObservableMasterSlaveCollection{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using IX.Observable.Adapters;
using IX.Observable.DebugAide;
using IX.Observable.UndoLevels;
using IX.StandardExtensions.Threading;

namespace IX.Observable
{
    /// <summary>
    /// An observable collection created from a master collection (to which updates go) and many slave, read-only collections.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <seealso cref="IX.Observable.ObservableListBase{T}" />
    /// <seealso cref="global::System.Collections.Generic.IList{T}" />
    /// <seealso cref="global::System.Collections.Generic.IReadOnlyCollection{T}" />
    /// <seealso cref="global::System.Collections.Generic.ICollection{T}" />
    /// <seealso cref="ICollection" />
    /// <seealso cref="IList" />
    /// <seealso cref="Observable.ObservableCollectionBase{TItem}" />
    [DebuggerDisplay("ObservableMasterSlaveCollection, Count = {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView<>))]
    public class ObservableMasterSlaveCollection<T> : ObservableListBase<T>, IList<T>, IReadOnlyCollection<T>, ICollection<T>, ICollection, IList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        public ObservableMasterSlaveCollection()
            : base(new MultiListMasterSlaveListAdapter<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        public ObservableMasterSlaveCollection(SynchronizationContext context)
            : base(new MultiListMasterSlaveListAdapter<T>(), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableMasterSlaveCollection(bool suppressUndoable)
            : base(new MultiListMasterSlaveListAdapter<T>(), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableMasterSlaveCollection{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableMasterSlaveCollection(SynchronizationContext context, bool suppressUndoable)
            : base(new MultiListMasterSlaveListAdapter<T>(), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Gets the count after an add operation. Used internally.
        /// </summary>
        /// <value>
        /// The count after add.
        /// </value>
        protected override int CountAfterAdd => ((MultiListMasterSlaveListAdapter<T>)this.InternalContainer).MasterCount;

        /// <summary>
        /// Sets the master list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        public void SetMasterList<TList>(TList list)
                    where TList : class, IList<T>, INotifyCollectionChanged
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.WriteLock())
            {
                ((MultiListMasterSlaveListAdapter<T>)this.InternalContainer).SetMaster(list);
            }

            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
        }

        /// <summary>
        /// Sets a slave list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        public void SetSlaveList<TList>(TList list)
                    where TList : class, IEnumerable<T>, INotifyCollectionChanged
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.WriteLock())
            {
                ((MultiListMasterSlaveListAdapter<T>)this.InternalContainer).SetSlave(list);
            }

            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
        }

        /// <summary>
        /// Removes a slave list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        public void RemoveSlaveList<TList>(TList list)
                    where TList : class, IEnumerable<T>, INotifyCollectionChanged
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.WriteLock())
            {
                ((MultiListMasterSlaveListAdapter<T>)this.InternalContainer).RemoveSlave(list);
            }

            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
        }

        /// <summary>
        /// Adds an item to the <see cref="T:IX.Observable.ObservableCollectionBase`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:IX.Observable.ObservableCollectionBase`1" />.</param>
        /// <remarks>On concurrent collections, this method is write-synchronized.</remarks>
        public override void Add(T item)
        {
            this.IncreaseIgnoreMustResetCounter();
            base.Add(item);
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="item">The item.</param>
        public override void Insert(int index, T item)
        {
            this.IncreaseIgnoreMustResetCounter();
            base.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:IX.Observable.ObservableCollectionBase`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:IX.Observable.ObservableCollectionBase`1" />.</param>
        /// <returns><c>true</c> if <paramref name="item" /> was successfully removed from the <see cref="T:IX.Observable.ObservableCollectionBase`1" />; otherwise, <c>false</c>.
        /// This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:IX.Observable.ObservableCollectionBase`1" />.</returns>
        /// <remarks>On concurrent collections, this method is write-synchronized.</remarks>
        public override bool Remove(T item)
        {
            this.IncreaseIgnoreMustResetCounter();
            if (!base.Remove(item))
            {
                this.IncreaseIgnoreMustResetCounter(-1);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to remove an item from.</param>
        public override void RemoveAt(int index)
        {
            this.ThrowIfCurrentObjectDisposed();

            T item;

            using (ReadWriteSynchronizationLocker lockContext = this.ReadWriteLock())
            {
                if (index >= this.InternalContainer.Count)
                {
                    return;
                }

                lockContext.Upgrade();

                item = this.InternalContainer[index];
                this.IncreaseIgnoreMustResetCounter();
                this.InternalContainer.RemoveAt(index);

                this.PushUndoLevel(new RemoveUndoLevel<T> { Index = index, RemovedItem = item });
            }

            this.RaiseCollectionChangedRemove(item, index);
            this.RaisePropertyChanged(nameof(this.Count));
            this.ContentsMayHaveChanged();
        }

        /// <summary>
        /// Removes all items from the <see cref="ObservableMasterSlaveCollection{T}" />.
        /// </summary>
        /// <returns>An array containing the original collection items.</returns>
        /// <remarks>On concurrent collections, this method is write-synchronized.</remarks>
        protected override T[] ClearInternal()
        {
            this.ThrowIfCurrentObjectDisposed();

            T[] originalArray;

            using (this.WriteLock())
            {
                var container = (MultiListMasterSlaveListAdapter<T>)this.InternalContainer;

                this.IncreaseIgnoreMustResetCounter(container.SlavesCount + 1);

                originalArray = new T[container.MasterCount];
                container.MasterCopyTo(originalArray, 0);

                this.InternalContainer.Clear();

                this.PushUndoLevel(new ClearUndoLevel<T> { OriginalItems = originalArray });
            }

            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Count));
            this.ContentsMayHaveChanged();

            return originalArray;
        }

        /// <summary>
        /// Called when the contents may have changed so that proper notifications can happen.
        /// </summary>
        protected override void ContentsMayHaveChanged() => this.RaisePropertyChanged(Constants.ItemsName);
    }
}