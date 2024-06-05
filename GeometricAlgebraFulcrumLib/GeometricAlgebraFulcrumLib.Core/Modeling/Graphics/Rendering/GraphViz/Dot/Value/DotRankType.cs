namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a ranking type value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotRankType : DotStoredValue
{
    public static readonly DotRankType Same = new DotRankType("same");

    public static readonly DotRankType Min = new DotRankType("min");
        
    public static readonly DotRankType Source = new DotRankType("source");
        
    public static readonly DotRankType Max = new DotRankType("max");
        
    public static readonly DotRankType Sink = new DotRankType("sink");


    private DotRankType(string value)
        : base(value)
    {
    }
}