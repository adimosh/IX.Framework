// <copyright file="ListListAdapter{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IX.Observable.Adapters
{
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "ListAdapterOf{0}", ItemName = "Item")]
    internal class ListListAdapter<T> : ListAdapter<T>
    {
        private readonly List<T> list;

        public ListListAdapter()
        {
            this.list = new List<T>();
        }

        public ListListAdapter(IEnumerable<T> source)
        {
            this.list = new List<T>(source);
        }

        public override int Count => this.list.Count;

        public override bool IsReadOnly => false;

        public override T this[int index]
        {
            get => this.list[index];
            set => this.list[index] = value;
        }

        public override int Add(T item)
        {
            this.list.Add(item);
            return this.list.Count - 1;
        }

        public override int AddRange(IEnumerable<T> items)
        {
            var index = this.list.Count;
            this.list.AddRange(items);
            return index;
        }

        public override void Clear() => this.list.Clear();

        public override bool Contains(T item) => this.list.Contains(item);

        public override void CopyTo(T[] array, int arrayIndex) => this.list.CopyTo(array, arrayIndex);

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - Unavoidable here
        public override IEnumerator<T> GetEnumerator() => this.list.GetEnumerator();
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

        public override int IndexOf(T item) => this.list.IndexOf(item);

        public override void Insert(int index, T item) => this.list.Insert(index, item);

        public override int Remove(T item)
        {
            var index = this.list.IndexOf(item);
            this.list.Remove(item);
            return index;
        }

        public override void RemoveAt(int index) => this.list.RemoveAt(index);
    }
}