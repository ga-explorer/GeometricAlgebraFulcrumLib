namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a justification value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotJustification : DotStoredValue
{
    public static readonly DotJustification Left = new DotJustification("l");

    public static readonly DotJustification Right = new DotJustification("r");
        
    public static readonly DotJustification Center = new DotJustification("c");


    private DotJustification(string value)
        : base(value)
    {
    }
}