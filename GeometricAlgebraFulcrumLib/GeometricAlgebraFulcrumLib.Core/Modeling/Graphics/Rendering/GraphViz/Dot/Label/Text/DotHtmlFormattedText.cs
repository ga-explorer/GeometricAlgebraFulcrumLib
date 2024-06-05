namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Label.Text;

/// <summary>
/// This class represents formatted HTML text
/// See http://www.graphviz.org/content/node-shapes#html for more details
/// </summary>
public sealed class DotHtmlFormattedText : DotHtmlTextItem
{
    public DotHtmlText HtmlText { get; }

    public IDotHtmlTagFormatting HtmlTag { get; }

    public override string Value => HtmlTag.FormatText(HtmlText);


    internal DotHtmlFormattedText(IDotHtmlTagFormatting htmlTag, DotHtmlText htmlText)
    {
        HtmlText = htmlText;

        HtmlTag = htmlTag;
    }
}