namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValueImageRendering : HtmlStoredValue
{
    public static HtmlValueImageRendering Auto { get; }
        = new HtmlValueImageRendering("auto");

    public static HtmlValueImageRendering OptimizeSpeed { get; }
        = new HtmlValueImageRendering("optimizeSpeed");

    public static HtmlValueImageRendering OptimizeQuality { get; }
        = new HtmlValueImageRendering("optimizeQuality");


    private HtmlValueImageRendering(string value) : base(value)
    {
    }
}