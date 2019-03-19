// <copyright file="IDirectory.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace IX.System.IO
{
    /// <summary>
    ///     Abstracts the <see cref="T:System.IO.Directory" /> class' static methods into a mock-able interface.
    /// </summary>
    [PublicAPI]
    public interface IDirectory
    {
        /// <summary>
        ///     Creates a new directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        void CreateDirectory([NotNull] string path);

        /// <summary>
        ///     Asynchronously creates a new directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task.
        /// </returns>
        [NotNull]
        Task CreateDirectoryAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Deletes a directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="recursive"><see langword="true" /> if deletion should be recursive to all subdirectories.</param>
        void Delete(
            [NotNull] string path,
            bool recursive = false);

        /// <summary>
        ///     Asynchronously deletes a directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="recursive"><see langword="true" /> if deletion should be recursive to all subdirectories.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        [NotNull]
        Task DeleteAsync(
            [NotNull] string path,
            bool recursive = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of directory paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateDirectories([NotNull] string path);

        /// <summary>
        ///     Asynchronously enumerates all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of directory paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateDirectoriesAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of directory paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateDirectories(
            [NotNull] string path,
            [NotNull] string searchPattern);

        /// <summary>
        ///     Asynchronously enumerates the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of directory paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateDirectoriesAsync(
            [NotNull] string path,
            [NotNull] string searchPattern,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates all the directories contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of directory paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateDirectoriesRecursively([NotNull] string path);

        /// <summary>
        ///     Enumerates all the directories contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of directory paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateDirectoriesRecursivelyAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates the directories contained at a certain directory and all the subdirectories with a specific search
        ///     pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of directory paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateDirectoriesRecursively(
            [NotNull] string path,
            [NotNull] string searchPattern);

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
        [NotNull]
        Task<IEnumerable<string>> EnumerateDirectoriesRecursivelyAsync(
            [NotNull] string path,
            [NotNull] string searchPattern,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFiles([NotNull] string path);

        /// <summary>
        ///     Asynchronously enumerates all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateFilesAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFiles(
            [NotNull] string path,
            [NotNull] string searchPattern);

        /// <summary>
        ///     Asynchronously enumerates the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateFilesAsync(
            [NotNull] string path,
            [NotNull] string searchPattern,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates all the files contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFilesRecursively([NotNull] string path);

        /// <summary>
        ///     Asynchronously enumerates all the files contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateFilesRecursivelyAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates the files contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFilesRecursively(
            [NotNull] string path,
            [NotNull] string searchPattern);

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
        [NotNull]
        Task<IEnumerable<string>> EnumerateFilesRecursivelyAsync(
            [NotNull] string path,
            [NotNull] string searchPattern,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates all the file system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFileSystemEntries([NotNull] string path);

        /// <summary>
        ///     Asynchronously enumerates all the file system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file system entry paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateFileSystemEntriesAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates the file system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFileSystemEntries(
            [NotNull] string path,
            [NotNull] string searchPattern);

        /// <summary>
        ///     Asynchronously enumerates the file system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file system entry paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateFileSystemEntriesAsync(
            [NotNull] string path,
            [NotNull] string searchPattern,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates all the file system entries contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFileSystemEntriesRecursively([NotNull] string path);

        /// <summary>
        ///     Asynchronously enumerates all the file system entries contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has an enumerable of file system entry paths as result.
        /// </returns>
        [NotNull]
        Task<IEnumerable<string>> EnumerateFileSystemEntriesRecursivelyAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Enumerates the file system entries contained at a certain directory and all the subdirectories with a specific
        ///     search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        [NotNull]
        IEnumerable<string> EnumerateFileSystemEntriesRecursively(
            [NotNull] string path,
            [NotNull] string searchPattern);

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
        [NotNull]
        Task<IEnumerable<string>> EnumerateFileSystemEntriesRecursivelyAsync(
            [NotNull] string path,
            [NotNull] string searchPattern,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Checks whether a directory exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///     Returns <see langword="true" /> if the specified directory exists and is accessible, <see langword="false" />
        ///     otherwise.
        /// </returns>
        bool Exists([NotNull] string path);

        /// <summary>
        ///     Asynchronously checks whether a directory exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     Returnsa task that has the result of a boolean of value <see langword="true" /> if the specified directory exists
        ///     and is accessible, <see langword="false" /> otherwise.
        /// </returns>
        [NotNull]
        Task<bool> ExistsAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets a specific directory's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>A <see cref="T:System.DateTime" /> in UTC.</returns>
        DateTime GetCreationTime([NotNull] string path);

        /// <summary>
        ///     Asynchronously gets a specific directory's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has a <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        [NotNull]
        Task<DateTime> GetCreationTimeAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets the current directory.
        /// </summary>
        /// <returns>The current directory.</returns>
        [NotNull]
        string GetCurrentDirectory();

        /// <summary>
        ///     Lists all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An array of directory paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateDirectories instead.")]
        [NotNull]
        string[] GetDirectories([NotNull] string path);

        /// <summary>
        ///     Lists all the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An array of directory paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateDirectories instead.")]
        [NotNull]
        string[] GetDirectories(
            [NotNull] string path,
            [NotNull] string searchPattern);

        /// <summary>
        ///     Lists all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An array of file paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFiles instead.")]
        [NotNull]
        string[] GetFiles([NotNull] string path);

        /// <summary>
        ///     Lists all the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An array of file paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFiles instead.")]
        [NotNull]
        string[] GetFiles(
            [NotNull] string path,
            [NotNull] string searchPattern);

        /// <summary>
        ///     Lists all the file system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An array of file system entry paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFileSystemEntries instead.")]
        [NotNull]
        string[] GetFileSystemEntries([NotNull] string path);

        /// <summary>
        ///     Lists all the file system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An array of file system entries paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFileSystemEntries instead.")]
        [NotNull]
        string[] GetFileSystemEntries(
            [NotNull] string path,
            [NotNull] string searchPattern);

        /// <summary>
        ///     Gets a specific directory's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>A <see cref="T:System.DateTime" /> in UTC.</returns>
        DateTime GetLastAccessTime([NotNull] string path);

        /// <summary>
        ///     GAsynchronously gets a specific directory's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has a <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        [NotNull]
        Task<DateTime> GetLastAccessTimeAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets a specific directory's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>A <see cref="T:System.DateTime" /> in UTC.</returns>
        DateTime GetLastWriteTime([NotNull] string path);

        /// <summary>
        ///     Asynchronously gets a specific directory's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A task that has a <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        [NotNull]
        Task<DateTime> GetLastWriteTimeAsync(
            [NotNull] string path,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Moves a directory to another location.
        /// </summary>
        /// <param name="sourceDirectoryName">The source directory name.</param>
        /// <param name="destinationDirectoryName">The destination directory name.</param>
        void Move(
            [NotNull] string sourceDirectoryName,
            [NotNull] string destinationDirectoryName);

        /// <summary>
        ///     Asynchronously moves a directory to another location.
        /// </summary>
        /// <param name="sourceDirectoryName">The source directory name.</param>
        /// <param name="destinationDirectoryName">The destination directory name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        [NotNull]
        Task MoveAsync(
            [NotNull] string sourceDirectoryName,
            [NotNull] string destinationDirectoryName,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Sets the directory's creation time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="creationTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        void SetCreationTime(
            [NotNull] string path,
            DateTime creationTime);

        /// <summary>
        ///     Asynchronously sets the directory's creation time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="creationTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        [NotNull]
        Task SetCreationTimeAsync(
            [NotNull] string path,
            DateTime creationTime,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Sets the directory's last access time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastAccessTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        void SetLastAccessTime(
            [NotNull] string path,
            DateTime lastAccessTime);

        /// <summary>
        ///     Asynchronously sets the directory's last access time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastAccessTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        [NotNull]
        Task SetLastAccessTimeAsync(
            [NotNull] string path,
            DateTime lastAccessTime,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Sets the directory's last write time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastWriteTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        void SetLastWriteTime(
            [NotNull] string path,
            DateTime lastWriteTime);

        /// <summary>
        ///     Asynchronously sets the directory's last write time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastWriteTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        [NotNull]
        Task SetLastWriteTimeAsync(
            [NotNull] string path,
            DateTime lastWriteTime,
            CancellationToken cancellationToken = default);
    }
}