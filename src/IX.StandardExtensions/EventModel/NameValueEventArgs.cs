// <copyright file="NameValueEventArgs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.EventModel
{
    /// <summary>
    /// An event arguments class depicting a named value.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class NameValueEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameValueEventArgs"/> class.
        /// </summary>
        /// <param name="nameValue">The name value.</param>
        public NameValueEventArgs(string nameValue)
        {
            this.Name = nameValue;
        }

        /// <summary>
        /// Gets the name value.
        /// </summary>
        /// <value>The name value.</value>
        public string Name { get; }
    }
}