using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.Section
{
    public abstract class StructSection<T> : GenericSection<T> where T : struct
    {
        protected override T[] ReadInternal(BinaryReader br, int totalSize)
        {
            return br.ReadStructArray<T>(totalSize);
        }

        protected override int WriteInternal(BinaryWriter bw, T[] data)
        {
            return bw.WriteStructArray<T>(data);
        }
    }
}
