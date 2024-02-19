namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueStrokeLineJoin : HtmlStoredValue
{
    public static HtmlValueStrokeLineJoin Miter { get; }
        = new HtmlValueStrokeLineJoin("miter");

    public static HtmlValueStrokeLineJoin Round { get; }
        = new HtmlValueStrokeLineJoin("round");

    public static HtmlValueStrokeLineJoin Bevel { get; }
        = new HtmlValueStrokeLineJoin("bevel");

    public static HtmlValueStrokeLineJoin Inherit { get; }
        = new HtmlValueStrokeLineJoin("inherit");



    private HtmlValueStrokeLineJoin(string value) : base(value)
    {
    }
}