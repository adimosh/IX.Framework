// <copyright file="IFile.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IX.System.IO
{
    /// <summary>
    /// Abstracts the <see cref="T:System.IO.File"/> class' static methods into a mock-able interface.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Appends lines of text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <remarks>
        /// <para>This operation always requires an encoding to be used. If <paramref name="encoding"/> is set to <see langword="null"/>, an implementation-specific
        /// encoding will be used.</para>
        /// </remarks>
        void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding = null);

        /// <summary>
        /// Appends text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <remarks>
        /// <para>This operation always requires an encoding to be used. If <paramref name="encoding"/> is set to <see langword="null"/>, an implementation-specific
        /// encoding will be used.</para>
        /// </remarks>
        void AppendAllText(string path, string contents, Encoding encoding = null);

        /// <summary>
        /// Opens a <see cref="T:System.IO.StreamWriter"/> to append text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.IO.StreamWriter"/> that can write to a file.</returns>
        StreamWriter AppendText(string path);

        /// <summary>
        /// Copies a file to another.
        /// </summary>
        /// <param name="sourceFileName">The source file.</param>
        /// <param name="destinationFileName">The destination file.</param>
        /// <param name="overwrite">If <see langword="true"/>, overwrites the destination file, if one exists, otherwise throws an exception. If a destination file doesn't
        /// exist, this parameter is ignored.</param>
        void Copy(string sourceFileName, string destinationFileName, bool overwrite = false);

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="bufferSize">The buffer size to use. Default is 4 kilobytes.</param>
        /// <returns>A <see cref="T:System.IO.Stream"/> that can read from and write to a file.</returns>
        Stream Create(string path, int bufferSize = 4096);

        /// <summary>
        /// Opens a <see cref="T:System.IO.StreamWriter"/> to write text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.IO.StreamWriter"/> that can write to a file.</returns>
        StreamWriter CreateText(string path);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        void Delete(string path);

        /// <summary>
        /// Checks whether a file exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>Returns <see langword="true"/> if the specified file exists and is accessible, <see langword="false"/> otherwise.</returns>
        bool Exists(string path);

        /// <summary>
        /// Gets a specific file's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.DateTime"/> in UTC.</returns>
        DateTime GetCreationTime(string path);

        /// <summary>
        /// Gets a specific file's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.DateTime"/> in UTC.</returns>
        DateTime GetLastAccessTime(string path);

        /// <summary>
        /// Gets a specific file's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.DateTime"/> in UTC.</returns>
        DateTime GetLastWriteTime(string path);

        /// <summary>
        /// Move a file.
        /// </summary>
        /// <param name="sourceFileName">The source file name.</param>
        /// <param name="destinationFileName">The destination file name.</param>
        void Move(string sourceFileName, string destinationFileName);

        /// <summary>
        /// Opens a <see cref="T:System.IO.Stream"/> to read from a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.IO.Stream"/> that can read from a file.</returns>
        Stream OpenRead(string path);

        /// <summary>
        /// Opens a <see cref="T:System.IO.StreamReader"/> to read text from a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.IO.StreamReader"/> that can read from a file.</returns>
        StreamReader OpenText(string path);

        /// <summary>
        /// Opens a <see cref="T:System.IO.Stream"/> to write to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A <see cref="T:System.IO.Stream"/> that can write to a file.</returns>
        Stream OpenWrite(string path);

        /// <summary>
        /// Reads the entire contents of a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>The contents of a file, in binary.</returns>
        byte[] ReadAllBytes(string path);

        /// <summary>
        /// Reads the entire contents of a file and splits them by end-of-line markers.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>An array of <see cref="string"/>.</returns>
        /// <remarks>
        /// <para>This operation always requires an encoding to be used. If <paramref name="encoding"/> is set to <see langword="null"/>, an implementation-specific
        /// encoding will be used.</para>
        /// </remarks>
        string[] ReadAllLines(string path, Encoding encoding = null);

        /// <summary>
        /// Reads the entire contents of a file as text.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>The entire file contents as a string.</returns>
        /// <remarks>
        /// <para>This operation always requires an encoding to be used. If <paramref name="encoding"/> is set to <see langword="null"/>, an implementation-specific
        /// encoding will be used.</para>
        /// </remarks>
        string ReadAllText(string path, Encoding encoding = null);

        /// <summary>
        /// Reads file contents as text line by line.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>An enumerable of strings, each representing one line of text.</returns>
        /// <remarks>
        /// <para>This operation always requires an encoding to be used. If <paramref name="encoding"/> is set to <see langword="null"/>, an implementation-specific
        /// encoding will be used.</para>
        /// </remarks>
        IEnumerable<string> ReadLines(string path, Encoding encoding = null);

        /// <summary>
        /// Sets the file's creation time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="creationTime">A <see cref="DateTime"/> with the file attribute to set.</param>
        void SetCreationTime(string path, DateTime creationTime);

        /// <summary>
        /// Sets the file's last access time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastAccessTime">A <see cref="DateTime"/> with the file attribute to set.</param>
        void SetLastAccessTime(string path, DateTime lastAccessTime);

        /// <summary>
        /// Sets the file's last write time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastWriteTime">A <see cref="DateTime"/> with the file attribute to set.</param>
        void SetLastWriteTime(string path, DateTime lastWriteTime);

        /// <summary>
        /// Writes binary contents to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="bytes">The contents to write.</param>
        void WriteAllBytes(string path, byte[] bytes);

        /// <summary>
        /// Writes lines of text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <remarks>
        /// <para>This operation always requires an encoding to be used. If <paramref name="encoding"/> is set to <see langword="null"/>, an implementation-specific
        /// encoding will be used.</para>
        /// </remarks>
        void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding = null);

        /// <summary>
        /// Writes text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <remarks>
        /// <para>This operation always requires an encoding to be used. If <paramref name="encoding"/> is set to <see langword="null"/>, an implementation-specific
        /// encoding will be used.</para>
        /// </remarks>
        void WriteAllText(string path, string contents, Encoding encoding = null);
    }
}