// <copyright file="IDirectory.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.System.IO
{
    /// <summary>
    /// Abstracts the <see cref="T:System.IO.Directory"/> class' static methods into a mock-able interface.
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        void CreateDirectory(string path);

        /// <summary>
        /// Deletes a directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="recursive"><see langword="true"/> if deletion should be recursive to all subdirectories.</param>
        void Delete(string path, bool recursive = false);

        /// <summary>
        /// Enumerates all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of directory paths.</returns>
        IEnumerable<string> EnumerateDirectories(string path);

        /// <summary>
        /// Enumerates the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of directory paths.</returns>
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern);

        /// <summary>
        /// Enumerates all the directories contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of directory paths.</returns>
        IEnumerable<string> EnumerateDirectoriesRecursively(string path);

        /// <summary>
        /// Enumerates the directories contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of directory paths.</returns>
        IEnumerable<string> EnumerateDirectoriesRecursively(string path, string searchPattern);

        /// <summary>
        /// Enumerates all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file paths.</returns>
        IEnumerable<string> EnumerateFiles(string path);

        /// <summary>
        /// Enumerates the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file paths.</returns>
        IEnumerable<string> EnumerateFiles(string path, string searchPattern);

        /// <summary>
        /// Enumerates all the files contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file paths.</returns>
        IEnumerable<string> EnumerateFilesRecursively(string path);

        /// <summary>
        /// Enumerates the files contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file paths.</returns>
        IEnumerable<string> EnumerateFilesRecursively(string path, string searchPattern);

        /// <summary>
        /// Enumerates all the file system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        IEnumerable<string> EnumerateFileSystemEntries(string path);

        /// <summary>
        /// Enumerates the file system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern);

        /// <summary>
        /// Enumerates all the file system entries contained at a certain directory and all the subdirectories.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path);

        /// <summary>
        /// Enumerates the file system entries contained at a certain directory and all the subdirectories with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An enumerable of file system entry paths.</returns>
        IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path, string searchPattern);

        /// <summary>
        /// Checks whether a directory exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>Returns <see langword="true"/> if the specified directory exists and is accessible, <see langword="false"/> otherwise.</returns>
        bool Exists(string path);

        /// <summary>
        /// Gets a specific directory's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>A <see cref="T:System.DateTime"/> in UTC.</returns>
        DateTime GetCreationTime(string path);

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        /// <returns>The current directory.</returns>
        string GetCurrentDirectory();

        /// <summary>
        /// Lists all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An array of directory paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateDirectories instead.")]
        string[] GetDirectories(string path);

        /// <summary>
        /// Lists all the directories contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An array of directory paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateDirectories instead.")]
        string[] GetDirectories(string path, string searchPattern);

        /// <summary>
        /// Lists all the files contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An array of file paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFiles instead.")]
        string[] GetFiles(string path);

        /// <summary>
        /// Lists all the files contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An array of file paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFiles instead.")]
        string[] GetFiles(string path, string searchPattern);

        /// <summary>
        /// Lists all the file system entries contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>An array of file system entry paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFileSystemEntries instead.")]
        string[] GetFileSystemEntries(string path);

        /// <summary>
        /// Lists all the file system entries contained at a certain directory with a specific search pattern.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <returns>An array of file system entries paths.</returns>
        [Obsolete("This method is obsolete, please use the EnumerateFileSystemEntries instead.")]
        string[] GetFileSystemEntries(string path, string searchPattern);

        /// <summary>
        /// Gets a specific directory's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>A <see cref="T:System.DateTime"/> in UTC.</returns>
        DateTime GetLastAccessTime(string path);

        /// <summary>
        /// Gets a specific directory's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>A <see cref="T:System.DateTime"/> in UTC.</returns>
        DateTime GetLastWriteTime(string path);

        /// <summary>
        /// Moves a directory to another location.
        /// </summary>
        /// <param name="sourceDirectoryName">The source directory name.</param>
        /// <param name="destinationDirectoryName">The destination directory name.</param>
        void Move(string sourceDirectoryName, string destinationDirectoryName);

        /// <summary>
        /// Sets the directory's creation time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="creationTime">A <see cref="DateTime"/> with the directory attribute to set.</param>
        void SetCreationTime(string path, DateTime creationTime);

        /// <summary>
        /// Sets the directory's last access time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastAccessTime">A <see cref="DateTime"/> with the directory attribute to set.</param>
        void SetLastAccessTime(string path, DateTime lastAccessTime);

        /// <summary>
        /// Sets the directory's last write time.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="lastWriteTime">A <see cref="DateTime"/> with the directory attribute to set.</param>
        void SetLastWriteTime(string path, DateTime lastWriteTime);
    }
}