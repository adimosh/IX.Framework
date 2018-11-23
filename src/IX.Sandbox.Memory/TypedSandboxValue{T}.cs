// <copyright file="TypedSandboxValue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Reflection;
using IX.Sandbox.Memory.Exceptions;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A strongly-typed value that is placed in the sandbox.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <seealso cref="IX.Sandbox.Memory.SandboxValue" />
    public abstract class TypedSandboxValue<T> : SandboxValue
    {
        private T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedSandboxValue{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public TypedSandboxValue(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the raw object that is represented by this value.
        /// </summary>
        /// <value>The raw object.</value>
        /// <exception cref="IX.Sandbox.Memory.Exceptions.ValueTypeNotSupportedException">The type of object that is set is not supported.</exception>
        public override object RawObject
        {
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is both unavoidable and desired
            get => this.value;
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

            protected set
            {
                if (value == null)
                {
                    this.value = default;
                }
                else if (typeof(T).GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
                {
                    this.value = (T)value;
                }
                else
                {
                    throw new ValueTypeNotSupportedException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; set; }
    }
}