namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueFillRule : HtmlStoredValue
{
    public static HtmlValueFillRule NonZero { get; }
        = new HtmlValueFillRule("nonzero");

    public static HtmlValueFillRule EvenOdd { get; }
        = new HtmlValueFillRule("evenodd");

    public static HtmlValueFillRule Inherit { get; }
        = new HtmlValueFillRule("inherit");


    private HtmlValueFillRule(string value) : base(value)
    {
    }
}