using LazyDataReader.Extensions;
using LazyDataReader.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LazyDataReader
{
    public static class Reader
    {
        #region Public Methods

        public static T GetFromFile<T>(string path, bool replaceCommaInNumbers = false, bool removeNamespaces = false,
            Encoding encoding = default, string classNamespace = default,
            IEnumerable<string> additionalNamespaces = default)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                replaceCommaInNumbers: replaceCommaInNumbers,
                removeNamespaces: removeNamespaces,
                encoding: encoding,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces);
        }

        public static T GetFromFile<T>(string path, bool replaceCommaInNumbers = false, bool removeNamespaces = false,
            Encoding encoding = default, string classNamespace = default, params string[] additionalNamespaces)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                replaceCommaInNumbers: replaceCommaInNumbers,
                removeNamespaces: removeNamespaces,
                encoding: encoding,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces);
        }

        public static T GetFromText<T>(string text, bool replaceCommaInNumbers = false, bool removeNamespaces = false,
            string classNamespace = default, IEnumerable<string> additionalNamespaces = default)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                replaceCommaInNumbers: replaceCommaInNumbers,
                removeNamespaces: removeNamespaces,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces);
        }

        public static T GetFromText<T>(string text, bool replaceCommaInNumbers = false, bool removeNamespaces = false,
            string classNamespace = default, params string[] additionalNamespaces)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                replaceCommaInNumbers: replaceCommaInNumbers,
                classNamespace: classNamespace,
                removeNamespaces: removeNamespaces,
                additionalNamespaces: additionalNamespaces);
        }

        #endregion Public Methods

        #region Private Methods

        private static TextReader GetAdditionalReaders(this TextReader textReader, bool replaceCommaInNumbers,
            bool removeNamespaces)
        {
            var result = textReader;

            if (replaceCommaInNumbers)
            {
                result = new CommaReplaceReader(result);
            }

            if (removeNamespaces)
            {
                result = new NamespaceRemoveReader(result);
            }

            return result;
        }

        private static T GetData<T>(this Func<TextReader> textReaderGetter, bool replaceCommaInNumbers,
            bool removeNamespaces, string classNamespace, IEnumerable<string> additionalNamespaces)
            where T : class
        {
            var result = default(T);

            var serializer = new XmlSerializer(typeof(T));

            using (var textReader = textReaderGetter.Invoke())
            {
                using (var replaceReader = textReader.GetAdditionalReaders(
                    replaceCommaInNumbers: replaceCommaInNumbers,
                    removeNamespaces: removeNamespaces))
                {
                    using (var xmlReader = new NamespaceReplaceReader(
                        reader: replaceReader,
                        classNamespace: classNamespace,
                        additionalNamespaces: additionalNamespaces))
                    {
                        result = serializer.Deserialize(xmlReader) as T;
                    }
                }
            }

            return result;
        }

        private static T GetDataFromFile<T>(string path, bool replaceCommaInNumbers, bool removeNamespaces,
            Encoding encoding, string classNamespace, IEnumerable<string> additionalNamespaces)
            where T : class
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file '{path}' does not exist.");
            }

            var result = default(T);

            try
            {
                encoding = encoding
                    ?? path.GetEncoding();

                TextReader textReaderGetter() => new StreamReader(
                    path: path,
                    encoding: encoding);

                result = GetData<T>(
                    textReaderGetter: textReaderGetter,
                    replaceCommaInNumbers: replaceCommaInNumbers,
                    removeNamespaces: removeNamespaces,
                    classNamespace: classNamespace,
                    additionalNamespaces: additionalNamespaces);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The file '{path}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        private static T GetDataFromText<T>(string text, bool replaceCommaInNumbers, bool removeNamespaces,
            string classNamespace, IEnumerable<string> additionalNamespaces)
            where T : class
        {
            var result = default(T);

            try
            {
                TextReader textReaderGetter() => new StringReader(text);

                result = GetData<T>(
                    textReaderGetter: textReaderGetter,
                    replaceCommaInNumbers: replaceCommaInNumbers,
                    removeNamespaces: removeNamespaces,
                    classNamespace: classNamespace,
                    additionalNamespaces: additionalNamespaces);
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