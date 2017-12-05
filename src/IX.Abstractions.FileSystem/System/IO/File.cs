// <copyright file="File.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FSFile = System.IO.File;

namespace IX.System.IO
{
    /// <summary>
    /// A class for implementing <see cref="IFile" /> with <see cref="System.IO.File" />.
    /// </summary>
    /// <seealso cref="IX.System.IO.IFile" />
    /// <seealso cref="System.IO.File" />
    public class File : IFile
    {
        /// <summary>
        /// Appends lines of text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="contents"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <remarks>
        /// This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to <c>null</c>, an implementation-specific
        /// encoding will be used.
        /// </remarks>
        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (contents == null)
            {
                throw new ArgumentNullException(nameof(contents));
            }

            if (encoding == null)
            {
                FSFile.AppendAllLines(path, contents);
            }
            else
            {
                FSFile.AppendAllLines(path, contents, encoding);
            }
        }

        /// <summary>
        /// Appends text to a specified file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="contents"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <remarks>
        /// This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to <c>null</c>, an implementation-specific
        /// encoding will be used.
        /// </remarks>
        public void AppendAllText(string path, string contents, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(contents))
            {
                throw new ArgumentNullException(nameof(contents));
            }

            if (encoding == null)
            {
                FSFile.AppendAllText(path, contents);
            }
            else
            {
                FSFile.AppendAllText(path, contents, encoding);
            }
        }

        /// <summary>
        /// Opens a <see cref="T:System.IO.StreamWriter" /> to append text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamWriter" /> that can write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public StreamWriter AppendText(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.AppendText(path);
        }

        /// <summary>
        /// Copies a file to another.
        /// </summary>
        /// <param name="sourceFileName">The source file.</param>
        /// <param name="destFileName">The destination file.</param>
        /// <param name="overwrite">If <c>true</c>, overwrites the destination file, if one exists, otherwise throws an exception. If a destination file doesn't
        /// exist, this parameter is ignored.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void Copy(string sourceFileName, string destFileName, bool overwrite = false)
        {
            if (string.IsNullOrWhiteSpace(sourceFileName))
            {
                throw new ArgumentNullException(nameof(sourceFileName));
            }

            if (string.IsNullOrWhiteSpace(destFileName))
            {
                throw new ArgumentNullException(nameof(destFileName));
            }

            FSFile.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="bufferSize">The buffer size to use. Default is 4 kilobytes.</param>
        /// <returns>
        /// A <see cref="T:System.IO.Stream" /> that can read from and write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="bufferSize"/> is less than or equal to 0
        /// </exception>
        public Stream Create(string path, int bufferSize = 4096)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }

            return FSFile.Create(path, bufferSize);
        }

        /// <summary>
        /// Opens a <see cref="T:System.IO.StreamWriter" /> to write text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamWriter" /> that can write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public StreamWriter CreateText(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.CreateText(path);
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void Delete(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSFile.Delete(path);
        }

        /// <summary>
        /// Checks whether a file exists and is accessible.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// Returns <c>true</c> if the specified file exists and is accessible, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public bool Exists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.Exists(path);
        }

        /// <summary>
        /// Gets a specific file's creation time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public DateTime GetCreationTime(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.GetCreationTimeUtc(path);
        }

        /// <summary>
        /// Gets a specific file's last access time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public DateTime GetLastAccessTime(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.GetLastAccessTimeUtc(path);
        }

        /// <summary>
        /// Gets a specific file's last write time, in UTC.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> in UTC.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public DateTime GetLastWriteTime(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        /// Move a file.
        /// </summary>
        /// <param name="sourceFileName">The source file name.</param>
        /// <param name="destFileName">The destination file name.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void Move(string sourceFileName, string destFileName)
        {
            if (string.IsNullOrWhiteSpace(sourceFileName))
            {
                throw new ArgumentNullException(nameof(sourceFileName));
            }

            if (string.IsNullOrWhiteSpace(destFileName))
            {
                throw new ArgumentNullException(nameof(destFileName));
            }

            FSFile.Move(sourceFileName, destFileName);
        }

        /// <summary>
        /// Opens a <see cref="T:System.IO.Stream" /> to read from a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.IO.Stream" /> that can read from a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public Stream OpenRead(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.OpenRead(path);
        }

        /// <summary>
        /// Opens a <see cref="T:System.IO.StreamReader" /> to read text from a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamReader" /> that can read from a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public StreamReader OpenText(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.OpenText(path);
        }

        /// <summary>
        /// Opens a <see cref="T:System.IO.Stream" /> to write to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// A <see cref="T:System.IO.Stream" /> that can write to a file.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public Stream OpenWrite(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.OpenWrite(path);
        }

        /// <summary>
        /// Reads the entire contents of a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// The contents of a file, in binary.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public byte[] ReadAllBytes(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return FSFile.ReadAllBytes(path);
        }

        /// <summary>
        /// Reads the entire contents of a file and splits them by end-of-line markers.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>
        /// An array of <see cref="T:System.String" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <remarks>
        /// This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to <c>null</c>, an implementation-specific
        /// encoding will be used.
        /// </remarks>
        public string[] ReadAllLines(string path, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (encoding == null)
            {
                return FSFile.ReadAllLines(path);
            }
            else
            {
                return FSFile.ReadAllLines(path, encoding);
            }
        }

        /// <summary>
        /// Reads the entire contents of a file as text.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>
        /// The entire file contents as a string.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <remarks>
        /// This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to <c>null</c>, an implementation-specific
        /// encoding will be used.
        /// </remarks>
        public string ReadAllText(string path, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (encoding == null)
            {
                return FSFile.ReadAllText(path);
            }
            else
            {
                return FSFile.ReadAllText(path, encoding);
            }
        }

        /// <summary>
        /// Reads file contents as text line by line.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>
        /// An enumerable of strings, each representing one line of text.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <remarks>
        /// This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to <c>null</c>, an implementation-specific
        /// encoding will be used.
        /// </remarks>
        public IEnumerable<string> ReadLines(string path, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (encoding == null)
            {
                return FSFile.ReadLines(path);
            }
            else
            {
                return FSFile.ReadLines(path, encoding);
            }
        }

        /// <summary>
        /// Sets the file's creation time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="creationTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void SetCreationTime(string path, DateTime creationTime)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSFile.SetCreationTimeUtc(path, creationTime);
        }

        /// <summary>
        /// Sets the file's last access time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSFile.SetLastAccessTimeUtc(path, lastAccessTime);
        }

        /// <summary>
        /// Sets the file's last write time.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> with the file attribute to set.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            FSFile.SetLastWriteTimeUtc(path, lastWriteTime);
        }

        /// <summary>
        /// Writes binary contents to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="bytes">The contents to write.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="bytes"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        public void WriteAllBytes(string path, byte[] bytes)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            FSFile.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Writes lines of text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="contents"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <remarks>
        /// This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to <c>null</c>, an implementation-specific
        /// encoding will be used.
        /// </remarks>
        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (contents == null)
            {
                throw new ArgumentNullException(nameof(contents));
            }

            if (encoding == null)
            {
                FSFile.WriteAllLines(path, contents);
            }
            else
            {
                FSFile.WriteAllLines(path, contents, encoding);
            }
        }

        /// <summary>
        /// Writes text to a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path"/> or <paramref name="contents"/> is <c>null</c> (<c>Nothing</c> in Visual Basic)
        /// </exception>
        /// <remarks>
        /// This operation always requires an encoding to be used. If <paramref name="encoding" /> is set to <c>null</c>, an implementation-specific
        /// encoding will be used.
        /// </remarks>
        public void WriteAllText(string path, string contents, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(contents))
            {
                throw new ArgumentNullException(nameof(contents));
            }

            if (encoding == null)
            {
                FSFile.WriteAllText(path, contents);
            }
            else
            {
                FSFile.WriteAllText(path, contents, encoding);
            }
        }
    }
}