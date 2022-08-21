using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using InsomniacArchive.IO;
using InsomniacArchive.Section;

namespace InsomniacArchive.FileTypes
{
    public class LocalizationFile : AssetFile
    {
        protected override string Signature => "Localization Built File";

        public Dictionary<string, string> ExtractStrings()
        {
            var keyOffset = GetSection<KeyOffsetSection>();
            var keyData = GetSection<KeyDataSection>();

            var stringOffset = GetSection<TranslationOffsetSection>();
            var stringData = GetSection<TranslationDataSection>();

            Dictionary<string, string> ret = new();

            for (int i = 0; i < keyOffset.Data.Length; i++)
            {
                string key = keyData.GetString(keyOffset.Data[i]);
                string value = stringData.GetString(stringOffset.Data[i]);

                ret.Add(key, value);
            }

            return ret;
        }

        public int ImportStrings(Dictionary<string, string> tr)
        {
            var keyOffset = GetSection<KeyOffsetSection>();
            var keyData = GetSection<KeyDataSection>();

            var stringOffset = GetSection<TranslationOffsetSection>();
            var stringData = GetSection<TranslationDataSection>();

            MemoryStream newStringData = new();
            ExtendedBinaryWriter bw = new (newStringData);

            int[] newStringOffsetArray = new int[stringOffset.Data.Length];
            int importedCount = 0;

            for (int i = 0; i < keyOffset.Data.Length; i++)
            {
                string key = keyData.GetString(keyOffset.Data[i]);
                string value;

                if (tr.TryGetValue(key, out value))
                {
                    importedCount++;
                }
                else
                {
                    value = stringData.GetString(stringOffset.Data[i]);
                }

                int offset;

                if (key != "INVALID" && value == string.Empty)
                {
                    offset = 0;
                }
                else
                {
                    offset = (int)newStringData.Position;
                    bw.WriteNullTerminatedString(value);
                }

                newStringOffsetArray[i] = offset;
            }

            stringOffset.Data = newStringOffsetArray;
            stringData.Data = newStringData.ToArray();

            return importedCount;
        }

        internal class KeyDataSection : StringDataSection
        {
            public override uint Id => 0x4d73cebdu;
        }

        internal class KeyOffsetSection : StructSection<int>
        {
            public override uint Id => 0xa4ea55b2;
        }

        internal class TranslationDataSection : StringDataSection
        {
            public override uint Id => 0x70a382b8;
        }

        internal class TranslationOffsetSection : StructSection<int>
        {
            public override uint Id => 0xf80deeb4u;
        }

        internal class UnknownSection05 : StructSection<int>
        {
            public override uint Id => 0xb0653243;
        }

        internal class UnknownSection06 : StructSection<int>
        {
            public override uint Id => 0xc43731b5u;
        }
    }
}
