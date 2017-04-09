using CommandLine;
using Serilog;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace SolutionNormalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeLog();

            var options = Parser.Default.ParseArguments<CommandLineOptions>(args);
            options.WithParsed(x => ExecuteOperation(x.SourceZipFile, x.WorkingFolder, x.StyleSheet));
        }

        public static void ExecuteOperation(string sourceFile, string workingFolder, string styleSheet)
        {
            Log.Debug("Source File: {@sourceFile}, Working Folder: {@workingFolder}, StyleSheet {@styleSheet}", sourceFile, workingFolder, styleSheet);

            if (string.IsNullOrEmpty(workingFolder))
            {
                var assembly = Assembly.GetExecutingAssembly();
                workingFolder = Path.GetDirectoryName(assembly.Location);
                Log.Debug("Setting new working folder {@workingFolder}", workingFolder);
            }

            if (string.IsNullOrEmpty(styleSheet))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var rootFolder = Path.GetDirectoryName(assembly.Location);
                styleSheet = Path.Combine(rootFolder, "sort-stylesheet.xsl");
                Log.Debug("Setting new working folder {@workingFolder}", styleSheet);
            }

            string sourceFileName = Path.GetFileName(sourceFile);
            string sourceFolder = Path.Combine(workingFolder, sourceFileName + "-source");
            string transformedFolder = Path.Combine(workingFolder, sourceFileName + "-transformed");

            if (Directory.Exists(sourceFolder))
            {
                Directory.Delete(sourceFolder, true);
                Log.Warning("{@sourceFolder} exists, deleting...", sourceFolder);
            }

            if (Directory.Exists(transformedFolder))
            {
                Directory.Delete(transformedFolder, true);
                Log.Warning("{@transformedFolder} exists, deleting...", transformedFolder);
            }

            Log.Information("Creating new folders {@sourceFolder}...", sourceFolder);
            Log.Information("Creating new folders {@transformedFolder}...", transformedFolder);
            Directory.CreateDirectory(sourceFolder);
            Directory.CreateDirectory(transformedFolder);

#if DEBUG
            System.Diagnostics.Debugger.Launch();
#endif

            Log.Information("Extracing {@sourceFile} into {@sourceFolder}...", sourceFile, sourceFolder);
            ZipFile.ExtractToDirectory(sourceFile, sourceFolder);

            var normalizationService = new Operations.XmlNormalizationService();
            normalizationService.StyleSheetPath = styleSheet;
            normalizationService.SourceFolder = sourceFolder;
            normalizationService.DestinationFolder = transformedFolder;

            Log.Information("Attempting to process folder...");
            normalizationService.ProcessSolution();
            Log.Information("Completed Successfully!");
        }

        private static void InitializeLog()
        {
            var loggerConfiguration =
                new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .CreateLogger();

            Log.Logger = loggerConfiguration;
        }
    }
}
