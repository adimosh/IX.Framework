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
        /// <value><c>true</c> to always suppress undo levels by default; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// <para>The behavior of this switch is overridden if a specific (either <c>true</c> or <c>false</c>) value is given to an undoable collection in its constructor.</para>
        /// <para>If you expect to serialize / deserialize </para>
        /// </remarks>
        public static bool AlwaysSuppressUndoLevelsByDefault { get; set; }
    }
}