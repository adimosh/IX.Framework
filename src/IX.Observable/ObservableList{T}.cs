// <copyright file="ObservableList{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using IX.Observable.Adapters;
using IX.Observable.DebugAide;

namespace IX.Observable
{
    /// <summary>
    /// An observable list.
    /// </summary>
    /// <typeparam name="T">The type of item in the list.</typeparam>
    /// <seealso cref="IX.Observable.ObservableListBase{T}" />
    [DebuggerDisplay("ObservableList, Count = {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView<>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "Observable{0}List", ItemName = "Item")]
    public class ObservableList<T> : ObservableListBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class.
        /// </summary>
        public ObservableList()
            : base(new ListListAdapter<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public ObservableList(IEnumerable<T> source)
            : base(new ListListAdapter<T>(source))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        public ObservableList(SynchronizationContext context)
            : base(new ListListAdapter<T>(), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="context">The context.</param>
        public ObservableList(IEnumerable<T> source, SynchronizationContext context)
            : base(new ListListAdapter<T>(source), context)
        {
        }
    }
}