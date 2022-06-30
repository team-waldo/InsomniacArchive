using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive
{
    public interface IAssetReplacer
    {
        public int GetSize();
        public int WriteData(Stream stream);
    }
}
