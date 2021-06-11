using CodeComposerLib.LaTeX.Code;
using CodeComposerLib.LaTeX.Documents;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.LaTeX
{
    public class LaTeXComposer : LaTeXCodeSectionsList
    {
        public LaTeXDocumentClass DocumentClass { get; private set; }

        public LaTeXPreamble Preamable { get; } 
            = new LaTeXPreamble();

        public LaTeXDocument Document { get; }
            = new LaTeXDocument();


        public override string ToString()
        {
            var composer = new LinearTextComposer() { IndentationDefault = "  " };

            DocumentClass.ToText(composer);
            Preamable.ToText(composer);
            Document.ToText(composer);

            return composer.ToString();
        }
    }
}
