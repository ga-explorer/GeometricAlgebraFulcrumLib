using System;

namespace CodeComposerLib.Irony.SourceCode
{
    [Serializable]
    public sealed class LanguageCodeFileLine : ISourceCodeUnitLine
    {
        public LanguageCodeFile ParentCodeFileUnit { get; }

        public ISourceCodeUnit ParentCodeUnit => ParentCodeFileUnit;

        public int LineNumber { get; }

        public int FirstCharacterPosition { get; }

        public int LastCharacterPosition => FirstCharacterPosition + CharactersCount - 1;

        public int CharactersCount { get; }

        public string LineCodeText => ParentCodeFileUnit.CodeText.Substring(FirstCharacterPosition, CharactersCount);


        internal LanguageCodeFileLine(LanguageCodeFile parentFile, int lineNumber, int firstCharPos, int charCount)
        {
            ParentCodeFileUnit = parentFile;

            LineNumber = lineNumber;

            FirstCharacterPosition = firstCharPos;

            CharactersCount = charCount;
        }


        public override string ToString()
        {
            return LineCodeText;
        }
    }
}
