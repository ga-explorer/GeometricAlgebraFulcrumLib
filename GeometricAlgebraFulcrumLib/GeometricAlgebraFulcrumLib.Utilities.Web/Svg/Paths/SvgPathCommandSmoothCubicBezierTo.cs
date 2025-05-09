using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

/// <summary>
/// Cubic Bezier Path Command
/// </summary>
public sealed class SvgPathCommandSmoothCubicBezierTo : 
    SvgPathCommandCurveToEndPoint
{
    public static SvgPathCommandSmoothCubicBezierTo CreateAbsolute(IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(
            false, 
            SvgLengthUnit.None, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothCubicBezierTo CreateRelative(IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(
            true, 
            SvgLengthUnit.None, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothCubicBezierTo Create(bool isRelative, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(
            isRelative, 
            SvgLengthUnit.None, 
            endControlPoint, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandSmoothCubicBezierTo CreateAbsolute(SvgLengthUnit unit, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(
            false, 
            unit, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothCubicBezierTo CreateRelative(SvgLengthUnit unit, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(
            true, 
            unit, 
            endControlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothCubicBezierTo Create(bool isRelative, SvgLengthUnit unit, IPair<double> endControlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(
            isRelative, 
            unit, 
            endControlPoint, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandSmoothCubicBezierTo CreateAbsolute(SvgPoint endControlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(false, endControlPoint, endPoint);
    }
        
    public static SvgPathCommandSmoothCubicBezierTo CreateRelative(SvgPoint endControlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(false, endControlPoint, endPoint);
    }

    public static SvgPathCommandSmoothCubicBezierTo Create(bool isRelative, SvgPoint endControlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandSmoothCubicBezierTo(isRelative, endControlPoint, endPoint);
    }

    
    public SvgPoint EndControlPoint { get; }

    public override char CommandSymbol 
        => IsRelative ? 's' : 'S';
        
    public override string ValueText
    {
        get
        {
            var composer = new StringBuilder();

            composer
                .Append(CommandSymbol)
                .Append(' ')
                .Append(EndControlPoint.ValueText)
                .Append(' ')
                .Append(EndPoint.ValueText);

            return composer.ToString();
        }
    }

        
    private SvgPathCommandSmoothCubicBezierTo(bool isRelative, SvgLengthUnit unit, IPair<double> endControlPoint, IPair<double> endPoint) 
        : base(isRelative, unit.CreatePoint(endPoint))
    {
        EndControlPoint = unit.CreatePoint(endControlPoint);
    }

    private SvgPathCommandSmoothCubicBezierTo(bool isRelative, SvgPoint endControlPoint, SvgPoint endPoint) 
        : base(isRelative, endPoint)
    {
        EndControlPoint = endControlPoint;
    }

}