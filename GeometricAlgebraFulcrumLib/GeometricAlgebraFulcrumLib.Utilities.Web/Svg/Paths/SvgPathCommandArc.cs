using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

/// <summary>
/// Elliptical Arc Path Command
/// </summary>
public sealed class SvgPathCommandArc : 
    SvgPathCommandCurveToEndPoint
{
    public static SvgPathCommandArc CreateAbsolute(double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
    {
        return new SvgPathCommandArc(
            false, 
            SvgLengthUnit.None, 
            radiusX, 
            radiusY, 
            xRotationAngle, 
            largeArcFlag, 
            sweepFlag, 
            point
        );
    }
        
    public static SvgPathCommandArc CreateRelative(double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
    {
        return new SvgPathCommandArc(
            true, 
            SvgLengthUnit.None, 
            radiusX, 
            radiusY, 
            xRotationAngle, 
            largeArcFlag, 
            sweepFlag, 
            point
        );
    }

    public static SvgPathCommandArc Create(bool isRelative, double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
    {
        return new SvgPathCommandArc(
            isRelative, 
            SvgLengthUnit.None, 
            radiusX, 
            radiusY, 
            xRotationAngle, 
            largeArcFlag, 
            sweepFlag, 
            point
        );
    }


    public static SvgPathCommandArc CreateAbsolute(SvgLengthUnit unit, double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
    {
        return new SvgPathCommandArc(false, unit, radiusX, radiusY, xRotationAngle, largeArcFlag, sweepFlag, point);
    }
        
    public static SvgPathCommandArc CreateRelative(SvgLengthUnit unit, double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
    {
        return new SvgPathCommandArc(true, unit, radiusX, radiusY, xRotationAngle, largeArcFlag, sweepFlag, point);
    }

    public static SvgPathCommandArc Create(bool isRelative, SvgLengthUnit unit, double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
    {
        return new SvgPathCommandArc(isRelative, unit, radiusX, radiusY, xRotationAngle, largeArcFlag, sweepFlag, point);
    }


    public static SvgPathCommandArc CreateAbsolute(SvgLength radiusX, SvgLength radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, SvgPoint point)
    {
        return new SvgPathCommandArc(false, radiusX, radiusY, xRotationAngle, largeArcFlag, sweepFlag, point);
    }
        
    public static SvgPathCommandArc CreateRelative(SvgLength radiusX, SvgLength radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, SvgPoint point)
    {
        return new SvgPathCommandArc(true, radiusX, radiusY, xRotationAngle, largeArcFlag, sweepFlag, point);
    }

    public static SvgPathCommandArc Create(bool isRelative, SvgLength radiusX, SvgLength radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, SvgPoint point)
    {
        return new SvgPathCommandArc(isRelative, radiusX, radiusY, xRotationAngle, largeArcFlag, sweepFlag, point);
    }
        

    public SvgLength RadiusX { get; }

    public SvgLength RadiusY { get; }

    public double XRotationAngle { get; }

    public bool LargeArcFlag { get; }

    public bool SweepFlag { get; }

    public override char CommandSymbol 
        => IsRelative ? 'a' : 'A';

    public override string ValueText 
        => new StringBuilder()
            .Append(CommandSymbol)
            .Append(' ')
            .Append(RadiusX.ValueText)
            .Append(' ')
            .Append(RadiusY.ValueText)
            .Append(' ')
            .Append(XRotationAngle)
            .Append(' ')
            .Append(LargeArcFlag ? '1' : '0')
            .Append(' ')
            .Append(SweepFlag ? '1' : '0')
            .Append(' ')
            .Append(EndPoint.ValueText)
            .ToString();


    private SvgPathCommandArc(bool isRelative, SvgLength radiusX, SvgLength radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, SvgPoint point)
        : base(isRelative, point)
    {
        RadiusX = radiusX;
        RadiusY = radiusY;
        XRotationAngle = xRotationAngle;
        LargeArcFlag = largeArcFlag;
        SweepFlag = sweepFlag;
    }

    private SvgPathCommandArc(bool isRelative, SvgLengthUnit unit, double radiusX, double radiusY, double xRotationAngle, bool largeArcFlag, bool sweepFlag, IPair<double> point)
        : base(isRelative, unit.CreatePoint(point))
    {
        RadiusX = unit.CreateLength(radiusX);
        RadiusY = unit.CreateLength(radiusY);
        XRotationAngle = xRotationAngle;
        LargeArcFlag = largeArcFlag;
        SweepFlag = sweepFlag;
    }

}