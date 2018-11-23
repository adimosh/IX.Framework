// <copyright file="MemoryScopeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using IX.Abstractions.Memory;
using IX.Observable;
using IX.StandardExtensions;
using IX.StandardExtensions.ComponentModel;

namespace IX.Sandbox.Memory
{
    /// <summary>
    /// A memory scope.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
    /// <seealso cref="IX.Abstractions.Memory.IScope" />
    [Obsolete("This class is about to be removed, please do not use it anymore.")]
    public abstract partial class MemoryScopeBase : NotifyPropertyChangedBase, IScope
    {
#pragma warning disable IDISP002 // Dispose member.
#pragma warning disable IDISP006 // Implement IDisposable.
        /// <summary>
        /// The named variables container.
        /// </summary>
        private ConcurrentObservableDictionary<string, INamedVariable> variables;

        /// <summary>
        /// The unnamed variables container.
        /// </summary>
        private ConcurrentObservableList<IVariable> unnamedVariables;

        /// <summary>
        /// The sub scopes container.
        /// </summary>
        private ConcurrentObservableDictionary<string, IScope> subScopes;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScopeBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        protected MemoryScopeBase(string id)
            : this(id, (IScope)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScopeBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="parent">The parent.</param>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        protected MemoryScopeBase(string id, IScope parent)
        {
            this.InitializeInternalContext(id, parent);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScopeBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        protected MemoryScopeBase(string id, SynchronizationContext synchronizationContext)
            : this(id, null, synchronizationContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScopeBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
        /// <exception cref="ArgumentNullException"><paramref name="id" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        protected MemoryScopeBase(string id, IScope parent, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.InitializeInternalContext(id, parent);
        }

        /// <summary>
        /// Gets the identifier of the scope.
        /// </summary>
        /// <value>The scope identifier.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public string Id { get; private set; }

        /// <summary>
        /// Gets the parent of this scope, if one exists.
        /// </summary>
        /// <value>The parent scope.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public IScope Parent { get; private set; }

        /// <summary>
        /// Gets the variables contained in this scope.
        /// </summary>
        /// <value>The variables contained in this scope.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        public ConcurrentObservableDictionary<string, INamedVariable> Variables => this.variables;

        /// <summary>
        /// Gets the sub-scopes of this scope.
        /// </summary>
        /// <value>The sub-scopes.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        protected ConcurrentObservableDictionary<string, IScope> SubScopes => this.subScopes;

        /// <summary>
        /// Gets the unnamed variables contained in this scope.
        /// </summary>
        /// <value>The unnamed variables.</value>
        [Obsolete("The class that this property belongs to is about to be removed, please do not use it anymore.")]
        protected ConcurrentObservableList<IVariable> UnnamedVariables => this.unnamedVariables;

        /// <summary>
        /// Creates a variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The new variable, if one has been created.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public abstract INamedVariable<T> CreateVariable<T>(string name);

        /// <summary>
        /// Creates an unnamed variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The new variable, if one has been created.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public abstract IVariable<T> CreateVariable<T>(T value);

        /// <summary>
        /// Creates a variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value.</param>
        /// <returns>The new variable, if one has been created.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public abstract INamedVariable<T> CreateVariable<T>(string name, T value);

#pragma warning disable HAA0302 // Display class allocation to capture closure
#pragma warning disable HAA0301 // Closure Allocation Source
        /// <summary>
        /// Disposes a variable by name.
        /// </summary>
        /// <param name="name">The name  of the variable.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public virtual void DisposeVariable(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.ThrowIfCurrentObjectDisposed();

            this.variables.RemoveThenAct(name, storedVariable => this.FireAndForget((st) => (st as IDisposable).Dispose(), storedVariable));
        }

        /// <summary>
        /// Disposes a named variable by reference.
        /// </summary>
        /// <param name="variable">The variable, by reference.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public virtual void DisposeVariable(ref INamedVariable variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException(nameof(variable));
            }

            this.ThrowIfCurrentObjectDisposed();

            if (this.variables.RemoveThenAct(variable.Name, storedVariable => this.FireAndForget((st) => (st as IDisposable).Dispose(), storedVariable)))
            {
                variable = null;
            }
        }
#pragma warning restore HAA0301 // Closure Allocation Source
#pragma warning restore HAA0302 // Display class allocation to capture closure

        /// <summary>
        /// Disposes an unnamed variable by reference.
        /// </summary>
        /// <param name="variable">The variable, by reference.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public virtual void DisposeVariable(ref IVariable variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException(nameof(variable));
            }

            this.ThrowIfCurrentObjectDisposed();

            if (this.unnamedVariables.Remove(variable))
            {
                IVariable storedVariable = variable;
                this.FireAndForget((st) => (st as IDisposable).Dispose(), storedVariable);
                variable = null;
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public bool Equals(IScope other)
        {
            if (other == null)
            {
                return false;
            }

            return ReferenceEquals(this, other);
        }

        /// <summary>
        /// Finalizes a scope that lives under the current scope by identifier.
        /// </summary>
        /// <param name="id">The scope identifier.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public virtual void FinalizeSubScope(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.ThrowIfCurrentObjectDisposed();

            if (this.subScopes.TryGetValue(id, out IScope subScope))
            {
                this.subScopes.Remove(id);

                this.FireAndForget((st) => (st as IDisposable).Dispose(), subScope);
            }
        }

        /// <summary>
        /// Finalizes a scope that lives under the current scope by reference.
        /// </summary>
        /// <param name="subScope">The scope reference.</param>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public virtual void FinalizeSubScope(ref IScope subScope)
        {
            if (subScope == null)
            {
                throw new ArgumentNullException(nameof(subScope));
            }

            this.ThrowIfCurrentObjectDisposed();

            var id = subScope.Id;
            if (this.subScopes.TryGetValue(id, out IScope storedScope))
            {
                if (subScope != storedScope)
                {
                    throw new InvalidOperationException(Resources.SubScopeDifferentThanStored);
                }

                this.subScopes.Remove(id);

                subScope = null;

                this.FireAndForget((st) => (st as IDisposable).Dispose(), storedScope);
            }
            else
            {
                throw new InvalidOperationException(Resources.SubScopeNotFound);
            }
        }

        /// <summary>
        /// Initiates a scope under the current scope.
        /// </summary>
        /// <param name="id">The identifier of the new scope.</param>
        /// <returns>The new scope.</returns>
        [Obsolete("The class that this method belongs to is about to be removed, please do not use it anymore.")]
        public abstract IScope InitiateSubScope(string id);

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
#pragma warning disable ERP022 // Catching everything considered harmful.
            // Dispose sub-scopes
            KeyValuePair<string, IScope>[] scopes = this.subScopes.ClearAndPersist();

            this.FireAndForget(
                (st) => st.ForEach(p =>
                {
                    try
                    {
                        p.Value?.Dispose();
                    }
                    catch
                    {
                    }
                }),
                scopes);

            // Dispose named variables
            KeyValuePair<string, INamedVariable>[] variables = this.variables.ClearAndPersist();

            this.FireAndForget(
                (st) => st.ForEach(p =>
                {
                    try
                    {
                        p.Value?.Dispose();
                    }
                    catch
                    {
                    }
                }),
                variables);

            // Dispose unnamed variables
            IVariable[] unnamedVariables = this.unnamedVariables.ClearAndPersist();

            this.FireAndForget(
                (st) => st.ForEach(p =>
                {
                    try
                    {
                        p?.Dispose();
                    }
                    catch
                    {
                    }
                }),
                unnamedVariables);

            // Dispose base
            base.DisposeManagedContext();
#pragma warning restore ERP022 // Catching everything considered harmful.
        }

        private void InitializeInternalContext(in string id, in IScope parent)
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            // Set parameters
            this.Id = id;
            this.Parent = parent;

            // Initialize internal scope
#pragma warning disable IDISP003 // Dispose previous before re-assigning.
            this.variables = new ConcurrentObservableDictionary<string, INamedVariable>(true);
            this.unnamedVariables = new ConcurrentObservableList<IVariable>(true);
            this.subScopes = new ConcurrentObservableDictionary<string, IScope>(true);
#pragma warning restore IDISP003 // Dispose previous before re-assigning.
        }
    }
}