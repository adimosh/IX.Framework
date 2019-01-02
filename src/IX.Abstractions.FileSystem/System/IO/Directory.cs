// <copyright file="Directory.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using IX.StandardExtensions.Contracts;

using FSDir = System.IO.Directory;

// ReSharper disable once CheckNamespace
namespace IX.System.IO
{
    /// <summary>
    /// A class for implementing <see cref="IDirectory" /> with <see cref="System.IO.Directory" />.
    /// </summary>
    /// <seealso cref="IX.System.IO.IDirectory" />
    /// <seealso cref="System.IO.Directory" />
    public class Directory : IDirectory
    {
        /// <summary>
        /// The all file default search pattern.
        /// </summary>
        private const string AllFilePattern = "*.*";

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="path">The path of the new directory.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public void CreateDirectory(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            FSDir.CreateDirectory(path);
        }

        /// <summary>
        /// Deletes a directory, optionally also doing a recursive delete.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="recursive">If set to <see langword="true"/>, does a recursive delete. This is <see langword="false"/> by default.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public void Delete(string path, bool recursive = false)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            if (recursive)
            {
                FSDir.Delete(path, true);
            }
            else
            {
                FSDir.Delete(path);
            }
        }

        /// <summary>
        /// Enumerates the subdirectories of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the subdirectories of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateDirectories(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return this.EnumerateDirectoriesInternal(path, AllFilePattern, false);
        }

        /// <summary>
        /// Enumerates the subdirectories of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The pattern to search.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the subdirectories of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/>
        /// or
        /// <paramref name="searchPattern"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return this.EnumerateDirectoriesInternal(path, searchPattern, false);
        }

        /// <summary>
        /// Enumerates all the directories contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of directory paths.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateDirectoriesRecursively(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return this.EnumerateDirectoriesInternal(path, AllFilePattern, true);
        }

        /// <summary>
        /// Enumerates the directories contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of directory paths.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/>
        /// or
        /// <paramref name="searchPattern"/>
        /// is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectoriesRecursively(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return this.EnumerateDirectoriesInternal(path, searchPattern, true);
        }

        /// <summary>
        /// Enumerates the files of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the files of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFiles(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return this.EnumerateFilesInternal(path, AllFilePattern, false);
        }

        /// <summary>
        /// Enumerates the files of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The pattern to search.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the files of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return this.EnumerateFilesInternal(path, searchPattern, false);
        }

        /// <summary>
        /// Enumerates all the files contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file paths.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFilesRecursively(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return this.EnumerateFilesInternal(path, AllFilePattern, true);
        }

        /// <summary>
        /// Enumerates the files contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file paths.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFilesRecursively(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return this.EnumerateFilesInternal(path, searchPattern, true);
        }

        /// <summary>
        /// Enumerates the file system entries of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the file system entries of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return this.EnumerateFileSystemEntriesInternal(path, AllFilePattern, false);
        }

        /// <summary>
        /// Enumerates the file system entries of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The pattern to search.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the file system entries of this directory.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return this.EnumerateFileSystemEntriesInternal(path, searchPattern, false);
        }

        /// <summary>
        /// Enumerates all the file system entries contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return this.EnumerateFileSystemEntriesInternal(path, AllFilePattern, true);
        }

        /// <summary>
        /// Enumerates the file system entries contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return this.EnumerateFileSystemEntriesInternal(path, searchPattern, true);
        }

        /// <summary>
        /// Checks whether a certain subdirectory exists.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///   <see langword="true"/> if the directory exists, <see langword="false"/> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public bool Exists(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return FSDir.Exists(path);
        }

        /// <summary>
        /// Gets a specific directory's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// A <see cref="DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public DateTime GetCreationTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return FSDir.GetCreationTimeUtc(path);
        }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        /// <returns>The current directory.</returns>
        public string GetCurrentDirectory() => FSDir.GetCurrentDirectory();

        /// <summary>
        /// Lists all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An array of directory paths.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public string[] GetDirectories(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return FSDir.GetDirectories(path);
        }

        /// <summary>
        /// Lists all the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>
        /// An array of directory paths.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public string[] GetDirectories(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return FSDir.GetDirectories(path, searchPattern);
        }

        /// <summary>
        /// Lists all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An array of file paths.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public string[] GetFiles(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return FSDir.GetFiles(path);
        }

        /// <summary>
        /// Lists all the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>
        /// An array of file paths.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public string[] GetFiles(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return FSDir.GetFiles(path, searchPattern);
        }

        /// <summary>
        /// Lists all the file-system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An array of file-system entry paths.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public string[] GetFileSystemEntries(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return FSDir.GetFileSystemEntries(path);
        }

        /// <summary>
        /// Lists all the file-system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>
        /// An array of file-system entries paths.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public string[] GetFileSystemEntries(string path, string searchPattern)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));
            Contract.RequiresNotNullOrWhitespace(searchPattern, nameof(searchPattern));

            return FSDir.GetFileSystemEntries(path, searchPattern);
        }

        /// <summary>
        /// Gets a specific directory's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// A <see cref="DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public DateTime GetLastAccessTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return FSDir.GetLastAccessTimeUtc(path);
        }

        /// <summary>
        /// Gets a specific directory's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// A <see cref="DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public DateTime GetLastWriteTime(string path)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            return FSDir.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        /// Moves a directory to another location.
        /// </summary>
        /// <param name="sourceDirName">The source directory name.</param>
        /// <param name="destDirName">The destination directory name.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destDirName"/> or <paramref name="sourceDirName"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public void Move(string sourceDirName, string destDirName)
        {
            Contract.RequiresNotNullOrWhitespace(sourceDirName, nameof(sourceDirName));
            Contract.RequiresNotNullOrWhitespace(destDirName, nameof(destDirName));

            FSDir.Move(sourceDirName, destDirName);
        }

        /// <summary>
        /// Sets the directory's creation time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="creationTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public void SetCreationTime(string path, DateTime creationTime)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            FSDir.SetCreationTime(path, creationTime);
        }

        /// <summary>
        /// Sets the directory's last access time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastAccessTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            FSDir.SetLastAccessTime(path, lastAccessTime);
        }

        /// <summary>
        /// Sets the directory's last write time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastWriteTime">A <see cref="DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            Contract.RequiresNotNullOrWhitespace(path, nameof(path));

            FSDir.SetLastWriteTime(path, lastWriteTime);
        }

        /// <summary>
        /// Enumerates directories.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchPattern">
        /// The search pattern.
        /// </param>
        /// <param name="recursively">
        /// The recursively.
        /// </param>
        /// <returns>
        /// The The enumerated directories.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IEnumerable<string> EnumerateDirectoriesInternal(string path, string searchPattern, bool recursively)
        {
            Contract.RequiresNotNullOrWhitespacePrivate(path, nameof(path));
            Contract.RequiresNotNullOrWhitespacePrivate(searchPattern, nameof(searchPattern));

            return FSDir.EnumerateDirectories(
                path,
                searchPattern,
                recursively ? global::System.IO.SearchOption.AllDirectories : global::System.IO.SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Enumerates files.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchPattern">
        /// The search pattern.
        /// </param>
        /// <param name="recursively">
        /// The recursively.
        /// </param>
        /// <returns>
        /// The The enumerated files.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IEnumerable<string> EnumerateFilesInternal(string path, string searchPattern, bool recursively)
        {
            Contract.RequiresNotNullOrWhitespacePrivate(path, nameof(path));
            Contract.RequiresNotNullOrWhitespacePrivate(searchPattern, nameof(searchPattern));

            return FSDir.EnumerateFiles(
                path,
                searchPattern,
                recursively ? global::System.IO.SearchOption.AllDirectories : global::System.IO.SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Enumerates file system entries.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchPattern">
        /// The search pattern.
        /// </param>
        /// <param name="recursively">
        /// The recursively.
        /// </param>
        /// <returns>
        /// The The enumerated file system entries.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IEnumerable<string> EnumerateFileSystemEntriesInternal(
            string path,
            string searchPattern,
            bool recursively)
        {
            Contract.RequiresNotNullOrWhitespacePrivate(path, nameof(path));
            Contract.RequiresNotNullOrWhitespacePrivate(searchPattern, nameof(searchPattern));

            return FSDir.EnumerateFileSystemEntries(
                path,
                searchPattern,
                recursively ? global::System.IO.SearchOption.AllDirectories : global::System.IO.SearchOption.TopDirectoryOnly);
        }
    }
}