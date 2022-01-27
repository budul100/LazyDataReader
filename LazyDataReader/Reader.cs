using LazyDataReader.Extensions;
using LazyDataReader.Readers;
using System;
using System.IO;
using System.Xml.Serialization;

namespace LazyDataReader
{
    public static class Reader
    {
        #region Public Methods

        public static T GetFromFile<T>(string path, string namespaceUri = default)
            where T : class
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file '{path}' does not exist.");
            }

            var result = default(T);

            try
            {
                var encoding = path.GetEncoding();

                TextReader textReaderGetter() => new StreamReader(
                    path: path,
                    encoding: encoding);

                result = GetData<T>(
                    textReaderGetter: textReaderGetter,
                    namespaceUri: namespaceUri);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The file '{path}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        public static T GetFromText<T>(string text, string namespaceUri = default)
            where T : class
        {
            var result = default(T);

            try
            {
                result = GetData<T>(
                    textReaderGetter: () => new StringReader(text),
                    namespaceUri: namespaceUri);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The text '{text}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static T GetData<T>(Func<TextReader> textReaderGetter, string namespaceUri)
            where T : class
        {
            var result = default(T);

            var serializer = new XmlSerializer(typeof(T));

            using (var textReader = textReaderGetter.Invoke())
            {
                using (var xmlReader = new NamespaceReplaceReader(
                    reader: textReader,
                    namespaceUri: namespaceUri))
                {
                    result = serializer.Deserialize(xmlReader) as T;
                }
            }

            return result;
        }

        #endregion Private Methods
    }
}