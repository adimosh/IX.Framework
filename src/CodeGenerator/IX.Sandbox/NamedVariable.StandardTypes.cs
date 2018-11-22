// <copyright file="NamedVariable.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Text;
using System.Threading;
using IX.Abstractions.Memory;
using IX.StandardExtensions;

namespace IX.Sandbox.Memory
{
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name
    /// <summary>
    /// A variable of discreet type <see cref="T:byte" />.
    /// </summary>
    public class NamedByteVariable : NamedVariableBase<byte>, IDeepCloneable<NamedByteVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedByteVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedByteVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedByteVariable(string name, byte value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedByteVariable(string name, byte value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = byte.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedByteVariable" /> to <see cref="T:byte" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator byte(NamedByteVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ByteVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedByteVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<byte> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<byte> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<byte> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<byte> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<byte> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<byte> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedByteVariable DeepClone() => new NamedByteVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<byte> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:sbyte" />.
    /// </summary>
    public class NamedSignedByteVariable : NamedVariableBase<sbyte>, IDeepCloneable<NamedSignedByteVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedSignedByteVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedSignedByteVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedSignedByteVariable(string name, sbyte value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedSignedByteVariable(string name, sbyte value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = sbyte.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedSignedByteVariable" /> to <see cref="T:sbyte" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator sbyte(NamedSignedByteVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="SignedByteVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedSignedByteVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<sbyte> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<sbyte> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<sbyte> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<sbyte> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<sbyte> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<sbyte> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedSignedByteVariable DeepClone() => new NamedSignedByteVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<sbyte> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:short" />.
    /// </summary>
    public class NamedShortVariable : NamedVariableBase<short>, IDeepCloneable<NamedShortVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedShortVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedShortVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedShortVariable(string name, short value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedShortVariable(string name, short value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = short.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedShortVariable" /> to <see cref="T:short" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator short(NamedShortVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ShortVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedShortVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<short> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<short> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<short> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<short> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<short> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<short> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedShortVariable DeepClone() => new NamedShortVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<short> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ushort" />.
    /// </summary>
    public class NamedUnsignedShortVariable : NamedVariableBase<ushort>, IDeepCloneable<NamedUnsignedShortVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedUnsignedShortVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedUnsignedShortVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedUnsignedShortVariable(string name, ushort value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedUnsignedShortVariable(string name, ushort value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = ushort.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedUnsignedShortVariable" /> to <see cref="T:ushort" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ushort(NamedUnsignedShortVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedShortVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedUnsignedShortVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<ushort> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<ushort> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<ushort> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<ushort> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<ushort> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<ushort> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedUnsignedShortVariable DeepClone() => new NamedUnsignedShortVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<ushort> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:char" />.
    /// </summary>
    public class NamedCharVariable : NamedVariableBase<char>, IDeepCloneable<NamedCharVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedCharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedCharVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedCharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedCharVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedCharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedCharVariable(string name, char value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedCharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedCharVariable(string name, char value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value =
#if STANDARD
                    string.IsNullOrEmpty(value) ? default : value.ToCharArray(0, 1)[0];
#else
                    char.Parse(value);
#endif
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedCharVariable" /> to <see cref="T:char" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator char(NamedCharVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="CharVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedCharVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<char> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<char> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<char> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<char> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<char> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<char> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedCharVariable DeepClone() => new NamedCharVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<char> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:int" />.
    /// </summary>
    public class NamedIntVariable : NamedVariableBase<int>, IDeepCloneable<NamedIntVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedIntVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedIntVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedIntVariable(string name, int value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedIntVariable(string name, int value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = int.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedIntVariable" /> to <see cref="T:int" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(NamedIntVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="IntVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedIntVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<int> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<int> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<int> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<int> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<int> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<int> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedIntVariable DeepClone() => new NamedIntVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<int> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:uint" />.
    /// </summary>
    public class NamedUnsignedIntVariable : NamedVariableBase<uint>, IDeepCloneable<NamedUnsignedIntVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedUnsignedIntVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedUnsignedIntVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedUnsignedIntVariable(string name, uint value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedUnsignedIntVariable(string name, uint value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = uint.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedUnsignedIntVariable" /> to <see cref="T:uint" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator uint(NamedUnsignedIntVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedIntVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedUnsignedIntVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<uint> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<uint> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<uint> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<uint> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<uint> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<uint> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedUnsignedIntVariable DeepClone() => new NamedUnsignedIntVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<uint> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:long" />.
    /// </summary>
    public class NamedLongVariable : NamedVariableBase<long>, IDeepCloneable<NamedLongVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedLongVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedLongVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedLongVariable(string name, long value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedLongVariable(string name, long value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = long.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedLongVariable" /> to <see cref="T:long" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator long(NamedLongVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LongVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedLongVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<long> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<long> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<long> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<long> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<long> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<long> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedLongVariable DeepClone() => new NamedLongVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<long> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ulong" />.
    /// </summary>
    public class NamedUnsignedLongVariable : NamedVariableBase<ulong>, IDeepCloneable<NamedUnsignedLongVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedUnsignedLongVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedUnsignedLongVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedUnsignedLongVariable(string name, ulong value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedUnsignedLongVariable(string name, ulong value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = ulong.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedUnsignedLongVariable" /> to <see cref="T:ulong" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ulong(NamedUnsignedLongVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedLongVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedUnsignedLongVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<ulong> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<ulong> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<ulong> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<ulong> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<ulong> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<ulong> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedUnsignedLongVariable DeepClone() => new NamedUnsignedLongVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<ulong> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:float" />.
    /// </summary>
    public class NamedFloatVariable : NamedVariableBase<float>, IDeepCloneable<NamedFloatVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedFloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedFloatVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedFloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedFloatVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedFloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedFloatVariable(string name, float value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedFloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedFloatVariable(string name, float value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = float.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedFloatVariable" /> to <see cref="T:float" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator float(NamedFloatVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="FloatVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedFloatVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<float> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<float> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<float> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<float> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<float> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<float> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedFloatVariable DeepClone() => new NamedFloatVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<float> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:double" />.
    /// </summary>
    public class NamedDoubleVariable : NamedVariableBase<double>, IDeepCloneable<NamedDoubleVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedDoubleVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedDoubleVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedDoubleVariable(string name, double value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedDoubleVariable(string name, double value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = double.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedDoubleVariable" /> to <see cref="T:double" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(NamedDoubleVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DoubleVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedDoubleVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<double> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<double> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<double> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<double> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<double> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<double> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedDoubleVariable DeepClone() => new NamedDoubleVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<double> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:decimal" />.
    /// </summary>
    public class NamedDecimalVariable : NamedVariableBase<decimal>, IDeepCloneable<NamedDecimalVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedDecimalVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedDecimalVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedDecimalVariable(string name, decimal value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedDecimalVariable(string name, decimal value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = decimal.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedDecimalVariable" /> to <see cref="T:decimal" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator decimal(NamedDecimalVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DecimalVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedDecimalVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<decimal> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<decimal> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<decimal> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<decimal> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<decimal> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<decimal> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedDecimalVariable DeepClone() => new NamedDecimalVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<decimal> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:DateTime" />.
    /// </summary>
    public class NamedDateTimeVariable : NamedVariableBase<DateTime>, IDeepCloneable<NamedDateTimeVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedDateTimeVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedDateTimeVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedDateTimeVariable(string name, DateTime value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedDateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedDateTimeVariable(string name, DateTime value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = DateTime.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedDateTimeVariable" /> to <see cref="T:DateTime" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator DateTime(NamedDateTimeVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DateTimeVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedDateTimeVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<DateTime> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<DateTime> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<DateTime> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<DateTime> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<DateTime> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<DateTime> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedDateTimeVariable DeepClone() => new NamedDateTimeVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<DateTime> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:bool" />.
    /// </summary>
    public class NamedBooleanVariable : NamedVariableBase<bool>, IDeepCloneable<NamedBooleanVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedBooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedBooleanVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedBooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedBooleanVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedBooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedBooleanVariable(string name, bool value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedBooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedBooleanVariable(string name, bool value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = bool.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedBooleanVariable" /> to <see cref="T:bool" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator bool(NamedBooleanVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="BooleanVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedBooleanVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<bool> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<bool> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<bool> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<bool> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<bool> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<bool> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedBooleanVariable DeepClone() => new NamedBooleanVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<bool> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:TimeSpan" />.
    /// </summary>
    public class NamedTimeSpanVariable : NamedVariableBase<TimeSpan>, IDeepCloneable<NamedTimeSpanVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedTimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedTimeSpanVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedTimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedTimeSpanVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedTimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedTimeSpanVariable(string name, TimeSpan value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedTimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedTimeSpanVariable(string name, TimeSpan value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = TimeSpan.Parse(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedTimeSpanVariable" /> to <see cref="T:TimeSpan" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator TimeSpan(NamedTimeSpanVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="TimeSpanVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedTimeSpanVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<TimeSpan> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<TimeSpan> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<TimeSpan> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<TimeSpan> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<TimeSpan> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<TimeSpan> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedTimeSpanVariable DeepClone() => new NamedTimeSpanVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<TimeSpan> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:string" />.
    /// </summary>
    public class NamedStringVariable : NamedVariableBase<string>, IDeepCloneable<NamedStringVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedStringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedStringVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedStringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedStringVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedStringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedStringVariable(string name, string value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedStringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedStringVariable(string name, string value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = value;
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedStringVariable" /> to <see cref="T:string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedStringVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<string> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<string> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<string> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.CompareTo(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<string> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<string> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<string> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedStringVariable DeepClone() => new NamedStringVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<string> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:byte[]" />.
    /// </summary>
    public class NamedByteArrayVariable : NamedVariableBase<byte[]>, IDeepCloneable<NamedByteArrayVariable>
    {
        private Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedByteArrayVariable(string name)
            : base(name)
        {
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedByteArrayVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public NamedByteArrayVariable(string name, byte[] value)
            : base(name)
        {
            this.encoding = Encoding.UTF8;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedByteArrayVariable(string name, byte[] value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = Encoding.UTF8;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        public NamedByteArrayVariable(string name, Encoding encoding)
            : base(name)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedByteArrayVariable(string name, Encoding encoding, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        public NamedByteArrayVariable(string name, byte[] value, Encoding encoding)
            : base(name)
        {
            this.encoding = encoding;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public NamedByteArrayVariable(string name, byte[] value, Encoding encoding, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = encoding;
            this.InternalValue = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public override string DebuggerValue
        {
            get => this.Value.ToString();

            set
            {
                this.Value = this.encoding.GetBytes(value);
                this.RaisePropertyChangedWithValidation(nameof(this.Value));
                this.RaisePropertyChanged(nameof(this.DebuggerValue));
                this.RaisePropertyChanged(nameof(this.RawDebuggerValue));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><see langword="true"/> if this instance is default; otherwise, <see langword="false"/>.</value>
        public override bool IsDefault => (this.Value?.Length ?? 0) == 0;

        /// <summary>
        /// Performs an implicit conversion from <see cref="NamedByteArrayVariable" /> to <see cref="T:byte[]" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator byte[](NamedByteArrayVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ByteArrayVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(NamedByteArrayVariable value) => value?.DebuggerValue ?? default;

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(IVariable<byte[]> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!(other is INamedVariable<byte[]> otherVariable))
            {
                return -1;
            }

            var nameComparison = otherVariable.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return otherVariable.RawDebuggerValue.SequenceCompare(this.RawDebuggerValue);
        }

        /// <summary>
        /// Compares this variable to another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><c>0</c> if the two are equal, a different value if not.</returns>
        public override int CompareTo(INamedVariable<byte[]> other)
        {
            if (other == null)
            {
                return -1;
            }

            var nameComparison = other.Name?.CompareTo(this.Name) ?? -1;
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return other.RawDebuggerValue.SequenceCompare(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(IVariable<byte[]> other)
        {
            if (other == null)
            {
                return false;
            }

            if (!(other is INamedVariable<byte[]> otherVariable))
            {
                return false;
            }

            var nameComparison = otherVariable.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return otherVariable.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Equates this variable with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns><see langword="true"/> if the variables are equal, <see langword="false"/> otherwise.</returns>
        public override bool Equals(INamedVariable<byte[]> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = other.Name?.Equals(this.Name) ?? false;
            if (!nameComparison)
            {
                return false;
            }

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public NamedByteArrayVariable DeepClone() => new NamedByteArrayVariable(this.Name, this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<byte[]> DeepCloneImplementation() => this.DeepClone();
    }
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
}