namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/d#closepath
/// </summary>
public sealed class SvgPathCommandClosePath : 
    SvgPathCommandSimple
{
    public static SvgPathCommandClosePath DefaultCommand { get; } 
        = new SvgPathCommandClosePath();

        
    public override char CommandSymbol 
        => IsRelative ? 'z' : 'Z';

    public override string ValueText 
        => "z";


    private SvgPathCommandClosePath()
        : base(false)
    {
    }
}