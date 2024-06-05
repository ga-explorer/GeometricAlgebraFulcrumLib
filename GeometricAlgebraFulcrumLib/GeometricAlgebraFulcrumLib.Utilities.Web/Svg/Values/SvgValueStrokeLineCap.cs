namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueStrokeLineCap : SvgStoredValue
{
    public static SvgValueStrokeLineCap Butt { get; } 
        = new SvgValueStrokeLineCap("butt");

    public static SvgValueStrokeLineCap Round { get; }
        = new SvgValueStrokeLineCap("round");

    public static SvgValueStrokeLineCap Square { get; }
        = new SvgValueStrokeLineCap("square");

    public static SvgValueStrokeLineCap Inherit { get; }
        = new SvgValueStrokeLineCap("inherit");



    private SvgValueStrokeLineCap(string value) : base(value)
    {
    }
}