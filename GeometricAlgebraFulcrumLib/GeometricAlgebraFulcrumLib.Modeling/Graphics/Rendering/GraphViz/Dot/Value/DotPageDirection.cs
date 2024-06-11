namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents a page direction value
/// See http://www.graphviz.org/content/attrs#kpagedir for more details
/// </summary>
public sealed class DotPageDirection : DotStoredValue
{
    public static readonly DotPageDirection BottomToTop_LeftToRight = new DotPageDirection("BL");

    public static readonly DotPageDirection BottomToTop_RightToLeft = new DotPageDirection("BR");

    public static readonly DotPageDirection TopToBottom_LeftToRight = new DotPageDirection("TL");

    public static readonly DotPageDirection TopToBottom_RightToLeft = new DotPageDirection("TR");

    public static readonly DotPageDirection LeftToRight_BottomToTop = new DotPageDirection("LB");

    public static readonly DotPageDirection LeftToRight_TopToBottom = new DotPageDirection("LT");

    public static readonly DotPageDirection RightToLeft_BottomToTop = new DotPageDirection("RB");

    public static readonly DotPageDirection RightToLeft_TopToBottom = new DotPageDirection("RT");


    private DotPageDirection(string value)
        : base(value)
    {
    }

}