using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Documents;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer;

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