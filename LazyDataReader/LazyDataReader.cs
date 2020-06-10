using LazyDataReader.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace LazyDataReader
{
    public static class Reader
    {
        #region Public Methods

        public static T GetData<T>(string path, string rootElement = default)
            where T : class
        {
            var result = ReadData<T>(
                path: path,
                rootElement: rootElement);

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static XmlRootAttribute GetRoot(string rootElement)
        {
            var result = new XmlRootAttribute
            {
                ElementName = rootElement,
                IsNullable = true,
            };
            // result.Namespace = "http://www.cpandl.com";

            return result;
        }

        private static XmlSerializer GetSerializer<T>()
            where T : class
        {
            var result = new XmlSerializer(
                type: typeof(T),
                defaultNamespace: string.Empty);

            return result;
        }

        private static XmlSerializer GetSerializer<T>(string rootElement)
            where T : class
        {
            var root = GetRoot(rootElement);

            var result = new XmlSerializer(
                type: typeof(T),
                root: root);

            return result;
        }

        private static T ReadData<T>(string path, string rootElement)
            where T : class
        {
            var result = default(T);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file '{path}' does not exist.");
            }

            try
            {
                var serializer = string.IsNullOrWhiteSpace(rootElement)
                    ? GetSerializer<T>()
                    : GetSerializer<T>(rootElement);

                using (StreamReader reader = new StreamReader(path))
                {
                    using (var namespaceIgnorantReader = new NamespaceIgnorantReader(reader))
                    {
                        result = (T)serializer.Deserialize(namespaceIgnorantReader);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The file '{path}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        #endregion Private Methods
    }
}