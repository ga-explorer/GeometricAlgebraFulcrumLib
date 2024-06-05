using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64Vector2D :
    IFloat64LinearAlgebraElement,
    IPair<Float64Scalar>
{
    Float64Scalar X { get; }

    Float64Scalar Y { get; }
}