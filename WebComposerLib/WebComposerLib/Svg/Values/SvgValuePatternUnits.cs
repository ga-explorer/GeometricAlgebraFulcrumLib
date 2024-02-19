namespace WebComposerLib.Svg.Values;

public sealed class SvgValuePatternUnits : SvgStoredValue
{
    public static SvgValuePatternUnits UserSpaceOnUse { get; }
        = new SvgValuePatternUnits("userSpaceOnUse");

    public static SvgValuePatternUnits ObjectBoundingBox { get; }
        = new SvgValuePatternUnits("objectBoundingBox");


    private SvgValuePatternUnits(string value) : base(value)
    {
    }
}