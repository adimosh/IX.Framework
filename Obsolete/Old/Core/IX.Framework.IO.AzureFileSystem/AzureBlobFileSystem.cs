using IX.Framework.IO.FileSystem;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Framework.IO.AzureFileSystem
{
    public class AzureBlobFileSystem : FileSystem.FileSystem
    {
        public AzureBlobFileSystem(string rootPath, string accountName, string keyValue, bool https)
            : base(string.Empty)
        {
            _storageCredentials = new StorageCredentials(accountName, keyValue);
            _account = new CloudStorageAccount(_storageCredentials, https);
        }

        public AzureBlobFileSystem(Uri rootPath, string accountName, string keyValue, bool https)
            : base(string.Empty)
        {
            _storageCredentials = new StorageCredentials(accountName, keyValue);
            _account = new CloudStorageAccount(_storageCredentials, https);
        }

        private readonly StorageCredentials _storageCredentials;

        private readonly CloudStorageAccount _account;

        #region Asynchronous methods

        protected override Task DeleteAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(absolutePath, false, cancellationToken);
        }

        protected override Task DeleteRecursiveAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(absolutePath, true, cancellationToken);
        }

        private async Task DeleteInternalAsync(Uri absolutePath, bool recursive, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            if (absolutePath.Segments.Length == 1)
            {
                CloudBlobContainer container = client.GetContainerReference(absolutePath.Segments[0]);

                if (!recursive && (await container.ListBlobsSegmentedAsync(null, cancellationToken)).Results.Count() > 0)
                    throw new InvalidOperationException();

                await container.DeleteAsync(cancellationToken);
            }
            else
            {
                List<IListBlobItem> blobsFound = await client.ListAllBlobsAsync(absolutePath.GetAzureBlobAddressOrPrefix().ToString(), cancellationToken: cancellationToken);

                if (blobsFound.Count == 0)
                    throw new ResourceNotFoundException();

                if (recursive)
                {
                    Parallel.ForEach(blobsFound, async p =>
                    {
                        await (await client.GetBlobReferenceFromServerAsync(p.Uri, cancellationToken)).DeleteAsync(cancellationToken);
                    });
                }
                else
                {
                    if (blobsFound.Count > 1)
                        throw new InvalidOperationException();

                    ICloudBlob blob = await client.GetBlobReferenceFromServerAsync(blobsFound.Single().Uri, cancellationToken);

                    await blob.DeleteAsync(cancellationToken);
                }
            }
        }

        protected override async Task<FileSystemEntity[]> GetContainedEntitiesAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            List<IListBlobItem> items = await client.ListAllBlobsAsync(absolutePath.GetAzureBlobAddressOrPrefix().ToString(), cancellationToken: cancellationToken);

            List<FileSystemEntity> entities = new List<FileSystemEntity>();

            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Uri relativeUri = item.Uri.GetAzureBlobAddressOrPrefix();
                if (relativeUri.Segments.Length > 1)
                {
                    if (entities.Any(p => p.IsContainer && p.ResourcePath == relativeUri.Segments[0]))
                        continue;

                    entities.Add(new FileSystemEntity
                    {
                        IsContainer = true,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Size = 0,
                        ResourceName = relativeUri.Segments[0],
                        ResourcePath = relativeUri.Segments[0]
                    });
                }
                else
                {
                    ICloudBlob blob = client.GetBlobReferenceFromServer(item.Uri);

                    BlobProperties bp = blob.Properties;

                    entities.Add(new FileSystemEntity
                    {
                        IsContainer = false,
                        CreatedDate = bp.LastModified?.DateTime ?? DateTime.UtcNow,
                        ModifiedDate = bp.LastModified?.DateTime ?? DateTime.UtcNow,
                        Size = bp.Length,
                        ResourceName = relativeUri.Segments[0],
                        ResourcePath = relativeUri.Segments[0]
                    });
                }
            }

            return entities.ToArray();
        }

        protected override async Task MoveAbsoluteAsync(Uri absolutePath, Uri newAbsolutePath, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = await client.GetBlobReferenceFromServerAsync(absolutePath, cancellationToken);
            if (await originalBlob.ExistsAsync(cancellationToken))
                throw new ResourceNotFoundException();
            ICloudBlob finalBlob = await client.GetBlobReferenceFromServerAsync(newAbsolutePath, cancellationToken);
            if (await finalBlob.ExistsAsync(cancellationToken))
                throw new InvalidOperationException();

            await finalBlob.StartCopyFromBlobAsync(absolutePath, cancellationToken);

            while (finalBlob.CopyState.Status == CopyStatus.Pending)
                await Task.Delay(1000, cancellationToken);

            if (finalBlob.CopyState.Status != CopyStatus.Success)
            {
                // possibly log
            }

            await originalBlob.DeleteAsync(cancellationToken);
        }

        protected override async Task<Stream> ReadAbsoluteAsync(Uri absolutePath, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = await client.GetBlobReferenceFromServerAsync(absolutePath, cancellationToken);
            if (!await originalBlob.ExistsAsync(cancellationToken))
                throw new ResourceNotFoundException();

            return await originalBlob.OpenReadAsync(cancellationToken);
        }

        protected override async Task<Stream> ReadPartialAbsoluteAsync(Uri absolutePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = await client.GetBlobReferenceFromServerAsync(absolutePath, cancellationToken);
            if (!await originalBlob.ExistsAsync(cancellationToken))
                throw new ResourceNotFoundException();

            using (Stream fs = await originalBlob.OpenReadAsync(cancellationToken))
            {
                MemoryStream ms = new MemoryStream();
                await fs.CopyToPartialAsync(ms, startPosition, length, cancellationToken);

                return ms;
            }
        }

        protected override async Task WriteAbsoluteAsync(Uri absolutePath, Stream data, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = await client.GetBlobReferenceFromServerAsync(absolutePath, cancellationToken);
            if (await originalBlob.ExistsAsync(cancellationToken))
                throw new ResourceNotFoundException();

            await originalBlob.UploadFromStreamAsync(data, cancellationToken);
        }

        protected override Task WritePartialAbsoluteAsync(Uri absolutePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Stream ReadAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = client.GetBlobReferenceFromServer(absolutePath);
            if (originalBlob.Exists())
                throw new ResourceNotFoundException();

            cancellationToken.ThrowIfCancellationRequested();

            return originalBlob.OpenRead();
        }

        protected override Stream ReadPartialAbsolute(Uri absolutePath, long startPosition, long length, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = client.GetBlobReferenceFromServer(absolutePath);
            if (!originalBlob.Exists())
                throw new ResourceNotFoundException();

            cancellationToken.ThrowIfCancellationRequested();

            using (Stream fs = originalBlob.OpenRead())
            {
                MemoryStream ms = new MemoryStream();
                fs.CopyToPartial(ms, startPosition, length);

                return ms;
            }
        }

        protected override void WriteAbsolute(Uri absolutePath, Stream data, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = client.GetBlobReferenceFromServer(absolutePath);
            if (originalBlob.Exists())
                throw new ResourceNotFoundException();

            cancellationToken.ThrowIfCancellationRequested();

            originalBlob.UploadFromStream(data);
        }

        protected override void WritePartialAbsolute(Uri absolutePath, Stream data, long startPosition, long length, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override FileSystemEntity[] GetContainedEntitiesAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            List<IListBlobItem> items = client.ListAllBlobs(absolutePath.GetAzureBlobAddressOrPrefix().ToString());

            List<FileSystemEntity> entities = new List<FileSystemEntity>();

            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Uri relativeUri = item.Uri.GetAzureBlobAddressOrPrefix();
                if (relativeUri.Segments.Length > 1)
                {
                    if (entities.Any(p => p.IsContainer && p.ResourcePath == relativeUri.Segments[0]))
                        continue;

                    entities.Add(new FileSystemEntity
                    {
                        IsContainer = true,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Size = 0,
                        ResourceName = relativeUri.Segments[0],
                        ResourcePath = relativeUri.Segments[0]
                    });
                }
                else
                {
                    ICloudBlob blob = client.GetBlobReferenceFromServer(item.Uri);

                    BlobProperties bp = blob.Properties;

                    entities.Add(new FileSystemEntity
                    {
                        IsContainer = false,
                        CreatedDate = bp.LastModified?.DateTime ?? DateTime.UtcNow,
                        ModifiedDate = bp.LastModified?.DateTime ?? DateTime.UtcNow,
                        Size = bp.Length,
                        ResourceName = relativeUri.Segments[0],
                        ResourcePath = relativeUri.Segments[0]
                    });
                }
            }

            return entities.ToArray();
        }

        protected override void DeleteAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteRecursiveAbsolute(Uri absolutePath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void DeleteInternal(Uri absolutePath, bool recursive, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            if (absolutePath.Segments.Length == 1)
            {
                CloudBlobContainer container = client.GetContainerReference(absolutePath.Segments[0]);

                if (!recursive && (container.ListBlobsSegmented(null)).Results.Count() > 0)
                    throw new InvalidOperationException();

                cancellationToken.ThrowIfCancellationRequested();

                container.Delete();
            }
            else
            {
                List<IListBlobItem> blobsFound = client.ListAllBlobs(absolutePath.GetAzureBlobAddressOrPrefix().ToString(), cancellationToken: cancellationToken);

                if (blobsFound.Count == 0)
                    throw new ResourceNotFoundException();

                cancellationToken.ThrowIfCancellationRequested();
                if (recursive)
                {
                    Parallel.ForEach(blobsFound, p =>
                    {
                        (client.GetBlobReferenceFromServer(p.Uri)).Delete();
                    });
                }
                else
                {
                    if (blobsFound.Count > 1)
                        throw new InvalidOperationException();

                    ICloudBlob blob = client.GetBlobReferenceFromServer(blobsFound.Single().Uri);

                    cancellationToken.ThrowIfCancellationRequested();

                    blob.Delete();
                }
            }
        }

        protected override void MoveAbsolute(Uri absolutePath, Uri newAbsolutePath, CancellationToken cancellationToken)
        {
            CloudBlobClient client = _account.CreateCloudBlobClient();

            ICloudBlob originalBlob = client.GetBlobReferenceFromServer(absolutePath);
            if (originalBlob.Exists())
                throw new ResourceNotFoundException();

            cancellationToken.ThrowIfCancellationRequested();

            ICloudBlob finalBlob = client.GetBlobReferenceFromServer(newAbsolutePath);
            if (finalBlob.Exists())
                throw new InvalidOperationException();

            cancellationToken.ThrowIfCancellationRequested();

            finalBlob.StartCopyFromBlob(absolutePath);

            while (finalBlob.CopyState.Status == CopyStatus.Pending)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Task.Delay(1000, cancellationToken);
            }

            if (finalBlob.CopyState.Status != CopyStatus.Success)
            {
                // possibly log
            }

            originalBlob.Delete();
        }

        #endregion Asynchronous methods
    }
}