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
        [ContractAnnotation("argument:null => halt")]
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
        [ContractAnnotation("argument:null => halt")]
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
        /// <exception cref="ArgumentNullOrEmptyException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
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
        /// <exception cref="ArgumentNullOrEmptyException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
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
        /// <exception cref="ArgumentNullOrWhitespaceException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
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
        /// <exception cref="ArgumentNullOrWhitespaceException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullOrWhitespacePrivate(string argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullOrEmptyCollection<T>([CanBeNull]ICollection<T> argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullOrEmptyCollectionPrivate<T>([CanBeNull]ICollection<T> argument, string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a collection argument is not null or empty.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyBinaryException">
        /// The argument is <see langword="null"/> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullOrEmptyBinary([CanBeNull] byte[] argument, string argumentName)
        {
            if ((argument?.Length ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyBinaryException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a collection argument is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <param name="argument">
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullOrEmptyBinaryException">
        /// The argument is <see langword="null" /> or empty.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullOrEmptyBinaryPrivate([CanBeNull] byte[] argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(sbyte argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(sbyte argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(byte argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(byte argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(short argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(short argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(ushort argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(ushort argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(int argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(int argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(uint argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(uint argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(long argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(long argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(ulong argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveIntegerException">
        /// The argument is 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(ulong argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(float argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(float argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(double argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(double argument, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumber(decimal argument, string argumentName)
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
        /// The byte array argument.
        /// </param>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        /// <exception cref="ArgumentNotPositiveException">
        /// The argument is negative or 0.
        /// </exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(decimal argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Requires(bool condition, string argumentName)
        {
            if (!condition)
            {
                throw new ArgumentException(Resources.AContractConditionIsNotBeingMet, argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument is positive.
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPrivate(bool condition, string argumentName)
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresArgumentOfType<T>(object argument, string argumentName)
        {
            if (argument is T)
            {
                throw new ArgumentInvalidTypeException(argumentName);
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
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresArgumentOfTypePrivate<T>(object argument, string argumentName)
        {
            if (argument is T)
            {
                throw new ArgumentInvalidTypeException(argumentName);
            }
        }

        /// <summary>
        /// Called when a contract requires that an argument is of a specific type.
        /// </summary>
        /// <param name="reference">
        /// The object reference to check for disposed.
        /// </param>
        /// <exception cref="ObjectDisposedException">If the reference object is disposed, this exception will be thrown.</exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotDisposed([NotNull] this DisposableBase reference) => reference.ThrowIfCurrentObjectDisposed();

        /// <summary>
        /// Called when a contract requires that an argument is of a specific type.
        /// </summary>
        /// <param name="reference">
        /// The object reference to check for disposed.
        /// </param>
        /// <exception cref="ObjectDisposedException">If the reference object is disposed, this exception will be thrown.</exception>
        [Conditional(Constants.ContractsAllSymbol)]
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotDisposedPrivate([NotNull] this DisposableBase reference) => reference.ThrowIfCurrentObjectDisposed();
    }
}