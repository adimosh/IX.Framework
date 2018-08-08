// <copyright file="AutoReleaseTransactionContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.StandardExtensions;
using IX.Undoable;

namespace IX.Observable
{
    /// <summary>
    /// An auto-capture-releasing class that captures in a transaction.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class AutoReleaseTransactionContext : IDisposable
    {
        private readonly IUndoableItem item;
        private readonly IEnumerable<IUndoableItem> items;
        private readonly EventHandler<EditCommittedEventArgs> editableHandler;
        private readonly IUndoableItem parentContext;

        private bool success;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoReleaseTransactionContext"/> class.
        /// </summary>
        public AutoReleaseTransactionContext()
        {
            this.success = true;
        }

#pragma warning disable IDE0016 // Use 'throw' expression
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoReleaseTransactionContext" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="editableHandler">The editable handler.</param>
        public AutoReleaseTransactionContext(IUndoableItem item, IUndoableItem parentContext, EventHandler<EditCommittedEventArgs> editableHandler)
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

            if (editableHandler == null)
            {
                throw new ArgumentNullException(nameof(editableHandler));
            }
#endif

            if (!item.IsCapturedIntoUndoContext || item.ParentUndoContext != parentContext)
            {
                throw new ItemNotCapturedIntoUndoContextException();
            }

            this.items = null;
            this.item = item;
            this.editableHandler = editableHandler;
            this.parentContext = parentContext;

            item.ReleaseFromUndoContext();

            if (item is IEditCommittableItem tei)
            {
                tei.EditCommitted -= editableHandler;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoReleaseTransactionContext" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="editableHandler">The editable handler.</param>
        public AutoReleaseTransactionContext(IEnumerable<IUndoableItem> items, IUndoableItem parentContext, EventHandler<EditCommittedEventArgs> editableHandler)
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

            if (editableHandler == null)
            {
                throw new ArgumentNullException(nameof(editableHandler));
            }
#endif

            if (items.Any((item, pc) => !item.IsCapturedIntoUndoContext || item.ParentUndoContext != pc, parentContext))
            {
                throw new ItemNotCapturedIntoUndoContextException();
            }

            this.items = items;
            this.item = null;
            this.editableHandler = editableHandler;

            foreach (IUndoableItem item in items)
            {
                item.ReleaseFromUndoContext();

                if (item is IEditCommittableItem tei)
                {
                    tei.EditCommitted -= editableHandler;
                }
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
                if (this.item != null)
                {
                    this.item.CaptureIntoUndoContext(this.parentContext);

                    if (this.item is IEditCommittableItem tei)
                    {
                        tei.EditCommitted += this.editableHandler;
                    }
                }

                if (this.items != null)
                {
                    foreach (IUndoableItem item in this.items)
                    {
                        item.CaptureIntoUndoContext(this.parentContext);

                        if (this.item is IEditCommittableItem tei)
                        {
                            tei.EditCommitted += this.editableHandler;
                        }
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