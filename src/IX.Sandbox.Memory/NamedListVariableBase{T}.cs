// <copyright file="NamedListVariableBase{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
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
    /// A named list variable.
    /// </summary>
    /// <typeparam name="T">The type of item in the list.</typeparam>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
    /// <seealso cref="IX.Abstractions.Memory.INamedVariable" />
    [Obsolete("This class is about to be removed, please do not use it anymore.")]
    public abstract class NamedListVariableBase<T> : ViewModelBase, INamedCollectionVariable<T>, IComparable<INamedCollectionVariable<T>>, IEquatable<INamedCollectionVariable<T>>
    {
        private readonly IScope parentScope;

#pragma warning disable IDISP002 // Dispose member.
#pragma warning disable IDISP006 // Implement IDisposable.
        private readonly ConcurrentObservableList<IVariable<T>> items;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.

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
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public string Name { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public IEnumerable<T> Value
        {
            get
            {
                foreach (IVariable<T> item in this.items)
                {
                    yield return item.Value;
                }
            }

#pragma warning disable HAA0302 // Display class allocation to capture closure
            set
#pragma warning restore HAA0302 // Display class allocation to capture closure
            {
                IVariable<T>[] oldItems = this.items.ClearAndPersist();

#pragma warning disable HAA0301 // Closure Allocation Source
                this.items.AddRange(value.Select(p => this.parentScope.CreateVariable(p)));

                this.FireAndForget((oi) => oi.ForEach<IVariable>(p => this.parentScope.DisposeVariable(ref p)), oldItems);
#pragma warning restore HAA0301 // Closure Allocation Source
            }
        }

        /// <summary>
        /// Gets or sets the raw debugger value.
        /// </summary>
        /// <value>The raw debugger value.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public IEnumerable<T> RawDebuggerValue
        {
            get => this.Value;
            set => this.Value = value;
        }

        /// <summary>
        /// Gets or sets the debugger value.
        /// </summary>
        /// <value>The debugger value.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public abstract string DebuggerValue { get; set; }

        /// <summary>
        /// Gets or sets the boxed value.
        /// </summary>
        /// <value>The boxed value.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public abstract object BoxedValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public bool IsDefault => this.items.Count == 0;

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public int Count => this.items.Count;

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public void Add(T item) => this.items.Add(this.parentScope.CreateVariable(item));

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public void Add(IVariable<T> item) => this.items.Add(item);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
#pragma warning disable HAA0301 // Closure Allocation Source
#pragma warning disable HAA0302 // Display class allocation to capture closure
        public void Clear() => this.FireAndForget((oi, ps) => oi.ForEach<IVariable>(p => ps.DisposeVariable(ref p)), this.items.ClearAndPersist(), this.parentScope);
#pragma warning restore HAA0302 // Display class allocation to capture closure
#pragma warning restore HAA0301 // Closure Allocation Source

        /// <summary>
        /// Compares the list with another variable.
        /// </summary>
        /// <param name="other">The variable to compare to.</param>
        /// <returns>The logical difference between the two variables.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public abstract bool Contains(T item);

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public bool Contains(IVariable<T> item) => this.items.Contains(item);

        /// <summary>
        /// Copies the raw values of the items contained in the collection to an array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">Index of the array to start copying into.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public void CopyTo(T[] array, int arrayIndex) => this.items.Select(p => p.Value).ToList().CopyTo(array, arrayIndex);

        /// <summary>
        /// Copies the items contained in the collection to an array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">Index of the array to start copying into.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public void CopyTo(IVariable<T>[] array, int arrayIndex) => this.items.CopyTo(array, arrayIndex);

        /// <summary>
        /// Equates the list with another variable.
        /// </summary>
        /// <param name="other">The variable to equate to.</param>
        /// <returns><c>true</c> if the two variables are equal, <c>false</c> otherwise.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
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
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public Type GetDataType() => typeof(IEnumerable<T>);

        /// <summary>
        /// Gets the enumerator of raw values for this collection.
        /// </summary>
        /// <returns>the enumerator for this collection.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.items.Select(p => p.Value).GetEnumerator();
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator

        /// <summary>
        /// Removes the specified item value.
        /// </summary>
        /// <param name="item">The item value.</param>
        /// <returns><c>true</c> if the item with the specified value was found and successfully removed, <c>false</c> otherwise.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public abstract bool Remove(T item);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the item has been successfully removed, <c>false</c> otherwise.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public bool Remove(IVariable<T> item) => this.items.Remove(item);

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
        /// <summary>
        /// Gets the enumerator of raw values for this collection.
        /// </summary>
        /// <returns>the enumerator for this collection.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        IEnumerator IEnumerable.GetEnumerator() => this.items.GetEnumerator();

        /// <summary>
        /// Gets the enumerator of raw values for this collection.
        /// </summary>
        /// <returns>the enumerator for this collection.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        IEnumerator<IVariable<T>> IEnumerable<IVariable<T>>.GetEnumerator() => this.items.GetEnumerator();
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator

        /// <summary>
        /// Compares the list with another list.
        /// </summary>
        /// <param name="other">The list to compare to.</param>
        /// <returns>The logical difference between the two lists.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        protected abstract int CompareToInternal(INamedCollectionVariable<T> other);

        /// <summary>
        /// Equates the list with another list.
        /// </summary>
        /// <param name="other">The list to equate to.</param>
        /// <returns><c>true</c> if the two lists are equal, <c>false</c> otherwise.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        protected abstract bool EqualsInternal(INamedCollectionVariable<T> other);
    }
}