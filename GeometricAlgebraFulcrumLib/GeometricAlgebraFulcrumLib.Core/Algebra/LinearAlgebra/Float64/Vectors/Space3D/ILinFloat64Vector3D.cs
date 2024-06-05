using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinFloat64Vector3D :
    IFloat64LinearAlgebraElement,
    ITriplet<Float64Scalar>
{
    Float64Scalar X { get; }

    Float64Scalar Y { get; }

    Float64Scalar Z { get; }
}