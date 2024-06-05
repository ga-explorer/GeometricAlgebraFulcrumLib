using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

public sealed class SvgPathCommandLineTo : 
    SvgPathCommandCurveToEndPoint
{
    public static SvgPathCommandLineTo CreateAbsolute(double x, double y)
    {
        return new SvgPathCommandLineTo(false, SvgLengthUnit.None, x, y);
    }

    public static SvgPathCommandLineTo CreateRelative(double x, double y)
    {
        return new SvgPathCommandLineTo(false, SvgLengthUnit.None, x, y);
    }

    public static SvgPathCommandLineTo Create(bool isRelative, double x, double y)
    {
        return new SvgPathCommandLineTo(isRelative, SvgLengthUnit.None, x, y);
    }


    public static SvgPathCommandLineTo CreateAbsolute(IPair<double> point)
    {
        return new SvgPathCommandLineTo(false, SvgLengthUnit.None, point.Item1, point.Item2);
    }

    public static SvgPathCommandLineTo CreateRelative(IPair<double> point)
    {
        return new SvgPathCommandLineTo(false, SvgLengthUnit.None, point.Item1, point.Item2);
    }

    public static SvgPathCommandLineTo Create(bool isRelative, IPair<double> point)
    {
        return new SvgPathCommandLineTo(isRelative, SvgLengthUnit.None, point.Item1, point.Item2);
    }


    public static SvgPathCommandLineTo CreateAbsolute(SvgLengthUnit unit, double x, double y)
    {
        return new SvgPathCommandLineTo(false, unit, x, y);
    }

    public static SvgPathCommandLineTo CreateRelative(SvgLengthUnit unit, double x, double y)
    {
        return new SvgPathCommandLineTo(false, unit, x, y);
    }

    public static SvgPathCommandLineTo Create(bool isRelative, SvgLengthUnit unit, double x, double y)
    {
        return new SvgPathCommandLineTo(isRelative, unit, x, y);
    }
        

    public static SvgPathCommandLineTo CreateAbsolute(SvgLengthUnit unit, IPair<double> point)
    {
        return new SvgPathCommandLineTo(false, unit, point.Item1, point.Item2);
    }

    public static SvgPathCommandLineTo CreateRelative(SvgLengthUnit unit, IPair<double> point)
    {
        return new SvgPathCommandLineTo(false, unit, point.Item1, point.Item2);
    }

    public static SvgPathCommandLineTo Create(bool isRelative, SvgLengthUnit unit, IPair<double> point)
    {
        return new SvgPathCommandLineTo(isRelative, unit, point.Item1, point.Item2);
    }
        

    public static SvgPathCommandLineTo CreateAbsolute(SvgPoint endPoint)
    {
        return new SvgPathCommandLineTo(false, endPoint);
    }

    public static SvgPathCommandLineTo CreateRelative(SvgPoint endPoint)
    {
        return new SvgPathCommandLineTo(false, endPoint);
    }

    public static SvgPathCommandLineTo Create(bool isRelative, SvgPoint endPoint)
    {
        return new SvgPathCommandLineTo(isRelative, endPoint);
    }

        
    public override char CommandSymbol 
        => IsRelative ? 'l' : 'L';

    public override string ValueText
    {
        get
        {
            var composer = new StringBuilder();

            composer
                .Append(CommandSymbol)
                .Append(' ')
                .Append(EndPoint.ValueText);

            return composer.ToString();
        }
    }


    private SvgPathCommandLineTo(bool isRelative, SvgLengthUnit unit, double x, double y) 
        : base(isRelative, unit, x, y)
    {
    }
        
    private SvgPathCommandLineTo(bool isRelative, SvgPoint endPoint) 
        : base(isRelative, endPoint)
    {
    }

        
}