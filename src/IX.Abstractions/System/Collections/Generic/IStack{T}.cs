// <copyright file="IStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.System.Collections.Generic
{
    /// <summary>
    ///     A contract for a stack.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    /// <seealso cref="IEnumerable{T}" />
    /// <seealso cref="ICollection" />
    /// <seealso cref="IReadOnlyCollection{T}" />
    [PublicAPI]
    public interface IStack<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        ///     Clears the observable stack.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Checks whether or not a certain item is in the stack.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><see langword="true" /> if the item was found, <see langword="false" /> otherwise.</returns>
        bool Contains(T item);

        /// <summary>
        ///     Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        T Peek();

        /// <summary>
        ///     Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        T Pop();

        /// <summary>
        ///     Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        void Push(T item);

        /// <summary>
        ///     Copies all elements of the stack to a new array.
        /// </summary>
        /// <returns>An array containing all items in the stack.</returns>
        [NotNull]
        T[] ToArray();

        /// <summary>
        ///     Sets the capacity to the actual number of elements in the stack if that number is less than 90 percent of current
        ///     capacity.
        /// </summary>
        void TrimExcess();
    }
}