// <copyright file="Stack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;

namespace IX.System.Collections.Generic
{
    /// <summary>
    /// Represents a variable-size last-in first-out (LIFO) collection of instances of the same specified type.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    /// <seealso cref="global::System.Collections.Generic.Stack{T}" />
    /// <seealso cref="IStack{T}" />
    public class Stack<T> : global::System.Collections.Generic.Stack<T>, IStack<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stack{T}"/> class.
        /// </summary>
        public Stack()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="Stack{T}" /> can contain.</param>
        public Stack(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public Stack(IEnumerable<T> collection)
            : base(collection)
        {
        }
    }
}