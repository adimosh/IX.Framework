// <copyright file="FilterableObservableMasterSlaveCollection{TItem,TFilter}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using IX.Observable.DebugAide;

namespace IX.Observable
{
    /// <summary>
    /// An observable collection created from a master collection (to which updates go) and many slave, read-only collections, whose items can also be filtered.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TFilter">The type of the filter.</typeparam>
    /// <seealso cref="IX.Observable.ObservableMasterSlaveCollection{T}" />
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView<>))]
    public class FilterableObservableMasterSlaveCollection<TItem, TFilter> : ObservableMasterSlaveCollection<TItem>
    {
        private TFilter filter;
        private IList<TItem> filteredElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableObservableMasterSlaveCollection{TItem, TFilter}" /> class.
        /// </summary>
        /// <param name="filteringPredicate">The filtering predicate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filteringPredicate"/> is <see langword="null"/> (<see langword="Nothing"/>) in Visual Basic.</exception>
        public FilterableObservableMasterSlaveCollection(Func<TItem, TFilter, bool> filteringPredicate)
            : base()
        {
            this.FilteringPredicate = filteringPredicate ?? throw new ArgumentNullException(nameof(filteringPredicate));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableObservableMasterSlaveCollection{TItem, TFilter}"/> class.
        /// </summary>
        /// <param name="filteringPredicate">The filtering predicate.</param>
        /// <param name="context">The synchronization context to use, if any.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filteringPredicate"/> is <see langword="null"/> (<see langword="Nothing"/>) in Visual Basic.</exception>
        public FilterableObservableMasterSlaveCollection(Func<TItem, TFilter, bool> filteringPredicate, SynchronizationContext context)
            : base(context)
        {
            this.FilteringPredicate = filteringPredicate ?? throw new ArgumentNullException(nameof(filteringPredicate));
        }

        /// <summary>
        /// Gets the filtering predicate.
        /// </summary>
        /// <value>
        /// The filtering predicate.
        /// </value>
        public Func<TItem, TFilter, bool> FilteringPredicate { get; }

        /// <summary>
        /// Gets or sets the filter value.
        /// </summary>
        /// <value>
        /// The filter value.
        /// </value>
        public TFilter Filter
        {
            get => this.filter;
            set
            {
                this.filter = value;

                this.ClearCachedContents();

                this.RaiseCollectionReset();
                this.RaisePropertyChanged(nameof(this.Count));
                this.RaisePropertyChanged(Constants.ItemsName);
            }
        }

        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        /// <value>
        /// The item count.
        /// </value>
        public override int Count
        {
            get
            {
                if (this.IsFilter())
                {
                    if (this.filteredElements == null)
                    {
                        this.FillCachedList();
                    }

                    return this.filteredElements.Count;
                }
                else
                {
                    return base.Count;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public override IEnumerator<TItem> GetEnumerator()
        {
            if (this.IsFilter())
            {
                if (this.filteredElements == null)
                {
                    this.FillCachedList();
                }
                else
                {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Unavoidable at this point
                    return this.filteredElements.GetEnumerator();
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                }
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Unavoidable
            return base.GetEnumerator();
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Called when an item is added to a collection.
        /// </summary>
        /// <param name="addedItem">The added item.</param>
        /// <param name="index">The index.</param>
        protected override void RaiseCollectionChangedAdd(TItem addedItem, int index)
        {
            if (this.IsFilter())
            {
                this.RaiseCollectionReset();
            }
            else
            {
                base.RaiseCollectionChangedAdd(addedItem, index);
            }
        }

        /// <summary>
        /// Called when an item in a collection is changed.
        /// </summary>
        /// <param name="oldItem">The old item.</param>
        /// <param name="newItem">The new item.</param>
        /// <param name="index">The index.</param>
        protected override void RaiseCollectionChangedChanged(TItem oldItem, TItem newItem, int index)
        {
            if (this.IsFilter())
            {
                this.RaiseCollectionReset();
            }
            else
            {
                base.RaiseCollectionChangedChanged(oldItem, newItem, index);
            }
        }

        /// <summary>
        /// Called when an item is removed from a collection.
        /// </summary>
        /// <param name="removedItem">The removed item.</param>
        /// <param name="index">The index.</param>
        protected override void RaiseCollectionChangedRemove(TItem removedItem, int index)
        {
            if (this.IsFilter())
            {
                this.RaiseCollectionReset();
            }
            else
            {
                base.RaiseCollectionChangedRemove(removedItem, index);
            }
        }

        private IEnumerator<TItem> EnumerateFiltered()
        {
            TFilter filter = this.Filter;

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Unavoidable
            using (IEnumerator<TItem> enumerator = base.GetEnumerator())
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
            {
                while (enumerator.MoveNext())
                {
                    TItem current = enumerator.Current;
                    if (this.FilteringPredicate(current, filter))
                    {
                        yield return current;
                    }
                }
            }

            yield break;
        }

        private void FillCachedList()
        {
            this.filteredElements = new List<TItem>(base.Count);

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Unavoidable
            using (IEnumerator<TItem> enumerator = this.EnumerateFiltered())
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
            {
                while (enumerator.MoveNext())
                {
                    TItem current = enumerator.Current;
                    this.filteredElements.Add(current);
                }
            }
        }

        private void ClearCachedContents()
        {
            if (this.filteredElements != null)
            {
                IList<TItem> coll = this.filteredElements;
                this.filteredElements = null;
                coll.Clear();
            }
        }

        private bool IsFilter() => !EqualityComparer<TFilter>.Default.Equals(this.Filter, default);
    }
}