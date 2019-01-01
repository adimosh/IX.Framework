// <copyright file="ILog.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using global::System;
using JetBrains.Annotations;

namespace IX.Abstractions.Logging
{
    /// <summary>
    /// A logging interface.
    /// </summary>
    [PublicAPI]
    public interface ILog
    {
        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        void Debug(string message);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Debug(string message, params string[] formatParameters);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Debug(string message, Exception exception, params string[] formatParameters);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        void Info(string message);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Info(string message, params string[] formatParameters);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Info(string message, Exception exception, params string[] formatParameters);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        void Warning(string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Warning(string message, params string[] formatParameters);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Warning(string message, Exception exception);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Warning(string message, Exception exception, params string[] formatParameters);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        void Error(string message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Error(string message, params string[] formatParameters);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Error(string message, Exception exception, params string[] formatParameters);

        /// <summary>
        /// Logs a critical error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        void Critical(string message);

        /// <summary>
        /// Logs a critical error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Critical(string message, params string[] formatParameters);

        /// <summary>
        /// Logs a critical error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Critical(string message, Exception exception);

        /// <summary>
        /// Logs a critical error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Critical(string message, Exception exception, params string[] formatParameters);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        void Fatal(string message);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Fatal(string message, params string[] formatParameters);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Fatal(string message, Exception exception);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="formatParameters">
        /// The string format parameters.
        /// </param>
        void Fatal(string message, Exception exception, params string[] formatParameters);
    }
}