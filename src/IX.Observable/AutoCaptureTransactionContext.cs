// <copyright file="AutoCaptureTransactionContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.Undoable;

namespace IX.Observable
{
    /// <summary>
    /// An auto-capturing class that captures in a transaction.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class AutoCaptureTransactionContext : IDisposable
    {
        private readonly IUndoableItem item;
        private readonly IEnumerable<IUndoableItem> items;

        private bool success;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCaptureTransactionContext"/> class.
        /// </summary>
        public AutoCaptureTransactionContext()
        {
            this.success = true;
        }

#pragma warning disable IDE0016 // Use 'throw' expression
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCaptureTransactionContext"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="parentContext">The parent context.</param>
        public AutoCaptureTransactionContext(IUndoableItem item, IUndoableItem parentContext)
        {
#if DEBUG
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (parentContext == null)
            {
                throw new ArgumentNullException(nameof(parentContext));
            }
#endif

            this.items = null;
            this.item = item;

            item.CaptureIntoUndoContext(parentContext);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCaptureTransactionContext"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="parentContext">The parent context.</param>
        public AutoCaptureTransactionContext(IEnumerable<IUndoableItem> items, IUndoableItem parentContext)
        {
#if DEBUG
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (parentContext == null)
            {
                throw new ArgumentNullException(nameof(parentContext));
            }
#endif

            this.items = items;
            this.item = null;

            foreach (IUndoableItem item in items)
            {
                item.CaptureIntoUndoContext(parentContext);
            }
        }
#pragma warning restore IDE0016 // Use 'throw' expression

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!this.success)
            {
                this.item?.ReleaseFromUndoContext();

                if (this.items != null)
                {
                    foreach (IUndoableItem item in this.items)
                    {
                        item.ReleaseFromUndoContext();
                    }
                }
            }
        }

        /// <summary>
        /// Marks this context as successful.
        /// </summary>
        public void Success() => this.success = true;
    }
}