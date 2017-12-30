// <copyright file="RetryContextWithReturnValue.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Retry.Contexts
{
    internal abstract class RetryContextWithReturnValue<TReturn> : RetryContext
    {
        internal RetryContextWithReturnValue(RetryOptions retryOptions)
            : base(retryOptions)
        {
        }

        private protected abstract TReturn GetReturnValue();
    }
}