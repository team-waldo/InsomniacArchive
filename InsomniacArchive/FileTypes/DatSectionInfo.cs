using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    internal record struct DatSectionInfo
    {
        public uint id;
        public int offset;
        public int size;
    }
}
