// <copyright file="Constants.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using JetBrains.Annotations;

namespace IX.Abstractions.Logging
{
    /// <summary>
    /// Generally-used constants.
    /// </summary>
    [PublicAPI]
    public class Constants
    {
        /// <summary>
        /// The logging symbol for all logging.
        /// </summary>
        public const string LoggingSymbolAll = "IXLOGGINGAll";

        /// <summary>
        /// The logging symbol for debug logging.
        /// </summary>
        public const string LoggingSymbolDebug = "IXLOGGINGDEBUG";

        /// <summary>
        /// The logging symbol for info logging.
        /// </summary>
        public const string LoggingSymbolInfo = "IXLOGGINGINFO";

        /// <summary>
        /// The logging symbol for warning logging.
        /// </summary>
        public const string LoggingSymbolWarning = "IXLOGGINGWARNING";

        /// <summary>
        /// The logging symbol for error logging.
        /// </summary>
        public const string LoggingSymbolError = "IXLOGGINGERROR";

        /// <summary>
        /// The logging symbol for critical error logging.
        /// </summary>
        public const string LoggingSymbolCritical = "IXLOGGINGCRITICAL";

        /// <summary>
        /// The logging symbol for fatal error logging.
        /// </summary>
        public const string LoggingSymbolFatal = "IXLOGGINGFATAL";
    }
}