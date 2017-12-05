// <copyright file="CollectionDebugView{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System;
using System.Diagnostics;

namespace IX.Observable.DebugAide
{
    internal sealed class CollectionDebugView<T>
    {
        private readonly ObservableCollectionBase<T> collection;

        public CollectionDebugView(ObservableCollectionBase<T> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                T[] items = new T[this.collection.InternalContainer.Count];
                this.collection.InternalContainer.CopyTo(items, 0);
                return items;
            }
        }
    }
}