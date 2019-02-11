// <copyright file="FireUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using IX.StandardExtensions;
using IX.StandardExtensions.TestUtils;
using IX.StandardExtensions.Threading;
using Xunit;
using ManualResetEventSlim = IX.System.Threading.ManualResetEventSlim;

namespace IX.UnitTests.IX.StandardExtensions
{
    /// <summary>
    /// Unit tests for <see cref="Fire"/>.
    /// </summary>
    public class FireUnitTests
    {
        /// <summary>
        /// Test basic Fire.AndForget mechanism.
        /// </summary>
        [Fact(DisplayName = "Test basic Fire.AndForget mechanism")]
        public void Test1()
        {
            int initialValue = DataGenerator.RandomInteger();
            int floatingValue = initialValue;

            using (var mre = new ManualResetEventSlim())
            {
                Fire.AndForget(
                    ev =>
                    {
                        Thread.Sleep(DataGenerator.RandomNonNegativeInteger(300) + 1);

                        Interlocked.Exchange(
                            ref floatingValue,
                            DataGenerator.RandomInteger());

                        ev.Set();
                    }, mre);

                Assert.True(mre.WaitOne(1000));
            }

            Assert.NotEqual(initialValue, floatingValue);
        }

        /// <summary>
        /// Test Fire.AndForget distinct threading mechanism.
        /// </summary>
        [Fact(DisplayName = "Test Fire.AndForget distinct threading mechanism")]
        public void Test2()
        {
            int initialValue = Thread.CurrentThread.ManagedThreadId;
            int floatingValue = initialValue;

            using (var mre = new ManualResetEventSlim())
            {
                Fire.AndForget(
                    ev =>
                    {
                        Thread.Sleep(DataGenerator.RandomNonNegativeInteger(300) + 1);

                        Interlocked.Exchange(
                            ref floatingValue,
                            Thread.CurrentThread.ManagedThreadId);

                        ev.Set();
                    }, mre);

                Assert.True(mre.WaitOne(1000));
            }

            Assert.NotEqual(initialValue, floatingValue);
        }

        /// <summary>
        /// Test Fire.AndForget eexception mechanism.
        /// </summary>
        [Fact(DisplayName = "Test Fire.AndForget exception mechanism")]
        public void Test3()
        {
            string argumentName = DataGenerator.RandomLowercaseString(
                DataGenerator.RandomInteger(
                    5,
                    10));

            Exception ex = null;

            using (var mre = new ManualResetEventSlim())
            {
                Fire.AndForget(
                    () =>
                    {
                        Thread.Sleep(DataGenerator.RandomNonNegativeInteger(300) + 1);

                        throw new ArgumentNotPositiveIntegerException(argumentName);
                    },
                    exception =>
                    {
                        Interlocked.Exchange(
                            ref ex,
                            exception);

                        // ReSharper disable once AccessToDisposedClosure - Guaranteed to either not be disposed or not relevant to context anymore at this point
                        mre.Set();
                    });

                Assert.True(mre.WaitOne(1000));
            }

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNotPositiveIntegerException>(ex);
            Assert.Equal(argumentName, ((ArgumentNotPositiveIntegerException)ex).ParamName);
        }
    }
}