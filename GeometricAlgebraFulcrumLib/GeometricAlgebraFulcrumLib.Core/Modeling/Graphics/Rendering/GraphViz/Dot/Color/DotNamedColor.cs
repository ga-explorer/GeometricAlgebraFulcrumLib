namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Color;

/// <summary>
/// This class represents a named color value
/// See http://www.graphviz.org/content/attrs#kcolor
/// and http://www.graphviz.org/content/attrs#kcolorList 
/// and http://www.graphviz.org/content/color-names for more details
/// </summary>
public sealed class DotNamedColor : DotColor
{
    public override string Value { get; }


    internal DotNamedColor(string value)
    {
        Value = value;
    }
}