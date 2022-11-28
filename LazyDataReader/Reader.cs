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

        public static T GetFromFile<T>(string path, bool replaceCommaInNumbers = false,
            string classNamespace = default, IEnumerable<string> additionalNamespaces = default)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                replaceCommaInNumbers: replaceCommaInNumbers,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces);
        }

        public static T GetFromFile<T>(string path, bool replaceCommaInNumbers = false,
            string classNamespace = default, params string[] additionalNamespaces)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                replaceCommaInNumbers: replaceCommaInNumbers,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces);
        }

        public static T GetFromText<T>(string text, bool replaceCommaInNumbers = false,
            string classNamespace = default, IEnumerable<string> additionalNamespaces = default)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                replaceCommaInNumbers: replaceCommaInNumbers,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces);
        }

        public static T GetFromText<T>(string text, bool replaceCommaInNumbers = false,
            string classNamespace = default, params string[] additionalNamespaces)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                replaceCommaInNumbers: replaceCommaInNumbers,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces);
        }

        #endregion Public Methods

        #region Private Methods

        private static T GetData<T>(Func<TextReader> textReaderGetter, bool replaceCommaInNumbers,
            string classNamespace, IEnumerable<string> additionalNamespaces)
            where T : class
        {
            var result = default(T);

            var serializer = new XmlSerializer(typeof(T));

            using (var textReader = textReaderGetter.Invoke())
            {
                using (var replaceReader = textReader.GetReplaceReader(
                    replaceCommaInNumbers: replaceCommaInNumbers))
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

        private static T GetDataFromFile<T>(string path, bool replaceCommaInNumbers,
            string classNamespace, IEnumerable<string> additionalNamespaces)
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
                    replaceCommaInNumbers: replaceCommaInNumbers,
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

        private static T GetDataFromText<T>(string text, bool replaceCommaInNumbers,
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

        private static TextReader GetReplaceReader(this TextReader textReader, bool replaceCommaInNumbers)
        {
            var result = replaceCommaInNumbers
                ? new CommaReplaceReader(textReader)
                : textReader;

            return result;
        }

        #endregion Private Methods
    }
}