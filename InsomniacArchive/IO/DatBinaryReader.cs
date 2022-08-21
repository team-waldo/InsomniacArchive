using InsomniacArchive.FileTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.IO
{
    public class DatBinaryReader : ExtendedBinaryReader
    {
        private readonly DatFileBase datFile;

        public DatBinaryReader(Stream input, DatFileBase datFile) : base(input)
        {
            this.datFile = datFile;
        }

        public string GetHashedStringLiteral(uint hash)
        {
            return datFile.GetStringLiteral(hash);
        }

        public string ReadHashedStringLiteral()
        {
            uint hash = ReadUInt32();
            return datFile.GetStringLiteral(hash);
        }
    }
}
