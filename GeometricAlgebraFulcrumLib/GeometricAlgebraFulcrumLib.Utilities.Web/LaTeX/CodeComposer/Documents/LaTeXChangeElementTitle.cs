using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Documents;

/// <summary>
/// https://tex.stackexchange.com/questions/82993/how-to-change-the-name-of-document-elements-like-figure-contents-bibliogr
/// </summary>
public sealed class LaTeXChangeElementTitle : ILaTeXCodeElement
{
    public static LaTeXChangeElementTitle Create(string elementName, string newTitle)
    {
        return new LaTeXChangeElementTitle(elementName, newTitle);
    }


    public string ElementName { get; }

    public string NewTitle { get; set; }

    public IEnumerable<ILaTeXCodeElement> Contents
        => Enumerable.Empty<ILaTeXCodeElement>();


    private LaTeXChangeElementTitle(string elementName, string newTitle)
    {
        ElementName = elementName;
        NewTitle = newTitle;
    }


    public void ToText(LinearTextComposer composer)
    {
        composer
            .AppendAtNewLine(@"renewcommand{\")
            .Append(ElementName)
            .Append("}{")
            .Append(NewTitle)
            .AppendLine("}");
    }

    public bool IsEmpty()
    {
        return false;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append(@"renewcommand{\")
            .Append(ElementName)
            .Append("}{")
            .Append(NewTitle)
            .AppendLine("}")
            .ToString();
    }
}