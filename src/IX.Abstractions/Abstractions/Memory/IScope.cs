// <copyright file="IScope.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.Abstractions.Memory
{
    /// <summary>
    /// A contract for a memory scope.
    /// </summary>
    /// <seealso cref="IDisposable" />
    [PublicAPI]
    public interface IScope : IEquatable<IScope>, IDisposable
    {
        /// <summary>
        /// Gets the identifier of the scope.
        /// </summary>
        /// <value>The scope identifier.</value>
        string Id { get; }

        /// <summary>
        /// Gets the parent of this scope, if one exists.
        /// </summary>
        /// <value>The parent scope.</value>
        IScope Parent { get; }

        /// <summary>
        /// Initiates a scope under the current scope.
        /// </summary>
        /// <param name="id">The identifier of the new scope.</param>
        /// <returns>The new scope.</returns>
        IScope InitiateSubScope(string id);

        /// <summary>
        /// Finalizes a scope that lives under the current scope by identifier.
        /// </summary>
        /// <param name="id">The scope identifier.</param>
        void FinalizeSubScope(string id);

        /// <summary>
        /// Finalizes a scope that lives under the current scope by reference.
        /// </summary>
        /// <param name="subScope">The scope reference.</param>
        void FinalizeSubScope(ref IScope subScope);

        /// <summary>
        /// Creates a variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The new variable, if one has been created.</returns>
        INamedVariable<T> CreateVariable<T>(string name);

        /// <summary>
        /// Creates an unnamed variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The new variable, if one has been created.</returns>
        IVariable<T> CreateVariable<T>(T value);

        /// <summary>
        /// Creates a variable of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value.</param>
        /// <returns>The new variable, if one has been created.</returns>
        INamedVariable<T> CreateVariable<T>(string name, T value);

        /// <summary>
        /// Disposes a variable by name.
        /// </summary>
        /// <param name="name">The name  of the variable.</param>
        void DisposeVariable(string name);

        /// <summary>
        /// Disposes a variable by reference.
        /// </summary>
        /// <param name="variable">The variable, by reference.</param>
        void DisposeVariable(ref INamedVariable variable);

        /// <summary>
        /// Disposes an unnamed variable by reference.
        /// </summary>
        /// <param name="variable">The variable, by reference.</param>
        void DisposeVariable(ref IVariable variable);
    }
}