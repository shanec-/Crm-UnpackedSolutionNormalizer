using CommandLine;

namespace SolutionNormalizer
{
    public class CommandLineOptions
    {
        [Option('s', Required = true, HelpText = "Source zip file")]
        public string SourceZipFile { get; set; }

        [Option('w', HelpText = "Working folder")]
        public string WorkingFolder { get; set; }

        [Option('x', HelpText = "Stylesheet file")]
        public string StyleSheet { get; set; }
    }
}
