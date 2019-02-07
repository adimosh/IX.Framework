// <copyright file="ActionRetryContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Retry.Contexts
{
    internal sealed class ActionRetryContext : RetryContext
    {
        private readonly Action action;

        internal ActionRetryContext(
            Action action,
            RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
        }

        private protected override void Invoke() => this.action();
    }
}