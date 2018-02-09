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
    public abstract partial class MemoryScopeBase : NotifyPropertyChangedBase, IScope
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScopeBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
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
        protected MemoryScopeBase(string id, IScope parent)
        {
            this.InitializeInternalContext(id, parent);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScopeBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="synchronizationContext">The synchronization context.</param>
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
        protected MemoryScopeBase(string id, IScope parent, SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.InitializeInternalContext(id, parent);
        }

        /// <summary>
        /// Gets the identifier of the scope.
        /// </summary>
        /// <value>The scope identifier.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the parent of this scope, if one exists.
        /// </summary>
        /// <value>The parent scope.</value>
        public IScope Parent { get; private set; }

        /// <summary>
        /// Gets the variables contained in this scope.
        /// </summary>
        /// <value>The variables contained in this scope.</value>
        public ConcurrentObservableDictionary<string, INamedVariable> Variables => this.variables;

        /// <summary>
        /// Gets the sub-scopes of this scope.
        /// </summary>
        /// <value>The sub-scopes.</value>
        protected ConcurrentObservableDictionary<string, IScope> SubScopes => this.subScopes;

        /// <summary>
        /// Gets the unnamed variables contained in this scope.
        /// </summary>
        /// <value>The unnamed variables.</value>
        protected ConcurrentObservableList<IVariable> UnnamedVariables => this.unnamedVariables;

        /// <summary>
        /// Creates a variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The new variable, if one has been created.</returns>
        public abstract INamedVariable<T> CreateVariable<T>(string name);

        /// <summary>
        /// Creates an unnamed variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The new variable, if one has been created.</returns>
        public abstract IVariable<T> CreateVariable<T>(T value);

        /// <summary>
        /// Creates a variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value.</param>
        /// <returns>The new variable, if one has been created.</returns>
        public abstract INamedVariable<T> CreateVariable<T>(string name, T value);

        /// <summary>
        /// Disposes a variable by name.
        /// </summary>
        /// <param name="name">The name  of the variable.</param>
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

        /// <summary>
        /// Disposes an unnamed variable by reference.
        /// </summary>
        /// <param name="variable">The variable, by reference.</param>
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
        public abstract IScope InitiateSubScope(string id);

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
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
        }

        private void InitializeInternalContext(string id, IScope parent)
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
            this.variables = new ConcurrentObservableDictionary<string, INamedVariable>(true);
            this.unnamedVariables = new ConcurrentObservableList<IVariable>(true);
            this.subScopes = new ConcurrentObservableDictionary<string, IScope>(true);
        }
    }
}