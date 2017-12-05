// <copyright file="ListAdapter{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;

namespace IX.Observable.Adapters
{
    /// <summary>
    /// An adapter class for various list types.
    /// </summary>
    /// <typeparam name="T">The type of items in the adapter.</typeparam>
    /// <seealso cref="IX.Observable.Adapters.ListAdapter{T}" />
    /// <seealso cref="IX.Observable.Adapters.IListAdapter{T}" />
    public abstract class ListAdapter<T> : CollectionAdapter<T>, IListAdapter<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is fixed size.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fixed size; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>Since this is an adapter, this property always returns false.</para>
        /// </remarks>
        public bool IsFixedSize => false;

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <value>
        /// The item at the specified index.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The item.</returns>
        public abstract T this[int index] { get; set; }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <value>
        /// The item at the specified index.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The item.</returns>
        object IList.this[int index]
        {
            get => this[index];
            set
            {
                if (value is T)
                {
                    this[index] = (T)value;
                }
                else
                {
                    throw new ArgumentOfWrongTypeException();
                }
            }
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <value>
        /// The item at the specified index.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The item.</returns>
        T IReadOnlyList<T>.this[int index] => this[index];

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="value">The value of the item to add.</param>
        /// <returns>The index of the freshly-added item.</returns>
        int IList.Add(object value) => this.Add(this.GetItemOfProperTypeFromObject(value));

        /// <summary>
        /// Adds a range of items to the list.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The index of the firstly-introduced item.</returns>
        public abstract int AddRange(IEnumerable<T> items);

        /// <summary>
        /// Determines whether the collection contains the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the collection contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        bool IList.Contains(object value) => this.Contains(this.GetItemOfProperTypeFromObject(value));

        /// <summary>
        /// Determines the index of a specific item value, if any.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The index of the item, or <c>-1</c> if not found.</returns>
        int IList.IndexOf(object value) => this.IndexOf(this.GetItemOfProperTypeFromObject(value));

        /// <summary>
        /// Determines the index of a specific item, if any.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index of the item, or <c>-1</c> if not found.</returns>
        public abstract int IndexOf(T item);

        /// <summary>
        /// Inserts an item value at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="value">The item value.</param>
        void IList.Insert(int index, object value) => this.Insert(index, this.GetItemOfProperTypeFromObject(value));

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="item">The item.</param>
        public abstract void Insert(int index, T item);

        /// <summary>
        /// Removes the specified item value.
        /// </summary>
        /// <param name="value">The item value to remove.</param>
        void IList.Remove(object value) => this.Remove(this.GetItemOfProperTypeFromObject(value));

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to remove an item from.</param>
        public abstract void RemoveAt(int index);

        /// <summary>
        /// Gets an item of the proper type from an object value.
        /// </summary>
        /// <param name="value">The object value to derive an item from.</param>
        /// <returns>An item of the proper collection item type.</returns>
        protected T GetItemOfProperTypeFromObject(object value)
        {
            if (!(value is T))
            {
                throw new ArgumentException(Resources.OperationItemTypeError, nameof(value));
            }

            return (T)value;
        }
    }
}