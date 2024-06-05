namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValueMarkerUnits : HtmlStoredValue
{
    public static HtmlValueMarkerUnits UserSpaceOnUse { get; }
        = new HtmlValueMarkerUnits("userSpaceOnUse");

    public static HtmlValueMarkerUnits StrokeWidth { get; }
        = new HtmlValueMarkerUnits("strokeWidth");


    private HtmlValueMarkerUnits(string value) : base(value)
    {
    }
}