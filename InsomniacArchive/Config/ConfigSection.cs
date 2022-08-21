using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

using InsomniacArchive.FileTypes;
using InsomniacArchive.IO;
using InsomniacArchive.Section;

namespace InsomniacArchive.Config
{
    internal abstract class ConfigSection : BaseSection
    {
        public JsonObject RootObject;

        internal override void Read(DatBinaryReader br, DatSectionInfo sectionInfo)
        {
#if DEBUG
            br.BaseStream.Position = sectionInfo.offset;
            var data = br.ReadBytes(sectionInfo.size);
            File.WriteAllBytes($"config/{GetType().Name}.bin", data);
#endif
            br.BaseStream.Position = sectionInfo.offset;

            RootObject = ConfigSerializer.Deserialize(br);
        }

        internal override DatSectionInfo Write(DatBinaryWriter bw)
        {
            throw new NotImplementedException();
        }
    }
}
