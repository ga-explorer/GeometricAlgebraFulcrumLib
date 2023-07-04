namespace WebComposerLib.Svg.Paths;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/d
/// https://developer.mozilla.org/en-US/docs/Web/SVG/Tutorial/Paths
/// </summary>
public abstract class SvgPathCommandSimple : 
    SvgPathCommand
{
    public abstract char CommandSymbol { get; }

    public bool IsRelative { get; }

    public bool IsAbsolute 
        => !IsRelative;


    protected SvgPathCommandSimple(bool isRelative)
    {
        IsRelative = isRelative;
    }


    public override string ToString()
    {
        return ValueText;
    }
}