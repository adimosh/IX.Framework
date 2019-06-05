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
    /// Methods for approximating the works of contract-oriented programming.
    /// </summary>
    [PublicAPI]
    public static partial class Contract
    {
        /// <summary>
        /// Called when a contract requires that an argument is not null.
        /// </summary>
        /// <typeparam name="T">The type of argument to validate.</typeparam>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" />.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNull<T>([CanBeNull, NoEnumeration, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T argument, [NotNull] string argumentName)
            where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an argument is not null. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">The type of argument to validate.</typeparam>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is <see langword="null" />.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullPrivate<T>([CanBeNull, NoEnumeration, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T argument, [NotNull] string argumentName)
            where T : class
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
        /// <exception cref="ArgumentNullOrEmptyException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, [NotNull] string argumentName)
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
        /// <exception cref="ArgumentNullOrEmptyException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmptyPrivate([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, [NotNull] string argumentName)
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
        /// <exception cref="ArgumentNullOrWhitespaceException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrWhitespace([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, [NotNull] string argumentName)
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
        /// <exception cref="ArgumentNullOrWhitespaceException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrWhitespacePrivate([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, [NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullOrWhitespaceException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a collection argument is not null or empty.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the collection.
        /// </typeparam>
        /// <param name="argument">
        /// The collection argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyCollectionException">
        /// The argument is <see langword="null"/> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty<T>([CanBeNull, NoEnumeration, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection<T> argument, [NotNull] string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a collection argument is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the collection.
        /// </typeparam>
        /// <param name="argument">
        /// The collection argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyCollectionException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmptyPrivate<T>([CanBeNull, NoEnumeration, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection<T> argument, [NotNull] string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an array argument is not null or empty.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the array.
        /// </typeparam>
        /// <param name="argument">
        /// The array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyBinaryException">
        /// The argument is <see langword="null"/> or empty.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty<T>([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T[] argument, [NotNull] string argumentName)
        {
            if ((argument?.Length ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyBinaryException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an array argument is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the array.
        /// </typeparam>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyBinaryException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        [AssertionMethod]
        public static void RequiresNotNullOrEmptyPrivate<T>([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T[] argument, [NotNull] string argumentName)
        {
            if ((argument?.Length ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyBinaryException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(sbyte argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(sbyte argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(byte argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(byte argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(short argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(short argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(ushort argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(ushort argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(int argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(int argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(uint argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(uint argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(long argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(long argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(ulong argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(ulong argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(float argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(float argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(double argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(double argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(decimal argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(decimal argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositive(TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositivePrivate(TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(sbyte argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(sbyte argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(short argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(short argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(int argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(int argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(long argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(long argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(float argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(float argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(double argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(double argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(decimal argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(decimal argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="argument">
        /// The numeric argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegative(TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNonNegativePrivate(TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative.
        /// </summary>
        /// <param name="condition">
        /// The condition that should be evaluated.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The condition is not being met.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [AssertionMethod]
        public static void Requires([AssertionCondition(AssertionConditionType.IS_TRUE)] bool condition, [NotNull] string argumentName)
        {
            if (!condition)
            {
                throw new ArgumentException(Resources.AContractConditionIsNotBeingMet, argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="condition">
        /// The condition that should be evaluated.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The condition is not being met.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [AssertionMethod]
        public static void RequiresPrivate([AssertionCondition(AssertionConditionType.IS_TRUE)] bool condition, [NotNull] string argumentName)
        {
            if (!condition)
            {
                throw new ArgumentException(Resources.AContractConditionIsNotBeingMet, argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an argument is of a specific type.
        /// </summary>
        /// <typeparam name="T">
        /// The type to check the argument for.
        /// </typeparam>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentInvalidTypeException">
        /// The condition is not being met.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresArgumentOfType<T>([CanBeNull] object argument, [NotNull] string argumentName)
        {
            if (!(argument is T))
            {
                throw new ArgumentInvalidTypeException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an argument is of a specific type. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">
        /// The type to check the argument for.
        /// </typeparam>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentInvalidTypeException">
        /// The condition is not being met.
        /// </exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresArgumentOfTypePrivate<T>([CanBeNull] object argument, [NotNull] string argumentName)
        {
            if (!(argument is T))
            {
                throw new ArgumentInvalidTypeException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an argument is not disposed.
        /// </summary>
        /// <param name="reference">
        /// The object reference to check for disposed.
        /// </param>
        /// <exception cref="ObjectDisposedException">If the reference object is disposed, this exception will be thrown.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotDisposed([NotNull] this DisposableBase reference) => reference.ThrowIfCurrentObjectDisposed();

        /// <summary>
        /// Called when a contract requires that an argument is not disposed. Use this method for non-public contracts.
        /// </summary>
        /// <param name="reference">
        /// The object reference to check for disposed.
        /// </param>
        /// <exception cref="ObjectDisposedException">If the reference object is disposed, this exception will be thrown.</exception>
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotDisposedPrivate([NotNull] this DisposableBase reference) => reference.ThrowIfCurrentObjectDisposed();
    }
}