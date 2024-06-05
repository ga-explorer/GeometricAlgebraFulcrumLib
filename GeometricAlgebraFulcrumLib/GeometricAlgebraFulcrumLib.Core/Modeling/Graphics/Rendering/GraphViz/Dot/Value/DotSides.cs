namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a side value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotSides : DotStoredValue
{
    public static readonly DotSides Left = new DotSides("L");

    public static readonly DotSides Right = new DotSides("R");

    public static readonly DotSides Top = new DotSides("T");

    public static readonly DotSides Bottom = new DotSides("B");


    private DotSides(string value)
        : base(value)
    {
    }

}