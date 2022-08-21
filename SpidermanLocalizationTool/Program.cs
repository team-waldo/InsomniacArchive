using CommandLine;
using System.IO;
using System;
using System.Text;

using InsomniacArchive;
using InsomniacArchive.FileTypes;
using System.Xml.Linq;

namespace SpidermanLocalizationTool
{
    internal class Program
    {

#pragma warning disable CS8618 // disable notnull check

        [Verb("export", HelpText = "Export localization file into csv file for translation")]
        private class LocExportOptions
        {
            [Value(0, HelpText = "Localization file input path", MetaName = nameof(InputPath))]
            public string InputPath { get; set; }

            [Value(1, HelpText = "Translation csv file output path", MetaName = nameof(OutputPath))]
            public string OutputPath { get; set; }
        }

        [Verb("import", HelpText = "Import translated csv file into original localization file")]
        private class LocImportOptions
        {
            [Value(0, HelpText = "Localization file input path", MetaName = nameof(InputPath))]
            public string InputPath { get; set; }

            [Value(1, HelpText = "Translation csv file input path", MetaName = nameof(TranslationPath))]
            public string TranslationPath { get; set; }

            [Value(2, HelpText = "Translated Localization file output path", MetaName = nameof(OutputPath))]
            public string OutputPath { get; set; }
        }

        [Verb("arc-extract", HelpText = "Extract file from the archive files")]
        private class ArchiveExtractOptions
        {
            [Value(0, HelpText = "Game archive files input directory", MetaName = nameof(InputDirectory))]
            public string InputDirectory { get; set; }

            [Value(1, HelpText = "Index of the asset to extract", MetaName = nameof(AssetIndex))]
            public int AssetIndex { get; set; }

            [Value(2, HelpText = "Path to save extracted asset file", MetaName = nameof(OutputPath))]
            public string OutputPath { get; set; }
        }

        [Verb("arc-extract-all", HelpText = "Extract file from the archive files")]
        private class ArchiveExtractAllOptions
        {
            [Value(0, HelpText = "Game archive files input directory", MetaName = nameof(InputDirectory))]
            public string InputDirectory { get; set; }

            [Value(1, HelpText = "Name of the asset to extract", MetaName = nameof(AssetName))]
            public string AssetName { get; set; }

            [Value(2, HelpText = "Path to save extracted asset file", MetaName = nameof(OutputDirectory))]
            public string OutputDirectory { get; set; }
        }

        [Verb("arc-import", HelpText = "Import file into the archive file")]
        private class ArchiveImportOptions
        {
            [Value(0, HelpText = "Game archive files input directory", MetaName = nameof(InputDirectory))]
            public string InputDirectory { get; set; }

            [Value(1, HelpText = "Path to save imported archive files", MetaName = nameof(OutputDirectory))]
            public string OutputDirectory { get; set; }

            [Value(2, HelpText = "Assets to replace in asset_number:asset_path format")]
            public IEnumerable<string> Replace { get; set; }
        }

#pragma warning restore CS8618

        private static int LocExport(LocExportOptions opt)
        {
            LocalizationFile loc = new();
            loc.LoadFile(opt.InputPath);

            var stringData = loc.ExtractStrings();
            var header = new string[] { "key", "source", "translation" };
            using var file = File.Create(opt.OutputPath);
            using var sw = new StreamWriter(file, Encoding.UTF8);

            Csv.CsvWriter.Write(sw, header, from x in stringData select new string[] { x.Key, x.Value, string.Empty });

            Console.WriteLine($"Exported {stringData.Count} strings to {opt.OutputPath}");

            return 0;
        }

        private static int LocImport(LocImportOptions opt)
        {
            Dictionary<string, string> translationData = new();

            using (var file = File.OpenRead(opt.TranslationPath))
            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                foreach (var line in Csv.CsvReader.Read(sr, new Csv.CsvOptions() { AllowNewLineInEnclosedFieldValues = true }))
                {
                    if (line.ColumnCount < 3) continue;
                    string key = line[0];
                    string translation = line[2];
                    
                    if (!string.IsNullOrEmpty(translation))
                    {
                        translationData[key] = translation;
                    }
                }
            }

            Console.WriteLine($"Loaded {translationData.Count} strings from {opt.TranslationPath}");

            LocalizationFile loc = new();
            loc.LoadFile(opt.InputPath);

            int importCount = loc.ImportStrings(translationData);

            loc.SaveFile(opt.OutputPath);

            Console.WriteLine($"Imported {importCount} strings and saved to {opt.OutputPath}");

            return 0;
        }

        private static int ArchiveExtract(ArchiveExtractOptions opt)
        {
            ArchiveDirectory dir = new(opt.InputDirectory);

            dir.ExtractFile(opt.AssetIndex, opt.OutputPath);

            Console.WriteLine($"Extracted asset {opt.AssetIndex} to {opt.OutputPath}");

            return 0;
        }

        private static int ArchiveExtractAll(ArchiveExtractAllOptions opt)
        {
            ArchiveDirectory dir = new(opt.InputDirectory);

            foreach (int index in dir.GetAllAssetIndex(opt.AssetName))
            {
                string path = Path.Combine(opt.OutputDirectory, $"{opt.AssetName}.{index}");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                dir.ExtractFile(index, path);
                Console.WriteLine($"Extracted {path}");
            }

            return 0;
        }

        private static int ArchiveImport(ArchiveImportOptions opt)
        {
            ArchiveDirectory dir = new(opt.InputDirectory);

            foreach (var replaceStr in opt.Replace)
            {
                string[] parts = replaceStr.Split(':');
                if (parts.Length != 2)
                {
                    Console.WriteLine($"Invalid replace argumet {replaceStr}");
                    Console.WriteLine("Aborting...");
                    return 1;
                }

                int assetIndex = int.Parse(parts[0]);
                string assetFilePath = parts[1];

                dir.ReplaceFile(assetIndex, new FilePathAssetReplacer(assetFilePath));

                Console.WriteLine($"Replaced asset {assetIndex} with {assetFilePath}");
            }

            dir.SaveArchives(opt.OutputDirectory);

            Console.WriteLine($"Saved to {opt.OutputDirectory}");

            return 0;
        }

        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<LocExportOptions, LocImportOptions, ArchiveExtractOptions, ArchiveExtractAllOptions, ArchiveImportOptions>(args)
                .MapResult(
                    (LocExportOptions opt) => LocExport(opt),
                    (LocImportOptions opt) => LocImport(opt),
                    (ArchiveExtractOptions opt) => ArchiveExtract(opt),
                    (ArchiveExtractAllOptions opt) => ArchiveExtractAll(opt),
                    (ArchiveImportOptions opt) => ArchiveImport(opt),
                    errs => 1
                );
        }
    }
}