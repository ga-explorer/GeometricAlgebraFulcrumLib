using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

public sealed class SvgPathCommandMoveTo : 
    SvgPathCommandCurveToEndPoint
{
    public static SvgPathCommandMoveTo CreateAbsolute(double x, double y)
    {
        return new SvgPathCommandMoveTo(false, SvgLengthUnit.None, x, y);
    }

    public static SvgPathCommandMoveTo CreateRelative(double x, double y)
    {
        return new SvgPathCommandMoveTo(false, SvgLengthUnit.None, x, y);
    }

    public static SvgPathCommandMoveTo Create(bool isRelative, double x, double y)
    {
        return new SvgPathCommandMoveTo(isRelative, SvgLengthUnit.None, x, y);
    }


    public static SvgPathCommandMoveTo CreateAbsolute(IPair<double> point)
    {
        return new SvgPathCommandMoveTo(false, SvgLengthUnit.None, point.Item1, point.Item2);
    }

    public static SvgPathCommandMoveTo CreateRelative(IPair<double> point)
    {
        return new SvgPathCommandMoveTo(false, SvgLengthUnit.None, point.Item1, point.Item2);
    }

    public static SvgPathCommandMoveTo Create(bool isRelative, IPair<double> point)
    {
        return new SvgPathCommandMoveTo(isRelative, SvgLengthUnit.None, point.Item1, point.Item2);
    }


    public static SvgPathCommandMoveTo CreateAbsolute(SvgLengthUnit unit, double x, double y)
    {
        return new SvgPathCommandMoveTo(false, unit, x, y);
    }

    public static SvgPathCommandMoveTo CreateRelative(SvgLengthUnit unit, double x, double y)
    {
        return new SvgPathCommandMoveTo(false, unit, x, y);
    }

    public static SvgPathCommandMoveTo Create(bool isRelative, SvgLengthUnit unit, double x, double y)
    {
        return new SvgPathCommandMoveTo(isRelative, unit, x, y);
    }
        

    public static SvgPathCommandMoveTo CreateAbsolute(SvgPoint endPoint)
    {
        return new SvgPathCommandMoveTo(false, endPoint);
    }

    public static SvgPathCommandMoveTo CreateRelative(SvgPoint endPoint)
    {
        return new SvgPathCommandMoveTo(false, endPoint);
    }

    public static SvgPathCommandMoveTo Create(bool isRelative, SvgPoint endPoint)
    {
        return new SvgPathCommandMoveTo(isRelative, endPoint);
    }


    public static SvgPathCommandMoveTo CreateAbsolute(SvgLengthUnit unit, IPair<double> point)
    {
        return new SvgPathCommandMoveTo(false, unit, point.Item1, point.Item2);
    }

    public static SvgPathCommandMoveTo CreateRelative(SvgLengthUnit unit, IPair<double> point)
    {
        return new SvgPathCommandMoveTo(false, unit, point.Item1, point.Item2);
    }

    public static SvgPathCommandMoveTo Create(bool isRelative, SvgLengthUnit unit, IPair<double> point)
    {
        return new SvgPathCommandMoveTo(isRelative, unit, point.Item1, point.Item2);
    }

        
    public override char CommandSymbol 
        => IsRelative ? 'm' : 'M';

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


    private SvgPathCommandMoveTo(bool isRelative, SvgLengthUnit unit, double x, double y)
        : base(isRelative, unit, x, y)
    {
    }
        
    private SvgPathCommandMoveTo(bool isRelative, SvgPoint endPoint) 
        : base(isRelative, endPoint)
    {
    }

        
}