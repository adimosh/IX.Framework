// <copyright file="Directory.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Threading;
using JetBrains.Annotations;
using FSDir = System.IO.Directory;

// ReSharper disable once CheckNamespace
namespace IX.System.IO
{
    /// <summary>
    ///     A class for implementing <see cref="IDirectory" /> with <see cref="System.IO.Directory" />.
    /// </summary>
    /// <seealso cref="IX.System.IO.IDirectory" />
    /// <seealso cref="System.IO.Directory" />
    [PublicAPI]
    public class Directory : IDirectory
    {
        /// <summary>
        ///     The all file default search pattern.
        /// </summary>
        private const string AllFilePattern = "*.*";

#pragma warning disable HAA0603 // Delegate allocation from a method group
        /// <summary>
        ///     Creates a new directory.
        /// </summary>
        /// <param name="path">The path of the new directory.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public void CreateDirectory(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSDir.CreateDirectory(path);
        }

        /// <summary>
        ///     Asynchronously creates a new directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task.
        /// </returns>
        public Task CreateDirectoryAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.CreateDirectory,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Deletes a directory, optionally also doing a recursive delete.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="recursive">
        ///     If set to <see langword="true" />, does a recursive delete. This is <see langword="false" /> by
        ///     default.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public void Delete(
            string path,
            bool recursive = false)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            if (recursive)
            {
                FSDir.Delete(
                    path,
                    true);
            }
            else
            {
                FSDir.Delete(path);
            }
        }

        /// <summary>
        ///     Asynchronously deletes a directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="recursive"><see langword="true" /> if deletion should be recursive to all subdirectories.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        public Task DeleteAsync(
            string path,
            bool recursive = false,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            if (recursive)
            {
                return Fire.OnThreadPool(
                    FSDir.Delete,
                    path,
                    true,
                    cancellationToken);
            }

            return Fire.OnThreadPool(
                FSDir.Delete,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the subdirectories of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     An enumerable of <c>string</c> values with the paths of the subdirectories of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectories(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return this.EnumerateDirectoriesInternal(
                path,
                AllFilePattern,
                false);
        }

        /// <summary>
        ///     Asynchronously enumerates all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of directory paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateDirectoriesAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                this.EnumerateDirectoriesInternal,
                path,
                AllFilePattern,
                false,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the subdirectories of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The pattern to search.</param>
        /// <returns>
        ///     An enumerable of <c>string</c> values with the paths of the subdirectories of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" />
        ///     or
        ///     <paramref name="searchPattern" />
        ///     is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectories(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return this.EnumerateDirectoriesInternal(
                path,
                searchPattern,
                false);
        }

        /// <summary>
        ///     Asynchronously enumerates the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of directory paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateDirectoriesAsync(
            string path,
            string searchPattern,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return Fire.OnThreadPool(
                this.EnumerateDirectoriesInternal,
                path,
                searchPattern,
                false,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates all the directories contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of directory paths.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectoriesRecursively(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return this.EnumerateDirectoriesInternal(
                path,
                AllFilePattern,
                true);
        }

        /// <summary>
        ///     Enumerates all the directories contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of directory paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateDirectoriesRecursivelyAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                this.EnumerateDirectoriesInternal,
                path,
                AllFilePattern,
                true,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the directories contained at a certain directory and all the subdirectories with a specific search
        ///     pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of directory paths.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" />
        ///     or
        ///     <paramref name="searchPattern" />
        ///     is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectoriesRecursively(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return this.EnumerateDirectoriesInternal(
                path,
                searchPattern,
                true);
        }

        /// <summary>
        ///     Asynchronously enumerates the directories contained at a certain directory and all the subdirectories with a
        ///     specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of directory paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateDirectoriesRecursivelyAsync(
            string path,
            string searchPattern,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return Fire.OnThreadPool(
                this.EnumerateDirectoriesInternal,
                path,
                searchPattern,
                true,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the files of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     An enumerable of <c>string</c> values with the paths of the files of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFiles(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return this.EnumerateFilesInternal(
                path,
                AllFilePattern,
                false);
        }

        /// <summary>
        ///     Asynchronously enumerates all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFilesAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                this.EnumerateFilesInternal,
                path,
                AllFilePattern,
                false,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the files of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The pattern to search.</param>
        /// <returns>
        ///     An enumerable of <c>string</c> values with the paths of the files of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="searchPattern" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFiles(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return this.EnumerateFilesInternal(
                path,
                searchPattern,
                false);
        }

        /// <summary>
        ///     Asynchronously enumerates the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFilesAsync(
            string path,
            string searchPattern,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return Fire.OnThreadPool(
                this.EnumerateFilesInternal,
                path,
                searchPattern,
                false,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates all the files contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file paths.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFilesRecursively(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return this.EnumerateFilesInternal(
                path,
                AllFilePattern,
                true);
        }

        /// <summary>
        ///     Asynchronously enumerates all the files contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFilesRecursivelyAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                this.EnumerateFilesInternal,
                path,
                AllFilePattern,
                true,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the files contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file paths.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="searchPattern" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFilesRecursively(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return this.EnumerateFilesInternal(
                path,
                searchPattern,
                true);
        }

        /// <summary>
        ///     Asynchronously enumerates the files contained at a certain directory and all the subdirectories with a specific
        ///     search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFilesRecursivelyAsync(
            string path,
            string searchPattern,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return Fire.OnThreadPool(
                this.EnumerateFilesInternal,
                path,
                searchPattern,
                true,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the file system entries of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     An enumerable of <c>string</c> values with the paths of the file system entries of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return this.EnumerateFileSystemEntriesInternal(
                path,
                AllFilePattern,
                false);
        }

        /// <summary>
        ///     Asynchronously enumerates all the file system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file system entry paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFileSystemEntriesAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                this.EnumerateFileSystemEntriesInternal,
                path,
                AllFilePattern,
                false,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the file system entries of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The pattern to search.</param>
        /// <returns>
        ///     An enumerable of <c>string</c> values with the paths of the file system entries of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="searchPattern" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFileSystemEntries(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return this.EnumerateFileSystemEntriesInternal(
                path,
                searchPattern,
                false);
        }

        /// <summary>
        ///     Asynchronously enumerates the file system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file system entry paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFileSystemEntriesAsync(
            string path,
            string searchPattern,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return Fire.OnThreadPool(
                this.EnumerateFileSystemEntriesInternal,
                path,
                searchPattern,
                false,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates all the file system entries contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return this.EnumerateFileSystemEntriesInternal(
                path,
                AllFilePattern,
                true);
        }

        /// <summary>
        ///     Asynchronously enumerates all the file system entries contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file system entry paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFileSystemEntriesRecursivelyAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                this.EnumerateFileSystemEntriesInternal,
                path,
                AllFilePattern,
                true,
                cancellationToken);
        }

        /// <summary>
        ///     Enumerates the file system entries contained at a certain directory and all the subdirectories with a specific
        ///     search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="searchPattern" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return this.EnumerateFileSystemEntriesInternal(
                path,
                searchPattern,
                true);
        }

        /// <summary>
        ///     EAsynchronously enumerates the file system entries contained at a certain directory and all the subdirectories with
        ///     a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file system entry paths as result.
        /// </returns>
        public Task<IEnumerable<string>> EnumerateFileSystemEntriesRecursivelyAsync(
            string path,
            string searchPattern,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return Fire.OnThreadPool(
                this.EnumerateFileSystemEntriesInternal,
                path,
                searchPattern,
                true,
                cancellationToken);
        }

        /// <summary>
        ///     Checks whether a certain subdirectory exists.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     <see langword="true" /> if the directory exists, <see langword="false" /> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public bool Exists(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSDir.Exists(path);
        }

        /// <summary>
        ///     Asynchronously checks whether a directory exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     Returnsa task that has the result of a boolean of value <see langword="true" /> if the specified directory exists
        ///     and is accessible, <see langword="false" /> otherwise.
        /// </returns>
        public Task<bool> ExistsAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.Exists,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Gets a specific directory's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     A <see cref="DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public DateTime GetCreationTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSDir.GetCreationTimeUtc(path);
        }

        /// <summary>
        ///     Asynchronously gets a specific directory's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has a <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        public Task<DateTime> GetCreationTimeAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.GetCreationTimeUtc,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Gets the current directory.
        /// </summary>
        /// <returns>The current directory.</returns>
        public string GetCurrentDirectory() => FSDir.GetCurrentDirectory();

        /// <summary>
        ///     Lists all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     An array of directory paths.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public string[] GetDirectories(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSDir.GetDirectories(path);
        }

        /// <summary>
        ///     Lists all the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>
        ///     An array of directory paths.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="searchPattern" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public string[] GetDirectories(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return FSDir.GetDirectories(
                path,
                searchPattern);
        }

        /// <summary>
        ///     Lists all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     An array of file paths.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public string[] GetFiles(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSDir.GetFiles(path);
        }

        /// <summary>
        ///     Lists all the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>
        ///     An array of file paths.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="searchPattern" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public string[] GetFiles(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return FSDir.GetFiles(
                path,
                searchPattern);
        }

        /// <summary>
        ///     Lists all the file-system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     An array of file-system entry paths.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public string[] GetFileSystemEntries(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSDir.GetFileSystemEntries(path);
        }

        /// <summary>
        ///     Lists all the file-system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>
        ///     An array of file-system entries paths.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> or <paramref name="searchPattern" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public string[] GetFileSystemEntries(
            string path,
            string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespace(
                searchPattern,
                nameof(searchPattern));

            return FSDir.GetFileSystemEntries(
                path,
                searchPattern);
        }

        /// <summary>
        ///     Gets a specific directory's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     A <see cref="DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public DateTime GetLastAccessTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSDir.GetLastAccessTimeUtc(path);
        }

        /// <summary>
        ///     GAsynchronously gets a specific directory's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has a <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        public Task<DateTime> GetLastAccessTimeAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.GetLastAccessTimeUtc,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Gets a specific directory's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     A <see cref="DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public DateTime GetLastWriteTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return FSDir.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        ///     Asynchronously gets a specific directory's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has a <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        public Task<DateTime> GetLastWriteTimeAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.GetLastWriteTimeUtc,
                path,
                cancellationToken);
        }

        /// <summary>
        ///     Moves a directory to another location.
        /// </summary>
        /// <param name="sourceDirName">The source directory name.</param>
        /// <param name="destDirName">The destination directory name.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="destDirName" /> or <paramref name="sourceDirName" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public void Move(
            string sourceDirName,
            string destDirName)
        {
            Contract.RequiresNotNullOrWhitespace(
                sourceDirName,
                nameof(sourceDirName));
            Contract.RequiresNotNullOrWhitespace(
                destDirName,
                nameof(destDirName));

            FSDir.Move(
                sourceDirName,
                destDirName);
        }

        /// <summary>
        ///     Asynchronously moves a directory to another location.
        /// </summary>
        /// <param name="sourceDirectoryName">The source directory name.</param>
        /// <param name="destinationDirectoryName">The destination directory name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        public Task MoveAsync(
            string sourceDirectoryName,
            string destinationDirectoryName,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                sourceDirectoryName,
                nameof(sourceDirectoryName));
            Contract.RequiresNotNullOrWhitespace(
                destinationDirectoryName,
                nameof(destinationDirectoryName));

            return Fire.OnThreadPool(
                FSDir.Move,
                sourceDirectoryName,
                destinationDirectoryName,
                cancellationToken);
        }

        /// <summary>
        ///     Sets the directory's creation time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="creationTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public void SetCreationTime(
            string path,
            DateTime creationTime)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSDir.SetCreationTime(
                path,
                creationTime);
        }

        /// <summary>
        ///     Asynchronously sets the directory's creation time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="creationTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        public Task SetCreationTimeAsync(
            string path,
            DateTime creationTime,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.SetCreationTime,
                path,
                creationTime,
                cancellationToken);
        }

        /// <summary>
        ///     Sets the directory's last access time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastAccessTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public void SetLastAccessTime(
            string path,
            DateTime lastAccessTime)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSDir.SetLastAccessTime(
                path,
                lastAccessTime);
        }

        /// <summary>
        ///     Asynchronously sets the directory's last access time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastAccessTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        public Task SetLastAccessTimeAsync(
            string path,
            DateTime lastAccessTime,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.SetLastAccessTime,
                path,
                lastAccessTime,
                cancellationToken);
        }

        /// <summary>
        ///     Sets the directory's last write time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastWriteTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path" /> is <see langword="null" /> (<see langword="Nothing" />
        ///     in Visual Basic).
        /// </exception>
        public void SetLastWriteTime(
            string path,
            DateTime lastWriteTime)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            FSDir.SetLastWriteTime(
                path,
                lastWriteTime);
        }

        /// <summary>
        ///     Asynchronously sets the directory's last write time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastWriteTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        public Task SetLastWriteTimeAsync(
            string path,
            DateTime lastWriteTime,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNullOrWhitespace(
                path,
                nameof(path));

            return Fire.OnThreadPool(
                FSDir.SetLastWriteTime,
                path,
                lastWriteTime,
                cancellationToken);
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group

        /// <summary>
        ///     Enumerates directories.
        /// </summary>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <param name="searchPattern">
        ///     The search pattern.
        /// </param>
        /// <param name="recursively">
        ///     The recursively.
        /// </param>
        /// <returns>
        ///     The The enumerated directories.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IEnumerable<string> EnumerateDirectoriesInternal(
            string path,
            string searchPattern,
            bool recursively)
        {
            Contract.RequiresNotNullOrWhitespacePrivate(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespacePrivate(
                searchPattern,
                nameof(searchPattern));

            return FSDir.EnumerateDirectories(
                path,
                searchPattern,
                recursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        ///     Enumerates files.
        /// </summary>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <param name="searchPattern">
        ///     The search pattern.
        /// </param>
        /// <param name="recursively">
        ///     The recursively.
        /// </param>
        /// <returns>
        ///     The The enumerated files.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IEnumerable<string> EnumerateFilesInternal(
            string path,
            string searchPattern,
            bool recursively)
        {
            Contract.RequiresNotNullOrWhitespacePrivate(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespacePrivate(
                searchPattern,
                nameof(searchPattern));

            return FSDir.EnumerateFiles(
                path,
                searchPattern,
                recursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        ///     Enumerates file system entries.
        /// </summary>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <param name="searchPattern">
        ///     The search pattern.
        /// </param>
        /// <param name="recursively">
        ///     The recursively.
        /// </param>
        /// <returns>
        ///     The The enumerated file system entries.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IEnumerable<string> EnumerateFileSystemEntriesInternal(
            string path,
            string searchPattern,
            bool recursively)
        {
            Contract.RequiresNotNullOrWhitespacePrivate(
                path,
                nameof(path));
            Contract.RequiresNotNullOrWhitespacePrivate(
                searchPattern,
                nameof(searchPattern));

            return FSDir.EnumerateFileSystemEntries(
                path,
                searchPattern,
                recursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}