using InsomniacArchive.IO;
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
        private uint AssetId { get; set; }

        private bool Compressed { get; set; }

        protected override void CompressData(MemoryStream input, Stream output)
        {
            ExtendedBinaryWriter bw = new(output);

            bw.Write(AssetId);
            bw.Write(input.Length);

            bw.Pad(0x24 - (int)bw.BaseStream.Position);

            if (!Compressed)
            {
                input.CopyTo(output);
                return;
            }

            byte[] data = input.ToArray();
            byte[] compressed = new byte[data.Length];

            int compSize = K4os.Compression.LZ4.LZ4Codec.Encode(data, compressed);

            bw.Write(compressed, 0, compSize);
        }

        protected override void DecompressData(Stream input, MemoryStream output)
        {
            int compSize;
            int rawsize;

            BinaryReader br = new(input);
            
            AssetId = br.ReadUInt32();

            compSize = (int)input.Length - 0x24;
            rawsize = br.ReadInt32();

            Compressed = false; // compSize != rawsize;

            input.Position = 0x24;

            if (!Compressed)
            {
                input.CopyTo(output);
                return;
            }

            byte[] compressedData = new byte[input.Length - 0x24];
            input.Read(compressedData);
            
            byte[] decompressedData = new byte[rawsize];
            K4os.Compression.LZ4.LZ4Codec.Decode(compressedData, decompressedData);

            output.Write(decompressedData);
        }
    }
}
