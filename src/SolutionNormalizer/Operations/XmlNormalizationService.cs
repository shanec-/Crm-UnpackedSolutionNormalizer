using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var files = Directory.GetFiles(SourceFolder, "*.xml", SearchOption.AllDirectories);

            XslCompiledTransform xslt = new XslCompiledTransform(true);
            xslt.Load(StyleSheetPath);

            foreach (string inputFile in files)
            {
                string outputFile = Path.Combine(DestinationFolder, Path.GetFileName(inputFile));

                using (var wr = this.GetWriter(outputFile))
                {
                    xslt.Transform(inputFile, null, wr);
                }
            }
        }

        public XmlWriter GetWriter(string outputFile)
        {
            var writer = new XmlWriterExplicitFullEndElement(outputFile, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            return writer;
        }
    }
}
