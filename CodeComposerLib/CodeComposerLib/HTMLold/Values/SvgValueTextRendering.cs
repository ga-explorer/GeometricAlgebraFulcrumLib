namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueTextRendering : HtmlStoredValue
{
    public static HtmlValueTextRendering Auto { get; }
        = new HtmlValueTextRendering("auto");

    public static HtmlValueTextRendering OptimizeSpeed { get; }
        = new HtmlValueTextRendering("optimizeSpeed");

    public static HtmlValueTextRendering OptimizeLegibility { get; }
        = new HtmlValueTextRendering("optimizeLegibility");

    public static HtmlValueTextRendering GeometricPrecision { get; }
        = new HtmlValueTextRendering("geometricPrecision");

    public static HtmlValueTextRendering Inherit { get; }
        = new HtmlValueTextRendering("Inherit");


    private HtmlValueTextRendering(string value) : base(value)
    {
    }
}