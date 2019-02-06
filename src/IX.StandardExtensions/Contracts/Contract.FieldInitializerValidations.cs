// <copyright file="Contract.FieldInitializerValidations.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

namespace IX.StandardExtensions.Contracts
{
    /// <summary>
    /// Methods for approximating the works of contract-oriented programming.
    /// </summary>
    public static partial class Contract
    {
        /// <summary>
        /// Called when a contract requires that an argument initializing a field is not null.
        /// </summary>
        /// <typeparam name="T">The type of argument to validate.</typeparam>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNull<T>(ref T field, [CanBeNull, NoEnumeration] T argument, [NotNull] string argumentName)
            where T : class => field = argument ?? throw new ArgumentNullException(argumentName);

        /// <summary>
        /// Called when a contract requires that an argument initializing a field is not null. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">The type of argument to validate.</typeparam>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullPrivate<T>(ref T field, [CanBeNull, NoEnumeration] T argument, [NotNull] string argumentName)
            where T : class => field = argument ?? throw new ArgumentNullException(argumentName);

        /// <summary>
        /// Called when a contract requires that a string argument initializing a field is not null or empty.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrEmpty(ref string field, [CanBeNull] string argument, [NotNull] string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a string argument initializing a field is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrEmptyPrivate(ref string field, [CanBeNull] string argument, [NotNull] string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a string argument initializing a field is not null or empty.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrWhitespace(ref string field, [CanBeNull] string argument, [NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullOrWhitespaceException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a string argument initializing a field is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrWhitespacePrivate(ref string field, [CanBeNull] string argument, [NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullOrWhitespaceException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a collection argument initializing a field is not null or empty.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the collection.
        /// </typeparam>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrEmptyCollection<T>(ref ICollection<T> field, [CanBeNull, NoEnumeration] ICollection<T> argument, [NotNull] string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a collection argument initializing a field is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the collection.
        /// </typeparam>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrEmptyCollectionPrivate<T>(ref ICollection<T> field, [CanBeNull, NoEnumeration] ICollection<T> argument, [NotNull] string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a collection argument initializing a field is not null or empty.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrEmptyBinary(ref byte[] field, [CanBeNull] byte[] argument, [NotNull] string argumentName)
        {
            if ((argument?.Length ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyBinaryException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a collection argument initializing a field is not null or empty. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrEmptyBinaryPrivate(ref byte[] field, [CanBeNull] byte[] argument, [NotNull] string argumentName)
        {
            if ((argument?.Length ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyBinaryException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref sbyte field, sbyte argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref sbyte field, sbyte argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref byte field, byte argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref byte field, byte argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref short field, short argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref short field, short argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref ushort field, ushort argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref ushort field, ushort argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref int field, int argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref int field, int argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref uint field, uint argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref uint field, uint argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref long field, long argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref long field, long argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref ulong field, ulong argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref ulong field, ulong argument, [NotNull] string argumentName)
        {
            if (argument == 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref float field, float argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref float field, float argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref double field, double argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref double field, double argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref decimal field, decimal argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref decimal field, decimal argument, [NotNull] string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a time span argument initializing a field is positive.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositive(ref TimeSpan field, TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a time span argument initializing a field is positive. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresPositivePrivate(ref TimeSpan field, TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref sbyte field, sbyte argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref sbyte field, sbyte argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref short field, short argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref short field, short argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref int field, int argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref int field, int argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref long field, long argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref long field, long argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref float field, float argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref float field, float argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref double field, double argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref double field, double argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref decimal field, decimal argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a numeric argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref decimal field, decimal argument, [NotNull] string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentNotPositiveException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a time span argument initializing a field is not negative.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegative(ref TimeSpan field, TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that a time span argument initializing a field is not negative. Use this method for non-public contracts.
        /// </summary>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNonNegativePrivate(ref TimeSpan field, TimeSpan argument, [NotNull] string argumentName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentNotPositiveIntegerException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that an argument initializing a field is of a specific type.
        /// </summary>
        /// <typeparam name="T">
        /// The type to check the argument for.
        /// </typeparam>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresArgumentOfType<T>(ref T field, [CanBeNull] object argument, [NotNull] string argumentName)
        {
            if (!(argument is T convertedArgument))
            {
                throw new ArgumentInvalidTypeException(argumentName);
            }

            field = convertedArgument;
        }

        /// <summary>
        /// Called when a contract requires that an argument initializing a field is of a specific type. Use this method for non-public contracts.
        /// </summary>
        /// <typeparam name="T">
        /// The type to check the argument for.
        /// </typeparam>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresArgumentOfTypePrivate<T>(ref T field, [CanBeNull] object argument, [NotNull] string argumentName)
        {
            if (!(argument is T convertedArgument))
            {
                throw new ArgumentInvalidTypeException(argumentName);
            }

            field = convertedArgument;
        }
    }
}