// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Threading;

namespace IX.Observable
{
    /// <summary>
    /// Environment settings for observable collections.
    /// </summary>
    public static class EnvironmentSettings
    {
        /// <summary>
        /// Gets or sets the specific synchronization context.
        /// </summary>
        /// <value>The specific synchronization context.</value>
        public static SynchronizationContext SpecificSynchronizationContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to always suppress undo levels by default until the <see cref="ObservableCollectionBase{T}.StartUndo"/> method is called.
        /// </summary>
        /// <value><see langword="true"/> to always suppress undo levels by default; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// <para>The behavior of this switch is overridden if a specific (either <see langword="true"/> or <see langword="false"/>) value is given to an undoable collection in its constructor.</para>
        /// <para>If you expect to serialize / deserialize collections, please take note of this setting and, after deserialization is complete, set the <see cref="ObservableCollectionBase{T}.HistoryLevels"/>
        /// property and call the <see cref="ObservableCollectionBase{T}.StartUndo"/> method.</para>
        /// </remarks>
        public static bool AlwaysSuppressUndoLevelsByDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how deep the undo/redo stacks are, by default.
        /// </summary>
        /// <value>The undo/redo stacks depth.</value>
        public static int DefaultUndoRedoLevels { get; set; } = 50;

        /// <summary>
        /// Gets or sets a value indicating whether to globally disable undo/redo facilities for all Observable collections.
        /// </summary>
        /// <value><see langword="true"/> to disable undo/redo; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// <para>This disables undo/redo only in the types present in the <see cref="IX.Undoable"/> namespace.</para>
        /// <para>Any custom implementation or type that is not part of this namespace will not be affected by this setting.</para>
        /// </remarks>
        public static bool DisableUndoable { get; set; }
    }
}