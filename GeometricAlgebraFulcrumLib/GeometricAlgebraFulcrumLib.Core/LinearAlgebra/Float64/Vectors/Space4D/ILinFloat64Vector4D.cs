using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinFloat64Vector4D :
    IFloat64LinearAlgebraElement,
    IQuad<Float64Scalar>
{
    Float64Scalar X { get; }

    Float64Scalar Y { get; }

    Float64Scalar Z { get; }

    Float64Scalar W { get; }
}