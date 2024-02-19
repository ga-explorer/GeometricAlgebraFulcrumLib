namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueAnimationAccumulate : HtmlStoredValue
{
    public static HtmlValueAnimationAccumulate None { get; }
        = new HtmlValueAnimationAccumulate("none");

    public static HtmlValueAnimationAccumulate Sum { get; }
        = new HtmlValueAnimationAccumulate("sum");


    private HtmlValueAnimationAccumulate(string value) : base(value)
    {
    }
}