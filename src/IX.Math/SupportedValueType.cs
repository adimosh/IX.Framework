// <copyright file="SupportedValueType.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math
{
    /// <summary>
    /// An enumeration of supported value types.
    /// </summary>
    public enum SupportedValueType
    {
        /// <summary>
        /// Not known (pass as <see cref="object"/>).
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Numeric (depends on the numeric type).
        /// </summary>
        Numeric = 1,

        /// <summary>
        /// Boolean (pass as <see cref="bool"/>).
        /// </summary>
        Boolean = 2,

        /// <summary>
        /// String (pass as <see cref="string"/>).
        /// </summary>
        String = 3,

        /// <summary>
        /// Byte array (pass as <see cref="T:byte[]"/>).
        /// </summary>
        ByteArray = 4,
    }
}