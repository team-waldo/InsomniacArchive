using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    public abstract class AssetFile : DatFileBase
    {
        private uint Id { get; set; }

        internal static byte[] DecompressAsset(string path)
        {
            int rawsize;
            byte[] compressed;

            using (var file = File.OpenRead(path))
            using (var br = new BinaryReader(file))
            {
                uint magic = br.ReadUInt32();
                rawsize = br.ReadInt32();

                file.Position = 0x24;
                compressed = new byte[file.Length - 0x24];
                file.Read(compressed);
            }

            byte[] decompressed = new byte[rawsize];

            K4os.Compression.LZ4.LZ4Codec.Decode(compressed, decompressed);

            return decompressed;
        }

        protected override void CompressData(MemoryStream input, Stream output)
        {
            byte[] data = input.ToArray();
            byte[] compressed = new byte[data.Length];

            int compSize = K4os.Compression.LZ4.LZ4Codec.Encode(data, compressed);

            using (BinaryWriter bw = new(output))
            {
                bw.Write(Id);
                bw.Write(data.Length);

                bw.Pad(0x24 - (int)bw.BaseStream.Position);
                bw.Write(compressed, 0, compSize);
            }
        }

        protected override void DecompressData(Stream input, MemoryStream output)
        {
            int rawsize;
            byte[] compressed;

            using (var br = new BinaryReader(input))
            {
                Id = br.ReadUInt32();
                rawsize = br.ReadInt32();

                input.Position = 0x24;
                compressed = new byte[input.Length - 0x24];
                input.Read(compressed);
            }

            byte[] decompressed = new byte[rawsize];
            K4os.Compression.LZ4.LZ4Codec.Decode(compressed, decompressed);

            output.Write(decompressed);
        }
    }
}
