// <copyright file="ObjectPoolQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IX.StandardExtensions.Efficiency
{
    /// <summary>
    /// A pool queue of objects that are waiting for an action to invoke for each, on a separate thread.
    /// </summary>
    /// <typeparam name="T">The type of object in the pool queue.</typeparam>
    public class ObjectPoolQueue<T>
    {
        private readonly Queue<T> objects;
        private readonly CancellationToken cancellationToken;
        private readonly Func<IEnumerable<T>, int, Task<bool>> queueAction;

#pragma warning disable HeapAnalyzerClosureCaptureRule // Display class allocation to capture closure - Acceptable as counterpart of below
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPoolQueue{T}" /> class.
        /// </summary>
        /// <param name="queueAction">The queue action.</param>
        /// <param name="objectLimit">The object limit.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// <para>The <paramref name="queueAction"/> will take two parameters: an enumerable of objects from the pool queue, and the retry count.</para>
        /// <para>Every time there is an exception, the action is re-invoked, and the retry count is increased.</para>
        /// <para>In order to stop retrying, a <see cref="StopRetryingException"/> should be thrown.</para>
        /// </remarks>
        public ObjectPoolQueue(Func<IEnumerable<T>, int, Task<bool>> queueAction, int objectLimit = 1000, CancellationToken cancellationToken = default)
#pragma warning restore HeapAnalyzerClosureCaptureRule // Display class allocation to capture closure
        {
            this.objects = new Queue<T>();
            this.cancellationToken = cancellationToken;
            this.ObjectLimit = objectLimit;
            this.queueAction = queueAction;

#pragma warning disable HeapAnalyzerClosureSourceRule // Closure Allocation Source - This is acceptable, as the lambda and closure will live as part of the object pool queue and provide state reference.
            Task.Run(() => this.Run(null));
#pragma warning restore HeapAnalyzerClosureSourceRule // Closure Allocation Source
        }

        /// <summary>
        /// Gets or sets the object limit.
        /// </summary>
        /// <value>The object limit.</value>
        public int ObjectLimit { get; set; }

        /// <summary>
        /// Enqueues the specified object in the pool queue.
        /// </summary>
        /// <param name="object">The object to enqueue.</param>
        public void Enqueue(T @object) => this.objects.Enqueue(@object);

        private void Run(Task originalTask)
        {
#if FULLDOTNET
            Thread.CurrentThread.Name = $"Object pool queue {Thread.CurrentThread.ManagedThreadId}";
#endif
            if (this.objects.Count == 0)
            {
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable at this point
                Task.Delay(1000, this.cancellationToken).ContinueWith(this.Run, TaskContinuationOptions.OnlyOnRanToCompletion);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group
            }
            else
            {
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable at this point
                Task.Run(
                    ProcessObjects,
                    this.cancellationToken).ContinueWith(this.Run, TaskContinuationOptions.OnlyOnRanToCompletion);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group

                async Task ProcessObjects()
                {
                    this.cancellationToken.ThrowIfCancellationRequested();

                    var objectLimit = this.ObjectLimit;
                    var initialSize = objectLimit < this.objects.Count ? objectLimit : this.objects.Count;

                    var listProcess = new List<T>(initialSize);

                    for (var i = 0; i < initialSize; i++)
                    {
                        listProcess.Add(this.objects.Dequeue());
                    }

                    var retryCounter = 0;
                    var shouldRetry = true;

                    while (shouldRetry)
                    {
                        this.cancellationToken.ThrowIfCancellationRequested();

                        try
                        {
                            shouldRetry = !(await this.queueAction(listProcess, retryCounter++).ConfigureAwait(false));
                        }
                        catch (StopRetryingException)
                        {
                            shouldRetry = false;
                        }
#pragma warning disable ERP022 // Catching everything considered harmful. - Do nothing, a retry is necessary at this point
                        catch (Exception)
                        {
                        }
#pragma warning restore ERP022 // Catching everything considered harmful.
                    }
                }
            }
        }
    }
}