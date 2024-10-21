using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LazyDataReader.Extensions;
using LazyDataReader.Readers;

namespace LazyDataReader
{
    public static class Reader
    {
        #region Public Methods

        public static T GetFromFile<T>(string path, bool replaceCommaInNumbers = false, Encoding encoding = default,
            string classNamespace = default, IEnumerable<string> additionalNamespaces = default,
            IEnumerable<string> removalPatterns = default)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                encoding: encoding,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces,
                removalPatterns: removalPatterns,
                replaceCommaInNumbers: replaceCommaInNumbers);
        }

        public static T GetFromFile<T>(string path, bool replaceCommaInNumbers = false, Encoding encoding = default,
            string classNamespace = default, string[] additionalNamespaces = default, params string[] removalPatterns)
            where T : class
        {
            return GetDataFromFile<T>(
                path: path,
                encoding: encoding,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces,
                removalPatterns: removalPatterns,
                replaceCommaInNumbers: replaceCommaInNumbers);
        }

        public static T GetFromText<T>(string text, bool replaceCommaInNumbers = false, string classNamespace = default,
            IEnumerable<string> additionalNamespaces = default, IEnumerable<string> removalPatterns = default)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces,
                removalPatterns: removalPatterns,
                replaceCommaInNumbers: replaceCommaInNumbers);
        }

        public static T GetFromText<T>(string text, bool replaceCommaInNumbers = false, string classNamespace = default,
            IEnumerable<string> additionalNamespaces = default, params string[] removalPatterns)
            where T : class
        {
            return GetDataFromText<T>(
                text: text,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces,
                removalPatterns: removalPatterns,
                replaceCommaInNumbers: replaceCommaInNumbers);
        }

        #endregion Public Methods

        #region Private Methods

        private static TextReader GetAdditionalReaders(this TextReader textReader, IEnumerable<string> removalPatterns,
            bool replaceCommaInNumbers)
        {
            var result = textReader;

            if (replaceCommaInNumbers)
            {
                result = new CommaReplaceReader(result);
            }

            if (removalPatterns?.Any(n => n.Length > 0) == true)
            {
                result = new AttributeRemoveReader(
                    textReader: result,
                    removalPatterns: removalPatterns);
            }

            return result;
        }

        private static T GetData<T>(this Func<TextReader> textReaderGetter, string classNamespace,
            IEnumerable<string> additionalNamespaces, IEnumerable<string> removalPatterns, bool replaceCommaInNumbers)
            where T : class
        {
            var result = default(T);

            var serializer = new XmlSerializer(typeof(T));

            using (var textReader = textReaderGetter.Invoke())

            using (var replaceReader = textReader.GetAdditionalReaders(
                removalPatterns: removalPatterns,
                replaceCommaInNumbers: replaceCommaInNumbers))

            using (var xmlReader = new NamespaceReplaceReader(
                reader: replaceReader,
                classNamespace: classNamespace,
                additionalNamespaces: additionalNamespaces))
            {
                result = serializer.Deserialize(xmlReader) as T;
            }

            return result;
        }

        private static T GetDataFromFile<T>(string path, Encoding encoding, string classNamespace,
            IEnumerable<string> additionalNamespaces, IEnumerable<string> removalPatterns, bool replaceCommaInNumbers)
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
                    classNamespace: classNamespace,
                    additionalNamespaces: additionalNamespaces,
                    removalPatterns: removalPatterns,
                    replaceCommaInNumbers: replaceCommaInNumbers);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(
                    message: $"The file '{path}' cannot be red.",
                    innerException: exception);
            }

            return result;
        }

        private static T GetDataFromText<T>(string text, string classNamespace,
            IEnumerable<string> additionalNamespaces, IEnumerable<string> removalPatterns, bool replaceCommaInNumbers)
            where T : class
        {
            var result = default(T);

            try
            {
                TextReader textReaderGetter() => new StringReader(text);

                result = GetData<T>(
                    textReaderGetter: textReaderGetter,
                    classNamespace: classNamespace,
                    additionalNamespaces: additionalNamespaces,
                    removalPatterns: removalPatterns, replaceCommaInNumbers: replaceCommaInNumbers);
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