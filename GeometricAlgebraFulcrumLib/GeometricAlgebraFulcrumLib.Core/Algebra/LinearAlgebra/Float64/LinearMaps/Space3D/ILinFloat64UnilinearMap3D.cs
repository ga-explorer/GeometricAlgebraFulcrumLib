using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;

public interface ILinFloat64UnilinearMap3D :
    IFloat64LinearAlgebraElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    bool IsNearIdentity(double epsilon = 1e-12d);

    ILinFloat64UnilinearMap3D GetInverseMap();

    LinFloat64Vector3D MapBasisVector(int index);

    LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector);
}