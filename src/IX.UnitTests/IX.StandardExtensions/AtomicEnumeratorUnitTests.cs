// <copyright file="AtomicEnumeratorUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using IX.StandardExtensions.Extensions;
using IX.StandardExtensions.Threading;
using Xunit;

namespace IX.UnitTests.IX.StandardExtensions
{
    /// <summary>
    /// Unit tests for the atomic enumerator.
    /// </summary>
    public class AtomicEnumeratorUnitTests
    {
        /// <summary>
        /// Unit test for the <see cref="AtomicEnumerator{TItem, TEnumerator}"/> correctness of enumeration.
        /// </summary>
        [Fact(DisplayName = "AtomicEnumerator direct instantiation test")]
        public void Test1()
        {
            // ARRANGE
            var q = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            using List<int>.Enumerator enumerator = q.GetEnumerator();

            List<int> newList1 = new List<int>(5), newList2 = new List<int>(5);

            using var ae = new AtomicEnumerator<int, List<int>.Enumerator>(
                enumerator,
                () => new ReadOnlySynchronizationLocker(null));

            // ACT
            while (ae.MoveNext())
            {
                newList1.Add(ae.Current);
            }

            ae.Reset();

            while (ae.MoveNext())
            {
                newList2.Add(ae.Current);
            }

            // ASSERT
            Assert.True(q.SequenceEquals(newList1));
            Assert.True(q.SequenceEquals(newList2));
        }

        /// <summary>
        /// Unit test for the <see cref="AtomicEnumerator{TItem, TEnumerator}"/> correctness of enumeration.
        /// </summary>
        [Fact(DisplayName = "AtomicEnumerator FromCollection test")]
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "HAA0401:Possible allocation of reference type enumerator",
            Justification = "We're dealing with an AtomicEnumerator.")]
        public void Test2()
        {
            // ARRANGE
            var q = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            List<int> newList1 = new List<int>(5), newList2 = new List<int>(5);

            // ACT
            using var ae = AtomicEnumerator<int>.FromCollection(
                q,
                () => new ReadOnlySynchronizationLocker(null));

            while (ae.MoveNext())
            {
                newList1.Add(ae.Current);
            }

            ae.Reset();

            while (ae.MoveNext())
            {
                newList2.Add(ae.Current);
            }

            // ASSERT
            Assert.True(q.SequenceEquals(newList1));
            Assert.True(q.SequenceEquals(newList2));
        }

        /// <summary>
        /// Unit test for the <see cref="AtomicEnumerator{TItem, TEnumerator}"/> correctness of enumeration.
        /// </summary>
        [Fact(DisplayName = "AtomicEnumerator FromEnumerator test")]
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "HAA0401:Possible allocation of reference type enumerator",
            Justification = "We're dealing with an AtomicEnumerator.")]
        public void Test3()
        {
            // ARRANGE
            var q = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            using List<int>.Enumerator enumerator = q.GetEnumerator();

            List<int> newList1 = new List<int>(5), newList2 = new List<int>(5);

            // ACT
            using var ae = AtomicEnumerator<int>.FromEnumerator(
                enumerator,
                () => new ReadOnlySynchronizationLocker(null));

            while (ae.MoveNext())
            {
                newList1.Add(ae.Current);
            }

            ae.Reset();

            while (ae.MoveNext())
            {
                newList2.Add(ae.Current);
            }

            // ASSERT
            Assert.True(q.SequenceEquals(newList1));
            Assert.True(q.SequenceEquals(newList2));
        }
    }
}