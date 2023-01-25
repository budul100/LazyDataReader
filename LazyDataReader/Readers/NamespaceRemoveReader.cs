using System.IO;

namespace LazyDataReader.Readers
{
    internal class NamespaceRemoveReader
        : TextReader
    {
        #region Private Fields

        private readonly TextReader textReader;
        private int? character1;
        private int? character2;
        private int? character3;
        private bool isNamespace;
        private bool isOnEnd;

        #endregion Private Fields

        #region Public Constructors

        public NamespaceRemoveReader(TextReader textReader)
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

            if (isNamespace)
            {
                if ((character1.Value != 58 && !isOnEnd)
                    || (character2.Value != 58 && isOnEnd))
                {
                    character1 = 32;
                }
                else
                {
                    isNamespace = false;

                    character1 = 60;
                    if (isOnEnd)
                    {
                        character2 = 47;
                    }
                }
            }
            else if (character1 == 60
                && character2 != 63)
            {
                isOnEnd = character2 == 47;
                isNamespace = true;

                character1 = 32;
                if (isOnEnd)
                {
                    character2 = 32;
                }
            }

            return character1.Value;
        }

        #endregion Public Methods
    }
}