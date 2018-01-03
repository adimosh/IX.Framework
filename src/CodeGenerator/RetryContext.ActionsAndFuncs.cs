// <copyright file="RetryContext.ActionsAndFuncs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Retry.Contexts
{
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name
    internal sealed class ActionWith1ParamRetryContext<TParam1> : RetryContext
    {
        private Action<TParam1> action;
        private TParam1 param1;

        internal ActionWith1ParamRetryContext(Action<TParam1> action, TParam1 param1, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
        }

        private protected override void Invoke() => this.action(this.param1);
    }

    internal sealed class FuncWith1ParamRetryContext<TParam1, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TReturn> func;
        private TParam1 param1;
        private TReturn returnValue;

        internal FuncWith1ParamRetryContext(Func<TParam1, TReturn> func, TParam1 param1, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1);
    }

    internal sealed class ActionWith2ParamRetryContext<TParam1, TParam2> : RetryContext
    {
        private Action<TParam1, TParam2> action;
        private TParam1 param1;
        private TParam2 param2;

        internal ActionWith2ParamRetryContext(Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
            this.param2 = param2;
        }

        private protected override void Invoke() => this.action(this.param1, this.param2);
    }

    internal sealed class FuncWith2ParamRetryContext<TParam1, TParam2, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TParam2, TReturn> func;
        private TParam1 param1;
        private TParam2 param2;
        private TReturn returnValue;

        internal FuncWith2ParamRetryContext(Func<TParam1, TParam2, TReturn> func, TParam1 param1, TParam2 param2, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
            this.param2 = param2;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1, this.param2);
    }

    internal sealed class ActionWith3ParamRetryContext<TParam1, TParam2, TParam3> : RetryContext
    {
        private Action<TParam1, TParam2, TParam3> action;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;

        internal ActionWith3ParamRetryContext(Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
        }

        private protected override void Invoke() => this.action(this.param1, this.param2, this.param3);
    }

    internal sealed class FuncWith3ParamRetryContext<TParam1, TParam2, TParam3, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TParam2, TParam3, TReturn> func;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TReturn returnValue;

        internal FuncWith3ParamRetryContext(Func<TParam1, TParam2, TParam3, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1, this.param2, this.param3);
    }

    internal sealed class ActionWith4ParamRetryContext<TParam1, TParam2, TParam3, TParam4> : RetryContext
    {
        private Action<TParam1, TParam2, TParam3, TParam4> action;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;

        internal ActionWith4ParamRetryContext(Action<TParam1, TParam2, TParam3, TParam4> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
        }

        private protected override void Invoke() => this.action(this.param1, this.param2, this.param3, this.param4);
    }

    internal sealed class FuncWith4ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TParam2, TParam3, TParam4, TReturn> func;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TReturn returnValue;

        internal FuncWith4ParamRetryContext(Func<TParam1, TParam2, TParam3, TParam4, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1, this.param2, this.param3, this.param4);
    }

    internal sealed class ActionWith5ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5> : RetryContext
    {
        private Action<TParam1, TParam2, TParam3, TParam4, TParam5> action;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;

        internal ActionWith5ParamRetryContext(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
        }

        private protected override void Invoke() => this.action(this.param1, this.param2, this.param3, this.param4, this.param5);
    }

    internal sealed class FuncWith5ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;
        private TReturn returnValue;

        internal FuncWith5ParamRetryContext(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1, this.param2, this.param3, this.param4, this.param5);
    }

    internal sealed class ActionWith6ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> : RetryContext
    {
        private Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;
        private TParam6 param6;

        internal ActionWith6ParamRetryContext(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
            this.param6 = param6;
        }

        private protected override void Invoke() => this.action(this.param1, this.param2, this.param3, this.param4, this.param5, this.param6);
    }

    internal sealed class FuncWith6ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;
        private TParam6 param6;
        private TReturn returnValue;

        internal FuncWith6ParamRetryContext(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
            this.param6 = param6;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1, this.param2, this.param3, this.param4, this.param5, this.param6);
    }

    internal sealed class ActionWith7ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> : RetryContext
    {
        private Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;
        private TParam6 param6;
        private TParam7 param7;

        internal ActionWith7ParamRetryContext(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
            this.param6 = param6;
            this.param7 = param7;
        }

        private protected override void Invoke() => this.action(this.param1, this.param2, this.param3, this.param4, this.param5, this.param6, this.param7);
    }

    internal sealed class FuncWith7ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;
        private TParam6 param6;
        private TParam7 param7;
        private TReturn returnValue;

        internal FuncWith7ParamRetryContext(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
            this.param6 = param6;
            this.param7 = param7;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1, this.param2, this.param3, this.param4, this.param5, this.param6, this.param7);
    }

    internal sealed class ActionWith8ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> : RetryContext
    {
        private Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;
        private TParam6 param6;
        private TParam7 param7;
        private TParam8 param8;

        internal ActionWith8ParamRetryContext(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
            this.param6 = param6;
            this.param7 = param7;
            this.param8 = param8;
        }

        private protected override void Invoke() => this.action(this.param1, this.param2, this.param3, this.param4, this.param5, this.param6, this.param7, this.param8);
    }

    internal sealed class FuncWith8ParamRetryContext<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> : RetryContextWithReturnValue<TReturn>
    {
        private Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func;
        private TParam1 param1;
        private TParam2 param2;
        private TParam3 param3;
        private TParam4 param4;
        private TParam5 param5;
        private TParam6 param6;
        private TParam7 param7;
        private TParam8 param8;
        private TReturn returnValue;

        internal FuncWith8ParamRetryContext(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TReturn> func, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.func = func;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
            this.param4 = param4;
            this.param5 = param5;
            this.param6 = param6;
            this.param7 = param7;
            this.param8 = param8;
        }

        internal override TReturn GetReturnValue() => this.returnValue;

        private protected override void Invoke() => this.returnValue = this.func(this.param1, this.param2, this.param3, this.param4, this.param5, this.param6, this.param7, this.param8);
    }
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
}