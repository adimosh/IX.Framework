// <copyright file="ArrayExtensions.StaticMethods.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    ///     Extensions for array types.
    /// </summary>
    public static partial class ArrayExtensions
    {
#if !STANDARD
        /// <summary>
        ///     Converts all elements of an array into elements of a different type.
        /// </summary>
        /// <typeparam name="TInput">The type of the input array items.</typeparam>
        /// <typeparam name="TOutput">The type of the output array items.</typeparam>
        /// <param name="array">The array to convert.</param>
        /// <param name="converter">The individual items converter.</param>
        /// <returns>The converted array.</returns>
        public static TOutput[] ConvertAll<TInput, TOutput>(
            this TInput[] array,
            Converter<TInput, TOutput> converter) => Array.ConvertAll(
            array,
            converter);
#endif

        /// <summary>
        ///     Searches a range of elements in a one-dimensional sorted array for a value, using
        ///     the specified <see cref="IComparer{T}" /> generic interface.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or- null to use
        ///     the <see cref="IComparable{T}" /> implementation of each element.
        /// </param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch<T>(
            this T[] array,
            int index,
            int length,
            T value,
            IComparer<T> comparer) => Array.BinarySearch(
            array,
            index,
            length,
            value,
            comparer);

        /// <summary>
        ///     Searches a range of elements in a one-dimensional sorted array for a value, using
        ///     the <see cref="IComparable" /> generic interface implemented by each element of the
        ///     <see cref="Array" /> and by the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch<T>(
            this T[] array,
            int index,
            int length,
            T value) => Array.BinarySearch(
            array,
            index,
            length,
            value);

        /// <summary>
        ///     Searches an entire one-dimensional sorted array for a value using the specified <see cref="IComparer{T}" /> generic
        ///     interface.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or- null to use
        ///     the <see cref="IComparable{T}" /> implementation of each element.
        /// </param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch<T>(
            this T[] array,
            T value,
            IComparer<T> comparer) => Array.BinarySearch(
            array,
            value,
            comparer);

        /// <summary>
        ///     Searches a range of elements in a one-dimensional sorted array for a value, using
        ///     the <see cref="IComparable" /> interface implemented by each element of the array and
        ///     by the specified value.
        /// </summary>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch(
            this Array array,
            int index,
            int length,
            object value) => Array.BinarySearch(
            array,
            index,
            length,
            value);

        /// <summary>
        ///     Searches an entire one-dimensional sorted array for a value using the specified
        ///     <see cref="IComparer" /> interface.
        /// </summary>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer" /> implementation to use when comparing elements.-or- null to use the
        ///     <see cref="IComparable" /> implementation of each element.
        /// </param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch(
            this Array array,
            object value,
            IComparer comparer) => Array.BinarySearch(
            array,
            value,
            comparer);

        /// <summary>
        ///     Searches an entire one-dimensional sorted array for a specific element, using
        ///     the <see cref="IComparable" /> interface implemented by each element of the array and
        ///     by the specified object.
        /// </summary>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch(
            this Array array,
            object value) => Array.BinarySearch(
            array,
            value);

        /// <summary>
        ///     Searches a range of elements in a one-dimensional sorted array for a value, using
        ///     the specified <see cref="IComparer" /> interface.
        /// </summary>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer" /> implementation to use when comparing elements.-or- null to use the
        ///     <see cref="IComparable" /> implementation of each element.
        /// </param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch(
            this Array array,
            int index,
            int length,
            object value,
            IComparer comparer) => Array.BinarySearch(
            array,
            index,
            length,
            value,
            comparer);

        /// <summary>
        ///     Searches an entire one-dimensional sorted array for a specific element, using
        ///     the <see cref="IComparable{T}" /> generic interface implemented by each element of the
        ///     <see cref="Array" /> and by the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <returns>
        ///     The index of the specified value in the specified array, if value is found. If
        ///     value is not found and value is less than one or more elements in array, a negative
        ///     number which is the bitwise complement of the index of the first element that
        ///     is larger than value. If value is not found and value is greater than any of
        ///     the elements in array, a negative number which is the bitwise complement of (the
        ///     index of the last element plus 1).
        /// </returns>
        public static int BinarySearch<T>(
            this T[] array,
            T value) => Array.BinarySearch(
            array,
            value);

        /// <summary>
        ///     Sets a range of elements in an array to the default value of each element type.
        /// </summary>
        /// <param name="array">The array whose elements need to be cleared.</param>
        /// <param name="index">The starting index of the range of elements to clear.</param>
        /// <param name="length">The number of elements to clear.</param>
        public static void Clear(
            this Array array,
            int index,
            int length) => Array.Clear(
            array,
            index,
            length);

        /// <summary>
        ///     Copies a range of elements from an <see cref="Array" /> starting at the specified source
        ///     index and pastes them to another <see cref="Array" /> starting at the specified destination
        ///     index. Guarantees that all changes are undone if the copy does not succeed completely.
        /// </summary>
        /// <param name="sourceArray">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="sourceIndex">
        ///     A 32-bit integer that represents the index in the <paramref name="sourceArray" /> at which
        ///     copying begins.
        /// </param>
        /// <param name="destinationArray">The <see cref="Array" /> that receives the data.</param>
        /// <param name="destinationIndex">
        ///     A 32-bit integer that represents the index in the <paramref name="destinationArray" />
        ///     at which storing begins.
        /// </param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        public static void ConstrainedCopy(
            this Array sourceArray,
            int sourceIndex,
            Array destinationArray,
            int destinationIndex,
            int length) => Array.ConstrainedCopy(
            sourceArray,
            sourceIndex,
            destinationArray,
            destinationIndex,
            length);

        /// <summary>
        ///     Copies a range of elements from an <see cref="Array" /> starting at the first element
        ///     and pastes them into another <see cref="Array" /> starting at the first element. The
        ///     length is specified as a 32-bit integer.
        /// </summary>
        /// <param name="sourceArray">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="destinationArray">The <see cref="Array" /> that receives the data.</param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        public static void Copy(
            this Array sourceArray,
            Array destinationArray,
            int length) => Array.Copy(
            sourceArray,
            destinationArray,
            length);

        /// <summary>
        ///     Copies a range of elements from an <see cref="Array" /> starting at the specified source
        ///     index and pastes them to another <see cref="Array" /> starting at the specified destination
        ///     index. The length and the indexes are specified as 32-bit integers.
        /// </summary>
        /// <param name="sourceArray">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="sourceIndex">
        ///     A 32-bit integer that represents the index in the <paramref name="sourceArray" /> at which
        ///     copying begins.
        /// </param>
        /// <param name="destinationArray">The <see cref="Array" /> that receives the data.</param>
        /// <param name="destinationIndex">
        ///     A 32-bit integer that represents the index in the <paramref name="destinationArray" />
        ///     at which storing begins.
        /// </param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        public static void Copy(
            this Array sourceArray,
            int sourceIndex,
            Array destinationArray,
            int destinationIndex,
            int length) => Array.Copy(
            sourceArray,
            sourceIndex,
            destinationArray,
            destinationIndex,
            length);

        /// <summary>
        ///     Determines whether the specified array contains elements that match the conditions
        ///     defined by the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     <see langword="true" /> if array contains one or more elements that match the conditions defined
        ///     by the specified predicate; otherwise, <see langword="false" />.
        /// </returns>
        public static bool Exists<T>(
            this T[] array,
            Predicate<T> match) => Array.Exists(
            array,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the first occurrence within the entire <see cref="Array" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The first element that matches the conditions defined by the specified predicate,
        ///     if found; otherwise, the default value for type <typeparamref name="T" />.
        /// </returns>
        public static T Find<T>(
            this T[] array,
            Predicate<T> match) => Array.Find(
            array,
            match);

        /// <summary>
        ///     Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     An <see cref="Array" /> containing all the elements that match the conditions defined
        ///     by the specified predicate, if found; otherwise, an empty <see cref="Array" />.
        /// </returns>
        public static T[] FindAll<T>(
            this T[] array,
            Predicate<T> match) => Array.FindAll(
            array,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the zero-based index of the first occurrence within the
        ///     range of elements in the <see cref="Array" /> that starts at the specified index and
        ///     contains the specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The zero-based index of the first occurrence of an element that matches the conditions
        ///     defined by match, if found; otherwise, –1.
        /// </returns>
        public static int FindIndex<T>(
            this T[] array,
            int startIndex,
            int count,
            Predicate<T> match) => Array.FindIndex(
            array,
            startIndex,
            count,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the zero-based index of the first occurrence within the
        ///     range of elements in the <see cref="Array" /> that extends from the specified index to
        ///     the last element.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The zero-based index of the first occurrence of an element that matches the conditions
        ///     defined by match, if found; otherwise, –1.
        /// </returns>
        public static int FindIndex<T>(
            this T[] array,
            int startIndex,
            Predicate<T> match) => Array.FindIndex(
            array,
            startIndex,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the zero-based index of the first occurrence within the
        ///     entire <see cref="Array" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The zero-based index of the first occurrence of an element that matches the conditions
        ///     defined by match, if found; otherwise, –1.
        /// </returns>
        public static int FindIndex<T>(
            this T[] array,
            Predicate<T> match) => Array.FindIndex(
            array,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the last occurrence within the entire <see cref="Array" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The last element that matches the conditions defined by the specified predicate,
        ///     if found; otherwise, the default value for type <typeparamref name="T" />.
        /// </returns>
        public static T FindLast<T>(
            this T[] array,
            Predicate<T> match) => Array.FindLast(
            array,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the zero-based index of the last occurrence within the
        ///     range of elements in the <see cref="Array" /> that contains the specified number of elements
        ///     and ends at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The zero-based index of the last occurrence of an element that matches the conditions
        ///     defined by match, if found; otherwise, –1.
        /// </returns>
        public static int FindLastIndex<T>(
            this T[] array,
            int startIndex,
            int count,
            Predicate<T> match) => Array.FindLastIndex(
            array,
            startIndex,
            count,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the zero-based index of the last occurrence within the
        ///     range of elements in the <see cref="Array" /> that extends from the first element to
        ///     the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The zero-based index of the last occurrence of an element that matches the conditions
        ///     defined by match, if found; otherwise, –1.
        /// </returns>
        public static int FindLastIndex<T>(
            this T[] array,
            int startIndex,
            Predicate<T> match) => Array.FindLastIndex(
            array,
            startIndex,
            match);

        /// <summary>
        ///     Searches for an element that matches the conditions defined by the specified
        ///     predicate, and returns the zero-based index of the last occurrence within the
        ///     entire <see cref="Array" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <returns>
        ///     The zero-based index of the last occurrence of an element that matches the conditions
        ///     defined by match, if found; otherwise, –1.
        /// </returns>
        public static int FindLastIndex<T>(
            this T[] array,
            Predicate<T> match) => Array.FindLastIndex(
            array,
            match);

        /// <summary>
        ///     Searches for the specified object and returns the index of its first occurrence
        ///     in a one-dimensional array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <returns>The zero-based index of the first occurrence of value in the entire array, if found; otherwise, –1.</returns>
        public static int IndexOf<T>(
            this T[] array,
            T value) => Array.IndexOf(
            array,
            value);

        /// <summary>
        ///     Searches for the specified object in a range of elements of a one-dimensional
        ///     array, and returns the index of ifs first occurrence. The range extends from
        ///     a specified index for a specified number of elements.
        /// </summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
        /// <param name="count">The number of elements to search.</param>
        /// <returns>
        ///     The index of the first occurrence of value, if it’s found, in the array from
        ///     index startIndex to startIndex + count - 1; otherwise, the lower bound of the
        ///     array minus 1.
        /// </returns>
        public static int IndexOf(
            this Array array,
            object value,
            int startIndex,
            int count) => Array.IndexOf(
            array,
            value,
            startIndex,
            count);

        /// <summary>
        ///     Searches for the specified object in a range of elements of a one-dimensional
        ///     array, and returns the index of its first occurrence. The range extends from
        ///     a specified index for a specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
        /// <param name="count">The number of elements to search.</param>
        /// <returns>
        ///     The zero-based index of the first occurrence of value within the range of elements
        ///     in array that starts at startIndex and contains the number of elements specified
        ///     in count, if found; otherwise, –1.
        /// </returns>
        public static int IndexOf<T>(
            this T[] array,
            T value,
            int startIndex,
            int count) => Array.IndexOf(
            array,
            value,
            startIndex,
            count);

        /// <summary>
        ///     Searches for the specified object and returns the index of its first occurrence
        ///     in a one-dimensional array.
        /// </summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <returns>
        ///     The index of the first occurrence of value in array, if found; otherwise, the
        ///     lower bound of the array minus 1.
        /// </returns>
        public static int IndexOf(
            this Array array,
            object value) => Array.IndexOf(
            array,
            value);

        /// <summary>
        ///     Searches for the specified object in a range of elements of a one dimensional
        ///     array, and returns the index of its first occurrence. The range extends from
        ///     a specified index to the end of the array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
        /// <returns>
        ///     The zero-based index of the first occurrence of value within the range of elements
        ///     in array that extends from startIndex to the last element, if found; otherwise,
        ///     –1.
        /// </returns>
        public static int IndexOf<T>(
            this T[] array,
            T value,
            int startIndex) => Array.IndexOf(
            array,
            value,
            startIndex);

        /// <summary>
        ///     Searches for the specified object in a range of elements of a one-dimensional
        ///     array, and returns the index of its first occurrence. The range extends from
        ///     a specified index to the end of the array.
        /// </summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
        /// <returns>
        ///     The index of the first occurrence of value, if it’s found, within the range of
        ///     elements in array that extends from startIndex to the last element; otherwise,
        ///     the lower bound of the array minus 1.
        /// </returns>
        public static int IndexOf(
            this Array array,
            object value,
            int startIndex) => Array.IndexOf(
            array,
            value,
            startIndex);

        /// <summary>
        ///     Searches for the specified object and returns the index of the last occurrence
        ///     within the range of elements in the <see cref="Array" /> that contains the specified
        ///     number of elements and ends at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of elements to search.</param>
        /// <returns>
        ///     The zero-based index of the last occurrence of value within the range of elements
        ///     in array that contains the number of elements specified in count and ends at
        ///     startIndex, if found; otherwise, –1.
        /// </returns>
        public static int LastIndexOf<T>(
            this T[] array,
            T value,
            int startIndex,
            int count) => Array.LastIndexOf(
            array,
            value,
            startIndex,
            count);

        /// <summary>
        ///     Searches for the specified object and returns the index of the last occurrence
        ///     within the range of elements in the <see cref="Array" /> that extends from the first
        ///     element to the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The starting index of the backward search.</param>
        /// <returns>
        ///     The zero-based index of the last occurrence of value within the range of elements
        ///     in array that extends from the first element to startIndex, if found; otherwise,
        ///     –1.
        /// </returns>
        public static int LastIndexOf<T>(
            this T[] array,
            T value,
            int startIndex) => Array.LastIndexOf(
            array,
            value,
            startIndex);

        /// <summary>
        ///     Searches for the specified object and returns the index of the last occurrence
        ///     within the range of elements in the one-dimensional <see cref="Array" /> that contains
        ///     the specified number of elements and ends at the specified index.
        /// </summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The starting index of the backward search.</param>
        /// <param name="count">The number of elements to search.</param>
        /// <returns>
        ///     The index of the last occurrence of value within the range of elements in array
        ///     that contains the number of elements specified in count and ends at startIndex,
        ///     if found; otherwise, the lower bound of the array minus 1.
        /// </returns>
        public static int LastIndexOf(
            this Array array,
            object value,
            int startIndex,
            int count) => Array.LastIndexOf(
            array,
            value,
            startIndex,
            count);

        /// <summary>
        ///     Searches for the specified object and returns the index of the last occurrence
        ///     within the range of elements in the one-dimensional <see cref="Array" /> that extends
        ///     from the first element to the specified index.
        /// </summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <param name="startIndex">The starting index of the backward search.</param>
        /// <returns>
        ///     The index of the last occurrence of value within the range of elements in array
        ///     that extends from the first element to startIndex, if found; otherwise, the lower
        ///     bound of the array minus 1.
        /// </returns>
        public static int LastIndexOf(
            this Array array,
            object value,
            int startIndex) => Array.LastIndexOf(
            array,
            value,
            startIndex);

        /// <summary>
        ///     Searches for the specified object and returns the index of the last occurrence
        ///     within the entire one-dimensional <see cref="Array" />.
        /// </summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <returns>
        ///     The index of the last occurrence of value within the entire array, if found;
        ///     otherwise, the lower bound of the array minus 1.
        /// </returns>
        public static int LastIndexOf(
            this Array array,
            object value) => Array.LastIndexOf(
            array,
            value);

        /// <summary>
        ///     Searches for the specified object and returns the index of the last occurrence
        ///     within the entire <see cref="Array" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in array.</param>
        /// <returns>
        ///     The zero-based index of the last occurrence of value within the entire array,
        ///     if found; otherwise, –1.
        /// </returns>
        public static int LastIndexOf<T>(
            this T[] array,
            T value) => Array.LastIndexOf(
            array,
            value);

        /// <summary>
        ///     Reverses the sequence of the elements in the entire one-dimensional <see cref="Array" />.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to reverse.</param>
        public static void Reverse(this Array array) => Array.Reverse(array);

        /// <summary>
        ///     Reverses the sequence of the elements in a range of elements in the one-dimensional <see cref="Array" />.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to reverse.</param>
        /// <param name="index">The starting index of the section to reverse.</param>
        /// <param name="length">The number of elements in the section to reverse.</param>
        public static void Reverse(
            this Array array,
            int index,
            int length) => Array.Reverse(
            array,
            index,
            length);

        /// <summary>
        ///     Sorts the elements in an <see cref="Array" /> using the specified <see cref="Comparison{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        /// <param name="comparison">The <see cref="Comparison{T}" /> to use when comparing elements.</param>
        public static void Sort<T>(
            this T[] array,
            Comparison<T> comparison) => Array.Sort(
            array,
            comparison);

        /// <summary>
        ///     Sorts the elements in a range of elements in an <see cref="Array" /> using the specified
        ///     <see cref="IComparer{T}" /> generic interface.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable{T}" /> implementation of each element.
        /// </param>
        public static void Sort<T>(
            this T[] array,
            int index,
            int length,
            IComparer<T> comparer) => Array.Sort(
            array,
            index,
            length,
            comparer);

        /// <summary>
        ///     Sorts the elements in a one-dimensional <see cref="Array" /> using the specified <see cref="IComparer" />.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable" /> implementation of each element.
        /// </param>
        public static void Sort(
            this Array array,
            IComparer comparer) => Array.Sort(
            array,
            comparer);

        /// <summary>
        ///     Sorts the elements in an <see cref="Array" /> using the specified <see cref="IComparer{T}" />
        ///     generic interface.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable{T}" /> implementation of each element.
        /// </param>
        public static void Sort<T>(
            this T[] array,
            IComparer<T> comparer) => Array.Sort(
            array,
            comparer);

        /// <summary>
        ///     Sorts the specified array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        public static void Sort<T>(this T[] array) => Array.Sort(array);

        /// <summary>
        ///     Sorts the elements in a range of elements in a one-dimensional <see cref="Array" /> using
        ///     the specified <see cref="IComparer" />.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable" /> implementation of each element.
        /// </param>
        public static void Sort(
            this Array array,
            int index,
            int length,
            IComparer comparer) => Array.Sort(
            array,
            index,
            length,
            comparer);

        /// <summary>
        ///     Sorts the elements in a range of elements in a one-dimensional <see cref="Array" /> using
        ///     the <see cref="IComparable" /> implementation of each element of the <see cref="Array" />.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        public static void Sort(
            this Array array,
            int index,
            int length) => Array.Sort(
            array,
            index,
            length);

        /// <summary>
        ///     Sorts the elements in a range of elements in an <see cref="Array" /> using the <see cref="IComparable{T}" />
        ///     generic interface implementation of each element of the <see cref="Array" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        public static void Sort<T>(
            this T[] array,
            int index,
            int length) => Array.Sort(
            array,
            index,
            length);

        /// <summary>
        ///     Determines whether every element in the array matches the conditions defined
        ///     by the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to check against the conditions.</param>
        /// <param name="match">The predicate that defines the conditions to check against the elements.</param>
        /// <returns>
        ///     <see langword="true" /> if every element in array matches the conditions defined by the specified
        ///     predicate; otherwise, <see langword="false" />. If there are no elements in the array, the return
        ///     value is <see langword="true" />.
        /// </returns>
        public static bool TrueForAll<T>(
            this T[] array,
            Predicate<T> match) => Array.TrueForAll(
            array,
            match);

#if !STANDARD
        /// <summary>
        ///     Sorts a pair of <see cref="Array" /> objects (one contains the keys and the other contains
        ///     the corresponding items) based on the keys in the first <see cref="Array" /> using the
        ///     <see cref="IComparable{T}" /> generic interface implementation of each key.
        /// </summary>
        /// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
        /// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        public static void Sort<TKey, TValue>(
            this TKey[] keys,
            TValue[] items) => Array.Sort(
            keys,
            items);

        /// <summary>
        ///     Sorts a pair of <see cref="Array" /> objects (one contains the keys and the other contains
        ///     the corresponding items) based on the keys in the first <see cref="Array" /> using the
        ///     specified <see cref="IComparer{T}" /> generic interface.
        /// </summary>
        /// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
        /// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        /// <param name="comparer">
        ///     The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable{T}" /> implementation of each element.
        /// </param>
        public static void Sort<TKey, TValue>(
            this TKey[] keys,
            TValue[] items,
            IComparer<TKey> comparer) => Array.Sort(
            keys,
            items,
            comparer);

        /// <summary>
        ///     Sorts a range of elements in a pair of <see cref="Array" /> objects (one contains the
        ///     keys and the other contains the corresponding items) based on the keys in the
        ///     first <see cref="Array" /> using the <see cref="IComparable{T}" /> generic interface implementation
        ///     of each key.
        /// </summary>
        /// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
        /// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        public static void Sort<TKey, TValue>(
            this TKey[] keys,
            TValue[] items,
            int index,
            int length) => Array.Sort(
            keys,
            items,
            index,
            length);

        /// <summary>
        ///     Sorts a range of elements in a pair of <see cref="Array" /> objects (one contains the
        ///     keys and the other contains the corresponding items) based on the keys in the
        ///     first <see cref="Array" /> using the specified <see cref="IComparer{T}" />
        ///     generic interface.
        /// </summary>
        /// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
        /// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable{T}" /> implementation of each element.
        /// </param>
        public static void Sort<TKey, TValue>(
            this TKey[] keys,
            TValue[] items,
            int index,
            int length,
            IComparer<TKey> comparer) => Array.Sort(
            keys,
            items,
            index,
            length,
            comparer);

        /// <summary>
        ///     Sorts a range of elements in a pair of one-dimensional <see cref="Array" /> objects (one
        ///     contains the keys and the other contains the corresponding items) based on the
        ///     keys in the first <see cref="Array" /> using the specified <see cref="IComparer" />.
        /// </summary>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="IComparer" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable" /> implementation of each element.
        /// </param>
        public static void Sort(
            this Array keys,
            Array items,
            int index,
            int length,
            IComparer comparer) => Array.Sort(
            keys,
            items,
            index,
            length,
            comparer);

        /// <summary>
        ///     Sorts the elements in an entire one-dimensional <see cref="Array" /> using the <see cref="IComparable" />
        ///     implementation of each element of the <see cref="Array" />.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to sort.</param>
        public static void Sort(this Array array) => Array.Sort(array);

        /// <summary>
        ///     Sorts a pair of one-dimensional <see cref="Array" /> objects (one contains the keys and
        ///     the other contains the corresponding items) based on the keys in the first <see cref="Array" />
        ///     using the specified <see cref="IComparer" />.
        /// </summary>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        /// <param name="comparer">
        ///     The <see cref="IComparer" /> implementation to use when comparing elements.-or-
        ///     <see langword="null" /> to use the <see cref="IComparable" /> implementation of each element.
        /// </param>
        public static void Sort(
            this Array keys,
            Array items,
            IComparer comparer) => Array.Sort(
            keys,
            items,
            comparer);

        /// <summary>
        ///     Sorts a pair of one-dimensional <see cref="Array" /> objects (one contains the keys and
        ///     the other contains the corresponding items) based on the keys in the first <see cref="Array" />
        ///     using the <see cref="IComparable" /> implementation of each key.
        /// </summary>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        public static void Sort(
            this Array keys,
            Array items) => Array.Sort(
            keys,
            items);

        /// <summary>
        ///     Sorts a range of elements in a pair of one-dimensional <see cref="Array" /> objects (one
        ///     contains the keys and the other contains the corresponding items) based on the
        ///     keys in the first <see cref="Array" /> using the <see cref="IComparable" /> implementation of
        ///     each key.
        /// </summary>
        /// <param name="keys">The one-dimensional <see cref="Array" /> that contains the keys to sort.</param>
        /// <param name="items">
        ///     The one-dimensional, zero-based <see cref="Array" /> that contains the items that correspond to the
        ///     keys in keys, or null to sort only keys.
        /// </param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        public static void Sort(
            this Array keys,
            Array items,
            int index,
            int length) => Array.Sort(
            keys,
            items,
            index,
            length);
#endif
    }
}