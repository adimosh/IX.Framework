// <copyright file="DispatcherObjectAwaiter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.AsyncUserInterface
{
    /// <summary>
    ///     An awaiter based on the dispatcher.
    /// </summary>
    /// <seealso cref="INotifyCompletion" />
    [PublicAPI]
    public class DispatcherObjectAwaiter : INotifyCompletion
    {
        [NotNull]
        private readonly DispatcherObject sourceObject;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DispatcherObjectAwaiter" /> class.
        /// </summary>
        /// <param name="sourceObject">The source object.</param>
        public DispatcherObjectAwaiter([NotNull] DispatcherObject sourceObject)
        {
            Contract.RequiresNotNullPrivate(
                in sourceObject,
                nameof(sourceObject));

            this.sourceObject = sourceObject;
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="DispatcherObjectAwaiter" /> is is completed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if is completed; otherwise, <c>false</c>.
        /// </value>
        public bool Iscompleted => this.sourceObject.CheckAccess();

        /// <summary>Schedules the continuation action that's invoked when the instance completes.</summary>
        /// <param name="continuation">The action to invoke when the operation completes.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <paramref name="continuation" /> argument is null (Nothing in
        ///     Visual Basic).
        /// </exception>
        public void OnCompleted(Action continuation)
        {
            Contract.RequiresNotNullPrivate(
                in continuation,
                nameof(continuation));

            this.sourceObject.Dispatcher.Invoke(continuation);
        }

        /// <summary>
        ///     Gets the result.
        /// </summary>
        public void GetResult()
        {
        }
    }
}