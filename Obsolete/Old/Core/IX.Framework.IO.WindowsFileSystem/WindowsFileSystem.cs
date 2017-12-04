using IX.Framework.Collections;
using IX.Framework.IO.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Framework.IO.WindowsFileSystem
{
    public class WindowsFileSystem : FileSystem.FileSystem
    {
        public WindowsFileSystem(string rootPath)
            : base(rootPath)
        { }

        public WindowsFileSystem(Uri rootPath)
            : base(rootPath)
        { }

        protected override void DeleteAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resourcePath = absolutePath.LocalPath;
            if (File.Exists(resourcePath))
                File.Delete(resourcePath);
            else if (Directory.Exists(resourcePath))
                Directory.Delete(resourcePath);
            else
                throw new ResourceNotFoundException();
        }

        protected override Task DeleteAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            return Task.Run(() => DeleteAbsolute(absolutePath, cancellationToken), cancellationToken);
        }

        protected override void DeleteRecursiveAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resourcePath = absolutePath.LocalPath;
            if (File.Exists(resourcePath))
                File.Delete(resourcePath);
            else if (Directory.Exists(resourcePath))
                Directory.Delete(resourcePath, true);
            else
                throw new ResourceNotFoundException();
        }

        protected override Task DeleteRecursiveAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            return Task.Run(() => DeleteRecursiveAbsolute(absolutePath, cancellationToken), cancellationToken);
        }

        protected override FileSystemEntity[] GetContainedEntitiesAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resourcePath = absolutePath.LocalPath;
            DirectoryInfo info = new DirectoryInfo(resourcePath);

            List<FileSystemEntity> entities = new List<FileSystemEntity>();
            info.GetDirectories().ForEach(p => entities.Add(new FileSystemEntity
            {
                CreatedDate = p.CreationTimeUtc,
                IsContainer = true,
                ModifiedDate = p.LastWriteTimeUtc,
                ResourceName = p.Name,
                Size = 0,
                ResourcePath = Path.Combine(resourcePath, p.Name)
            }));

            cancellationToken.ThrowIfCancellationRequested();

            info.GetFiles().ForEach(p => entities.Add(new FileSystemEntity
            {
                CreatedDate = p.CreationTimeUtc,
                IsContainer = false,
                ModifiedDate = p.LastWriteTimeUtc,
                ResourceName = p.Name,
                Size = p.Length,
                ResourcePath = Path.Combine(resourcePath, p.Name)
            }));

            return entities.ToArray();
        }

        protected override Task<FileSystemEntity[]> GetContainedEntitiesAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            return Task.Run(() => GetContainedEntitiesAbsolute(absolutePath, cancellationToken), cancellationToken);
        }

        protected override void MoveAbsolute(Uri absolutePath, Uri newAbsolutePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resourcePath = absolutePath.LocalPath;
            string newPath = newAbsolutePath.LocalPath;

            if (File.Exists(resourcePath))
                File.Move(resourcePath, newPath);
            else if (Directory.Exists(resourcePath))
                Directory.Move(resourcePath, newPath);
            else
                throw new ResourceNotFoundException();
        }

        protected override Task MoveAbsoluteAsync(Uri absolutePath, Uri newAbsolutePath, CancellationToken cancellationToken)
        {
            return Task.Run(() => MoveAbsolute(absolutePath, newAbsolutePath, cancellationToken), cancellationToken);
        }

        protected override Stream ReadAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resourcePath = absolutePath.LocalPath;

            if (!File.Exists(resourcePath))
                throw new ResourceNotFoundException();

            return File.OpenRead(resourcePath);
        }

        protected override Task<Stream> ReadAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            return Task.Run(() => ReadAbsolute(absolutePath, cancellationToken), cancellationToken);
        }

        protected override Stream ReadPartialAbsolute(Uri absolutePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resourcePath = absolutePath.LocalPath;

            if (File.Exists(resourcePath))
                throw new ResourceNotFoundException();

            using (FileStream fs = File.OpenRead(resourcePath))
            {
                cancellationToken.ThrowIfCancellationRequested();

                MemoryStream ms = new MemoryStream();
                fs.CopyToPartial(ms, startPosition, length, cancellationToken);

                ms.Seek(0, SeekOrigin.Begin);

                fs.Close();

                return ms;
            }
        }

        protected override async Task<Stream> ReadPartialAbsoluteAsync(Uri absolutePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            string resourcePath = absolutePath.LocalPath;

            if (File.Exists(resourcePath))
                throw new ResourceNotFoundException();

            using (FileStream fs = File.OpenRead(resourcePath))
            {
                MemoryStream ms = new MemoryStream();
                await fs.CopyToPartialAsync(ms, startPosition, length, cancellationToken);

                ms.Seek(0, SeekOrigin.Begin);

                fs.Close();

                return ms;
            }
        }

        protected override void WriteAbsolute(Uri absolutePath, Stream data, CancellationToken cancellationToken)
        {
            string resourcePath = absolutePath.LocalPath;

            cancellationToken.ThrowIfCancellationRequested();

            if (File.Exists(resourcePath))
            {
                FileStream fs = File.OpenWrite(resourcePath);
                data.CopyTo(fs, 4096);
            }
            else
                throw new ResourceNotFoundException();
        }

        protected override async Task WriteAbsoluteAsync(Uri absolutePath, Stream data, CancellationToken cancellationToken)
        {
            string resourcePath = absolutePath.LocalPath;

            if (File.Exists(resourcePath))
            {
                FileStream fs = File.OpenWrite(resourcePath);
                await data.CopyToAsync(fs, 4096, cancellationToken);
            }
            else
                throw new ResourceNotFoundException();
        }

        protected override void WritePartialAbsolute(Uri absolutePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            string resourcePath = absolutePath.LocalPath;

            if (File.Exists(resourcePath))
            {
                FileStream fs = File.OpenWrite(resourcePath);

                fs.Seek(startPosition, SeekOrigin.Begin);
                data.CopyToPartial(fs, 0, data.Length < length ? data.Length : length, cancellationToken);
            }
            else
                throw new ResourceNotFoundException();
        }

        protected override async Task WritePartialAbsoluteAsync(Uri absolutePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            string resourcePath = absolutePath.LocalPath;

            if (File.Exists(resourcePath))
            {
                FileStream fs = File.OpenWrite(resourcePath);

                fs.Seek(startPosition, SeekOrigin.Begin);
                await data.CopyToPartialAsync(fs, 0, data.Length < length ? data.Length : length, cancellationToken);
            }
            else
                throw new ResourceNotFoundException();
        }
    }
}