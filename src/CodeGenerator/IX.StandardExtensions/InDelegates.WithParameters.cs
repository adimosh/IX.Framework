// <copyright file="InDelegates.WithParameters.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.StandardExtensions.Efficiency
{
    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TResult>(in TParam1 param1);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1>(in TParam1 param1);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TParam2, TResult>(in TParam1 param1, in TParam2 param2);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1, TParam2>(in TParam1 param1, in TParam2 param2);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TParam2, TParam3, TResult>(in TParam1 param1, in TParam2 param2, in TParam3 param3);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1, TParam2, TParam3>(in TParam1 param1, in TParam2 param2, in TParam3 param3);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TParam2, TParam3, TParam4, TResult>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1, TParam2, TParam3, TParam4>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1, TParam2, TParam3, TParam4, TParam5>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5, in TParam6 param6);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5, in TParam6 param6);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TParam7">The type of the parameter at index 6.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by read-only reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5, in TParam6 param6, in TParam7 param7);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TParam7">The type of the parameter at index 6.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by read-only reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5, in TParam6 param6, in TParam7 param7);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TParam7">The type of the parameter at index 6.</typeparam>
    /// <typeparam name="TParam8">The type of the parameter at index 7.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by read-only reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by read-only reference.</param>
    /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the method at index 7. This parameter is passed by read-only reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult InFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5, in TParam6 param6, in TParam7 param7, in TParam8 param8);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;in&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TParam7">The type of the parameter at index 6.</typeparam>
    /// <typeparam name="TParam8">The type of the parameter at index 7.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by read-only reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by read-only reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by read-only reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by read-only reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by read-only reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by read-only reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by read-only reference.</param>
    /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the method at index 7. This parameter is passed by read-only reference.</param>
    public delegate void InAction<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(in TParam1 param1, in TParam2 param2, in TParam3 param3, in TParam4 param4, in TParam5 param5, in TParam6 param6, in TParam7 param7, in TParam8 param8);
}