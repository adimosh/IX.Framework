// <copyright file="Contract.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IX.StandardExtensions.Contracts
{
    /// <summary>
    /// Methods for approximating the works of contract-oriented programming.
    /// </summary>
    public static class Contract
    {
        /// <summary>
        /// Called when a contract requires that an argument is not null.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" />.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an argument is not null. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" />.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotNullPrivate(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a string argument is not null or empty.
        /// </summary>
        /// <param name="argument">
        /// The string argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a string argument is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The string argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotNullOrEmptyPrivate(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a string argument is not null or empty.
        /// </summary>
        /// <param name="argument">
        /// The string argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotNullOrWhitespace(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullOrWhitespaceException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a string argument is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The string argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotNullOrWhitespacePrivate(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullOrWhitespaceException(argumentName);
            }
        }
    }
}