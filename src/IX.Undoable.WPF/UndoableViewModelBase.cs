// <copyright file="UndoableViewModelBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Undoable.WPF
{
    /// <summary>
    /// A base class for undo-able view models.
    /// </summary>
    public abstract class UndoableViewModelBase : EditableItemBase, IEditCommittableItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UndoableViewModelBase"/> class.
        /// </summary>
        protected UndoableViewModelBase()
            : base(50)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoableViewModelBase" /> class.
        /// </summary>
        /// <param name="limit">The limit of the undo/redo context.</param>
        protected UndoableViewModelBase(int limit)
            : base(limit)
        {
        }
    }
}