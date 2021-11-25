using System.Linq;
using System.Text;
using UtfUnknown;

namespace LazyDataReader.Extensions
{
    internal static class FileExtensions
    {
        #region Public Methods

        public static Encoding GetEncoding(this string path)
        {
            var detection = CharsetDetector.DetectFromFile(path);

            var detected = detection.Detected
                ?? detection.Details
                    .OrderByDescending(d => d.Encoding != default)
                    .ThenByDescending(d => d.Confidence).FirstOrDefault();

            var result = detected?.Encoding
                ?? Encoding.Default;

            return result;
        }

        #endregion Public Methods
    }
}