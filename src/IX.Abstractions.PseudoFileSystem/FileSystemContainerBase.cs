// <copyright file="FileSystemContainerBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;

namespace IX.Abstractions.PseudoFileSystem
{
    /// <summary>
    /// A base class for containers.
    /// </summary>
    /// <seealso cref="IX.Abstractions.PseudoFileSystem.IFileSystemContainer" />
    public abstract class FileSystemContainerBase : IFileSystemContainer
    {
        /// <summary>
        /// The sub-folders contained.
        /// </summary>
        private readonly Dictionary<string, FileSystemContainerBase> folders;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemContainerBase"/> class.
        /// </summary>
        /// <param name="pathSeparator">The path separator.</param>
        protected FileSystemContainerBase(string pathSeparator)
        {
            // Collections
            this.folders = new Dictionary<string, FileSystemContainerBase>();

            // Fields
            this.PathSeparator = pathSeparator;
        }

        /// <summary>
        /// Gets the path separator.
        /// </summary>
        /// <value>
        /// The path separator.
        /// </value>
        public string PathSeparator { get; private set; }

        /// <summary>
        /// Gets the parent of the current container.
        /// </summary>
        /// <value>
        /// The parent of the current container.
        /// </value>
        public abstract IFileSystemContainer Parent { get; }

        /// <summary>
        /// Gets the folders contained in this container.
        /// </summary>
        /// <value>
        /// The folders.
        /// </value>
        public Dictionary<string, FileSystemContainerBase> Folders => this.folders;

        /// <summary>
        /// Navigates to the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// An <see cref="IFileSystemContainer" /> representing the specified path, or <c>null</c> (<c>Nothing</c> in Visual Basic) if the path was not found.
        /// </returns>
        public abstract IFileSystemContainer Navigate(string path);
    }
}