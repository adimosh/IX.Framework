// <copyright file="Constants.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Observable
{
    /// <summary>
    /// Constants used within this project.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// The data contract namespace for this entire assembly.
        /// </summary>
        internal const string DataContractNamespace = "http://ns.ixiancorp.com/IX/IX.Observable";

        /// <summary>
        /// The concurrent lock acquisition timeout.
        /// </summary>
        internal const int ConcurrentLockAcquisitionTimeout = 100;

        /// <summary>
        /// The name &quot;Items[]&quot;.
        /// </summary>
        internal const string ItemsName = "Item[]";

        /// <summary>
        /// The standard undo/redo levels.
        /// </summary>
        internal const int StandardUndoRedoLevels = 50;
    }
}