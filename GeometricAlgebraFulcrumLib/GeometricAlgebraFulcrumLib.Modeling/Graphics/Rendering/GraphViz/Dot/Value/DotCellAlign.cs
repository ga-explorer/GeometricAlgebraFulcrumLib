namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a cell alignment value
/// See http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotCellAlign : DotStoredValue
{
    public static readonly DotCellAlign Left = new DotCellAlign("left");

    public static readonly DotCellAlign Right = new DotCellAlign("right");

    public static readonly DotCellAlign Center = new DotCellAlign("center");

    public static readonly DotCellAlign TextAlign = new DotCellAlign("text");


    private DotCellAlign(string value)
        : base(value)
    {
    }
}