namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueTextDecoration : HtmlStoredValue
{
    public static HtmlValueTextDecoration None { get; }
        = new HtmlValueTextDecoration("none");

    public static HtmlValueTextDecoration Underline { get; }
        = new HtmlValueTextDecoration("underline");

    public static HtmlValueTextDecoration Overline { get; }
        = new HtmlValueTextDecoration("overline");

    public static HtmlValueTextDecoration LineThrough { get; }
        = new HtmlValueTextDecoration("line-through");

    public static HtmlValueTextDecoration Blink { get; }
        = new HtmlValueTextDecoration("blink");

    public static HtmlValueTextDecoration Inherit { get; }
        = new HtmlValueTextDecoration("inherit");


    private HtmlValueTextDecoration(string value) : base(value)
    {
    }
}