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
        {
            this.objects = new Queue<T>();
            this.cancellationToken = cancellationToken;
            this.ObjectLimit = objectLimit;
            this.queueAction = queueAction;

            Task.Run(() => this.Run(null));
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
#if NET45
            Thread.CurrentThread.Name = $"Object pool queue {Thread.CurrentThread.ManagedThreadId}";
#endif
            if (this.objects.Count == 0)
            {
                Task.Delay(1000, this.cancellationToken).ContinueWith(this.Run, TaskContinuationOptions.OnlyOnRanToCompletion);
            }
            else
            {
                Task.Run(
                    ProcessObjects,
                    this.cancellationToken).ContinueWith(this.Run, TaskContinuationOptions.OnlyOnRanToCompletion);

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
                        catch (Exception)
                        {
                            // Do nothing, as a retry is necessary at this point
                        }
                    }
                }
            }
        }
    }
}