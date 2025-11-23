using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;

public sealed record Float64Ray2D : 
    IFloat64Ray<LinFloat64Vector2D>
{
    public LinFloat64Vector2D Origin { get; }

    public LinFloat64Vector2D Direction { get; }


    public Float64Ray2D(LinFloat64Vector2D origin, LinFloat64Vector2D direction)
    {
        Origin = origin;
        Direction = direction;

        Debug.Assert(IsValid());
    }

    public void Deconstruct(out LinFloat64Vector2D origin, out LinFloat64Vector2D direction)
    {
        origin = Origin;
        direction = Direction;
    }


    public bool IsValid()
    {
        return Origin.IsValid() && Direction.IsValid();
    }

    public LinFloat64Vector2D PointAt(double t)
    {
        return Origin + t * Direction;
    }
}