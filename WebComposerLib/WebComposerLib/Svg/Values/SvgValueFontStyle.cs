namespace WebComposerLib.Svg.Values;

public sealed class SvgValueFontStyle : SvgStoredValue
{
    public static SvgValueFontStyle Normal { get; } 
        = new SvgValueFontStyle("normal");

    public static SvgValueFontStyle Italic { get; } 
        = new SvgValueFontStyle("italic");

    public static SvgValueFontStyle Oblique { get; } 
        = new SvgValueFontStyle("oblique");

    public static SvgValueFontStyle Inherit { get; } 
        = new SvgValueFontStyle("inherit");


    private SvgValueFontStyle(string value) : base(value)
    {
    }
}