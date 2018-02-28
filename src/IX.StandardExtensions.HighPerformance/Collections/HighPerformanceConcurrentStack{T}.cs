// <copyright file="HighPerformanceConcurrentStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using IX.StandardExtensions.Threading;
using IX.System.Collections.Generic;
using IX.System.Threading;

namespace IX.StandardExtensions.HighPerformance.Collections
{
    /// <summary>
    /// A high-performance concurrent stack.
    /// </summary>
    /// <typeparam name="T">The type of items in the stack</typeparam>
    /// <seealso cref="IX.StandardExtensions.Threading.ReaderWriterSynchronizedBase" />
    /// <seealso cref="IX.System.Collections.Generic.IStack{T}" />
    public class HighPerformanceConcurrentStack<T> : ReaderWriterSynchronizedBase, IStack<T>
    {
        private readonly Stack<T> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentStack{T}"/> class.
        /// </summary>
        public HighPerformanceConcurrentStack()
        {
            this.items = new Stack<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentStack{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public HighPerformanceConcurrentStack(int capacity)
        {
            this.items = new Stack<T>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentStack{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public HighPerformanceConcurrentStack(global::System.Collections.Generic.IEnumerable<T> collection)
        {
            this.items = new Stack<T>(collection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentStack{T}" /> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentStack(IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Stack<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentStack{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentStack(int capacity, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Stack<T>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentStack{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentStack(global::System.Collections.Generic.IEnumerable<T> collection, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Stack<T>(collection);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="HighPerformanceConcurrentStack{T}"/>.
        /// </summary>
        /// <value>The count.</value>
        public int Count => this.ReadLock(() => this.items.Count);

        /// <summary>
        /// Clears the observable stack.
        /// </summary>
        public void Clear() => this.WriteLock(this.items.Clear);

        /// <summary>
        /// Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><c>true</c> if the item was found, <c>false</c> otherwise.</returns>
        public bool Contains(T item) => this.ReadLock(itemL1 => this.items.Contains(itemL1), item);

        /// <summary>
        /// Copies the elements in the stack to an array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public void CopyTo(T[] array, int index) => this.ReadLock((arrayL1, indexL1) => this.items.CopyTo(arrayL1, indexL1), array, index);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public global::System.Collections.Generic.IEnumerator<T> GetEnumerator() => new AtomicEnumerator<T>(this.items.GetEnumerator(), this.ReadLock);

        /// <summary>
        /// Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek() => this.ReadLock(this.items.Peek);

        /// <summary>
        /// Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop() => this.WriteLock(this.items.Pop);

        /// <summary>
        /// Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item) => this.WriteLock(itemL1 => this.items.Push(itemL1), item);

        /// <summary>
        /// Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        public T[] ToArray() => this.ReadLock(this.items.ToArray);

        /// <summary>
        /// Sets the capacity to the actual number of elements in the stack if that number is less than 90 percent of current capacity.
        /// </summary>
        public void TrimExcess() => this.WriteLock(this.items.TrimExcess);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}