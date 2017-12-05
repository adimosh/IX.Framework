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

namespace IX.MemorySandbox
{
    /// <summary>
    /// A memory scope.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
    /// <seealso cref="IX.Abstractions.Memory.IScope" />
    public abstract class MemoryScopeBase : NotifyPropertyChangedBase, IScope
    {
        /// <summary>
        /// The variables container.
        /// </summary>
        private ConcurrentObservableDictionary<string, IVariable> variables;

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
        /// Gets the variables contained in this space.
        /// </summary>
        /// <value>The variables contained in this scope.</value>
        public ObservableDictionary<string, IVariable> Variables => this.variables;

        /// <summary>
        /// Gets the sub-scopes of this scope.
        /// </summary>
        /// <value>The sub-scopes.</value>
        protected ObservableDictionary<string, IScope> SubScopes => this.subScopes;

        /// <summary>
        /// Creates a variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The new variable, if one has been created.</returns>
        public abstract IVariable<T> CreateVariable<T>(string name);

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

            if (this.variables.TryGetValue(name, out IVariable storedVariable))
            {
                this.variables.Remove(name);

                this.FireAndForget((st) => (st as IDisposable).Dispose(), storedVariable);
            }
        }

        /// <summary>
        /// Disposes a variable by reference.
        /// </summary>
        /// <param name="variable">The variable, by reference.</param>
        public virtual void DisposeVariable(ref IVariable variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException(nameof(variable));
            }

            this.ThrowIfCurrentObjectDisposed();

            var name = variable.Name;
            if (this.variables.TryGetValue(name, out IVariable storedVariable))
            {
                if (storedVariable != variable)
                {
                    throw new InvalidOperationException(Resources.VariableDifferentThanStored);
                }

                this.variables.Remove(name);

                variable = null;

                this.FireAndForget((st) => (st as IDisposable).Dispose(), storedVariable);
            }
            else
            {
                throw new InvalidOperationException(Resources.VariableNotFound);
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
            KeyValuePair<string, IScope>[] scopes = new KeyValuePair<string, IScope>[this.subScopes.Count];

            this.subScopes.CopyTo(scopes, 0);

            this.subScopes.Clear();

            this.FireAndForget((st) => ((KeyValuePair<string, IScope>[])st).ForEach(p => p.Value.Dispose()), scopes);

            KeyValuePair<string, IVariable>[] variables = new KeyValuePair<string, IVariable>[this.variables.Count];

            this.variables.CopyTo(variables, 0);

            this.variables.Clear();

            this.FireAndForget((st) => ((KeyValuePair<string, IVariable>[])st).ForEach(p => p.Value.Dispose()), variables);

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
            this.variables = new ConcurrentObservableDictionary<string, IVariable>();
            this.subScopes = new ConcurrentObservableDictionary<string, IScope>();
        }
    }
}