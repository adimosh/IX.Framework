// <copyright file="UndoableViewModelBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System.ComponentModel;

namespace IX.Undoable.WPF
{
    /// <summary>
    /// A base class for undo-able view models.
    /// </summary>
    public abstract class UndoableViewModelBase : EditableItemBase, IEditableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UndoableViewModelBase"/> class.
        /// </summary>
        protected UndoableViewModelBase()
        {
        }
    }
}