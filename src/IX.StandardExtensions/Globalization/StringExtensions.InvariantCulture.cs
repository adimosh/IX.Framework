// <copyright file="StringExtensions.InvariantCulture.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Globalization;

namespace IX.StandardExtensions.Globalization
{
    /// <summary>
    /// Extensions to strings to help with globalization.
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// Determines whether a source string contains the specified value string in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string contains the specified value string; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureContains(this string source, string value) => source.InvariantCultureIndexOf(value) >= 0;

        /// <summary>
        /// Determines whether a source string contains the specified value string in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string contains the specified value string; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureContainsInsensitive(this string source, string value) => source.InvariantCultureIndexOfInsensitive(value) >= 0;

        /// <summary>
        /// Finds the index of the specified value string in a source string in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   The index where the string is found, otherwise -1.
        /// </returns>
        public static int InvariantCultureIndexOf(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo?.IndexOf(source, value, CompareOptions.None) ?? -1;

        /// <summary>
        /// Finds the index of the specified value string in a source string in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <param name="startIndex">The index in the source string to start searching at.</param>
        /// <returns>The index where the string is found, otherwise -1.</returns>
        public static int InvariantCultureIndexOf(this string source, string value, int startIndex) => CultureInfo.InvariantCulture?.CompareInfo?.IndexOf(source, value, startIndex, CompareOptions.None) ?? -1;

        /// <summary>
        /// Finds the index of the specified value string in a source string in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <param name="startIndex">The index in the source string to start searching at.</param>
        /// <param name="count">The number of characters to search.</param>
        /// <returns>The index where the string is found, otherwise -1.</returns>
        public static int InvariantCultureIndexOf(this string source, string value, int startIndex, int count) => CultureInfo.InvariantCulture?.CompareInfo?.IndexOf(source, value, startIndex, count, CompareOptions.None) ?? -1;

        /// <summary>
        /// Finds the index of the specified value string in a source string in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   The index where the string is found, otherwise -1.
        /// </returns>
        public static int InvariantCultureIndexOfInsensitive(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo?.IndexOf(source, value, CompareOptions.IgnoreCase) ?? -1;

        /// <summary>
        /// Finds the index of the specified value string in a source string in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <param name="startIndex">The index in the source string to start searching at.</param>
        /// <returns>The index where the string is found, otherwise -1.</returns>
        public static int InvariantCultureIndexOfInsensitive(this string source, string value, int startIndex) => CultureInfo.InvariantCulture?.CompareInfo?.IndexOf(source, value, startIndex, CompareOptions.IgnoreCase) ?? -1;

        /// <summary>
        /// Finds the index of the specified value string in a source string in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <param name="startIndex">The index in the source string to start searching at.</param>
        /// <param name="count">The number of characters to search.</param>
        /// <returns>The index where the string is found, otherwise -1.</returns>
        public static int InvariantCultureIndexOfInsensitive(this string source, string value, int startIndex, int count) => CultureInfo.InvariantCulture?.CompareInfo?.IndexOf(source, value, startIndex, count, CompareOptions.IgnoreCase) ?? -1;

        /// <summary>
        /// Compares the source string with a selected value in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   The comparison of the two strings, with 0 meaning equality.
        /// </returns>
        public static int InvariantCultureCompareTo(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo.Compare(source, value, CompareOptions.None) ?? 0;

        /// <summary>
        /// Compares the source string with a selected value in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   The comparison of the two strings, with 0 meaning equality.
        /// </returns>
        public static int InvariantCultureCompareToInsensitive(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo.Compare(source, value, CompareOptions.IgnoreCase) ?? 0;

        /// <summary>
        /// Equates the source string with a selected value in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string is equal to the value; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureEquals(this string source, string value) => source.InvariantCultureCompareTo(value) == 0;

        /// <summary>
        /// Equates the source string with a selected value in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string is equal to the value; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureEqualsInsensitive(this string source, string value) => source.InvariantCultureCompareToInsensitive(value) == 0;

        /// <summary>
        /// Checks whether or not the source string starts with a selected value in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string is equal to the value; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureStartsWith(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo.IsPrefix(source, value, CompareOptions.None) ?? false;

        /// <summary>
        /// Checks whether or not the source string starts with a selected value in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string is equal to the value; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureStartsWithInsensitive(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo.IsPrefix(source, value, CompareOptions.IgnoreCase) ?? false;

        /// <summary>
        /// Checks whether or not the source string ends with a selected value in a case-sensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string is equal to the value; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureEndsWith(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo.IsSuffix(source, value, CompareOptions.None) ?? false;

        /// <summary>
        /// Checks whether or not the source string ends with a selected value in a case-insensitive manner using the comparison rules of the invariant culture.
        /// </summary>
        /// <param name="source">The source to search in.</param>
        /// <param name="value">The string value to do the evaluation.</param>
        /// <returns>
        ///   <c>true</c> if the source string is equal to the value; otherwise, <c>false</c>.
        /// </returns>
        public static bool InvariantCultureEndsWithInsensitive(this string source, string value) => CultureInfo.InvariantCulture?.CompareInfo.IsSuffix(source, value, CompareOptions.IgnoreCase) ?? false;
    }
}