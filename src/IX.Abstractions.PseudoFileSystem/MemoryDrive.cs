using System;
using System.Collections.Generic;
using System.Text;
using IX.System.IO;

namespace IX.Abstractions.PseudoFileSystem
{
    /// <summary>
    /// A virtual drive that exists in the memory of the containing process.
    /// </summary>
    /// <seealso cref="IX.Abstractions.PseudoFileSystem.IFileSystemContainer" />
    /// <seealso cref="IX.System.IO.IDirectory" />
    public sealed class MemoryDrive : FileSystemContainerBase, IDirectory
    {
        private FileSystemContainerBase currentFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryDrive"/> class.
        /// </summary>
        /// <param name="label">The drive label.</param>
        public MemoryDrive(string label)
            : this(label, "/")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryDrive"/> class.
        /// </summary>
        /// <param name="label">The drive label.</param>
        /// <param name="pathSeparator">The path separator.</param>
        public MemoryDrive(string label, string pathSeparator)
            : base(pathSeparator)
        {
            // Initialize fields
            this.Label = label;
        }

        /// <summary>
        /// Gets the label of the current drive.
        /// </summary>
        /// <value>
        /// The drive label.
        /// </value>
        public string Label { get; private set; }

        /// <summary>
        /// Gets the parent of the current drive, which is itself.
        /// </summary>
        /// <value>
        /// The current drive.
        /// </value>
        /// <remarks>
        /// <para>Please note that the parent of a drive will always be itself. This property will never return <c>null</c> (<c>Nothing</c> in Visual Basic).</para>
        /// </remarks>
        public override IFileSystemContainer Parent => this;

        /// <summary>
        /// Gets the current folder.
        /// </summary>
        /// <value>
        /// The current folder.
        /// </value>
        public IFileSystemContainer CurrentFolder => this.currentFolder;

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        public void CreateDirectory(string path)
        {
            this.currentFolder = this.Navigate(path) as FileSystemContainerBase;
        }

        public void Delete(string path, bool recursive = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enumerates all the directories contained at a certain directory.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <returns>
        /// An enumerable of directory paths.
        /// </returns>
        public IEnumerable<string> EnumerateDirectories(string path)
        {
            CreateDirectory(path);

            return currentFolder.Folders.Keys;
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateDirectoriesRecursively(string path) => throw new NotImplementedException();
        public IEnumerable<string> EnumerateDirectoriesRecursively(string path, string searchPattern) => throw new NotImplementedException();

        public IEnumerable<string> EnumerateFiles(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateFilesRecursively(string path) => throw new NotImplementedException();
        public IEnumerable<string> EnumerateFilesRecursively(string path, string searchPattern) => throw new NotImplementedException();

        public IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path) => throw new NotImplementedException();
        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path, string searchPattern) => throw new NotImplementedException();

        public bool Exists(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCreationTime(string path)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentDirectory() => throw new NotImplementedException();

        public string[] GetDirectories(string path)
        {
            throw new NotImplementedException();
        }

        public string[] GetDirectories(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public string[] GetFiles(string path)
        {
            throw new NotImplementedException();
        }

        public string[] GetFiles(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public string[] GetFileSystemEntries(string path)
        {
            throw new NotImplementedException();
        }

        public string[] GetFileSystemEntries(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastAccessTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastWriteTime(string path)
        {
            throw new NotImplementedException();
        }

        public void Move(string sourceDirName, string destDirName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Navigates to the specified path.
        /// </summary>
        /// <param name="path">The path to navigate to.</param>
        /// <returns>
        /// An <see cref="IFileSystemContainer" /> representing the specified path, or <c>null</c> (<c>Nothing</c> in Visual Basic) if the path was not found.
        /// </returns>
        public override IFileSystemContainer Navigate(string path)
        {
            var pathComponents = SplitPath(path);

            if (pathComponents.root)
            {
                currentFolder = this;
            }

            var firstPath = pathComponents.components[0];
            if (!Folders.TryGetValue(firstPath, out var folder))
            {
                folder = new Folder(PathSeparator);
            }
            currentFolder = folder;

            for (int i = 1; i < pathComponents.components.Length; i++)
            {
                currentFolder = currentFolder.Navigate(pathComponents.components[i]) as FileSystemContainerBase;
            }

            return currentFolder;
        }

        public void SetCreationTime(string path, DateTime creationTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            throw new NotImplementedException();
        }

        private (string[] components, bool root) SplitPath(string path)
        {
            var pathComponents = path.Split(new[] { PathSeparator }, StringSplitOptions.None);

            if (pathComponents.Length == 1)
            {
                return (pathComponents, false);
            }
            else if (string.IsNullOrEmpty(pathComponents[0]))
            {
                // We have a path stated like /something/, which points to a root path

                if (string.IsNullOrWhiteSpace(pathComponents[1]))
                {
                    throw new ArgumentException("A path should not contain empty or whitespace-only parts.", nameof(path));
                }

                string[] newComponents = new string[pathComponents.Length - 1];
                Array.Copy(pathComponents, 1, newComponents, 0, newComponents.Length);

                return (newComponents, true);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(pathComponents[0]))
                {
                    throw new ArgumentException("A path should not contain empty or whitespace-only parts.", nameof(path));
                }

                return (pathComponents, true);
            }
        }

        private void SwitchFolder(string path)
        {
        }
    }
}