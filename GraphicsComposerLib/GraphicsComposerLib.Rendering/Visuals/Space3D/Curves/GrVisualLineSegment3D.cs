using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualLineSegment3D :
    GrVisualCurve3D
{
    public IFloat64Tuple3D Position1 { get; set; }

    public IFloat64Tuple3D Position2 { get; set; }


    public GrVisualLineSegment3D(string name) 
        : base(name)
    {
    }


    public double GetLength()
    {
        return Position1.GetDistanceToPoint(Position2);
    }
    
    public Float64Tuple3D GetDirection()
    {
        return Position1.GetDirectionTo(Position2);
    }
    
    public Float64Tuple3D GetUnitDirection()
    {
        return Position1.GetUnitDirectionTo(Position2);
    }
}