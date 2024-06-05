namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValueShapeRendering : HtmlStoredValue
{
    public static HtmlValueShapeRendering Auto { get; }
        = new HtmlValueShapeRendering("auto");

    public static HtmlValueShapeRendering OptimizeSpeed { get; }
        = new HtmlValueShapeRendering("optimizeSpeed");

    public static HtmlValueShapeRendering CrispEdges { get; }
        = new HtmlValueShapeRendering("crispEdges");

    public static HtmlValueShapeRendering GeometricPrecision { get; }
        = new HtmlValueShapeRendering("geometricPrecision");


    private HtmlValueShapeRendering(string value) : base(value)
    {
    }
}