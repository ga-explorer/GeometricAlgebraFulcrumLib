using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;

public interface IFloat64Ray<out TVector> : IAlgebraicElement
    where TVector : ILinFloat64Vector
{
    TVector Origin { get; }

    TVector Direction { get; }

    TVector PointAt(double t);
}