using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinFloat64Vector3D :
    IFloat64LinearAlgebraElement,
    ITriplet<Float64Scalar>
{
    Float64Scalar X { get; }

    Float64Scalar Y { get; }

    Float64Scalar Z { get; }
}