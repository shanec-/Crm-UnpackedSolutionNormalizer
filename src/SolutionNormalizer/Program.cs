﻿using CommandLine;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace SolutionNormalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<CommandLineOptions>(args);
            options.WithParsed(x => ExecuteOperation(x.SourceZipFile, x.WorkingFolder, x.StyleSheet));

        }

        public static void ExecuteOperation(string sourceFile, string workingFolder, string styleSheet)
        {
            if (string.IsNullOrEmpty(workingFolder))
            {
                var assembly = Assembly.GetExecutingAssembly();
                workingFolder = Path.GetDirectoryName(assembly.Location);
            }

            if (string.IsNullOrEmpty(styleSheet))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var rootFolder = Path.GetDirectoryName(assembly.Location);
                styleSheet = Path.Combine(rootFolder, "sort-stylesheet.xsl");
            }

            string sourceFileName = Path.GetFileName(sourceFile);
            string sourceFolder = Path.Combine(workingFolder, sourceFileName + "-source");
            string transformedFolder = Path.Combine(workingFolder, sourceFileName + "-tranformed");

            if (Directory.Exists(sourceFolder))
            {
                Directory.Delete(sourceFolder, true);
            }

            if (Directory.Exists(transformedFolder))
            {
                Directory.Delete(transformedFolder, true);
            }

            Directory.CreateDirectory(sourceFolder);
            Directory.CreateDirectory(transformedFolder);

            ZipFile.ExtractToDirectory(sourceFile, sourceFolder);

            var normalizationService = new Operations.XmlNormalizationService();
            normalizationService.StyleSheetPath = styleSheet;
            normalizationService.SourceFolder = sourceFolder;
            normalizationService.DestinationFolder = transformedFolder;

            normalizationService.ProcessSolution();
        }
    }
}
