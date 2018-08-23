// <copyright file="ObservableCollectionBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading;
using IX.Guaranteed;
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
    public abstract class ObservableCollectionBase<T> : ObservableReadOnlyCollectionBase<T>, ICollection<T>, IUndoableItem, IEditCommittableItem
    {
        private PushDownStack<StateChange> undoStack = new PushDownStack<StateChange>(EnvironmentSettings.DisableUndoable ? 0 : EnvironmentSettings.DefaultUndoRedoLevels);
        private PushDownStack<StateChange> redoStack = new PushDownStack<StateChange>(EnvironmentSettings.DisableUndoable ? 0 : EnvironmentSettings.DefaultUndoRedoLevels);

        private bool suppressUndoable;
        private bool automaticallyCaptureSubItems;
        private UndoableUnitBlockTransaction<T> currentUndoBlockTransaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container of items.</param>
        protected ObservableCollectionBase(ICollectionAdapter<T> internalContainer)
            : base(internalContainer)
        {
            this.InitializeInternalState(internalContainer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container of items.</param>
        /// <param name="context">The synchronization context to use, if any.</param>
        protected ObservableCollectionBase(ICollectionAdapter<T> internalContainer, SynchronizationContext context)
            : base(internalContainer, context)
        {
            this.InitializeInternalState(internalContainer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionBase{T}" /> class.
        /// </summary>
        /// <param name="internalContainer">The internal container of items.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        protected ObservableCollectionBase(ICollectionAdapter<T> internalContainer, bool suppressUndoable)
            : base(internalContainer)
        {
            this.InitializeInternalState(internalContainer, suppressUndoable);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionBase{T}"/> class.
        /// </summary>
        /// <param name="internalContainer">The internal container of items.</param>
        /// <param name="context">The synchronization context to use, if any.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        protected ObservableCollectionBase(ICollectionAdapter<T> internalContainer, SynchronizationContext context, bool suppressUndoable)
            : base(internalContainer, context)
        {
            this.InitializeInternalState(internalContainer, suppressUndoable);
        }

        /// <summary>
        /// Occurs when an edit is committed to the collection, whichever that may be.
        /// </summary>
        protected event EventHandler<EditCommittedEventArgs> EditCommittedInternal;

        /// <summary>
        /// Occurs when an edit on this item is committed.
        /// </summary>
        event EventHandler<EditCommittedEventArgs> IEditCommittableItem.EditCommitted
        {
            add { this.EditCommittedInternal += value; }
            remove { this.EditCommittedInternal -= value; }
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
        public bool IsCapturedIntoUndoContext => this.ParentUndoContext != null;

        /// <summary>
        /// Gets a value indicating whether or not the implementer can perform an undo.
        /// </summary>
        /// <value><c>true</c> if the call to the <see cref="M:IX.Undoable.IUndoableItem.Undo" /> method would result in a state change, <c>false</c> otherwise.</value>
        public bool CanUndo
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                return this.ParentUndoContext?.CanUndo ?? this.ReadLock((c2This) => c2This.undoStack.Count > 0, this);
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the implementer can perform a redo.
        /// </summary>
        /// <value><c>true</c> if the call to the <see cref="M:IX.Undoable.IUndoableItem.Redo" /> method would result in a state change, <c>false</c> otherwise.</value>
        public bool CanRedo
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                return this.ParentUndoContext?.CanRedo ?? this.ReadLock((c2This) => c2This.redoStack.Count > 0, this);
            }
        }

        /// <summary>
        /// Gets the parent undo context, if any.
        /// </summary>
        /// <value>The parent undo context.</value>
        /// <remarks>
        /// <para>This member is not serialized, as it represents the undo/redo context, which cannot itself be serialized.</para>
        /// <para>The concept of the undo/redo context is incompatible with serialization. Any collection that is serialized will be free of any original context
        /// when deserialized.</para>
        /// </remarks>
        public IUndoableItem ParentUndoContext { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether to automatically capture sub items in the current undo/redo context.
        /// </summary>
        /// <value><c>true</c> to automatically capture sub items; otherwise, <c>false</c>.</value>
        public bool AutomaticallyCaptureSubItems
        {
            get => this.automaticallyCaptureSubItems;

            set
            {
                this.automaticallyCaptureSubItems = value;

                if (this.ItemsAreUndoable)
                {
                    if (value)
                    {
                        using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
                        {
                            if (((ICollection<T>)this.InternalContainer).Count > 0)
                            {
                                locker.Upgrade();

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
                                foreach (IUndoableItem item in this.InternalContainer.Cast<IUndoableItem>())
                                {
                                    item.CaptureIntoUndoContext(this);
                                }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                            }
                        }
                    }
                    else
                    {
                        using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
                        {
                            if (((ICollection<T>)this.InternalContainer).Count > 0)
                            {
                                locker.Upgrade();

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
                                foreach (IUndoableItem item in this.InternalContainer.Cast<IUndoableItem>())
                                {
                                    item.ReleaseFromUndoContext();
                                }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether items are undoable.
        /// </summary>
        /// <value><c>true</c> if items are undoable; otherwise, <c>false</c>.</value>
        public bool ItemsAreUndoable { get; } = typeof(IUndoableItem).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo());

        /// <summary>
        /// Gets a value indicating whether items are key/value pairs.
        /// </summary>
        /// <value><c>true</c> if items are key/value pairs; otherwise, <c>false</c>.</value>
        public bool ItemsAreKeyValuePairs { get; }

        /// <summary>
        /// Starts the undoable operations on this object.
        /// </summary>
        /// <remarks>
        /// <para>If undoable operations were suppressed, no undo levels will accumulate before calling this method.</para>
        /// </remarks>
        public void StartUndo() => this.suppressUndoable = false;

        /// <summary>
        /// Adds an item to the <see cref="ObservableCollectionBase{T}" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ObservableCollectionBase{T}" />.</param>
        /// <remarks>
        /// <para>On concurrent collections, this method is write-synchronized.</para>
        /// </remarks>
        public virtual void Add(T item)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            int newIndex;

            // Under write lock
            using (this.WriteLock())
            {
                // Using an undo/redo transaction lock
                using (OperationTransaction tc = this.CheckItemAutoCapture(item))
                {
                    // Add the item
                    newIndex = this.InternalContainer.Add(item);

                    // Push the undo level
                    this.PushUndoLevel(new AddUndoLevel<T> { AddedItem = item, Index = newIndex });

                    // Mark the transaction as a success
                    tc.Success();
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
                this.RaiseCollectionChangedAdd(item, newIndex);
            }

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();
        }

        /// <summary>
        /// Removes all items from the <see cref="ObservableCollectionBase{T}" />.
        /// </summary>
        /// <remarks>
        /// <para>On concurrent collections, this method is write-synchronized.</para>
        /// </remarks>
        public void Clear() => this.ClearInternal();

        /// <summary>
        /// Removes all items from the <see cref="ObservableCollectionBase{T}" /> and returns them as an array.
        /// </summary>
        /// <returns>An array containing the original collection items.</returns>
        /// <remarks>
        /// <para>On concurrent collections, this method is write-synchronized.</para>
        /// </remarks>
        public T[] ClearAndPersist() => this.ClearInternal();

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
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            int oldIndex;

            // Under write lock
            using (this.WriteLock())
            {
                // Inside an undo/redo transaction
                using (OperationTransaction tc = this.CheckItemAutoRelease(item))
                {
                    // Remove the item
                    oldIndex = this.InternalContainer.Remove(item);

                    // Push an undo level
                    this.PushUndoLevel(new RemoveUndoLevel<T> { RemovedItem = item, Index = oldIndex });

                    // Mark the transaction as a success
                    tc.Success();
                }
            }

            // NOTIFICATIONS AND RETURN

            // Collection changed
            if (oldIndex >= 0)
            {
                // Collection changed with a specific index
                this.RaiseCollectionChangedRemove(item, oldIndex);

                // Property changed
                this.RaisePropertyChanged(nameof(this.Count));

                // Contents may have changed
                this.ContentsMayHaveChanged();

                return true;
            }
            else if (oldIndex < -1)
            {
                // Collection changed with no specific index (Dictionary remove)
                this.RaiseCollectionReset();

                // Property changed
                this.RaisePropertyChanged(nameof(this.Count));

                // Contents may have changed
                this.ContentsMayHaveChanged();

                return true;
            }
            else
            {
                // Unsuccessful removal
                return false;
            }
        }

        /// <summary>
        /// Allows the implementer to be captured by a containing undo-/redo-capable object so that undo and redo operations
        /// can be coordinated across a larger scope.
        /// </summary>
        /// <param name="parent">The parent undo and redo context.</param>
        public void CaptureIntoUndoContext(IUndoableItem parent) => this.CaptureIntoUndoContext(parent, false);

        /// <summary>
        /// Allows the implementer to be captured by a containing undo-/redo-capable object so that undo and redo operations
        /// can be coordinated across a larger scope.
        /// </summary>
        /// <param name="parent">The parent undo and redo context.</param>
        /// <param name="automaticallyCaptureSubItems">if set to <c>true</c>, the collection automatically captures sub-items into its undo/redo context.</param>
        public void CaptureIntoUndoContext(IUndoableItem parent, bool automaticallyCaptureSubItems)
        {
            this.ThrowIfCurrentObjectDisposed();

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            using (this.WriteLock())
            {
                this.AutomaticallyCaptureSubItems = automaticallyCaptureSubItems;
                this.ParentUndoContext = parent;
            }
        }

        /// <summary>
        /// Releases the implementer from being captured into an undo and redo context.
        /// </summary>
        public void ReleaseFromUndoContext()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.WriteLock())
            {
                this.AutomaticallyCaptureSubItems = false;
                this.ParentUndoContext = null;
            }
        }

        /// <summary>
        /// Has the last operation performed on the implementing instance undone.
        /// </summary>
        /// <remarks><para>If the object is captured, the method will call the capturing parent's Undo method, which can bubble down to
        /// the last instance of an undo-/redo-capable object.</para>
        /// <para>If that is the case, the capturing object is solely responsible for ensuring that the inner state of the whole
        /// system is correct. Implementing classes should not expect this method to also handle state.</para>
        /// <para>If the object is released, it is expected that this method once again starts ensuring state when called.</para></remarks>
        public void Undo()
        {
            this.ThrowIfCurrentObjectDisposed();

            if (this.ParentUndoContext != null)
            {
                this.ParentUndoContext.Undo();
                return;
            }

            if (this.currentUndoBlockTransaction != null)
            {
                throw new InvalidOperationException(Resources.UndoAndRedoOperationsAreNotSupportedWhileAnExplicitTransactionBlockIsOpen);
            }

            Action<object> toInvoke;
            object state;
            bool internalResult;
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.undoStack.Count == 0)
                {
                    return;
                }

                locker.Upgrade();

                StateChange level = this.undoStack.Pop();
                internalResult = this.UndoInternally(level, out toInvoke, out state);
                if (internalResult)
                {
                    this.redoStack.Push(level);
                }
            }

            if (internalResult)
            {
                toInvoke?.Invoke(state);
            }

            this.RaisePropertyChanged(nameof(this.CanUndo));
            this.RaisePropertyChanged(nameof(this.CanRedo));
        }

        /// <summary>
        /// Has the last undone operation performed on the implemented instance, presuming that it has not changed, redone.
        /// </summary>
        /// <remarks><para>If the object is captured, the method will call the capturing parent's Redo method, which can bubble down to
        /// the last instance of an undo-/redo-capable object.</para>
        /// <para>If that is the case, the capturing object is solely responsible for ensuring that the inner state of the whole
        /// system is correct. Implementing classes should not expect this method to also handle state.</para>
        /// <para>If the object is released, it is expected that this method once again starts ensuring state when called.</para></remarks>
        public void Redo()
        {
            this.ThrowIfCurrentObjectDisposed();

            if (this.ParentUndoContext != null)
            {
                this.ParentUndoContext.Redo();
                return;
            }

            if (this.currentUndoBlockTransaction != null)
            {
                throw new InvalidOperationException(Resources.UndoAndRedoOperationsAreNotSupportedWhileAnExplicitTransactionBlockIsOpen);
            }

            Action<object> toInvoke;
            object state;
            bool internalResult;
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.redoStack.Count == 0)
                {
                    return;
                }

                locker.Upgrade();

                StateChange level = this.redoStack.Pop();
                internalResult = this.RedoInternally(level, out toInvoke, out state);
                if (internalResult)
                {
                    this.undoStack.Push(level);
                }
            }

            if (internalResult)
            {
                toInvoke?.Invoke(state);
            }

            this.RaisePropertyChanged(nameof(this.CanUndo));
            this.RaisePropertyChanged(nameof(this.CanRedo));
        }

        /// <summary>
        /// Has the state changes received undone from the object.
        /// </summary>
        /// <param name="stateChanges">The state changes to redo.</param>
        /// <exception cref="ItemNotCapturedIntoUndoContextException">There is no capturing context.</exception>
        public void UndoStateChanges(StateChange[] stateChanges)
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // Current object captured in an undo/redo context
            if (!this.IsCapturedIntoUndoContext)
            {
                throw new ItemNotCapturedIntoUndoContextException();
            }

            // ACTION
            foreach (StateChange sc in stateChanges)
            {
                switch (sc)
                {
                    case SubItemStateChange sisc:
                        {
                            sisc.SubObject.UndoStateChanges(sisc.StateChanges);

                            break;
                        }

                    case BlockStateChange bsc:
                        {
                            Action<object> act;
                            object state;
                            bool internalResult;

                            foreach (StateChange ulsc in bsc.StateChanges)
                            {
                                using (this.WriteLock())
                                {
                                    internalResult = this.UndoInternally(ulsc, out act, out state);
                                }

                                if (internalResult)
                                {
                                    act?.Invoke(state);
                                }
                            }

                            break;
                        }

                    case StateChange ulsc:
                        {
                            Action<object> act;
                            object state;
                            bool internalResult;

                            using (this.WriteLock())
                            {
                                internalResult = this.UndoInternally(ulsc, out act, out state);
                            }

                            if (internalResult)
                            {
                                act?.Invoke(state);
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
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // Current object captured in an undo/redo context
            if (!this.IsCapturedIntoUndoContext)
            {
                throw new ItemNotCapturedIntoUndoContextException();
            }

            // ACTION
            foreach (StateChange sc in stateChanges)
            {
                switch (sc)
                {
                    case SubItemStateChange sisc:
                        {
                            sisc.SubObject.RedoStateChanges(sisc.StateChanges);

                            break;
                        }

                    case BlockStateChange blsc:
                        {
                            Action<object> act;
                            object state;
                            bool internalResult;

                            foreach (StateChange ulsc in blsc.StateChanges)
                            {
                                using (this.WriteLock())
                                {
                                    internalResult = this.RedoInternally(ulsc, out act, out state);
                                }

                                if (internalResult)
                                {
                                    act?.Invoke(state);
                                }
                            }

                            break;
                        }

                    case StateChange ulsc:
                        {
                            Action<object> act;
                            object state;
                            bool internalResult;

                            using (this.WriteLock())
                            {
                                internalResult = this.RedoInternally(ulsc, out act, out state);
                            }

                            if (internalResult)
                            {
                                act?.Invoke(state);
                            }

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Starts an explicit undo block transaction.
        /// </summary>
        /// <returns>OperationTransaction.</returns>
        public OperationTransaction StartExplicitUndoBlockTransaction()
        {
            if (this.IsCapturedIntoUndoContext)
            {
                throw new InvalidOperationException(Resources.TheCollectionIsCapturedIntoAContextItCannotStartAnExplicitTransaction);
            }

            if (this.currentUndoBlockTransaction != null)
            {
                throw new InvalidOperationException(Resources.ThereAlreadyIsAnOpenUndoTransaction);
            }

            var transaction = new UndoableUnitBlockTransaction<T>(this);

            Interlocked.Exchange(ref this.currentUndoBlockTransaction, transaction);

            return transaction;
        }

        /// <summary>
        /// Finishes the explicit undo block transaction.
        /// </summary>
        internal void FinishExplicitUndoBlockTransaction()
        {
            this.undoStack.Push(this.currentUndoBlockTransaction.StateChanges);

            Interlocked.Exchange(ref this.currentUndoBlockTransaction, null);

            this.redoStack.Clear();

            this.RaisePropertyChanged(nameof(this.CanUndo));
            this.RaisePropertyChanged(nameof(this.CanRedo));
        }

        /// <summary>
        /// Fails the explicit undo block transaction.
        /// </summary>
        internal void FailExplicitUndoBlockTransaction() => Interlocked.Exchange(ref this.currentUndoBlockTransaction, null);

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            base.DisposeManagedContext();

            Interlocked.Exchange(ref this.undoStack, null)?.Dispose();
            Interlocked.Exchange(ref this.redoStack, null)?.Dispose();
        }

        /// <summary>
        /// Has the last operation undone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <param name="state">The state object to pass to the invocation.</param>
        /// <returns><c>true</c> if the undo was successful, <c>false</c> otherwise.</returns>
        protected virtual bool UndoInternally(StateChange undoRedoLevel, out Action<object> toInvokeOutsideLock, out object state)
        {
            if (undoRedoLevel is SubItemStateChange lvl)
            {
                lvl.SubObject.UndoStateChanges(lvl.StateChanges);

                toInvokeOutsideLock = null;
                state = null;

                return true;
            }
            else if (undoRedoLevel is BlockStateChange bsc)
            {
                var count = bsc.StateChanges.Count;
                if (count == 0)
                {
                    toInvokeOutsideLock = null;
                    state = null;

                    return true;
                }

                var actionsToInvoke = new Action<object>[count];
                var states = new object[count];
                var counter = 0;
                var result = true;
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - OK, we're doing a Reverse anyway
                foreach (StateChange sc in ((IEnumerable<StateChange>)bsc.StateChanges).Reverse())
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                {
                    try
                    {
                        var localResult = this.UndoInternally(sc, out Action<object> toInvoke, out var toState);

                        if (!localResult)
                        {
                            result = false;
                        }
                        else
                        {
                            actionsToInvoke[counter] = toInvoke;
                            states[counter] = toState;
                        }
                    }
                    finally
                    {
                        counter++;
                    }
                }

                state = new Tuple<Action<object>[], object[], ObservableCollectionBase<T>>(actionsToInvoke, states, this);
                toInvokeOutsideLock = (innerState) =>
                {
                    var convertedState = (Tuple<Action<object>[], object[], ObservableCollectionBase<T>>)innerState;

                    convertedState.Item3.InterpretBlockStateChangesOutsideLock(convertedState.Item1, convertedState.Item2);
                };

                return result;
            }
            else
            {
                toInvokeOutsideLock = null;
                state = null;

                return false;
            }
        }

        /// <summary>
        /// Has the last undone operation redone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <param name="state">The state object to pass to the invocation.</param>
        /// <returns><c>true</c> if the redo was successful, <c>false</c> otherwise.</returns>
        protected virtual bool RedoInternally(StateChange undoRedoLevel, out Action<object> toInvokeOutsideLock, out object state)
        {
            if (undoRedoLevel is SubItemStateChange lvl)
            {
                lvl.SubObject.RedoStateChanges(lvl.StateChanges);

                toInvokeOutsideLock = null;
                state = null;

                return true;
            }
            else if (undoRedoLevel is BlockStateChange bsc)
            {
                var count = bsc.StateChanges.Count;
                if (count == 0)
                {
                    toInvokeOutsideLock = null;
                    state = null;

                    return true;
                }

                var actionsToInvoke = new Action<object>[count];
                var states = new object[count];
                var counter = 0;
                var result = true;
                foreach (StateChange sc in bsc.StateChanges)
                {
                    try
                    {
                        var localResult = this.RedoInternally(sc, out Action<object> toInvoke, out var toState);

                        if (!localResult)
                        {
                            result = false;
                        }
                        else
                        {
                            actionsToInvoke[counter] = toInvoke;
                            states[counter] = toState;
                        }
                    }
                    finally
                    {
                        counter++;
                    }
                }

                state = new Tuple<Action<object>[], object[], ObservableCollectionBase<T>>(actionsToInvoke, states, this);
                toInvokeOutsideLock = (innerState) =>
                {
                    var convertedState = (Tuple<Action<object>[], object[], ObservableCollectionBase<T>>)innerState;

                    convertedState.Item3.InterpretBlockStateChangesOutsideLock(convertedState.Item1, convertedState.Item2);
                };

                return result;
            }
            else
            {
                toInvokeOutsideLock = null;
                state = null;

                return false;
            }
        }

        /// <summary>
        /// Interprets the block state changes outside the write lock.
        /// </summary>
        /// <param name="actions">The actions to employ.</param>
        /// <param name="states">The state objects to send to the corresponding actions.</param>
        protected virtual void InterpretBlockStateChangesOutsideLock(Action<object>[] actions, object[] states)
        {
            for (var i = 0; i < actions.Length; i++)
            {
                actions[i]?.Invoke(states[i]);
            }
        }

        /// <summary>
        /// Push an undo level into the stack.
        /// </summary>
        /// <param name="undoRedoLevel">The undo level to push.</param>
        protected void PushUndoLevel(StateChange undoRedoLevel)
        {
            if (this.suppressUndoable || EnvironmentSettings.DisableUndoable)
            {
                return;
            }

            if (this.IsCapturedIntoUndoContext)
            {
                this.EditCommittedInternal?.Invoke(this, new EditCommittedEventArgs(undoRedoLevel));

                this.redoStack.Clear();

                this.RaisePropertyChanged(nameof(this.CanUndo));
                this.RaisePropertyChanged(nameof(this.CanRedo));
            }
            else if (this.currentUndoBlockTransaction == null)
            {
                this.undoStack.Push(undoRedoLevel);

                this.redoStack.Clear();

                this.RaisePropertyChanged(nameof(this.CanUndo));
                this.RaisePropertyChanged(nameof(this.CanRedo));
            }
            else
            {
                this.currentUndoBlockTransaction.StateChanges.StateChanges.Add(undoRedoLevel);
            }
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

        /// <summary>
        /// Checks and automatically captures an item in a capturing transaction.
        /// </summary>
        /// <param name="item">The item to capture.</param>
        /// <returns>An auto-capture transaction context that reverts the capture if things go wrong.</returns>
        protected virtual OperationTransaction CheckItemAutoCapture(T item)
        {
            if (this.AutomaticallyCaptureSubItems && this.ItemsAreUndoable)
            {
                if (item is IUndoableItem ui)
                {
                    return new AutoCaptureTransactionContext(ui, this, this.Tei_EditCommitted);
                }
            }

            return new AutoCaptureTransactionContext();
        }

        /// <summary>
        /// Checks and automatically captures items in a capturing transaction.
        /// </summary>
        /// <param name="items">The items to capture.</param>
        /// <returns>An auto-capture transaction context that reverts the capture if things go wrong.</returns>
        protected virtual OperationTransaction CheckItemAutoCapture(IEnumerable<T> items)
        {
            if (this.AutomaticallyCaptureSubItems && this.ItemsAreUndoable)
            {
                return new AutoCaptureTransactionContext(items.Cast<IUndoableItem>(), this, this.Tei_EditCommitted);
            }

            return new AutoCaptureTransactionContext();
        }

        /// <summary>
        /// Checks and automatically captures an item in a capturing transaction.
        /// </summary>
        /// <param name="item">The item to capture.</param>
        /// <returns>An auto-capture transaction context that reverts the capture if things go wrong.</returns>
        protected virtual OperationTransaction CheckItemAutoRelease(T item)
        {
            if (this.AutomaticallyCaptureSubItems && this.ItemsAreUndoable)
            {
                if (item is IUndoableItem ui)
                {
                    return new AutoReleaseTransactionContext(ui, this, this.Tei_EditCommitted);
                }
            }

            return new AutoReleaseTransactionContext();
        }

        /// <summary>
        /// Checks and automatically captures items in a capturing transaction.
        /// </summary>
        /// <param name="items">The items to capture.</param>
        /// <returns>An auto-capture transaction context that reverts the capture if things go wrong.</returns>
        protected virtual OperationTransaction CheckItemAutoRelease(IEnumerable<T> items)
        {
            if (this.AutomaticallyCaptureSubItems && this.ItemsAreUndoable)
            {
                return new AutoReleaseTransactionContext(items.Cast<IUndoableItem>(), this, this.Tei_EditCommitted);
            }

            return new AutoReleaseTransactionContext();
        }

        /// <summary>
        /// Removes all items from the <see cref="ObservableCollectionBase{T}" /> and returns them as an array.
        /// </summary>
        /// <returns>An array containing the original collection items.</returns>
        protected virtual T[] ClearInternal()
        {
            // PRECONDITIONS

            // Current object not disposed
            this.ThrowIfCurrentObjectDisposed();

            // ACTION
            T[] tempArray;

            // Under write lock
            using (this.WriteLock())
            {
                // Save existing items
                tempArray = new T[((ICollection<T>)this.InternalContainer).Count];
                this.InternalContainer.CopyTo(tempArray, 0);

                // Into an undo/redo transaction context
                using (OperationTransaction tc = this.CheckItemAutoRelease(tempArray))
                {
                    // Do the actual clearing
                    this.InternalContainer.Clear();

                    // Push an undo level
                    this.PushUndoLevel(new ClearUndoLevel<T> { OriginalItems = tempArray });

                    // Mark the transaction as a success
                    tc.Success();
                }
            }

            // NOTIFICATIONS

            // Collection changed
            this.RaiseCollectionReset();

            // Property changed
            this.RaisePropertyChanged(nameof(this.Count));

            // Contents may have changed
            this.ContentsMayHaveChanged();

            return tempArray;
        }

        private void Tei_EditCommitted(object sender, EditCommittedEventArgs e) => this.PushUndoLevel(new SubItemStateChange { SubObject = sender as IUndoableItem, StateChanges = e.StateChanges });

        private void InitializeInternalState(ICollectionAdapter<T> internalContainer, bool? suppressUndoable = null)
        {
            this.InternalContainer = internalContainer;

            this.suppressUndoable = suppressUndoable ?? EnvironmentSettings.AlwaysSuppressUndoLevelsByDefault;
        }
    }
}