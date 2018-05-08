// <copyright file="FileMock.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.IO;
using System.Text;
using IX.System.IO;
using Moq;

namespace IX.Abstractions.Moq
{
    /// <summary>
    /// A mock of a file abstraction.
    /// </summary>
    /// <seealso cref="T:Moq.Mock{T}" />
    public class FileMock : Mock<IFile>
    {
        /// <summary>
        /// Sets up a stand-alone reading operation for a self-closing and self-disposing stream.
        /// </summary>
        /// <param name="fileName">The name of the file to set up the mock for.</param>
        /// <param name="fileContents">The contents of the file.</param>
        /// <remarks>
        /// <para>The default encoding for the text contents will be used, which is UTF8.</para>
        /// </remarks>
        /// <seealso cref="Encoding.UTF8"/>
        public void SetupStandaloneOpenRead(string fileName, string fileContents) => this.SetupStandaloneOpenRead(fileName, fileContents, Encoding.UTF8);

        /// <summary>
        /// Sets up a stand-alone reading operation for a self-closing and self-disposing stream.
        /// </summary>
        /// <param name="fileName">The name of the file to set up the mock for.</param>
        /// <param name="fileContents">The contents of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <seealso cref="Encoding"/>
        public void SetupStandaloneOpenRead(string fileName, string fileContents, Encoding encoding)
        {
            var bytes = encoding.GetBytes(fileContents);

            this.SetupStandaloneOpenRead(fileName, bytes);
        }

        /// <summary>
        /// Sets up a stand-alone reading operation for a self-closing and self-disposing stream.
        /// </summary>
        /// <param name="fileName">The name of the file to set up the mock for.</param>
        /// <param name="fileContents">The contents of the file.</param>
        public void SetupStandaloneOpenRead(string fileName, byte[] fileContents) =>
            this.Setup(p => p.OpenRead(It.Is<string>(q => q == fileName))).Returns(() => new MemoryStream(fileContents));

        /// <summary>
        /// Sets up a stand-alone text reading operation for a self-closing and self-disposing stream.
        /// </summary>
        /// <param name="fileName">The name of the file to set up the mock for.</param>
        /// <param name="fileContents">The contents of the file.</param>
        /// <remarks>
        /// <para>The default encoding for the text contents will be used, which is UTF8.</para>
        /// </remarks>
        /// <seealso cref="Encoding.UTF8"/>
        public void SetupStandaloneOpenText(string fileName, string fileContents) => this.SetupStandaloneOpenText(fileName, fileContents, Encoding.UTF8);

        /// <summary>
        /// Sets up a stand-alone text reading operation for a self-closing and self-disposing stream.
        /// </summary>
        /// <param name="fileName">The name of the file to set up the mock for.</param>
        /// <param name="fileContents">The contents of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <seealso cref="Encoding"/>
        public void SetupStandaloneOpenText(string fileName, string fileContents, Encoding encoding)
        {
            var bytes = Encoding.UTF8.GetBytes(fileContents);

            this.SetupStandaloneOpenText(fileName, bytes);
        }

        /// <summary>
        /// Sets up a stand-alone text reading operation for a self-closing and self-disposing stream.
        /// </summary>
        /// <param name="fileName">The name of the file to set up the mock for.</param>
        /// <param name="fileContents">The contents of the file.</param>
        public void SetupStandaloneOpenText(string fileName, byte[] fileContents) =>
            this.Setup(p => p.OpenText(It.Is<string>(q => q == fileName))).Returns(() => new StreamReader(new MemoryStream(fileContents)));

        /// <summary>
        /// Sets up a stand-alone writing operation for a self-closing and self-disposing stream.
        /// </summary>
        /// <param name="fileName">The name of the file to set up the mock for.</param>
        /// <returns>A <see cref="WriteMockWaiter"/> that can be used to determine when the file writing operation has finished and to assert on the resulting contents.</returns>
        public WriteMockWaiter SetupStandaloneOpenWrite(string fileName)
        {
            var wmw = new WriteMockWaiter();

            this.Setup(p => p.OpenWrite(It.Is<string>(q => q == fileName))).Returns(wmw.MemoryStream);

            return wmw;
        }
    }
}