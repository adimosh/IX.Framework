// <copyright file="NameSwitchEventArgs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.StandardExtensions.EventModel
{
    /// <summary>
    ///     An event arguments class depicting a named switch.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    [PublicAPI]
    public class NameSwitchEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NameSwitchEventArgs" /> class.
        /// </summary>
        /// <param name="switchName">The switch name.</param>
        /// <param name="switchValue">The switch value.</param>
        public NameSwitchEventArgs(
            string switchName,
            bool switchValue)
        {
            this.Name = switchName;
            this.Value = switchValue;
        }

#pragma warning disable SA1623 // Property summary documentation should match accessors - It matches, however the analyzer expects that this is a switch instead of a boolean value.
        /// <summary>
        ///     Gets the value of the switch.
        /// </summary>
        /// <value>The switch value.</value>
        public bool Value { get; }
#pragma warning restore SA1623 // Property summary documentation should match accessors

        /// <summary>
        ///     Gets the name value.
        /// </summary>
        /// <value>The name value.</value>
        public string Name { get; }
    }
}