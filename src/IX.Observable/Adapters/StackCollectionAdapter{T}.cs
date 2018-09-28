// <copyright file="StackCollectionAdapter{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IX.Observable.Adapters
{
    /// <summary>
    /// A collection adapter for a stack.
    /// </summary>
    /// <typeparam name="T">The type of item in the stack.</typeparam>
    /// <seealso cref="IX.Observable.Adapters.CollectionAdapter{T}" />
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "StackAdapterOf{0}", ItemName = "Item")]
    internal class StackCollectionAdapter<T> : CollectionAdapter<T>
    {
        /// <summary>
        /// The base stack.
        /// </summary>
        private readonly Stack<T> stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackCollectionAdapter{T}"/> class.
        /// </summary>
        internal StackCollectionAdapter()
        {
            this.stack = new Stack<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackCollectionAdapter{T}"/> class.
        /// </summary>
        /// <param name="stack">The stack.</param>
        internal StackCollectionAdapter(Stack<T> stack)
        {
            this.stack = new Stack<T>(stack);
        }

        /// <summary>
        /// Gets the number of items.
        /// </summary>
        /// <value>The number of items.</value>
        public override int Count => this.stack.Count;

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public override bool IsReadOnly => false;

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index of the freshly-added item.</returns>
        public override int Add(T item)
        {
            this.stack.Push(item);
            return this.stack.Count - 1;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear() => this.stack.Clear();

        /// <summary>
        /// Determines whether the container list contains the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the container list contains the specified item; otherwise, <c>false</c>.</returns>
        public override bool Contains(T item) => this.stack.Contains(item);

        /// <summary>
        /// Copies the contents of the container to an array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public override void CopyTo(T[] array, int arrayIndex) => this.stack.CopyTo(array, arrayIndex);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index of the removed item, or <c>-1</c> if removal was not successful.</returns>
        public override int Remove(T item) => -1;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - Unavoidable here
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public override IEnumerator<T> GetEnumerator() => this.stack.GetEnumerator();
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

        /// <summary>
        /// Pops an item in the stack.
        /// </summary>
        /// <returns>T.</returns>
        public T Pop() => this.stack.Pop();

        /// <summary>
        /// Peeks at the top item in the stack.
        /// </summary>
        /// <returns>T.</returns>
        public T Peek() => this.stack.Peek();

        /// <summary>
        /// Pushes the specified item in the stack.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Push(T item) => this.stack.Push(item);

        /// <summary>
        /// Converts all items in the stack to an array.
        /// </summary>
        /// <returns>The array of items.</returns>
        public T[] ToArray() => this.stack.ToArray();

        /// <summary>
        /// Trims the excess space in the stack.
        /// </summary>
        public void TrimExcess() => this.stack.TrimExcess();
    }
}