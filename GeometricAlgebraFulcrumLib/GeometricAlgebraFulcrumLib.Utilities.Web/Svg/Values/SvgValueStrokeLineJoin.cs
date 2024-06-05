namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueStrokeLineJoin : SvgStoredValue
{
    public static SvgValueStrokeLineJoin Miter { get; }
        = new SvgValueStrokeLineJoin("miter");

    public static SvgValueStrokeLineJoin Round { get; }
        = new SvgValueStrokeLineJoin("round");

    public static SvgValueStrokeLineJoin Bevel { get; }
        = new SvgValueStrokeLineJoin("bevel");

    public static SvgValueStrokeLineJoin Inherit { get; }
        = new SvgValueStrokeLineJoin("inherit");



    private SvgValueStrokeLineJoin(string value) : base(value)
    {
    }
}