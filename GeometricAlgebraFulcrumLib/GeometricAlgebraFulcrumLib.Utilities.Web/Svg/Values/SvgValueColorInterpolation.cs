namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueColorInterpolation : SvgStoredValue
{
    public static SvgValueColorInterpolation Auto { get; }
        = new SvgValueColorInterpolation("auto");

    public static SvgValueColorInterpolation StandardRgb { get; }
        = new SvgValueColorInterpolation("sRGB");

    public static SvgValueColorInterpolation LinearRgb { get; }
        = new SvgValueColorInterpolation("linearRGB");

    public static SvgValueColorInterpolation Inherit { get; }
        = new SvgValueColorInterpolation("inherit");


    private SvgValueColorInterpolation(string value) : base(value)
    {
    }
}