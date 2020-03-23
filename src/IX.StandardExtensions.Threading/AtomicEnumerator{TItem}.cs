// <copyright file="AtomicEnumerator{TItem}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using IX.StandardExtensions.ComponentModel;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Efficiency;
using JetBrains.Annotations;
using DiagCA = System.Diagnostics.CodeAnalysis;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    ///     An atomic enumerator that can enumerate items one at a time, atomically.
    /// </summary>
    /// <typeparam name="TItem">The type of the items to enumerate.</typeparam>
    /// <seealso cref="IEnumerator{T}" />
    /// <seealso cref="DisposableBase" />
    [PublicAPI]
    public abstract class AtomicEnumerator<TItem> : DisposableBase, IEnumerator<TItem>
    {
        [DiagCA.SuppressMessage(
            "ReSharper",
            "StaticMemberInGenericType",
            Justification = "We have a specialized staatic field.")]
        private static readonly ConcurrentDictionary<Type, Delegate> ConstructionDelegates = new ConcurrentDictionary<Type, Delegate>();

        /// <summary>
        /// Prevents a default instance of the <see cref="AtomicEnumerator{TItem}"/> class from being created.
        /// </summary>
        protected private AtomicEnumerator()
        {
        }

        /// <summary>
        ///     Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The current element.</value>
        public abstract TItem Current { get; }

        /// <summary>
        ///     Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The current element.</value>
        [DiagCA.SuppressMessage(
            "Performance",
            "HAA0601:Value type to reference type conversion causing boxing allocation",
            Justification = "Unavoidable with a generic enumerator.")]
        object IEnumerator.Current => this.Current;

        /// <summary>
        /// Creates an atomic enumerator from a collection.
        /// </summary>
        /// <typeparam name="TCollection">The type of the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="readLock">The function to acquire a read lock.</param>
        /// <returns>The created atomic enumerator.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/>
        /// or
        /// <paramref name="readLock"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        [DiagCA.SuppressMessage(
            "Performance",
            "HAA0303:Lambda or anonymous method in a generic method allocates a delegate instance",
            Justification = "We need this instance allocated.")]
        public static AtomicEnumerator<TItem> FromCollection<TCollection>([NotNull] TCollection collection, [NotNull] Func<ReadOnlySynchronizationLocker> readLock)
            where TCollection : class, IEnumerable<TItem>
        {
            // Validate arguments
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Contract.RequiresNotNull(
                in readLock,
                nameof(readLock));

            var initializer = ConstructionDelegates.GetOrAdd(
                typeof(TCollection),
                (
                    collectionType,
                    internalCollection) =>
                {
                    // Get used types
                    var getEnumeratorMethodInfo = collectionType.GetMethod(
                        nameof(IEnumerable<TItem>.GetEnumerator),
                        BindingFlags.Public | BindingFlags.Instance);

                    // ReSharper disable once PossibleNullReferenceException - We know this cannot be null, as we're interrogating the GetEnumerator method if an IEnumerable - there must be at least one
                    var enumeratorType = getEnumeratorMethodInfo.ReturnType;
                    var atomicEnumeratorType = typeof(AtomicEnumerator<,>).MakeGenericType(
                        typeof(TItem),
                        enumeratorType);
                    var atomicEnumeratorConstructorInfo = atomicEnumeratorType.GetConstructors()[0];

                    // Prepare parameter expressions
                    var parameter1 = Expression.Parameter(collectionType);
                    var parameter2 = Expression.Parameter(typeof(Func<ReadOnlySynchronizationLocker>));

                    // Prepare expression
                    return Expression.Lambda(
                            Expression.New(
                                atomicEnumeratorConstructorInfo,
                                Expression.Call(parameter1, getEnumeratorMethodInfo),
                                parameter2),
                            parameter1,
                            parameter2)
                        .Compile();
                },
                collection);

            return (AtomicEnumerator<TItem>)initializer.DynamicInvoke(
                collection,
                readLock);
        }

        /// <summary>
        /// Creates an atomic enumerator from an already-existing enumerator.
        /// </summary>
        /// <typeparam name="TEnumerator">The type of the enumerator.</typeparam>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="readLock">The function to acquire a read lock.</param>
        /// <returns>The created atomic enumerator.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerator"/>
        /// or
        /// <paramref name="readLock"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static AtomicEnumerator<TItem> FromEnumerator<TEnumerator>(
            [NotNull] TEnumerator enumerator,
            Func<ReadOnlySynchronizationLocker> readLock)
            where TEnumerator : IEnumerator<TItem>
        {
            if (enumerator == null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            Contract.RequiresNotNull(
                in readLock,
                nameof(readLock));

            return new AtomicEnumerator<TItem, TEnumerator>(
                enumerator,
                readLock);
        }

        /// <summary>
        ///     Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the enumerator was successfully advanced to the next element;
        ///     <see langword="false" /> if the enumerator has passed the end of the collection.
        /// </returns>
        public abstract bool MoveNext();

        /// <summary>
        ///     Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public abstract void Reset();
    }
}