using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Framework.IO
{
    public static class StreamExtensions
    {
        public static async Task<long> CopyToPartialAsync(this Stream source, Stream destination, long startIndex, long length, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (!source.CanRead)
                throw new ArgumentException(nameof(source));
            if (!destination.CanWrite)
                throw new ArgumentException(nameof(destination));

            if (source.CanSeek)
            {
                source.Seek(startIndex, SeekOrigin.Begin);
            }

            byte[] buffer = new byte[4096];
            int currentlyToReadBytes = (4096 > length ? 4096 : (int)length);
            int readBytes = 0; long totalReadBytes = 0;

            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                readBytes = await source.ReadAsync(buffer, 0, currentlyToReadBytes);
                totalReadBytes += readBytes;

                if (readBytes == 0 || totalReadBytes == length)
                    break;

                await destination.WriteAsync(buffer, 0, readBytes);

                currentlyToReadBytes = (length - totalReadBytes > 4096) ? 4096 : (int)(length - totalReadBytes);
            }
            while (true);

            return totalReadBytes;
        }

        public static long CopyToPartial(this Stream source, Stream destination, long startIndex, long length, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (!source.CanRead)
                throw new ArgumentException(nameof(source));
            if (!destination.CanWrite)
                throw new ArgumentException(nameof(destination));

            if (source.CanSeek)
            {
                source.Seek(startIndex, SeekOrigin.Begin);
            }

            byte[] buffer = new byte[4096];
            int currentlyToReadBytes = (4096 > length ? 4096 : (int)length);
            int readBytes = 0; long totalReadBytes = 0;

            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                readBytes = source.Read(buffer, 0, currentlyToReadBytes);
                totalReadBytes += readBytes;

                if (readBytes == 0 || totalReadBytes == length)
                    break;

                destination.Write(buffer, 0, readBytes);

                currentlyToReadBytes = (length - totalReadBytes > 4096) ? 4096 : (int)(length - totalReadBytes);
            }
            while (true);

            return totalReadBytes;
        }
    }
}
