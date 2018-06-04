// <copyright file="RefDelegates.WithParameters.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.StandardExtensions.Efficiency
{
    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TResult>(ref TParam1 param1);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1>(ref TParam1 param1);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TParam2, TResult>(ref TParam1 param1, ref TParam2 param2);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1, TParam2>(ref TParam1 param1, ref TParam2 param2);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TParam2, TParam3, TResult>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1, TParam2, TParam3>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TParam2, TParam3, TParam4, TResult>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1, TParam2, TParam3, TParam4>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1, TParam2, TParam3, TParam4, TParam5>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5, ref TParam6 param6);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5, ref TParam6 param6);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TParam7">The type of the parameter at index 6.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5, ref TParam6 param6, ref TParam7 param7);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TParam7">The type of the parameter at index 6.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5, ref TParam6 param6, ref TParam7 param7);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;, with a return type.
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
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by reference.</param>
    /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the method at index 7. This parameter is passed by reference.</param>
    /// <returns>The result of the method.</returns>
    public delegate TResult RefFunc<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5, ref TParam6 param6, ref TParam7 param7, ref TParam8 param8);

    /// <summary>
    /// A generic delegate for a method whose parameters are all marked as &quot;ref&quot;.
    /// </summary>
    /// <typeparam name="TParam1">The type of the parameter at index 0.</typeparam>
    /// <typeparam name="TParam2">The type of the parameter at index 1.</typeparam>
    /// <typeparam name="TParam3">The type of the parameter at index 2.</typeparam>
    /// <typeparam name="TParam4">The type of the parameter at index 3.</typeparam>
    /// <typeparam name="TParam5">The type of the parameter at index 4.</typeparam>
    /// <typeparam name="TParam6">The type of the parameter at index 5.</typeparam>
    /// <typeparam name="TParam7">The type of the parameter at index 6.</typeparam>
    /// <typeparam name="TParam8">The type of the parameter at index 7.</typeparam>
    /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the method at index 0. This parameter is passed by reference.</param>
    /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the method at index 1. This parameter is passed by reference.</param>
    /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the method at index 2. This parameter is passed by reference.</param>
    /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the method at index 3. This parameter is passed by reference.</param>
    /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the method at index 4. This parameter is passed by reference.</param>
    /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the method at index 5. This parameter is passed by reference.</param>
    /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the method at index 6. This parameter is passed by reference.</param>
    /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the method at index 7. This parameter is passed by reference.</param>
    public delegate void RefAction<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(ref TParam1 param1, ref TParam2 param2, ref TParam3 param3, ref TParam4 param4, ref TParam5 param5, ref TParam6 param6, ref TParam7 param7, ref TParam8 param8);
}