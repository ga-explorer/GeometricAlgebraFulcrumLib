namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValuePatternUnits : HtmlStoredValue
{
    public static HtmlValuePatternUnits UserSpaceOnUse { get; }
        = new HtmlValuePatternUnits("userSpaceOnUse");

    public static HtmlValuePatternUnits ObjectBoundingBox { get; }
        = new HtmlValuePatternUnits("objectBoundingBox");


    private HtmlValuePatternUnits(string value) : base(value)
    {
    }
}