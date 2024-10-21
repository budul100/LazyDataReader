using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyDataReader.Extensions
{
    internal static class ContentExtensions
    {
        #region Public Methods

        public static string GetString(this IEnumerable<int> stream)
        {
            var result = new StringBuilder();

            if (stream?.Count() > 0)
            {
                foreach (var character in stream)
                {
                    result.Append((char)character);
                }
            }

            return result.ToString();
        }

        #endregion Public Methods
    }
}