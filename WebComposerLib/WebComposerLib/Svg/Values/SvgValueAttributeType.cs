namespace WebComposerLib.Svg.Values;

public sealed class SvgValueAttributeType : SvgStoredValue
{
    public static SvgValueAttributeType Auto { get; }
        = new SvgValueAttributeType("auto");

    public static SvgValueAttributeType Css { get; }
        = new SvgValueAttributeType("css");

    public static SvgValueAttributeType Xml { get; }
        = new SvgValueAttributeType("xml");


    private SvgValueAttributeType(string value) : base(value)
    {
    }
}