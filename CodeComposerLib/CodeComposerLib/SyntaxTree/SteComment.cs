using System;
using System.Collections.Generic;
using TextComposerLib;

namespace CodeComposerLib.SyntaxTree
{
    public class SteComment : SteSyntaxElement
    {
        private bool _singleLineComment = true;

        public bool SingleLineComment
        {
            get { return _singleLineComment; }
            set { _singleLineComment = value; }
        }

        public bool MultiLineComment 
        {
            get { return !_singleLineComment; }
            set { _singleLineComment = !value; }
        }

        public string[] CommentedTextLines { get; }

        public int CommentedTextLinesCount => CommentedTextLines.Length;


        public SteComment()
        {
            CommentedTextLines = new[] {String.Empty};
        }

        public SteComment(int emptyLinesCount)
        {
            CommentedTextLines = new string[emptyLinesCount];

            for (var i = 0; i < emptyLinesCount; i++)
                CommentedTextLines[i] = String.Empty;
        }

        public SteComment(string commentedText)
        {
            CommentedTextLines = 
                String.IsNullOrEmpty(commentedText)
                ? new[] { String.Empty }
                : commentedText.SplitLines();
        }

        public SteComment(IEnumerable<string> commentedTextStrings)
        {
            var lines = new List<string>();

            foreach (var textString in commentedTextStrings)
                if (String.IsNullOrEmpty(textString))
                    lines.Add(String.Empty);
                else
                    lines.AddRange(textString.SplitLines());

            CommentedTextLines = 
                lines.Count == 0
                ? new[] { String.Empty }
                : lines.ToArray();
        }

        public SteComment(params string[] commentedTextStrings)
        {
            var lines = new List<string>();

            foreach (var textString in commentedTextStrings)
                if (String.IsNullOrEmpty(textString))
                    lines.Add(String.Empty);
                else
                    lines.AddRange(textString.SplitLines());

            CommentedTextLines =
                lines.Count == 0
                ? new[] { String.Empty }
                : lines.ToArray();
        }
    }
}
