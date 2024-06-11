using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Label.Text;

/// <summary>
/// This class represents an HTML line break with optional alignment
/// See http://www.graphviz.org/content/node-shapes#html for more details
/// </summary>
public sealed class DotHtmlLineBreak : DotHtmlTextItem
{
    private readonly string _align;

    public override string Value
    {
        get
        {
            if (String.IsNullOrEmpty(_align)) 
                return "<BR/>";

            return 
                new StringBuilder()
                    .Append("<BR ALIGN=")
                    .Append(_align)
                    .Append("/>")
                    .ToString();
        }
    }


    internal DotHtmlLineBreak()
    {
        _align = String.Empty;
    }

    internal DotHtmlLineBreak(string align)
    {
        _align = align;
    }
}