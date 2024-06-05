using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinFloat64Vector4D :
    IFloat64LinearAlgebraElement,
    IQuad<Float64Scalar>
{
    Float64Scalar X { get; }

    Float64Scalar Y { get; }

    Float64Scalar Z { get; }

    Float64Scalar W { get; }
}