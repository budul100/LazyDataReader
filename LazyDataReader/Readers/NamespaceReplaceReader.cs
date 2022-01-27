using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace LazyDataReader.Readers
{
    public class NamespaceReplaceReader
        : XmlTextReader
    {
        #region Private Fields

        private readonly IEnumerable<string> acceptedNamespaces;
        private readonly string classNamespace;

        #endregion Private Fields

        #region Public Constructors

        public NamespaceReplaceReader(TextReader reader, string classNamespace, IEnumerable<string> acceptedNamespaces)
            : base(reader)
        {
            this.classNamespace = classNamespace;
            this.acceptedNamespaces = acceptedNamespaces;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string BaseURI => classNamespace;

        public override string NamespaceURI => GetNamespaceUri();

        #endregion Public Properties

        #region Private Methods

        private string GetNamespaceUri()
        {
            if (acceptedNamespaces?.Contains(base.NamespaceURI) ?? false)
            {
                return base.NamespaceURI;
            }
            else if (NodeType != XmlNodeType.Attribute
                && !string.IsNullOrWhiteSpace(classNamespace))
            {
                return classNamespace;
            }

            return string.Empty;
        }

        #endregion Private Methods
    }
}