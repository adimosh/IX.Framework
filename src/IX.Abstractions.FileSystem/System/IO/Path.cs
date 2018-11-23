// <copyright file="Path.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using FSPath = System.IO.Path;

namespace IX.System.IO
{
    /// <summary>
    /// A class for implementing <see cref="IX.System.IO.IPath" /> with <see cref="System.IO.Path" />.
    /// </summary>
    /// <seealso cref="IX.System.IO.IPath" />
    /// <seealso cref="System.IO.Path" />
    public class Path : IPath
    {
        /// <summary>
        /// Gets a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.
        /// </summary>
        public char DirectorySeparatorChar => FSPath.DirectorySeparatorChar;

        /// <summary>
        /// Gets a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.
        /// </summary>
        public char AltDirectorySeparatorChar => FSPath.AltDirectorySeparatorChar;

        /// <summary>
        /// Gets a platform-specific volume separator character.
        /// </summary>
        public char VolumeSeparatorChar => FSPath.VolumeSeparatorChar;

        /// <summary>
        /// Gets a platform-specific separator character used to separate path strings in environment variables.
        /// </summary>
        public char PathSeparator => FSPath.PathSeparator;

        /// <summary>
        /// Changes the extension of a path string.
        /// </summary>
        /// <param name="path">The path information to modify. The path cannot contain any of the characters defined in <see cref="GetInvalidPathChars"/>.</param>
        /// <param name="extension">The new extension (with or without a leading period). Specify <see langword="null"/> (<see langword="Nothing"/> in Visual Basic) to remove an existing extension from path.</param>
        /// <returns>The modified path information.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="path"/> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars"/>.</exception>
        /// <remarks>
        /// <para>On Windows-based desktop platforms, if path is <see langword="null"/> or an empty string (""), the path information is returned unmodified.</para>
        /// <para> If extension is <see langword="null"/>, the returned string contains the specified path with its extension removed. If path has no extension, and extension is not <see langword="null"/>, the returned path string contains extension appended to the end of path.</para>
        /// </remarks>
        public string ChangeExtension(string path, string extension) => FSPath.ChangeExtension(path, extension);

        /// <summary>
        /// Combines an array of strings into a path.
        /// </summary>
        /// <param name="paths">An array of parts of the path.</param>
        /// <returns>The combined paths.</returns>
        /// <exception cref="ArgumentException">One of the strings in the array contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars"/>.</exception>
        /// <exception cref="ArgumentNullException">One of the strings in the array is <see langword="null"/>.</exception>
        public string Combine(params string[] paths) => FSPath.Combine(paths);

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>Directory information for path, or <see langword="null"/> if path denotes a root directory or is <see langword="null"/>. Returns <see cref="string.Empty"/> if path does not contain directory information.</returns>
        /// <exception cref="ArgumentException">The <paramref name="path"/> contains invalid characters, is empty, or contains only white spaces.</exception>
        /// <exception cref="global::System.IO.PathTooLongException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="global::System.IO.IOException" />, instead.The path parameter is longer than the system-defined maximum length.</exception>
        public string GetDirectoryName(string path) => FSPath.GetDirectoryName(path);

        /// <summary>
        /// Returns the extension of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to get the extension.</param>
        /// <returns>The extension of the specified path (including the period &quot;.&quot;), or <see langword="null"/>, or <see cref="string.Empty"/>. If path is <see langword="null"/>, the method returns <see langword="null"/>. If path does not have extension information, the method returns <see cref="string.Empty"/>.</returns>
        public string GetExtension(string path) => FSPath.GetExtension(path);

        /// <summary>
        /// Returns the file name and extension of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to obtain the file name and extension.</param>
        /// <returns>The characters after the last directory character in path. If the last character of path is a directory or volume separator character, this method returns <see cref="string.Empty"/>. If path is <see langword="null"/>, this method returns <see langword="null"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        public string GetFileName(string path) => FSPath.GetFileName(path);

        /// <summary>
        /// Returns the file name of the specified path string without the extension.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>The string returned by <see cref="GetFileName(string)"/> , minus the last period (.) and all characters following it.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        public string GetFileNameWithoutExtension(string path) => FSPath.GetFileNameWithoutExtension(path);

        /// <summary>
        /// Returns the absolute path for the specified path string.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain absolute path information.</param>
        /// <returns>The fully qualified location of path, such as &quot;C:\MyFile.txt&quot;.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />. -or- The system could not retrieve the absolute path.</exception>
        /// <exception cref="global::System.Security.SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
        /// <exception cref="NotSupportedException"><paramref name="path"/> contains a colon (&quot;:&quot;) that is not part of a volume identifier (for example, &quot;c:\&quot;).</exception>
        /// <exception cref="global::System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        public string GetFullPath(string path) => FSPath.GetFullPath(path);

        /// <summary>
        /// Gets an array containing the characters that are not allowed in file names.
        /// </summary>
        /// <returns>An array containing the characters that are not allowed in file names.</returns>
        public char[] GetInvalidFileNameChars() => FSPath.GetInvalidFileNameChars();

        /// <summary>
        /// Gets an array containing the characters that are not allowed in path names.
        /// </summary>
        /// <returns>An array containing the characters that are not allowed in path names.</returns>
        public char[] GetInvalidPathChars() => FSPath.GetInvalidPathChars();

        /// <summary>
        /// Gets the root directory information of the specified path.
        /// </summary>
        /// <param name="path">The path from which to obtain root directory information.</param>
        /// <returns>The root directory of path, such as &quot;C:\&quot;, or <see langword="null"/> if <paramref name="path"/> is <see langword="null"/>, or an empty string if <paramref name="path"/> does not contain root directory information.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />. -or- <see cref="string.Empty"/> was passed to <paramref name="path"/>.</exception>
        public string GetPathRoot(string path) => FSPath.GetPathRoot(path);

        /// <summary>
        /// Returns a random folder name or file name.
        /// </summary>
        /// <returns>A random folder name or file name.</returns>
        public string GetRandomFileName() => FSPath.GetRandomFileName();

        /// <summary>
        /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
        /// </summary>
        /// <returns>The full path of the temporary file.</returns>
        /// <exception cref="global::System.IO.IOException">An I/O error occurs, such as no unique temporary file name is available. -or- This method was unable to create a temporary file.</exception>
        public string GetTempFileName() => FSPath.GetTempFileName();

        /// <summary>
        /// Returns the path of the current user's temporary folder.
        /// </summary>
        /// <returns>The path to the temporary folder, ending with a backslash.</returns>
        /// <exception cref="global::System.Security.SecurityException">The caller does not have the required permissions.</exception>
        public string GetTempPath() => FSPath.GetTempPath();

        /// <summary>
        /// Determines whether a path includes a file name extension.
        /// </summary>
        /// <param name="path">The path to search for an extension.</param>
        /// <returns><see langword="true"/> if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        public bool HasExtension(string path) => FSPath.HasExtension(path);

        /// <summary>
        /// Gets a value indicating whether the specified path string contains a root.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns><see langword="true"/> if path contains a root; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="path"/> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        public bool IsPathRooted(string path) => FSPath.IsPathRooted(path);
    }
}