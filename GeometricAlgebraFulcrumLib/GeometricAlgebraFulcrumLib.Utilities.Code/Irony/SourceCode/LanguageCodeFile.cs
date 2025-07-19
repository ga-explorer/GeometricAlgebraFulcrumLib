using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;

[Serializable]
public sealed class LanguageCodeFile : ISourceCodeUnit
{
    private Encoding _textEncoding = Encoding.Unicode;


    public LanguageCodeProject ParentProject { get; }

    public string FileRelativePath { get; }

    public Encoding TextEncoding
    {
        get { return _textEncoding; }
        private set { _textEncoding = value ?? Encoding.Unicode; }
    }

    public string FilePath => Path.GetFullPath(Path.Combine(ParentProject.ProjectFolderPath, FileRelativePath));

    public bool FileExists => File.Exists(FilePath);

    public int CharactersCount => CodeText.Length;

    public int LinesCount => _sourceCodeLines.Count;

    public string CodeText { get; private set; }

    public bool IsFile => true;

    public bool IsText => false;

    public IEnumerable<ISourceCodeUnitLine> CodeUnitLines => _sourceCodeLines;

    public IEnumerable<LanguageCodeFileLine> FileLines => _sourceCodeLines;


    private readonly List<LanguageCodeFileLine> _sourceCodeLines = new List<LanguageCodeFileLine>();


    internal LanguageCodeFile(LanguageCodeProject parentProject, string filePath)
    {
        ParentProject = parentProject;
        FileRelativePath = filePath;
        TextEncoding = Encoding.Unicode;
    }

    internal LanguageCodeFile(LanguageCodeProject parentProject, string filePath, Encoding textEncoding)
    {
        ParentProject = parentProject;
        FileRelativePath = filePath;
        TextEncoding = textEncoding;
    }


    public FileInfo GetFileInfo()
    {
        return new FileInfo(FilePath);
    }

    public DateTime LastWriteTime => GetFileInfo().LastWriteTime;

    /// <summary>
    /// Remove all source code lines information from this DSL file object
    /// </summary>
    public void ClearSourceCodeLines()
    {
        CodeText = string.Empty;
        _sourceCodeLines.Clear();
    }

    /// <summary>
    /// Read all source code lines from the disk file associated with this DSL file object
    /// </summary>
    /// <returns></returns>
    internal string ReadSourceCodeTextFromFile()
    {
        var lines = File.ReadAllLines(FilePath, TextEncoding);

        _sourceCodeLines.Clear();

        var startPos = 0;

        var lineNumber = 0;

        var s = new StringBuilder();

        foreach (var lineText in lines)
        {
            s.AppendLine(lineText);

            var charCount = lineText.Length + Environment.NewLine.Length;

            var newLine = new LanguageCodeFileLine(this, lineNumber, startPos, charCount);

            _sourceCodeLines.Add(newLine);

            startPos = newLine.LastCharacterPosition + 1;

            lineNumber++;
        }

        CodeText = s.ToString();

        return CodeText;
    }

    public void TranslateCharacterLocation(int lineNumber, int columnNumber, out int absolutePos)
    {
        absolutePos = 0;

        var line = _sourceCodeLines[lineNumber];

        if (columnNumber < 0 || columnNumber >= line.CharactersCount)
            throw new IndexOutOfRangeException();

        for (var lineIndex = 0; lineIndex < lineNumber; lineIndex++)
            absolutePos += _sourceCodeLines[lineNumber].CharactersCount;

        absolutePos += columnNumber;
    }

    public void TranslateCharacterLocation(int absolutePos, out int lineNumber, out int columnNumber)
    {
        if (absolutePos < 0 || absolutePos >= CharactersCount)
            throw new IndexOutOfRangeException();

        lineNumber = 0;
        columnNumber = absolutePos;

        while (columnNumber >= _sourceCodeLines[lineNumber].CharactersCount)
        {
            columnNumber -= _sourceCodeLines[lineNumber].CharactersCount;
            lineNumber++;
        }
    }

    public LanguageCodeLocation TranslateCharacterLocation(int absolutePos)
    {
        TranslateCharacterLocation(absolutePos, out var lineNumber, out var columnNumber);

        return new LanguageCodeLocation(this, absolutePos, lineNumber, columnNumber);
    }

    public LanguageCodeLocation TranslateCharacterLocation(int lineNumber, int columnNumber)
    {
        TranslateCharacterLocation(lineNumber, columnNumber, out var absolutePos);

        return new LanguageCodeLocation(this, absolutePos, lineNumber, columnNumber);
    }


    public override string ToString()
    {
        return FileRelativePath;
    }
}