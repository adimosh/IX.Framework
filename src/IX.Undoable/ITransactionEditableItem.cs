// <copyright file="ITransactionEditableItem.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Undoable
{
    /// <summary>
    /// A contract for an item that is editable in a transactional-style way.
    /// </summary>
    public interface ITransactionEditableItem : IEditCommittableItem
    {
        /// <summary>
        /// Gets a value indicating whether this instance is in edit mode.
        /// </summary>
        /// <value><see langword="true"/> if this instance is in edit mode; otherwise, <see langword="false"/>.</value>
        bool IsInEditMode { get; }

        /// <summary>
        /// Begins the editing of an item.
        /// </summary>
        void BeginEdit();

        /// <summary>
        /// Commits the changes to the item as they are, without ending the editing.
        /// </summary>
        void CommitEdit();

        /// <summary>
        /// Discards all changes to the item, reloading the state at the last commit or at the beginning of the edit transaction, whichever occurred last.
        /// </summary>
        void CancelEdit();

        /// <summary>
        /// Ends the editing of an item.
        /// </summary>
        void EndEdit();
    }
}