using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SolutionNormalizer.Operations
{
    public class XmlWriterExplicitFullEndElement : XmlTextWriter
    {
        public XmlWriterExplicitFullEndElement(string fileName, Encoding encoding) : base(fileName, encoding)
        {
        }
        public override void WriteEndElement()
        {
            base.WriteFullEndElement();
        }

        public override Task WriteEndElementAsync()
        {
            return base.WriteFullEndElementAsync();
        }
    }
}
