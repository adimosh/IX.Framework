// <copyright file="Constants.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Abstractions.Collections
{
    internal static class Constants
    {
        /// <summary>
        /// The data contract namespace for this entire assembly.
        /// </summary>
        internal const string DataContractNamespace = "http://ns.ixiancorp.com/IX/IX.Abstractions.Collections";

        /// <summary>
        /// The concurrent lock acquisition timeout.
        /// </summary>
        internal const int ConcurrentLockAcquisitionTimeout = 100;

        /// <summary>
        /// The default limit for push-down collections.
        /// </summary>
        internal const int DefaultPushDownLimit = 5;
    }
}