using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

/// <summary>
/// Cubic Bezier Path Command
/// </summary>
public sealed class SvgPathCommandCubicBezierTo : 
    SvgPathCommandCurveToEndPoint
{
    public static SvgPathCommandCubicBezierTo CreateAbsolute(IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandCubicBezierTo(
            false, 
            SvgLengthUnit.None, 
            startControlPoint, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandCubicBezierTo CreateRelative(IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandCubicBezierTo(
            true, 
            SvgLengthUnit.None, 
            startControlPoint, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandCubicBezierTo Create(bool isRelative, IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandCubicBezierTo(
            isRelative, 
            SvgLengthUnit.None, 
            startControlPoint, 
            endControlPoint, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandCubicBezierTo CreateAbsolute(SvgLengthUnit unit, IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandCubicBezierTo(
            false, 
            unit, 
            startControlPoint, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandCubicBezierTo CreateRelative(SvgLengthUnit unit, IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandCubicBezierTo(
            true, 
            unit, 
            startControlPoint, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandCubicBezierTo Create(bool isRelative, SvgLengthUnit unit, IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandCubicBezierTo(
            isRelative, 
            unit, 
            startControlPoint, 
            endControlPoint, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandCubicBezierTo CreateAbsolute(SvgPoint startControlPoint, SvgPoint endControlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandCubicBezierTo(false, startControlPoint, endControlPoint, endPoint);
    }
        
    public static SvgPathCommandCubicBezierTo CreateRelative(SvgPoint startControlPoint, SvgPoint endControlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandCubicBezierTo(false, startControlPoint, endControlPoint, endPoint);
    }

    public static SvgPathCommandCubicBezierTo Create(bool isRelative, SvgPoint startControlPoint, SvgPoint endControlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandCubicBezierTo(isRelative, startControlPoint, endControlPoint, endPoint);
    }


    public SvgPoint StartControlPoint { get; }

    public SvgPoint EndControlPoint { get; }

    public override char CommandSymbol 
        => IsRelative ? 'c' : 'C';
        
    public override string ValueText
    {
        get
        {
            var composer = new StringBuilder();

            composer
                .Append(CommandSymbol)
                .Append(' ')
                .Append(StartControlPoint.ValueText)
                .Append(' ')
                .Append(EndControlPoint.ValueText)
                .Append(' ')
                .Append(EndPoint.ValueText);

            return composer.ToString();
        }
    }

        
    private SvgPathCommandCubicBezierTo(bool isRelative, SvgLengthUnit unit, IPair<double> startControlPoint, IPair<double> endControlPoint, IPair<double> endPoint) 
        : base(isRelative, unit.CreatePoint(endPoint))
    {
        StartControlPoint = unit.CreatePoint(startControlPoint);
        EndControlPoint = unit.CreatePoint(endControlPoint);
    }

    private SvgPathCommandCubicBezierTo(bool isRelative, SvgPoint startControlPoint, SvgPoint endControlPoint, SvgPoint endPoint) 
        : base(isRelative, endPoint)
    {
        StartControlPoint = startControlPoint;
        EndControlPoint = endControlPoint;
    }

}