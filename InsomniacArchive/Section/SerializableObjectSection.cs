using InsomniacArchive.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.Section
{
    internal abstract class SerializableObjectSection<T> : GenericSection<T> where T : IBinarySerializable, new()
    {
        protected override T[] ReadImpl(DatBinaryReader br, int totalSize)
        {
            List<T> result = new();

            long startPos = br.BaseStream.Position;
            long endPos = startPos + totalSize;

            while (br.BaseStream.Position < endPos)
            {
                T t = new();
                t.Load(br);
                result.Add(t);
            }

            return result.ToArray();
        }

        protected override int WriteImpl(DatBinaryWriter bw, T[] data)
        {
            long startPos = bw.BaseStream.Position;

            bw.WriteObjectArray<T>(data);

            long endPos = bw.BaseStream.Position;

            return (int)(endPos - startPos);
        }
    }
}
