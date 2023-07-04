using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Paths;

public abstract class SvgPathCommandCurveToEndPoint :
    SvgPathCommandSimple
{
    public SvgPoint EndPoint { get; } 


    protected SvgPathCommandCurveToEndPoint(bool isRelative, SvgPoint point) 
        : base(isRelative)
    {
        EndPoint = point;
    }
    
    protected SvgPathCommandCurveToEndPoint(bool isRelative, SvgLengthUnit unit, double x, double y) 
        : base(isRelative)
    {
        EndPoint = SvgPoint.Create(unit, x, y);
    }
}