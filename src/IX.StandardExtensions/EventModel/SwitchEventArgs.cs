// <copyright file="SwitchEventArgs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.StandardExtensions.EventModel
{
    /// <summary>
    ///     An event arguments class depicting a boolean switch.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    [PublicAPI]
    public class SwitchEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SwitchEventArgs" /> class.
        /// </summary>
        /// <param name="switchValue">The value of the switch.</param>
        public SwitchEventArgs(bool switchValue)
        {
            this.Value = switchValue;
        }

#pragma warning disable SA1623 // Property summary documentation should match accessors - It matches, however the analyzer expects that this is itself a switch instead of the boolean value of a switch.
        /// <summary>
        ///     Gets the value of the switch.
        /// </summary>
        /// <value>The switch value.</value>
        public bool Value { get; }
#pragma warning restore SA1623 // Property summary documentation should match accessors
    }
}