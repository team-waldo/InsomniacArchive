using InsomniacArchive.Section;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    public class TocFile : DatFileBase
    {
        private const uint TOC_COMPRESSED_SIGNATURE = 0x77AF12AFu; // ????

        protected override string Signature => "ArchiveTOC";

        internal ArchiveFileStruct[] archiveFileArray;
        internal ulong[] nameHashArray;
        internal FileChunkDataEntry[] fileChunkDataArray;
        internal ulong[] keyAssetHashArray;
        internal ChunkInfoEntry[] chunkInfoArray;
        internal GroupEntry[] GroupDataArray;

        internal Dictionary<int, string> languageMap = new();

        protected override void DecompressData(Stream input, MemoryStream output)
        {
            using (BinaryReader br = new BinaryReader(input))
            {
                uint sig = br.ReadUInt32();
                if (sig != TOC_COMPRESSED_SIGNATURE)
                    throw new IOException($"Invalid TOC signature 0x{sig:08X}, expected 0x{TOC_COMPRESSED_SIGNATURE:08X}");

                int uncompSize = br.ReadInt32();

                ushort zlibSig = br.ReadUInt16();
                if (zlibSig != 0xDA78u)
                    throw new IOException($"Invalid ZLIB signature 0x{zlibSig:04X}, expected 0xDA78");

                using DeflateStream dfs = new(input, CompressionMode.Decompress);
                dfs.CopyTo(output);
            }
        }

        protected override void CompressData(MemoryStream input, Stream output)
        {
            using BinaryWriter bw = new BinaryWriter(output);

            bw.Write(TOC_COMPRESSED_SIGNATURE);
            bw.Write((int)input.Length);
            bw.Write((ushort)0xDA78u);

            using DeflateStream dfs = new(output, CompressionMode.Compress);
            input.CopyTo(dfs);
        }

        internal int GetGroupIndex(int index)
        {
            for (int i = 0; i < GroupDataArray.Length; i++)
            {
                var range = GroupDataArray[i];
                if (range.offset <= index && range.offset + range.size >= index)
                {
                    return i;
                }
            }
            return -1;
        }

        protected override void Load(BinaryReader br)
        {
            base.Load(br);

            archiveFileArray = GetSection<ArchiveFileSection>().Data;
            nameHashArray = GetSection<NameHashSection>().Data;
            fileChunkDataArray = GetSection<FileChunkDataSection>().Data;
            keyAssetHashArray = GetSection<KeyAssetHashSection>().Data;
            chunkInfoArray = GetSection<ChunkInfoSection>().Data;
            GroupDataArray = GetSection<GroupSection>().Data;

            for (int i = 0; i < nameHashArray.Length; i++)
            {
                var file = fileChunkDataArray[i];
                var chunk = chunkInfoArray[file.chunkArrayIndex];
                var archive = archiveFileArray[chunk.archiveFileNo];
                string filename = archive.FileName;
                int dotIndex = filename.IndexOf('.');
                if (dotIndex == -1)
                    continue;
                string lang = filename.Substring(dotIndex + 1);

                int groupIndex = GetGroupIndex(i);
                if (!languageMap.ContainsKey(groupIndex))
                    languageMap[groupIndex] = lang;
            }
        }

        public class ArchiveFileSection : StructSection<ArchiveFileStruct>
        {
            public override uint Id => 0x398abff0;
        }

        [DebuggerDisplay("{flag},{unk02},{unk03},{unk04},{unk06},{FileName}")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public unsafe struct ArchiveFileStruct
        {
            public ushort flag;
            public byte unk02;
            public byte unk03;
            public ushort unk04;
            public ushort unk06;
            private fixed byte _fileName[16];

            public string FileName
            {
                get
                {
                    fixed (byte* p = _fileName)
                    {
                        return Marshal.PtrToStringAnsi((IntPtr)p);
                    }
                }
                
                set
                {
                    fixed (byte* p = _fileName)
                    {
                        byte[] data = Encoding.ASCII.GetBytes(value);
                        if (data.Length >= 16)
                            throw new ArgumentException("FileName is too long");

                        Marshal.Copy(data, 0, (IntPtr)p, data.Length);
                        p[data.Length] = 0;
                    }
                }
            }
        }

        public record class ArchiveFileEntry : IBinarySerializable
        {
            public ushort flag;
            public byte unk02;
            public byte unk03;
            public ushort unk04;
            public ushort unk06;
            public string fileName = string.Empty;

            public void Load(BinaryReader br)
            {
                flag = br.ReadUInt16();
                unk02 = br.ReadByte();
                unk03 = br.ReadByte();
                unk04 = br.ReadUInt16();
                unk06 = br.ReadUInt16();
                fileName = br.ReadFixedLengthAsciiString(16);
            }

            public void Save(BinaryWriter bw)
            {
                bw.Write(flag);
                bw.Write(unk02);
                bw.Write(unk03);
                bw.Write(unk04);
                bw.Write(unk06);
                bw.WriteFixedLengthString(fileName, 16);
            }
        }

        public class NameHashSection : StructSection<ulong>
        {
            public override uint Id => 0x506D_7B8A;
        }

        public class KeyAssetHashSection : StructSection<ulong>
        {
            public override uint Id => 0x6D92_1D7B;
        }

        public class ChunkInfoSection : StructSection<ChunkInfoEntry>
        {
            public override uint Id => 0xDCD720B5;
        }

        public record struct ChunkInfoEntry
        {
            public int archiveFileNo;
            public uint offset;
        }

        public class FileChunkDataSection : StructSection<FileChunkDataEntry>
        {
            public override uint Id => 0x65BC_F461;
        }

        public record struct FileChunkDataEntry
        {
            public int chunkCount;
            public int totalSize;
            public int chunkArrayIndex;
        }

        public class GroupSection : StructSection<GroupEntry>
        {
            public override uint Id => 0xEDE8_ADA9;
        }

        public record struct GroupEntry
        {
            public uint offset;
            public uint size;
        }
    }
}
