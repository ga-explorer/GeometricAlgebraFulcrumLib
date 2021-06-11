using System.Text;
using Irony.Parsing;

namespace CodeComposerLib.Irony.SourceCode
{
    public sealed class IronyAstObjectCodeLocation
    {
        public static IronyAstObjectCodeLocation Create(LanguageCodeProject project, ParseTreeNode node)
        {
            if (project.HasActiveCodeUnit == false)
                return null;

            var absolutePos = node.Span.Location.Position;

            var location = 
                project
                .ActiveCodeUnit
                .TranslateCharacterLocation(absolutePos);

            return new IronyAstObjectCodeLocation(project, location, node);
        }


        public LanguageCodeLocation CodeLocation { get; }

        public Token CodeToken { get; }

        public ParseTreeNode CodeParseNode { get; }

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
                    s.Append(CodeUnit.CodeText.Substring(CodeLocation.CharacterNumber, CodeParseNode.Span.Length));
                }
                else if (ReferenceEquals(CodeToken, null) == false)
                {
                    s.Append(CodeToken.Text);
                }
                else
                {
                    s.Append(CodeUnit.CodeText.Substring(CodeLocation.CharacterNumber, 128));

                    if (s.Length == 128)
                        s.Append("...");
                }

                return s.ToString();
            }
        }


        private IronyAstObjectCodeLocation(ISourceCodeUnitsContainer codeUnitsContainer, LanguageCodeLocation location, ParseTreeNode node)
        {
            CodeUnitsContainer = codeUnitsContainer;
            CodeLocation = location;
            CodeToken = node.FindToken();
            CodeParseNode = node;

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

            s.AppendLine("Code: ");
            s.AppendLine(CodeText);

            return s.ToString();
        }
    }
}
