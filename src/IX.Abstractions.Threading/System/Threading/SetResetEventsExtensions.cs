// <copyright file="SetResetEventsExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.System.Threading
{
    /// <summary>
    /// Extension methods for set/reset event classes.
    /// </summary>
    [PublicAPI]
    public static class SetResetEventsExtensions
    {
        /// <summary>
        /// Converts the source <see cref="T:System.Threading.AutoResetEvent"/> to a <see cref="ISetResetEvent"/> abstraction.
        /// </summary>
        /// <param name="source">The source event.</param>
        /// <returns>The abstracted version of the same event.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static ISetResetEvent AsAbstraction(this global::System.Threading.AutoResetEvent source) => new AutoResetEvent(source ?? throw new ArgumentNullException(nameof(source)));

        /// <summary>
        /// Converts the source <see cref="T:System.Threading.ManualResetEvent"/> to a <see cref="ISetResetEvent"/> abstraction.
        /// </summary>
        /// <param name="source">The source event.</param>
        /// <returns>The abstracted version of the same event.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static ISetResetEvent AsAbstraction(this global::System.Threading.ManualResetEvent source) => new ManualResetEvent(source ?? throw new ArgumentNullException(nameof(source)));

        /// <summary>
        /// Converts the source <see cref="T:System.Threading.ManualResetEventSlim"/> to a <see cref="ISetResetEvent"/> abstraction.
        /// </summary>
        /// <param name="source">The source event.</param>
        /// <returns>The abstracted version of the same event.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static ISetResetEvent AsAbstraction(this global::System.Threading.ManualResetEventSlim source) => new ManualResetEventSlim(source ?? throw new ArgumentNullException(nameof(source)));
    }
}