using LazyDataReader.Readers;
using System;
using System.IO;
using System.Xml.Serialization;

namespace LazyDataReader
{
    public static class Reader
    {
        #region Public Methods

        public static T GetData<T>(string path, string classNamespaceUri)
            where T : class
        {
            return GetData<T>(
                path: path,
                classNamespaceUri: classNamespaceUri,
                fileNamespaceUri: default);
        }

        public static T GetData<T>(string path, string classNamespaceUri, string fileNamespaceUri)
            where T : class
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file '{path}' does not exist.");
            }

            var result = ReadData<T>(
                path: path,
                fileNamespaceUri: fileNamespaceUri,
                classNamespaceUri: classNamespaceUri);

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static T ReadData<T>(string path, string classNamespaceUri, string fileNamespaceUri)
            where T : class
        {
            var result = default(T);

            try
            {
                var serializer = new XmlSerializer(typeof(T));

                using (var textReader = new StreamReader(path))
                {
                    using (var xmlReader = new NamespaceReplaceReader(
                        reader: textReader,
                        fileNamespaceUri: fileNamespaceUri,
                        classNamespaceUri: classNamespaceUri))
                    {
                        result = serializer.Deserialize(xmlReader) as T;
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