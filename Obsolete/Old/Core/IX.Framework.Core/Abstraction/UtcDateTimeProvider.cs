using System;

namespace IX.Framework.Abstraction
{
    /// <summary>
    /// Represents the UTC time zone.
    /// </summary>
    public sealed class UtcDateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date and time value.
        /// </summary>
        /// <returns>
        /// A <see cref="System.DateTime" /> value representing the current moment.
        /// </returns>
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the current date value.
        /// </summary>
        /// <returns>
        /// A <see cref="System.DateTime" /> value representing the current day.
        /// </returns>
        public DateTime Today()
        {
            return DateTime.UtcNow.Date;
        }
    }
}