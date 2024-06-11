namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a vertical alignment value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotVerticalAlign : DotStoredValue
{
    public static readonly DotVerticalAlign Top = new DotVerticalAlign("top");

    public static readonly DotVerticalAlign Middle = new DotVerticalAlign("middle");

    public static readonly DotVerticalAlign Bottom = new DotVerticalAlign("bottom");


    private DotVerticalAlign(string value)
        : base(value)
    {
    }
}