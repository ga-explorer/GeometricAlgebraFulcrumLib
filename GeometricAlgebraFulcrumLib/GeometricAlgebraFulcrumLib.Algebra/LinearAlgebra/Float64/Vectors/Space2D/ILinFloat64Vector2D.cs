using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64Vector2D :
    IFloat64LinearAlgebraElement,
    IPair<Float64Scalar>
{
    Float64Scalar X { get; }

    Float64Scalar Y { get; }
}