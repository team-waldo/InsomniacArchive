using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using InsomniacArchive.FileTypes;

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

        public void ExtractFile(int index, string outputPath)
        {
            var fileChunkData = Toc.fileChunkDataArray[index];

            if (fileChunkData.chunkCount != 1)
                throw new IOException("multiple chunk file is not supported");

            var chunk = Toc.chunkInfoArray[fileChunkData.chunkArrayIndex];
            var archive = Toc.archiveFileArray[chunk.archiveFileNo];

            int bytes = fileChunkData.totalSize;

            var archivePath = GetFilePath(archive.FileName);

            using (var archiveFile = File.OpenRead(archivePath))
            using (var outputFile = File.Create(outputPath))
            {
                archiveFile.Position = chunk.offset;

                byte[] buffer = new byte[0x8000];
                int read;
                while (bytes > 0 && (read = archiveFile.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
                {
                    outputFile.Write(buffer, 0, read);
                    bytes -= read;
                }
            }
        }

        public void ReplaceFile(int index, IAssetReplacer replacer)
        {
            replacerDict[index] = replacer;
        }

        public void SaveArchives(string outputDirectory)
        {
            Dictionary<string, Stream> newArchivesDict = new();

            Stream GetNewArchiveStream(string name)
            {
                if (newArchivesDict.TryGetValue(name, out Stream opened))
                {
                    return opened;
                }

                string newArchivePath = Path.Combine(outputDirectory, name);
                File.Copy(GetFilePath(name), newArchivePath, true);

                Stream newArchiveStream = File.OpenWrite(newArchivePath);
                newArchiveStream.Seek(0, SeekOrigin.End);

                newArchivesDict.Add(name, newArchiveStream);

                return newArchiveStream;
            }

            foreach (var replacerPair in replacerDict)
            {
                int index = replacerPair.Key;

                int group = Toc.GetGroupIndex(index);
                IAssetReplacer replacer = replacerPair.Value;

                TocFile.FileChunkDataEntry fileChunkData = Toc.fileChunkDataArray[index];
                fileChunkData.totalSize = replacer.GetSize();
                Toc.fileChunkDataArray[index] = fileChunkData;

                TocFile.ChunkInfoEntry chunkInfo = Toc.chunkInfoArray[fileChunkData.chunkArrayIndex];

                string archiveName = Toc.archiveFileArray[chunkInfo.archiveFileNo].FileName;
                Stream outputFileStream = GetNewArchiveStream(archiveName);

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
