using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IX.Retry.Contexts
{
    internal sealed class ActionRetryContext : RetryContext
    {
        private Action action;

        internal ActionRetryContext(Action action, RetryOptions retryOptions)
            : base(retryOptions)
        {
            this.action = action;
        }

        private protected override void Invoke() => this.action();
    }
}