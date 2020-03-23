// <copyright file="PushDownStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.Serialization;
using IX.Abstractions.Collections;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;
using Constants = IX.Abstractions.Collections.Constants;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    ///     A stack that pushes down extra items above a certain limit.
    /// </summary>
    /// <typeparam name="T">The stack item type.</typeparam>
    /// <seealso cref="IX.StandardExtensions.Threading.ReaderWriterSynchronizedBase" />
    /// <seealso cref="IX.System.Collections.Generic.IStack{T}" />
    /// <seealso cref="IDisposable" />
    /// <seealso cref="IStack{T}" />
    [DataContract(
        Namespace = Constants.DataContractNamespace,
        Name = "PushDownStackOf{0}")]
    [PublicAPI]
    public class PushDownStack<T> : PushingCollectionBase<T>, IStack<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PushDownStack{T}" /> class.
        /// </summary>
        public PushDownStack()
            : this(Constants.DefaultPushDownLimit)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PushDownStack{T}" /> class.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <exception cref="IX.Abstractions.Collections.LimitArgumentNegativeException">
        ///     <paramref name="limit" /> is a negative
        ///     integer.
        /// </exception>
        public PushDownStack(int limit)
        : base(limit)
        {
        }

        /// <summary>
        ///     Peeks in the stack to view the topmost item, without removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Peek()
        {
            if (!this.TryPeek(out var item))
            {
                throw new InvalidOperationException(Resources.ErrorStackIsEmpty);
            }

            return item;
        }

        /// <summary>
        ///     Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop()
        {
            if (!this.TryPop(out var item))
            {
                throw new InvalidOperationException(Resources.ErrorStackIsEmpty);
            }

            return item;
        }

        /// <summary>
        ///     Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item) => this.Append(item);

        /// <summary>
        /// Pushes a range of elements to the top of the stack.
        /// </summary>
        /// <param name="items">The item range to push.</param>
        public void PushRange(T[] items) => this.Append(items);

        /// <summary>
        /// Pushes a range of elements to the top of the stack.
        /// </summary>
        /// <param name="items">The item range to push.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to push.</param>
        public void PushRange(
            T[] items,
            int startIndex,
            int count) =>
            this.Append(
                items,
                startIndex,
                count);

        /// <summary>
        /// Attempts to peek at the topmost item from the stack, without removing it.
        /// </summary>
        /// <param name="item">The topmost element in the stack, default if unsuccessful.</param>
        /// <returns>
        /// <see langword="true" /> if an item is peeked at successfully, <see langword="false" /> otherwise, or if the
        /// stack is empty.
        /// </returns>
        public bool TryPeek(out T item)
        {
            this.RequiresNotDisposed();

            if (this.Limit == 0)
            {
                item = default;
                return false;
            }

            using (this.ReadLock())
            {
                var index = this.InternalContainer.Count - 1;

                if (index < 0)
                {
                    item = default;
                    return false;
                }

                item = this.InternalContainer[index];
                return true;
            }
        }

        /// <summary>
        /// Attempts to pop the topmost item from the stack, and remove it if successful.
        /// </summary>
        /// <param name="item">The topmost element in the stack, default if unsuccessful.</param>
        /// <returns>
        /// <see langword="true" /> if an item is popped successfully, <see langword="false" /> otherwise, or if the
        /// stack is empty.
        /// </returns>
        public bool TryPop(out T item)
        {
            this.RequiresNotDisposed();

            if (this.Limit == 0)
            {
                item = default;
                return false;
            }

            using (this.WriteLock())
            {
                var index = this.InternalContainer.Count - 1;

                if (index < 0)
                {
                    item = default;
                    return false;
                }

                item = this.InternalContainer[index];

                this.InternalContainer.RemoveAt(index);

                return true;
            }
        }

        /// <summary>
        ///     This method does nothing.
        /// </summary>
        void IStack<T>.TrimExcess()
        {
        }
    }
}