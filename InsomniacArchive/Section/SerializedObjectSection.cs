using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

using InsomniacArchive.FileTypes;
using InsomniacArchive.IO;
using InsomniacArchive.Serialization;

namespace InsomniacArchive.Section
{
    internal abstract class SerializedObjectSection : BaseSection
    {
        public JsonObject RootObject;

        public List<JsonObject> ExtraObjects = new();

        internal override void Read(DatBinaryReader br, DatSectionInfo sectionInfo)
        {
            br.BaseStream.Position = sectionInfo.offset;
            RootObject = ObjectSerializer.Deserialize(br);

            while (br.BaseStream.Position < sectionInfo.offset + sectionInfo.size)
            {
                ExtraObjects.Add(ObjectSerializer.Deserialize(br));
            }
        }

        internal override DatSectionInfo Write(DatBinaryWriter bw)
        {
            throw new NotImplementedException();
        }
    }
}
