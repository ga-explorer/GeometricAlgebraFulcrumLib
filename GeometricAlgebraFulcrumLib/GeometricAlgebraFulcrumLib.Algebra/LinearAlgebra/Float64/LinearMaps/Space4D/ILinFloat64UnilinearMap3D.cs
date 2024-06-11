using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D;

public interface ILinFloat64UnilinearMap4D :
    IFloat64LinearAlgebraElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    bool IsNearIdentity(double epsilon = 1e-12d);

    ILinFloat64UnilinearMap4D GetInverseMap();

    LinFloat64Vector4D MapBasisVector(int index);

    LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector);
}