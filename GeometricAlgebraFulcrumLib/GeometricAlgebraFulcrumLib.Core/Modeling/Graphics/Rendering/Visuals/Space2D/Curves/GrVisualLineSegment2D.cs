using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space2D.Styles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space2D.Curves;

public sealed class GrVisualLineSegment2D :
    GrVisualCurve2D
{
    public ILinFloat64Vector2D Position1 { get; }

    public ILinFloat64Vector2D Position2 { get; }
        
    public LinFloat64Vector2D Direction 
        => Position1.GetDirectionTo(Position2);
        
    public LinFloat64Vector2D UnitDirection 
        => Position1.GetUnitDirectionTo(Position2);

    public override int PathPointCount 
        => 2;

    public override double Length 
        => Position1.GetDistanceToPoint(Position2);
        

    private GrVisualLineSegment2D(string name, IGrVisualCurveStyle2D style, ILinFloat64Vector2D position1, ILinFloat64Vector2D position2) 
        : base(name, style)
    {
        if (position1.VectorSubtract(position2).IsZero())
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