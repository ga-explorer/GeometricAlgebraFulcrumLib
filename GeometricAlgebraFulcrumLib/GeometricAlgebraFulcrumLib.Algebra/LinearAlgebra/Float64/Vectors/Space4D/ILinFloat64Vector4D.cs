using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinFloat64Vector4D :
    IFloat64LinearAlgebraElement,
    IQuad<Float64Scalar>
{
    Float64Scalar X { get; }

    Float64Scalar Y { get; }

    Float64Scalar Z { get; }

    Float64Scalar W { get; }
}