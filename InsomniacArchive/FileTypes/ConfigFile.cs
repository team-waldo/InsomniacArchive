using InsomniacArchive.Serialization;
using InsomniacArchive.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    internal class ConfigFile : AssetFile
    {
        protected override string Signature => "Config Built File";

        protected override void Load(DatBinaryReader br)
        {
            base.Load(br);
        }
    }
}
