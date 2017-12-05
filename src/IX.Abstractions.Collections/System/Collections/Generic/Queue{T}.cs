// <copyright file="Queue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;

namespace IX.System.Collections.Generic
{
    /// <summary>
    /// Represents a variable-size first-in first-out (FIFO) collection of instances of the same specified type.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    /// <seealso cref="global::System.Collections.Generic.Queue{T}" />
    /// <seealso cref="IQueue{T}" />
    public class Queue<T> : global::System.Collections.Generic.Queue<T>, IQueue<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        public Queue()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="Queue{T}" /> can contain.</param>
        public Queue(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public Queue(IEnumerable<T> collection)
            : base(collection)
        {
        }
    }
}