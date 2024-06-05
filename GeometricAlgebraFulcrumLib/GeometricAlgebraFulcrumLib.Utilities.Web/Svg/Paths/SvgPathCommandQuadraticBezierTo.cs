using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

public sealed class SvgPathCommandQuadraticBezierTo : 
    SvgPathCommandCurveToEndPoint
{
    public static SvgPathCommandQuadraticBezierTo CreateAbsolute(IPair<double> controlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            false, 
            SvgLengthUnit.None, 
            controlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandQuadraticBezierTo CreateRelative(IPair<double> controlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            true, 
            SvgLengthUnit.None, 
            controlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandQuadraticBezierTo Create(bool isRelative, IPair<double> controlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            isRelative, 
            SvgLengthUnit.None, 
            controlPoint, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandQuadraticBezierTo CreateAbsolute(SvgLengthUnit unit, IPair<double> controlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            false, 
            unit, 
            controlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandQuadraticBezierTo CreateRelative(SvgLengthUnit unit, IPair<double> controlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            true, 
            unit, 
            controlPoint, 
            endPoint
        );
    }
        
    public static SvgPathCommandQuadraticBezierTo Create(bool isRelative, SvgLengthUnit unit, IPair<double> controlPoint, IPair<double> endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            isRelative, 
            unit, 
            controlPoint, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandQuadraticBezierTo CreateAbsolute(SvgPoint controlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            false, 
            controlPoint,
            endPoint
        );
    }
        
    public static SvgPathCommandQuadraticBezierTo CreateRelative(SvgPoint controlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            false, 
            controlPoint, 
            endPoint
        );
    }

    public static SvgPathCommandQuadraticBezierTo Create(bool isRelative, SvgPoint controlPoint, SvgPoint endPoint)
    {
        return new SvgPathCommandQuadraticBezierTo(
            isRelative, 
            controlPoint, 
            endPoint
        );
    }


    public SvgPoint ControlPoint { get; }
    
    public override char CommandSymbol 
        => IsRelative ? 'q' : 'Q';
        
    public override string ValueText
    {
        get
        {
            var composer = new StringBuilder();

            composer
                .Append(CommandSymbol)
                .Append(' ')
                .Append(ControlPoint.ValueText)
                .Append(' ')
                .Append(EndPoint.ValueText);

            return composer.ToString();
        }
    }

        
    private SvgPathCommandQuadraticBezierTo(bool isRelative, SvgLengthUnit unit, IPair<double> controlPoint, IPair<double> endPoint) 
        : base(isRelative, unit.CreatePoint(endPoint))
    {
        ControlPoint = unit.CreatePoint(controlPoint);
    }

    private SvgPathCommandQuadraticBezierTo(bool isRelative, SvgPoint controlPoint, SvgPoint endPoint) 
        : base(isRelative, endPoint)
    {
        ControlPoint = controlPoint;
    }
}