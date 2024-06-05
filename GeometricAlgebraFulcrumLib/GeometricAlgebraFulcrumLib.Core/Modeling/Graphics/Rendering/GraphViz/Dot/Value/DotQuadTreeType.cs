namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a quad tree type value
/// See http://www.graphviz.org/content/attrs#kquadType for more details
/// </summary>
public sealed class DotQuadTreeType : DotStoredValue
{
    public static readonly DotQuadTreeType True = new DotQuadTreeType("true");

    public static readonly DotQuadTreeType False = new DotQuadTreeType("false");

    public static readonly DotQuadTreeType Normal = new DotQuadTreeType("normal");

    public static readonly DotQuadTreeType Fast = new DotQuadTreeType("fast");

    public static readonly DotQuadTreeType None = new DotQuadTreeType("none");


    private DotQuadTreeType(string value)
        : base(value)
    {
    }
}