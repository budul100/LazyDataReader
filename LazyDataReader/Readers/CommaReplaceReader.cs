using System.IO;

namespace LazyDataReader.Readers
{
    internal class CommaReplaceReader
        : TextReader
    {
        #region Private Fields

        private readonly TextReader textReader;

        private int? character1;
        private int? character2;
        private int? character3;
        private bool replaceComma;

        #endregion Private Fields

        #region Public Constructors

        public CommaReplaceReader(TextReader textReader)
        {
            this.textReader = textReader;
        }

        #endregion Public Constructors

        #region Public Methods

        public override int Read()
        {
            character1 = character2
                ?? textReader.Read();

            character2 = character3
                ?? textReader.Read();

            character3 = textReader.Read();

            if (replaceComma
                && character1 == 44)
            {
                character1 = 46;
                replaceComma = false;
            }
            else
            {
                replaceComma = (character2 == 44)
                    && (character1 >= 48 && character1 <= 57)
                    && (character3 >= 48 && character3 <= 57);
            }

            return character1.Value;
        }

        #endregion Public Methods
    }
}