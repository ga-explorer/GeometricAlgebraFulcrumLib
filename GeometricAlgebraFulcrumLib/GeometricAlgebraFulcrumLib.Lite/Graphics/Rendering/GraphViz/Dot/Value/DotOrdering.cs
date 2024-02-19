namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents an ordering value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotOrdering : DotStoredValue
{
    public static readonly DotOrdering In = new DotOrdering("in");

    public static readonly DotOrdering Out = new DotOrdering("out");


    private DotOrdering(string value)
        : base(value)
    {
    }
}