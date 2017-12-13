// <copyright file="MultiListListAdapter{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace IX.Observable.Adapters
{
    internal class MultiListListAdapter<T> : ListAdapter<T>
    {
        private List<IEnumerable<T>> lists;

        internal MultiListListAdapter()
        {
            this.lists = new List<IEnumerable<T>>();
        }

        public override int Count => this.lists.Sum(p => p.Count());

        public int SlavesCount => this.lists.Count;

        public override bool IsReadOnly => true;

        public override T this[int index]
        {
            get
            {
                var idx = index;

                foreach (IEnumerable<T> list in this.lists)
                {
                    if (list.Count() <= idx)
                    {
                        idx -= list.Count();
                        continue;
                    }

                    return list.ElementAt(idx);
                }

                return default;
            }

            set => throw new InvalidOperationException();
        }

        public override int Add(T item) => throw new InvalidOperationException();

        public override int AddRange(IEnumerable<T> items) => throw new InvalidOperationException();

        public override void Clear() => throw new InvalidOperationException();

        public override bool Contains(T item) => this.lists.Any(p => p.Contains(item));

        public override void CopyTo(T[] array, int arrayIndex)
        {
            var totalCount = this.Count + arrayIndex;
            IEnumerator<T> enumerator = this.GetEnumerator();

            for (var i = arrayIndex; i < totalCount; i++)
            {
                if (!enumerator.MoveNext())
                {
                    break;
                }

                array[i] = enumerator.Current;
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            foreach (IEnumerable<T> lst in this.lists)
            {
                foreach (T var in lst)
                {
                    yield return var;
                }
            }

            yield break;
        }

        public override int Remove(T item) => throw new InvalidOperationException();

        public override void Insert(int index, T item) => throw new InvalidOperationException();

        public override int IndexOf(T item)
        {
            var offset = 0;

            int foundIndex;
            foreach (List<T> list in this.lists.Select(p => p.ToList()))
            {
                if ((foundIndex = list.IndexOf(item)) != -1)
                {
                    return foundIndex + offset;
                }
                else
                {
                    offset += list.Count();
                }
            }

            return -1;
        }

        public override void RemoveAt(int index) => throw new InvalidOperationException();

        internal void SetList<TList>(TList list)
            where TList : class, IEnumerable<T>, INotifyCollectionChanged
        {
            this.lists.Add(list ?? throw new ArgumentNullException(nameof(list)));
            list.CollectionChanged += this.List_CollectionChanged;
        }

        internal void RemoveList<TList>(TList list)
            where TList : class, IEnumerable<T>, INotifyCollectionChanged
        {
            try
            {
                list.CollectionChanged -= this.List_CollectionChanged;
            }
            catch
            {
            }

            this.lists.Remove(list ?? throw new ArgumentNullException(nameof(list)));
        }

        private void List_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => this.TriggerReset();
    }
}