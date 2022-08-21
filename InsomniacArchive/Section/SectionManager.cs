using InsomniacArchive.FileTypes;
using InsomniacArchive.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.Section
{
    internal class SectionManager
    {
        private static SectionManager _instance;

        public static SectionManager Instance => _instance ??= new SectionManager();

        private readonly Dictionary<uint, Type> _sectionTypes = new();

        private SectionManager()
        {
            var classes = typeof(SectionManager).Assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(BaseSection)))
                .ToList();

            foreach (var cls in classes)
            {
                try
                {
                    var instance = (BaseSection)Activator.CreateInstance(cls);
                    _sectionTypes.Add(instance.Id, cls);
                }
                catch (NotImplementedException)
                {
                    continue;
                }
            }
        }

        public static BaseSection ReadSection(DatBinaryReader br, DatSectionInfo sectionInfo)
        {
            uint id = sectionInfo.id;

            if (!Instance._sectionTypes.TryGetValue(id, out Type cls))
                cls = typeof(RawSection);

            var instance = (BaseSection)Activator.CreateInstance(cls);
            instance.Read(br, sectionInfo);
            return instance;
        }
    }
}
