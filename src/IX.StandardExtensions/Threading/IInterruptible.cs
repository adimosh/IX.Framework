// <copyright file="IInterruptible.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    ///     Interface IInterruptible.
    /// </summary>
    /// <seealso cref="IDisposable" />
    [PublicAPI]
    public interface IInterruptible : IDisposable
    {
        /// <summary>
        ///     Interrupts this instance.
        /// </summary>
        void Interrupt();

        /// <summary>
        ///     Resumes this instance.
        /// </summary>
        void Resume();
    }
}