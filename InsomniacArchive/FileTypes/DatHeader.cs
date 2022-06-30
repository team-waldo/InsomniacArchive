using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    internal struct DatHeader
    {
        internal const uint DAT_SIGNATURE = 0x44415431u; // DAT1

        internal uint magic;
        internal uint hash;
        internal int totalSize;
        internal int sectionCount;
    }
}
