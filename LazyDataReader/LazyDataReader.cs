using LazyDataReader.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace LazyDataReader
{
    public static class Reader<T>
        where T : class
    {
        #region Public Methods

        public static T GetData(string path)
        {
            var result = ReadData(path);

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static T ReadData(string path)
        {
            var result = default(T);

            if (!File.Exists(path))
                throw new FileNotFoundException($"The file '{path}' does not exist.");

            try
            {
                var serializer = new XmlSerializer(
                    type: typeof(T),
                    defaultNamespace: string.Empty);

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