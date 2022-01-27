using LazyDataReader.Extensions;
using LazyDataReader.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LazyDataReader
{
    public static class Reader
    {
        #region Public Methods

        public static T GetFromFile<T>(string path, string classNamespace = default,
            IEnumerable<string> acceptedNamespaces = default)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                classNamespace: classNamespace,
                acceptedNamespaces: acceptedNamespaces);
        }

        public static T GetFromFile<T>(string path, string classNamespace = default,
            params string[] acceptedNamespaces)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                classNamespace: classNamespace,
                acceptedNamespaces: acceptedNamespaces);
        }

        public static T GetFromText<T>(string text, string classNamespace = default,
            IEnumerable<string> acceptedNamespaces = default)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                classNamespace: classNamespace,
                acceptedNamespaces: acceptedNamespaces);
        }

        public static T GetFromText<T>(string text, string classNamespace = default,
            params string[] acceptedNamespaces)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                classNamespace: classNamespace,
                acceptedNamespaces: acceptedNamespaces);
        }

        #endregion Public Methods

        #region Private Methods

        private static T GetData<T>(Func<TextReader> textReaderGetter, string classNamespace,
            IEnumerable<string> acceptedNamespaces)
            where T : class
        {
            var result = default(T);

            var serializer = new XmlSerializer(typeof(T));

            using (var textReader = textReaderGetter.Invoke())
            {
                using (var xmlReader = new NamespaceReplaceReader(
                    reader: textReader,
                    classNamespace: classNamespace,
                    acceptedNamespaces: acceptedNamespaces))
                {
                    result = serializer.Deserialize(xmlReader) as T;
                }
            }

            return result;
        }

        private static T GetDataFromFile<T>(string path, string classNamespace = default,
            IEnumerable<string> acceptedNamespaces = default)
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
                    classNamespace: classNamespace,
                    acceptedNamespaces: acceptedNamespaces);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The file '{path}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        private static T GetDataFromText<T>(string text, string classNamespace = default,
            IEnumerable<string> acceptedNamespaces = default)
            where T : class
        {
            var result = default(T);

            try
            {
                result = GetData<T>(
                    textReaderGetter: () => new StringReader(text),
                    classNamespace: classNamespace,
                    acceptedNamespaces: acceptedNamespaces);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The text '{text}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        #endregion Private Methods
    }
}