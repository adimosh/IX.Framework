// <copyright file="VariableBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.Abstractions.Memory;
using IX.StandardExtensions;
using IX.StandardExtensions.ComponentModel;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A base class for variable containers.
    /// </summary>
    /// <typeparam name="T">The discreet type of data contained in the variable.</typeparam>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
    /// <seealso cref="IX.Abstractions.Memory.IVariable{T}" />
    public abstract class VariableBase<T> : ViewModelBase, IVariable<T>, IDeepCloneable<VariableBase<T>>
    {
        private T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableBase{T}"/> class.
        /// </summary>
        protected VariableBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableBase{T}"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        protected VariableBase(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        /// <value>The value.</value>
        public virtual T Value
        {
            get => this.value;

            set
            {
                this.value = value;

                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets or sets the raw value of the variable for a debugger window.
        /// </summary>
        /// <value>The raw value for the debugger.</value>
        /// <remarks>The getter of this property should not involve state changes within the variable.</remarks>
        public virtual T RawDebuggerValue
        {
            get => this.value;

            set
            {
                this.value = value;

                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets or sets the value that is shown in and loaded from a debugger window.
        /// </summary>
        /// <value>The debugger value.</value>
        /// <remarks><para>Implementations of this property should treat converting to/from string representations of values.</para>
        /// <para>The getter of this property should not involve state changes within the variable.</para></remarks>
        public abstract string DebuggerValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether the value stored in this variable is the default value for this type.
        /// </summary>
        /// <value><c>true</c> if the contained value is default for the type; otherwise, <c>false</c>.</value>
        public abstract bool IsDefault { get; }

        /// <summary>
        /// Gets or sets the boxed value contained within the variable.
        /// </summary>
        /// <value>The boxed value.</value>
        /// <remarks><para>This property will box/unbox values as required by the caller, based on the abilities of the implementing class.</para>
        /// <para>For this reason, please refrain from using this property if you don't absolutely have to.</para></remarks>
        object IVariable.BoxedValue
        {
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - That is the point of this property
            get => this.value;
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

            set
            {
                if (typeof(T) != value?.GetType())
                {
                    throw new ArgumentInvalidTypeException();
                }

                this.Value = (T)value;
            }
        }

        /// <summary>
        /// Gets or sets the value, internally.
        /// </summary>
        /// <value>The value.</value>
        protected T InternalValue
        {
            get => this.value;

            set => this.value = value;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.</returns>
        public abstract int CompareTo(IVariable<T> other);

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        VariableBase<T> IDeepCloneable<VariableBase<T>>.DeepClone() => this.DeepCloneImplementation();

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public abstract bool Equals(IVariable<T> other);

        /// <summary>
        /// Gets the discreet type of the data contained within the variable.
        /// </summary>
        /// <returns>The data type.</returns>
        public Type GetDataType() => typeof(T);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected abstract VariableBase<T> DeepCloneImplementation();
    }
}