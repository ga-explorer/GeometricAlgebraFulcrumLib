namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueFontStyle : HtmlStoredValue
{
    public static HtmlValueFontStyle Normal { get; } 
        = new HtmlValueFontStyle("normal");

    public static HtmlValueFontStyle Italic { get; } 
        = new HtmlValueFontStyle("italic");

    public static HtmlValueFontStyle Oblique { get; } 
        = new HtmlValueFontStyle("oblique");

    public static HtmlValueFontStyle Inherit { get; } 
        = new HtmlValueFontStyle("inherit");


    private HtmlValueFontStyle(string value) : base(value)
    {
    }
}