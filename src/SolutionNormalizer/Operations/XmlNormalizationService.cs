using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Xsl;

namespace SolutionNormalizer.Operations
{
    public class XmlNormalizationService
    {
        public string SourceFolder { get; set; }

        public string DestinationFolder { get; set; }

        public string StyleSheetPath { get; set; }

        public void ProcessSolution()
        {
            var files = Directory.GetFiles(SourceFolder, "*", SearchOption.AllDirectories)
                .Where(x => x.EndsWith(".xml") || x.EndsWith(".xaml"));

            XslCompiledTransform xslt = new XslCompiledTransform(true);
            xslt.Load(StyleSheetPath);

            foreach (string inputFile in files)
            {
                string destinationFile = inputFile.Replace(SourceFolder, DestinationFolder);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFile));

                using (var wr = this.GetWriter(destinationFile))
                {
                    xslt.Transform(inputFile, null, wr);
                }
            }
        }

        public XmlWriter GetWriter(string outputFile)
        {

            //todo: factory to decide the type of xml writer
            //var writer = new XmlWriterExplicitFullEndElement(outputFile, System.Text.Encoding.UTF8);
            //writer.Formatting = Formatting.Indented;

            var writer = XmlWriter.Create(outputFile, new XmlWriterSettings()
            {
                Indent = true,
                ConformanceLevel = ConformanceLevel.Fragment
                
            });
            return writer;
        }
    }
}
