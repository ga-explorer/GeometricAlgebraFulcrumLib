namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

public sealed class SvgValueAnimationRestart : SvgStoredValue
{
    public static SvgValueAnimationRestart Always { get;}
        = new SvgValueAnimationRestart("always");

    public static SvgValueAnimationRestart WhenNotActive { get; }
        = new SvgValueAnimationRestart("whenNotActive");

    public static SvgValueAnimationRestart Never { get; }
        = new SvgValueAnimationRestart("never");


    private SvgValueAnimationRestart(string value) : base(value)
    {
    }
}