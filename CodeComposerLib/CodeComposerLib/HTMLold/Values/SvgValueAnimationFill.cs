namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueAnimationFill : HtmlStoredValue
{
    public static HtmlValueAnimationFill Remove { get; }
        = new HtmlValueAnimationFill("remove");

    public static HtmlValueAnimationFill Freeze { get; }
        = new HtmlValueAnimationFill("freeze");


    private HtmlValueAnimationFill(string value) : base(value)
    {
    }
}