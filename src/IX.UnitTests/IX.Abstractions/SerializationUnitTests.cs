// <copyright file="SerializationUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.IO;
using System.Runtime.Serialization;
using System.Text;
using IX.Abstractions.Collections;
using IX.StandardExtensions;
using IX.StandardExtensions.TestUtils;
using IX.System.Collections.Generic;
using Xunit;

namespace IX.UnitTests.IX.Abstractions
{
    /// <summary>
    /// Tests for serialization.
    /// </summary>
    public class SerializationUnitTests
    {
        /// <summary>
        /// Tests the push down stack serialization.
        /// </summary>
        [Fact(DisplayName = "Serialization tests for PushDownStack")]
        public void TestPushDownStackSerialization()
        {
            // ARRANGE
            // =======
            var item1 = DataGenerator.RandomNonNegativeInteger();
            var item2 = DataGenerator.RandomNonNegativeInteger();
            var item3 = DataGenerator.RandomNonNegativeInteger();
            var item4 = DataGenerator.RandomNonNegativeInteger();
            var item5 = DataGenerator.RandomNonNegativeInteger();
            var l1 = new PushDownStack<int>(4);

            l1.Push(item1);
            l1.Push(item2);
            l1.Push(item3);
            l1.Push(item4);
            l1.Push(item5);

            // The serializer
            var dcs = new DataContractSerializer(typeof(PushDownStack<int>));

            // The deserialization variable
            PushDownStack<int> l2;

            // The serialization content
            string content;

            // ACT
            // ===
            using (var ms = new MemoryStream())
            {
                dcs.WriteObject(ms, l1);

                ms.Seek(0, SeekOrigin.Begin);

                using (var textReader = new StreamReader(ms, Encoding.UTF8, false, 32768, true))
                {
                    content = textReader.ReadToEnd();
                }

                ms.Seek(0, SeekOrigin.Begin);

                l2 = dcs.ReadObject(ms) as PushDownStack<int>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"<PushDownStackOfint xmlns=""{Constants.DataContractNamespace}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance""><Items xmlns:a=""http://schemas.microsoft.com/2003/10/Serialization/Arrays""><a:int>{item2}</a:int><a:int>{item3}</a:int><a:int>{item4}</a:int><a:int>{item5}</a:int></Items><Limit>4</Limit></PushDownStackOfint>",
                content);

            // Deserialized object is OK
            Assert.NotNull(l2);
            Assert.Equal(l1.Count, l2.Count);
            Assert.Equal(l1.Limit, l2.Limit);
            Assert.True(l1.SequenceEquals(l2));

            l1.Dispose();
            l2.Dispose();
        }
    }
}