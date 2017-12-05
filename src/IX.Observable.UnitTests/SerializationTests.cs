// <copyright file="SerializationTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using IX.StandardExtensions;
using Xunit;

namespace IX.Observable.UnitTests
{
    /// <summary>
    /// Serialization tests.
    /// </summary>
    public class SerializationTests
    {
        /// <summary>
        /// Observables the list serialization test.
        /// </summary>
        [Fact(DisplayName = "ObservableList serialization")]
        public void ObservableListSerializationTest()
        {
            // ARRANGE
            // =======

            // A random generator (we'll test random values to avoid hard-codings)
            var r = new Random();

            // The data contract serializer we'll use to serialize and deserialize
            var dcs = new DataContractSerializer(typeof(ObservableList<DummyDataContract>));

            // The dummy data
            var ddc1 = new DummyDataContract { RandomValue = r.Next() };
            var ddc2 = new DummyDataContract { RandomValue = r.Next() };
            var ddc3 = new DummyDataContract { RandomValue = r.Next() };
            var ddc4 = new DummyDataContract { RandomValue = r.Next() };

            // The original observable list
            var l1 = new ObservableList<DummyDataContract>
            {
                ddc1,
                ddc2,
                ddc3,
                ddc4,
            };

            // The deserialized list
            ObservableList<DummyDataContract> l2;

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

                l2 = dcs.ReadObject(ms) as ObservableList<DummyDataContract>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"<ObservableDDCList xmlns=""{Constants.DataContractNamespace}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><Item><a:RandomValue>{ddc1.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc2.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc3.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc4.RandomValue}</a:RandomValue></Item></ObservableDDCList>",
                content);

            // Deserialized object is OK
            Assert.NotNull(l2);
            Assert.Equal(l1.Count, l2.Count);
            Assert.True(l1.SequenceEquals(l2));
        }

        /// <summary>
        /// Observables the list serialization test.
        /// </summary>
        [Fact(DisplayName = "ConcurrentObservableList serialization")]
        public void ConcurrentObservableListSerializationTest()
        {
            // ARRANGE
            // =======

            // A random generator (we'll test random values to avoid hard-codings)
            var r = new Random();

            // The data contract serializer we'll use to serialize and deserialize
            var dcs = new DataContractSerializer(typeof(ConcurrentObservableList<DummyDataContract>));

            // The dummy data
            var ddc1 = new DummyDataContract { RandomValue = r.Next() };
            var ddc2 = new DummyDataContract { RandomValue = r.Next() };
            var ddc3 = new DummyDataContract { RandomValue = r.Next() };
            var ddc4 = new DummyDataContract { RandomValue = r.Next() };

            // The original observable list
            var l1 = new ConcurrentObservableList<DummyDataContract>
            {
                ddc1,
                ddc2,
                ddc3,
                ddc4,
            };

            // The deserialized list
            ConcurrentObservableList<DummyDataContract> l2;

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

                l2 = dcs.ReadObject(ms) as ConcurrentObservableList<DummyDataContract>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"<ObservableDDCList xmlns=""{Constants.DataContractNamespace}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><Item><a:RandomValue>{ddc1.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc2.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc3.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc4.RandomValue}</a:RandomValue></Item></ObservableDDCList>",
                content);

            // Deserialized object is OK
            Assert.NotNull(l2);
            Assert.Equal(l1.Count, l2.Count);
            Assert.True(l1.SequenceEquals(l2));
        }

        /// <summary>
        /// Observables the list serialization test.
        /// </summary>
        [Fact(DisplayName = "ObservableDictionary serialization")]
        public void ObservableDictionarySerializationTest()
        {
            // ARRANGE
            // =======

            // A random generator (we'll test random values to avoid hard-codings)
            var r = new Random();

            // The data contract serializer we'll use to serialize and deserialize
            var dcs = new DataContractSerializer(typeof(ObservableDictionary<int, DummyDataContract>));

            // The dummy data
            var ddc1 = new DummyDataContract { RandomValue = r.Next() };
            var ddc2 = new DummyDataContract { RandomValue = r.Next() };
            var ddc3 = new DummyDataContract { RandomValue = r.Next() };
            var ddc4 = new DummyDataContract { RandomValue = r.Next() };

            // The original observable list
            var l1 = new ObservableDictionary<int, DummyDataContract>
            {
                [ddc1.RandomValue] = ddc1,
                [ddc2.RandomValue] = ddc2,
                [ddc3.RandomValue] = ddc3,
                [ddc4.RandomValue] = ddc4,
            };

            // The deserialized list
            ObservableDictionary<int, DummyDataContract> l2;

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

                l2 = dcs.ReadObject(ms) as ObservableDictionary<int, DummyDataContract>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"<ObservableDDCDictionaryByint xmlns=""{Constants.DataContractNamespace}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance""><Entry><Key>{ddc1.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc1.RandomValue}</a:RandomValue></Value></Entry><Entry><Key>{ddc2.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc2.RandomValue}</a:RandomValue></Value></Entry><Entry><Key>{ddc3.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc3.RandomValue}</a:RandomValue></Value></Entry><Entry><Key>{ddc4.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc4.RandomValue}</a:RandomValue></Value></Entry></ObservableDDCDictionaryByint>",
                content);

            // Deserialized object is OK
            Assert.NotNull(l2);
            Assert.Equal(l1.Count, l2.Count);

            foreach (var key in l1.Keys)
            {
                Assert.True(l1[key].Equals(l2[key]));
            }
        }

        /// <summary>
        /// Observables the list serialization test.
        /// </summary>
        [Fact(DisplayName = "ConcurrentObservableDictionary serialization")]
        public void ConcurrentObservableDictionarySerializationTest()
        {
            // ARRANGE
            // =======

            // A random generator (we'll test random values to avoid hard-codings)
            var r = new Random();

            // The data contract serializer we'll use to serialize and deserialize
            var dcs = new DataContractSerializer(typeof(ConcurrentObservableDictionary<int, DummyDataContract>));

            // The dummy data
            var ddc1 = new DummyDataContract { RandomValue = r.Next() };
            var ddc2 = new DummyDataContract { RandomValue = r.Next() };
            var ddc3 = new DummyDataContract { RandomValue = r.Next() };
            var ddc4 = new DummyDataContract { RandomValue = r.Next() };

            // The original observable list
            var l1 = new ConcurrentObservableDictionary<int, DummyDataContract>
            {
                [ddc1.RandomValue] = ddc1,
                [ddc2.RandomValue] = ddc2,
                [ddc3.RandomValue] = ddc3,
                [ddc4.RandomValue] = ddc4,
            };

            // The deserialized list
            ConcurrentObservableDictionary<int, DummyDataContract> l2;

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

                l2 = dcs.ReadObject(ms) as ConcurrentObservableDictionary<int, DummyDataContract>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"<ObservableDDCDictionaryByint xmlns=""{Constants.DataContractNamespace}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance""><Entry><Key>{ddc1.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc1.RandomValue}</a:RandomValue></Value></Entry><Entry><Key>{ddc2.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc2.RandomValue}</a:RandomValue></Value></Entry><Entry><Key>{ddc3.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc3.RandomValue}</a:RandomValue></Value></Entry><Entry><Key>{ddc4.RandomValue}</Key><Value xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><a:RandomValue>{ddc4.RandomValue}</a:RandomValue></Value></Entry></ObservableDDCDictionaryByint>",
                content);

            // Deserialized object is OK
            Assert.NotNull(l2);
            Assert.Equal(l1.Count, l2.Count);

            foreach (var key in l1.Keys)
            {
                Assert.True(l1[key].Equals(l2[key]));
            }
        }

        /// <summary>
        /// Observables the list serialization test.
        /// </summary>
        [Fact(DisplayName = "ObservableQueue serialization")]
        public void ObservableQueueSerializationTest()
        {
            // ARRANGE
            // =======

            // A random generator (we'll test random values to avoid hard-codings)
            var r = new Random();

            // The data contract serializer we'll use to serialize and deserialize
            var dcs = new DataContractSerializer(typeof(ObservableQueue<DummyDataContract>));

            // The dummy data
            var ddc1 = new DummyDataContract { RandomValue = r.Next() };
            var ddc2 = new DummyDataContract { RandomValue = r.Next() };
            var ddc3 = new DummyDataContract { RandomValue = r.Next() };
            var ddc4 = new DummyDataContract { RandomValue = r.Next() };

            // The original observable list
            var l1 = new ObservableQueue<DummyDataContract>
            {
                ddc1,
                ddc2,
                ddc3,
                ddc4,
            };

            // The deserialized list
            ObservableQueue<DummyDataContract> l2;

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

                l2 = dcs.ReadObject(ms) as ObservableQueue<DummyDataContract>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"<ObservableDDCQueue xmlns=""{Constants.DataContractNamespace}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><Item><a:RandomValue>{ddc1.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc2.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc3.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc4.RandomValue}</a:RandomValue></Item></ObservableDDCQueue>",
                content);

            // Deserialized object is OK
            Assert.NotNull(l2);
            Assert.Equal(l1.Count, l2.Count);
            Assert.True(l1.SequenceEquals(l2));
        }

        /// <summary>
        /// Observables the list serialization test.
        /// </summary>
        [Fact(DisplayName = "ConcurrentObservableQueue serialization")]
        public void ConcurrentObservableQueueSerializationTest()
        {
            // ARRANGE
            // =======

            // A random generator (we'll test random values to avoid hard-codings)
            var r = new Random();

            // The data contract serializer we'll use to serialize and deserialize
            var dcs = new DataContractSerializer(typeof(ConcurrentObservableQueue<DummyDataContract>));

            // The dummy data
            var ddc1 = new DummyDataContract { RandomValue = r.Next() };
            var ddc2 = new DummyDataContract { RandomValue = r.Next() };
            var ddc3 = new DummyDataContract { RandomValue = r.Next() };
            var ddc4 = new DummyDataContract { RandomValue = r.Next() };

            // The original observable list
            var l1 = new ConcurrentObservableQueue<DummyDataContract>
            {
                ddc1,
                ddc2,
                ddc3,
                ddc4,
            };

            // The deserialized list
            ConcurrentObservableQueue<DummyDataContract> l2;

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

                l2 = dcs.ReadObject(ms) as ConcurrentObservableQueue<DummyDataContract>;
            }

            // ASSERT
            // ======

            // Serialization content is OK
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Equal(
                $@"<ObservableDDCQueue xmlns=""{Constants.DataContractNamespace}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:a=""http://schemas.datacontract.org/2004/07/IX.Observable.UnitTests""><Item><a:RandomValue>{ddc1.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc2.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc3.RandomValue}</a:RandomValue></Item><Item><a:RandomValue>{ddc4.RandomValue}</a:RandomValue></Item></ObservableDDCQueue>",
                content);

            // Deserialized object is OK
            Assert.NotNull(l2);
            Assert.Equal(l1.Count, l2.Count);
            Assert.True(l1.SequenceEquals(l2));
        }

        /// <summary>
        /// Class DummyDataContract.
        /// </summary>
        /// <seealso cref="System.IEquatable{DummyDataContract}" />
        [DebuggerDisplay("DDC {RandomValue}")]
        [DataContract(Name = "DDC")]
        private class DummyDataContract : IEquatable<DummyDataContract>
        {
            /// <summary>
            /// Gets or sets the random value.
            /// </summary>
            /// <value>The random value.</value>
            [DataMember]
            public int RandomValue { get; set; }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
            public bool Equals(DummyDataContract other) => this.RandomValue == other?.RandomValue;
        }
    }
}