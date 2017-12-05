// <copyright file="ObservableCollectionBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using IX.Observable.Adapters;
using IX.Observable.UndoLevels;
using IX.StandardExtensions.Threading;
using IX.System.Collections.Generic;
using IX.Undoable;

namespace IX.Observable
{
    /// <summary>
    /// A base class for collections that are observable.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <seealso cref="global::System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="INotifyCollectionChanged" />
    /// <seealso cref="global::System.Collections.Generic.IEnumerable{T}" />
    public abstract class ObservableCollectionBase<T> : ObservableReadOnlyCollectionBase<T>, ICollection<T>, IUndoableItem
    {
        private object resetCountLocker;

        private PushDownStack<UndoRedoLevel> undoStack;
        private PushDownStack<UndoRedoLevel> redoStack;

        private bool isCapturedIntoUndoContext;
        private IUndoableItem parentUndoableContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container of items.</param>
        protected ObservableCollectionBase(CollectionAdapter<T> internalContainer)
            : base(internalContainer)
        {
            this.InternalContainer = internalContainer;
            this.resetCountLocker = new object();

            this.undoStack = new PushDownStack<UndoRedoLevel>(Constants.StandardUndoRedoLevels);
            this.redoStack = new PushDownStack<UndoRedoLevel>(Constants.StandardUndoRedoLevels);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container of items.</param>
        /// <param name="context">The synchronization context to use, if any.</param>
        protected ObservableCollectionBase(CollectionAdapter<T> internalContainer, SynchronizationContext context)
            : base(internalContainer, context)
        {
            this.InternalContainer = internalContainer;
            this.resetCountLocker = new object();

            this.undoStack = new PushDownStack<UndoRedoLevel>(Constants.StandardUndoRedoLevels);
            this.redoStack = new PushDownStack<UndoRedoLevel>(Constants.StandardUndoRedoLevels);
        }

        /// <summary>
        /// Gets or sets the number of levels to keep undo or redo information.
        /// </summary>
        /// <value>The history levels.</value>
        /// <remarks>
        /// <para>If this value is set, for example, to 7, then the implementing object should allow the <see cref="M:IX.Undoable.IUndoableItem.Undo" /> method
        /// to be called 7 times to change the state of the object. Upon calling it an 8th time, there should be no change in the
        /// state of the object.</para>
        /// <para>Any call beyond the limit imposed here should not fail, but it should also not change the state of the object.</para>
        /// <para>This member is not serialized, as it interferes with the undo/redo context, which cannot itself be serialized.</para>
        /// </remarks>
        public int HistoryLevels
        {
            get => this.undoStack.Limit;
            set
            {
                this.ThrowIfCurrentObjectDisposed();

                if (this.undoStack.Limit != value)
                {
                    this.undoStack.Limit = value;
                    this.redoStack.Limit = value;

                    this.RaisePropertyChanged(nameof(this.HistoryLevels));

                    this.RaisePropertyChanged(nameof(this.CanUndo));
                    this.RaisePropertyChanged(nameof(this.CanRedo));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is caught into an undo context.
        /// </summary>
        public bool IsCapturedIntoUndoContext => this.isCapturedIntoUndoContext;

        /// <summary>
        /// Gets a value indicating whether or not the implementer can perform an undo.
        /// </summary>
        /// <value><c>true</c> if the call to the <see cref="M:IX.Undoable.IUndoableItem.Undo" /> method would result in a state change, <c>false</c> otherwise.</value>
        public bool CanUndo => this.CheckDisposed(() => this.ReadLock(() => this.undoStack.Count > 0 || this.ParentUndoContext != null));

        /// <summary>
        /// Gets a value indicating whether or not the implementer can perform a redo.
        /// </summary>
        /// <value><c>true</c> if the call to the <see cref="M:IX.Undoable.IUndoableItem.Redo" /> method would result in a state change, <c>false</c> otherwise.</value>
        public bool CanRedo => this.CheckDisposed(() => this.ReadLock(() => this.redoStack.Count > 0 || this.ParentUndoContext != null));

        /// <summary>
        /// Gets the parent undo context, if any.
        /// </summary>
        /// <value>The parent undo context.</value>
        /// <remarks>
        /// <para>This member is not serialized, as it represents the undo/redo context, which cannot itself be serialized.</para>
        /// <para>The concept of the undo/redo context is incompatible with serialization. Any collection that is serialized will be free of any original context
        /// when deserialized.</para>
        /// </remarks>
        protected IUndoableItem ParentUndoContext => this.parentUndoableContext;

        /// <summary>
        /// Adds an item to the <see cref="ObservableCollectionBase{T}" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ObservableCollectionBase{T}" />.</param>
        /// <remarks>
        /// <para>On concurrent collections, this method is write-synchronized.</para>
        /// </remarks>
        public virtual void Add(T item)
        {
            this.ThrowIfCurrentObjectDisposed();

            int newIndex;
            using (this.WriteLock())
            {
                newIndex = this.InternalContainer.Add(item);
                this.PushUndoLevel(new AddUndoLevel<T> { AddedItem = item, Index = newIndex });
            }

            if (newIndex == -1)
            {
                this.RaiseCollectionReset();
            }
            else
            {
                this.RaiseCollectionChangedAdd(item, newIndex);
            }

            this.RaisePropertyChanged(nameof(this.Count));
            this.ContentsMayHaveChanged();
        }

        /// <summary>
        /// Removes all items from the <see cref="ObservableCollectionBase{T}" />.
        /// </summary>
        /// <remarks>
        /// <para>On concurrent collections, this method is write-synchronized.</para>
        /// </remarks>
        public virtual void Clear()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.WriteLock())
            {
                T[] tempArray = new T[this.InternalContainer.Count];
                this.InternalContainer.CopyTo(tempArray, 0);

                this.InternalContainer.Clear();

                this.PushUndoLevel(new ClearUndoLevel<T> { OriginalItems = tempArray });
            }

            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Count));
            this.ContentsMayHaveChanged();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ObservableCollectionBase{T}" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ObservableCollectionBase{T}" />.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="item" /> was successfully removed from the <see cref="ObservableCollectionBase{T}" />; otherwise, <c>false</c>.
        /// This method also returns false if <paramref name="item" /> is not found in the original <see cref="ObservableCollectionBase{T}" />.
        /// </returns>
        /// <remarks>
        /// <para>On concurrent collections, this method is write-synchronized.</para>
        /// </remarks>
        public virtual bool Remove(T item)
        {
            this.ThrowIfCurrentObjectDisposed();

            int oldIndex;
            using (this.WriteLock())
            {
                oldIndex = this.InternalContainer.Remove(item);
                this.PushUndoLevel(new RemoveUndoLevel<T> { RemovedItem = item, Index = oldIndex });
            }

            if (oldIndex >= 0)
            {
                this.RaiseCollectionChangedRemove(item, oldIndex);
                this.RaisePropertyChanged(nameof(this.Count));
                this.ContentsMayHaveChanged();

                return true;
            }
            else if (oldIndex < -1)
            {
                this.RaiseCollectionReset();
                this.RaisePropertyChanged(nameof(this.Count));
                this.ContentsMayHaveChanged();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Allows the implementer to be captured by a containing undo-/redo-capable object so that undo and redo operations
        /// can be coordinated across a larger scope.
        /// </summary>
        /// <param name="parent">The parent undo and redo context.</param>
        public void CaptureIntoUndoContext(IUndoableItem parent) => this.CheckDisposed(() => this.WriteLock(() =>
        {
            this.isCapturedIntoUndoContext = true;
            this.parentUndoableContext = parent ?? throw new ArgumentNullException(nameof(parent));
        }));

        /// <summary>
        /// Releases the implementer from being captured into an undo and redo context.
        /// </summary>
        public void ReleaseFromUndoContext() => this.CheckDisposed(() => this.WriteLock(() =>
        {
            this.isCapturedIntoUndoContext = false;
        }));

        /// <summary>
        /// Has the last operation performed on the implementing instance undone.
        /// </summary>
        /// <remarks><para>If the object is captured, the method will call the capturing parent's Undo method, which can bubble down to
        /// the last instance of an undo-/redo-capable object.</para>
        /// <para>If that is the case, the capturing object is solely responsible for ensuring that the inner state of the whole
        /// system is correct. Implementing classes should not expect this method to also handle state.</para>
        /// <para>If the object is released, it is expected that this method once again starts ensuring state when called.</para></remarks>
        public void Undo() => this.CheckDisposed(() =>
        {
            if (this.ParentUndoContext != null)
            {
                this.ParentUndoContext.Undo();
                return;
            }

            Action toInvoke;
            bool internalResult;
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.undoStack.Count == 0)
                {
                    return;
                }

                locker.Upgrade();

                UndoRedoLevel level = this.undoStack.Pop();
                internalResult = this.UndoInternally(level, out toInvoke);
                if (internalResult)
                {
                    this.redoStack.Push(level);
                }
            }

            if (internalResult)
            {
                toInvoke?.Invoke();
            }

            this.RaisePropertyChanged(nameof(this.CanUndo));
            this.RaisePropertyChanged(nameof(this.CanRedo));
        });

        /// <summary>
        /// Has the last undone operation performed on the implemented instance, presuming that it has not changed, redone.
        /// </summary>
        /// <remarks><para>If the object is captured, the method will call the capturing parent's Redo method, which can bubble down to
        /// the last instance of an undo-/redo-capable object.</para>
        /// <para>If that is the case, the capturing object is solely responsible for ensuring that the inner state of the whole
        /// system is correct. Implementing classes should not expect this method to also handle state.</para>
        /// <para>If the object is released, it is expected that this method once again starts ensuring state when called.</para></remarks>
        public void Redo() => this.CheckDisposed(() =>
        {
            if (this.ParentUndoContext != null)
            {
                this.ParentUndoContext.Redo();
                return;
            }

            Action toInvoke;
            bool internalResult;
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.redoStack.Count == 0)
                {
                    return;
                }

                locker.Upgrade();

                UndoRedoLevel level = this.redoStack.Pop();
                internalResult = this.RedoInternally(level, out toInvoke);
                if (internalResult)
                {
                    this.undoStack.Push(level);
                }
            }

            if (internalResult)
            {
                toInvoke?.Invoke();
            }

            this.RaisePropertyChanged(nameof(this.CanUndo));
            this.RaisePropertyChanged(nameof(this.CanRedo));
        });

        /// <summary>
        /// Has the state changes received undone from the object.
        /// </summary>
        /// <param name="stateChanges">The state changes to redo.</param>
        /// <exception cref="ItemNotCapturedIntoUndoContextException">There is no capturing context.</exception>
        public void UndoStateChanges(StateChange[] stateChanges)
        {
            this.ThrowIfCurrentObjectDisposed();

            if (!this.isCapturedIntoUndoContext)
            {
                throw new ItemNotCapturedIntoUndoContextException();
            }

            foreach (StateChange sc in stateChanges)
            {
                switch (sc)
                {
                    case SubItemStateChange sisc:
                        {
                            if (sisc.SubObject is IUndoableItem iu)
                            {
                                iu.UndoStateChanges(sisc.StateChanges);
                            }

                            break;
                        }

                    case UndoLevelStateChange ulsc:
                        {
                            foreach (UndoRedoLevel level in ulsc.Levels)
                            {
                                Action act;
                                bool internalResult;

                                using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
                                {
                                    internalResult = this.UndoInternally(level, out act);
                                }

                                if (internalResult)
                                {
                                    act?.Invoke();
                                }
                            }

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Has the state changes received redone into the object.
        /// </summary>
        /// <param name="stateChanges">The state changes to redo.</param>
        /// <exception cref="ItemNotCapturedIntoUndoContextException">There is no capturing context.</exception>
        public void RedoStateChanges(StateChange[] stateChanges)
        {
            this.ThrowIfCurrentObjectDisposed();

            if (!this.isCapturedIntoUndoContext)
            {
                throw new ItemNotCapturedIntoUndoContextException();
            }

            foreach (StateChange sc in stateChanges)
            {
                switch (sc)
                {
                    case SubItemStateChange sisc:
                        {
                            if (sisc.SubObject is IUndoableItem iu)
                            {
                                iu.RedoStateChanges(sisc.StateChanges);
                            }

                            break;
                        }

                    case UndoLevelStateChange ulsc:
                        {
                            foreach (UndoRedoLevel level in ulsc.Levels)
                            {
                                Action act;
                                bool internalResult;

                                using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
                                {
                                    internalResult = this.RedoInternally(level, out act);
                                }

                                if (internalResult)
                                {
                                    act?.Invoke();
                                }
                            }

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Has the last operation undone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <returns><c>true</c> if the undo was successful, <c>false</c> otherwise.</returns>
        protected virtual bool UndoInternally(UndoRedoLevel undoRedoLevel, out Action toInvokeOutsideLock)
        {
            if (undoRedoLevel is ItemChangeUndoLevel)
            {
                var lvl = undoRedoLevel as ItemChangeUndoLevel;

                lvl.Instance.UndoStateChanges(lvl.StateChanges);

                toInvokeOutsideLock = null;

                return true;
            }
            else
            {
                toInvokeOutsideLock = null;

                return false;
            }
        }

        /// <summary>
        /// Has the last undone operation redone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <returns><c>true</c> if the redo was successful, <c>false</c> otherwise.</returns>
        protected virtual bool RedoInternally(UndoRedoLevel undoRedoLevel, out Action toInvokeOutsideLock)
        {
            if (undoRedoLevel is ItemChangeUndoLevel)
            {
                var lvl = undoRedoLevel as ItemChangeUndoLevel;

                lvl.Instance.UndoStateChanges(lvl.StateChanges);

                toInvokeOutsideLock = null;

                return true;
            }
            else
            {
                toInvokeOutsideLock = null;

                return false;
            }
        }

        /// <summary>
        /// Push an undo level into the stack.
        /// </summary>
        /// <param name="undoRedoLevel">The undo level to push.</param>
        protected void PushUndoLevel(UndoRedoLevel undoRedoLevel)
        {
            this.undoStack.Push(undoRedoLevel);
            this.redoStack.Clear();

            this.RaisePropertyChanged(nameof(this.CanUndo));
            this.RaisePropertyChanged(nameof(this.CanRedo));
        }

        /// <summary>
        /// Called when an item is added to a collection.
        /// </summary>
        /// <param name="addedItem">The added item.</param>
        /// <param name="index">The index.</param>
        protected virtual void RaiseCollectionChangedAdd(T addedItem, int index)
            => this.RaiseCollectionAdd(index, addedItem);

        /// <summary>
        /// Called when an item in a collection is changed.
        /// </summary>
        /// <param name="oldItem">The old item.</param>
        /// <param name="newItem">The new item.</param>
        /// <param name="index">The index.</param>
        protected virtual void RaiseCollectionChangedChanged(T oldItem, T newItem, int index)
            => this.RaiseCollectionReplace(index, oldItem, newItem);

        /// <summary>
        /// Called when an item is removed from a collection.
        /// </summary>
        /// <param name="removedItem">The removed item.</param>
        /// <param name="index">The index.</param>
        protected virtual void RaiseCollectionChangedRemove(T removedItem, int index)
            => this.RaiseCollectionRemove(index, removedItem);
    }
}