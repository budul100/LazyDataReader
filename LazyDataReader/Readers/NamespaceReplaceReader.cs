using System.IO;
using System.Xml;

namespace LazyDataReader.Readers
{
    public class NamespaceReplaceReader
        : XmlTextReader
    {
        #region Private Fields

        private readonly string classNamespaceUri;
        private readonly string fileNamespaceUri;

        #endregion Private Fields

        #region Public Constructors

        public NamespaceReplaceReader(TextReader reader, string fileNamespaceUri, string classNamespaceUri)
            : base(reader)
        {
            this.classNamespaceUri = classNamespaceUri ?? string.Empty;
            this.fileNamespaceUri = fileNamespaceUri ?? string.Empty;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string NamespaceURI => GetNamespaceUri();

        #endregion Public Properties

        #region Private Methods

        private string GetNamespaceUri()
        {
            if (NodeType != XmlNodeType.Attribute
                && (fileNamespaceUri == default
                || base.NamespaceURI == fileNamespaceUri))
            {
                return classNamespaceUri;
            }
            else
            {
                return base.NamespaceURI;
            }
        }

        #endregion Private Methods
    }
}