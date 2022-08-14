using InsomniacArchive.Section;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.FileTypes
{
    public abstract class DatFileBase
    {
        private DatHeader Header { get; set; }

        protected List<BaseSection> Sections = new ();

        protected abstract string Signature { get; }

        protected virtual void Load(BinaryReader br)
        {
            Header = br.ReadStruct<DatHeader>();

            if (Header.magic != DatHeader.DAT_SIGNATURE)
                throw new IOException($"Invalid DAT signature 0x{Header.magic:08X}, expected 0x{DatHeader.DAT_SIGNATURE:08X}");

            DatSectionInfo[] datSectionInfos = br.ReadStructArray<DatSectionInfo>(Header.sectionCount * Marshal.SizeOf<DatSectionInfo>());
            string sig = br.ReadStringToNull();
            if (sig != Signature)
            {
                throw new IOException($"Invalid DAT signature string '{sig}', expected '{Signature}'");
            }

            Array.Sort(datSectionInfos, (a, b) => (a.offset < b.offset) ? -1 : 1);

            foreach (var sectionInfo in datSectionInfos)
            {
                var section = SectionManager.ReadSection(br, sectionInfo);
                Sections.Add(section);
            }
        }

        internal T GetSection<T>() where T : BaseSection, new()
        {
            // Original implementation is binary searching section ID
            // this is way slow but doesn't matter
            foreach (var item in Sections)
            {
                if (item.GetType() == typeof(T))
                    return (T)item;
            }
            throw new ArgumentException($"Section with type {typeof(T)} is not found.");
        }

        protected virtual void Save(BinaryWriter bw)
        {
            bw.WriteStruct(Header);

            long sectionInfoPos = bw.BaseStream.Position;
            bw.Pad(Sections.Count * Marshal.SizeOf<DatSectionInfo>());

            bw.WriteNullTerminatedString(Signature);

            DatSectionInfo[] sectionInfos = new DatSectionInfo[Sections.Count];

            for (int i = 0; i < Sections.Count; i++)
            {
                var info = Sections[i].Write(bw);
                sectionInfos[i] = info;
            }

            long endPos = bw.BaseStream.Position;

            // section infos should be stored in ID increasing order
            // for binary search
            Array.Sort(sectionInfos, (a, b) => (a.id < b.id) ? -1 : 1);

            bw.BaseStream.Position = sectionInfoPos;
            bw.WriteStructArray(sectionInfos);

            bw.BaseStream.Position = endPos;
        }

        protected abstract void DecompressData(Stream input, MemoryStream output);

        protected abstract void CompressData(MemoryStream input, Stream output);

        public void LoadFile(string path)
        {
            MemoryStream ms = new();

            using (var file = System.IO.File.OpenRead(path))
            {
                DecompressData(file, ms);
            }

#if DEBUG
            System.IO.File.WriteAllBytes(path + ".raw", ms.ToArray());
#endif

            ms.Seek(0, SeekOrigin.Begin);

            using (var br = new BinaryReader(ms))
            {
                Load(br);
            }
        }

        public void SaveFile(string path)
        {
            MemoryStream ms = new();

            using (var bw = new BinaryWriter(ms, Encoding.UTF8, leaveOpen: true))
            {
                Save(bw);
            }

#if DEBUG
            System.IO.File.WriteAllBytes(path + ".raw", ms.ToArray());
#endif

            ms.Seek(0, SeekOrigin.Begin);

            using (var file = System.IO.File.Create(path))
            {
                CompressData(ms, file);
            }
        }
    }
}
