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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNull([CanBeNull] object argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullPrivate([CanBeNull] object argument, [NotNull] string argumentName)
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
        public static void RequiresNotNullOrEmpty([CanBeNull] string argument, [NotNull] string argumentName)
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
        public static void RequiresNotNullOrEmptyPrivate([CanBeNull] string argument, [NotNull] string argumentName)
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
        public static void RequiresNotNullOrWhitespace([CanBeNull] string argument, [NotNull] string argumentName)
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
        public static void RequiresNotNullOrWhitespacePrivate([CanBeNull] string argument, [NotNull] string argumentName)
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
        public static void RequiresNotNullOrEmptyCollection<T>([CanBeNull] ICollection<T> argument, [NotNull] string argumentName)
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
        public static void RequiresNotNullOrEmptyCollectionPrivate<T>([CanBeNull] ICollection<T> argument, [NotNull] string argumentName)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullOrEmptyBinary([CanBeNull] byte[] argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("argument:null => halt")]
        public static void RequiresNotNullOrEmptyBinaryPrivate([CanBeNull] byte[] argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(sbyte argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(sbyte argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(byte argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(byte argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(short argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(short argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(ushort argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(ushort argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(int argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(int argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(uint argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(uint argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(long argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(long argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(ulong argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(ulong argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(float argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(float argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(double argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(double argument, [NotNull] string argumentName)
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
        public static void RequiresPositiveNumber(decimal argument, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPositiveNumberPrivate(decimal argument, [NotNull] string argumentName)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Requires(bool condition, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresPrivate(bool condition, [NotNull] string argumentName)
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
        [Conditional(Constants.ContractsNonPublicSymbol)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresArgumentOfTypePrivate<T>([CanBeNull] object argument, [NotNull] string argumentName)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresNotDisposed([NotNull] this DisposableBase reference) => reference.ThrowIfCurrentObjectDisposed();

        /// <summary>
        /// Called when a contract requires that an argument is of a specific type.
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