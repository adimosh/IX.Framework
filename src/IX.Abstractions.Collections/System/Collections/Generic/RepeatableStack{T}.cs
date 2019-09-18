// <copyright file="RepeatableStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    ///     A stack that is able to accurately repeat the sequence of items that has been popped from it.
    /// </summary>
    /// <typeparam name="T">The type of items contained in this stack.</typeparam>
    /// <seealso cref="IStack{T}" />
    [PublicAPI]
    public class RepeatableStack<T> : IStack<T>
    {
        private readonly IStack<T> internalStack;
        private readonly List<T> internalRepeatingStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatableStack{T}"/> class.
        /// </summary>
        public RepeatableStack()
        {
            this.internalStack = new Stack<T>();
            this.internalRepeatingStack = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatableStack{T}" /> class.
        /// </summary>
        /// <param name="originalStack">The original stack.</param>
        /// <remarks><para>
        /// Please note that the <paramref name="originalStack"/> will be taken and used as a source for this repeatable stack, meaning that operations on this instance
        /// will reflect to the original.
        /// </para>
        /// </remarks>
        public RepeatableStack(IStack<T> originalStack)
        {
            Contract.RequiresNotNull(
                ref this.internalStack,
                originalStack,
                nameof(originalStack));
            this.internalRepeatingStack = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatableStack{T}" /> class.
        /// </summary>
        /// <param name="originalData">The original data.</param>
        public RepeatableStack(IEnumerable<T> originalData)
        {
            Contract.RequiresNotNull(
                in originalData,
                nameof(originalData));
            this.internalStack = new Stack<T>(originalData);
            this.internalRepeatingStack = new List<T>();
        }

        /// <summary>Gets the number of elements in the collection.</summary>
        /// <returns>The number of elements in the collection. </returns>
        public int Count => this.internalStack.Count;

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        [SuppressMessage("Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "Unavoidable.")]
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        [SuppressMessage("Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "Unavoidable.")]
        public IEnumerator<T> GetEnumerator() => this.internalStack.GetEnumerator();

        /// <summary>
        ///     Clears the observable stack.
        /// </summary>
        public void Clear() => this.internalStack.Clear();

        /// <summary>
        ///     Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><see langword="true" /> if the item was found, <see langword="false" /> otherwise.</returns>
        public bool Contains(T item) => this.internalStack.Contains(item);

        /// <summary>
        ///     Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek() => this.internalStack.Peek();

        /// <summary>
        ///     Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop()
        {
            var item = this.internalStack.Pop();
            this.internalRepeatingStack.Add(item);
            return item;
        }

        /// <summary>
        ///     Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item) => this.internalStack.Push(item);

        /// <summary>
        ///     Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        public T[] ToArray() => this.internalStack.ToArray();

        /// <summary>
        ///     Sets the capacity to the actual number of elements in the stack if that number is less than 90 percent of current
        ///     capacity.
        /// </summary>
        public void TrimExcess() => this.internalStack.TrimExcess();

        /// <summary>
        ///     Gets a repeat of the sequence of elements popped from this instance.
        /// </summary>
        /// <returns>A repeating stack.</returns>
        public IStack<T> Repeat() => new Stack<T>(this.internalRepeatingStack.AsEnumerable().Reverse().ToArray());
    }
}