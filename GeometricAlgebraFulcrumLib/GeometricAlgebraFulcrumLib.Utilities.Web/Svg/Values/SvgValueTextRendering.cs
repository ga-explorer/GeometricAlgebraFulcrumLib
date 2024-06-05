namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueTextRendering : SvgStoredValue
{
    public static SvgValueTextRendering Auto { get; }
        = new SvgValueTextRendering("auto");

    public static SvgValueTextRendering OptimizeSpeed { get; }
        = new SvgValueTextRendering("optimizeSpeed");

    public static SvgValueTextRendering OptimizeLegibility { get; }
        = new SvgValueTextRendering("optimizeLegibility");

    public static SvgValueTextRendering GeometricPrecision { get; }
        = new SvgValueTextRendering("geometricPrecision");

    public static SvgValueTextRendering Inherit { get; }
        = new SvgValueTextRendering("Inherit");


    private SvgValueTextRendering(string value) : base(value)
    {
    }
}