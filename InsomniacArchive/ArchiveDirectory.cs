using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using InsomniacArchive.FileTypes;
using InsomniacArchive.Hash;
using InsomniacArchive.IO;

namespace InsomniacArchive
{
    public class ArchiveDirectory
    {
        public string DirectoryPath { get; init; }

        internal TocFile Toc;
        // internal DagFile Dag;

        private Dictionary<int, IAssetReplacer> replacerDict = new();

        public ArchiveDirectory(string directoryPath)
        {
            DirectoryPath = directoryPath;

            Toc = new TocFile();
            Toc.LoadFile(GetFilePath("toc"));

            // Dag = new DagFile();
            // Dag.LoadFile(GetFilePath("dag"));
        }

        internal string GetFilePath(string fileName) => Path.Combine(DirectoryPath, fileName);

        public void ExtractAll(string name, string outputPath)
        {
            ulong hash = Crc64.CalcHash(name);

            foreach (int index in Toc.nameHashArray.Select((b, i) => b == hash ? i : -1).Where(i => i != -1))
            {
                string filePath = Path.Combine(outputPath, $"{name}.{index}");

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                ExtractFile(index, filePath);
            }
        }

        public List<int> GetAllAssetIndex(string name)
        {
            ulong hash = Crc64.CalcHash(name);
            return Toc.nameHashArray.Select((b, i) => b == hash ? i : -1).Where(i => i != -1).ToList();
        }

        public void ExtractFile(int index, string outputPath)
        {
            var fileChunkData = Toc.fileChunkDataArray[index];

            if (fileChunkData.chunkCount != 1)
                throw new IOException("multiple chunk file is not supported");

            var chunk = Toc.chunkInfoArray[fileChunkData.chunkArrayIndex];
            var archive = Toc.archiveFileArray[chunk.archiveFileNo];

            int fileSize = fileChunkData.totalSize;

            var archivePath = GetFilePath(archive.FileName);

            ChunkedArchiveFile archiveFile = new(archivePath);
            archiveFile.ExtractFileToPath(outputPath, chunk.offset, fileSize);
        }

        public void ReplaceFile(int index, IAssetReplacer replacer)
        {
            replacerDict[index] = replacer;
        }

        public void SaveArchives(string outputDirectory)
        {
            Dictionary<string, Stream> newArchivesDict = new();

            string newArchivePath = Path.Combine(outputDirectory, "patch.archive");
            Stream newArchiveStream = File.Create(newArchivePath);

            var archiveFileArray = Toc.archiveFileArray;
            Array.Resize(ref archiveFileArray, archiveFileArray.Length + 1);

            var newArchiveFileEntry = new TocFile.ArchiveFileEntry()
            {
                flag = 2,
                unk02 = 0,
                unk03 = 0,
                unk04 = 0xCCCC,
                unk06 = 1,
                FileName = "patch.archive",
            };

            archiveFileArray[^1] = newArchiveFileEntry;
            Toc.GetSection<TocFile.ArchiveFileSection>().Data = archiveFileArray;

            foreach (var replacerPair in replacerDict)
            {
                int index = replacerPair.Key;
                IAssetReplacer replacer = replacerPair.Value;

                TocFile.FileChunkDataEntry fileChunkData = Toc.fileChunkDataArray[index];
                fileChunkData.totalSize = replacer.GetSize();
                Toc.fileChunkDataArray[index] = fileChunkData;

                TocFile.ChunkInfoEntry chunkInfo = Toc.chunkInfoArray[fileChunkData.chunkArrayIndex];

                Stream outputFileStream = newArchiveStream;

                chunkInfo.archiveFileNo = archiveFileArray.Length - 1;
                chunkInfo.offset = (uint)outputFileStream.Position;
                Toc.chunkInfoArray[fileChunkData.chunkArrayIndex] = chunkInfo;

                replacer.WriteData(outputFileStream);
            }

            foreach (var stream in newArchivesDict.Values)
            {
                stream.Dispose();
            }

            string newTocFilePath = Path.Combine(outputDirectory, "toc");
            Toc.SaveFile(newTocFilePath);
        }
    }
}
