// <copyright file="NamedListVariableBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IX.Abstractions.Memory;
using IX.Observable;
using IX.StandardExtensions;
using IX.StandardExtensions.ComponentModel;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A named list variable
    /// </summary>
    /// <typeparam name="T">The type of item in the list.</typeparam>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
    /// <seealso cref="IX.Abstractions.Memory.INamedVariable" />
    public abstract class NamedListVariableBase<T> : ViewModelBase, INamedCollectionVariable<T>, IComparable<INamedCollectionVariable<T>>, IEquatable<INamedCollectionVariable<T>>
    {
        private readonly IScope parentScope;

        private ConcurrentObservableList<IVariable<T>> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedListVariableBase{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parentScope">The parent scope.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parentScope"/>
        /// or
        /// <paramref name="name"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </exception>
        public NamedListVariableBase(string name, IScope parentScope)
            : base()
        {
            this.parentScope = parentScope ?? throw new ArgumentNullException(nameof(parentScope));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));

            this.items = new ConcurrentObservableList<IVariable<T>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedListVariableBase{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parentScope">The parent scope.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parentScope"/>
        /// or
        /// <paramref name="name"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </exception>
        public NamedListVariableBase(string name, IScope parentScope, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.parentScope = parentScope ?? throw new ArgumentNullException(nameof(parentScope));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));

            this.items = new ConcurrentObservableList<IVariable<T>>(synchronizationContext, true);
        }

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        /// <value>The name of the variable.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public IEnumerable<T> Value
        {
            get
            {
                foreach (IVariable<T> item in this.items)
                {
                    yield return item.Value;
                }
            }

            set
            {
                IVariable<T>[] oldItems = this.items.ClearAndPersist();

                this.items.AddRange(value.Select(p => this.parentScope.CreateVariable(p)));

                this.FireAndForget((oi) => oi.ForEach<IVariable>(p => this.parentScope.DisposeVariable(ref p)), oldItems);
            }
        }

        /// <summary>
        /// Gets or sets the raw debugger value.
        /// </summary>
        /// <value>The raw debugger value.</value>
        public IEnumerable<T> RawDebuggerValue
        {
            get => this.Value;
            set => this.Value = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        public abstract string DebuggerValue { get; set; }

        /// <summary>
        /// Gets or sets the boxed value.
        /// </summary>
        /// <value>The boxed value.</value>
        public abstract object BoxedValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public bool IsDefault => this.items.Count == 0;

        /// <summary>
        /// Compares the list with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns>The logical difference between the two variables.</returns>
        public int CompareTo(IVariable<IEnumerable<T>> other)
        {
            if (other is INamedCollectionVariable<T> ncv)
            {
                return this.CompareTo(ncv);
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Compares the list with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns>The logical difference between the two variables.</returns>
        public int CompareTo(INamedVariable<IEnumerable<T>> other)
        {
            if (other is INamedCollectionVariable<T> ncv)
            {
                return this.CompareTo(ncv);
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Compares the list with another list.
        /// </summary>
        /// <param name="other">The list to compare to.</param>
        /// <returns>The logical difference between the two lists.</returns>
        public int CompareTo(INamedCollectionVariable<T> other)
        {
            if (other == null)
            {
                return 1;
            }

            var nameComparison = this.Name.CompareTo(other.Name);
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return this.CompareToInternal(other);
        }

        /// <summary>
        /// Equates the list with another variable.
        /// </summary>
        /// <param name="other">The variable to equate to.</param>
        /// <returns><c>true</c> if the two variables are equal, <c>false</c> otherwise.</returns>
        public bool Equals(IVariable<IEnumerable<T>> other)
        {
            if (other is INamedCollectionVariable<T> ncv)
            {
                return this.Equals(ncv);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equates the list with another variable.
        /// </summary>
        /// <param name="other">The variable to equate to.</param>
        /// <returns><c>true</c> if the two variables are equal, <c>false</c> otherwise.</returns>
        public bool Equals(INamedVariable<IEnumerable<T>> other)
        {
            if (other is INamedCollectionVariable<T> ncv)
            {
                return this.Equals(ncv);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equates the list with another list.
        /// </summary>
        /// <param name="other">The list to equate to.</param>
        /// <returns><c>true</c> if the two lists are equal, <c>false</c> otherwise.</returns>
        public bool Equals(INamedCollectionVariable<T> other)
        {
            if (other == null)
            {
                return false;
            }

            var nameComparison = this.Name.Equals(other.Name);
            if (!nameComparison)
            {
                return false;
            }

            return this.EqualsInternal(other);
        }

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <returns>Type.</returns>
        public Type GetDataType() => typeof(IEnumerable<T>);

        /// <summary>
        /// Compares the list with another list.
        /// </summary>
        /// <param name="other">The list to compare to.</param>
        /// <returns>The logical difference between the two lists.</returns>
        protected abstract int CompareToInternal(INamedCollectionVariable<T> other);

        /// <summary>
        /// Equates the list with another list.
        /// </summary>
        /// <param name="other">The list to equate to.</param>
        /// <returns><c>true</c> if the two lists are equal, <c>false</c> otherwise.</returns>
        protected abstract bool EqualsInternal(INamedCollectionVariable<T> other);
    }
}