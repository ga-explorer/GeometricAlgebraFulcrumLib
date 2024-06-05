namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueShapeRendering : SvgStoredValue
{
    public static SvgValueShapeRendering Auto { get; }
        = new SvgValueShapeRendering("auto");

    public static SvgValueShapeRendering OptimizeSpeed { get; }
        = new SvgValueShapeRendering("optimizeSpeed");

    public static SvgValueShapeRendering CrispEdges { get; }
        = new SvgValueShapeRendering("crispEdges");

    public static SvgValueShapeRendering GeometricPrecision { get; }
        = new SvgValueShapeRendering("geometricPrecision");


    private SvgValueShapeRendering(string value) : base(value)
    {
    }
}