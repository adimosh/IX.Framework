// <copyright file="IUndoableItem.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Undoable
{
    /// <summary>
    /// A service contract for an item that supports undo and redo operations.
    /// </summary>
    public interface IUndoableItem
    {
        /// <summary>
        /// Gets or sets the number of levels to keep undo or redo information.
        /// </summary>
        /// <remarks>
        /// <para>If this value is set, for example, to 7, then the implementing object should allow the <see cref="Undo" /> method
        /// to be called 7 times to change the state of the object. Upon calling it an 8th time, there should be no change in the
        /// state of the object.</para>
        /// <para>Any call beyond the limit imposed here should not fail, but it should also not change the state of the object.</para>
        /// </remarks>
        int HistoryLevels { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the implementer can perform an undo.
        /// </summary>
        /// <value><see langword="true"/> if the call to the <see cref="Undo"/> method would result in a state change, <see langword="false"/> otherwise.</value>
        bool CanUndo { get; }

        /// <summary>
        /// Gets a value indicating whether or not the implementer can perform a redo.
        /// </summary>
        /// <value><see langword="true"/> if the call to the <see cref="Redo"/> method would result in a state change, <see langword="false"/> otherwise.</value>
        bool CanRedo { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is captured into an undo/redo context.
        /// </summary>
        /// <value><see langword="true"/> if this instance is captured into an undo/redo context; otherwise, <see langword="false"/>.</value>
        bool IsCapturedIntoUndoContext { get; }

        /// <summary>
        /// Gets the parent undo context.
        /// </summary>
        /// <value>The parent undo context.</value>
        IUndoableItem ParentUndoContext { get; }

        /// <summary>
        /// Has the last operation performed on the implementing instance undone.
        /// </summary>
        /// <remarks>
        /// <para>If the object is captured, the method will call the capturing parent's Undo method, which can bubble down to
        /// the last instance of an undo-/redo-capable object.</para>
        /// <para>If that is the case, the capturing object is solely responsible for ensuring that the inner state of the whole
        /// system is correct. Implementing classes should not expect this method to also handle state.</para>
        /// <para>If the object is released, it is expected that this method once again starts ensuring state when called.</para>
        /// </remarks>
        void Undo();

        /// <summary>
        /// Has the last undone operation performed on the implemented instance, presuming that it has not changed, redone.
        /// </summary>
        /// <remarks>
        /// <para>If the object is captured, the method will call the capturing parent's Redo method, which can bubble down to
        /// the last instance of an undo-/redo-capable object.</para>
        /// <para>If that is the case, the capturing object is solely responsible for ensuring that the inner state of the whole
        /// system is correct. Implementing classes should not expect this method to also handle state.</para>
        /// <para>If the object is released, it is expected that this method once again starts ensuring state when called.</para>
        /// </remarks>
        void Redo();

        /// <summary>
        /// When implemented, has the state changes received undone from the object.
        /// </summary>
        /// <param name="stateChanges">The state changes to redo.</param>
        /// <remarks>
        /// <para>This method is to only be used by a capturing undo/redo context. If the implementer is not captured, an
        /// <see cref="ItemNotCapturedIntoUndoContextException"/> should be thrown.</para>
        /// </remarks>
        void UndoStateChanges(StateChange[] stateChanges);

        /// <summary>
        /// When implemented, has the state changes received redone into the object.
        /// </summary>
        /// <param name="stateChanges">The state changes to redo.</param>
        /// <remarks>
        /// <para>This method is to only be used by a capturing undo/redo context. If the implementer is not captured, an
        /// <see cref="ItemNotCapturedIntoUndoContextException"/> should be thrown.</para>
        /// </remarks>
        void RedoStateChanges(StateChange[] stateChanges);

        /// <summary>
        /// Allows the implementer to be captured by a containing undo-/redo-capable object so that undo and redo operations
        /// can be coordinated across a larger scope.
        /// </summary>
        /// <param name="parent">The parent undo and redo context.</param>
        void CaptureIntoUndoContext(IUndoableItem parent);

        /// <summary>
        /// Releases the implementer from being captured into an undo and redo context.
        /// </summary>
        void ReleaseFromUndoContext();
    }
}