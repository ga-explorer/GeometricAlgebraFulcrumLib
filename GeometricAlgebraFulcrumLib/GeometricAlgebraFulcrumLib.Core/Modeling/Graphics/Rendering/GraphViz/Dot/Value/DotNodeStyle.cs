namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a node style value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotNodeStyle : DotStoredValue
{
    public static readonly DotNodeStyle Solid = new DotNodeStyle("solid");
        
    public static readonly DotNodeStyle Dashed = new DotNodeStyle("dashed");
        
    public static readonly DotNodeStyle Dotted = new DotNodeStyle("dotted");
        
    public static readonly DotNodeStyle Bold = new DotNodeStyle("bold");
        
    public static readonly DotNodeStyle Rounded = new DotNodeStyle("rounded");
        
    public static readonly DotNodeStyle Diagonals = new DotNodeStyle("diagonals");
        
    public static readonly DotNodeStyle Filled = new DotNodeStyle("filled");
        
    public static readonly DotNodeStyle Striped = new DotNodeStyle("striped");
        
    public static readonly DotNodeStyle Wedged = new DotNodeStyle("wedged");


    private DotNodeStyle(string value)
        : base(value)
    {
    }
}