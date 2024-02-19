namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a node image scaling method value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotNodeImageScale : DotStoredValue
{
    public static readonly DotNodeImageScale True = new DotNodeImageScale("true");

    public static readonly DotNodeImageScale False = new DotNodeImageScale("false");
        
    public static readonly DotNodeImageScale Width = new DotNodeImageScale("width");
        
    public static readonly DotNodeImageScale Height = new DotNodeImageScale("height");
        
    public static readonly DotNodeImageScale Both = new DotNodeImageScale("both");


    private DotNodeImageScale(string value)
        : base(value)
    {
    }
}