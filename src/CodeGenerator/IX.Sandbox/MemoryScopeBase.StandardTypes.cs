// <copyright file="MemoryScopeBase.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.Abstractions.Memory;
using IX.StandardExtensions;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A memory scope.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
    /// <seealso cref="IX.Abstractions.Memory.IScope" />
    public abstract partial class MemoryScopeBase
    {
        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedByteVariable(string name, byte value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedByteVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedByteVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateByteVariable(byte value)
        {
            var newVariable = new ByteVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedSignedByteVariable(string name, sbyte value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedSignedByteVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedSignedByteVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateSignedByteVariable(sbyte value)
        {
            var newVariable = new SignedByteVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedShortVariable(string name, short value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedShortVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedShortVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateShortVariable(short value)
        {
            var newVariable = new ShortVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedUnsignedShortVariable(string name, ushort value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedUnsignedShortVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedUnsignedShortVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateUnsignedShortVariable(ushort value)
        {
            var newVariable = new UnsignedShortVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedCharVariable(string name, char value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedCharVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedCharVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateCharVariable(char value)
        {
            var newVariable = new CharVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedIntVariable(string name, int value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedIntVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedIntVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateIntVariable(int value)
        {
            var newVariable = new IntVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedUnsignedIntVariable(string name, uint value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedUnsignedIntVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedUnsignedIntVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateUnsignedIntVariable(uint value)
        {
            var newVariable = new UnsignedIntVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedLongVariable(string name, long value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedLongVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedLongVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateLongVariable(long value)
        {
            var newVariable = new LongVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedUnsignedLongVariable(string name, ulong value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedUnsignedLongVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedUnsignedLongVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateUnsignedLongVariable(ulong value)
        {
            var newVariable = new UnsignedLongVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedFloatVariable(string name, float value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedFloatVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedFloatVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateFloatVariable(float value)
        {
            var newVariable = new FloatVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedDoubleVariable(string name, double value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedDoubleVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedDoubleVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateDoubleVariable(double value)
        {
            var newVariable = new DoubleVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedDecimalVariable(string name, decimal value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedDecimalVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedDecimalVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateDecimalVariable(decimal value)
        {
            var newVariable = new DecimalVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedDateTimeVariable(string name, DateTime value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedDateTimeVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedDateTimeVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateDateTimeVariable(DateTime value)
        {
            var newVariable = new DateTimeVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedBooleanVariable(string name, bool value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedBooleanVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedBooleanVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateBooleanVariable(bool value)
        {
            var newVariable = new BooleanVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedTimeSpanVariable(string name, TimeSpan value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedTimeSpanVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedTimeSpanVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateTimeSpanVariable(TimeSpan value)
        {
            var newVariable = new TimeSpanVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedStringVariable(string name, string value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedStringVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedStringVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateStringVariable(string value)
        {
            var newVariable = new StringVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }

        /// <summary>
        /// Creates a named variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public INamedVariable CreateNamedByteArrayVariable(string name, byte[] value)
            => this.variables.CreateOrChangeState(
                name,
                (nameL1, valueL1, scL1) => new NamedByteArrayVariable(nameL1, valueL1, scL1),
                (var, nameL1, valueL1, scL1) =>
                {
                    if (!(var is NamedByteArrayVariable nbv))
                    {
                        throw new ArgumentInvalidTypeException(nameof(name));
                    }
                    else
                    {
                        nbv.Value = valueL1;
                    }
                },
                name,
                value,
                this.SynchronizationContext);

        /// <summary>
        /// Creates an unnamed variable, or reassigns if one is found with the same name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The named variable, new or existing.</returns>
        public IVariable CreateByteArrayVariable(byte[] value)
        {
            var newVariable = new ByteArrayVariable(value, this.SynchronizationContext);

            this.unnamedVariables.Add(newVariable);

            return newVariable;
        }
    }
}