namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueAnimationAccumulate : SvgStoredValue
{
    public static SvgValueAnimationAccumulate None { get; }
        = new SvgValueAnimationAccumulate("none");

    public static SvgValueAnimationAccumulate Sum { get; }
        = new SvgValueAnimationAccumulate("sum");


    private SvgValueAnimationAccumulate(string value) : base(value)
    {
    }
}