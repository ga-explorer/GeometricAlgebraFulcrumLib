namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValueColorInterpolation : HtmlStoredValue
{
    public static HtmlValueColorInterpolation Auto { get; }
        = new HtmlValueColorInterpolation("auto");

    public static HtmlValueColorInterpolation StandardRgb { get; }
        = new HtmlValueColorInterpolation("sRGB");

    public static HtmlValueColorInterpolation LinearRgb { get; }
        = new HtmlValueColorInterpolation("linearRGB");

    public static HtmlValueColorInterpolation Inherit { get; }
        = new HtmlValueColorInterpolation("inherit");


    private HtmlValueColorInterpolation(string value) : base(value)
    {
    }
}