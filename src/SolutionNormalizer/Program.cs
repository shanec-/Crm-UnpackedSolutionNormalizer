using CommandLine;
using Serilog;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Linq;

namespace SolutionNormalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeLog();

            var options = Parser.Default.ParseArguments<CommandLineOptions>(args);
            options.WithParsed(x => ExecuteOperation(x.SourceZipFile, x.WorkingFolder));
        }

        public static void ExecuteOperation(string sourceZipFile, string workingFolder)
        {
            Log.Debug("Source File: {@sourceFile}, Working Folder: {@workingFolder}", sourceZipFile, workingFolder);

            if (string.IsNullOrEmpty(workingFolder))
            {
                var assembly = Assembly.GetExecutingAssembly();
                workingFolder = Path.GetDirectoryName(assembly.Location);
                Log.Debug("Setting new working folder {@workingFolder}", workingFolder);
            }

            string sourceFileNamePath = Path.GetFileName(sourceZipFile);
            string sourceFolder = Path.Combine(workingFolder, sourceFileNamePath + "-source");

            if (Directory.Exists(sourceFolder))
            {
                Directory.Delete(sourceFolder, true);
                Log.Warning("{@sourceFolder} exists, deleting...", sourceFolder);
            }
            Log.Information("Creating new folders {@sourceFolder}...", sourceFolder);
            Directory.CreateDirectory(sourceFolder);

            Log.Information("Extracting {@sourceZipFile} into {@sourceFolder}...", sourceZipFile, sourceFolder);
            ZipFile.ExtractToDirectory(sourceZipFile, sourceFolder);

            var stylesheetPaths = Directory.GetFiles(".", "*.xsl", SearchOption.AllDirectories).OrderBy(x => x);

            foreach (string styleSheetPath in stylesheetPaths)
            {
                string styleSheetName = Path.GetFileName(styleSheetPath);
                string transformedFolder = Path.Combine(workingFolder, sourceFileNamePath + styleSheetName + "-transformed");
                if (Directory.Exists(transformedFolder))
                {
                    Directory.Delete(transformedFolder, true);
                    Log.Warning("{@transformedFolder} exists, deleting...", transformedFolder);
                }

                Log.Information("Creating new folders {@transformedFolder}...", transformedFolder);
                Directory.CreateDirectory(transformedFolder);

                var normalizationService = new Operations.XmlNormalizationService()
                {
                    StyleSheetPath = styleSheetPath,
                    SourceFolder = sourceFolder,
                    DestinationFolder = transformedFolder
                };

                Log.Information("Attempting to process {@styleSheetName}...", styleSheetName);
                normalizationService.ProcessSolution();
                Log.Information("Processing completed {@styleSheetName}.", styleSheetName);

                sourceFolder = transformedFolder;
            }
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
