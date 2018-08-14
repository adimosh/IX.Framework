// <copyright file="ObservableQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using IX.Observable.Adapters;
using IX.Observable.DebugAide;
using IX.Observable.UndoLevels;
using IX.System.Collections.Generic;
using IX.Undoable;

namespace IX.Observable
{
    /// <summary>
    /// A queue that broadcasts its changes.
    /// </summary>
    /// <typeparam name="T">The type of items in the queue.</typeparam>
    [DebuggerDisplay("ObservableQueue, Count = {Count}")]
    [DebuggerTypeProxy(typeof(QueueDebugView<>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "Observable{0}Queue", ItemName = "Item")]
    public class ObservableQueue<T> : ObservableCollectionBase<T>, IQueue<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        public ObservableQueue()
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="collection">A collection of items to copy from.</param>
        public ObservableQueue(IEnumerable<T> collection)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(collection)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the queue.</param>
        public ObservableQueue(int capacity)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(capacity)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        public ObservableQueue(SynchronizationContext context)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>()), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="collection">A collection of items to copy from.</param>
        public ObservableQueue(SynchronizationContext context, IEnumerable<T> collection)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(collection)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the queue.</param>
        public ObservableQueue(SynchronizationContext context, int capacity)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(capacity)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableQueue(bool suppressUndoable)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>()), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="collection">A collection of items to copy from.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableQueue(IEnumerable<T> collection, bool suppressUndoable)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(collection)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the queue.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableQueue(int capacity, bool suppressUndoable)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(capacity)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableQueue(SynchronizationContext context, bool suppressUndoable)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>()), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="collection">A collection of items to copy from.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableQueue(SynchronizationContext context, IEnumerable<T> collection, bool suppressUndoable)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(collection)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the queue.</param>
        /// <param name="suppressUndoable">If set to <c>true</c>, suppresses undoable capabilities of this collection.</param>
        public ObservableQueue(SynchronizationContext context, int capacity, bool suppressUndoable)
            : base(new QueueCollectionAdapter<T>(new System.Collections.Generic.Queue<T>(capacity)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Dequeues and removes an item from the queue.
        /// </summary>
        /// <returns>The dequeued item.</returns>
        public T Dequeue()
        {
            this.ThrowIfCurrentObjectDisposed();

            T item;

            using (this.WriteLock())
            {
                item = ((QueueCollectionAdapter<T>)this.InternalContainer).Dequeue();
                this.PushUndoLevel(new DequeueUndoLevel<T> { DequeuedItem = item });
            }

            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
            this.RaiseCollectionChangedRemove(item, 0);

            return item;
        }

        /// <summary>
        /// Enqueues an item into the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item)
        {
            this.ThrowIfCurrentObjectDisposed();

            int newIndex;

            using (this.WriteLock())
            {
                var internalContainer = (QueueCollectionAdapter<T>)this.InternalContainer;
                internalContainer.Enqueue(item);
                newIndex = internalContainer.Count - 1;
                this.PushUndoLevel(new EnqueueUndoLevel<T> { EnqueuedItem = item });
            }

            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
            this.RaiseCollectionChangedAdd(item, newIndex);
        }

        /// <summary>
        /// Peeks at the topmost item in the queue without dequeuing it.
        /// </summary>
        /// <returns>The topmost item in the queue.</returns>
        public T Peek()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.ReadLock())
            {
                return ((QueueCollectionAdapter<T>)this.InternalContainer).Peek();
            }
        }

        /// <summary>
        /// Copies the items of the queue into a new array.
        /// </summary>
        /// <returns>An array of items that are contained in the queue.</returns>
        public T[] ToArray()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.ReadLock())
            {
                return ((QueueCollectionAdapter<T>)this.InternalContainer).ToArray();
            }
        }

        /// <summary>
        /// Sets the capacity to the actual number of elements in the <see cref="ObservableQueue{T}"/>, if that number is less than 90 percent of current capacity.
        /// </summary>
        public void TrimExcess()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.WriteLock())
            {
                ((QueueCollectionAdapter<T>)this.InternalContainer).TrimExcess();
            }
        }

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
                        var container = (QueueCollectionAdapter<T>)this.InternalContainer;
                        var array = new T[container.Count];
                        container.CopyTo(array, 0);
                        container.Clear();

                        for (var i = 0; i < array.Length - 1; i++)
                        {
                            container.Enqueue(array[i]);
                        }

                        var index = container.Count;
                        T item = array.Last();
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableQueue<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableQueue<T>, T, int>(this, item, index);

                        break;
                    }

                case EnqueueUndoLevel<T> eul:
                    {
                        var container = (QueueCollectionAdapter<T>)this.InternalContainer;
                        var array = new T[container.Count];
                        container.CopyTo(array, 0);
                        container.Clear();

                        for (var i = 0; i < array.Length - 1; i++)
                        {
                            container.Enqueue(array[i]);
                        }

                        var index = container.Count;
                        T item = array.Last();
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableQueue<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableQueue<T>, T, int>(this, item, index);

                        break;
                    }

                case DequeueUndoLevel<T> dul:
                    {
                        var container = (QueueCollectionAdapter<T>)this.InternalContainer;
                        container.Enqueue(dul.DequeuedItem);

                        var index = container.Count - 1;
                        T item = dul.DequeuedItem;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableQueue<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableQueue<T>, T, int>(this, item, index);

                        break;
                    }

                case RemoveUndoLevel<T> rul:
                    {
                        toInvokeOutsideLock = null;
                        state = null;
                        break;
                    }

                case ClearUndoLevel<T> cul:
                    {
                        var container = (QueueCollectionAdapter<T>)this.InternalContainer;
                        for (var i = 0; i < cul.OriginalItems.Length - 1; i++)
                        {
                            container.Enqueue(cul.OriginalItems[i]);
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as ObservableQueue<T>;

                            convertedState.RaisePropertyChanged(nameof(convertedState.Count));
                            convertedState.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.RaiseCollectionReset();
                        };

                        state = this;

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
                        var container = (QueueCollectionAdapter<T>)this.InternalContainer;

                        container.Enqueue(aul.AddedItem);

                        var index = container.Count - 1;
                        T item = aul.AddedItem;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableQueue<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableQueue<T>, T, int>(this, item, index);

                        break;
                    }

                case EnqueueUndoLevel<T> eul:
                    {
                        var container = (QueueCollectionAdapter<T>)this.InternalContainer;

                        container.Enqueue(eul.EnqueuedItem);

                        var index = container.Count - 1;
                        T item = eul.EnqueuedItem;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableQueue<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableQueue<T>, T, int>(this, item, index);

                        break;
                    }

                case DequeueUndoLevel<T> dul:
                    {
                        var container = (QueueCollectionAdapter<T>)this.InternalContainer;

                        container.Dequeue();

                        var index = 0;
                        T item = dul.DequeuedItem;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableQueue<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableQueue<T>, T, int>(this, item, index);

                        break;
                    }

                case RemoveUndoLevel<T> rul:
                    {
                        toInvokeOutsideLock = null;
                        state = null;
                        break;
                    }

                case ClearUndoLevel<T> cul:
                    {
                        this.InternalContainer.Clear();

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as ObservableQueue<T>;

                            convertedState.RaisePropertyChanged(nameof(convertedState.Count));
                            convertedState.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.RaiseCollectionReset();
                        };

                        state = this;

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