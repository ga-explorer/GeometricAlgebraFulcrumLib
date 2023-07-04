using System.Text;
using DataStructuresLib.Basic;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths;

public sealed class SvgPathCommandSmoothQuadraticBezierTo : 
    SvgPathCommandCurveToEndPoint
{
    public static SvgPathCommandSmoothQuadraticBezierTo CreateAbsolute(IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            false, 
            SvgLengthUnit.None, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothQuadraticBezierTo CreateRelative(IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            true, 
            SvgLengthUnit.None, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothQuadraticBezierTo Create(bool isRelative, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            isRelative, 
            SvgLengthUnit.None, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandSmoothQuadraticBezierTo CreateAbsolute(SvgLengthUnit unit, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            false, 
            unit, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothQuadraticBezierTo CreateRelative(SvgLengthUnit unit, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            true, 
            unit, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothQuadraticBezierTo Create(bool isRelative, SvgLengthUnit unit, IPair<double> endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            isRelative, 
            unit, 
            endPoint
        );
    }
        
        
    public static SvgPathCommandSmoothQuadraticBezierTo CreateAbsolute(SvgPoint endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            false, 
            endPoint
        );
    }
        
    public static SvgPathCommandSmoothQuadraticBezierTo CreateRelative(SvgPoint endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            false, 
            endPoint
        );
    }

    public static SvgPathCommandSmoothQuadraticBezierTo Create(bool isRelative, SvgPoint endPoint)
    {
        return new SvgPathCommandSmoothQuadraticBezierTo(
            isRelative, 
            endPoint
        );
    }

    
    public override char CommandSymbol 
        => IsRelative ? 't' : 'T';
        
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

        
    private SvgPathCommandSmoothQuadraticBezierTo(bool isRelative, SvgLengthUnit unit, IPair<double> endPoint) 
        : base(isRelative, unit.CreatePoint(endPoint))
    {
    }

    private SvgPathCommandSmoothQuadraticBezierTo(bool isRelative, SvgPoint endPoint) 
        : base(isRelative, endPoint)
    {
    }
}