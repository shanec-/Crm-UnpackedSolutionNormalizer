using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SolutionNormalizer.Operations
{
    /// <summary>
    /// Xml Writer Explicit Full End Element
    /// </summary>
    /// <seealso cref="System.Xml.XmlTextWriter" />
    public class XmlWriterExplicitFullEndElement : XmlTextWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriterExplicitFullEndElement"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="encoding">The encoding.</param>
        public XmlWriterExplicitFullEndElement(string fileName, Encoding encoding) : base(fileName, encoding)
        {
        }

        /// <summary>
        /// Closes one element and pops the corresponding namespace scope.
        /// </summary>
        public override void WriteEndElement()
        {
            base.WriteFullEndElement();
        }

        /// <summary>
        /// Asynchronously closes one element and pops the corresponding namespace scope.
        /// </summary>
        /// <returns>
        /// The task that represents the asynchronous WriteEndElement operation.
        /// </returns>
        public override Task WriteEndElementAsync()
        {
            return base.WriteFullEndElementAsync();
        }
    }
}
