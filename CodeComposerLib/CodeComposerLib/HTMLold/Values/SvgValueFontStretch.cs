namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueFontStretch : HtmlStoredValue
{
    public static HtmlValueFontStretch Normal { get; } 
        = new HtmlValueFontStretch("normal");

    public static HtmlValueFontStretch Wider { get; } 
        = new HtmlValueFontStretch("wider");

    public static HtmlValueFontStretch Narrower { get; } 
        = new HtmlValueFontStretch("narrower");

    public static HtmlValueFontStretch UltraCondensed { get; } 
        = new HtmlValueFontStretch("ultra-condensed");

    public static HtmlValueFontStretch ExtraCondensed { get; } 
        = new HtmlValueFontStretch("extra-condensed");

    public static HtmlValueFontStretch Condensed { get; } 
        = new HtmlValueFontStretch("condensed");

    public static HtmlValueFontStretch SemiCondensed { get; } 
        = new HtmlValueFontStretch("semi-condensed");

    public static HtmlValueFontStretch SemiExpanded { get; } 
        = new HtmlValueFontStretch("semi-expanded");

    public static HtmlValueFontStretch Expanded { get; } 
        = new HtmlValueFontStretch("expanded");

    public static HtmlValueFontStretch ExtraExpanded { get; } 
        = new HtmlValueFontStretch("extra-expanded");

    public static HtmlValueFontStretch UltraExpanded { get; } 
        = new HtmlValueFontStretch("ultra-expanded");

    public static HtmlValueFontStretch Inherit { get; } 
        = new HtmlValueFontStretch("inherit");


    private HtmlValueFontStretch(string value) : base(value)
    {
    }
}