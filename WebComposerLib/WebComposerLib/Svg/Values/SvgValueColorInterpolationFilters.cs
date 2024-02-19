namespace WebComposerLib.Svg.Values;

public sealed class SvgValueColorInterpolationFilters : SvgStoredValue
{
    public static SvgValueColorInterpolationFilters Auto { get; }
        = new SvgValueColorInterpolationFilters("auto");

    public static SvgValueColorInterpolationFilters StandardRgb { get; }
        = new SvgValueColorInterpolationFilters("sRGB");

    public static SvgValueColorInterpolationFilters LinearRgb { get; }
        = new SvgValueColorInterpolationFilters("linearRGB");

    public static SvgValueColorInterpolationFilters Inherit { get; }
        = new SvgValueColorInterpolationFilters("inherit");


    private SvgValueColorInterpolationFilters(string value) : base(value)
    {
    }
}