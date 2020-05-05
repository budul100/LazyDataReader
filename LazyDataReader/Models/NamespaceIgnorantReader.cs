using System.IO;
using System.Xml;

namespace LazyDataReader.Models
{
    // Helper class to ignore namespaces when de-serializing
    internal class NamespaceIgnorantReader
        : XmlTextReader
    {
        #region Public Constructors

        public NamespaceIgnorantReader(TextReader reader)
            : base(reader)
        { }

        #endregion Public Constructors

        #region Public Properties

        public override string NamespaceURI => string.Empty;

        #endregion Public Properties
    }
}