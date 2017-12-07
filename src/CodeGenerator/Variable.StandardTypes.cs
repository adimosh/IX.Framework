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
    public class ByteVariable : VariableBase<byte>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:sbyte" />.
    /// </summary>
    public class SignedByteVariable : VariableBase<sbyte>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:short" />.
    /// </summary>
    public class ShortVariable : VariableBase<short>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ushort" />.
    /// </summary>
    public class UnsignedShortVariable : VariableBase<ushort>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:char" />.
    /// </summary>
    public class CharVariable : VariableBase<char>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:int" />.
    /// </summary>
    public class IntVariable : VariableBase<int>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:uint" />.
    /// </summary>
    public class UnsignedIntVariable : VariableBase<uint>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:long" />.
    /// </summary>
    public class LongVariable : VariableBase<long>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ulong" />.
    /// </summary>
    public class UnsignedLongVariable : VariableBase<ulong>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:float" />.
    /// </summary>
    public class FloatVariable : VariableBase<float>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:double" />.
    /// </summary>
    public class DoubleVariable : VariableBase<double>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:decimal" />.
    /// </summary>
    public class DecimalVariable : VariableBase<decimal>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:DateTime" />.
    /// </summary>
    public class DateTimeVariable : VariableBase<DateTime>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:bool" />.
    /// </summary>
    public class BooleanVariable : VariableBase<bool>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:TimeSpan" />.
    /// </summary>
    public class TimeSpanVariable : VariableBase<TimeSpan>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:string" />.
    /// </summary>
    public class StringVariable : VariableBase<string>
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:byte[]" />.
    /// </summary>
    public class ByteArrayVariable : VariableBase<byte[]>
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
        /// <param name="encoding">The enoding to use when converting to/from strings.</param>
        public ByteArrayVariable(string name, Encoding encoding)
            : base(name)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="encoding">The enoding to use when converting to/from strings.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(string name, Encoding encoding, SynchronizationContext synchronizationContext)
            : base(name, synchronizationContext)
        {
            this.encoding = encoding;
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
        /// <returns><c>0</c> if the two are equal, a diffrent value if not.</returns>
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
    }
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
}