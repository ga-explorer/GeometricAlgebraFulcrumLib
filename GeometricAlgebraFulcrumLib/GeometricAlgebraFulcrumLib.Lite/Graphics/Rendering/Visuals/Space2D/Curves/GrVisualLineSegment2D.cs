using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Curves;

public sealed class GrVisualLineSegment2D :
    GrVisualCurve2D
{
    public IFloat64Vector2D Position1 { get; }

    public IFloat64Vector2D Position2 { get; }
        
    public Float64Vector2D Direction 
        => Position1.GetDirectionTo(Position2);
        
    public Float64Vector2D UnitDirection 
        => Position1.GetUnitDirectionTo(Position2);

    public override int PathPointCount 
        => 2;

    public override double Length 
        => Position1.GetDistanceToPoint(Position2);
        

    private GrVisualLineSegment2D(string name, IGrVisualCurveStyle2D style, IFloat64Vector2D position1, IFloat64Vector2D position2) 
        : base(name, style)
    {
        if (position1.Subtract(position2).IsZero())
            throw new InvalidOperationException();

        Position1 = position1;
        Position2 = position2;

        Debug.Assert(IsValid());
    }

        
    public override bool IsValid()
    {
        return Position1.IsValid() &&
               Position2.IsValid();
    }

    public override IPointsPath2D GetPositionsPath()
    {
        return new LinearPointsPath2D(
            Position1, 
            Position2, 
            2
        );
    }
        
}