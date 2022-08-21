using InsomniacArchive.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    internal class ChunkedArchiveFile : IDisposable
    {
        private const int DEFAULT_CHUNK_SIZE = 0x40000; // 256KB

        private readonly ChunkedArchiveFileReader chunkReaderStream;

        private readonly DsarHeader header;
        private readonly DsarChunkEntry[] chunks;

        public ChunkedArchiveFile(string path)
        {
            Stream archiveBaseStream = File.OpenRead(path);

            var br = new ExtendedBinaryReader(archiveBaseStream);
            header = br.ReadStruct<DsarHeader>();
            chunks = br.ReadStructArray<DsarChunkEntry>(header.chunkCount * Marshal.SizeOf<DsarChunkEntry>());

            chunkReaderStream = new ChunkedArchiveFileReader(archiveBaseStream, this);
        }

        public byte[] ExtractFile(long offset, int size)
        {
            byte[] data = new byte[size];
            chunkReaderStream.Position = offset;
            chunkReaderStream.Read(data, 0, size);
            return data;
        }

        public void ExtractFileToPath(string path, long offset, int size)
        {
            int fileSize = size;

            using var outputFile = File.Create(path);
            chunkReaderStream.Position = offset;

            byte[] buffer = new byte[0x4000];
            int read;

            while (fileSize > 0 && (read = chunkReaderStream.Read(buffer, 0, Math.Min(buffer.Length, fileSize))) > 0)
            {
                outputFile.Write(buffer, 0, read);
                fileSize -= read;
            }
        }

        public void Dispose()
        {
            chunkReaderStream.Dispose();
        }

        private class ChunkedArchiveFileReader : Stream
        {
            private readonly Stream baseStream;

            private readonly DsarHeader header;
            private readonly DsarChunkEntry[] chunks;

            private long virtualPosition = 0;
            private int currentChunkIndex = -1;

            private readonly byte[] chunkCompressedBuffer;
            private readonly byte[] chunkDecompressedBuffer;

            internal ChunkedArchiveFileReader(Stream baseStream, ChunkedArchiveFile archiveFile)
            {
                this.baseStream = baseStream;

                header = archiveFile.header;
                chunks = archiveFile.chunks;

                int chunkCompressdMaxSize = chunks.Select(x => x.chunkCompressedSize).Max();
                int chunkDecompressedMaxSize = chunks.Select(x => x.chunkDecompressedSize).Max(); // 0x40000

                chunkCompressedBuffer = new byte[chunkCompressdMaxSize];
                chunkDecompressedBuffer = new byte[chunkDecompressedMaxSize];
            }

            public override bool CanRead => true;

            public override bool CanSeek => true;

            public override bool CanWrite => false;

            public override long Length => header.totalVirtualSize;

            public override long Position
            {
                get => virtualPosition;
                set => Seek(value, SeekOrigin.Begin);
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                int dstOffset = offset;
                int left = count;
                int read = 0;

                while (left != 0)
                {
                    var currentChunk = chunks[currentChunkIndex];

                    int chunkLeft = (int)(currentChunk.virtualOffset + currentChunk.chunkDecompressedSize - virtualPosition);
                    int toRead = Math.Min(chunkLeft, left);

                    Buffer.BlockCopy(chunkDecompressedBuffer, (int)(virtualPosition - currentChunk.virtualOffset), buffer, dstOffset, toRead);
                    dstOffset += toRead;
                    left -= toRead;
                    read += toRead;

                    virtualPosition += toRead;

                    if (toRead == chunkLeft && left != 0)
                    {
                        if (currentChunkIndex + 1 >= chunks.Length)
                        {
                            return read;
                        }
                        LoadChunk(currentChunkIndex + 1);
                    }
                }

                return read;
            }

            protected override void Dispose(bool disposing)
            {
                baseStream.Dispose();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                long absoluteOffset;
                if (origin == SeekOrigin.Begin)
                {
                    absoluteOffset = offset;
                }
                else if (origin == SeekOrigin.End)
                {
                    absoluteOffset = header.totalVirtualSize - offset;
                }
                else if (origin == SeekOrigin.Current)
                {
                    absoluteOffset = virtualPosition + offset;
                }
                else
                {
                    throw new ArgumentException($"Invalid SeekOrigin {origin}");
                }

                if (absoluteOffset < 0 || absoluteOffset > header.totalVirtualSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }

                SeekAbsolute(absoluteOffset);
                return virtualPosition;
            }

            private void SeekAbsolute(long offset)
            {
                int chunkIndex = Array.FindIndex(chunks, x => x.virtualOffset + x.chunkDecompressedSize > offset);
                if (chunkIndex < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
                LoadChunk(chunkIndex);
                virtualPosition = offset;
            }

            private void LoadChunk(int chunkIndex)
            {
                if (chunkIndex == currentChunkIndex)
                    return;

                var newChunk = chunks[chunkIndex];
                baseStream.Position = newChunk.realOffset;

                baseStream.Read(chunkCompressedBuffer, 0, newChunk.chunkCompressedSize);
                int decompressedLength = K4os.Compression.LZ4.LZ4Codec.Decode(chunkCompressedBuffer, 0, newChunk.chunkCompressedSize, chunkDecompressedBuffer, 0, newChunk.chunkDecompressedSize);

                if (decompressedLength != newChunk.chunkDecompressedSize)
                {
                    throw new IOException("Failed to decompress the chunk.");
                }

                currentChunkIndex = chunkIndex;
            }

            public override void SetLength(long value)
            {
                throw new InvalidOperationException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new InvalidOperationException();
            }

            public override void Flush()
            {
                throw new InvalidOperationException();
            }
        }

        //private class ChunkedArchiveFileWriter : Stream
        //{
        //    private readonly Stream outputStream;
        //    private readonly BinaryWriter bw;

        //    private readonly long totalVirtualSize;
        //    private readonly int chunkSize;

        //    private readonly int chunkCount;

        //    private long currentVirtualPosition = 0;

        //    private int currentChunkIndex = 0;
        //    private int currentChunkPosition = 0;
        //    private long currentChunkStartVirtualPosition = 0;

        //    private readonly DsarHeader header;
        //    private readonly DsarChunkEntry[] chunks;

        //    private byte[] chunkRawBuffer;
        //    private byte[] chunkCompressedBuffer;

        //    public ChunkedArchiveFileWriter(Stream outputStream, long totalSize, int chunkSize = DEFAULT_CHUNK_SIZE)
        //    {
        //        this.outputStream = outputStream;
        //        this.bw = new BinaryWriter(outputStream);

        //        this.totalVirtualSize = totalSize;
        //        this.chunkSize = chunkSize;

        //        this.chunkCount = (int)Math.Ceiling((double)(totalSize / chunkSize));

        //        header = new()
        //        {
        //            magic = DsarHeader.DSAR_MAGIC,
        //            unk04 = DsarHeader.DEFAULT_UNK04,
        //            unk06 = DsarHeader.DEFAULT_UNK06,
        //            chunkCount = this.chunkCount,
        //            tableSize = 0x20 * (this.chunkCount + 1),
        //            totalVirtualSize = this.totalVirtualSize,
        //            padding = DsarHeader.DEFAULT_PADDING,
        //        };

        //        chunks = new DsarChunkEntry[chunkCount];

        //        int compressedBufferSize = K4os.Compression.LZ4.LZ4Codec.MaximumOutputSize(chunkSize);

        //        chunkRawBuffer = new byte[chunkSize];
        //        chunkCompressedBuffer = new byte[compressedBufferSize];
        //    }

        //    public override void Write(byte[] buffer, int offset, int count)
        //    {
        //        int inputIndex = offset;
        //        int dataLeft = count;
                
        //        while (dataLeft > 0)
        //        {
        //            int chunkLeft = chunkSize - currentChunkPosition;
        //            int toWrite = Math.Min(chunkLeft, dataLeft);

        //            Buffer.BlockCopy(buffer, inputIndex, chunkRawBuffer, currentChunkPosition, toWrite);
        //            currentVirtualPosition += toWrite;
        //            currentChunkPosition += toWrite;
        //            dataLeft -= toWrite;

        //            if (currentChunkPosition == chunkSize && dataLeft > 0 && currentVirtualPosition < totalVirtualSize)
        //            {
        //                WriteCurrentChunk();
        //                currentChunkIndex++;
        //            }
        //        }
                
        //        if (currentVirtualPosition == totalVirtualSize)
        //        {
        //            WriteCurrentChunk();
        //            WriteHeader();
        //        }
        //    }

        //    public override void Flush()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    private void WriteCurrentChunk()
        //    {
        //        if (currentChunkPosition == 0)
        //            throw new InvalidOperationException("Cannot write an empty chunk");

        //        bw.Seek(0, SeekOrigin.End);
        //        bw.Pad(0x10);

        //        int compressedSize = K4os.Compression.LZ4.LZ4Codec.Encode(chunkRawBuffer, 0, currentChunkPosition, chunkCompressedBuffer, 0, chunkSize);
        //        outputStream.Write(chunkCompressedBuffer, 0, compressedSize);

        //        DsarChunkEntry chunkInfo = new()
        //        {
        //            virtualOffset = currentChunkStartVirtualPosition,
        //            realOffset = outputStream.Position,
        //            chunkDecompressedSize = currentChunkPosition,
        //            chunkCompressedSize = compressedSize,
        //            unk = 0x5555555555555503,
        //        };

        //        chunks[currentChunkIndex] = chunkInfo;

        //        currentChunkPosition = 0;
        //        currentChunkStartVirtualPosition = currentVirtualPosition;
        //    }

        //    private void WriteHeader()
        //    {
        //        outputStream.Position = 0;

        //        DsarHeader header = new()
        //        {
        //            magic = DsarHeader.DSAR_MAGIC,
        //            unk04 = DsarHeader.DEFAULT_UNK04,
        //            unk06 = DsarHeader.DEFAULT_UNK06,
        //            chunkCount = this.chunkCount,
        //            tableSize = 0x20 * (this.chunkCount + 1),
        //            totalVirtualSize = this.totalVirtualSize,
        //            padding = DsarHeader.DEFAULT_PADDING,
        //        };

        //        bw.WriteStruct<DsarHeader>(header);
        //    }

        //    public override bool CanRead => false;

        //    public override bool CanSeek => false;

        //    public override bool CanWrite => false;

        //    public override long Length => throw new NotImplementedException();

        //    public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //    public override int Read(byte[] buffer, int offset, int count) => throw new InvalidOperationException();

        //    public override long Seek(long offset, SeekOrigin origin) => throw new InvalidOperationException();

        //    public override void SetLength(long value) => throw new InvalidOperationException();

        //}

        internal record struct DsarHeader
        {
            public uint magic;  // 0x52415344u = 'DSAR'
            public short unk04; // 0x0003
            public short unk06; // 0x0001
            public int chunkCount;
            public int tableSize;
            public long totalVirtualSize;
            public long padding;
        }

        internal record struct DsarChunkEntry
        {
            public long virtualOffset;
            public long realOffset;
            public int chunkDecompressedSize;
            public int chunkCompressedSize;
            public long unk; // 0x5555555555555503
        }
    }
}
