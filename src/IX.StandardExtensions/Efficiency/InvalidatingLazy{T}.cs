// <copyright file="InvalidatingLazy{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Efficiency
{
    /// <summary>
    ///     A lazy initializing class that also offers he possibility to invalidate the value stored in it.
    /// </summary>
    /// <typeparam name="T">The type of item instance initialized by the invalidating lazy.</typeparam>
    /// <seealso cref="System.Lazy{T}" />
    [PublicAPI]
    public class InvalidatingLazy<T>
    {
        private Lazy<T> internalLazy;
        private Func<Lazy<T>> lazyCreator;

#pragma warning disable HAA0302 // Display class allocation to capture closure - we need to form a closure
#pragma warning disable HAA0301 // Closure Allocation Source
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidatingLazy{T}" /> class.
        /// </summary>
        public InvalidatingLazy()
        {
            this.internalLazy = new Lazy<T>();
            this.lazyCreator = () => new Lazy<T>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidatingLazy{T}" /> class.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        public InvalidatingLazy(Func<T> valueFactory)
        {
            this.internalLazy = new Lazy<T>(valueFactory);
            this.lazyCreator = () => new Lazy<T>(valueFactory);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidatingLazy{T}" /> class.
        /// </summary>
        /// <param name="isThreadSafe">
        ///     <see langword="true" /> to make this instance usable concurrently by multiple threads;
        ///     <see langword="false" /> to make the instance usable by only one thread at a time.
        /// </param>
        public InvalidatingLazy(bool isThreadSafe)
        {
            this.internalLazy = new Lazy<T>(isThreadSafe);
            this.lazyCreator = () => new Lazy<T>(isThreadSafe);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidatingLazy{T}" /> class.
        /// </summary>
        /// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
        public InvalidatingLazy(LazyThreadSafetyMode mode)
        {
            this.internalLazy = new Lazy<T>(mode);
            this.lazyCreator = () => new Lazy<T>(mode);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidatingLazy{T}" /> class.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        /// <param name="isThreadSafe">
        ///     <see langword="true" /> to make this instance usable concurrently by multiple threads;
        ///     <see langword="false" /> to make this instance usable by only one thread at a time.
        /// </param>
        public InvalidatingLazy(
            Func<T> valueFactory,
            bool isThreadSafe)
        {
            this.internalLazy = new Lazy<T>(
                valueFactory,
                isThreadSafe);
            this.lazyCreator = () => new Lazy<T>(
                valueFactory,
                isThreadSafe);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidatingLazy{T}" /> class.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        /// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
        public InvalidatingLazy(
            Func<T> valueFactory,
            LazyThreadSafetyMode mode)
        {
            this.internalLazy = new Lazy<T>(
                valueFactory,
                mode);
            this.lazyCreator = () => new Lazy<T>(
                valueFactory,
                mode);
        }
#pragma warning restore HAA0301 // Closure Allocation Source
#pragma warning restore HAA0302 // Display class allocation to capture closure

        /// <summary>Gets a value indicating whether a value has been created for this <see cref="InvalidatingLazy{T}"></see> instance.</summary>
        /// <returns>true if a value has been created for this <see cref="InvalidatingLazy{T}"></see> instance; otherwise, false.</returns>
        public bool IsValueCreated => this.internalLazy.IsValueCreated;

        /// <summary>Gets the lazily initialized value of the current <see cref="InvalidatingLazy{T}"></see> instance.</summary>
        /// <returns>The lazily initialized value of the current <see cref="InvalidatingLazy{T}"></see> instance.</returns>
        /// <exception cref="T:System.MemberAccessException">
        ///     The <see cref="InvalidatingLazy{T}"></see> instance is initialized to use
        ///     the default constructor of the type that is being lazily initialized, and permissions to access the constructor are
        ///     missing.
        /// </exception>
        /// <exception cref="T:System.MissingMemberException">
        ///     The <see cref="InvalidatingLazy{T}"></see> instance is initialized to use
        ///     the default constructor of the type that is being lazily initialized, and that type does not have a public,
        ///     parameterless constructor.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The initialization function tries to access
        ///     <see cref="InvalidatingLazy{T}.Value"></see> on this instance.
        /// </exception>
        public T Value => this.internalLazy.Value;

        /// <summary>
        ///     Creates and returns a string representation of the <see cref="InvalidatingLazy{T}.Value"></see> property for this
        ///     instance.
        /// </summary>
        /// <returns>
        ///     The result of calling the <see cref="M:System.Object.ToString"></see> method on the
        ///     <see cref="InvalidatingLazy{T}.Value"></see> property for this instance, if the value has been created (that is, if the
        ///     <see cref="InvalidatingLazy{T}.IsValueCreated"></see> property returns true). Otherwise, a string indicating that the
        ///     value has not been created.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <see cref="InvalidatingLazy{T}.Value"></see> property is null.</exception>
        public override string ToString() => this.internalLazy.ToString();

        /// <summary>
        ///     Invalidates the instance stored into this lazy initializer, and allows a new one to be created.
        /// </summary>
        /// <returns>
        ///     The initial instance stored into this invalidating lazy initializer, if one exists, or a default value
        ///     otherwise.
        /// </returns>
        public T Invalidate()
        {
            var existingLazy = Interlocked.Exchange(
                ref this.internalLazy,
                this.lazyCreator());

            return existingLazy.IsValueCreated ? existingLazy.Value : default;
        }
    }
}