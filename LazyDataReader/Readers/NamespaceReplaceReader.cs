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
            this.classNamespaceUri = classNamespaceUri;
            this.fileNamespaceUri = fileNamespaceUri;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string NamespaceURI => GetNamespaceUri();

        #endregion Public Properties

        #region Private Methods

        private string GetNamespaceUri()
        {
            if (string.IsNullOrWhiteSpace(classNamespaceUri))
            {
                return string.Empty;
            }
            if (base.NamespaceURI == (fileNamespaceUri ?? string.Empty))
            {
                return classNamespaceUri ?? string.Empty;
            }
            else
            {
                return base.NamespaceURI;
            }
        }

        #endregion Private Methods
    }
}