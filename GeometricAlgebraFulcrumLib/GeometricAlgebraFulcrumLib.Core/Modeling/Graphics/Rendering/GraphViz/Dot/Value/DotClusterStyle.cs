namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a cluster style value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotClusterStyle : DotStoredValue
{
    public static readonly DotClusterStyle Solid = new DotClusterStyle("solid");
        
    public static readonly DotClusterStyle Dashed = new DotClusterStyle("dashed");
        
    public static readonly DotClusterStyle Dotted = new DotClusterStyle("dotted");
        
    public static readonly DotClusterStyle Bold = new DotClusterStyle("bold");
        
    public static readonly DotClusterStyle Rounded = new DotClusterStyle("rounded");
        
    public static readonly DotClusterStyle Filled = new DotClusterStyle("filled");
        
    public static readonly DotClusterStyle Striped = new DotClusterStyle("striped");


    private DotClusterStyle(string value)
        : base(value)
    {
    }
}