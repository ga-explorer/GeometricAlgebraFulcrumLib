namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;

public sealed class LanguageCodeTextLine : ISourceCodeUnitLine
{
    public LanguageCodeText ParentTextCodeUnit { get; }

    public ISourceCodeUnit ParentCodeUnit => ParentTextCodeUnit;

    public int LineNumber { get; }

    public int FirstCharacterPosition { get; }

    public int LastCharacterPosition => FirstCharacterPosition + CharactersCount - 1;

    public int CharactersCount { get; }

    public string LineCodeText => ParentTextCodeUnit.CodeText.Substring(FirstCharacterPosition, CharactersCount);


    internal LanguageCodeTextLine(LanguageCodeText parentTextSourceCode, int lineNumber, int firstCharPos, int charCount)
    {
        ParentTextCodeUnit = parentTextSourceCode;

        LineNumber = lineNumber;

        FirstCharacterPosition = firstCharPos;
            
        CharactersCount = charCount;
    }


    public override string ToString()
    {
        return LineCodeText;
    }
}