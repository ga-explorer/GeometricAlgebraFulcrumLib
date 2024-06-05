namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueMarkerUnits : SvgStoredValue
{
    public static SvgValueMarkerUnits UserSpaceOnUse { get; }
        = new SvgValueMarkerUnits("userSpaceOnUse");

    public static SvgValueMarkerUnits StrokeWidth { get; }
        = new SvgValueMarkerUnits("strokeWidth");


    private SvgValueMarkerUnits(string value) : base(value)
    {
    }
}