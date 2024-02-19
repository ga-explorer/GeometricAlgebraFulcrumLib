namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueAnimationAdditive : HtmlStoredValue
{
    public static HtmlValueAnimationAdditive Replace { get; }
        = new HtmlValueAnimationAdditive("replace");

    public static HtmlValueAnimationAdditive Sum { get; }
        = new HtmlValueAnimationAdditive("sum");


    private HtmlValueAnimationAdditive(string value) : base(value)
    {
    }
}