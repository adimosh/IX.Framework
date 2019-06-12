// <copyright file="Contract.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using IX.StandardExtensions.ComponentModel;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Contracts
{
    /// <summary>
    ///     Methods for approximating the works of contract-oriented programming.
    /// </summary>
    [PublicAPI]
    public static partial class Contract
    {
#pragma warning disable EPS02 // Non-readonly struct used as in-parameter - These are primitive types that the compiler can handle
        /// <summary>
        ///     Called when a contract requires that an argument is not null.
        /// </summary>
        /// <typeparam name="T">The type of argument to validate.</typeparam>
        /// <param name="argument">
        ///     The argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The argument is <see langword="null" />.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNull<T>(
            [CanBeNull] [NoEnumeration] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            in T argument,
            [NotNull] string argumentName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that an argument is not null. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">The type of argument to validate.</typeparam>
        /// <param name="argument">
        ///     The argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The argument is <see langword="null" />.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullPrivate<T>(
            [CanBeNull] [NoEnumeration] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            in T argument,
            [NotNull] string argumentName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a string argument is not null or empty.
        /// </summary>
        /// <param name="argument">
        ///     The string argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            string argument,
            [NotNull] string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a string argument is not null or empty. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <param name="argument">
        ///     The string argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmptyPrivate(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            string argument,
            [NotNull] string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a string argument is not null or empty.
        /// </summary>
        /// <param name="argument">
        ///     The string argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrWhitespaceException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrWhitespace(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            string argument,
            [NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullOrWhitespaceException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a string argument is not null or empty. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <param name="argument">
        ///     The string argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrWhitespaceException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrWhitespacePrivate(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            string argument,
            [NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullOrWhitespaceException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a collection argument is not null or empty.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the collection.
        /// </typeparam>
        /// <param name="argument">
        ///     The collection argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyCollectionException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty<T>(
            [CanBeNull] [NoEnumeration] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            ICollection<T> argument,
            [NotNull] string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a collection argument is not null or empty. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the collection.
        /// </typeparam>
        /// <param name="argument">
        ///     The collection argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyCollectionException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmptyPrivate<T>(
            [CanBeNull] [NoEnumeration] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            ICollection<T> argument,
            [NotNull] string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that an array argument is not null or empty.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the array.
        /// </typeparam>
        /// <param name="argument">
        ///     The array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyBinaryException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            T[] argument,
            [NotNull] string argumentName)
        {
            if ((argument?.Length ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyBinaryException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that an array argument is not null or empty. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the array.
        /// </typeparam>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyBinaryException">
        ///     The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmptyPrivate<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            T[] argument,
            [NotNull] string argumentName)
        {
            if ((argument?.Length ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyBinaryException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            sbyte argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in sbyte argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in byte argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in byte argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in short argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in short argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in ushort argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in ushort argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in int argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in int argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in uint argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in uint argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in long argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in long argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in ulong argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in ulong argument,
            [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in float argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in float argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in double argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in double argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in decimal argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in decimal argument,
            [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(
            in TimeSpan argument,
            [NotNull] string argumentName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(
            in TimeSpan argument,
            [NotNull] string argumentName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>Called when a contract requires that aspecific index is valid for an array.</summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the index.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayIndex<T>(
            in int argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument < 0 || argument >= array.Length)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that aspecific index is valid for an array. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the index.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayIndexPrivate<T>(
            in int argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument < 0 || argument >= array.Length)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }
        }

#if !STANDARD
        /// <summary>Called when a contract requires that aspecific index is valid for an array.</summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the index.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayIndex<T>(
            in long argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument < 0 || argument >= array.LongLength)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that aspecific index is valid for an array. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the index.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayIndexPrivate<T>(
            in long argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument < 0 || argument >= array.LongLength)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }
        }
#endif

        /// <summary>Called when a contract requires that aspecific index and length, constituting a range, is valid for an array.</summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="indexArgument">The numeric index argument to validate.</param>
        /// <param name="lengthArgument">The numeric length argument to validate.</param>
        /// <param name="array">The array for which we are validating the range.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayRange<T>(
            in int indexArgument,
            in int lengthArgument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (indexArgument < 0 || indexArgument >= array.Length)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }

            if (lengthArgument <= 0 || indexArgument + lengthArgument > array.Length)
            {
                throw new ArgumentsNotValidRangeException(
                    nameof(indexArgument),
                    nameof(lengthArgument));
            }
        }

        /// <summary>
        ///     Called when a contract requires that aspecific index and length, constituting a range, is valid for an array.
        ///     Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="indexArgument">The numeric index argument to validate.</param>
        /// <param name="lengthArgument">The numeric length argument to validate.</param>
        /// <param name="array">The array for which we are validating the range.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayRangePrivate<T>(
            in int indexArgument,
            in int lengthArgument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (indexArgument < 0 || indexArgument >= array.Length)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }

            if (lengthArgument <= 0 || indexArgument + lengthArgument > array.Length)
            {
                throw new ArgumentsNotValidRangeException(
                    nameof(indexArgument),
                    nameof(lengthArgument));
            }
        }

#if !STANDARD
        /// <summary>Called when a contract requires that aspecific index and length, constituting a range, is valid for an array.</summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="indexArgument">The numeric index argument to validate.</param>
        /// <param name="lengthArgument">The numeric length argument to validate.</param>
        /// <param name="array">The array for which we are validating the range.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayRange<T>(
            in long indexArgument,
            in long lengthArgument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (indexArgument < 0 || indexArgument >= array.LongLength)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }

            if (lengthArgument <= 0 || indexArgument + lengthArgument > array.LongLength)
            {
                throw new ArgumentsNotValidRangeException(
                    nameof(indexArgument),
                    nameof(lengthArgument));
            }
        }

        /// <summary>
        ///     Called when a contract requires that aspecific index and length, constituting a range, is valid for an array.
        ///     Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="indexArgument">The numeric index argument to validate.</param>
        /// <param name="lengthArgument">The numeric length argument to validate.</param>
        /// <param name="array">The array for which we are validating the range.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayRangePrivate<T>(
            in long indexArgument,
            in long lengthArgument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (indexArgument < 0 || indexArgument >= array.LongLength)
            {
                throw new ArgumentNotValidIndexException(argumentName);
            }

            if (lengthArgument <= 0 || indexArgument + lengthArgument > array.LongLength)
            {
                throw new ArgumentsNotValidRangeException(
                    nameof(indexArgument),
                    nameof(lengthArgument));
            }
        }
#endif

        /// <summary>Called when a contract requires that aspecific length is valid for an array.</summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the length.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayLength<T>(
            in int argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument <= 0 || argument > array.Length)
            {
                throw new ArgumentNotValidLengthException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that aspecific length is valid for an array. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the length.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayLengthPrivate<T>(
            in int argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument <= 0 || argument > array.Length)
            {
                throw new ArgumentNotValidLengthException(argumentName);
            }
        }

#if !STANDARD
        /// <summary>Called when a contract requires that aspecific length is valid for an array.</summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the length.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayLength<T>(
            in long argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument <= 0 || argument > array.LongLength)
            {
                throw new ArgumentNotValidLengthException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that aspecific length is valid for an array. Use this method for non-public
        ///     contracts.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="argument">The numeric argument to validate.</param>
        /// <param name="array">The array for which we are validating the length.</param>
        /// <param name="argumentName">The argument name.</param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is either negative or exceeds the bounds of the
        ///     array.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresValidArrayLengthPrivate<T>(
            in long argument,
            in T[] array,
            [NotNull] string argumentName)
        {
            if (argument <= 0 || argument > array.LongLength)
            {
                throw new ArgumentNotValidLengthException(argumentName);
            }
        }
#endif

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in sbyte argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in sbyte argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in short argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in short argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in int argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in int argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in long argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in long argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in float argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in float argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in double argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in double argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in decimal argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        ///     The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in decimal argument,
            [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        ///     The numeric argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(
            in TimeSpan argument,
            [NotNull] string argumentName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        ///     The byte array argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        ///     The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(
            in TimeSpan argument,
            [NotNull] string argumentName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="condition">
        ///     The condition that should be evaluated.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     The condition is not being met.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [AssertionMethod]
        public static void Requires(
            [AssertionCondition(AssertionConditionType.IS_TRUE)]
            bool condition,
            [NotNull] string argumentName)
        {
            if (!condition)
            {
                throw new ArgumentException(
                    Resources.AContractConditionIsNotBeingMet,
                    argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="condition">
        ///     The condition that should be evaluated.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     The condition is not being met.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [AssertionMethod]
        public static void RequiresPrivate(
            [AssertionCondition(AssertionConditionType.IS_TRUE)]
            bool condition,
            [NotNull] string argumentName)
        {
            if (!condition)
            {
                throw new ArgumentException(
                    Resources.AContractConditionIsNotBeingMet,
                    argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that an argument is of a specific type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type to check the argument for.
        /// </typeparam>
        /// <param name="argument">
        ///     The argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentInvalidTypeException">
        ///     The condition is not being met.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresArgumentOfType<T>(
            [CanBeNull] object argument,
            [NotNull] string argumentName)
        {
            if (!(argument is T))
            {
                throw new ArgumentInvalidTypeException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that an argument is of a specific type. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">
        ///     The type to check the argument for.
        /// </typeparam>
        /// <param name="argument">
        ///     The argument.
        /// </param>
        /// <param name="argumentName">
        ///     The argument name.
        /// </param>
        /// <exception cref="ArgumentInvalidTypeException">
        ///     The condition is not being met.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresArgumentOfTypePrivate<T>(
            [CanBeNull] object argument,
            [NotNull] string argumentName)
        {
            if (!(argument is T))
            {
                throw new ArgumentInvalidTypeException(argumentName);
            }
        }

        /// <summary>
        ///     Called when a contract requires that an argument is not disposed.
        /// </summary>
        /// <param name="reference">
        ///     The object reference to check for disposed.
        /// </param>
        /// <exception cref="ObjectDisposedException">If the reference object is disposed, this exception will be thrown.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotDisposed([NotNull] this DisposableBase reference) =>
            reference.ThrowIfCurrentObjectDisposed();

        /// <summary>
        ///     Called when a contract requires that an argument is not disposed. Use this method for non-public contracts.
        /// </summary>
        /// <param name="reference">
        ///     The object reference to check for disposed.
        /// </param>
        /// <exception cref="ObjectDisposedException">If the reference object is disposed, this exception will be thrown.</exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotDisposedPrivate([NotNull] this DisposableBase reference) =>
            reference.ThrowIfCurrentObjectDisposed();
#pragma warning restore EPS02 // Non-readonly struct used as in-parameter
    }
}