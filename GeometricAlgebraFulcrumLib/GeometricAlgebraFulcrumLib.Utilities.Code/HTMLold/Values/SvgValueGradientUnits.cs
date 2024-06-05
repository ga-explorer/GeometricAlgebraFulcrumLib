namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValueGradientUnits : HtmlStoredValue
{
    public static HtmlValueGradientUnits UserSpaceOnUse { get; }
        = new HtmlValueGradientUnits("userSpaceOnUse");

    public static HtmlValueGradientUnits ObjectBoundingBox { get; }
        = new HtmlValueGradientUnits("objectBoundingBox");


    private HtmlValueGradientUnits(string value) : base(value)
    {
    }
}