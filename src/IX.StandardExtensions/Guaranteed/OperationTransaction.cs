// <copyright file="OperationTransaction.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using IX.StandardExtensions.ComponentModel;
using JetBrains.Annotations;

namespace IX.Guaranteed
{
    /// <summary>
    /// A base class for a local, in-process, in-memory transaction.
    /// </summary>
    /// <remarks>
    /// <para>This class is in no way related to transactions or distributed transactions, and will not promote a transaction scope to a distributed transaction.</para>
    /// </remarks>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.DisposableBase" />
    [PublicAPI]
    public abstract class OperationTransaction : DisposableBase
    {
        private List<Tuple<Action<object>, object>> revertSteps;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTransaction"/> class.
        /// </summary>
        protected OperationTransaction()
        {
            this.revertSteps = new List<Tuple<Action<object>, object>>();
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="OperationTransaction"/> is successful.
        /// </summary>
        /// <value><see langword="true"/> if successful; otherwise, <see langword="false"/>.</value>
        public bool Successful { get; private set; }

        /// <summary>
        /// Declares this operation transaction a succes.
        /// </summary>
        public void Success() => this.Successful = true;

        /// <summary>
        /// Adds a revert step.
        /// </summary>
        /// <param name="action">The revert action.</param>
        /// <param name="state">The state object to pass to the revert action.</param>
        protected void AddRevertStep(Action<object> action, object state)
            => this.revertSteps.Add(
                new Tuple<Action<object>, object>(
                    action ?? throw new ArgumentNullException(nameof(action)),
                    state));

        /// <summary>
        /// Gets invoked when the transaction commits and is successful.
        /// </summary>
        protected abstract void WhenSuccessful();

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            base.DisposeGeneralContext();

            if (this.Successful)
            {
                this.WhenSuccessful();
            }
            else
            {
                foreach (Tuple<Action<object>, object> revertStep in this.revertSteps)
                {
                    revertStep.Item1(revertStep.Item2);
                }
            }

            Interlocked.Exchange(ref this.revertSteps, null)?.Clear();
        }
    }
}