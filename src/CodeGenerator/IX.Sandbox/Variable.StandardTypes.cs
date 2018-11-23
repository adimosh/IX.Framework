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
        public ByteVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ByteVariable(byte value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteVariable(byte value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public ByteVariable DeepClone() => new ByteVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<byte> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:sbyte" />.
    /// </summary>
    public class SignedByteVariable : VariableBase<sbyte>, IDeepCloneable<SignedByteVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        public SignedByteVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public SignedByteVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public SignedByteVariable(sbyte value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedByteVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public SignedByteVariable(sbyte value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public SignedByteVariable DeepClone() => new SignedByteVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<sbyte> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:short" />.
    /// </summary>
    public class ShortVariable : VariableBase<short>, IDeepCloneable<ShortVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        public ShortVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ShortVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ShortVariable(short value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ShortVariable(short value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public ShortVariable DeepClone() => new ShortVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<short> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ushort" />.
    /// </summary>
    public class UnsignedShortVariable : VariableBase<ushort>, IDeepCloneable<UnsignedShortVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        public UnsignedShortVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedShortVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public UnsignedShortVariable(ushort value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedShortVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedShortVariable(ushort value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public UnsignedShortVariable DeepClone() => new UnsignedShortVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<ushort> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:char" />.
    /// </summary>
    public class CharVariable : VariableBase<char>, IDeepCloneable<CharVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        public CharVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public CharVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public CharVariable(char value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public CharVariable(char value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public CharVariable DeepClone() => new CharVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<char> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:int" />.
    /// </summary>
    public class IntVariable : VariableBase<int>, IDeepCloneable<IntVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        public IntVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public IntVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public IntVariable(int value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public IntVariable(int value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public IntVariable DeepClone() => new IntVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<int> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:uint" />.
    /// </summary>
    public class UnsignedIntVariable : VariableBase<uint>, IDeepCloneable<UnsignedIntVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        public UnsignedIntVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedIntVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public UnsignedIntVariable(uint value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedIntVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedIntVariable(uint value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public UnsignedIntVariable DeepClone() => new UnsignedIntVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<uint> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:long" />.
    /// </summary>
    public class LongVariable : VariableBase<long>, IDeepCloneable<LongVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        public LongVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public LongVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public LongVariable(long value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public LongVariable(long value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public LongVariable DeepClone() => new LongVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<long> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:ulong" />.
    /// </summary>
    public class UnsignedLongVariable : VariableBase<ulong>, IDeepCloneable<UnsignedLongVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        public UnsignedLongVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedLongVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public UnsignedLongVariable(ulong value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedLongVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public UnsignedLongVariable(ulong value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public UnsignedLongVariable DeepClone() => new UnsignedLongVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<ulong> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:float" />.
    /// </summary>
    public class FloatVariable : VariableBase<float>, IDeepCloneable<FloatVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        public FloatVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public FloatVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public FloatVariable(float value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public FloatVariable(float value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public FloatVariable DeepClone() => new FloatVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<float> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:double" />.
    /// </summary>
    public class DoubleVariable : VariableBase<double>, IDeepCloneable<DoubleVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        public DoubleVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DoubleVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public DoubleVariable(double value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DoubleVariable(double value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public DoubleVariable DeepClone() => new DoubleVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<double> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:decimal" />.
    /// </summary>
    public class DecimalVariable : VariableBase<decimal>, IDeepCloneable<DecimalVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        public DecimalVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DecimalVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public DecimalVariable(decimal value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DecimalVariable(decimal value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public DecimalVariable DeepClone() => new DecimalVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<decimal> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:DateTime" />.
    /// </summary>
    public class DateTimeVariable : VariableBase<DateTime>, IDeepCloneable<DateTimeVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        public DateTimeVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DateTimeVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public DateTimeVariable(DateTime value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public DateTimeVariable(DateTime value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public DateTimeVariable DeepClone() => new DateTimeVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<DateTime> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:bool" />.
    /// </summary>
    public class BooleanVariable : VariableBase<bool>, IDeepCloneable<BooleanVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        public BooleanVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public BooleanVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public BooleanVariable(bool value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public BooleanVariable(bool value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public BooleanVariable DeepClone() => new BooleanVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<bool> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:TimeSpan" />.
    /// </summary>
    public class TimeSpanVariable : VariableBase<TimeSpan>, IDeepCloneable<TimeSpanVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        public TimeSpanVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public TimeSpanVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public TimeSpanVariable(TimeSpan value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public TimeSpanVariable(TimeSpan value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public TimeSpanVariable DeepClone() => new TimeSpanVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<TimeSpan> DeepCloneImplementation() => this.DeepClone();
    }

    /// <summary>
    /// A variable of discreet type <see cref="T:string" />.
    /// </summary>
    public class StringVariable : VariableBase<string>, IDeepCloneable<StringVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        public StringVariable()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public StringVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringVariable(string value)
            : base()
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public StringVariable(string value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public StringVariable DeepClone() => new StringVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<string> DeepCloneImplementation() => this.DeepClone();
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
        public ByteArrayVariable()
            : base()
        {
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ByteArrayVariable(byte[] value)
            : base()
        {
            this.encoding = Encoding.UTF8;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(byte[] value, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.encoding = Encoding.UTF8;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        public ByteArrayVariable(Encoding encoding)
            : base()
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(Encoding encoding, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        public ByteArrayVariable(byte[] value, Encoding encoding)
            : base()
        {
            this.encoding = encoding;
            this.InternalValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayVariable"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding to use when converting to/from strings.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        public ByteArrayVariable(byte[] value, Encoding encoding, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
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

            return other.RawDebuggerValue.Equals(this.RawDebuggerValue);
        }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public ByteArrayVariable DeepClone() => new ByteArrayVariable(this.InternalValue, this.SynchronizationContext);

        /// <summary>
        /// Creates a deep clone of the source object. This method implements the actual operation.
        /// </summary>
        /// <returns>A deep clone.</returns>
        protected sealed override VariableBase<byte[]> DeepCloneImplementation() => this.DeepClone();
    }
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
}