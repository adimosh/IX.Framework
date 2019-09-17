// <copyright file="RepeatableCollectionsUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using IX.StandardExtensions.TestUtils;
using IX.System.Collections.Generic;
using Xunit;

namespace IX.UnitTests.IX.Abstractions
{
    /// <summary>
    /// Unit tests for <see cref="System.Collections.Generic.RepeatableQueue{T}"/> and <see cref="System.Collections.Generic.RepeatableStack{T}"/>.
    /// </summary>
    public class RepeatableCollectionsUnitTests
    {
        /// <summary>
        /// Repeatable queue sequential enqueue.
        /// </summary>
        [Fact(DisplayName = "Repeatable queue sequential enqueue")]
        public void Test1()
        {
            // ARRANGE
            var q = new RepeatableQueue<int>();
            int int1 = DataGenerator.RandomInteger();
            int int2 = DataGenerator.RandomInteger();
            int int3 = DataGenerator.RandomInteger();
            int int4 = DataGenerator.RandomInteger();
            int int5 = DataGenerator.RandomInteger();
            int int6 = DataGenerator.RandomInteger();
            q.Enqueue(int1);
            q.Enqueue(int2);
            q.Enqueue(int3);
            q.Enqueue(int4);
            q.Enqueue(int5);
            q.Enqueue(int6);

            // ACT
            int rd1 = q.Dequeue();
            int rd2 = q.Dequeue();
            int rd3 = q.Dequeue();
            var rr = q.Repeat();
            var rr1 = rr.Dequeue();
            var rr2 = rr.Dequeue();
            var rr3 = rr.Dequeue();
            var rq = q.Repeat();
            var rq1 = rq.Dequeue();
            var rq2 = rq.Dequeue();
            var rq3 = rq.Dequeue();

            // ASSERT
            Assert.Equal(int1, rd1);
            Assert.Equal(int1, rr1);
            Assert.Equal(int1, rq1);

            Assert.Equal(int2, rd2);
            Assert.Equal(int2, rr2);
            Assert.Equal(int2, rq2);

            Assert.Equal(int3, rd3);
            Assert.Equal(int3, rr3);
            Assert.Equal(int3, rq3);
        }

        /// <summary>
        /// Repeatable queue collection enqueue.
        /// </summary>
        [Fact(DisplayName = "Repeatable queue collection enqueue")]
        public void Test2()
        {
            // ARRANGE
            var seq = new List<int>();
            int int1 = DataGenerator.RandomInteger();
            int int2 = DataGenerator.RandomInteger();
            int int3 = DataGenerator.RandomInteger();
            int int4 = DataGenerator.RandomInteger();
            int int5 = DataGenerator.RandomInteger();
            int int6 = DataGenerator.RandomInteger();
            seq.Add(int1);
            seq.Add(int2);
            seq.Add(int3);
            seq.Add(int4);
            seq.Add(int5);
            seq.Add(int6);
            var q = new RepeatableQueue<int>(seq);

            // ACT
            int rd1 = q.Dequeue();
            int rd2 = q.Dequeue();
            int rd3 = q.Dequeue();
            var rr = q.Repeat();
            var rr1 = rr.Dequeue();
            var rr2 = rr.Dequeue();
            var rr3 = rr.Dequeue();
            var rq = q.Repeat();
            var rq1 = rq.Dequeue();
            var rq2 = rq.Dequeue();
            var rq3 = rq.Dequeue();

            // ASSERT
            Assert.Equal(int1, rd1);
            Assert.Equal(int1, rr1);
            Assert.Equal(int1, rq1);

            Assert.Equal(int2, rd2);
            Assert.Equal(int2, rr2);
            Assert.Equal(int2, rq2);

            Assert.Equal(int3, rd3);
            Assert.Equal(int3, rr3);
            Assert.Equal(int3, rq3);
        }

        /// <summary>
        /// Repeatable queue with original queue.
        /// </summary>
        [Fact(DisplayName = "Repeatable queue with original queue")]
        public void Test3()
        {
            // ARRANGE
            var seq = new global::System.Collections.Generic.Queue<int>();
            int int1 = DataGenerator.RandomInteger();
            int int2 = DataGenerator.RandomInteger();
            int int3 = DataGenerator.RandomInteger();
            int int4 = DataGenerator.RandomInteger();
            int int5 = DataGenerator.RandomInteger();
            int int6 = DataGenerator.RandomInteger();
            seq.Enqueue(int1);
            seq.Enqueue(int2);
            seq.Enqueue(int3);
            seq.Enqueue(int4);
            seq.Enqueue(int5);
            seq.Enqueue(int6);
            var q = new RepeatableQueue<int>(seq);

            // ACT
            int rd1 = q.Dequeue();
            int rd2 = q.Dequeue();
            int rd3 = q.Dequeue();
            var rr = q.Repeat();
            var rr1 = rr.Dequeue();
            var rr2 = rr.Dequeue();
            var rr3 = rr.Dequeue();
            var rq = q.Repeat();
            var rq1 = rq.Dequeue();
            var rq2 = rq.Dequeue();
            var rq3 = rq.Dequeue();

            // ASSERT
            Assert.Equal(int1, rd1);
            Assert.Equal(int1, rr1);
            Assert.Equal(int1, rq1);

            Assert.Equal(int2, rd2);
            Assert.Equal(int2, rr2);
            Assert.Equal(int2, rq2);

            Assert.Equal(int3, rd3);
            Assert.Equal(int3, rr3);
            Assert.Equal(int3, rq3);

            Assert.Equal(3, seq.Count);
            Assert.Contains(int4, seq);
            Assert.Contains(int5, seq);
            Assert.Contains(int6, seq);
        }

        /// <summary>
        /// Repeatable stack sequential push.
        /// </summary>
        [Fact(DisplayName = "Repeatable stack sequential push")]
        public void Test4()
        {
            // ARRANGE
            var q = new RepeatableStack<int>();
            int int1 = DataGenerator.RandomInteger();
            int int2 = DataGenerator.RandomInteger();
            int int3 = DataGenerator.RandomInteger();
            int int4 = DataGenerator.RandomInteger();
            int int5 = DataGenerator.RandomInteger();
            int int6 = DataGenerator.RandomInteger();
            q.Push(int1);
            q.Push(int2);
            q.Push(int3);
            q.Push(int4);
            q.Push(int5);
            q.Push(int6);

            // ACT
            int rd1 = q.Pop();
            int rd2 = q.Pop();
            int rd3 = q.Pop();
            var rr = q.Repeat();
            var rr1 = rr.Pop();
            var rr2 = rr.Pop();
            var rr3 = rr.Pop();
            var rq = q.Repeat();
            var rq1 = rq.Pop();
            var rq2 = rq.Pop();
            var rq3 = rq.Pop();

            // ASSERT
            Assert.Equal(int6, rd1);
            Assert.Equal(int6, rr1);
            Assert.Equal(int6, rq1);

            Assert.Equal(int5, rd2);
            Assert.Equal(int5, rr2);
            Assert.Equal(int5, rq2);

            Assert.Equal(int4, rd3);
            Assert.Equal(int4, rr3);
            Assert.Equal(int4, rq3);
        }

        /// <summary>
        /// Repeatable stack collection push.
        /// </summary>
        [Fact(DisplayName = "Repeatable stack collection push")]
        public void Test5()
        {
            // ARRANGE
            var seq = new List<int>();
            int int1 = DataGenerator.RandomInteger();
            int int2 = DataGenerator.RandomInteger();
            int int3 = DataGenerator.RandomInteger();
            int int4 = DataGenerator.RandomInteger();
            int int5 = DataGenerator.RandomInteger();
            int int6 = DataGenerator.RandomInteger();
            seq.Add(int1);
            seq.Add(int2);
            seq.Add(int3);
            seq.Add(int4);
            seq.Add(int5);
            seq.Add(int6);
            var q = new RepeatableStack<int>(seq);

            // ACT
            int rd1 = q.Pop();
            int rd2 = q.Pop();
            int rd3 = q.Pop();
            var rr = q.Repeat();
            var rr1 = rr.Pop();
            var rr2 = rr.Pop();
            var rr3 = rr.Pop();
            var rq = q.Repeat();
            var rq1 = rq.Pop();
            var rq2 = rq.Pop();
            var rq3 = rq.Pop();

            // ASSERT
            Assert.Equal(int6, rd1);
            Assert.Equal(int6, rr1);
            Assert.Equal(int6, rq1);

            Assert.Equal(int5, rd2);
            Assert.Equal(int5, rr2);
            Assert.Equal(int5, rq2);

            Assert.Equal(int4, rd3);
            Assert.Equal(int4, rr3);
            Assert.Equal(int4, rq3);
        }

        /// <summary>
        /// Repeatable stack with original stack.
        /// </summary>
        [Fact(DisplayName = "Repeatable stack with original stack")]
        public void Test6()
        {
            // ARRANGE
            var seq = new global::System.Collections.Generic.Stack<int>();
            int int1 = DataGenerator.RandomInteger();
            int int2 = DataGenerator.RandomInteger();
            int int3 = DataGenerator.RandomInteger();
            int int4 = DataGenerator.RandomInteger();
            int int5 = DataGenerator.RandomInteger();
            int int6 = DataGenerator.RandomInteger();
            seq.Push(int1);
            seq.Push(int2);
            seq.Push(int3);
            seq.Push(int4);
            seq.Push(int5);
            seq.Push(int6);
            var q = new RepeatableStack<int>(seq);

            // ACT
            int rd1 = q.Pop();
            int rd2 = q.Pop();
            int rd3 = q.Pop();
            var rr = q.Repeat();
            var rr1 = rr.Pop();
            var rr2 = rr.Pop();
            var rr3 = rr.Pop();
            var rq = q.Repeat();
            var rq1 = rq.Pop();
            var rq2 = rq.Pop();
            var rq3 = rq.Pop();

            // ASSERT
            Assert.Equal(int6, rd1);
            Assert.Equal(int6, rr1);
            Assert.Equal(int6, rq1);

            Assert.Equal(int5, rd2);
            Assert.Equal(int5, rr2);
            Assert.Equal(int5, rq2);

            Assert.Equal(int4, rd3);
            Assert.Equal(int4, rr3);
            Assert.Equal(int4, rq3);

            Assert.Equal(3, seq.Count);
            Assert.Contains(int1, seq);
            Assert.Contains(int2, seq);
            Assert.Contains(int3, seq);
        }
    }
}