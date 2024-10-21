using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LazyDataReader.Extensions;

namespace LazyDataReader.Readers
{
    internal class AttributeRemoveReader
        : TextReader
    {
        #region Private Fields

        private const int CharTagClose = 62;
        private const int CharTagOpen = 60;
        private const int EndOfFile = -1;

        private readonly IEnumerable<Regex> regexs;
        private readonly TextReader textReader;

        private Queue<int> characters;
        private int? last;

        #endregion Private Fields

        #region Public Constructors

        public AttributeRemoveReader(TextReader textReader, IEnumerable<string> removalPatterns)
        {
            this.textReader = textReader;

            this.regexs = GetRegexs(removalPatterns);
        }

        #endregion Public Constructors

        #region Public Methods

        public override int Read()
        {
            if (!(characters?.Count > 0))
            {
                characters = new Queue<int>(GetCharacters());
            }

            var result = characters.Dequeue();

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private IEnumerable<int> GetCharacters()
        {
            var stream = GetStream().ToArray();

            if (stream?.Length > 0
                && stream[0] == CharTagOpen
                && regexs?.Count() > 0)
            {
                var text = stream.GetString();

                foreach (var regex in regexs)
                {
                    text = regex.Replace(
                        input: text,
                        replacement: string.Empty);
                }

                stream = text
                    .Select(c => (int)c).ToArray();
            }

            return stream;
        }

        private IEnumerable<Regex> GetRegexs(IEnumerable<string> removalPatterns)
        {
            var relevants = removalPatterns?
                .Where(n => n.Trim().Length > 0).ToArray();

            if (relevants?.Count() > 0)
            {
                foreach (var relevant in relevants)
                {
                    var result = new Regex(relevant);

                    yield return result;
                }
            }
        }

        private IEnumerable<int> GetStream()
        {
            while (true)
            {
                var isWithoutLast = !last.HasValue;

                if (last.HasValue)
                {
                    yield return last.Value;
                }

                var result = textReader.Read();

                if (result == CharTagClose
                    || result == EndOfFile)
                {
                    last = default;
                    yield return result;
                }
                else
                {
                    last = result;
                }

                if (result == CharTagClose
                    || result == EndOfFile
                    || (!isWithoutLast && result == CharTagOpen))
                {
                    yield break;
                }
            }
        }

        #endregion Private Methods
    }
}