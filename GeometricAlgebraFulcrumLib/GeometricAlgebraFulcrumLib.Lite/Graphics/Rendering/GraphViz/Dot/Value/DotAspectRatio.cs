namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents the aspect ratio (drawing height/drawing width) for the drawing
/// See http://www.graphviz.org/content/attrs#dratio for more details
/// </summary>
public sealed class DotAspectRatio : DotStoredValue
{
    public static readonly DotAspectRatio Fill = new DotAspectRatio("fill");

    public static readonly DotAspectRatio Compress = new DotAspectRatio("compress");

    public static readonly DotAspectRatio Expand = new DotAspectRatio("expand");

    public static readonly DotAspectRatio Auto = new DotAspectRatio("auto");


    private DotAspectRatio(string value)
        : base(value)
    {
    }
}