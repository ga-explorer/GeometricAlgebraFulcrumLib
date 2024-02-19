using TextComposerLib.Text.Linear;
using WebComposerLib.LaTeX.CodeComposer.Code;
using WebComposerLib.LaTeX.CodeComposer.Documents;

namespace WebComposerLib.LaTeX.CodeComposer;

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