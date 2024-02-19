namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents the node overlap value
/// See http://www.graphviz.org/content/attrs#doverlap for more details
/// </summary>
public sealed class DotOverlap : DotStoredValue
{
    public static readonly DotOverlap True = new DotOverlap("true");

    public static readonly DotOverlap False = new DotOverlap("false");

    public static readonly DotOverlap Scale = new DotOverlap("scale");

    public static readonly DotOverlap Prism = new DotOverlap("prism");

    public static readonly DotOverlap Voronoi = new DotOverlap("voronoi");

    public static readonly DotOverlap ScaleXy = new DotOverlap("scalexy");

    public static readonly DotOverlap Compress = new DotOverlap("compress");

    public static readonly DotOverlap Ortho = new DotOverlap("ortho");

    public static readonly DotOverlap Ortho_Yx = new DotOverlap("ortho_yx");

    public static readonly DotOverlap POrtho = new DotOverlap("portho");

    public static readonly DotOverlap POrtho_Yx = new DotOverlap("portho_yx");

    public static readonly DotOverlap Vpsc = new DotOverlap("vpsc");

    public static readonly DotOverlap OrthoXy = new DotOverlap("orthoxy");

    public static readonly DotOverlap OrthoYx = new DotOverlap("orthoyx");

    public static readonly DotOverlap PorthoXy = new DotOverlap("porthoxy");

    public static readonly DotOverlap PorthoYx = new DotOverlap("porthoyx");

    public static readonly DotOverlap IPsep = new DotOverlap("ipsep");


    private DotOverlap(string value)
        : base(value)
    {
    }
}