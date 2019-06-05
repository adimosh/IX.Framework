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
#pragma warning disable EPS02 // Non-readonly struct used as in-parameter - These are primitive types that the compiler can handle
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
        [AssertionMethod]
        public static void RequiresNotNull<T>(ref T field, [CanBeNull, NoEnumeration, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T argument, [NotNull] string argumentName)
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
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty(ref string field, [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, [NotNull] string argumentName)
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
        [AssertionMethod]
        public static void RequiresNotNullOrWhitespace(ref string field, [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, [NotNull] string argumentName)
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
        [AssertionMethod]
        public static void RequiresNotNullOrEmpty<T>(ref ICollection<T> field, [CanBeNull, NoEnumeration, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection<T> argument, [NotNull] string argumentName)
        {
            if ((argument?.Count ?? 0) == 0)
            {
                throw new ArgumentNullOrEmptyCollectionException(argumentName);
            }

            field = argument;
        }

        /// <summary>
        /// Called when a contract requires that an array argument initializing a field is not null or empty.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the collection.
        /// </typeparam>
        /// <param name="field">
        /// The field that this argument is initializing.
        /// </param>
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
        public static void RequiresNotNullOrEmpty<T>(ref T[] field, [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T[] argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref sbyte field, in sbyte argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref byte field, in byte argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref short field, in short argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref ushort field, in ushort argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref int field, in int argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref uint field, in uint argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref long field, in long argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref ulong field, in ulong argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref float field, in float argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref double field, in double argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref decimal field, in decimal argument, [NotNull] string argumentName)
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
        public static void RequiresPositive(ref TimeSpan field, in TimeSpan argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref sbyte field, in sbyte argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref short field, in short argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref int field, in int argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref long field, in long argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref float field, in float argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref double field, in double argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref decimal field, in decimal argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegativePrivate(ref decimal field, in decimal argument, [NotNull] string argumentName)
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
        public static void RequiresNonNegative(ref TimeSpan field, in TimeSpan argument, [NotNull] string argumentName)
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
#pragma warning restore EPS02 // Non-readonly struct used as in-parameter
    }
}