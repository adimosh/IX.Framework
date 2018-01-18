// <copyright file="SerializationTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.IO;
using System.Runtime.Serialization;
using System.Text;
using IX.StandardExtensions;
using IX.System.Collections.Generic;
using Xunit;

namespace IX.Abstractions.UnitTests
{
    /// <summary>
    /// Tests for serialization.
    /// </summary>
    public class SerializationTests
    {
        /// <summary>
        /// Tests the push down stack serialization.
        /// </summary>
        [Fact(DisplayName = "Serialization tests for PushDownStack")]
        public void TestPushDownStackSerialization()
        {
            // ARRANGE
            // =======
            var item1 = StandardExtensions.TestUtils.DataGenerator.RandomNonNegativeInteger();
            var item2 = StandardExtensions.TestUtils.DataGenerator.RandomNonNegativeInteger();
            var item3 = StandardExtensions.TestUtils.DataGenerator.RandomNonNegativeInteger();
            var item4 = StandardExtensions.TestUtils.DataGenerator.RandomNonNegativeInteger();
            var item5 = StandardExtensions.TestUtils.DataGenerator.RandomNonNegativeInteger();
            var pushDownStack = new PushDownStack<int>(4);

            pushDownStack.Push(item1);
            pushDownStack.Push(item2);
            pushDownStack.Push(item3);
            pushDownStack.Push(item4);
            pushDownStack.Push(item5);

            // The serializer
            var dcs = new DataContractSerializer(typeof(PushDownStack<int>));

            // The deserialization variable
            PushDownStack<int> deserializedPushDownStack;

            // The serialization content
            string content;

            // ACT
            // ===
            using (var ms = new MemoryStream())
            {
                dcs.WriteObject(ms, pushDownStack);

                ms.Seek(0, SeekOrigin.Begin);

                using (var textReader = new StreamReader(ms, Encoding.UTF8, false, 32768, true))
                {
                    content = textReader.ReadToEnd();
                }

                ms.Seek(0, SeekOrigin.Begin);

                deserializedPushDownStack = dcs.ReadObject(ms) as PushDownStack<int>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"",
                content);

            // Deserialized object is OK
            Assert.NotNull(deserializedPushDownStack);
            Assert.Equal(pushDownStack.Count, deserializedPushDownStack.Count);
            Assert.Equal(pushDownStack.Limit, deserializedPushDownStack.Limit);
            Assert.True(pushDownStack.SequenceEquals(deserializedPushDownStack));
        }
    }
}