using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;

public sealed record Float64Ray : 
    IFloat64Ray<LinFloat64Vector>
{
    public LinFloat64Vector Origin { get; }

    public LinFloat64Vector Direction { get; }


    public Float64Ray(LinFloat64Vector origin, LinFloat64Vector direction)
    {
        Origin = origin;
        Direction = direction;

        Debug.Assert(IsValid());
    }

    public void Deconstruct(out LinFloat64Vector origin, out LinFloat64Vector direction)
    {
        origin = Origin;
        direction = Direction;
    }


    public bool IsValid()
    {
        return Origin.IsValid() && Direction.IsValid();
    }

    public LinFloat64Vector PointAt(double t)
    {
        return Origin + t * Direction;
    }
}