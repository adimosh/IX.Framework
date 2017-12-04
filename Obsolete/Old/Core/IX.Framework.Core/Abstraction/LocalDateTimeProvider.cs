using System;

namespace IX.Framework.Abstraction
{
    /// <summary>
    /// Provides date/time values for local time.
    /// </summary>
    public sealed class LocalDateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date and time value.
        /// </summary>
        /// <returns>
        /// A <see cref="System.DateTime" /> value representing the current moment.
        /// </returns>
        public DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Gets the current date value.
        /// </summary>
        /// <returns>
        /// A <see cref="System.DateTime" /> value representing the current day.
        /// </returns>
        public DateTime Today()
        {
            return DateTime.Today;
        }
    }
}