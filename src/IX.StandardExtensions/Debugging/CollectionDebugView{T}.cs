// <copyright file="CollectionDebugView{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IX.StandardExtensions.Debugging
{
    /// <summary>
    /// A debugging view for collections. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">The type of the item contained in the collection.</typeparam>
    [ComVisible(false)]
    public sealed class CollectionDebugView<T>
    {
        private readonly ICollection<T> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionDebugView{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Studio).</exception>
        public CollectionDebugView(ICollection<T> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var items = new T[this.collection.Count];
                this.collection.CopyTo(items, 0);
                return items;
            }
        }
    }
}