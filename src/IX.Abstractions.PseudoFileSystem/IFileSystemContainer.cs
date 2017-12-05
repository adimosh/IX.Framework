namespace IX.Abstractions.PseudoFileSystem
{
    /// <summary>
    /// Represents a file system container, which could be a folder or the root of the file system.
    /// </summary>
    public interface IFileSystemContainer
    {
        /// <summary>
        /// Gets the parent of the current container.
        /// </summary>
        /// <value>
        /// The parent of the current container.
        /// </value>
        IFileSystemContainer Parent { get; }

        /// <summary>
        /// Navigates to the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// An <see cref="IFileSystemContainer"/> representing the specified path, or <c>null</c> (<c>Nothing</c> in Visual Basic) if the path was not found.
        /// </returns>
        IFileSystemContainer Navigate(string path);
    }
}