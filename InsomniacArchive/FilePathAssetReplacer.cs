using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive
{
    public class FilePathAssetReplacer : IAssetReplacer
    {
        public string FilePath { get; init; }

        public FilePathAssetReplacer(string path)
        {
            FilePath = path;
        }

        public int GetSize()
        {
            FileInfo info = new(FilePath);
            return (int)info.Length;
        }

        public int WriteData(Stream stream)
        {
            byte[] data = System.IO.File.ReadAllBytes(FilePath);
            stream.Write(data);
            return data.Length;
        }
    }
}
