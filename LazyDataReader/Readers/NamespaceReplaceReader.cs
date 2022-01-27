using System.IO;
using System.Xml;

namespace LazyDataReader.Readers
{
    public class NamespaceReplaceReader
        : XmlTextReader
    {
        #region Private Fields

        private readonly string namespaceUri;

        #endregion Private Fields

        #region Public Constructors

        public NamespaceReplaceReader(TextReader reader, string namespaceUri)
            : base(reader)
        {
            this.namespaceUri = namespaceUri;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string NamespaceURI => GetNamespaceUri();

        #endregion Public Properties

        #region Private Methods

        private string GetNamespaceUri()
        {
            return !string.IsNullOrWhiteSpace(namespaceUri)
                ? namespaceUri
                : string.Empty;
        }

        #endregion Private Methods
    }
}