using InsomniacArchive.FileTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.Section
{
    public abstract class BaseSection
    {
        public abstract uint Id { get; }

        internal abstract void Read(BinaryReader br, DatSectionInfo sectionInfo);

        internal abstract DatSectionInfo Write(BinaryWriter bw);
    }

    public abstract class GenericSection<T> : BaseSection
    {
        public T[] Data { get; set; }

        internal override void Read(BinaryReader br, DatSectionInfo sectionInfo)
        {
            br.BaseStream.Position = sectionInfo.offset;
            Data = ReadImpl(br, sectionInfo.size);
        }

        internal override DatSectionInfo Write(BinaryWriter bw)
        {
            DatSectionInfo sectionInfo = new();

            bw.AlignStream();

            sectionInfo.id = Id;
            sectionInfo.offset = (int)bw.BaseStream.Position;
            sectionInfo.size = WriteImpl(bw, Data);

            return sectionInfo;
        }

        protected abstract T[] ReadImpl(BinaryReader br, int totalSize);
        protected abstract int WriteImpl(BinaryWriter bw, T[] data);
    }
}
