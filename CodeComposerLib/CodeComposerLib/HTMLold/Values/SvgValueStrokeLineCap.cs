namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueStrokeLineCap : HtmlStoredValue
{
    public static HtmlValueStrokeLineCap Butt { get; } 
        = new HtmlValueStrokeLineCap("butt");

    public static HtmlValueStrokeLineCap Round { get; }
        = new HtmlValueStrokeLineCap("round");

    public static HtmlValueStrokeLineCap Square { get; }
        = new HtmlValueStrokeLineCap("square");

    public static HtmlValueStrokeLineCap Inherit { get; }
        = new HtmlValueStrokeLineCap("inherit");



    private HtmlValueStrokeLineCap(string value) : base(value)
    {
    }
}