// <copyright file="AutoCaptureTransactionContext.cs" company="Adrian Mos">
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
    /// An auto-capturing class that captures in a transaction.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class AutoCaptureTransactionContext : IDisposable
    {
        private readonly IUndoableItem item;
        private readonly IEnumerable<IUndoableItem> items;
        private readonly EventHandler<EditCommittedEventArgs> editableHandler;

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
        /// Initializes a new instance of the <see cref="AutoCaptureTransactionContext" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="editableHandler">The editable handler.</param>
        public AutoCaptureTransactionContext(IUndoableItem item, IUndoableItem parentContext, EventHandler<EditCommittedEventArgs> editableHandler)
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

            if (item.IsCapturedIntoUndoContext && item.ParentUndoContext != parentContext)
            {
                throw new ItemAlreadyCapturedIntoUndoContextException();
            }

            this.items = null;
            this.item = item;
            this.editableHandler = editableHandler;

            item.CaptureIntoUndoContext(parentContext);

            if (item is IEditCommittableItem tei)
            {
                tei.EditCommitted += editableHandler;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCaptureTransactionContext" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="editableHandler">The editable handler.</param>
        public AutoCaptureTransactionContext(IEnumerable<IUndoableItem> items, IUndoableItem parentContext, EventHandler<EditCommittedEventArgs> editableHandler)
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

            if (items.Any((item, pc) => item.IsCapturedIntoUndoContext && item.ParentUndoContext != pc, parentContext))
            {
                throw new ItemAlreadyCapturedIntoUndoContextException();
            }

            this.items = items;
            this.item = null;
            this.editableHandler = editableHandler;

#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
            foreach (IUndoableItem item in items)
            {
                item.CaptureIntoUndoContext(parentContext);

                if (item is IEditCommittableItem tei)
                {
                    tei.EditCommitted += editableHandler;
                }
            }
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
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
                    this.item.ReleaseFromUndoContext();

                    if (this.item is IEditCommittableItem tei)
                    {
                        tei.EditCommitted -= this.editableHandler;
                    }
                }

                if (this.items != null)
                {
#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
                    foreach (IUndoableItem item in this.items)
                    {
                        item.ReleaseFromUndoContext();

                        if (this.item is IEditCommittableItem tei)
                        {
                            tei.EditCommitted -= this.editableHandler;
                        }
                    }
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
                }
            }
        }

        /// <summary>
        /// Marks this context as successful.
        /// </summary>
        public void Success() => this.success = true;
    }
}