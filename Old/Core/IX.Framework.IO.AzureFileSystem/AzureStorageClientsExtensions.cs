using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Framework.IO.AzureFileSystem
{
    internal static class AzureStorageClientsExtensions
    {
        public static List<IListBlobItem> ListAllBlobs(this CloudBlobClient client, string prefix, bool useFlatBlobListing = false, BlobListingDetails blobListingDetails = BlobListingDetails.None, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<IListBlobItem> blobItems = new List<IListBlobItem>();
            BlobResultSegment resultSegment = null;

            do
            {
                resultSegment = client.ListBlobsSegmented(prefix, useFlatBlobListing, blobListingDetails, null, resultSegment?.ContinuationToken, options, operationContext);
                blobItems.AddRange(resultSegment.Results);

                cancellationToken.ThrowIfCancellationRequested();
            }
            while (resultSegment?.ContinuationToken != null);

            return blobItems;
        }

        public static async Task<List<IListBlobItem>> ListAllBlobsAsync(this CloudBlobClient client, string prefix, bool useFlatBlobListing = false, BlobListingDetails blobListingDetails = BlobListingDetails.None, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<IListBlobItem> blobItems = new List<IListBlobItem>();
            BlobResultSegment resultSegment = null;

            do
            {
                resultSegment = await client.ListBlobsSegmentedAsync(prefix, useFlatBlobListing, blobListingDetails, null, resultSegment?.ContinuationToken, options, operationContext, cancellationToken);
                blobItems.AddRange(resultSegment.Results);

                cancellationToken.ThrowIfCancellationRequested();
            }
            while (resultSegment?.ContinuationToken != null);

            return blobItems;
        }

        public static Uri GetAzureBlobAddressOrPrefix(this Uri uri)
        {
            return uri.MakeRelativeUri(new Uri(new Uri(uri.Host), uri.Segments[0]));
        }
    }
}
