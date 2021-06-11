using System.Text;
using Irony.Parsing;
using TextComposerLib;

namespace CodeComposerLib.Irony.SourceCode
{
    public sealed class LanguageCompilationMessage
    {
        public LanguageCodeLocation CodeLocation { get; }

        public Token CodeToken { get; }

        public ParseTreeNode CodeParseNode { get; }

        public string Description { get; }

        public ISourceCodeUnitsContainer CodeUnitsContainer { get; private set; }


        public ISourceCodeUnit CodeUnit => CodeLocation.CodeUnit;

        public int CodeSpanLength => CodeLocation.SpanLength;

        public string CodeText
        {
            get
            {
                var s = new StringBuilder();

                if (ReferenceEquals(CodeParseNode, null) == false)
                {
                    s.Append(CodeUnit.CodeText.TryGetSubstring(CodeLocation.CharacterNumber, CodeParseNode.Span.Length));
                }
                else if (ReferenceEquals(CodeToken, null) == false)
                {
                    s.Append(CodeToken.Text);
                }
                else
                {
                    s.Append(CodeUnit.CodeText.TryGetSubstring(CodeLocation.CharacterNumber, 128));

                    if (s.Length == 128)
                        s.Append("...");
                }

                return s.ToString();
            }
        }


        internal LanguageCompilationMessage(ISourceCodeUnitsContainer codeUnitsContainer, LanguageCodeLocation location, ParseTreeNode node, string description)
        {
            CodeUnitsContainer = codeUnitsContainer;
            CodeLocation = location;
            CodeToken = node.FindToken();
            CodeParseNode = node;
            Description = description;

            SetLocationSpan();
        }

        internal LanguageCompilationMessage(ISourceCodeUnitsContainer codeUnitsContainer, LanguageCodeLocation location, Token token, string description)
        {
            CodeUnitsContainer = codeUnitsContainer;
            CodeLocation = location;
            CodeToken = token;
            CodeParseNode = null;
            Description = description;

            SetLocationSpan();
        }

        internal LanguageCompilationMessage(ISourceCodeUnitsContainer codeUnitsContainer, LanguageCodeLocation location, string description)
        {
            CodeUnitsContainer = codeUnitsContainer;
            CodeLocation = location;
            CodeToken = null;
            CodeParseNode = null;
            Description = description;

            SetLocationSpan();
        }


        private void SetLocationSpan()
        {
            if (ReferenceEquals(CodeParseNode, null) == false)
                CodeLocation.SpanLength = CodeParseNode.Span.Length;

            else if (ReferenceEquals(CodeToken, null) == false)
                CodeLocation.SpanLength = CodeToken.Text.Length;
                
            else
                CodeLocation.SpanLength = 128;
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append("At: ");
            s.AppendLine(CodeLocation.ToString());

            s.AppendLine("Message: ");
            s.AppendLine(Description);

            s.AppendLine("Code: ");
            s.AppendLine(CodeText);

            return s.ToString();
        }
    }
}
