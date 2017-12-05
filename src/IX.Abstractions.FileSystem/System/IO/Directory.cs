// <copyright file="Directory.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using FSDir = System.IO.Directory;

namespace IX.System.IO
{
    /// <summary>
    /// A class for implementing <see cref="IDirectory" /> with <see cref="System.IO.Directory" />.
    /// </summary>
    /// <seealso cref="IX.System.IO.IDirectory" />
    /// <seealso cref="System.IO.Directory" />
    public class Directory : IDirectory
    {
        private const string AllFilePattern = "*.*";

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="path">The path of the new directory.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public void CreateDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSDir.CreateDirectory(path);
        }

        /// <summary>
        /// Deletes a directory, optionally also doing a recursive delete.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="recursive">If set to <c>true</c>, does a recursive delete. This is <c>false</c> by default.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public void Delete(string path, bool recursive = false)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateDirectories(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/>
        /// or
        /// <paramref name="searchPattern"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return this.EnumerateDirectoriesInternal(path, searchPattern, false);
        }

        /// <summary>
        /// Enumerates all the directories contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of directory paths.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public IEnumerable<string> EnumerateDirectoriesRecursively(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return this.EnumerateDirectoriesInternal(path, AllFilePattern, true);
        }

        /// <summary>
        /// Enumerates the directories contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of directory paths.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/>
        /// or
        /// <paramref name="searchPattern"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).
        /// </exception>
        public IEnumerable<string> EnumerateDirectoriesRecursively(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return this.EnumerateDirectoriesInternal(path, searchPattern, true);
        }

        /// <summary>
        /// Enumerates the files of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the files of this directory.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFiles(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return this.EnumerateFilesInternal(path, searchPattern, false);
        }

        /// <summary>
        /// Enumerates all the files contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file paths.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFilesRecursively(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return this.EnumerateFilesInternal(path, AllFilePattern, true);
        }

        /// <summary>
        /// Enumerates the files contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file paths.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFilesRecursively(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return this.EnumerateFilesInternal(path, searchPattern, true);
        }

        /// <summary>
        /// Enumerates the file system entries of a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An enumerable of <c>string</c> values with the paths of the file system entries of this directory.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return this.EnumerateFileSystemEntriesInternal(path, searchPattern, false);
        }

        /// <summary>
        /// Enumerates all the file system entries contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return this.EnumerateFileSystemEntriesInternal(path, AllFilePattern, true);
        }

        /// <summary>
        /// Enumerates the file system entries contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return this.EnumerateFileSystemEntriesInternal(path, searchPattern, true);
        }

        /// <summary>
        /// Checks whether a certain subdirectory exists.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        ///   <c>true</c> if the directory exists, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public bool Exists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSDir.Exists(path);
        }

        /// <summary>
        /// Gets a specific directory's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public DateTime GetCreationTime(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public string[] GetDirectories(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public string[] GetDirectories(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return FSDir.GetDirectories(path, searchPattern);
        }

        /// <summary>
        /// Lists all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An array of file paths.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public string[] GetFiles(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public string[] GetFiles(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return FSDir.GetFiles(path, searchPattern);
        }

        /// <summary>
        /// Lists all the filesystem entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An array of filesystem entry paths.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public string[] GetFileSystemEntries(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSDir.GetFileSystemEntries(path);
        }

        /// <summary>
        /// Lists all the filesystem entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>
        /// An array of filesystem entries paths.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> or <paramref name="searchPattern"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public string[] GetFileSystemEntries(string path, string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            return FSDir.GetFileSystemEntries(path, searchPattern);
        }

        /// <summary>
        /// Gets a specific directory's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public DateTime GetLastAccessTime(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSDir.GetLastAccessTimeUtc(path);
        }

        /// <summary>
        /// Gets a specific directory's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public DateTime GetLastWriteTime(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSDir.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        /// Moves a directory to another location.
        /// </summary>
        /// <param name="sourceDirName">The source directory name.</param>
        /// <param name="destDirName">The destination directory name.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="destDirName"/> or <paramref name="sourceDirName"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void Move(string sourceDirName, string destDirName)
        {
            if (string.IsNullOrWhiteSpace(sourceDirName))
            {
                throw new ArgumentNullException(nameof(sourceDirName));
            }

            if (string.IsNullOrWhiteSpace(destDirName))
            {
                throw new ArgumentNullException(nameof(destDirName));
            }

            FSDir.Move(sourceDirName, destDirName);
        }

        /// <summary>
        /// Sets the directory's creation time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="creationTime">A <see cref="T:System.DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public void SetCreationTime(string path, DateTime creationTime)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSDir.SetCreationTime(path, creationTime);
        }

        /// <summary>
        /// Sets the directory's last access time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSDir.SetLastAccessTime(path, lastAccessTime);
        }

        /// <summary>
        /// Sets the directory's last write time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> with the directory attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)</exception>
        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSDir.SetLastWriteTime(path, lastWriteTime);
        }

        private IEnumerable<string> EnumerateDirectoriesInternal(string path, string searchPattern, bool recursively)
            => FSDir.EnumerateDirectories(path, searchPattern, recursively ? global::System.IO.SearchOption.AllDirectories : global::System.IO.SearchOption.TopDirectoryOnly);

        private IEnumerable<string> EnumerateFilesInternal(string path, string searchPattern, bool recursively)
            => FSDir.EnumerateFiles(path, searchPattern, recursively ? global::System.IO.SearchOption.AllDirectories : global::System.IO.SearchOption.TopDirectoryOnly);

        private IEnumerable<string> EnumerateFileSystemEntriesInternal(string path, string searchPattern, bool recursively)
            => FSDir.EnumerateFileSystemEntries(path, searchPattern, recursively ? global::System.IO.SearchOption.AllDirectories : global::System.IO.SearchOption.TopDirectoryOnly);
    }
}