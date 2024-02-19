namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueFontVariant : HtmlStoredValue
{
    public static HtmlValueFontVariant Normal { get; } 
        = new HtmlValueFontVariant("normal");

    public static HtmlValueFontVariant SmallCaps { get; } 
        = new HtmlValueFontVariant("small-caps");

    public static HtmlValueFontVariant Inherit { get; } 
        = new HtmlValueFontVariant("inherit");


    private HtmlValueFontVariant(string value) : base(value)
    {
    }
}