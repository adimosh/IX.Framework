// <copyright file="FireUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.StandardExtensions;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.TestUtils;
using IX.StandardExtensions.Threading;
using Xunit;
using Xunit.Abstractions;
using ManualResetEventSlim = IX.System.Threading.ManualResetEventSlim;

namespace IX.UnitTests.IX.StandardExtensions
{
    /// <summary>
    /// Unit tests for <see cref="Fire"/>.
    /// </summary>
    public class FireUnitTests
    {
        private const int MaxWaitTime = 5000;
        private const int StandardWaitTime = 300;
        private readonly ITestOutputHelper output;

        /// <summary>
        /// Initializes a new instance of the <see cref="FireUnitTests"/> class.
        /// </summary>
        /// <param name="output">The test output.</param>
        public FireUnitTests(ITestOutputHelper output)
        {
            Contract.RequiresNotNull(ref this.output, output, nameof(output));
        }

        /// <summary>
        /// Test basic Fire.AndForget mechanism.
        /// </summary>
        [Fact(DisplayName = "Test basic Fire.AndForget mechanism")]
        public void Test1()
        {
            // ARRANGE
            int initialValue = DataGenerator.RandomInteger();
            int floatingValue = initialValue;
            int waitTime = DataGenerator.RandomNonNegativeInteger(StandardWaitTime) + 1;
            bool result;

            // ACT
            using (var mre = new ManualResetEventSlim())
            {
                Fire.AndForget(
                    ev =>
                    {
                        Thread.Sleep(waitTime);

                        Interlocked.Exchange(
                            ref floatingValue,
                            DataGenerator.RandomInteger());

                        ev.Set();
                    }, mre);

                result = mre.WaitOne(MaxWaitTime);
            }

            // ASSERT
            try
            {
                Assert.True(result);
                Assert.NotEqual(initialValue, floatingValue);
            }
            catch
            {
                this.output.WriteLine("Assert phase failed.");
                this.output.WriteLine($"Test parameters: Expected Value: {initialValue}; Actual Value: {floatingValue}; Wait Time: {waitTime}; Wait Result: {result}.");
                throw;
            }
        }

        /// <summary>
        /// Test Fire.AndForget distinct threading mechanism.
        /// </summary>
        [Fact(DisplayName = "Test Fire.AndForget distinct threading mechanism")]
        public void Test2()
        {
            // ARRANGE
            int initialValue = Thread.CurrentThread.ManagedThreadId;
            int floatingValue = initialValue;
            int waitTime = DataGenerator.RandomNonNegativeInteger(StandardWaitTime) + 1;
            bool result;

            // ACT
            using (var mre = new ManualResetEventSlim())
            {
                Fire.AndForget(
                    ev =>
                    {
                        Thread.Sleep(waitTime);

                        Interlocked.Exchange(
                            ref floatingValue,
                            Thread.CurrentThread.ManagedThreadId);

                        ev.Set();
                    }, mre);

                result = mre.WaitOne(MaxWaitTime);
            }

            // ASSERT
            try
            {
                Assert.True(result);
                Assert.NotEqual(initialValue, floatingValue);
            }
            catch
            {
                this.output.WriteLine("Assert phase failed.");
                this.output.WriteLine($"Test parameters: Expected Value: {initialValue}; Actual Value: {floatingValue}; Wait Time: {waitTime}; Wait Result: {result}.");
                throw;
            }
        }

        /// <summary>
        /// Test Fire.AndForget eexception mechanism.
        /// </summary>
        [Fact(DisplayName = "Test Fire.AndForget exception mechanism")]
        public void Test3()
        {
            // ARRANGE
            string argumentName = DataGenerator.RandomLowercaseString(
                DataGenerator.RandomInteger(
                    5,
                    10));
            int waitTime = DataGenerator.RandomNonNegativeInteger(StandardWaitTime) + 1;
            bool result;
            Exception ex = null;

            // ACT
            using (var mre = new ManualResetEventSlim())
            {
#if DEBUG
                DateTime dt = DateTime.UtcNow;
#endif
                Fire.AndForget(
                    () =>
                    {
#if DEBUG
                        this.output.WriteLine($"Beginning inner method after {(DateTime.UtcNow - dt).TotalMilliseconds} ms.");
#endif
                        Thread.Sleep(waitTime);
#if DEBUG
                        this.output.WriteLine($"Inner method wait finished after {(DateTime.UtcNow - dt).TotalMilliseconds} ms.");
#endif

                        throw new ArgumentNotPositiveIntegerException(argumentName);
                    },
                    exception =>
                    {
#if DEBUG
                        this.output.WriteLine($"Exception handler started after {(DateTime.UtcNow - dt).TotalMilliseconds} ms.");
#endif
                        Interlocked.Exchange(
                            ref ex,
                            exception);

                        // ReSharper disable once AccessToDisposedClosure - Guaranteed to either not be disposed or not relevant to context anymore at this point
                        mre.Set();
                    });

                result = mre.WaitOne(MaxWaitTime);
#if DEBUG
                this.output.WriteLine($"Outer method unlocked after {(DateTime.UtcNow - dt).TotalMilliseconds} ms.");
#endif
            }

            // ASSERT
            try
            {
                Assert.True(result);
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNotPositiveIntegerException>(ex);
                Assert.Equal(argumentName, ((ArgumentNotPositiveIntegerException)ex).ParamName);
            }
            catch
            {
                this.output.WriteLine("Assert phase failed.");
                this.output.WriteLine($"Test parameters: Wait Time: {waitTime}; Wait Result: {result}; Resulting exception: {ex?.ToString() ?? "null"}.");
                throw;
            }
        }
    }
}