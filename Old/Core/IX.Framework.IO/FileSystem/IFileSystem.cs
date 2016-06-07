using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Framework.IO.FileSystem
{
    public interface IFileSystem
    {
        #region Asynchronous methods

        Task<Stream> ReadAsync(string resourcePath);

        Task<Stream> ReadAsync(string resourcePath, CancellationToken cancellationToken);

        Task<Stream> ReadAsync(Uri resourcePath);

        Task<Stream> ReadAsync(Uri resourcePath, CancellationToken cancellationToken);

        Task<Stream> ReadPartialAsync(string resourcePath, int startPosition, int length);

        Task<Stream> ReadPartialAsync(string resourcePath, long startPosition, long length);

        Task<Stream> ReadPartialAsync(string resourcePath, int startPosition, int length, CancellationToken cancellationToken);

        Task<Stream> ReadPartialAsync(string resourcePath, long startPosition, long length, CancellationToken cancellationToken);

        Task<Stream> ReadPartialAsync(Uri resourcePath, int startPosition, int length);

        Task<Stream> ReadPartialAsync(Uri resourcePath, long startPosition, long length);

        Task<Stream> ReadPartialAsync(Uri resourcePath, int startPosition, int length, CancellationToken cancellationToken);

        Task<Stream> ReadPartialAsync(Uri resourcePath, long startPosition, long length, CancellationToken cancellationToken);

        Task WriteAsync(string resourcePath, Stream data);

        Task WriteAsync(string resourcePath, Stream data, CancellationToken cancellationToken);

        Task WriteAsync(Uri resourcePath, Stream data);

        Task WriteAsync(Uri resourcePath, Stream data, CancellationToken cancellationToken);

        Task WritePartialAsync(string resourcePath, Stream data, int startPosition, int length);

        Task WritePartialAsync(string resourcePath, Stream data, long startPosition, long length);

        Task WritePartialAsync(string resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken);

        Task WritePartialAsync(string resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken);

        Task WritePartialAsync(Uri resourcePath, Stream data, int startPosition, int length);

        Task WritePartialAsync(Uri resourcePath, Stream data, long startPosition, long length);

        Task WritePartialAsync(Uri resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken);

        Task WritePartialAsync(Uri resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken);

        Task<FileSystemEntity[]> GetContainedEntitiesAsync(string resourcePath);

        Task<FileSystemEntity[]> GetContainedEntitiesAsync(string resourcePath, CancellationToken cancellationToken);

        Task<FileSystemEntity[]> GetContainedEntitiesAsync(Uri resourcePath);

        Task<FileSystemEntity[]> GetContainedEntitiesAsync(Uri resourcePath, CancellationToken cancellationToken);

        Task DeleteAsync(string resourcePath);

        Task DeleteAsync(string resourcePath, CancellationToken cancellationToken);

        Task DeleteAsync(Uri resourcePath);

        Task DeleteAsync(Uri resourcePath, CancellationToken cancellationToken);

        Task DeleteRecursiveAsync(string resourcePath);

        Task DeleteRecursiveAsync(string resourcePath, CancellationToken cancellationToken);

        Task DeleteRecursiveAsync(Uri resourcePath);

        Task DeleteRecursiveAsync(Uri resourcePath, CancellationToken cancellationToken);

        Task MoveAsync(string resourcePath, string newPath);

        Task MoveAsync(string resourcePath, string newPath, CancellationToken cancellationToken);

        Task MoveAsync(Uri resourcePath, Uri newPath);

        Task MoveAsync(Uri resourcePath, Uri newPath, CancellationToken cancellationToken);

        #endregion Asynchronous methods

        #region Synchronous methods

        Stream Read(string resourcePath);

        Stream Read(string resourcePath, CancellationToken cancellationToken);

        Stream Read(Uri resourcePath);

        Stream Read(Uri resourcePath, CancellationToken cancellationToken);

        Stream ReadPartial(string resourcePath, int startPosition, int length);

        Stream ReadPartial(string resourcePath, long startPosition, long length);

        Stream ReadPartial(string resourcePath, int startPosition, int length, CancellationToken cancellationToken);

        Stream ReadPartial(string resourcePath, long startPosition, long length, CancellationToken cancellationToken);

        Stream ReadPartial(Uri resourcePath, int startPosition, int length);

        Stream ReadPartial(Uri resourcePath, long startPosition, long length);

        Stream ReadPartial(Uri resourcePath, int startPosition, int length, CancellationToken cancellationToken);

        Stream ReadPartial(Uri resourcePath, long startPosition, long length, CancellationToken cancellationToken);

        void Write(string resourcePath, Stream data);

        void Write(string resourcePath, Stream data, CancellationToken cancellationToken);

        void Write(Uri resourcePath, Stream data);

        void Write(Uri resourcePath, Stream data, CancellationToken cancellationToken);

        void WritePartial(string resourcePath, Stream data, int startPosition, int length);

        void WritePartial(string resourcePath, Stream data, long startPosition, long length);

        void WritePartial(string resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken);

        void WritePartial(string resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken);

        void WritePartial(Uri resourcePath, Stream data, int startPosition, int length);

        void WritePartial(Uri resourcePath, Stream data, long startPosition, long length);

        void WritePartial(Uri resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken);

        void WritePartial(Uri resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken);

        FileSystemEntity[] GetContainedEntities(string resourcePath);

        FileSystemEntity[] GetContainedEntities(string resourcePath, CancellationToken cancellationToken);

        FileSystemEntity[] GetContainedEntities(Uri resourcePath);

        FileSystemEntity[] GetContainedEntities(Uri resourcePath, CancellationToken cancellationToken);

        void Delete(string resourcePath);

        void Delete(string resourcePath, CancellationToken cancellationToken);

        void Delete(Uri resourcePath);

        void Delete(Uri resourcePath, CancellationToken cancellationToken);

        void DeleteRecursive(string resourcePath);

        void DeleteRecursive(string resourcePath, CancellationToken cancellationToken);

        void DeleteRecursive(Uri resourcePath);

        void DeleteRecursive(Uri resourcePath, CancellationToken cancellationToken);

        void Move(string resourcePath, string newPath);

        void Move(string resourcePath, string newPath, CancellationToken cancellationToken);

        void Move(Uri resourcePath, Uri newPath);

        void Move(Uri resourcePath, Uri newPath, CancellationToken cancellationToken);

        #endregion Asynchronous methods
    }
}