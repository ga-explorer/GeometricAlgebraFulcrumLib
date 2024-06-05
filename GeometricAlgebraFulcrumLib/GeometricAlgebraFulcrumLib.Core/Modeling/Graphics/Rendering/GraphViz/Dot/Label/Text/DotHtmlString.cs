using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Label.Text;

/// <summary>
/// This class represents an HTML string
/// See http://www.graphviz.org/content/node-shapes#html for more details
/// </summary>
public sealed class DotHtmlString : DotHtmlTextItem
{
    public override string Value { get; }


    internal DotHtmlString(string text)
    {
        Value = 
            text
                .ToHtmlSafeString()
                .SplitLines()
                .Concatenate("<BR/>");
    }


    public override string ToString()
    {
        return Value;
    }
}