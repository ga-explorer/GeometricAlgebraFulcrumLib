using System;
using System.Collections.Generic;
using System.Text;
using TextComposerLib;

namespace CodeComposerLib.Irony.SourceCode
{
    public sealed class LanguageCodeText : ISourceCodeUnitsContainer, ISourceCodeUnit
    {
        private Encoding _textEncoding = Encoding.Unicode;

        private readonly List<LanguageCodeTextLine> _sourceCodeLines = new List<LanguageCodeTextLine>();


        public string CodeTitle { get; }

        public bool IsProject => false;

        public bool IsText => true;

        public string CodeText { get; }

        public Encoding TextEncoding
        {
            get { return _textEncoding; }
            private set { _textEncoding = value ?? Encoding.Unicode; }
        }

        public List<string> SourceCodeUnitsText => ContainsSourceCodeUnits 
            ? new List<string> { CodeText } 
            : new List<string>();

        public IEnumerable<ISourceCodeUnit> SourceCodeUnits
        {
            get
            {
                if (ContainsSourceCodeUnits)
                    yield return this;
            }
        }

        public bool ContainsSourceCodeUnits => string.IsNullOrEmpty(CodeText);

        public ISourceCodeUnit ActiveCodeUnit { get { return this; } set { } }

        public bool IsFile => false;

        public string FilePath => CodeTitle ?? string.Empty;

        public int CharactersCount => CodeText.Length;

        public int LinesCount => _sourceCodeLines.Count;


        public IEnumerable<ISourceCodeUnitLine> CodeUnitLines => _sourceCodeLines;

        public IEnumerable<LanguageCodeTextLine> TextLines => _sourceCodeLines;

        public bool RequiresSourceCodeTextUpdate => false;


        public LanguageCodeText(string codeTitle, string codeText)
        {
            CodeTitle =
                codeTitle ?? string.Empty;

            CodeText = 
                ReferenceEquals(codeText, null) 
                ? string.Empty 
                : ReadSourceCodeLinesFromText(codeText);
        }

        public LanguageCodeText(string codeTitle, string codeText, Encoding textEncoding)
        {
            CodeTitle =
                codeTitle ?? string.Empty;

            CodeText =
                ReferenceEquals(codeText, null)
                ? string.Empty
                : ReadSourceCodeLinesFromText(codeText);

            TextEncoding = textEncoding ?? Encoding.Unicode;
        }


        /// <summary>
        /// Read all source code lines from the disk file associated with this DSL file object
        /// </summary>
        /// <param name="codeText"></param>
        /// <returns></returns>
        private string ReadSourceCodeLinesFromText(string codeText)
        {
            var startPos = 0;

            var lines = codeText.SplitLines();

            _sourceCodeLines.Clear();

            var lineNumber = 0;

            var s = new StringBuilder();

            foreach (var lineText in lines)
            {
                s.AppendLine(lineText);

                var charCount = lineText.Length + Environment.NewLine.Length;

                var newLine = new LanguageCodeTextLine(this, lineNumber, startPos, charCount);

                _sourceCodeLines.Add(newLine);

                startPos = newLine.LastCharacterPosition + 1;

                lineNumber++;
            }

            return s.ToString();
        }

        public LanguageCodeLocation TranslateCharacterLocation(int absolutePos)
        {
            if (absolutePos < 0 || absolutePos >= CharactersCount)
                throw new IndexOutOfRangeException("Character position out of range of source code text length");

            TranslateCharacterLocation(absolutePos, out var lineNumber, out var columnNumber);

            return new LanguageCodeLocation(this, absolutePos, lineNumber, columnNumber);
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
                throw new IndexOutOfRangeException("Character position out of range of source code text length");

            lineNumber = 0;
            columnNumber = absolutePos;

            while (columnNumber >= _sourceCodeLines[lineNumber].CharactersCount)
            {
                columnNumber -= _sourceCodeLines[lineNumber].CharactersCount;
                lineNumber++;
            }
        }

        public LanguageCodeLocation TranslateCharacterLocation(int lineNumber, int columnNumber)
        {
            TranslateCharacterLocation(lineNumber, columnNumber, out var absolutePos);

            return new LanguageCodeLocation(this, absolutePos, lineNumber, columnNumber);
        }

        public void UpdateSourceCodeUnitsText()
        {
        }

        public override string ToString()
        {
            return CodeTitle;
        }
    }
}