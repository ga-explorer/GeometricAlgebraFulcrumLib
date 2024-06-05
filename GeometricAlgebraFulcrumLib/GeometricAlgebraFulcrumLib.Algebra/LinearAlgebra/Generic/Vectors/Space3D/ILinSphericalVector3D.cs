using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinSphericalVector3D<T> :
    ILinVector3D<T>
{
    Scalar<T> R { get; }

    LinAngle<T> Theta { get; }

    LinAngle<T> Phi { get; }

    bool IsUnitVector();

    bool IsNearUnitVector();
}