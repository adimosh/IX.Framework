// <copyright file="SandboxHeap.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.Sandbox.Memory.Exceptions;
using IX.StandardExtensions.ComponentModel;
using IX.StandardExtensions.Efficiency;
using GlobalConcurrentCollections = global::System.Collections.Concurrent;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A memory heap, sandboxed.
    /// </summary>
    public class SandboxHeap : DisposableBase
    {
        // Data
        private ConcurrentDictionary<Guid, SandboxValue> values;
        private ConcurrentDictionary<string, SandboxSymbol> symbols;

        // Factories
        private ConcurrentDictionary<Type, Func<Type, object, SandboxValue>> sandboxValueCreators;
        private GlobalConcurrentCollections.ConcurrentBag<Func<string, SandboxValue[]>> stringValueInterpreters;

        /// <summary>
        /// Initializes a new instance of the <see cref="SandboxHeap"/> class.
        /// </summary>
        public SandboxHeap()
        {
            // Data
            this.values = new ConcurrentDictionary<Guid, SandboxValue>();
            this.symbols = new ConcurrentDictionary<string, SandboxSymbol>();

            // Factories
            this.sandboxValueCreators = new ConcurrentDictionary<Type, Func<Type, object, SandboxValue>>();
            this.stringValueInterpreters = new GlobalConcurrentCollections.ConcurrentBag<Func<string, SandboxValue[]>>();
        }

        private static Func<Type, object, SandboxValue> UpdateFromState(Type type, object oldValue, Func<Type, object, SandboxValue> state) => state;

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is OK
        /// <summary>Registers a sandbox value creation function.</summary>
        /// <param name="containedDataType">Type of the contained data.</param>
        /// <param name="sandboxValueCreator">The sandbox value creator.</param>
        /// <exception cref="ArgumentNullException"><paramref name="containedDataType"/>
        /// or
        /// <paramref name="sandboxValueCreator"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        /// <remarks>
        /// <para>The registered function gets the contained type as the first parameter. It is, therefore, possible to register multiple types with the same function.</para>
        /// <para>The resulting <see cref="SandboxValue.Id"/> of the resulting value should not be set, as it will be overwritten after the creation function is invoked.</para>
        /// </remarks>
        public void RegisterCreationFunction(Type containedDataType, Func<Type, object, SandboxValue> sandboxValueCreator) =>
            this.sandboxValueCreators.AddOrUpdate(
                containedDataType ?? throw new ArgumentNullException(nameof(containedDataType)),
                sandboxValueCreator ?? throw new ArgumentNullException(nameof(sandboxValueCreator)),
                UpdateFromState,
                sandboxValueCreator);

        /// <summary>Registers the creation function.</summary>
        /// <typeparam name="T">The type of contained data this function is for.</typeparam>
        /// <param name="sandboxValueCreator">The sandbox value creator.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sandboxValueCreator"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        /// <remarks>
        /// <para>The registered function gets the contained type as the first parameter. It is, therefore, possible to register multiple types with the same function.</para>
        /// <para>The resulting <see cref="SandboxValue.Id"/> of the resulting value should not be set, as it will be overwritten after the creation function is invoked.</para>
        /// </remarks>
        public void RegisterCreationFunction<T>(Func<Type, object, SandboxValue> sandboxValueCreator) =>
            this.sandboxValueCreators.AddOrUpdate(
                typeof(T),
                sandboxValueCreator ?? throw new ArgumentNullException(nameof(sandboxValueCreator)),
                UpdateFromState,
                sandboxValueCreator);
#pragma warning restore HAA0603 // Delegate allocation from a method group

        /// <summary>
        /// Registers a string interpretation function.
        /// </summary>
        /// <param name="sandboxValueCreator">The sandbox value creator.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sandboxValueCreator"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public void RegisterStringInterpretationFunction(Func<string, SandboxValue[]> sandboxValueCreator) =>
            this.stringValueInterpreters.Add(sandboxValueCreator ?? throw new ArgumentNullException(nameof(sandboxValueCreator)));

        /// <summary>
        /// Creates the symbol.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>SingleValueDynamicSandboxSymbol.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        /// <exception cref="InvalidOperationException">Ading the symbol to the heap was prevented.</exception>
        public SingleValueDynamicSandboxSymbol CreateSymbol(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var symbol = new SingleValueDynamicSandboxSymbol(this, name);

            if (!this.symbols.TryAdd(name, symbol))
            {
                throw new InvalidOperationException(Resources.TheSymbolWasAlreadyCreated);
            }

            return symbol;
        }

        /// <summary>
        /// Gets a symbol by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>SandboxSymbol.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public SandboxSymbol GetSymbol(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (this.symbols.TryGetValue(name, out SandboxSymbol value))
            {
                return value;
            }

            return null;
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is unavoidable, and the value will be heap-bound anyway
        /// <summary>
        /// Creates a sandbox value.
        /// </summary>
        /// <typeparam name="T">The final type of data contained in the sandbox value.</typeparam>
        /// <param name="value">The contained value.</param>
        /// <returns>A sandbox value.</returns>
        internal SandboxValue CreateSandboxValue<T>(T value) => this.CreateSandboxValue(typeof(T), value);
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

        /// <summary>
        /// Creates a sandbox value.
        /// </summary>
        /// <param name="type">The type of the contained value.</param>
        /// <param name="value">The contained value.</param>
        /// <returns>A sandbox value.</returns>
        /// <exception cref="IX.Sandbox.Memory.Exceptions.ValueTypeNotSupportedException">The value type is not supported.</exception>
        /// <exception cref="InvalidOperationException">Ading the value to the heap was prevented.</exception>
        internal SandboxValue CreateSandboxValue(Type type, object value)
        {
            if (!this.sandboxValueCreators.TryGetValue(type, out Func<Type, object, SandboxValue> creator))
            {
                throw new ValueTypeNotSupportedException();
            }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is unavoidable, and the value will be heap-bound anyway
            SandboxValue val = creator(type, value);
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

            if (this.values.TryAdd(val.Id, val))
            {
                throw new InvalidOperationException();
            }

            return val;
        }

        internal SandboxValue[] CreateSandboxValue(string sourceExpression)
        {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is expected
            foreach (Func<string, SandboxValue[]> sandboxValueCreator in this.stringValueInterpreters)
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
            {
                SandboxValue[] result = sandboxValueCreator(sourceExpression);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        internal void CaptureSandboxValue(Guid id)
        {
            if (id == Guid.Empty || !this.values.TryGetValue(id, out SandboxValue val))
            {
                throw new InvalidObjectKeyException();
            }

            val.CaptureOne();
        }

        internal void ReleaseSandboxValue(Guid id)
        {
            if (id == Guid.Empty || !this.values.TryGetValue(id, out SandboxValue val))
            {
                throw new InvalidObjectKeyException();
            }

            val.ReleaseOne();
        }

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            // Dispose symbols
            ConcurrentDictionary<string, SandboxSymbol> oldSymbols = Interlocked.Exchange(ref this.symbols, null);

            if (oldSymbols != null)
            {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Currently acceptable
                foreach (SandboxSymbol o in oldSymbols.Values)
                {
                    o.Dispose();
                }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator

                oldSymbols.Clear();
            }

            // Dispose old values
            ConcurrentDictionary<Guid, SandboxValue> oldValues = Interlocked.Exchange(ref this.values, null);

            if (oldValues != null)
            {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Currently acceptable
                foreach (SandboxValue o in oldValues.Values)
                {
                    o.Dispose();
                }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator

                oldValues.Clear();
            }

            // Clean methods
            Interlocked.Exchange(ref this.sandboxValueCreators, null)?.Clear();

            // Clean string methods
            Interlocked.Exchange(ref this.stringValueInterpreters, null);

            base.DisposeManagedContext();
        }
    }
}