using InsomniacArchive.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    /// <summary>
    /// Directed acyclic graph file. Not really sure how to parse these data.
    /// </summary>
    internal class DagFile
    {
        private const uint DAG_COMPRESSED_SIGNATURE = 0x891F77AFu; // ????
        private const uint DAG_DECOMPRESSED_SIGNATURE = 0x44415431u; // DAT1

        private DatHeader Header { get; set; }

        internal byte[] flags;
        internal long[] unk1;
        internal int[] unk2;
        internal int[] nameOffsetArray;
        internal int[] unk4;

        internal string[] nameArray;

        public void LoadFile(string path)
        {
            MemoryStream ms;

            using (var file = File.OpenRead(path))
            {
                ms = DecompressDag(file);
            }

            using (var br = new ExtendedBinaryReader(ms))
            {
                Load(br);
            }
        }

        private void SaveFile(string path)
        {
            throw new NotImplementedException();
        }

        private void Load(ExtendedBinaryReader br)
        {
            Header = br.ReadStruct<DatHeader>();

            if (Header.magic != DAG_DECOMPRESSED_SIGNATURE)
                throw new IOException($"Invalid DAG signature 0x{Header.magic:08X}, expected 0x{DAG_DECOMPRESSED_SIGNATURE:08X}");

            TocSectionEntry[] tocSectionEntries = br.ReadStructArray<TocSectionEntry>(Header.sectionCount * 12);

            string dependencyDag = br.ReadFixedLengthAsciiString(0x0E);
            if (dependencyDag != "DependencyDag")
                throw new IOException($"Invalid DAG signature string '{dependencyDag}', expected 'DependencyDag'");

            br.BaseStream.Position = tocSectionEntries[0].offset;
            flags = br.ReadBytes(tocSectionEntries[0].size);

            br.BaseStream.Position = tocSectionEntries[1].offset;
            unk1 = br.ReadStructArray<long>(tocSectionEntries[1].size);

            br.BaseStream.Position = tocSectionEntries[2].offset;
            unk2 = br.ReadStructArray<int>(tocSectionEntries[2].size);

            br.BaseStream.Position = tocSectionEntries[3].offset;
            unk4 = br.ReadStructArray<int>(tocSectionEntries[3].size);

            br.BaseStream.Position = tocSectionEntries[4].offset;
            nameOffsetArray = br.ReadStructArray<int>(tocSectionEntries[4].size);

            nameArray = new string[nameOffsetArray.Length];
            for (int i = 0; i < nameArray.Length; i++)
            {
                br.BaseStream.Seek(nameOffsetArray[i], SeekOrigin.Begin);
                nameArray[i] = br.ReadStringToNull();
            }
        }

        public void Save(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        private static MemoryStream DecompressDag(Stream stream)
        {
            MemoryStream ms;

            using (BinaryReader br = new BinaryReader(stream))
            {
                uint sig = br.ReadUInt32();
                if (sig != DAG_COMPRESSED_SIGNATURE)
                    throw new IOException($"Invalid TOC signature 0x{sig:08X}, expected 0x{DAG_COMPRESSED_SIGNATURE:08X}");

                int uncompSize = br.ReadInt32();
                ms = new(uncompSize);

                int compSize = br.ReadInt32();

                ushort zlibSig = br.ReadUInt16();
                if (zlibSig != 0xDA78u)
                    throw new IOException($"Invalid ZLIB signature 0x{zlibSig:04X}, expected 0xDA78");

                using DeflateStream dfs = new(stream, CompressionMode.Decompress);
                dfs.CopyTo(ms);
            }

            ms.Position = 0;
            return ms;
        }

        public record class TocFileEntry
        {
            public uint Hash;
            public string FilePath = string.Empty;
            public int TotalSize;
            public bool KeyAsset;
            public List<ChunkData> Chunks = new();

            public record class ChunkData
            {
                public string ArchiveFileName;
                public uint Offset;
                public uint Size;
            }
        }

        internal record struct TocSectionEntry
        {
            public uint id;
            public int offset;
            public int size;
        }

        public record struct UnknownSection1Entry
        {
            public int unk0;
            public int unk4;
        }
    }
}
