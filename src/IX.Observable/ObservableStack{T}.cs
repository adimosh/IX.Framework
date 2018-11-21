// <copyright file="ObservableStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using IX.Observable.Adapters;
using IX.Observable.DebugAide;
using IX.Observable.UndoLevels;
using IX.System.Collections.Generic;
using IX.Undoable;

namespace IX.Observable
{
    /// <summary>
    /// A stack that broadcasts its changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    /// <remarks>
    /// <para>This class is not serializable. In order to serialize / deserialize content, please use the copying methods and serialize the result.</para>
    /// </remarks>
    [DebuggerDisplay("ObservableStack, Count = {Count}")]
    [DebuggerTypeProxy(typeof(StackDebugView<>))]
    public class ObservableStack<T> : ObservableCollectionBase<T>, IStack<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        public ObservableStack()
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the stack.</param>
        public ObservableStack(int capacity)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(capacity)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="collection">A collection of items to copy into the stack.</param>
        public ObservableStack(IEnumerable<T> collection)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(collection)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        public ObservableStack(SynchronizationContext context)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>()), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the stack.</param>
        public ObservableStack(SynchronizationContext context, int capacity)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(capacity)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="collection">A collection of items to copy into the stack.</param>
        public ObservableStack(SynchronizationContext context, IEnumerable<T> collection)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(collection)), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="suppressUndoable">If set to <see langword="true"/>, suppresses undoable capabilities of this collection.</param>
        public ObservableStack(bool suppressUndoable)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>()), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the stack.</param>
        /// <param name="suppressUndoable">If set to <see langword="true"/>, suppresses undoable capabilities of this collection.</param>
        public ObservableStack(int capacity, bool suppressUndoable)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(capacity)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="collection">A collection of items to copy into the stack.</param>
        /// <param name="suppressUndoable">If set to <see langword="true"/>, suppresses undoable capabilities of this collection.</param>
        public ObservableStack(IEnumerable<T> collection, bool suppressUndoable)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(collection)), suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="suppressUndoable">If set to <see langword="true"/>, suppresses undoable capabilities of this collection.</param>
        public ObservableStack(SynchronizationContext context, bool suppressUndoable)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>()), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the stack.</param>
        /// <param name="suppressUndoable">If set to <see langword="true"/>, suppresses undoable capabilities of this collection.</param>
        public ObservableStack(SynchronizationContext context, int capacity, bool suppressUndoable)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(capacity)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStack{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="collection">A collection of items to copy into the stack.</param>
        /// <param name="suppressUndoable">If set to <see langword="true"/>, suppresses undoable capabilities of this collection.</param>
        public ObservableStack(SynchronizationContext context, IEnumerable<T> collection, bool suppressUndoable)
            : base(new StackCollectionAdapter<T>(new System.Collections.Generic.Stack<T>(collection)), context, suppressUndoable)
        {
        }

        /// <summary>
        /// Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.ReadLock())
            {
                return ((StackCollectionAdapter<T>)this.InternalContainer).Peek();
            }
        }

        /// <summary>
        /// Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop()
        {
            this.ThrowIfCurrentObjectDisposed();

            T item;
            int index;

            using (this.WriteLock())
            {
                var container = (StackCollectionAdapter<T>)this.InternalContainer;
                item = container.Pop();
                index = container.Count;
            }

            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
            this.RaiseCollectionChangedRemove(item, index);

            return item;
        }

        /// <summary>
        /// Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item)
        {
            this.ThrowIfCurrentObjectDisposed();

            int index;

            using (this.WriteLock())
            {
                var container = (StackCollectionAdapter<T>)this.InternalContainer;
                container.Push(item);
                index = container.Count - 1;
            }

            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
            this.RaiseCollectionChangedAdd(item, index);
        }

        /// <summary>
        /// Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        public T[] ToArray()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.ReadLock())
            {
                return ((StackCollectionAdapter<T>)this.InternalContainer).ToArray();
            }
        }

        /// <summary>
        /// Sets the capacity to the actual number of elements in the stack if that number is less than 90 percent of current capacity.
        /// </summary>
        public void TrimExcess()
        {
            this.ThrowIfCurrentObjectDisposed();

            using (this.WriteLock())
            {
                ((StackCollectionAdapter<T>)this.InternalContainer).TrimExcess();
            }
        }

        /// <summary>
        /// Has the last undone operation redone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <param name="state">The state object to pass to the invocation.</param>
        /// <returns><see langword="true"/> if the redo was successful, <see langword="false"/> otherwise.</returns>
        protected override bool RedoInternally(StateChange undoRedoLevel, out Action<object> toInvokeOutsideLock, out object state)
        {
            if (base.UndoInternally(undoRedoLevel, out toInvokeOutsideLock, out state))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<T> aul:
                    {
                        var container = (StackCollectionAdapter<T>)this.InternalContainer;

                        container.Push(aul.AddedItem);

                        var index = aul.Index;
                        T item = aul.AddedItem;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableStack<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableStack<T>, T, int>(this, item, index);

                        break;
                    }

                case EnqueueUndoLevel<T> eul:
                    {
                        var container = (StackCollectionAdapter<T>)this.InternalContainer;

                        container.Push(eul.EnqueuedItem);

                        var index = container.Count - 1;
                        T item = eul.EnqueuedItem;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableStack<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableStack<T>, T, int>(this, item, index);

                        break;
                    }

                case DequeueUndoLevel<T> dul:
                    {
                        var container = (StackCollectionAdapter<T>)this.InternalContainer;

                        T item = container.Pop();
                        var index = container.Count;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableStack<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableStack<T>, T, int>(this, item, index);

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
                            var convertedState = innerState as ObservableStack<T>;

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
        /// Has the last operation undone.
        /// </summary>
        /// <param name="undoRedoLevel">A level of undo, with contents.</param>
        /// <param name="toInvokeOutsideLock">An action to invoke outside of the lock.</param>
        /// <param name="state">The state object to pass to the invocation.</param>
        /// <returns><see langword="true"/> if the undo was successful, <see langword="false"/> otherwise.</returns>
        protected override bool UndoInternally(StateChange undoRedoLevel, out Action<object> toInvokeOutsideLock, out object state)
        {
            if (base.RedoInternally(undoRedoLevel, out toInvokeOutsideLock, out state))
            {
                return true;
            }

            switch (undoRedoLevel)
            {
                case AddUndoLevel<T> aul:
                    {
                        var container = (StackCollectionAdapter<T>)this.InternalContainer;

                        T item = container.Pop();
                        var index = container.Count;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableStack<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableStack<T>, T, int>(this, item, index);

                        break;
                    }

                case EnqueueUndoLevel<T> eul:
                    {
                        var container = (StackCollectionAdapter<T>)this.InternalContainer;

                        T item = container.Pop();
                        var index = container.Count;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableStack<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedRemove(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableStack<T>, T, int>(this, item, index);

                        break;
                    }

                case DequeueUndoLevel<T> dul:
                    {
                        var container = (StackCollectionAdapter<T>)this.InternalContainer;

                        container.Push(dul.DequeuedItem);

                        var index = container.Count - 1;
                        T item = dul.DequeuedItem;
                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as Tuple<ObservableStack<T>, T, int>;

                            convertedState.Item1.RaisePropertyChanged(nameof(convertedState.Item1.Count));
                            convertedState.Item1.RaisePropertyChanged(Constants.ItemsName);
                            convertedState.Item1.RaiseCollectionChangedAdd(convertedState.Item2, convertedState.Item3);
                        };

                        state = new Tuple<ObservableStack<T>, T, int>(this, item, index);

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
                        var container = (StackCollectionAdapter<T>)this.InternalContainer;
                        for (var i = 0; i < cul.OriginalItems.Length - 1; i++)
                        {
                            container.Push(cul.OriginalItems[i]);
                        }

                        toInvokeOutsideLock = (innerState) =>
                        {
                            var convertedState = innerState as ObservableStack<T>;

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
        /// Interprets the block state changes outside the write lock.
        /// </summary>
        /// <param name="actions">The actions to employ.</param>
        /// <param name="states">The state objects to send to the corresponding actions.</param>
        protected override void InterpretBlockStateChangesOutsideLock(Action<object>[] actions, object[] states)
        {
            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
            this.RaiseCollectionReset();
        }
    }
}