using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive
{
    internal interface IBinarySerializable
    {
        public void Load(BinaryReader br);

        public void Save(BinaryWriter bw);
    }
}
