// <copyright file="Variable.StandardTypes.cs" company="Adrian Mos">
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
    public class ByteVariable : VariableBase<byte>, IDeepCloneable<ByteVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ByteVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public ByteVariable(string name, byte value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteVariable(string name, byte value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ByteVariable" /> to <see cref="T:byte" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator byte(ByteVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ByteVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(ByteVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<byte> other)
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
        public override VariableBase<byte> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        ByteVariable IDeepCloneable<ByteVariable>.DeepClone() => new ByteVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:sbyte" />.
    /// </summary>
    public class SignedByteVariable : VariableBase<sbyte>, IDeepCloneable<SignedByteVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SignedByteVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public SignedByteVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public SignedByteVariable(string name, sbyte value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public SignedByteVariable(string name, sbyte value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="SignedByteVariable" /> to <see cref="T:sbyte" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator sbyte(SignedByteVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="SignedByteVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(SignedByteVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<sbyte> other)
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
        public override VariableBase<sbyte> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        SignedByteVariable IDeepCloneable<SignedByteVariable>.DeepClone() => new SignedByteVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:short" />.
    /// </summary>
    public class ShortVariable : VariableBase<short>, IDeepCloneable<ShortVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ShortVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ShortVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public ShortVariable(string name, short value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ShortVariable(string name, short value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ShortVariable" /> to <see cref="T:short" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator short(ShortVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ShortVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(ShortVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<short> other)
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
        public override VariableBase<short> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        ShortVariable IDeepCloneable<ShortVariable>.DeepClone() => new ShortVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ushort" />.
    /// </summary>
    public class UnsignedShortVariable : VariableBase<ushort>, IDeepCloneable<UnsignedShortVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public UnsignedShortVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedShortVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public UnsignedShortVariable(string name, ushort value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedShortVariable(string name, ushort value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedShortVariable" /> to <see cref="T:ushort" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ushort(UnsignedShortVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedShortVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(UnsignedShortVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<ushort> other)
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
        public override VariableBase<ushort> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        UnsignedShortVariable IDeepCloneable<UnsignedShortVariable>.DeepClone() => new UnsignedShortVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:char" />.
    /// </summary>
    public class CharVariable : VariableBase<char>, IDeepCloneable<CharVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CharVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public CharVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public CharVariable(string name, char value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public CharVariable(string name, char value, SynchronizationContext synchronizationContext)
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
#if NETSTANDARD1_1
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="CharVariable" /> to <see cref="T:char" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator char(CharVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="CharVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(CharVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<char> other)
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
        public override VariableBase<char> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        CharVariable IDeepCloneable<CharVariable>.DeepClone() => new CharVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:int" />.
    /// </summary>
    public class IntVariable : VariableBase<int>, IDeepCloneable<IntVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public IntVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public IntVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public IntVariable(string name, int value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public IntVariable(string name, int value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="IntVariable" /> to <see cref="T:int" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(IntVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="IntVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(IntVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<int> other)
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
        public override VariableBase<int> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        IntVariable IDeepCloneable<IntVariable>.DeepClone() => new IntVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:uint" />.
    /// </summary>
    public class UnsignedIntVariable : VariableBase<uint>, IDeepCloneable<UnsignedIntVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public UnsignedIntVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedIntVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public UnsignedIntVariable(string name, uint value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedIntVariable(string name, uint value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedIntVariable" /> to <see cref="T:uint" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator uint(UnsignedIntVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedIntVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(UnsignedIntVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<uint> other)
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
        public override VariableBase<uint> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        UnsignedIntVariable IDeepCloneable<UnsignedIntVariable>.DeepClone() => new UnsignedIntVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:long" />.
    /// </summary>
    public class LongVariable : VariableBase<long>, IDeepCloneable<LongVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LongVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public LongVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public LongVariable(string name, long value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public LongVariable(string name, long value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LongVariable" /> to <see cref="T:long" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator long(LongVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LongVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(LongVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<long> other)
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
        public override VariableBase<long> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        LongVariable IDeepCloneable<LongVariable>.DeepClone() => new LongVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ulong" />.
    /// </summary>
    public class UnsignedLongVariable : VariableBase<ulong>, IDeepCloneable<UnsignedLongVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public UnsignedLongVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedLongVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public UnsignedLongVariable(string name, ulong value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedLongVariable(string name, ulong value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedLongVariable" /> to <see cref="T:ulong" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ulong(UnsignedLongVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="UnsignedLongVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(UnsignedLongVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<ulong> other)
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
        public override VariableBase<ulong> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        UnsignedLongVariable IDeepCloneable<UnsignedLongVariable>.DeepClone() => new UnsignedLongVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:float" />.
    /// </summary>
    public class FloatVariable : VariableBase<float>, IDeepCloneable<FloatVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public FloatVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public FloatVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public FloatVariable(string name, float value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public FloatVariable(string name, float value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="FloatVariable" /> to <see cref="T:float" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator float(FloatVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="FloatVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(FloatVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<float> other)
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
        public override VariableBase<float> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        FloatVariable IDeepCloneable<FloatVariable>.DeepClone() => new FloatVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:double" />.
    /// </summary>
    public class DoubleVariable : VariableBase<double>, IDeepCloneable<DoubleVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DoubleVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DoubleVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public DoubleVariable(string name, double value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DoubleVariable(string name, double value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DoubleVariable" /> to <see cref="T:double" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(DoubleVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DoubleVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(DoubleVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<double> other)
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
        public override VariableBase<double> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        DoubleVariable IDeepCloneable<DoubleVariable>.DeepClone() => new DoubleVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:decimal" />.
    /// </summary>
    public class DecimalVariable : VariableBase<decimal>, IDeepCloneable<DecimalVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DecimalVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DecimalVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public DecimalVariable(string name, decimal value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DecimalVariable(string name, decimal value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DecimalVariable" /> to <see cref="T:decimal" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator decimal(DecimalVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DecimalVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(DecimalVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<decimal> other)
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
        public override VariableBase<decimal> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        DecimalVariable IDeepCloneable<DecimalVariable>.DeepClone() => new DecimalVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:DateTime" />.
    /// </summary>
    public class DateTimeVariable : VariableBase<DateTime>, IDeepCloneable<DateTimeVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DateTimeVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DateTimeVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public DateTimeVariable(string name, DateTime value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DateTimeVariable(string name, DateTime value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DateTimeVariable" /> to <see cref="T:DateTime" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator DateTime(DateTimeVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="DateTimeVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(DateTimeVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<DateTime> other)
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
        public override VariableBase<DateTime> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        DateTimeVariable IDeepCloneable<DateTimeVariable>.DeepClone() => new DateTimeVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:bool" />.
    /// </summary>
    public class BooleanVariable : VariableBase<bool>, IDeepCloneable<BooleanVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public BooleanVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public BooleanVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public BooleanVariable(string name, bool value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public BooleanVariable(string name, bool value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="BooleanVariable" /> to <see cref="T:bool" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator bool(BooleanVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="BooleanVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(BooleanVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<bool> other)
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
        public override VariableBase<bool> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        BooleanVariable IDeepCloneable<BooleanVariable>.DeepClone() => new BooleanVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:TimeSpan" />.
    /// </summary>
    public class TimeSpanVariable : VariableBase<TimeSpan>, IDeepCloneable<TimeSpanVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TimeSpanVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public TimeSpanVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public TimeSpanVariable(string name, TimeSpan value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public TimeSpanVariable(string name, TimeSpan value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="TimeSpanVariable" /> to <see cref="T:TimeSpan" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator TimeSpan(TimeSpanVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="TimeSpanVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(TimeSpanVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<TimeSpan> other)
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
        public override VariableBase<TimeSpan> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        TimeSpanVariable IDeepCloneable<TimeSpanVariable>.DeepClone() => new TimeSpanVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:string" />.
    /// </summary>
    public class StringVariable : VariableBase<string>, IDeepCloneable<StringVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public StringVariable(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public StringVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public StringVariable(string name, string value)
            : base(name)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public StringVariable(string name, string value, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => this.Value == default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="StringVariable" /> to <see cref="T:string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(StringVariable value) => value?.RawDebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<string> other)
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
        public override VariableBase<string> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        StringVariable IDeepCloneable<StringVariable>.DeepClone() => new StringVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:byte[]" />.
    /// </summary>
    public class ByteArrayVariable : VariableBase<byte[]>, IDeepCloneable<ByteArrayVariable>
    {
        private Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ByteArrayVariable(string name)
            : base(name)
        {
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(string name, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public ByteArrayVariable(string name, byte[] value)
            : base(name)
        {
            this.encoding = Encoding.UTF8;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(string name, byte[] value, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = Encoding.UTF8;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        public ByteArrayVariable(string name, Encoding encoding)
            : base(name)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(string name, Encoding encoding, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        public ByteArrayVariable(string name, byte[] value, Encoding encoding)
            : base(name)
        {
            this.encoding = encoding;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(string name, byte[] value, Encoding encoding, SynchronizationContext synchronizationContext)
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
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public override bool IsDefault => (this.Value?.Length ?? 0) == 0;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ByteArrayVariable" /> to <see cref="T:byte[]" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator byte[](ByteArrayVariable value) => value?.RawDebuggerValue ?? default;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ByteArrayVariable" /> to <see cref="string" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(ByteArrayVariable value) => value?.DebuggerValue ?? default;

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
        /// <returns><c>true</c> if the variables are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(IVariable<byte[]> other)
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
        public override VariableBase<byte[]> DeepClone() => this.DeepClone();

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        ByteArrayVariable IDeepCloneable<ByteArrayVariable>.DeepClone() => new ByteArrayVariable(this.Name, this.InternalValue, this.SynchronizationContext);
    }
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
}