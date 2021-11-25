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

        public static T GetFromFile<T>(string path, string classNamespaceUri = default)
            where T : class
        {
            return GetFromFile<T>(
                path: path,
                classNamespaceUri: classNamespaceUri,
                fileNamespaceUri: default);
        }

        public static T GetFromFile<T>(string path, string fileNamespaceUri, string classNamespaceUri)
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
                    fileNamespaceUri: fileNamespaceUri,
                    classNamespaceUri: classNamespaceUri);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The file '{path}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        public static T GetFromText<T>(string text, string classNamespaceUri = default)
            where T : class
        {
            return GetFromText<T>(
                text: text,
                classNamespaceUri: classNamespaceUri,
                fileNamespaceUri: default);
        }

        public static T GetFromText<T>(string text, string fileNamespaceUri, string classNamespaceUri)
            where T : class
        {
            var result = default(T);

            try
            {
                result = GetData<T>(
                    textReaderGetter: () => new StringReader(text),
                    fileNamespaceUri: fileNamespaceUri,
                    classNamespaceUri: classNamespaceUri);
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

        private static T GetData<T>(Func<TextReader> textReaderGetter, string fileNamespaceUri, string classNamespaceUri)
            where T : class
        {
            var result = default(T);

            var serializer = new XmlSerializer(typeof(T));

            using (var textReader = textReaderGetter.Invoke())
            {
                using (var xmlReader = new NamespaceReplaceReader(
                    reader: textReader,
                    fileNamespaceUri: fileNamespaceUri,
                    classNamespaceUri: classNamespaceUri))
                {
                    result = serializer.Deserialize(xmlReader) as T;
                }
            }

            return result;
        }

        #endregion Private Methods
    }
}