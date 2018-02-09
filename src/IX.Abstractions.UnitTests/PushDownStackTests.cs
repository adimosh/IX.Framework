// <copyright file="PushDownStackTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.StandardExtensions.TestUtils;
using IX.System.Collections.Generic;
using Xunit;

namespace IX.Abstractions.UnitTests
{
    /// <summary>
    /// Tests for <see cref="PushDownStack{T}"/>.
    /// </summary>
    public class PushDownStackTests
    {
        /// <summary>
        /// Tests <see cref="PushDownStack{T}"/> according to the <see cref="FactAttribute"/> on the method.
        /// </summary>
        [Fact(DisplayName = "PushDownStack with zero limit")]
        public void Test1()
        {
            // Arrange
            var pds = new PushDownStack<int>(0);

            // Act
            pds.Push(DataGenerator.RandomInteger());
            pds.Push(DataGenerator.RandomInteger());
            pds.Push(DataGenerator.RandomInteger());
            pds.Push(DataGenerator.RandomInteger());

            // Assert
            Assert.Empty(pds);
        }

        /// <summary>
        /// Tests <see cref="PushDownStack{T}"/> according to the <see cref="FactAttribute"/> on the method.
        /// </summary>
        [Fact(DisplayName = "PushDownStack with elements under limit")]
        public void Test2()
        {
            // Arrange
            var pds = new PushDownStack<int>(12);

            // Act
            pds.Push(DataGenerator.RandomInteger());
            pds.Push(DataGenerator.RandomInteger());
            pds.Push(DataGenerator.RandomInteger());
            pds.Push(DataGenerator.RandomInteger());

            // Assert
            Assert.Equal(4, pds.Count);
        }

        /// <summary>
        /// Tests <see cref="PushDownStack{T}"/> according to the <see cref="FactAttribute"/> on the method.
        /// </summary>
        [Fact(DisplayName = "PushDownStack with elements above limit")]
        public void Test3()
        {
            // Arrange
            var pds = new PushDownStack<int>(3);
            var v1 = DataGenerator.RandomInteger();
            var v2 = DataGenerator.RandomInteger();
            var v3 = DataGenerator.RandomInteger();
            var v4 = DataGenerator.RandomInteger();

            // Act
            pds.Push(v1);
            pds.Push(v2);
            pds.Push(v3);
            pds.Push(v4);

            // Assert
            Assert.Equal(3, pds.Count);
            Assert.Equal(v4, pds.Pop());
            Assert.Equal(v3, pds.Pop());
            Assert.Equal(v2, pds.Pop());
            Assert.Equal(default(int), pds.Pop());
        }

        /// <summary>
        /// Tests <see cref="PushDownStack{T}"/> according to the <see cref="FactAttribute"/> on the method.
        /// </summary>
        [Fact(DisplayName = "PushDownStack with elements initially below limit, then changed to above limit")]
        public void Test4()
        {
            // Arrange
            var pds = new PushDownStack<int>(12);
            var v1 = DataGenerator.RandomInteger();
            var v2 = DataGenerator.RandomInteger();
            var v3 = DataGenerator.RandomInteger();
            var v4 = DataGenerator.RandomInteger();

            pds.Push(v1);
            pds.Push(v2);
            pds.Push(v3);
            pds.Push(v4);

            // Assert correct arrangement
            Assert.Equal(4, pds.Count);

            // Act
            pds.Limit = 3;

            // Assert
            Assert.Equal(3, pds.Count);
            Assert.Equal(v4, pds.Pop());
            Assert.Equal(v3, pds.Pop());
            Assert.Equal(v2, pds.Pop());
            Assert.Equal(default(int), pds.Pop());
        }
    }
}