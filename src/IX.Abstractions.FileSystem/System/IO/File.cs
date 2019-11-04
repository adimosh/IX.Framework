// <copyright file="File.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Threading;
using JetBrains.Annotations;
using FSFile = System.IO.File;

// ReSharper disable once CheckNamespace
namespace IX.System.IO
{
    /// <summary>
    ///     A class for implementing <see cref="IFile" /> with <see cref="System.IO.File" />.
    /// </summary>
    /// <seealso cref="IX.System.IO.IFile" />
    /// <seealso cref="System.IO.File" />
    [PublicAPI]
    [SuppressMessage(
        "Performance",
        "HAA0603:Delegate allocation from a method group",
        Justification = "We're doing multithreaded, so this can't really be done in any other way.")]
    public class File : IFile
    {
        /// <summary>
        ///     Appends lines of text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public void AppendAllLines(
            string path,
            IEnumerable<string> contents,
            Encoding encoding = null)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNull(
                in contents,
                nameof(contents));

            if (encoding == null)
            {
                FSFile.AppendAllLines(
                    path,
                    contents);
            }
            else
            {
                FSFile.AppendAllLines(
                    path,
                    contents,
                    encoding);
            }
        }

        /// <summary>
        ///     Asynchronously appends lines of text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public Task AppendAllLinesAsync(
            string path,
            IEnumerable<string> contents,
            Encoding encoding = null,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNull(
                in contents,
                nameof(contents));

            return encoding == null
                ? Fire.OnThreadPool(
                    FSFile.AppendAllLines,
                    path,
                    contents,
                    cancellationToken)
                : Fire.OnThreadPool(
                    FSFile.AppendAllLines,
                    path,
                    contents,
                    encoding,
                    cancellationToken);
        }

        /// <summary>
        ///     Appends text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public void AppendAllText(
            string path,
            string contents,
            Encoding encoding = null)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                contents,
                nameof(contents));

            if (encoding == null)
            {
                FSFile.AppendAllText(
                    path,
                    contents);
            }
            else
            {
                FSFile.AppendAllText(
                    path,
                    contents,
                    encoding);
            }
        }

        /// <summary>
        ///     Asynchronously appends text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public Task AppendAllTextAsync(
            string path,
            string contents,
            Encoding encoding = null,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                contents,
                nameof(contents));

            return encoding == null
                ? Fire.OnThreadPool(
                    FSFile.AppendAllText,
                    path,
                    contents,
                    cancellationToken)
                : Fire.OnThreadPool(
                    FSFile.AppendAllText,
                    path,
                    contents,
                    encoding,
                    cancellationToken);
        }

        /// <summary>
        ///     Opens a <see cref="T:System.IO.StreamWriter" /> to append text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.IO.StreamWriter" /> that can write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public StreamWriter AppendText(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.AppendText(path);
        }

        /// <summary>
        ///     Copies a file to another.
        /// </summary>
        /// <param name="sourceFileName">The source file.</param>
        /// <param name="destFileName">The destination file.</param>
        /// <param name="overwrite">
        ///     If <see langword="true" />, overwrites the destination file, if one exists, otherwise throws an exception. If a
        ///     destination file doesn't
        ///     exist, this parameter is ignored.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public void Copy(
            string sourceFileName,
            string destFileName,
            bool overwrite = false)
        {
            Contract.RequiresNotNullOrWhitespace(
                sourceFileName,
                nameof(sourceFileName));
            Contract.RequiresNotNullOrWhitespace(
                destFileName,
                nameof(destFileName));

            FSFile.Copy(
                sourceFileName,
                destFileName,
                overwrite);
        }

        /// <summary>
        ///     Asynchronously copies a file to another.
        /// </summary>
        /// <param name="sourceFileName">The source file.</param>
        /// <param name="destinationFileName">The destination file.</param>
        /// <param name="overwrite">
        ///     If <see langword="true" />, overwrites the destination file, if one exists, otherwise throws an exception. If a
        ///     destination file doesn't
        ///     exist, this parameter is ignored.
        /// </param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task CopyAsync(
            string sourceFileName,
            string destinationFileName,
            bool overwrite = false,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                sourceFileName,
                nameof(sourceFileName));
            Contract.RequiresNotNullOrWhitespace(
                destinationFileName,
                nameof(destinationFileName));

            return Fire.OnThreadPool(
                FSFile.Copy,
                sourceFileName,
                destinationFileName,
                overwrite,
                cancellationToken);
        }

        /// <summary>
        ///     Creates a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="bufferSize">The buffer size to use. Default is 4 kilobytes.</param>
        /// <returns>
        ///     A <see cref="T:System.IO.Stream" /> that can read from and write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="bufferSize" /> is less than or equal to 0.
        /// </exception>
        public Stream Create(
            string path,
            int bufferSize = 4096)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresPositive(
                in bufferSize,
                nameof(bufferSize));

            return FSFile.Create(
                path,
                bufferSize);
        }

        /// <summary>
        ///     Opens a <see cref="T:System.IO.StreamWriter" /> to write text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.IO.StreamWriter" /> that can write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public StreamWriter CreateText(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.CreateText(path);
        }

        /// <summary>
        ///     Deletes a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public void Delete(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSFile.Delete(path);
        }

        /// <summary>
        ///     Asynchronously deletes a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task DeleteAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.Delete,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Checks whether a file exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     Returns <see langword="true" /> if the specified file exists and is accessible, <see langword="false" /> otherwise.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public bool Exists(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.Exists(path);
        }

        /// <summary>
        ///     Asynchronously checks whether a file exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>
        ///     Returns <see langword="true" /> if the specified file exists and is accessible, <see langword="false" /> otherwise.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task<bool> ExistsAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.Exists,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Gets a specific file's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public DateTime GetCreationTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.GetCreationTimeUtc(path);
        }

        /// <summary>
        ///     Asynchronously gets a specific file's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>
        ///     A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task<DateTime> GetCreationTimeAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.GetCreationTimeUtc,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Gets a specific file's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public DateTime GetLastAccessTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.GetLastAccessTimeUtc(path);
        }

        /// <summary>
        ///     Asynchronously gets a specific file's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>
        ///     A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task<DateTime> GetLastAccessTimeAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.GetLastAccessTimeUtc,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Gets a specific file's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public DateTime GetLastWriteTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        ///     Asynchronously gets a specific file's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>
        ///     A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task<DateTime> GetLastWriteTimeAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.GetLastWriteTimeUtc,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Move a file.
        /// </summary>
        /// <param name="sourceFileName">The source file name.</param>
        /// <param name="destFileName">The destination file name.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public void Move(
            string sourceFileName,
            string destFileName)
        {
            Contract.RequiresNotNullOrWhitespace(
                sourceFileName,
                nameof(sourceFileName));
            Contract.RequiresNotNullOrWhitespace(
                destFileName,
                nameof(destFileName));

            FSFile.Move(
                sourceFileName,
                destFileName);
        }

        /// <summary>
        ///     Asynchronously move a file.
        /// </summary>
        /// <param name="sourceFileName">The source file name.</param>
        /// <param name="destinationFileName">The destination file name.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task MoveAsync(
            string sourceFileName,
            string destinationFileName,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                sourceFileName,
                nameof(sourceFileName));
            Contract.RequiresNotNullOrWhitespace(
                destinationFileName,
                nameof(destinationFileName));

            return Fire.OnThreadPool(
                FSFile.Move,
                sourceFileName,
                destinationFileName,
                cancellationToken);
        }

        /// <summary>
        ///     Opens a <see cref="T:System.IO.Stream" /> to read from a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.IO.Stream" /> that can read from a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Stream OpenRead(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.OpenRead(path);
        }

        /// <summary>
        ///     Opens a <see cref="T:System.IO.StreamReader" /> to read text from a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.IO.StreamReader" /> that can read from a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public StreamReader OpenText(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.OpenText(path);
        }

        /// <summary>
        ///     Opens a <see cref="T:System.IO.Stream" /> to write to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     A <see cref="T:System.IO.Stream" /> that can write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Stream OpenWrite(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.OpenWrite(path);
        }

        /// <summary>
        ///     Reads the entire contents of a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        ///     The contents of a file, in binary.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public byte[] ReadAllBytes(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSFile.ReadAllBytes(path);
        }

        /// <summary>
        ///     Asynchronously reads the entire contents of a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>
        ///     The contents of a file, in binary.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task<byte[]> ReadAllBytesAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.ReadAllBytes,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Reads the entire contents of a file and splits them by end-of-line markers.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>
        ///     An array of <see cref="T:System.String" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public string[] ReadAllLines(
            string path,
            Encoding encoding = null)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return encoding == null
                ? FSFile.ReadAllLines(path)
                : FSFile.ReadAllLines(
                    path,
                    encoding);
        }

        /// <summary>
        ///     Asynchronously reads the entire contents of a file and splits them by end-of-line markers.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>
        ///     An array of <see cref="T:System.String" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public Task<string[]> ReadAllLinesAsync(
            string path,
            Encoding encoding = null,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return encoding == null
                ? Fire.OnThreadPool(
                    FSFile.ReadAllLines,
                    path,
                    cancellationToken)
                : Fire.OnThreadPool(
                    FSFile.ReadAllLines,
                    path,
                    encoding,
                    cancellationToken);
        }

        /// <summary>
        ///     Reads the entire contents of a file as text.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>
        ///     The entire file contents as a string.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public string ReadAllText(
            string path,
            Encoding encoding = null)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return encoding == null
                ? FSFile.ReadAllText(path)
                : FSFile.ReadAllText(
                    path,
                    encoding);
        }

        /// <summary>
        ///     Asynchronously reads the entire contents of a file as text.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>
        ///     The entire file contents as a string.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public Task<string> ReadAllTextAsync(
            string path,
            Encoding encoding = null,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return encoding == null
                ? Fire.OnThreadPool(
                    FSFile.ReadAllText,
                    path,
                    cancellationToken)
                : Fire.OnThreadPool(
                    FSFile.ReadAllText,
                    path,
                    encoding,
                    cancellationToken);
        }

        /// <summary>
        ///     Reads file contents as text line by line.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>
        ///     An enumerable of strings, each representing one line of text.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public IEnumerable<string> ReadLines(
            string path,
            Encoding encoding = null)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return encoding == null
                ? FSFile.ReadLines(path)
                : FSFile.ReadLines(
                    path,
                    encoding);
        }

        /// <summary>
        ///     Sets the file's creation time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="creationTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public void SetCreationTime(
            string path,
            DateTime creationTime)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSFile.SetCreationTimeUtc(
                path,
                creationTime);
        }

        /// <summary>
        ///     Asynchronously sets the file's creation time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="creationTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task SetCreationTimeAsync(
            string path,
            DateTime creationTime,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.SetCreationTimeUtc,
                path,
                creationTime,
                cancellationToken);
        }

        /// <summary>
        ///     Sets the file's last access time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public void SetLastAccessTime(
            string path,
            DateTime lastAccessTime)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSFile.SetLastAccessTimeUtc(
                path,
                lastAccessTime);
        }

        /// <summary>
        ///     Asynchronously sets the file's last access time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task SetLastAccessTimeAsync(
            string path,
            DateTime lastAccessTime,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.SetLastAccessTimeUtc,
                path,
                lastAccessTime,
                cancellationToken);
        }

        /// <summary>
        ///     Sets the file's last write time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public void SetLastWriteTime(
            string path,
            DateTime lastWriteTime)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSFile.SetLastWriteTimeUtc(
                path,
                lastWriteTime);
        }

        /// <summary>
        ///     Asynchronously sets the file's last write time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public Task SetLastWriteTimeAsync(
            string path,
            DateTime lastWriteTime,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSFile.SetLastWriteTimeUtc,
                path,
                lastWriteTime,
                cancellationToken);
        }

        /// <summary>
        ///     Writes binary contents to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="bytes">The contents to write.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        public void WriteAllBytes(
            string path,
            byte[] bytes)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrEmpty(
                bytes,
                nameof(bytes));

            FSFile.WriteAllBytes(
                path,
                bytes);
        }

        /// <summary>
        ///     Asynchronously writes binary contents to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="bytes">The contents to write.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="bytes" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        public Task WriteAllBytesAsync(
            string path,
            byte[] bytes,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrEmpty(
                bytes,
                nameof(bytes));

            return Fire.OnThreadPool(
                FSFile.WriteAllBytes,
                path,
                bytes,
                cancellationToken);
        }

        /// <summary>
        ///     Writes lines of text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public void WriteAllLines(
            string path,
            IEnumerable<string> contents,
            Encoding encoding = null)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNull(
                in contents,
                nameof(contents));

            if (encoding == null)
            {
                FSFile.WriteAllLines(
                    path,
                    contents);
            }
            else
            {
                FSFile.WriteAllLines(
                    path,
                    contents,
                    encoding);
            }
        }

        /// <summary>
        ///     Asynchronously writes lines of text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public Task WriteAllLinesAsync(
            string path,
            IEnumerable<string> contents,
            Encoding encoding = null,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNull(
                in contents,
                nameof(contents));

            return encoding == null
                ? Fire.OnThreadPool(
                    FSFile.WriteAllLines,
                    path,
                    contents,
                    cancellationToken)
                : Fire.OnThreadPool(
                    FSFile.WriteAllLines,
                    path,
                    contents,
                    encoding,
                    cancellationToken);
        }

        /// <summary>
        ///     Writes text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public void WriteAllText(
            string path,
            string contents,
            Encoding encoding = null)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                contents,
                nameof(contents));

            if (encoding == null)
            {
                FSFile.WriteAllText(
                    path,
                    contents);
            }
            else
            {
                FSFile.WriteAllText(
                    path,
                    contents,
                    encoding);
            }
        }

        /// <summary>
        ///     Asynchronously writes text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="cancellationToken">The cancellation token to stop this operation.</param>
        /// <returns>A task representing the current operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" /> (<see langword="Nothing" /> in
        ///     Visual Basic).
        /// </exception>
        /// <remarks>
        ///     This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to
        ///     <see langword="null" />, an implementation-specific
        ///     encoding will be used.
        /// </remarks>
        public Task WriteAllTextAsync(
            string path,
            string contents,
            Encoding encoding = null,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                contents,
                nameof(contents));

            return encoding == null
                ? Fire.OnThreadPool(
                    FSFile.WriteAllText,
                    path,
                    contents,
                    cancellationToken)
                : Fire.OnThreadPool(
                    FSFile.WriteAllText,
                    path,
                    contents,
                    encoding,
                    cancellationToken);
        }
    }
}