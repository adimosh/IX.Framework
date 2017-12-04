using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Framework.IO.FileSystem
{
    public abstract class FileSystem : IFileSystem, IDisposable
    {
        #region Constructors

        protected FileSystem(string rootPath)
            : this(new Uri(rootPath))
        {
            if (string.IsNullOrWhiteSpace(rootPath))
                throw new ArgumentNullException(nameof(rootPath));
        }

        protected FileSystem(Uri rootPath)
        {
            if (rootPath == null)
                throw new ArgumentNullException(nameof(rootPath));
            if (!rootPath.IsAbsoluteUri)
                throw new ArgumentException(nameof(rootPath));

            RootPath = rootPath;
        }

        #endregion Constructors

        #region Path

        public Uri RootPath { get; private set; }

        protected Uri FormAbsoluteUri(Uri relativeUri)
        {
            return new Uri(RootPath, relativeUri);
        }

        #endregion Path

        #region Asynchronous methods

        public Task DeleteAsync(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            return DeleteAbsoluteAsync(FormAbsoluteUri(resourcePath), cancellationToken);
        }

        public Task DeleteAsync(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return DeleteAsync(new Uri(resourcePath), cancellationToken);
        }

        public Task DeleteAsync(string resourcePath)
        {
            return DeleteAsync(resourcePath, default(CancellationToken));
        }

        public Task DeleteAsync(Uri resourcePath)
        {
            return DeleteAsync(resourcePath, default(CancellationToken));
        }

        protected abstract Task DeleteAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken);

        public Task DeleteRecursiveAsync(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            return DeleteRecursiveAbsoluteAsync(FormAbsoluteUri(resourcePath), cancellationToken);
        }

        public Task DeleteRecursiveAsync(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return DeleteRecursiveAsync(new Uri(resourcePath), cancellationToken);
        }

        public Task DeleteRecursiveAsync(string resourcePath)
        {
            return DeleteRecursiveAsync(resourcePath, default(CancellationToken));
        }

        public Task DeleteRecursiveAsync(Uri resourcePath)
        {
            return DeleteRecursiveAsync(resourcePath, default(CancellationToken));
        }

        protected abstract Task DeleteRecursiveAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken);

        public Task<FileSystemEntity[]> GetContainedEntitiesAsync(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            return GetContainedEntitiesAbsoluteAsync(FormAbsoluteUri(resourcePath), cancellationToken);
        }

        public Task<FileSystemEntity[]> GetContainedEntitiesAsync(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return GetContainedEntitiesAsync(new Uri(resourcePath), cancellationToken);
        }

        public Task<FileSystemEntity[]> GetContainedEntitiesAsync(string resourcePath)
        {
            return GetContainedEntitiesAsync(resourcePath, default(CancellationToken));
        }

        public Task<FileSystemEntity[]> GetContainedEntitiesAsync(Uri resourcePath)
        {
            return GetContainedEntitiesAsync(resourcePath, default(CancellationToken));
        }

        protected abstract Task<FileSystemEntity[]> GetContainedEntitiesAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken);

        public Task MoveAsync(Uri resourcePath, Uri newPath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            if (newPath == null)
                throw new ArgumentNullException(nameof(newPath));
            if (newPath.IsAbsoluteUri)
                throw new ArgumentException(nameof(newPath));

            return MoveAbsoluteAsync(FormAbsoluteUri(resourcePath), FormAbsoluteUri(newPath), cancellationToken);
        }

        public Task MoveAsync(string resourcePath, string newPath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));
            if (string.IsNullOrWhiteSpace(newPath))
                throw new ArgumentNullException(nameof(newPath));

            return MoveAsync(new Uri(resourcePath), new Uri(newPath), cancellationToken);
        }

        public Task MoveAsync(string resourcePath, string newPath)
        {
            return MoveAsync(resourcePath, newPath, default(CancellationToken));
        }

        public Task MoveAsync(Uri resourcePath, Uri newPath)
        {
            return MoveAsync(resourcePath, newPath, default(CancellationToken));
        }

        protected abstract Task MoveAbsoluteAsync(Uri absolutePath, Uri newAbsolutePath, CancellationToken cancellationToken);

        public Task<Stream> ReadAsync(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            return ReadAbsoluteAsync(FormAbsoluteUri(resourcePath), cancellationToken);
        }

        public Task<Stream> ReadAsync(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return ReadAsync(new Uri(resourcePath), cancellationToken);
        }

        public Task<Stream> ReadAsync(string resourcePath)
        {
            return ReadAsync(resourcePath, default(CancellationToken));
        }

        public Task<Stream> ReadAsync(Uri resourcePath)
        {
            return ReadAsync(resourcePath, default(CancellationToken));
        }

        protected abstract Task<Stream> ReadAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken);

        public Task<Stream> ReadPartialAsync(Uri resourcePath, int startPosition, int length, CancellationToken cancellationToken)
        {
            return ReadPartialAsync(resourcePath, startPosition, length, cancellationToken);
        }

        public Task<Stream> ReadPartialAsync(Uri resourcePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));
            if (startPosition < 0)
                throw new ArgumentException(nameof(startPosition));
            if (length <= 0)
                throw new ArgumentException(nameof(length));

            return ReadPartialAbsoluteAsync(FormAbsoluteUri(resourcePath), startPosition, length, cancellationToken);
        }

        public Task<Stream> ReadPartialAsync(string resourcePath, int startPosition, int length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return ReadPartialAsync(new Uri(resourcePath), startPosition, length, cancellationToken);
        }

        public Task<Stream> ReadPartialAsync(string resourcePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return ReadPartialAsync(new Uri(resourcePath), startPosition, length, cancellationToken);
        }

        public Task<Stream> ReadPartialAsync(string resourcePath, int startPosition, int length)
        {
            return ReadPartialAsync(resourcePath, startPosition, length, default(CancellationToken));
        }

        public Task<Stream> ReadPartialAsync(string resourcePath, long startPosition, long length)
        {
            return ReadPartialAsync(resourcePath, startPosition, length, default(CancellationToken));
        }

        public Task<Stream> ReadPartialAsync(Uri resourcePath, int startPosition, int length)
        {
            return ReadPartialAsync(resourcePath, startPosition, length, default(CancellationToken));
        }

        public Task<Stream> ReadPartialAsync(Uri resourcePath, long startPosition, long length)
        {
            return ReadPartialAsync(resourcePath, startPosition, length, default(CancellationToken));
        }

        protected abstract Task<Stream> ReadPartialAbsoluteAsync(Uri absolutePath, long startPosition, long length, CancellationToken cancellationToken);

        public Task WriteAsync(Uri resourcePath, Stream data, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length == 0)
                throw new ArgumentException(nameof(data));

            return WriteAbsoluteAsync(FormAbsoluteUri(resourcePath), data, cancellationToken);
        }

        public Task WriteAsync(string resourcePath, Stream data, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return WriteAsync(new Uri(resourcePath), data, cancellationToken);
        }

        public Task WriteAsync(string resourcePath, Stream data)
        {
            return WriteAsync(resourcePath, data, default(CancellationToken));
        }

        public Task WriteAsync(Uri resourcePath, Stream data)
        {
            return WriteAsync(resourcePath, data, default(CancellationToken));
        }

        protected abstract Task WriteAbsoluteAsync(Uri absolutePath, Stream data, CancellationToken cancellationToken);

        public Task WritePartialAsync(string resourcePath, Stream data, int startPosition, int length)
        {
            return WritePartialAsync(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public Task WritePartialAsync(string resourcePath, Stream data, long startPosition, long length)
        {
            return WritePartialAsync(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public Task WritePartialAsync(Uri resourcePath, Stream data, int startPosition, int length)
        {
            return WritePartialAsync(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public Task WritePartialAsync(Uri resourcePath, Stream data, long startPosition, long length)
        {
            return WritePartialAsync(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public Task WritePartialAsync(string resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return WritePartialAsync(new Uri(resourcePath), data, startPosition, length, cancellationToken);
        }

        public Task WritePartialAsync(Uri resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken)
        {
            return WritePartialAsync(resourcePath, data, (long)startPosition, length, cancellationToken);
        }

        public Task WritePartialAsync(Uri resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length == 0)
                throw new ArgumentException(nameof(data));
            if (startPosition < 0)
                throw new ArgumentException(nameof(startPosition));
            if (length <= 0 || length > data.Length)
                throw new ArgumentException(nameof(length));

            return WritePartialAbsoluteAsync(FormAbsoluteUri(resourcePath), data, startPosition, length, default(CancellationToken));
        }

        public Task WritePartialAsync(string resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return WritePartialAsync(new Uri(resourcePath), data, startPosition, length, cancellationToken);
        }

        protected abstract Task WritePartialAbsoluteAsync(Uri absolutePath, Stream data, long startPosition, long length, CancellationToken cancellationToken);

        #endregion Asynchronous methods

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        ~FileSystem()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support

        #region Synchronous methods

        public Stream Read(string resourcePath)
        {
            return Read(resourcePath, default(CancellationToken));
        }

        public Stream Read(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return Read(new Uri(resourcePath), cancellationToken);
        }

        public Stream Read(Uri resourcePath)
        {
            return Read(resourcePath, default(CancellationToken));
        }

        public Stream Read(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            return ReadAbsolute(resourcePath, cancellationToken);
        }

        protected abstract Stream ReadAbsolute(Uri absolutePath, CancellationToken cancellationToken);

        public Stream ReadPartial(string resourcePath, int startPosition, int length)
        {
            return ReadPartial(resourcePath, startPosition, length, default(CancellationToken));
        }

        public Stream ReadPartial(string resourcePath, long startPosition, long length)
        {
            return ReadPartial(resourcePath, startPosition, length, default(CancellationToken));
        }

        public Stream ReadPartial(string resourcePath, int startPosition, int length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return ReadPartial(new Uri(resourcePath), startPosition, length, cancellationToken);
        }

        public Stream ReadPartial(string resourcePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return ReadPartial(new Uri(resourcePath), startPosition, length, cancellationToken);
        }

        public Stream ReadPartial(Uri resourcePath, int startPosition, int length)
        {
            return ReadPartial(resourcePath, startPosition, length, default(CancellationToken));
        }

        public Stream ReadPartial(Uri resourcePath, long startPosition, long length)
        {
            return ReadPartial(resourcePath, startPosition, length, default(CancellationToken));
        }

        public Stream ReadPartial(Uri resourcePath, int startPosition, int length, CancellationToken cancellationToken)
        {
            return ReadPartial(resourcePath, startPosition, (long)length, default(CancellationToken));
        }

        public Stream ReadPartial(Uri resourcePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));
            if (startPosition < 0)
                throw new ArgumentException(nameof(startPosition));
            if (length <= 0)
                throw new ArgumentException(nameof(length));

            return ReadPartialAbsolute(resourcePath, startPosition, length, cancellationToken);
        }

        protected abstract Stream ReadPartialAbsolute(Uri absolutePath, long startPosition, long length, CancellationToken cancellationToken);

        public void Write(string resourcePath, Stream data)
        {
            Write(resourcePath, data, default(CancellationToken));
        }

        public void Write(string resourcePath, Stream data, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            Write(new Uri(resourcePath), data, default(CancellationToken));
        }

        public void Write(Uri resourcePath, Stream data)
        {
            Write(resourcePath, data, default(CancellationToken));
        }

        public void Write(Uri resourcePath, Stream data, CancellationToken cancellationToken)
        {
            WriteAbsolute(resourcePath, data, cancellationToken);
        }

        protected abstract void WriteAbsolute(Uri absolutePath, Stream data, CancellationToken cancellationToken);

        public void WritePartial(string resourcePath, Stream data, int startPosition, int length)
        {
            WritePartial(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public void WritePartial(string resourcePath, Stream data, long startPosition, long length)
        {
            WritePartial(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public void WritePartial(string resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            WritePartial(new Uri(resourcePath), data, startPosition, length, cancellationToken);
        }

        public void WritePartial(string resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            WritePartial(new Uri(resourcePath), data, startPosition, length, cancellationToken);
        }

        public void WritePartial(Uri resourcePath, Stream data, int startPosition, int length)
        {
            WritePartial(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public void WritePartial(Uri resourcePath, Stream data, long startPosition, long length)
        {
            WritePartial(resourcePath, data, startPosition, length, default(CancellationToken));
        }

        public void WritePartial(Uri resourcePath, Stream data, int startPosition, int length, CancellationToken cancellationToken)
        {
            WritePartial(resourcePath, data, startPosition, (long)length, cancellationToken);
        }

        public void WritePartial(Uri resourcePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length == 0)
                throw new ArgumentException(nameof(data));
            if (startPosition < 0)
                throw new ArgumentException(nameof(startPosition));
            if (length <= 0 || length > data.Length)
                throw new ArgumentException(nameof(length));

            WritePartialAbsolute(resourcePath, data, startPosition, length, cancellationToken);
        }

        protected abstract void WritePartialAbsolute(Uri absolutePath, Stream data, long startPosition, long length, CancellationToken cancellationToken);

        public FileSystemEntity[] GetContainedEntities(string resourcePath)
        {
            return GetContainedEntities(resourcePath, default(CancellationToken));
        }

        public FileSystemEntity[] GetContainedEntities(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            return GetContainedEntities(new Uri(resourcePath), cancellationToken);
        }

        public FileSystemEntity[] GetContainedEntities(Uri resourcePath)
        {
            return GetContainedEntities(resourcePath, default(CancellationToken));
        }

        public FileSystemEntity[] GetContainedEntities(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            return GetContainedEntitiesAbsolute(resourcePath, cancellationToken);
        }

        protected abstract FileSystemEntity[] GetContainedEntitiesAbsolute(Uri absolutePath, CancellationToken cancellationToken);

        public void Delete(string resourcePath)
        {
            Delete(resourcePath, default(CancellationToken));
        }

        public void Delete(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            Delete(new Uri(resourcePath), cancellationToken);
        }

        public void Delete(Uri resourcePath)
        {
            Delete(resourcePath, default(CancellationToken));
        }

        public void Delete(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            DeleteAbsolute(resourcePath, cancellationToken);
        }

        protected abstract void DeleteAbsolute(Uri absolutePath, CancellationToken cancellationToken);

        public void DeleteRecursive(string resourcePath)
        {
            DeleteRecursive(resourcePath, default(CancellationToken));
        }

        public void DeleteRecursive(string resourcePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));

            DeleteRecursive(new Uri(resourcePath), cancellationToken);
        }

        public void DeleteRecursive(Uri resourcePath)
        {
            DeleteRecursive(resourcePath, default(CancellationToken));
        }

        public void DeleteRecursive(Uri resourcePath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));

            DeleteRecursiveAbsolute(resourcePath, cancellationToken);
        }

        protected abstract void DeleteRecursiveAbsolute(Uri absolutePath, CancellationToken cancellationToken);

        public void Move(string resourcePath, string newPath)
        {
            Move(resourcePath, newPath, default(CancellationToken));
        }

        public void Move(string resourcePath, string newPath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentNullException(nameof(resourcePath));
            if (string.IsNullOrWhiteSpace(newPath))
                throw new ArgumentNullException(nameof(newPath));

            Move(new Uri(resourcePath), new Uri(newPath), cancellationToken);
        }

        public void Move(Uri resourcePath, Uri newPath)
        {
            Move(resourcePath, newPath, default(CancellationToken));
        }

        public void Move(Uri resourcePath, Uri newPath, CancellationToken cancellationToken)
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));
            if (resourcePath.IsAbsoluteUri)
                throw new ArgumentException(nameof(resourcePath));
            if (newPath == null)
                throw new ArgumentNullException(nameof(newPath));
            if (newPath.IsAbsoluteUri)
                throw new ArgumentException(nameof(newPath));

            MoveAbsolute(resourcePath, newPath, cancellationToken);
        }

        protected abstract void MoveAbsolute(Uri absolutePath, Uri newAbsolutePath, CancellationToken cancellationToken);

        #endregion Synchronous methods
    }
}