using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;

public sealed record Float64Ray3D : 
    IFloat64Ray<LinFloat64Vector3D>
{
    public LinFloat64Vector3D Origin { get; }

    public LinFloat64Vector3D Direction { get; }


    public Float64Ray3D(LinFloat64Vector3D origin, LinFloat64Vector3D direction)
    {
        Origin = origin;
        Direction = direction;

        Debug.Assert(IsValid());
    }

    public void Deconstruct(out LinFloat64Vector3D origin, out LinFloat64Vector3D direction)
    {
        origin = Origin;
        direction = Direction;
    }


    public bool IsValid()
    {
        return Origin.IsValid() && Direction.IsValid();
    }

    public LinFloat64Vector3D PointAt(double t)
    {
        return Origin + t * Direction;
    }
}