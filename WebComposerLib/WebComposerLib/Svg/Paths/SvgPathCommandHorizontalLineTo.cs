using System.Text;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths;

public sealed class SvgPathCommandHorizontalLineTo : 
    SvgPathCommandSimple
{
    public static SvgPathCommandHorizontalLineTo CreateAbsolute(double value)
    {
        return new SvgPathCommandHorizontalLineTo(false, SvgLengthUnit.None, value);
    }

    public static SvgPathCommandHorizontalLineTo CreateRelative(double value)
    {
        return new SvgPathCommandHorizontalLineTo(true, SvgLengthUnit.None, value);
    }

    public static SvgPathCommandHorizontalLineTo Create(bool isRelative, double value)
    {
        return new SvgPathCommandHorizontalLineTo(isRelative, SvgLengthUnit.None, value);
    }


    public static SvgPathCommandHorizontalLineTo CreateAbsolute(SvgLengthUnit unit, double value)
    {
        return new SvgPathCommandHorizontalLineTo(false, unit, value);
    }

    public static SvgPathCommandHorizontalLineTo CreateRelative(SvgLengthUnit unit, double value)
    {
        return new SvgPathCommandHorizontalLineTo(true, unit, value);
    }

    public static SvgPathCommandHorizontalLineTo Create(bool isRelative, SvgLengthUnit unit, double value)
    {
        return new SvgPathCommandHorizontalLineTo(isRelative, unit, value);
    }

        
    public override char CommandSymbol 
        => IsRelative ? 'h' : 'H';

    public SvgLength Value { get; }

    public override string ValueText
    {
        get
        {
            var composer = new StringBuilder();

            composer
                .Append(CommandSymbol)
                .Append(' ')
                .Append(Value.ValueText);

            return composer.ToString();
        }
    }


    private SvgPathCommandHorizontalLineTo(bool isRelative, SvgLengthUnit unit, double value) 
        : base(isRelative)
    {
        Value = SvgLength.Create(unit, value);
    }

}