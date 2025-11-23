using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;

public sealed record Float64Ray4D : 
    IFloat64Ray<LinFloat64Vector4D>
{
    public LinFloat64Vector4D Origin { get; }

    public LinFloat64Vector4D Direction { get; }


    public Float64Ray4D(LinFloat64Vector4D origin, LinFloat64Vector4D direction)
    {
        Origin = origin;
        Direction = direction;

        Debug.Assert(IsValid());
    }

    public void Deconstruct(out LinFloat64Vector4D origin, out LinFloat64Vector4D direction)
    {
        origin = Origin;
        direction = Direction;
    }


    public bool IsValid()
    {
        return Origin.IsValid() && Direction.IsValid();
    }

    public LinFloat64Vector4D PointAt(double t)
    {
        return Origin + t * Direction;
    }
}