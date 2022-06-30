using InsomniacArchive.FileTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.Section
{
    internal class RawSection : StructSection<byte>
    {
        public override uint Id => OverrideId;

        public uint OverrideId { get; private set; } = 0;

        internal override void Read(BinaryReader br, DatSectionInfo sectionInfo)
        {
            base.Read(br, sectionInfo);
            OverrideId = sectionInfo.id;
        }
    }
}
