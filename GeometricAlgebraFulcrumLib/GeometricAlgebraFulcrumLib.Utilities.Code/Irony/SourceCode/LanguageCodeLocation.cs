using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;

public sealed class LanguageCodeLocation
{
    public ISourceCodeUnit CodeUnit { get; }

    public int CharacterNumber { get; }
        
    public int LineNumber { get; }
        
    public int ColumnNumber { get; }

    public int SpanLength { get; set; }


    public int OneBasedLineNumber => 1 + LineNumber;

    public int OneBasedColumnNumber => 1 + ColumnNumber;


    internal LanguageCodeLocation(ISourceCodeUnit codeUnit, int charNum, int lineNum, int colNum)
    {
        CodeUnit = codeUnit;
        CharacterNumber = charNum;
        LineNumber = lineNum;
        ColumnNumber = colNum;
        SpanLength = 1;
    }

    internal LanguageCodeLocation(ISourceCodeUnit codeUnit, int charNum, int lineNum, int colNum, int spanLength)
    {
        CodeUnit = codeUnit;
        CharacterNumber = charNum;
        LineNumber = lineNum;
        ColumnNumber = colNum;
        SpanLength = spanLength;
    }


    public string LineColumnDescription()
    {
        var s = new StringBuilder();

        s.Append("line: ");
        s.Append(OneBasedLineNumber);
        s.Append(", column: ");
        s.Append(OneBasedColumnNumber);

        return s.ToString();
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        s.Append("file: <")
            .Append(CodeUnit.FilePath)
            .Append("> character: ")
            .Append(CharacterNumber)
            .Append(", span: ")
            .Append(SpanLength)
            .Append(", line: ")
            .Append(OneBasedLineNumber)
            .Append(", column: ")
            .Append(OneBasedColumnNumber);

        return s.ToString();
    }
}