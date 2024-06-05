namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValueColorInterpolationFilters : HtmlStoredValue
{
    public static HtmlValueColorInterpolationFilters Auto { get; }
        = new HtmlValueColorInterpolationFilters("auto");

    public static HtmlValueColorInterpolationFilters StandardRgb { get; }
        = new HtmlValueColorInterpolationFilters("sRGB");

    public static HtmlValueColorInterpolationFilters LinearRgb { get; }
        = new HtmlValueColorInterpolationFilters("linearRGB");

    public static HtmlValueColorInterpolationFilters Inherit { get; }
        = new HtmlValueColorInterpolationFilters("inherit");


    private HtmlValueColorInterpolationFilters(string value) : base(value)
    {
    }
}