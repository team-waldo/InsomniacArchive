using InsomniacArchive.FileTypes;
using InsomniacArchive.Hash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.IO
{
    public class DatBinaryWriter : ExtendedBinaryWriter
    {
        private readonly DatFileBase datFile;

        public DatBinaryWriter(Stream output, DatFileBase datFile) : base(output)
        {
            this.datFile = datFile;
        }

        public void WriteHashedString(string value)
        {
            uint hash = Crc32.HashPath(value);
            Write(hash);
            datFile.AddStringLiteral(value);
        }
    }
}
