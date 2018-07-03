// <copyright file="BasicSynchronizedSerializationClass.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Runtime.Serialization;
using IX.StandardExtensions.Threading;

namespace IX.UnitTests.IX.StandardExtensions.Helpers
{
    [DataContract(Namespace = "http://test.namespaces.org/butter")]
    internal class BasicSynchronizedSerializationClass : ReaderWriterSynchronizedBase
    {
        [DataMember]
        public int Setty { get; set; }
    }
}