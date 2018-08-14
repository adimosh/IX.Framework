// <copyright file="INotifyThreadException.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// A service contract for an entity that notifies of safely-ignorable exceptions.
    /// </summary>
    /// <remarks>
    /// <para>WARNING !!! Do not use this interface to unsafely block exceptions from propagating.</para>
    /// <para>This interface is to be used solely when dealing with exceptions that can either be safely ignored and should only be reported to a logging service, or
    /// have handling at a lower level.</para>
    /// <para>A common use case for this should be for safe disposal of exceptions on fire-and-forget tasks that should not otherwise crash the application.</para>
    /// </remarks>
    public interface INotifyThreadException
    {
        /// <summary>
        /// Triggered when an exception has occurred on a different thread.
        /// </summary>
        event EventHandler<ExceptionOccurredEventArgs> ExceptionOccurredOnSeparateThread;
    }
}