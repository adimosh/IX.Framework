// <copyright file="IContextAwareDeepCloneable{TContext,TResult}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.StandardExtensions
{
    /// <summary>
    /// Interface for implementing context-aware deep cloning for an object.
    /// </summary>
    /// <typeparam name="TContext">The type of the cloning context.</typeparam>
    /// <typeparam name="TResult">The type of object to clone.</typeparam>
    public interface IContextAwareDeepCloneable<TContext, TResult>
    {
        /// <summary>
        /// Creates a deep clone of the source object based on an existing context.
        /// </summary>
        /// <param name="context">The deep cloning context.</param>
        /// <returns>A deep clone.</returns>
        TResult DeepClone(TContext context);
    }
}