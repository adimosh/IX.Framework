using System;
using System.Diagnostics.Contracts;

namespace IX.Framework.Abstraction
{
    /// <summary>
    /// Represents a synchronized provider for date/time values.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date and time value.
        /// </summary>
        /// <returns>A <see cref="System.DateTime" /> value representing the current moment.</returns>
        [Pure]
        DateTime Now();

        /// <summary>
        /// Gets the current date value.
        /// </summary>
        /// <returns>A <see cref="System.DateTime" /> value representing the current day.</returns>
        [Pure]
        DateTime Today();
    }
}