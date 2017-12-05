// <copyright file="QueueDebugView{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System;
using System.Diagnostics;

namespace IX.Observable.DebugAide
{
    internal sealed class QueueDebugView<T>
    {
        private readonly ObservableQueue<T> queue;

        public QueueDebugView(ObservableQueue<T> queue)
        {
            this.queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                T[] items = new T[this.queue.InternalContainer.Count];
                this.queue.InternalContainer.CopyTo(items, 0);
                return items;
            }
        }
    }
}