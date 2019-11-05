// <copyright file="PushDownStack{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.Serialization;
using IX.Abstractions.Collections;
using JetBrains.Annotations;

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
        public T Peek() =>
            this.InvokeIfNotDisposed(
                reference => reference.ReadLock(
                    referenceL2 => referenceL2.InternalContainer.Count > 0
                        ? referenceL2.InternalContainer[referenceL2.InternalContainer.Count - 1]
                        : default, reference), this);

        /// <summary>
        ///     Pops the topmost element from the stack, removing it.
        /// </summary>
        /// <returns>The topmost element in the stack, if any.</returns>
        public T Pop() => this.InvokeIfNotDisposed(
            reference =>
            {
                if (reference.Limit == 0)
                {
                    return default;
                }

                return reference.WriteLock(
                    referenceL2 =>
                    {
                        var index = referenceL2.InternalContainer.Count - 1;

                        if (index < 0)
                        {
                            return default;
                        }

                        T item = referenceL2.InternalContainer[index];

                        referenceL2.InternalContainer.RemoveAt(index);

                        return item;
                    }, reference);
            }, this);

        /// <summary>
        ///     Pushes an element to the top of the stack.
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item) =>
            this.InvokeIfNotDisposed(
                (
                    itemL2,
                    cThis) =>
                {
                    if (cThis.Limit == 0)
                    {
                        return;
                    }

                    cThis.WriteLock(
                        (
                            itemL1,
                            c2This) =>
                        {
                            if (c2This.InternalContainer.Count == c2This.Limit)
                            {
                                c2This.InternalContainer.RemoveAt(0);
                            }

                            c2This.InternalContainer.Add(itemL1);
                        }, itemL2,
                        cThis);
                }, item,
                this);

        /// <summary>
        ///     This method does nothing.
        /// </summary>
        void IStack<T>.TrimExcess()
        {
        }
    }
}